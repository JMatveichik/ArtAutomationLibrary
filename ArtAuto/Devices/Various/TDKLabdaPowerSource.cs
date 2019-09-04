using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArtAuto.Devices.Various
{
    public class TDKLabdaPowerSource : BaseDevice, IAnalogInputDevice, IAnalogOutputDevice, IDiscreteOutputDevice
    {
        public TDKLabdaPowerSource(string name, string desc, int updateInterval) :
            base("TDK-Lambda", "Z+ 100-2", name, desc, updateInterval)
        {
            AnalogInputs = new List<AnalogInput>();
            AnalogOutputs = new List<AnalogOutput>();
            DiscreteOutputs = new List<DiscreteOutput>();
        }

        public TDKLabdaPowerSource()
        {
            AnalogInputs = new List<AnalogInput>();
            AnalogOutputs = new List<AnalogOutput>();
            DiscreteOutputs = new List<DiscreteOutput>();

            Manufacturer = "TDK-Lambda";
            Model = "Z+ 100-2";//COM.ReadLine();
        }

        private SerialPort COM = null;

        public List<AnalogInput> AnalogInputs
        {
            get;
            private set;
        }

        public List<AnalogOutput> AnalogOutputs
        {
            get;
            private set;
        }

        public List<DiscreteOutput> DiscreteOutputs
        {
            get;
            private set;
        }

        public int DeviceAddress
        {
            get;
            private set;
        }
        public override bool Connect(string connection, int timeout)
        {

            string[] addr = connection.Split(':');

            if (addr.GetLength(0) != 3)
                throw new ArgumentException("Invalid connection string");

            string name = addr[0];
            DeviceAddress = int.Parse(addr[1]);

            int baud = int.Parse(addr[2]);

            COM = new SerialPort();
            COM.BaudRate = baud;
            COM.PortName = name;
            COM.ReadTimeout = timeout;
            COM.WriteTimeout = timeout;
            COM.Parity = Parity.None;
            COM.StopBits = StopBits.One;

            try
            {
                COM.Open();
            }
            catch (Exception e)
            {
                log.Fatal("Connection exception : {0}", e.Message);
                return false;
            }


            return Initialize();
        }

        public override void Disconnect()
        {
            COM.Close();
        }

        protected override bool Initialize()
        {
            if (!COM.IsOpen)
                return false;

            string cmd = string.Format("ADR {0}\r", DeviceAddress);
            COM.Write(cmd);
            Thread.Sleep(100);

            string res = COM.ReadTo("\r");

            if (!res.Contains("OK"))
                return false;

            /*
            COM.Write("IDN?\r");
            Thread.Sleep(100);
          
            /*string[] str = Model.Split(',', '-');

            if (str.GetLength(0) != 3)
                return false;
            */

            double nv = 100.0; //double.Parse(str[1].Substring(1));
            double nc = 2.0;   //double.Parse(str[2]);

            AnalogInputs.Add(new AnalogInput(this, 0, new MeassureRangeInfo(0, "Measured voltage", "V", 0.0, nv)));
            AnalogInputs.Add(new AnalogInput(this, 1, new MeassureRangeInfo(1, "Measured current", "A", 0.0, nc)));
            AnalogInputs.Add(new AnalogInput(this, 2, new MeassureRangeInfo(0, "High voltage defence level", "V", 0.0, nv)));
            AnalogInputs.Add(new AnalogInput(this, 3, new MeassureRangeInfo(0, "Low voltage defence level", "V", 0.0, nv)));

            AnalogOutputs.Add(new AnalogOutput(this, 0, new MeassureRangeInfo(0, "Set voltage", "V", 0.0, nv)));
            AnalogOutputs.Add(new AnalogOutput(this, 1, new MeassureRangeInfo(1, "Set current", "A", 0.0, nc)));

            DiscreteOutputs.Add(new DiscreteOutput(this, 0));

            cmd = string.Format("REV?\r");
            COM.Write(cmd);
            Thread.Sleep(100);

            FirmwareVersion = COM.ReadTo("\r");
            return true;

        }



        protected override void UpdateData()
        {
            if (!COM.IsOpen)
                throw new InvalidOperationException("Device not connected");

            string cmd;
            string res;

            lock (CommLock)
            {
                COM.DiscardInBuffer();
                COM.DiscardOutBuffer();

                cmd = string.Format("DVC?\r");
                COM.Write(cmd);

                Thread.Sleep(100);
                res = COM.ReadTo("\r");

                log.Trace("Command {0:10} ; Response {1:30} ", cmd, res);
            }

            string[] v = res.Split(',');
            if (v.GetLength(0) != 6)
                throw new FormatException(string.Format("Invalid response format for command : {0}", res));

            AnalogInputs[0].Value = double.Parse(v[0]);
            AnalogOutputs[0].Value = double.Parse(v[1]);

            AnalogInputs[1].Value = double.Parse(v[2]);
            AnalogOutputs[1].Value = double.Parse(v[3]);

            AnalogInputs[2].Value = double.Parse(v[4]);
            AnalogInputs[3].Value = double.Parse(v[5]);

            lock (CommLock)
            {
                cmd = string.Format("OUT?\r");
                COM.Write(cmd);

                res = COM.ReadTo("\r");
                res = res.ToUpper();

                log.Trace("Command {0:10} ; Response {1:30} ", cmd, res);
            }

            DiscreteOutputs[0].IsEnabled = res.Contains("ON");

        }

        public void UpdateAnalogInputs()
        {

        }

        public void UpdateAnalogOutputs()
        {

        }

        public void SetAnalogOutput(int wire, double value)
        {
            if (!COM.IsOpen)
                throw new InvalidOperationException("Device not connected");

            string cmd;
            string res;

            switch (wire)
            {
                case 0:
                    cmd = string.Format("PV {0:F5}\r", value);
                    break;
                case 1:
                    cmd = string.Format("PC {0:F5}\r", value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Analog output channel number {0} out of range"));
            }

            lock (CommLock)
            {
                COM.DiscardInBuffer();
                COM.DiscardOutBuffer();

                COM.Write(cmd);
                Thread.Sleep(100);

                res = COM.ReadTo("\r");

                log.Trace("Command {0:10} ; Response {1:30} ", cmd, res);

                if (!res.Contains("OK"))
                    throw new InvalidOperationException(string.Format("Writing to analog output failed. Error code {0}", res));
            }
        }

        public void UpdateDiscreteOutputs()
        {
        }

        public void SetDiscreteOutput(int wire, bool state)
        {
            if (!COM.IsOpen)
                throw new InvalidOperationException("Device not connected");

            if (wire != 0)
                throw new ArgumentOutOfRangeException(string.Format("Discrete output channel number {0} out of range"));

            string cmd = string.Format("OUT {0}\r", state ? 1 : 0); 
            string res;

            lock (CommLock)
            {
                COM.DiscardInBuffer();
                COM.DiscardOutBuffer();

                COM.Write(cmd);
                Thread.Sleep(100);

                res = COM.ReadTo("\r");
                log.Trace("Command {0:10} ; Response {1:30} ", cmd, res);
                
            }
        }
    }
}
