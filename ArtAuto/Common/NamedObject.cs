using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ArtAuto.Common
{
    public class NamedObject : INotifyPropertyChanged
    {
        public NamedObject(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        /// <summary>        
        /// Строковый идентификатор объекта
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description
        {
            get
            {
                return desc;
            }
            protected set
            {
                if (value != name)
                {
                    desc = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// Создание объекта из узла XML
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>true - в случае успешного объекта</returns>
        /// <returns>false - в случае неудачной попытки объекта</returns>
        public virtual bool CreateFromXmlNode(XmlNode node)
        {
            if (node.Attributes["NAME"] == null)
                throw new ArtAutoXmlExceptions("Attribute not set", "NAME");

            name = node.Attributes["NAME"].Value;
            if (name.Length == 0)
                throw new ArtAutoXmlExceptions("Empty attribute", "NAME");

            desc = node.InnerText.Trim(' ', '\t', '\n', '\r');
            return true;
        }

        /// <summary>
        /// Сохранение объекта в узел XML
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <returns>
        /// true - если сохранение объекта прошло успешно
        /// false  - если не удалось сохранить объект
        /// </returns>
        public virtual bool SaveToXmlNode(XmlNode node, XmlDocument doc)
        {
            SetAttributeSafe(node, "NAME", Name, doc);
            node.InnerText = Description;            
            return true;
        }

        /// <summary>
        /// Безопасное задание атрибута узла
        /// </summary>
        /// <param name="node">Узел документа XML </param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="value">Значение атрибута</param>
        /// <param name="doc">XML документ</param>
        protected void SetAttributeSafe(XmlNode node, string name, string value, XmlDocument doc)
        {

            if (node.Attributes[name] != null)
                node.Attributes[name].Value = value;
            else
            {
                XmlAttribute a = doc.CreateAttribute(name);
                a.Value = value;
                node.Attributes.Append(a);
            }
            
        }

        protected string name;
        protected string desc;
    }
}
