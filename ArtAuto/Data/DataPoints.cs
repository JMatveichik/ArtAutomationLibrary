using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;
using System.Diagnostics;

namespace ArtAuto.Data
{

    public class DataPointsDictionary : Dictionary<string, DataPoint>
    {
        public bool Load(XmlDocument doc)
        {
            XmlNodeList dslist = doc.SelectNodes("//DS");

            foreach (XmlNode node in dslist)
            {
                DataPoint dp = null;

                try
                {
                    dp = XmlOjectTypeFactory<DataPoint>.CreateFromXml(node);
                }
                catch (ArtAutoXmlExceptions e)
                {
                    Debug.WriteLine("{0} TAG:{1} VALUE:{2}", e.Message, e.Attribute, e.AtributeValue);
                    continue;
                }

                Add(dp.Name, dp);
            }

            return true;
        }

    }

}
