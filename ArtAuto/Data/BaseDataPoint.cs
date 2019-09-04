using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Data
{
    /// <summary>
    /// Класс точка данных
    /// </summary>
    public class DataPoint : NamedObject
    {
        /// <summary>
        /// 
        /// </summary>
        public DataPoint(string name, string description) : base(name, description)
        {

        }

        /// <summary>
        /// Создание объекта из узла XML
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>true - в случае успешного создания объекта</returns>
        /// <returns>false - в случае неудачной попытки создания объекта</returns>
        public override bool CreateFromXmlNode(XmlNode node)
        {
            try
            {
                base.CreateFromXmlNode(node);
            }
            catch(ArtAutoXmlExceptions axe)
            {
                return false;
            }   

            prior = 100;
            if (node.Attributes["PRIORITY"] != null)
                prior = Convert.ToInt32(node.Attributes["PRIORITY"].Value);

            return true;
        }
        
        
        /// <summary>
        /// Приориитет доступа
        /// </summary>
        public int Priority
        {
            get { return prior; }           
        }

        protected int prior;       
    }
    
   

    

}
