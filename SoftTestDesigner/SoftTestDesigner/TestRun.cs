// -----------------------------------------------------------------------
// <copyright file="TestRun.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TestRun
{
    using System.Collections.Generic;
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
            testrunname = "ULP_CoreAdminPhase2_BulkEdit_LISQA8_Oct  7 2011 11:04PM";
            managername = "TFxIDXManager";
            branchName = "LFS00006";
            version = "2.0";

            Assignments = new List<Assignment>();
            Assignments.Add(new Assignment());

            Serializer.CreateInstance().SerializeToXML(
               this,
               this.GetType(),
               "sample.labrun.xml");
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
