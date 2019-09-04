using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public class DeviceException : Exception
    {
        public DeviceException(BaseDevice dev, string message) : base(message)
        {
            Device = dev;
        }

        public BaseDevice Device
        {
            get;
            private set;
        }
    }

    public class NotSupportedInterfaceException : DeviceException
    {
        public NotSupportedInterfaceException(BaseDevice dev, string message) : base(dev, message)
        {

        }
    }

    public class DeviceConnectionException : DeviceException
    {
        public DeviceConnectionException(BaseDevice dev, string message) : base(dev, message)
        {
        }
    }

    public class DeviceIOException : DeviceException
    {
        public DeviceIOException(BaseDevice dev, string message) : base(dev, message)
        {

        }
    }
}
