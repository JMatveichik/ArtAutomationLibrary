using Advantech.Adam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices.ADAM6000
{
    public class Adam6050 : Adam6000Base, IDiscreteInputDevice, IDiscreteOutputDevice
    {
     
        public Adam6050()
        {
            AdamModel = Adam6000Type.Adam6050;
            DiscreteOutputs = new List<DiscreteOutput>();
            DiscreteInputs = new List<DiscreteInput>();
        }  

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя устройства</param>
        /// <param name="description">Описание </param>
        /// <param name="updateInterval">Интервал обновления данных</param>
        public Adam6050(string name, string description, int updateInterval) : base("ADAM-6050", name, description, updateInterval)
        {
            AdamModel = Adam6000Type.Adam6050;            
            DiscreteOutputs = new List<DiscreteOutput> () ;
            DiscreteInputs = new List<DiscreteInput>();
        }

        #region ДИСКРЕТНЫЕ ВХОДЫ
        public List<DiscreteInput> DiscreteInputs
        {
            get;
            private set;
        }

        public void UpdateDiscreteInputs()
        {
            readDiscreteInputs();
        }

        #endregion

        #region ДИСКРЕТНЫЕ ВЫХОДЫ

        /// <summary>
        /// Список дискретных выходов
        /// </summary>
        public List<DiscreteOutput> DiscreteOutputs
        {
            get;
            private set;
        }

        /// <summary>
        /// Установить цифровой выход в заданное состояние
        /// </summary>
        /// <param name="wire">Номер выхода</param>
        /// <param name="state">Заданное состояние</param>
        public void SetDiscreteOutput(int wire, bool state)
        {
            try
            {
                setDiscreteOutput(wire, state);
            }
            catch(DeviceIOException de)
            {
                log.Error("Set discrete output {0} to {1} filed for device {2} [{3}] : {4}", wire, state, de.Device.Model, de.Device.Name, de.Message);

                //reconnect and try set again
                Reconnect();
                setDiscreteOutput(wire, state);
            }
        }

      


        /// <summary>
        /// Обновить состояние дискретных выходов
        /// </summary>
        public void UpdateDiscreteOutputs()
        {
           readDiscreteOutputs();
        }

        #endregion
        

        #region ПЕРЕОПРЕДЕЛЕННЫЕ МЕТОДЫ

        /// <summary>
        /// Инициализация модуля ввода/ввывода
        /// </summary>
        /// <returns></returns>
        protected override bool Initialize()
        {
            if (!base.Initialize())
                return false;
            
            /// Число дискретных входов
            int iDiCount = Advantech.Adam.DigitalInput.GetChannelTotal(AdamModel);
            for (int i = 0; i < iDiCount; i++)
                DiscreteInputs.Add(new DiscreteInput(this, i));


            /// Число дискретных выходов        
            int iDoCount = Advantech.Adam.DigitalOutput.GetChannelTotal(AdamModel);

            for (int i = 0; i < iDoCount; i++)
                DiscreteOutputs.Add(new DiscreteOutput(this, i));

            UpdateData();

            return true;
        }

       

        /// <summary>
        /// Обновление данных устройства
        /// </summary>
        protected override void UpdateData()
        {
            try
            {
                ///ОБНОВЛЯЕМ СОСТОЯНИЕ АНАЛОГОВЫХ ВХОДОВ
                UpdateDiscreteInputs();

                ///ОБНОВЛЯЕМ СОСТОЯНИЕ ДИСКРЕТНЫХ ВЫХОДОВ
                UpdateDiscreteOutputs();                

            }
            catch(DeviceIOException de)
            {
                log.Error("Update data failed for device {0} [{1}]: {2}",  Model, Name, de.Message);
                Reconnect();
            }
        }
        #endregion
       
    }
}
