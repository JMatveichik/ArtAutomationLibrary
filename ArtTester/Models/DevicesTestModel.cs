using ArtAuto.Devices.ADAM6000;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace ArtTester.Models
{
    class DevicesTestModel : ViewModelBase
    {

        public DevicesTestModel()
        {
            DevicesModels = new ObservableCollection<AdamViewModel>();

            A15 = new Adam6018("A15", "Analog input module", 1000);
          

            A16 = new Adam6018("A16", "Analog input module", 1000);
           
            A17 = new Adam6066("A17", "Discrete input/output module", 1000);
           

            A18 = new Adam6052("A18", "Discrete input/output module", 1000);
           

            A19 = new Adam6052("A19", "Discrete input/output module", 1000);
           

            A20 = new Adam6052("A20", "Discrete input/output module", 1000);
           

            A21 = new Adam6017("A21", "Analog input module", 1000);

            A111 = new Adam6050("A111", "Analog input module", 1000);

            A121 = new Adam6017("A121", "Analog input module", 1000);


        }

        public ObservableCollection<AdamViewModel> DevicesModels
        {
            get;
            private set;
        }

        private AdamViewModel dev;

        public AdamViewModel SelectedModel
        {
            get { return dev; }
            set
            {
                if (dev == value)
                    return;

                dev = value;
                OnPropertyChanged("SelectedModel");
            }
        }

        public bool Initialize()
        {
            if (A15.Connect("192.168.10.15", 1000))
                A15.Start();

            if (A16.Connect("192.168.10.16", 1000))
                A16.Start();

            if (A17.Connect("192.168.10.17", 1000))
                A17.Start();

            if (A18.Connect("192.168.10.18", 1000))
                A18.Start();

            if (A19.Connect("192.168.10.19", 1000))
                A19.Start();

            if (A20.Connect("192.168.10.20", 1000))
                A20.Start();

            if (A21.Connect("192.168.10.21", 1000))
                A21.Start();

            if (A111.Connect("192.168.10.111", 1000))
                A111.Start();

            if (A121.Connect("192.168.10.121", 1000))
                A121.Start();

            DevicesModels.Add(new AdamViewModel(A15));
            DevicesModels.Add(new AdamViewModel(A16));
            DevicesModels.Add(new AdamViewModel(A17));
            DevicesModels.Add(new AdamViewModel(A18));
            DevicesModels.Add(new AdamViewModel(A19));
            DevicesModels.Add(new AdamViewModel(A20));
            DevicesModels.Add(new AdamViewModel(A21));
            DevicesModels.Add(new AdamViewModel(A111));
            DevicesModels.Add(new AdamViewModel(A121));

            dev = DevicesModels[0];

            return true;
        }

        public Adam6050 A111
        {
            get;
            private set;
        }

        public Adam6017 A121
        {
            get;
            private set;
        }

        public Adam6018 A15
        {
            get;
            private set;
        }

        public Adam6018 A16
        {
            get;
            private set;
        }

        public Adam6066 A17
        {
            get;
            private set;
        }

        public Adam6052 A18
        {
            get;
            private set;
        }

        public Adam6052 A19
        {
            get;
            private set;
        }

        public Adam6052 A20
        {
            get;
            private set;
        }

        public Adam6017 A21
        {
            get;
            private set;
        }
    }
}
