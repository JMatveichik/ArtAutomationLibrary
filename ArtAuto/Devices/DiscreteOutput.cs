using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class DiscreteOutput : Channel
    {
        public DiscreteOutput(IDiscreteOutputDevice dev, int wire) : base (wire)
        {
            
            IsEnabled = false;
            Parent = dev;
        }

        public IDiscreteOutputDevice Parent
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
