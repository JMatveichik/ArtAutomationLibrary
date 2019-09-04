using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Common
{
    class ArtAutoXmlExceptions : Exception
    {
        public ArtAutoXmlExceptions() { }

        public ArtAutoXmlExceptions(string message, string atribute, string value = "") : base(message)
        {
            Attribute = atribute;
            AtributeValue = value;
        }
    
        public ArtAutoXmlExceptions(string message, Exception inner) : base(message, inner) { }

        public string Attribute
        {
            get;
            private set;
        }

        public string AtributeValue
        {
            get;
            private set;
        }
    }

    

}
