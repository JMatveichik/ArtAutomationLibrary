using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;
using ArtAuto.Converters;

namespace ArtAuto.Data
{
    public class AnalogInputDataPoint : AnalogDataPoint
    {

        public AnalogInputDataPoint() : base("UnSet", "Empty")
        {

        }

        public AnalogInputDataPoint(string name, string description) : base(name, description)
        {

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

        public static AnalogInputDataPoint FromXmlNode(XmlNode node)
        {
            AnalogInputDataPoint dp = new AnalogInputDataPoint();
            try
            {
                if (dp.CreateFromXmlNode(node))
                    return dp;
            }
            catch (ArtAutoXmlExceptions xe)
            {
                throw xe;
            }

            return null;
        }
    }
}
