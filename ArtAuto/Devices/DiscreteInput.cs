using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class DiscreteInput : Channel
    {
        public DiscreteInput(IDiscreteInputDevice dev, int wire) : base (wire)
        {
           
            IsEnabled = false;
            Parent = dev;
        }

        public IDiscreteInputDevice Parent
        {
            get;
            private set;
        }

        public bool IsEnabled
        {
            get;
            set;
        }
    }
}
