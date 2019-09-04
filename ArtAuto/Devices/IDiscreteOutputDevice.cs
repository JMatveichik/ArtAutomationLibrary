using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    public interface IDiscreteOutputDevice
    {
        /// <summary>
        ///Получение списка дискретных выходов
        /// </summary>        
        List<DiscreteOutput> DiscreteOutputs
        {
            get;
        }

        /// <summary>
        /// Получение данных со всех дискретных выходов (выполняется запрос на чтение данных)
        /// </summary>        
        void UpdateDiscreteOutputs();

        /// <summary>
        /// Установить дискретный выход в заданное состояние 
        /// </summary>
        /// <param name="wire">Номер дискретного выхода</param>
        /// <param name="state">Состояние в которое переключается выход</param>
        void SetDiscreteOutput(int wire, bool state);
    }
}
