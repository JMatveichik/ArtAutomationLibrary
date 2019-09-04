using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Common;

namespace ArtAuto.Data
{
    public class DiscreteOutputDataPoint : DiscreteDataPoint
    {
        public DiscreteOutputDataPoint() :  base("UnSet", "Empty")
        {

        }

        public DiscreteOutputDataPoint(string name, string description) : base(name, description)
        {

        }

        public static DiscreteOutputDataPoint FromXmlNode(XmlNode node)
        {
            DiscreteOutputDataPoint dp = new DiscreteOutputDataPoint();
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
