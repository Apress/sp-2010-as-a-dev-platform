using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;

namespace ConsoleApplicationTest
{

    public abstract class PropertyReader    
    {

        public static XDocument Serialize<T>(T source) where T : PropertyReader
        {
            XDocument target = new XDocument();
            XmlSerializer s = new XmlSerializer(typeof(T));
            XmlWriter writer = target.CreateWriter();
            s.Serialize(writer, source);
            writer.Close();
            return target;
        }

        public static T Deserialize<T>(XDocument doc) where T : PropertyReader
        {
            XmlSerializer s = new XmlSerializer(typeof(T));
            XmlReader r = doc.CreateReader();
            T props = s.Deserialize(r) as T;
            return props;
        }

        public static T ReadArguments<T>(string[] args) where T : PropertyReader, new()
        {
            T props = new T();
            foreach (string arg in args)
            {
                // assume the parameter looks like param:value
                string[] param = arg.Split(new char[] {':'}, 2);
                if (param.Length != 2) throw new ArgumentOutOfRangeException("args");
                PropertyInfo pi = props.GetType().GetProperty(param[0]);
                if (pi == null) throw new ArgumentOutOfRangeException("args");
                object anyTypeValue = Convert.ChangeType(param[1], pi.PropertyType);
                pi.SetValue(props, anyTypeValue, null);
            }
            return props;
        }

    }


    [XmlRoot(ElementName="Properties")]
    public class Properties : PropertyReader
    {

        [XmlElement(ElementName = "ShowAll")]
        public bool ShowAll { get; set; }

        [XmlElement(ElementName = "Filter")]
        public string Filter { get; set; }

        [XmlElement(ElementName = "WebUrl")]
        public string WebUrl { get; set; }

    }
}
