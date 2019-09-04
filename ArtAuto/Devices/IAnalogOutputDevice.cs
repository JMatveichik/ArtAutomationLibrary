using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public interface IAnalogOutputDevice
    {
        /// <summary>
        ///Получение списка аналоговых выходов
        /// </summary>        
        List<AnalogOutput> AnalogOutputs
        {
            get;
        }

        /// <summary>
        /// Получение данных со всех аналоговых выходов (выполняется запрос на чтение данных)
        /// </summary>        
        void UpdateAnalogOutputs();

        /// <summary>
        /// Установить аналоговый выход в заданное состояние 
        /// </summary>
        /// <param name="wire">Номер аналогового выхода</param>
        /// <param name="value">Значение записываемое в выход</param>
        void SetAnalogOutput(int wire, double value);

    }
}
