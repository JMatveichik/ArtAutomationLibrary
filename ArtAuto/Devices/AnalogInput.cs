using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class AnalogInput : Channel
    {
        public AnalogInput(IAnalogInputDevice dev, int wire, MeassureRangeInfo mr) : base (wire)
        {            
            ChannelInfo = mr;
            Value = 0.0;
            Parent = dev;            
        }

        public IAnalogInputDevice Parent
        {
            get;
            private set;
        }

        public MeassureRangeInfo ChannelInfo
        {
            get;
            private set;
        }

        public double Value
        {
            get;
            set;
        }

        public ushort Status
        {
            get;
            set;
        }

    }
}
