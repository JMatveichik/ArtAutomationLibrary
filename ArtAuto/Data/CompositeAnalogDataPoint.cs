using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtAuto.Converters;

namespace ArtAuto.Data
{
    public class CompositeAnalogDataPoint : AnalogDataPoint, ICompositeDataPoint
    {

        public CompositeAnalogDataPoint() :  base("UnSet", "Empty")
        {

        }

        public CompositeAnalogDataPoint(string name, string description) : base(name, description)
        {

        }

        public int Count
        {
            get
            {
                return dataPoints.Count;
            }
        }

        private List<DataPoint> dataPoints = new List<DataPoint>();

        public bool Add(string name)
        {
            return false;
        }

        public bool Remove(string name)
        {
            return false;
        }

        /// <summary>
        /// Получить значение аналоговой величины после использования преобразований и фильтрации
        /// </summary>
        public override double Value
        {
            get
            {
                double v = lastSignal;
                foreach (BaseConverter sc in converters)
                    v = sc.Convert(v);

                return v;
            }
        }

        /// <summary>
        /// Получить значение исходного сигнала без фильтации и преобразований 
        /// </summary>
        public override double Signal
        {
            get
            {
                return lastSignal;
            }
        }

    }
}
