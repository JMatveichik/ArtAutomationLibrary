using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class AnalogOutput : Channel
    {
        public AnalogOutput(IAnalogOutputDevice dev, int wire, MeassureRangeInfo mr) : base (wire)
        {
            ChannelInfo = mr;
            Value = 0.0;
            Parent = dev; 
                                   
        }


        public IAnalogOutputDevice Parent
        {
            get;
            private set;
        }

        public MeassureRangeInfo ChannelInfo
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }
    }
}
