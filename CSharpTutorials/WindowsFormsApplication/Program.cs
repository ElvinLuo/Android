using Expedia.Automation.ExpSOATemplates.Hotels.APM.Schemas.RatePlanGet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using WindowsFormsApplication.File;

namespace WindowsFormsApplication
{
    public class TestReport
    {
        public TestRunResult TestRunTotals_DailyProjects { get; set; }
        public TestRunResult TestRunTotals_CurrentProjects { get; set; }
        public List<TestRunResult> TestRunResults { get; set; }

        public TestReport()
        {
            TestRunTotals_DailyProjects = new TestRunResult();
            TestRunTotals_CurrentProjects = new TestRunResult();
            TestRunResults = new List<TestRunResult>();
        }

        public class TestRunResult
        {
            public string TrxFileName { get; set; }
            public int PassCount { get; set; }
            public int HighPriorityFailCount { get; set; }
            public int FailCount { get; set; }
            public int SkipCount { get; set; }
            public string Notes { get; set; }
            public List<TestMethodResult> TestMethodResults { get; set; }

            public TestRunResult()
            {
                TestMethodResults = new List<TestMethodResult>();
            }

            public int GetTotalTests() { return PassCount + FailCount + SkipCount; }
            public int GetPercentPass()
            {
                if (PassCount == 0) return 0;

                return (PassCount * 100) / GetTotalTests();
            }
        }

        public class TestMethodResult
        {
            public string TestMethodClass { get; set; }
            public string TestMethodName { get; set; }
            public string TestRowName { get; set; }
            public string TestOutcome { get; set; }
            public string TestPriority { get; set; }
            public string TestFailureReason { get; set; }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var time = FromUnixTime(1418515200000);
            var sec = ToUnixTime(time);
        }

        public static DateTime FromUnixTime(double unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(System.Convert.ToDouble(unixTime));
        }

        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return System.Convert.ToInt64((date - epoch).TotalMilliseconds);
        }

        private static bool Validate(string uri)
        {
            try
            {
                WebClient client = new WebClient();
                client.Credentials = new System.Net.NetworkCredential("DeployItLIS", "d3pl0yLi$");
                client.DownloadString(uri);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region

        public static string GetFirstGuidString(string input)
        {
            string guidPattern = @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})";
            Match match = Regex.Match(input, guidPattern);
            return match.Success ? match.Groups[0].Value : string.Empty;
        }

        public static IEnumerable<IRowContext> GenerateRowContext()
        {
            for (int i = 0; i < 10; i++)
            {
                DataTable dt = new DataTable();
                yield return new RowContext<IDataTable>(dt, i, i);
            }
        }

        public struct DayOfWeekMask
        {
            public const byte Sunday = 0x01;
            public const byte Monday = 0x02;
            public const byte Tuesday = 0x04;
            public const byte Wednesday = 0x08;
            public const byte Thursday = 0x10;
            public const byte Friday = 0x20;
            public const byte Saturday = 0x40;
            public const byte All = 0x7f;

            public static byte FromSystem(DayOfWeek value)
            {
                return (byte)(1 << ((byte)value));
            }

            public static int ConvertToSystem(List<int> dayOfWeekMaskList)
            {
                int result = 0;
                dayOfWeekMaskList.ForEach(i => result |= i);
                return result;
            }
        }

