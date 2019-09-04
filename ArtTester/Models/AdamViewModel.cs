using ArtAuto.Devices;
using ArtAuto.Devices.ADAM6000;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace ArtTester.Models
{
    class AdamViewModel : ViewModelBase
    {
        public AdamViewModel(Adam6000Base dev)
        {

            Device = dev;

            dev.DataReady += DeviceDataReady;

            Info = string.Format("{0} {1} - [{2}]", dev.Name, dev.Model, dev.IP);

            if (dev is IAnalogInputDevice)
            {
                for (int i = 0; i < ((IAnalogInputDevice)dev).AnalogInputs.Count; i++)
                    AnalogInputs.Add(0.0);
            }

            if (dev is IAnalogOutputDevice)
            {
                for (int i = 0; i < ((IAnalogOutputDevice)dev).AnalogOutputs.Count; i++)
                    AnalogOutputs.Add(0.0);
            }

            if (dev is IDiscreteInputDevice)
            {
                for (int i = 0; i < ((IDiscreteInputDevice)dev).DiscreteInputs.Count; i++)
                    DiscreteInputs.Add(false);
            }

            if (dev is IDiscreteOutputDevice)
            {
                for (int i = 0; i < ((IDiscreteOutputDevice)dev).DiscreteOutputs.Count; i++)
                    DiscreteOutputs.Add(false);
            }

        }

        private string info;

        public string Info
        {
            get { return info; }
            set
            {
                if (info == value)
                    return;

                info = value;
                OnPropertyChanged("Info");
            }
        }






        private void DeviceDataReady(object sender, EventArgs e)
        {
            InvokeOnUIThread(() =>
            {
                if (sender is IAnalogInputDevice)
                {
                    IAnalogInputDevice dev = (IAnalogInputDevice)sender;
                    for (int i = 0; i < dev.AnalogInputs.Count; i++)
                        AnalogInputs[i] = dev.AnalogInputs[i].Value;

                }

                if (sender is IAnalogOutputDevice)
                {
                    IAnalogOutputDevice dev = (IAnalogOutputDevice)sender;
                    for (int i = 0; i < dev.AnalogOutputs.Count; i++)
                        AnalogOutputs.Add(dev.AnalogOutputs[i].Value);
                }

                if (sender is IDiscreteInputDevice)
                {
                    IDiscreteInputDevice dev = (IDiscreteInputDevice)sender;


                    for (int i = 0; i < dev.DiscreteInputs.Count; i++)
                        DiscreteInputs[i] = dev.DiscreteInputs[i].IsEnabled;
                }

                if (sender is IDiscreteOutputDevice)
                {
                    IDiscreteOutputDevice dev = (IDiscreteOutputDevice)sender;
                    for (int i = 0; i < dev.DiscreteOutputs.Count; i++)
                        DiscreteOutputs[i] = (dev.DiscreteOutputs[i].IsEnabled);
                }
            });
        }


        public ObservableCollection<double> AnalogInputs
        {
            get { return ai; }
            set
            {
                if (ai == value)
                    return;

                ai = value;
                OnPropertyChanged("AnalogInputs");
            }
        }
        ObservableCollection<double> ai = new ObservableCollection<double>();


        public Adam6000Base Device = null;
        public ObservableCollection<double> AnalogOutputs
        {
            get { return ao; }
            set
            {
                if (ao == value)
                    return;

                ao = value;
                OnPropertyChanged("AnalogOutputs");
            }
        }
        ObservableCollection<double> ao = new ObservableCollection<double>();

        public ObservableCollection<bool> DiscreteInputs
        {
            get { return di; }
            set
            {
                if (di == value)
                    return;

                di = value;
                OnPropertyChanged("DiscreteInputs");
            }
        }

        ObservableCollection<bool> di = new ObservableCollection<bool>();

        public ObservableCollection<bool> DiscreteOutputs
        {
            get { return dou; }
            set
            {
                if (dou == value)
                    return;

                dou = value;
                OnPropertyChanged("DiscreteOutputs");
            }
        }

        ObservableCollection<bool> dou = new ObservableCollection<bool>();


    }
}
