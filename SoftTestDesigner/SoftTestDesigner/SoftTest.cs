// -----------------------------------------------------------------------
// <copyright file="SoftTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestPKG
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using SoftTestDesigner;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoftTest
    {
        public int? id;
        public string testTeam;
        public string category;
        public int risktier;
        public Invoke invoke;
        public List<string> parameters;
        public List<Data> testData;
        public List<lobmask> LOBMasks;
        public List<siteflag> siteFlags;
        public List<environmenttype> environmentTypes;
        public List<riskitem> riskitems;

        public SoftTest()
        {
        }

        public SoftTest(
            int id,
            string testTeam,
            string category,
            string risktier,
            bool overrideMethod,
            string method,
            string lobmask,
            string environmentType,
            string filename,
            List<string> nameList,
            string[] valueArray)
        {
            if (string.IsNullOrEmpty(filename)) return;

            this.id = id;
            this.testTeam = testTeam;
            this.category = category;
            this.risktier = Convert.ToInt32(risktier);
            invoke = new Invoke("HotelTest.dll", method);
            parameters = new List<string>();

            testData = new List<Data>();

            for (int i = 0; i < nameList.Count; i++)
            {
                string configName = nameList.ElementAt(i);
                string configValue = valueArray[i];

                if (string.IsNullOrEmpty(configName) ||
                    string.IsNullOrEmpty(configValue) ||
                    configValue.ToLower().Equals("null"))
                { continue; }

                testData.Add(new Data(
                configName,
                configValue,
                string.Empty));
            }

            testData.Sort();

            LOBMasks = new List<lobmask>();
            LOBMasks.Add(new lobmask(lobmask));

            string site = "HIMS";
            for (int i = 0; i < nameList.Count; i++)
            {
                if (nameList.ElementAt(i).ToLower().Equals("onextranet") &&
                    valueArray[i].ToLower().Equals("true"))
                {
                    site = "XNet";
                    break;
                }
            }

            siteFlags = new List<siteflag>();
            siteFlags.Add(new siteflag(site));

            environmentTypes = new List<environmenttype>();
            environmentTypes.Add(new environmenttype(environmentType));

            riskitems = new List<riskitem>();
            filename = filename.Replace('.', '\\') + ".test.xml";

            string path = Path.GetDirectoryName(filename);

            if (File.Exists(filename))
            {
                SoftTest existingSoftTest = Serializer.CreateInstance().DeserializeFromXML(this.GetType(), filename) as SoftTest;
                this.id = existingSoftTest.id;

                if (!overrideMethod)
                {
                    this.invoke.method = existingSoftTest.invoke.method;
                }
            }
            else
            {
                if (!Directory.Exists(path) && !string.IsNullOrEmpty(path))
                { Directory.CreateDirectory(path); }
            }

            Serializer.CreateInstance().SerializeToXML(
                this,
                this.GetType(),
                filename);
        }

    }

    public class Invoke
    {
        public string module;
        public string method;

        public Invoke()
        {
        }

        public Invoke(string module, string method)
        {
            this.module = module;
            this.method = method;
        }
    }

    public class Data : IComparable
    {
        public string dataName;
        public string defaultValue;
        public string stripingValues;

        public Data()
        {
        }

        public Data(string dataName, string defaultValue, string stripingValues)
        {
            this.dataName = dataName;
            this.defaultValue = defaultValue;
            this.stripingValues = stripingValues;
        }

        public int CompareTo(object obj)
        {
            return this.dataName.CompareTo((obj as Data).dataName);
        }
    }

    public class lobmask
    {
        [XmlAttribute]
        public string name;

        public lobmask()
        {
        }

        public lobmask(string name)
        {
            this.name = name;
        }
    }

    public class siteflag
    {
        [XmlAttribute]
        public string name;

        public siteflag()
        {
        }

        public siteflag(string name)
        {
            this.name = name;
        }
    }

    public class environmenttype
    {
        [XmlAttribute]
        public string name;

        public environmenttype()
        {
        }

        public environmenttype(string name)
        {
            this.name = name;
        }
    }

    public class riskitem
    {
        [XmlAttribute]
        public string name;

        public riskitem()
        {
        }

        public riskitem(string name)
        {
            this.name = name;
        }
    }

}
