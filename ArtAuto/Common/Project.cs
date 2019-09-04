using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ArtAuto.Data;

namespace ArtAuto.Common
{
    public class Project : NamedObject
    {
        public Project(string name, string description) : base(name, description)
        {
            DataPoints = new DataPointsDictionary();
        }

        public bool Load(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            DataPoints.Load(doc);

            return true;
        }

        public DataPointsDictionary DataPoints
        {
            get;
            private set;
        }

        
    }
}
