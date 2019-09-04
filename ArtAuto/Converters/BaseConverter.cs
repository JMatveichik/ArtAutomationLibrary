using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Converters
{
    public abstract class BaseConverter : NamedObject
    {

        public BaseConverter(string name, string description) : base (name, description)
        {

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
         
            LiteralX = 'x';
            LiteralY = 'y';

            XmlAttribute attr = node.Attributes["LETTERX"];            
            if (attr != null)
                LiteralX = attr.Value[0];

            attr = node.Attributes["LETTERY"];            
            if (attr != null)
                LiteralY = attr.Value[0];

            return true;
        }

        public char LiteralX {get; set;}
        public char LiteralY {get; set;}
         
        /// <summary>
        /// Прямое преобразование x -> y
        /// </summary>
        /// <param name="x">Входная величина для преобразования</param>
        /// <returns>Сконвертированная величина</returns>
        public abstract double Convert(double x);

        /// <summary>
        /// Обратное преобразование y -> x
        /// </summary>
        /// <param name="y">Входная величина для обратного преобразования</param>
        /// <returns>Сконвертированная в обратном направлении величина</returns>
        public abstract double ConvertBack(double y);


    }
      

    

}
