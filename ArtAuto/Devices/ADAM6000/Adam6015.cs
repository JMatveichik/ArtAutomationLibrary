using Advantech.Adam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices.ADAM6000
{
    public class Adam6015 : Adam6000Base, IAnalogInputDevice, IDiscreteOutputDevice
    {
     
        public Adam6015()
        {
            AdamModel = Adam6000Type.Adam6015;            
        }  

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя устройства</param>
        /// <param name="description">Описание </param>
        /// <param name="updateInterval">Интервал обновления данных</param>
        public Adam6015(string name, string description, int updateInterval) : base("ADAM-6015", name, description, updateInterval)
        {
            AdamModel = Adam6000Type.Adam6015;
        }

        #region АНАЛОГОВЫЕ ВХОДЫ

        public List<AnalogInput> AnalogInputs
        {
            get;
            private set;
        }

        public void UpdateAnalogInputs()
        {
            readAnalogInputs();
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
            setDiscreteOutput(wire, state);
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

            AnalogInputs = new List<AnalogInput>();

            /// Число аналоговых входов
            int iAiCount = Advantech.Adam.AnalogInput.GetChannelTotal(AdamModel);
            log.Info("Initialize analog inputs {0} for {1}:", iAiCount, AdamModel);

            ///
            //инициализация аналоговых входов
            for (int w = 0; w < iAiCount; w++)
            {
                string desc;
                string units;
                ushort rgn;                
                float mins;
                float maxs;

                ///для старых версий прошивки
                if (DeviceFirmwareVerion < Adam6000Base.NewerFirmwareVersion)
                {
                    byte rgnB;
                    Socket.AnalogInput().GetInputRange(w, out rgnB);                    
                    units = Advantech.Adam.AnalogInput.GetUnitName(AdamModel, rgnB);
                    Advantech.Adam.AnalogInput.GetRangeHighLow(AdamModel, rgnB, out maxs, out mins);
                    desc = Advantech.Adam.AnalogInput.GetRangeName(AdamModel, rgnB);

                    rgn = rgnB;
                }                    
                else
                {
                    Socket.AnalogInput().GetInputRange(w, out rgn);
                    units = Advantech.Adam.AnalogInput.GetUnitName(AdamModel, rgn);
                    Advantech.Adam.AnalogInput.GetRangeHighLow(AdamModel, rgn, out maxs, out mins);
                    desc = Advantech.Adam.AnalogInput.GetRangeName(AdamModel, rgn);
                }

                log.Info("AI{0} :  {1} {2} ({3}) {4}", mins, maxs, units, desc);

                MeassureRangeInfo mri = new MeassureRangeInfo(rgn, desc, units, mins, maxs);

                AnalogInputs.Add(new AnalogInput(this, w, mri));
            }

            /// Число дискретных выходов        
            int iDoCount = Advantech.Adam.DigitalOutput.GetChannelTotal(AdamModel);
            DiscreteOutputs = new List<DiscreteOutput>();

            log.Info("Initialize discrete outputs ({0}) for {1}:", iDoCount, AdamModel);

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
                UpdateAnalogInputs();

                ///ОБНОВЛЯЕМ СОСТОЯНИЕ ДИСКРЕТНЫХ ВЫХОДОВ
                UpdateDiscreteOutputs();                

            }
            catch(Exception e)
            {
                log.Error(e, "Update device data");
            }
        }
        #endregion
       
    }
}
