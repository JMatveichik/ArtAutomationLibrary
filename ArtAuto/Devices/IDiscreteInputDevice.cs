using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public interface IDiscreteInputDevice 
    {
        /// <summary>
        ///Получение списка аналоговых входов
        /// </summary>        
        List<DiscreteInput> DiscreteInputs
        {
            get;
        }

        /// <summary>
        /// Получение данных со всех дискретных входов (выполняется запрос на чтение данных)
        /// </summary>        
        void UpdateDiscreteInputs();
    }
}
