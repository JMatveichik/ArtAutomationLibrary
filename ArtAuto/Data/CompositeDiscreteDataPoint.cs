using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Data
{
    public class CompositeDiscreteDataPoint : DiscreteDataPoint, ICompositeDataPoint
    {
        public CompositeDiscreteDataPoint() :  base("UnSet", "Empty")
        {

        }

        public CompositeDiscreteDataPoint(string name, string description) : base(name, description)
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

    }
}
