// -----------------------------------------------------------------------
// <copyright file="TestRun.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TestRunPKG
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using SoftTestDesigner;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestRun
    {
        [XmlAttribute]
        public string testrunname;

        [XmlAttribute]
        public string managername;

        [XmlAttribute]
        public string branchName;

        [XmlAttribute]
        public string version;

        public List<Assignment> Assignments;

        public TestRun()
        {
        }

        public TestRun(
            string testrunname,
            string managername,
            string branchName,
            string version,
            List<string> softTestNameList,
            List<string> testConfigNameList,
            List<string[]> testConfigValueList,
            string methodModule,
            List<string[]> varNameArrayList,
            List<string[]> varValueArrayList)
        {
            this.testrunname = testrunname ?? "ULP_CoreAdminPhase2_BulkEdit_LISQA8_Oct  7 2011 11:04PM";
            this.managername = managername ?? "TFxIDXManager";
            this.branchName = branchName ?? "LFS00006";
            this.version = version ?? "2.0";

            Assignments = new List<Assignment>();
            for (int i = 0; i < softTestNameList.Count; i++)
            {
                if (string.IsNullOrEmpty(softTestNameList[i]) ||
                    softTestNameList[i].Equals(""))
                { continue; }

                Assignment assignment = new Assignment(
                    i + 1,
                    softTestNameList,
                    testConfigNameList,
                    testConfigValueList.ElementAt(i),
                    methodModule,
                    varNameArrayList,
                    varValueArrayList);
                Assignments.Add(assignment);
            }

            Serializer.CreateInstance().SerializeToXML(
               this,
               this.GetType(),
               "RunInTestStudio.labrun.xml");
        }
    }

    public class Assignment
    {
        [XmlAttribute]
        public string id;

        [XmlAttribute]
        public string Disabled;

        public SoftTest SoftTest;
        public List<Var> Configs;

        public Assignment()
        {
            id = "10";
            Disabled = "False";
            SoftTest = new SoftTest();
            Configs = new List<Var>();
            Configs.Add(new Var());
            Configs.Add(new Var("langid", "1033"));
        }

        public Assignment(
            int i,
            List<string> softTestNameArray,
            List<string> testConfigNameList,
            string[] testConfigValueArray,
            string methodModule,
            List<string[]> varNameArrayList,
            List<string[]> varValueArrayList)
        {
            this.id = i.ToString();
            this.Disabled = false.ToString();
            SoftTest = new SoftTest(softTestNameArray[i - 1], i, testConfigNameList, testConfigValueArray, methodModule);

            Configs = new List<Var>();

            for (int idx = 0; idx < testConfigNameList.Count; idx++)
            {
                if (string.IsNullOrEmpty(testConfigNameList[idx]) ||
                    string.IsNullOrEmpty(testConfigValueArray[idx]))
                { continue; }

                Configs.Add(new Var(testConfigNameList[idx], testConfigValueArray[idx]));
            }

            if (!softTestNameArray.Contains("eap")) Configs.Add(new Var("eap", "0"));
            if (!softTestNameArray.Contains("flags")) Configs.Add(new Var("flags", "4"));
            if (!softTestNameArray.Contains("langid")) Configs.Add(new Var("langid", "1033"));
            if (!softTestNameArray.Contains("servername")) Configs.Add(new Var("servername", "CHELLIWEBQA301"));
            if (!softTestNameArray.Contains("tpid")) Configs.Add(new Var("tpid", "20001"));
            if (!softTestNameArray.Contains("utilsite")) Configs.Add(new Var("utilsite", "10.184.16.200"));
            if (!softTestNameArray.Contains("himssite")) Configs.Add(new Var("himssite", "10.184.17.234"));
            if (!softTestNameArray.Contains("site")) Configs.Add(new Var("site", "10.184.17.234"));
        }
    }

    public class SoftTest
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string id;

        public List<TestConfig> TestConfigs;
        public List<Method> Methods;

        public SoftTest()
        {
            name = "Hotels.ULP.Phase2.BulkEdit.ARI.HIMS.ChangeCloseOut";
            id = "85726";
            TestConfigs = new List<TestConfig>();
            TestConfigs.Add(new TestConfig());
            TestConfigs.Add(new TestConfig("ARIEnabled", "true"));

            Methods = new List<Method>();
            Methods.Add(new Method());
        }

        public SoftTest(
            string softTestName,
            int id,
            List<string> testConfigNameList,
            string[] testConfigValueArray,
            string methodModule)
        {
            this.name = softTestName;
            this.id = id.ToString();
            //TestConfig testConfig;
            TestConfigs = new List<TestConfig>();

            //for (int i = 0; i < testConfigNameList.Count; i++)
            //{
            //    if (string.IsNullOrEmpty(testConfigNameList[i]) ||
            //        string.IsNullOrEmpty(testConfigValueArray[i]))
            //    { continue; }

            //    testConfig = new TestConfig(testConfigNameList[i], testConfigValueArray[i]);
            //    TestConfigs.Add(testConfig);
            //}

            Methods = new List<Method>();
            Methods.Add(new Method("HotelTest.dll", methodModule));
        }
    }

    public class TestConfig
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string value;

        public TestConfig()
        {
            name = "HotelContractType";
            value = "3";
        }

        public TestConfig(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public class Method
    {
        [XmlAttribute]
        public string module;

        [XmlAttribute]
        public string name;

        public List<Parameter> Parameters;

        public Method()
        {
            module = "HotelTest.dll";
            name = "Expedia.Automation.Test.Hotels.ICPRUpdateUI.Functional.ChangeCloseOut";
            Parameters = new List<Parameter>();
        }

        public Method(string module, string name)
        {
            this.module = module;
            this.name = name;
            Parameters = new List<Parameter>();
        }
    }

    public class Parameter
    {
        public Parameter()
        {
        }
    }

    public class Var
    {
        [XmlAttribute]
        public string varName;

        [XmlAttribute]
        public string varValue;

        public Var()
        {
            varName = "site";
            varValue = "10.184.23.95";
        }

        public Var(string varName, string varValue)
        {
            this.varName = varName;
            this.varValue = varValue;
        }
    }
}
