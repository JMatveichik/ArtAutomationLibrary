using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Converters;

namespace ArtAuto.Data
{
    /// <summary>
    /// Абстрактный базовый класс для данных представляющих собой аналоговую величину
    /// </summary>
    public abstract class AnalogDataPoint : DataPoint
    {
        public AnalogDataPoint() : base("UnSet", "Empty")
        {

        }

        public AnalogDataPoint(string name, string description) : base(name, description)
        {

        }

        /// <summary>
        /// Получить значение аналоговой величины после использования преобразований и фильтрации
        /// </summary>
        public abstract double Value
        {
            get;
        }

        /// <summary>
        /// Получить значение исходного сигнала без фильтации и преобразований 
        /// </summary>
        public abstract double Signal
        {
            get;
        }

        /// <summary>
        /// Цепочка вторичных преобразователей использующаяся для конвертирования исходного сигнала в
        /// конкретную физическую величину 
        /// </summary>
        protected List<BaseConverter> converters;

        /// <summary>
        /// Еденицы измерения аналоговой величины
        /// </summary>
        public string Units
        {
            get
            {
                return units;
            }
            protected set
            {
                units = value;
            }
        }

        /// <summary>
        /// Создание объекта из узла XML
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>true - в случае успешного объекта</returns>
        /// <returns>false - в случае неудачной попытки объекта</returns>
        public override bool CreateFromXmlNode(XmlNode node)
        {
            if (!base.CreateFromXmlNode(node))
                return false;

            if (node.Attributes["UNITS"] != null)
                Units = node.Attributes["UNITS"].Value;

            return true;
        }

        protected string units;
        protected double lastSignal = 0.0;
    }
}
