using Advantech.Adam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ArtAuto.Devices.ADAM6000
{
    abstract public class Adam6000Base : BaseDevice
    {
        public Adam6000Base()
        {

        }

        public Adam6000Base(string model, string name, string description, int updateInterval) 
            : base("Advantech", model, name, description, updateInterval)
        {
            
        }

        /// <summary>
        /// IP Adress of ADAM-6000 series device
        /// </summary>
        public string IP
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }


        public TimeSpan ReconnectionTimeout
        {
            get;
            set;
        } = TimeSpan.FromSeconds(30.0);

        public bool Reconnect()
        {
            Stop();

            log.Trace("Try reconnect with device...");
            //Отключаем сокет
            if (Socket.Connected)
                Socket.Disconnect();

            //
            soc = new AdamSocket(AdamType.Adam6000);

            DateTime rs = DateTime.Now;
            while(true)
            {
                Thread.Sleep(1000);

                if (Socket.Connect(IP, ProtocolType.Tcp, Port) )
                    break;

                if (DateTime.Now - rs  > ReconnectionTimeout)
                    throw new DeviceConnectionException(this, "Reconnection timeout");                
            }

            Start();
            return true;
        }
        
        /// <summary>
        /// Новейшая версия прошивки 
        /// </summary>
        protected const int NewerFirmwareVersion = 5;

        /// <summary>
        /// Версия прошивки устройства
        /// </summary>
        protected int DeviceFirmwareVerion = 4;

        protected bool Connect(string ip, int port, int timeout)
        {
            
            Socket.SetTimeout(timeout, timeout, timeout);
            log.Debug("Connect to {0} port {1}...", ip, port);

            if (!Socket.Connect(ip, ProtocolType.Tcp, port))
            {
                log.Debug("Connection failed {0}", Socket.LastError);
                return false;
            }                

            Port = port;
            IP = ip;

            return Initialize(); 
        }


        public override bool Connect(string connection, int timeout)
        {
            string[] addr = connection.Split(':');
            log.Debug("Parse connection line : {0}", connection);

            string ip = "";
            int port = 502;

            if (addr.GetLength(0) == 2)
            {
                ip = addr[0];
                int.TryParse(addr[1], out port);
            }
            else
                ip = addr[0];
            return Connect(ip, port, timeout);
        }

        public override void Disconnect()
        {
            if (Socket != null)
                Socket.Disconnect();
        }

        protected override bool Initialize()
        {
            log.Info("Initialize device");
            AdamSocket adamUDP = new AdamSocket();
            adamUDP.SetTimeout(1000, 1000, 1000); // set timeout for UDP

            if (!adamUDP.Connect(AdamType.Adam6000, IP, ProtocolType.Udp))
            {
                log.Error("Connection to device with UDP");
                return false;
            }

            string ver;
            string name;

            if (adamUDP.Configuration().GetFirmwareVer(out ver))
                DeviceFirmwareVerion = int.Parse(ver.Trim().Substring(0, 1));

            FirmwareVersion = ver;
            if (adamUDP.Configuration().GetModuleName(out name))
            {
                log.Debug("Found device at IP ({0}) is {1}. Firmware version {2}", IP, name, ver);
            }

            adamUDP.Disconnect();
            return true;
        }


        private AdamSocket soc = new AdamSocket(AdamType.Adam6000);

        /// <summary>
        /// Device connection socket
        /// </summary>
        protected AdamSocket Socket
        {
            get { return soc; }            
        }

        /// <summary>
        /// ADAM-6000 device model
        /// </summary>
        public Adam6000Type AdamModel
        {
            get;
            protected set;
        }

        protected void readAnalogInputs( )
        {
            ///начальный регистр для данных аналоговых входов
            int iStart = 1;

            ///начальный регистр для статуса аналогового входа
            int iAiStatusStart = 101;  

            ///число аналоговых входов
            int iCount = Advantech.Adam.AnalogInput.GetChannelTotal(AdamModel);

            ///массив регистров данных
            int[] iData;

            //массив  регистров статуса
            int[] iAiStatus;

            IAnalogInputDevice aid = this as IAnalogInputDevice;

            if (aid == null)
                throw new NotSupportedInterfaceException(this, "Analog input operations are not supported for this type of device.");


            if (Socket == null)
                throw new DeviceConnectionException(this, "Socket not initialized yet.");

            lock (CommLock)
            {
                if (Socket.Modbus().ReadInputRegs(iStart, iCount, out iData))
                {
                    for (int iIdx = 0; iIdx < iCount; iIdx++)
                    {
                        ///для старых версий прошивки
                        if (DeviceFirmwareVerion < Adam6000Base.NewerFirmwareVersion)
                        {
                            byte rng = (byte)aid.AnalogInputs[iIdx].ChannelInfo.ID;
                            aid.AnalogInputs[iIdx].Value = Advantech.Adam.AnalogInput.GetScaledValue(AdamModel, rng, iData[iIdx]);
                        }
                        else
                        {
                            ushort rng = aid.AnalogInputs[iIdx].ChannelInfo.ID;
                            aid.AnalogInputs[iIdx].Value = Advantech.Adam.AnalogInput.GetScaledValue(AdamModel, rng, (ushort)iData[iIdx]);
                        }

                    }

                    if (Socket.Modbus().ReadInputRegs(iAiStatusStart, (iCount * 2), out iAiStatus))
                    {
                        for (int iIdx = 0; iIdx < iCount; iIdx++)
                            aid.AnalogInputs[iIdx].Status = (ushort)iAiStatus[(0 * 2)];
                    }
                    else
                    {
                        throw new DeviceIOException(this, string.Format("Read input registers failed. Erorr code : {0}", Socket.LastError));
                    }

                }
                else
                {
                    throw new DeviceIOException(this, string.Format("Read input registers failed. Erorr code : {0}", Socket.LastError));
                }

            }
        }

        protected void readDiscreteOutputs()
        {
            ///начальный регистр для данных дискретных выходов
            int iStart = 17;

            ///число дискретных выходов
            int iDoCount = Advantech.Adam.DigitalOutput.GetChannelTotal(AdamModel);

            IDiscreteOutputDevice dod = this as IDiscreteOutputDevice;

            if (dod == null)
                throw new NotSupportedInterfaceException(this, "Analog output operations are not supported for this type of device.");


            if (Socket == null)
                throw new DeviceConnectionException(this, "Socket not initialized yet.");


            bool[] bData = new bool[iDoCount];

            lock (CommLock)
            {

                if (Socket.Modbus().ReadCoilStatus(iStart, iDoCount, out bData))
                {
                    for (int iIdx = 0; iIdx < iDoCount; iIdx++)
                        dod.DiscreteOutputs[iIdx].IsEnabled = bData[iIdx];
                }
                else
                {
                    throw new DeviceIOException(this, string.Format("Read coil status for discrete outputs failed. Erorr code : {0}", Socket.LastError));
                }
            }
        }

        protected void readDiscreteInputs()
        {
            ///начальный регистр для данных дискретных выходов
            int iStart = 1;

            ///число дискретных входов
            int iDiCount = Advantech.Adam.DigitalInput.GetChannelTotal(AdamModel);

            IDiscreteInputDevice did = this as IDiscreteInputDevice;

            if (did == null)
                throw new NotSupportedInterfaceException(this, "Discrete input operations are not supported for this type of device.");

            if (Socket == null)
                throw new DeviceConnectionException(this, "Socket not initialized yet.");

            bool[] bData = new bool[iDiCount];

            lock (CommLock)
            {
                if (Socket.Modbus().ReadCoilStatus(iStart, iDiCount, out bData))
                {
                    for (int iIdx = 0; iIdx < iDiCount; iIdx++)
                        did.DiscreteInputs[iIdx].IsEnabled = bData[iIdx];                    
                }
                else
                {
                    throw new DeviceIOException(this, string.Format("Read coil status for discrete inputs failed. Erorr code : {0}", Socket.LastError));
                }
            }
        }

        protected void setDiscreteOutput(int wire, bool state)
        {
            ///начальный регистр для данных дискретных выходов
            int iStart = 17 + wire;

            log.Info("Set discrete output {0} to {1}...", wire, state);

            IDiscreteOutputDevice dod = this as IDiscreteOutputDevice;

            if (dod == null)
                throw new NotSupportedInterfaceException(this, "Discrete output operations are not supported for this type of device.");

            if (Socket == null)
                throw new DeviceConnectionException(this, "Socket not initialized yet.");

            log.Debug("Socket before: {0}  {1}  {2}", Socket.GetIP(), Socket.AdamSeriesType, Socket.GetProtocolType());

            lock (CommLock)
            {
                if (Socket.Modbus().ForceSingleCoil(iStart, state))
                {
                    log.Debug("Socket after: {0}  {1}  {2}", Socket.GetIP(), Socket.AdamSeriesType, Socket.GetProtocolType());
                    readDiscreteOutputs();
                }   
                else
                {
                    throw new DeviceIOException(this, string.Format("Force single coil failed. Erorr code : {0}", Socket.LastError));
                }
                    
            }
            
        }



    }
}
