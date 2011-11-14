// -----------------------------------------------------------------------
// <copyright file="SoftTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftCaseGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoftTest
    {
        public int id;
        public string testTeam;
        public string category;
        public int risktier;
        public Invoke invoke;
        public List<string> parameters;
        public List<Data> testData;

        public SoftTest()
        {
            id = 77091;
            testTeam = "Lodging Inventory Systems";
            category = "Regression";
            risktier = 2;
            invoke = new Invoke("", "");
            parameters = new List<string>();
            testData = new List<Data>();
            testData.Add(new Data("", "", ""));
            testData.Add(new Data("", "", ""));
        }
    }

    public struct Invoke
    {
        public string module;
        public string method;

        public Invoke(string module, string method)
        {
            this.module = "HotelTest.dll";
            this.method = "Expedia.Automation.Test.Hotels.ExpediaServiceFee.Functional.ExpediaServiceFeeBulkUpdate";
        }
    }

    public struct Data
    {
        public string dataName;
        public string defaultValue;
        public string stripingValues;

        public Data(string dataName, string defaultValue, string stripingValues)
        {
            this.dataName = "ContainAgencyHotel";
            this.defaultValue = "true";
            this.stripingValues = string.Empty;
        }
    }
}
