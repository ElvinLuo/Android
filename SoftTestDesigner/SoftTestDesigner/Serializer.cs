// -----------------------------------------------------------------------
// <copyright file="Serializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Serializer
    {
        private static Serializer instance;

        public static Serializer CreateInstance()
        {
            if (instance == null)
            {
                instance = new Serializer();
            }
            return instance;
        }

        public void SerializeToXML(object instance, Type type, string file)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(type);
                TextWriter text = new StreamWriter(file);
                x.Serialize(text, instance);
                text.Flush();
                text.Close();
            }
            catch (Exception exception)
            { Console.WriteLine(exception.Message); }
        }

        public object DeserializeFromXML(Type type, string file)
        {
            XmlSerializer x = new XmlSerializer(type);
            TextReader text = new StreamReader(file);
            object resut = x.Deserialize(text);
            text.Close();
            return resut;
        }

    }
}
