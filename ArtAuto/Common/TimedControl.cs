using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArtAuto.Common
{
    public abstract class TimedControl : NamedObject
    {

        public TimedControl(string name, string description, int ms) : base(name, description)
        {
            Name = name;
            Description = description;
            Interval = TimeSpan.FromMilliseconds(ms);
        }

        public TimedControl(string name, string description, TimeSpan interval) : base(name, description)
        {
            Name = name;
            Description = description;
            Interval = interval;
        }


        public void Start()
        {
            if (controlThread != null)
            {
                Stop();
                controlThread = null;
            }

            controlThread = new Thread(new ThreadStart(ControlThreadFunction) );
            controlThread.Start();

            if (Started != null)
                Started(this, EventArgs.Empty);
        }

        public void Start(TimeSpan interval)
        {
            Interval = interval;
            Start();
        }

        public void Start(int ms)
        {
            Interval = TimeSpan.FromMilliseconds(ms);
            Start();
        }


        public void Stop()
        {
            stopEvent.Set();
            
        }

        public bool IsAlive
        {
            get {
                if (controlThread == null)
                    return false;

                return controlThread.IsAlive;
            }
        }

        public event EventHandler Started;
        public event EventHandler Stopped;

        protected virtual void OnStop()
        {

        }

        protected virtual void OnStart()
        {

        }

        protected abstract void ControlProcedure();

        private void ControlThreadFunction()
        {
            while (true)
            {
                //ожидаем
                if (IsIntermittent)
                {
                    DateTime sw = DateTime.Now;
                    TimeSpan wd = TimeSpan.FromMilliseconds(0);

                    while (true)
                    {
                        Thread.Sleep(MinDelay);

                        //проверяем флаг остановки
                        if (stopEvent.WaitOne(10))
                            break;
                        
                        wd = DateTime.Now - sw;
                        if (wd >= Interval)
                            break;
                    }

                }
                else
                {
                    Thread.Sleep(Interval);
                }

                //проверяем флаг остановки
                if (stopEvent.WaitOne(10))
                {
                    OnStop();
                    return;
                }

                //выполняем действие
                ControlProcedure();
            }
        }

        public bool IsIntermittent
        {
            get;
            set;

        } = false;

        /// <summary>
        /// Period for call object control procedure
        /// </summary>
        public TimeSpan Interval
        {
            get {
                lock (locker) {
                    return interval;
                }
            }
            set
            {
                lock (locker)
                {
                    if (interval != value)
                        interval = value;
                }
            }
        }


        private TimeSpan interval = TimeSpan.FromMilliseconds(1000);

        private const int MinDelay = 100;
        private const int BaseDelay = 250;

        /// <summary>
        /// Sync object
        /// </summary>
        private object locker = new object();

        /// <summary>
        /// Main thread object
        /// </summary>
        private Thread controlThread = null;


        /// <summary>
        /// Stop event
        /// </summary>
        private ManualResetEvent stopEvent = new ManualResetEvent(false);


    }
}
