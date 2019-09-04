using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public interface IAnalogInputDevice 
    {

        /// <summary>
        ///Получение списка аналоговых входов
        /// </summary>        
        List<AnalogInput> AnalogInputs
        {
            get;
        }

        /// <summary>
        /// Получение данных со всех аналоговых входов (выполняется запрос на чтение данных)
        /// </summary>        
        void UpdateAnalogInputs();
    }
}
