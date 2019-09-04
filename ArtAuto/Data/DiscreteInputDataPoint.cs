using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Data
{
    public class DiscreteInputDataPoint : DiscreteDataPoint
    {
        public DiscreteInputDataPoint() :  base("UnSet", "Empty")
        {

        }

        public DiscreteInputDataPoint(string name, string description) : base(name, description)
        {

        }


        public static DiscreteInputDataPoint FromXmlNode(XmlNode node)
        {
            DiscreteInputDataPoint dp = new DiscreteInputDataPoint();
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
