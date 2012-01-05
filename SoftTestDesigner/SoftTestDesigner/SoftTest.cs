// -----------------------------------------------------------------------
// <copyright file="SoftTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.IO;
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
            id = 77091;
            testTeam = "Lodging Inventory Systems";
            category = "Regression";
            risktier = 2;
            invoke = new Invoke(
                "HotelTest.dll",
                "Expedia.Automation.Test.Hotels.ExpediaServiceFee.Functional.ExpediaServiceFeeBulkUpdate");
            parameters = new List<string>();

            testData = new List<Data>();
            testData.Add(new Data(
                "ContainAgencyHotel",
                "true",
                string.Empty));
            testData.Add(new Data(
                "ContainDualHotel",
                "true",
                string.Empty));
            testData.Add(new Data(
                "ContainMerchantHotel",
                "true",
                string.Empty));
            testData.Add(new Data(
                "ExpediaServivceFeeType",
                "HSSF",
                string.Empty));
            testData.Add(new Data(
                "IsProduction",
                "false",
                string.Empty));
            testData.Add(new Data(
                "OnExtranet",
                "false",
                string.Empty));
            testData.Add(new Data(
                "pageloadtimeout",
                "600",
                string.Empty));
            testData.Add(new Data(
                "ShoppingPathForESF",
                "P",
                string.Empty));
            testData.Add(new Data(
                "tpidForESF",
                "20001",
                string.Empty));


            LOBMasks = new List<lobmask>();
            LOBMasks.Add(new lobmask("Hotel"));

            siteFlags = new List<siteflag>();
            siteFlags.Add(new siteflag("HIMS"));

            environmentTypes = new List<environmenttype>();
            environmentTypes.Add(new environmenttype("Lab"));

            riskitems = new List<riskitem>();
        }

        public SoftTest(
            string filename,
            List<string> configItemsName,
            string configItemsValue)
        {
            id = 77091;
            testTeam = "Lodging Inventory Systems";
            category = "Regression";
            risktier = 2;
            invoke = new Invoke(
                "HotelTest.dll",
                "Expedia.Automation.Test.Hotels.ExpediaServiceFee.Functional.ExpediaServiceFeeBulkUpdate");
            parameters = new List<string>();

            string[] values = configItemsValue.Split(new char[] { ';' });
            testData = new List<Data>();

            for (int i = 0; i < configItemsName.Count; i++)
            {
                string configName = configItemsName.ElementAt(i);
                string configValue = values[i];
                testData.Add(new Data(
                configName,
                configValue,
                string.Empty));
            }

            LOBMasks = new List<lobmask>();
            LOBMasks.Add(new lobmask("Hotel"));

            siteFlags = new List<siteflag>();
            siteFlags.Add(new siteflag("HIMS"));

            environmentTypes = new List<environmenttype>();
            environmentTypes.Add(new environmenttype("Lab"));

            riskitems = new List<riskitem>();
            filename = filename.Replace('.', '\\') + ".soft.xml";

            string path = Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
            { Directory.CreateDirectory(path); }

            Serializer.CreateInstance().SerializeToXML(
                this,
                this.GetType(),
                filename);
        }

        public SoftTest(
            int id,
            string testTeam,
            string category,
            string risktier,
            string method,
            string lobmask,
            string environmentType,
            string filename,
            List<string> nameList,
            string[] valueArray)
        {
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
                testData.Add(new Data(
                configName,
                configValue,
                string.Empty));
            }

            LOBMasks = new List<lobmask>();
            LOBMasks.Add(new lobmask(lobmask));

            string site = "HIMS";
            for (int i = 0; i < nameList.Count; i++)
            {
                if (nameList.ElementAt(i).ToLower().Equals("OnExtranet") &&
                    valueArray[i].ToLower().Equals("true"))
                { site = "XNet"; }
            }

            siteFlags = new List<siteflag>();
            siteFlags.Add(new siteflag(site));

            environmentTypes = new List<environmenttype>();
            environmentTypes.Add(new environmenttype(environmentType));

            riskitems = new List<riskitem>();
            filename = filename.Replace('.', '\\') + ".soft.xml";

            string path = Path.GetDirectoryName(filename);

            if (!Directory.Exists(path))
            { Directory.CreateDirectory(path); }

            Serializer.CreateInstance().SerializeToXML(
                this,
                this.GetType(),
                filename);
        }

    }

    public struct Invoke
    {
        public string module;
        public string method;

        public Invoke(string module, string method)
        {
            this.module = module;
            this.method = method;
        }
    }

    public struct Data
    {
        public string dataName;
        public string defaultValue;
        public string stripingValues;

        public Data(string dataName, string defaultValue, string stripingValues)
        {
            this.dataName = dataName;
            this.defaultValue = defaultValue;
            this.stripingValues = stripingValues;
        }
    }

    public struct lobmask
    {
        [XmlAttribute]
        public string name;

        public lobmask(string name)
        {
            this.name = name;
        }
    }

    public struct siteflag
    {
        [XmlAttribute]
        public string name;

        public siteflag(string name)
        {
            this.name = name;
        }
    }

    public struct environmenttype
    {
        [XmlAttribute]
        public string name;

        public environmenttype(string name)
        {
            this.name = name;
        }
    }

    public struct riskitem
    {
        [XmlAttribute]
        public string name;

        public riskitem(string name)
        {
            this.name = name;
        }
    }

}
