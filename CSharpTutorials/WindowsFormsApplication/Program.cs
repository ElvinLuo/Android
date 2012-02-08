using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WindowsFormsApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<string> stringList = new List<string> { "PDP1", "OBP2", "PDP1", "OBP2", "PDP1" };

            int index1;
            index1 = stringList.IndexOf("PDP1");
            index1 = stringList.IndexOf("OBP2");

            List<string[]> stringArrayList = new List<string[]>();
            string[] stringArray = new string[2];
            stringArray = new string[] { "PDP", "1" };
            stringArrayList.Add(stringArray);
            stringArray = new string[] { "OBP", "2" };
            stringArrayList.Add(stringArray);
            stringArray = new string[] { "PDP", "1" };
            stringArrayList.Add(stringArray);
            stringArray = new string[] { "OBP", "2" };
            stringArrayList.Add(stringArray);
            stringArray = new string[] { "PDP", "1" };
            stringArrayList.Add(stringArray);
            stringArrayList.Distinct();

            int index2;
            index2 = stringArrayList.IndexOf(new string[] { "PDP", "1" });
            index2 = stringArrayList.IndexOf(new string[] { "OBP", "2" });

            List<int[]> intArrayList = new List<int[]>();
            int[] intArray = new int[2];
            intArray = new int[] { 1, 2 };
            intArrayList.Add(intArray);
            intArray = new int[] { 2, 2 };
            intArrayList.Add(intArray);
            intArray = new int[] { 1, 2 };
            intArrayList.Add(intArray);
            intArray = new int[] { 2, 2 };
            intArrayList.Add(intArray);
            intArray = new int[] { 1, 2 };
            intArrayList.Add(intArray);
            intArrayList.Distinct();

            int index3;
            index3 = intArrayList.IndexOf(new int[] { 1, 2 });
            index3 = intArrayList.IndexOf(new int[] { 2, 2 });

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
            Console.WriteLine(i);
        }

        private static void MergeTables()
        {
            DataTable table1 = new DataTable("Items");

            // Add columns
            DataColumn idColumn = new DataColumn("id", typeof(System.Int32));
            DataColumn itemColumn = new DataColumn("item", typeof(System.Int32));
            table1.Columns.Add(idColumn);
            table1.Columns.Add(itemColumn);

            // Set the primary key column.
            table1.PrimaryKey = new DataColumn[] { idColumn };

            // Add RowChanged event handler for the table.
            table1.RowChanged += new System.Data.DataRowChangeEventHandler(Row_Changed);

            // Add ten rows.
            DataRow row;
            for (int i = 0; i <= 9; i++)
            {
                row = table1.NewRow();
                row["id"] = i;
                row["item"] = i;
                table1.Rows.Add(row);
            }

            // Accept changes.
            table1.AcceptChanges();
            PrintValues(table1, "Original values");

            // Create a second DataTable identical to the first.
            DataTable table2 = table1.Clone();

            // Add column to the second column, so that the 
            // schemas no longer match.

            // Add three rows. Note that the id column can't be the 
            // same as existing rows in the original table.
            row = table2.NewRow();
            row["id"] = 14;
            row["item"] = 774;
            table2.Rows.Add(row);

            row = table2.NewRow();
            row["id"] = 12;
            row["item"] = 555;
            table2.Rows.Add(row);

            row = table2.NewRow();
            row["id"] = 13;
            row["item"] = 665;
            table2.Rows.Add(row);

            // Merge table2 into the table1.
            DataRow[] dr = table1.Select("id > 3 and item < 8");
            Console.WriteLine("Merging");
            table1.Merge(table2, false, MissingSchemaAction.Add);
            PrintValues(table1, "Merged With table1, schema added");
        }

        private static void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            Console.WriteLine("Row changed {0}\t{1}", e.Action, e.Row.ItemArray[0]);
        }

        private static void PrintValues(DataTable table, string label)
        {
            // Display the values in the supplied DataTable:
            Console.WriteLine(label);
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("\t " + row[col].ToString());
                }
                Console.WriteLine();
            }
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

}
