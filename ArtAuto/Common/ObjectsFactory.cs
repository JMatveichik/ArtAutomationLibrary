using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

using ArtAuto.Data;
using ArtAuto.Converters;
using ArtAuto.Devices;

namespace ArtAuto.Common
{
    
    internal class XmlOjectTypeFactory<T>
    {

        delegate T CreateObjectFromXml(XmlNode node);

        static XmlOjectTypeFactory()
        {
            Type dps = typeof(T);
            Assembly asm = Assembly.GetAssembly(dps);

            RegisterCreators(asm);
        }

        public static T CreateFromXml(XmlNode node)
        {
            if (node.Attributes["TYPE"] == null)
                throw new ArtAutoXmlExceptions("Attribute not find", "TYPE");

            string type = node.Attributes["TYPE"].Value;

            if (type.Length == 0)
                throw new ArtAutoXmlExceptions("Empty attribute", "TYPE");

            if (!creators.Keys.Contains(type))
                throw new ArtAutoXmlExceptions("Not register creator", "TYPE", type);

            CreateObjectFromXml creator = creators[type];
            if (creator == null)
                throw new ArtAutoXmlExceptions("Invalid creator function", "TYPE", type);

            try
            {
                T obj = creator(node);
                if (obj != null)
                    return obj;
            }
            catch (ArtAutoXmlExceptions xe)
            {
                throw xe;
            }

            return default(T);
        }

        public static void RegisterCreators(Assembly asm)
        {

            Type ot = typeof(T);

            //для каждого базового класса
            IEnumerable<Type> list = asm.GetTypes().Where(t => t.IsSubclassOf(ot));

            foreach (Type dpt in list)
            {
                if (!dpt.IsAbstract)
                {
                    MethodInfo method = dpt.GetMethod("FromXmlNode");

                    if (method == null)
                        continue;

                    CreateObjectFromXml cdp = (CreateObjectFromXml)Delegate.CreateDelegate(typeof(CreateObjectFromXml), method);
                    creators.Add(dpt.Name, cdp);
                }
            }
        }

        private static Dictionary<string, CreateObjectFromXml> creators = new Dictionary<string, CreateObjectFromXml>();

    }

    /*
    public static class XmlObjectsGlobalFactory
    {
        static XmlObjectsFactory()
        {
            Type dps = typeof(DataPoint);
            Assembly asm = Assembly.GetAssembly(dps);

            RegisterCreators(asm);
        }

        /// <summary>
        /// Абстрактный метод создает объект точки данных
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>Экземпляр точки данных в случае успешного создания</returns>
        /// <returns>null  - в случае неудачной попытки создания точки данных</returns>
        public static DataPoint CreateDataPointObject(XmlNode node)
        {

            if (node.Attributes["TYPE"] == null)
                throw new ArtAutoXmlExceptions("Attribute not find", "TYPE");

            string type = node.Attributes["TYPE"].Value;

            if (type.Length == 0)
                throw new ArtAutoXmlExceptions("Empty attribute", "TYPE");

            if (!dataPointCreators.Keys.Contains(type))
                throw new ArtAutoXmlExceptions("Not register creator", "TYPE", type);

            CreateDataPoint cdp = dataPointCreators[type];
            if (cdp == null)
                throw new ArtAutoXmlExceptions("Invalid creator function", "TYPE", type);

            try
            {
                DataPoint dp = cdp(node);
                if (dp != null)
                    return dp;
            }
            catch (ArtAutoXmlExceptions xe)
            {
                throw xe;
            }
            return null;
        }


        public static BaseConverter CreateConverterObject(XmlNode node)
        {
            
        }

        
        }

        //private static Dictionary<string, CreateDataPoint> dataPointCreators = new Dictionary<string, CreateDataPoint>();
        //private static Dictionary<string, CreateDataPoint> dataPointCreators = new Dictionary<string, CreateDataPoint>();
        //private static List<Type> creators = new List<Type>();
      
    }
  */


}
