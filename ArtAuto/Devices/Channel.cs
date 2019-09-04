using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class Channel
    {
        public Channel(int wire)
        {
            Wire = wire;
        }

        public int Wire
        {
            get;
            private set;
        }        
    }
}
