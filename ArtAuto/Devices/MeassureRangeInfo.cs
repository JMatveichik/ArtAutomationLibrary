using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Devices
{
    /// <summary>
    /// Информация об измеряемом диапазоне канала
    /// </summary>
    public class MeassureRangeInfo
    {
        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="id">Идентификатор измеряемого диапазона</param>
        /// <param name="desc">Описание</param>
        /// <param name="units">Единицы измерения</param>
        /// <param name="mins">Минимальное значение измеряемой величины</param>
        /// <param name="maxs">Максимальное значение измеряемой величины</param>
        public MeassureRangeInfo(ushort id, string desc, string units, double mins, double maxs)
        {
            //идентификатор измеряемого диапазона
            ID = id;

            //измеряемая величина
            Description = desc;

            //единицы	 измерения
            Units = units;

            //минимальное значение измеряемой величины
            SignalMinimum = mins;

            //максимальное значение измеряемой величины
            SignalMaximum = maxs;   
        }
        

        /// <summary>
        /// Идентификатор измеряемого диапазона
        /// </summary>
        public ushort ID
        {
            get;
            set;
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public string Units
        {
            get;
            set;
        }

        /// <summary>
        /// Минимальное значение измеряемой величины
        /// </summary>
        public double SignalMinimum
        {
            get;
            set;
        }

        /// <summary>
        /// Максимальное значение измеряемой величины
        /// </summary>
        public double SignalMaximum
        {
            get;
            set;
        }

    }
}