        public static T CloneValue<T>(T origin)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, origin);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }

            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //MemoryStream memoryStream = new MemoryStream();
            //binaryFormatter.Serialize(memoryStream, origin);
            //memoryStream.Seek(0, SeekOrigin.Begin);
            //T result = (T)binaryFormatter.Deserialize(memoryStream);
            //memoryStream.Close();
            //return result;
        }

        public static void CopyValue(object origin, object target)
        {
            object value;
            PropertyInfo[] originProperties = (origin.GetType()).GetProperties();
            PropertyInfo[] targetProperties = (target.GetType()).GetProperties();

            foreach (PropertyInfo originProperty in originProperties)
            {
                foreach (PropertyInfo targetProperty in targetProperties)
                {
                    if (targetProperty.CanWrite &&
                        originProperty.Name == targetProperty.Name)
                    {
                        value = originProperty.GetValue(origin, null);
                        targetProperty.SetValue(target, value, null);
                    }
                }
            }
        }

        public static bool SetObjectProperty(object obj, string propertyName, object propertyValue)
        {
            string[] propertyNodes = propertyName.Split('.');
            int length = propertyNodes.Length;

            for (int i = 0; i < length; i++)
            {
                if (obj == null) return false;

                bool isMatched = false;
                PropertyInfo[] properties =
                    obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == propertyNodes[i])
                    {
                        if (i == length - 1)
                        {
                            property.SetValue(obj, propertyValue, null);
                            return true;
                        }

                        isMatched = true;
                        obj = property.GetValue(obj, null);
                        break;
                    }
                }

                if (!isMatched) return false;
            }

            return false;
        }

        public static void CommentedCode()
        {
            //string table = "RateRuleSiteType";
            //string folder = @"C:\Documents and Settings\v-elluo\Desktop\";
            //FileReader fileReader = new FileReader();
            //List<string> columns = fileReader.ReadFile(string.Format("{0}{1}Columns.txt", folder, table));
            //List<string> codeLines = fileReader.ReadFile(string.Format("{0}{1}.txt", folder, table));
            //SqlConnection myConnection = new SqlConnection(
            //    "Data Source=lodginginventorytx1.db.LISQA3.sb.karmalab.net,1433;Initial Catalog=LodgingInventoryMaster;Integrated Security=SSPI");

            //try
            //{
            //    SqlDataReader myReader = null;
            //    StringBuilder output = new StringBuilder();
            //    foreach (string column in columns)
            //    {
            //        StringBuilder values = new StringBuilder();
            //        myConnection.Open();
            //        string select = string.Format(
            //            "SELECT * FROM (SELECT DISTINCT CAST({0} AS VARCHAR(MAX)) AS VALUE FROM dbo.{1} WITH (NOLOCK)) AS T ORDER BY T.VALUE",
            //            column,
            //            table);
            //        SqlCommand myCommand = new SqlCommand(select, myConnection);
            //        myReader = myCommand.ExecuteReader();
            //        int count = 0;
            //        while (myReader.Read())
            //        {
            //            count++;
            //            values.Append(string.Format("{0}, ", myReader["VALUE"]));
            //            if (count > 10) break;
            //        }
            //        myConnection.Close();

            //        string outputLine = column;
            //        for (int i = 0; i < codeLines.Count; i++)
            //        {
            //            string line = codeLines.ElementAt(i);
            //            if (line.Contains(column))
            //            {
            //                int index;
            //                if (line.Contains(",") || line.Contains(";"))
            //                {
            //                    index = line.IndexOf("=");
            //                    if (index > 0)
            //                    {
            //                        outputLine = string.Format("{0}\t{1}\t{2}", column, values.ToString(), line.Substring(index + 2));
            //                    }
            //                }
            //                else
            //                {
            //                    string targetLine = line;
            //                    if (i + 2 < codeLines.Count)
            //                    {
            //                        targetLine = codeLines.ElementAt(i + 1).Trim() + codeLines.ElementAt(i + 2).Trim();
            //                    }
            //                    index = targetLine.IndexOf(":");
            //                    outputLine = string.Format("{0}\t{1}\t{2}", column, values.ToString(), targetLine);
            //                    i += 2;
            //                }
            //            }
            //        }
            //        output.AppendLine(outputLine);
            //    }

            //    string final = table + "\r\n\r\n" + output.ToString();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
            //finally
            //{
            //    myConnection.Close();
            //}
            FileReader reader = new FileReader();
            List<string> lines = reader.ReadFile("C:\\Documents and Settings\\v-elluo\\Desktop\\Expected.txt");
            List<string> actual = reader.ReadFile("C:\\Documents and Settings\\v-elluo\\Desktop\\Actual.txt");

            string prefix = "Expedia.Automation.Test.Hotels.APM.RatePlanUpdate.Negative.RatePlanUpdateNegativeTest.";
            List<string> expected = new List<string>();
            foreach (string line in lines)
            {
                if (line.Contains(prefix))
                {
                    int index = line.IndexOf(prefix);
                    string methodName =
                        line.Substring(index + prefix.Length, line.Length - index - prefix.Length - 2);
                    expected.Add(methodName);
                }
            }

            StringBuilder message = new StringBuilder();
            message.AppendLine("Below methods in LRM should be deleted:");
            foreach (string method in actual)
            {
                if (!expected.Contains(method))
                {
                    message.AppendLine(method);
                }
            }

            message.AppendLine();
            message.AppendLine("Below methods should be added to LRM:");
            foreach (string method in expected)
            {
                if (!actual.Contains(method))
                {
                    message.AppendLine(method);
                }
            }
            List<int> dayOfWeekMaskList = new List<int>()
            {
                //DayOfWeekMask.Monday,
                //DayOfWeekMask.Tuesday,
                DayOfWeekMask.Wednesday,
                //DayOfWeekMask.Thursday,
                //DayOfWeekMask.Friday,
                //DayOfWeekMask.Saturday,
                DayOfWeekMask.Sunday,
                DayOfWeekMask.Monday,
                DayOfWeekMask.Tuesday,
                //DayOfWeekMask.Wednesday,
                //DayOfWeekMask.Thursday,
                //DayOfWeekMask.Friday,
                //DayOfWeekMask.Saturday,
                DayOfWeekMask.Sunday
            };

            int value = DayOfWeekMask.ConvertToSystem(dayOfWeekMaskList);
            RatePlan_NoIdent ratePlan = new RatePlan_NoIdent();
            RatePlan_NoIdent copiedRatePlan;
            ratePlan.RatePlanID = 1;
            ratePlan.RatePlanTypeID = 2;

            List<RatePlan_NoIdent> ratePlanList = new List<RatePlan_NoIdent>();
            ratePlanList.Add(ratePlan);

            List<RatePlan_NoIdent> copiedRatePlanList;

            //CopyValue(ratePlan, copiedRatePlan);
            copiedRatePlan = CloneValue(ratePlan);
            copiedRatePlanList = CloneValue(ratePlanList);

            List<MyList> list = new List<MyList>
            { 
                new MyList(0),
                new MyList(1)
            };
            IEnumerable<MyList> r1 = list.Where(m => m.age == 1);
            IEnumerable<MyList> r2 = list.Where(m => m.age == 2);

            //const byte Sunday = 0x01;
            //const byte Monday = 0x02;
            //const byte Tuesday = 0x04;
            //const byte Wednesday = 0x08;
            //const byte Thursday = 0x10;
            //const byte Friday = 0x20;
            //const byte Saturday = 0x40;
            //const byte All = 0x7f;

            //System.Diagnostics.Debug.WriteLine(Sunday + Monday);

            //BrowserControllerForm form = new BrowserControllerForm();
            //Application.Run(form);

            Name name = new Name();
            name.FirstName = "Elvin";
            name.LastName = "Luo";

            Person p = new Person();
            p.Age = 0;
            SetObjectProperty(p, "Age", 10);
            SetObjectProperty(p, "Name", name);
            SetObjectProperty(p, "Name.FirstName", "New first name");
            SetObjectProperty(p, "Name.LastName", "New last name");
            //int Expedia = 0x00080000;
            //int ExpediaCorporate = 0x00800000;
            //int ExpediaPackage = 0x01000000;
            //System.Diagnostics.Debug.WriteLine(Expedia);
            //System.Diagnostics.Debug.WriteLine(ExpediaCorporate);
            //System.Diagnostics.Debug.WriteLine(ExpediaPackage);

            //string source = "~!@#$%^&()_+{}|:\"<>?`-=[]\\;',./";
            //char[] array = source.ToCharArray();

            //DataTable table = new DataTable();
            //table.Columns.Add("c1");
            //table.Columns.Add("c2");
            //string[] rowString = new string[] { "PDP", "1" };
            //table.Rows.Add(rowString);
            //rowString = new string[] { "PDP", "2" };
            //table.Rows.Add(rowString);
            //rowString = new string[] { "PPP", "2" };
            //table.Rows.Add(rowString);
            //DataRow[] rows = table.Select("NOT c1='PDP' AND c2='2'");

            //List<string> stringList = new List<string> { "PDP1", "OBP2", "PDP1", "OBP2", "PDP1" };

            //int index1;
            //index1 = stringList.IndexOf("PDP1");
            //index1 = stringList.IndexOf("OBP2");

            //List<string[]> stringArrayList = new List<string[]>();
            //string[] stringArray = new string[2];
            //stringArray = new string[] { "PDP", "1" };
            //stringArrayList.Add(stringArray);
            //stringArray = new string[] { "OBP", "2" };
            //stringArrayList.Add(stringArray);
            //stringArray = new string[] { "PDP", "1" };
            //stringArrayList.Add(stringArray);
            //stringArray = new string[] { "OBP", "2" };
            //stringArrayList.Add(stringArray);
            //stringArray = new string[] { "PDP", "1" };
            //stringArrayList.Add(stringArray);
            //stringArrayList.Distinct();

            //int index2;
            //index2 = stringArrayList.IndexOf(new string[] { "PDP", "1" });
            //index2 = stringArrayList.IndexOf(new string[] { "OBP", "2" });

            //List<int[]> intArrayList = new List<int[]>();
            //int[] intArray = new int[2];
            //intArray = new int[] { 1, 2 };
            //intArrayList.Add(intArray);
            //intArray = new int[] { 2, 2 };
            //intArrayList.Add(intArray);
            //intArray = new int[] { 1, 2 };
            //intArrayList.Add(intArray);
            //intArray = new int[] { 2, 2 };
            //intArrayList.Add(intArray);
            //intArray = new int[] { 1, 2 };
            //intArrayList.Add(intArray);
            //intArrayList.Distinct();

            //int index3;
            //index3 = intArrayList.IndexOf(new int[] { 1, 2 });
            //index3 = intArrayList.IndexOf(new int[] { 2, 2 });

            //string str1 = "1/2/";
            //string str2 = "/1/2";

            //string[] array = str1.Split('/');
            //array = str2.Split('/');

            //TestInstance a = new TestInstance(1, 2);
            //TestInstance b = new TestInstance();
            //b = a;
            //b.i = 3;
            //b.j = 4;

            //Convert<int>(1);
            //Convert<string>("s");
            //Convert<bool>(true);
            //MergeTables();

            //int originalV = 3;
            //int originalH = (originalV & 2) >> 1;
            //int originalL = originalV & 1;
            //int? h = null;
            //int? l = null;
            //int result = 10 * (h.HasValue ? h.Value : originalH) +
            //    (l.HasValue ? l.Value : originalL);
            //int j = System.Convert.ToInt32(result.ToString(), 2);

            //Dictionary<string, string> expected = new Dictionary<string, string>
            //{ { "200021792", "289400" },
            //{ "200021030", "Automation 000711182612" } };

            //Dictionary<string, string> actual = new Dictionary<string, string>
            //{{ "200021030", "Automation 000711182612" },
            //{ "200021792", "289400" }};

            //bool result = IsEquals(expected, actual);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new BrowserControllerForm());
        }

        public class TestInstance
        {
            public int i, j;

            public TestInstance()
            {
            }

            public TestInstance(int i, int j)
            {
                this.i = i;
                this.j = j;
            }

        }

        private static void Convert<T>(T value)
        {
            int? i = value as int?;
            string s = value as string;
            bool? b = value as bool?;
            System.Diagnostics.Debug.WriteLine(i);
        }

        private static void MergeTables()
        {
            //DataTable table1 = new DataTable("Items");

            //// Add columns
            //DataColumn idColumn = new DataColumn("id", typeof(System.Int32));
            //DataColumn itemColumn = new DataColumn("item", typeof(System.Int32));
            //table1.Columns.Add(idColumn);
            //table1.Columns.Add(itemColumn);

            //// Set the primary key column.
            //table1.PrimaryKey = new DataColumn[] { idColumn };

            //// Add RowChanged event handler for the table.
            //table1.RowChanged += new System.Data.DataRowChangeEventHandler(Row_Changed);

            //// Add ten rows.
            //DataRow row;
            //for (int i = 0; i <= 9; i++)
            //{
            //    row = table1.NewRow();
            //    row["id"] = i;
            //    row["item"] = i;
            //    table1.Rows.Add(row);
            //}

            //// Accept changes.
            //table1.AcceptChanges();
            //PrintValues(table1, "Original values");

            //// Create a second DataTable identical to the first.
            //DataTable table2 = table1.Clone();

            //// Add column to the second column, so that the 
            //// schemas no longer match.

            //// Add three rows. Note that the id column can't be the 
            //// same as existing rows in the original table.
            //row = table2.NewRow();
            //row["id"] = 14;
            //row["item"] = 774;
            //table2.Rows.Add(row);

            //row = table2.NewRow();
            //row["id"] = 12;
            //row["item"] = 555;
            //table2.Rows.Add(row);

            //row = table2.NewRow();
            //row["id"] = 13;
            //row["item"] = 665;
            //table2.Rows.Add(row);

            //// Merge table2 into the table1.
            //DataRow[] dr = table1.Select("id > 3 and item < 8");
            //System.Diagnostics.Debug.WriteLine("Merging");
            //table1.Merge(table2, false, MissingSchemaAction.Add);
            //PrintValues(table1, "Merged With table1, schema added");
        }

        private static void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Row changed {0}\t{1}", e.Action, e.Row.ItemArray[0]);
        }

        private static void PrintValues(DataTable table, string label)
        {
            // Display the values in the supplied DataTable:
            System.Diagnostics.Debug.WriteLine(label);
            //foreach (DataRow row in table.Rows)
            //{
            //    foreach (DataColumn col in table.Columns)
            //    {
            //        Console.Write("\t " + row[col].ToString());
            //    }
            //    System.Diagnostics.Debug.WriteLine("");
            //}
        }

        static bool IsEquals(Dictionary<string, string> actual, Dictionary<string, string> expected)
        {
            if (actual.Count != expected.Count)
            {
                return false;
            }
            else
            {
                foreach (KeyValuePair<string, string> pair in actual)
                {
                    if (!expected.Contains(pair))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

    public class Person
    {
        public Name Name { get; set; }
        public int Age { get; set; }
    }

    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

        #endregion

    [Serializable]
    public class RatePlan_NoIdent
    {
        #region Table Fields

        public Int32 RatePlanID { get; set; }

        public Int32 RatePlanTypeID { get; set; }

        public Int32 ActiveStatusTypeID { get; set; }

        public String RatePlanCodeSupplier { get; set; }

        public Int16 PersonCntIncluded { get; set; }

        public Boolean ManageOnExtranetBool { get; set; }

        public DateTime UpdateDate { get; set; }

        public Int32 UpdateTUID { get; set; }

        #endregion
    }

    public class TestData
    {
        public static List<RatePlan_NoIdent> testObjectComparisonSuccess1 = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 1,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 1,
                ManageOnExtranetBool = true,
                UpdateTUID = 100,
                UpdateDate = Convert.ToDateTime("2/28/2013 17:27:07")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            }
        };

        public static List<RatePlan_NoIdent> testObjectComparisonSuccess2 = new List<RatePlan_NoIdent>()
        {
            null,
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            }
        };

        public static List<RatePlan_NoIdent> testObjectComparisonWithWarning = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 1,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 1,
                ManageOnExtranetBool = true,
                UpdateTUID = 100,
                UpdateDate = Convert.ToDateTime("2/28/2013 17:27:07")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = true,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 19:29:09")
            }
        };

        public static List<RatePlan_NoIdent> testObjectComparisonWithWarningError = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 1,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 1,
                ManageOnExtranetBool = true,
                UpdateTUID = 100,
                UpdateDate = Convert.ToDateTime("2/28/2013 17:27:07")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 111,
                UpdateDate = Convert.ToDateTime("2/28/2013 19:29:09")
            }
        };

        public static List<RatePlan_NoIdent> testObjectComparisonWithError = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 1,
                RatePlanCodeSupplier = "initial rate plan",
                PersonCntIncluded = 1,
                ManageOnExtranetBool = true,
                UpdateTUID = 100,
                UpdateDate = Convert.ToDateTime("2/28/2013 17:27:07")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 110,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "updated rate plan",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 111,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            }
        };

        public static List<RatePlan_NoIdent> testObjectListComparisonInitial = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 2,
                RatePlanTypeID = 2,
                ActiveStatusTypeID = 2,
                RatePlanCodeSupplier = "initial rate plan 2",
                PersonCntIncluded = 2,
                ManageOnExtranetBool = false,
                UpdateTUID = 200,
                UpdateDate = Convert.ToDateTime("2/28/2013 18:28:08")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 1,
                ActiveStatusTypeID = 1,
                RatePlanCodeSupplier = "initial rate plan 1",
                PersonCntIncluded = 1,
                ManageOnExtranetBool = true,
                UpdateTUID = 100,
                UpdateDate = Convert.ToDateTime("2/28/2013 17:27:07")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 3,
                RatePlanTypeID = 3,
                ActiveStatusTypeID = 3,
                RatePlanCodeSupplier = "initial rate plan 3",
                PersonCntIncluded = 3,
                ManageOnExtranetBool = true,
                UpdateTUID = 300,
                UpdateDate = Convert.ToDateTime("2/28/2013 19:29:09")
            }
        };

        public static List<RatePlan_NoIdent> testObjectListComparisonExpected = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 10,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "expected rate plan 1",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = false,
                UpdateTUID = 1000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 2,
                RatePlanTypeID = 20,
                ActiveStatusTypeID = 20,
                RatePlanCodeSupplier = "expected rate plan 2",
                PersonCntIncluded = 20,
                ManageOnExtranetBool = true,
                UpdateTUID = 2000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 3,
                RatePlanTypeID = 30,
                ActiveStatusTypeID = 30,
                RatePlanCodeSupplier = "expected rate plan 3",
                PersonCntIncluded = 30,
                ManageOnExtranetBool = false,
                UpdateTUID = 3000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 4,
                RatePlanTypeID = 40,
                ActiveStatusTypeID = 40,
                RatePlanCodeSupplier = "expected rate plan 3",
                PersonCntIncluded = 40,
                ManageOnExtranetBool = false,
                UpdateTUID = 4000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            }
        };

        public static List<RatePlan_NoIdent> testObjectListComparisonActual = new List<RatePlan_NoIdent>()
        {
            new RatePlan_NoIdent()
            {
                RatePlanID = 1,
                RatePlanTypeID = 11,
                ActiveStatusTypeID = 10,
                RatePlanCodeSupplier = "actual rate plan 1",
                PersonCntIncluded = 10,
                ManageOnExtranetBool = true,
                UpdateTUID = 1000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 2,
                RatePlanTypeID = 2,
                ActiveStatusTypeID = 20,
                RatePlanCodeSupplier = "actual rate plan 2",
                PersonCntIncluded = 20,
                ManageOnExtranetBool = true,
                UpdateTUID = 2000,
                UpdateDate = Convert.ToDateTime("2/28/2013 10:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 30,
                RatePlanTypeID = 30,
                ActiveStatusTypeID = 30,
                RatePlanCodeSupplier = "actual rate plan 3",
                PersonCntIncluded = 30,
                ManageOnExtranetBool = false,
                UpdateTUID = 3000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            },
            new RatePlan_NoIdent()
            {
                RatePlanID = 40,
                RatePlanTypeID = 40,
                ActiveStatusTypeID = 40,
                RatePlanCodeSupplier = "expected rate plan 3",
                PersonCntIncluded = 40,
                ManageOnExtranetBool = false,
                UpdateTUID = 4000,
                UpdateDate = Convert.ToDateTime("2/28/2013 20:30:10")
            }
        };
    }

    public class MyList
    {
        public int age;

        public MyList(int age)
        {
            this.age = age;
        }
    }

    public class RowContext<TTable> : IRowContext
    where TTable : IDataTable
    {
        public TTable Table { get; set; }
        public int Index { get; set; }
        public int RowCount { get; set; }
        public int StartingIndex { get; private set; }

        public int RemainingUncheckedRowsCount
        {
            get { return (Index >= StartingIndex) ? (RowCount - Index + StartingIndex) : (StartingIndex - Index); }
        }

        public RowContext(TTable table, int index, int startingIndex)
        {
            Table = table;
            Index = index;
            RowCount = table.Count;
            StartingIndex = startingIndex;
        }

        public int GetIndex(int offset)
        {
            return (Index + offset) % RowCount;
        }
    }

    public interface IRowContext
    {
        int Index { get; }
        int GetIndex(int offset);
        int RemainingUncheckedRowsCount { get; }
        int RowCount { get; }
        int StartingIndex { get; }
    }

    public class DataTable : IDataTable
    {
        public int Count { get { return 0; } }
    }

    public interface IDataTable
    {
        int Count { get; }
    }
}
