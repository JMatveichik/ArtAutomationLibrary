﻿using Advantech.Adam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices.ADAM6000
{
    public class Adam6024 : Adam6000Base, IAnalogInputDevice, IDiscreteOutputDevice, IDiscreteInputDevice, IAnalogOutputDevice
    {
        public Adam6024()
        {
            AdamModel = Adam6000Type.Adam6024;

            AnalogInputs = new List<AnalogInput>();

            DiscreteOutputs = new List<DiscreteOutput>();

            DiscreteInputs = new List<DiscreteInput>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя устройства</param>
        /// <param name="description">Описание </param>
        /// <param name="updateInterval">Интервал обновления данных</param>
        public Adam6024(string name, string description, int updateInterval) : base("ADAM-6024", name, description, updateInterval)
        {
            AdamModel = Adam6000Type.Adam6024;

            AnalogInputs = new List<AnalogInput>();

            DiscreteOutputs = new List<DiscreteOutput> () ;

            DiscreteInputs = new List<DiscreteInput>();
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

        #region АНАЛОГОВЫЕ ВЫХОДЫ
       
        /// <summary>
        ///Получение списка аналоговых выходов
        /// </summary>        
        public List<AnalogOutput> AnalogOutputs
        {
            get;
            private set;

        }

        /// <summary>
        /// Получение данных со всех аналоговых выходов (выполняется запрос на чтение данных)
        /// </summary>        
        public void UpdateAnalogOutputs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Установить аналоговый выход в заданное состояние 
        /// </summary>
        /// <param name="wire">Номер аналогового выхода</param>
        /// <param name="value">Значение записываемое в выход</param>
        public void SetAnalogOutput(int wire, double value)
        {
            throw new NotImplementedException();
        }

        
        #endregion

        #region ДИСКРЕТНЫЕ ВХОДЫ
        public List<DiscreteInput> DiscreteInputs
        {
            get;
            private set;
        }

        public void UpdateDiscreteInputs()
        {
            throw new NotImplementedException();
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
            
            /// Число аналоговых входов
            int AnalogInputsCount = Advantech.Adam.AnalogInput.GetChannelTotal(AdamModel);         
           

            ///
            //инициализация аналоговых входов
            for (int w = 0; w < AnalogInputsCount; w++)
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

                MeassureRangeInfo mri = new MeassureRangeInfo(rgn, desc, units, mins, maxs);

                AnalogInputs.Add(new AnalogInput(this, w, mri));
            }

            /// Число дискретных выходов        
            int DiscreteOutputsCount = Advantech.Adam.DigitalOutput.GetChannelTotal(AdamModel);

            for (int i = 0; i < DiscreteOutputsCount; i++)
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

            }
        }
        
        #endregion

    }
}