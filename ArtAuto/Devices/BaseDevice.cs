using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtAuto.Common;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ArtAuto.Devices
{

 

    public abstract class BaseDevice : TimedControl
    {
        public BaseDevice() : base("unknown", "", 1000)
        {
            
        }

        public BaseDevice(string manufacturer, string model, string name, string description, int updateInterval) : base(name, description, updateInterval)
        {
            Manufacturer = manufacturer;
            Model = model;

            log = BaseDevice.PrepareLogger(name);
            log.Info("Create device object : {0} of ({1}) with name {2}", model, manufacturer, name);
        }



        /// <summary>
        /// Событие готовности данных 
        /// </summary>
        public event EventHandler DataReady;


        public static NLog.Logger PrepareLogger(string name)
        {
            if (LogManager.Configuration == null)
                LogManager.Configuration = new LoggingConfiguration();

            FileTarget fileTarget = (FileTarget)LogManager.Configuration.FindTargetByName(name);
            if (fileTarget == null)
            {
                fileTarget = new FileTarget(name);
                fileTarget.FileName = @"${basedir}/logs/devices/${shortdate}_" + name + @".log";

                FileTarget template = (FileTarget)LogManager.Configuration.FindTargetByName("flog");
                if (template != null)
                {
                    fileTarget.Layout = template.Layout;
                }
                else
                {
                    fileTarget.Layout = @"${longdate} ${uppercase:${level}} ${message}";
                }

                LogManager.Configuration.AddTarget(fileTarget);
                var rule = new LoggingRule(name, LogLevel.Debug, fileTarget) { Final = true };

                LogManager.Configuration.LoggingRules.Add(rule);
                LogManager.ReconfigExistingLoggers();
            }

            return LogManager.GetLogger(name);
        }       

        /// <summary>
        /// Объект синхронизации коммуникаций
        /// </summary>
        protected object CommLock = new object();


        protected NLog.Logger log = null;
        

        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer
        {
            get;
            protected set;
        }

      
        /// <summary>
        /// Модель
        /// </summary>
        public string Model
        {
            get;
            protected set;
        }

        public string FirmwareVersion
        {
            get;
            protected set;
        }

        /// <summary>
        /// Соединение с устройством
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public abstract bool Connect(string connection, int timeout);

        /// <summary>
        /// Инициализация внутренних данных устройства
        /// </summary>
        /// <returns></returns>
        protected abstract bool Initialize();
        
        /// <summary>
        /// Разъеденение с устройством
        /// </summary>
        public abstract void Disconnect();        

        /// <summary>
        /// Процедура обновления данных 
        /// </summary>
        protected override void ControlProcedure()
        {
            try
            {
                UpdateData();

                if (DataReady != null)
                    DataReady(this, null);
            }
            catch (Exception ex)
            {
                log.Error("Exception in control procedure : {0}", ex.Message);
            }
        }

        /// <summary>
        /// Функция обновления данных
        /// </summary>
        protected abstract void UpdateData();


       
    }
}
