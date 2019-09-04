using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Converters
{
    /// <summary>
    /// Класс вторичного преобразователя по линейному закону. 
    /// </summary>
    public class LinearConverter : BaseConverter
    {

        public LinearConverter(string name, string description) : base(name, description)
        {
            K = 1.0;
            B = 0.0;
            LiteralX = 'x';
            LiteralY = 'y';
        }

        public LinearConverter(double x1, double y1, double x2, double y2, string name, string description) : base(name, description)
        {
            K = (y2 - y1) / (x2 - x1);
            B = -K * x1 + y1;
            LiteralX = 'x';
            LiteralY = 'y';
        }

        public LinearConverter(double k, double b, string name, string description) : base(name, description)
        {
            K = k;
            B = b;
            LiteralX = 'x';
            LiteralY = 'y';
        }

        /// <summary>
        /// Создание линейного вторичного преобразователя данных из узла XML
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>true - в случае успешного создания вторичного преобразователя</returns>
        /// <returns>false - в случае неудачной попытки создания вторичного преобразователя</returns>
        public override bool CreateFromXmlNode(XmlNode node)
        {
            if (!base.CreateFromXmlNode(node))
                return false;

            double x1 = 0.0;
            double x2 = 0.0;
            double y1 = 0.0;
            double y2 = 0.0;

            XmlAttribute attr = node.Attributes["MINS"];
            if (attr == null)
                throw new ArtAutoXmlExceptions("Node attribute not set", "MINS");
            else
                x1 = System.Convert.ToDouble(attr.Value);

            attr = node.Attributes["MAXS"];
            if (attr == null)
                throw new ArtAutoXmlExceptions("Node attribute not set", "MAXS");
            else
                x2 = System.Convert.ToDouble(attr.Value);

            attr = node.Attributes["MINV"];
            if (attr == null)
                throw new ArtAutoXmlExceptions("Node attribute not set", "MINV");
            else
                y1 = System.Convert.ToDouble(attr.Value);

            attr = node.Attributes["MAXV"];
            if (attr == null)
                throw new ArtAutoXmlExceptions("Node attribute not set", "MAXV");
            else
                y2 = System.Convert.ToDouble(attr.Value);


            try
            {
                K = (y2 - y1) / (x2 - x1);
                B = -K * x1 + y1;
            }
            catch (DivideByZeroException)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Прямое преобразование x -> y
        /// </summary>
        /// <param name="x">Входная величина для преобразования</param>
        /// <returns>Сконвертированная величина</returns>
        public override double Convert(double x)
        {
            return K * x + B;
        }

        /// <summary>
        /// Обратное преобразование y -> x
        /// </summary>
        /// <param name="y">Входная величина для обратного преобразования</param>
        /// <returns>Сконвертированная в обратном направлении величина</returns>
        public override double ConvertBack(double y)
        {
            return (y - B) / K;
        }


        /// <summary>
        /// Cтроковое представление формуулы прямой y = k*x + b
        /// </summary>
        /// <returns>Строку формулы прямой</returns>

        public override string ToString()
        {
            return string.Format("{0} = {1}{2} + {3}", LiteralY, K, LiteralX, B);
        }


        /// <summary>
        /// Коэффициент наклона прямой y = k*x + b
        /// </summary>
        public double K
        {
            get
            {
                return k;
            }
            set
            {
                if (value == k)
                    return;

                k = value;
                OnPropertyChanged("K");
            }
        }
        private double k = 1.0;


        /// <summary>
        /// Смещение прямой относительно нулевой точки y = k*x + b
        /// </summary>
        public double B
        {
            get
            {
                return b;
            }
            set
            {
                if (value == b)
                    return;

                b = value;
                OnPropertyChanged("B");
            }
        }
        private double b = 0.0; 
    }
}
