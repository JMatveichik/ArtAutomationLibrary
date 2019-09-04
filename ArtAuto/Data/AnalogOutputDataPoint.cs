using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Data
{
    public class AnalogOutputDataPoint : AnalogDataPoint
    {

        public AnalogOutputDataPoint() : base("UnSet", "Empty")
        {

        }

        public AnalogOutputDataPoint(string name, string description) : base(name, description)
        {

        }
        /// <summary>
        /// Получить значение аналоговой величины после использования преобразований и фильтрации
        /// </summary>
        public override double Value
        {
            get
            {
                return lastSignal;
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

        public static AnalogOutputDataPoint FromXmlNode(XmlNode node)
        {
            AnalogOutputDataPoint dp = new AnalogOutputDataPoint();
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
