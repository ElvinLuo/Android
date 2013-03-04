using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace WindowsFormsApplication
{
    public delegate bool IsEqualDelegate(object left, object right);

    public class DataTableObjectListComparer<T>
    {
        private List<T> initialList;
        private List<T> expectedList;
        private List<T> actualList;
        private PropertyInfo[] properties;
        private PropertyInfo[] primaryKeys;
        private PropertyInfo[] ignoringProperties;
        private Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate;

        private bool? isEqual = null;
        private string header;
        private StringBuilder informationMessage;
        private StringBuilder warningMessage;
        private StringBuilder errorMessage;

        public DataTableObjectListComparer(List<T> initialList, List<T> expectedList, List<T> actualList)
            : this(initialList, expectedList, actualList, null, null, null)
        {
        }

        public DataTableObjectListComparer(
            List<T> initialList,
            List<T> expectedList,
            List<T> actualList,
            PropertyInfo[] primaryKeys,
            PropertyInfo[] ignoringProperties,
            Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate)
        {
            this.initialList = initialList;
            this.expectedList = expectedList;
            this.actualList = actualList;
            this.properties = typeof(T).GetProperties();
            this.primaryKeys = primaryKeys;
            this.ignoringProperties = ignoringProperties;
            this.propertyDelegate = propertyDelegate;

            this.informationMessage = new StringBuilder();
            this.warningMessage = new StringBuilder();
            this.errorMessage = new StringBuilder();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Comparing list of DataTableObject");
            stringBuilder.AppendLine(GetProperty());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Initial list:");
            stringBuilder.AppendLine(GetValue(initialList));
            stringBuilder.AppendLine("Expected list:");
            stringBuilder.AppendLine(GetValue(expectedList));
            stringBuilder.AppendLine("Actual list:");
            stringBuilder.AppendLine(GetValue(actualList));
            header = stringBuilder.ToString();
        }

        private bool Matching(T left, T right)
        {
            if (left == null || right == null)
            {
                return false;
            }

            foreach (PropertyInfo property in primaryKeys)
            {
                object leftValue = property.GetValue(left, null) ?? "Null";
                object rightValue = property.GetValue(right, null) ?? "Null";

                if (leftValue.ToString() != rightValue.ToString())
                {
                    return false;
                }
            }

            return true;
        }

        private void FindMathingItem(
            List<T> leftList,
            List<T> rightList,
            List<T> matchingList,
            List<T> mismatchingList)
        {
            foreach (T left in leftList)
            {
                IEnumerable<T> foundItems = rightList.Where(i => Matching(i, left));
                int foundCount = foundItems.Count();

                if (foundCount == 1)
                {
                    matchingList.Add(left);
                }
                else
                {
                    mismatchingList.Add(left);
                }
            }
        }

        public bool Compare()
        {
            isEqual = true;
            List<T> expectedObjectNeedToCompare = new List<T>();
            List<T> actualObjectNeedToCompare = new List<T>();
            List<T> mismatchingExpectedObjectList = new List<T>();
            List<T> mismatchingActualObjectList = new List<T>();

            FindMathingItem(actualList, expectedList, actualObjectNeedToCompare, mismatchingActualObjectList);
            FindMathingItem(expectedList, actualList, expectedObjectNeedToCompare, mismatchingExpectedObjectList);

            if (mismatchingExpectedObjectList.Count() != 0)
            {
                isEqual = false;
                errorMessage.AppendLine();
                errorMessage.AppendLine("Below rows cannot be found in database.");
                mismatchingExpectedObjectList.ForEach(i => errorMessage.AppendLine(GetValue(i)));
            }

            if (mismatchingActualObjectList.Count() != 0)
            {
                isEqual = false;
                errorMessage.AppendLine();
                errorMessage.AppendLine("Below rows should not be added to database.");
                mismatchingActualObjectList.ForEach(i => errorMessage.AppendLine(GetValue(i)));
            }

            foreach (T expected in expectedObjectNeedToCompare)
            {
                DataTableObjectComparer<T> objectComparer;
                IEnumerable<T> foundInInitialList = initialList.Where(i => Matching(i, expected));
                T actual = actualObjectNeedToCompare.First(i => Matching(i, expected));

                if (foundInInitialList.Count() == 1)
                {
                    objectComparer = new DataTableObjectComparer<T>(
                        foundInInitialList.First(),
                        expected,
                        actual,
                        ignoringProperties,
                        propertyDelegate);
                }
                else
                {
                    objectComparer = new DataTableObjectComparer<T>(
                        expected,
                        actual,
                        ignoringProperties,
                        propertyDelegate);
                }

                bool isObjectEqual = objectComparer.Compare();
                string objectFinalMessage = "\r\n" + objectComparer.GetFinalMessage();
                if (isObjectEqual)
                {
                    if (objectComparer.GetWarningMessage().Length != 0)
                    {
                        warningMessage.AppendLine(objectFinalMessage);
                    }
                }
                else
                {
                    isEqual = false;
                    errorMessage.AppendLine(objectFinalMessage);
                }
            }

            return isEqual.Value;
        }

        public string GetFinalMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            StringBuilder stringBuilder = new StringBuilder(header);
            if (isEqual.Value)
            {
                if (warningMessage.Length == 0)
                {
                    stringBuilder.AppendLine("The actual object list is equal to the expected.");
                }
                else
                {
                    stringBuilder.AppendLine("The actual object list is equal to the expected, please notice the warning(s).");
                    stringBuilder.AppendLine(warningMessage.ToString());
                }
            }
            else
            {
                stringBuilder.AppendLine("The actual object list is not equal to the expected.");
                stringBuilder.AppendLine(errorMessage.ToString());
            }

            return stringBuilder.ToString();
        }

        public string GetInformationMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return informationMessage.ToString();
        }

        public string GetWarningMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return warningMessage.ToString();
        }

        public string GetErrorMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return errorMessage.ToString();
        }

        private string GetProperty()
        {
            StringBuilder message = new StringBuilder();
            properties.ToList().ForEach(i => message.Append(string.Format("{0}\t", i.Name)));
            return message.ToString();
        }

        private string GetValue(T dataTableObject)
        {
            if (dataTableObject == null)
            {
                return "The object is null.";
            }

            StringBuilder message = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(dataTableObject, null) ?? "Null";
                message.Append(string.Format("{0}\t", value));
            }
            return message.ToString();
        }

        private string GetValue(List<T> objList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            objList.ForEach(obj => stringBuilder.AppendLine(GetValue(obj)));
            return stringBuilder.ToString();
        }
    }

    public class DataTableObjectComparer<T>
    {
        private readonly T initial;
        private readonly T expected;
        private readonly T actual;
        private readonly PropertyInfo[] properties;
        private readonly PropertyInfo[] ignoringProperties;
        private readonly Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate;

        private bool? isEqual = null;
        private string header;
        private StringBuilder informationMessage;
        private StringBuilder warningMessage;
        private StringBuilder errorMessage;

        public DataTableObjectComparer(T expected, T actual)
            : this(expected, actual, null, null)
        {
        }

        public DataTableObjectComparer(T initial, T expected, T actual)
            : this(initial, expected, actual, null, null)
        {
        }

        public DataTableObjectComparer(
            T expected,
            T actual,
            PropertyInfo[] ignoringProperties,
            Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate)
        {
            this.expected = expected;
            this.actual = actual;
            this.properties = typeof(T).GetProperties();
            this.ignoringProperties = ignoringProperties;
            this.propertyDelegate = propertyDelegate;

            Init();
        }

        public DataTableObjectComparer(
            T initial,
            T expected,
            T actual,
            PropertyInfo[] ignoringProperties,
            Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate)
        {
            this.initial = initial;
            this.expected = expected;
            this.actual = actual;
            this.properties = typeof(T).GetProperties();
            this.ignoringProperties = ignoringProperties;
            this.propertyDelegate = propertyDelegate;

            Init();
        }

        private void Init()
        {
            this.informationMessage = new StringBuilder();
            this.warningMessage = new StringBuilder();
            this.errorMessage = new StringBuilder();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Comparing single DataTableObject");
            stringBuilder.AppendLine(GetProperty());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Initial: " + GetValue(initial));
            stringBuilder.AppendLine("Expected: " + GetValue(expected));
            stringBuilder.AppendLine("Actual: " + GetValue(actual));
            stringBuilder.AppendLine();
            header = stringBuilder.ToString();
        }

        public bool Compare()
        {
            isEqual = true;

            if (expected == null && actual != null)
            {
                isEqual = false;
                errorMessage.AppendLine("The actual value is not null while the expected is null.");
            }
            else if (expected != null && actual == null)
            {
                isEqual = false;
                errorMessage.AppendLine("The actual value is null while the expected is not null.");
            }
            else if (expected == null && actual == null)
            {
                isEqual = true;
                warningMessage.AppendLine("The actual value is null as expected.");
            }
            else
            {
                string propertyMessage = null;

                foreach (PropertyInfo property in properties)
                {
                    object initialPropertyValue =
                        initial == null ?
                        "Null" : property.GetValue(initial, null) ?? "Null";
                    object expectedPropertyValue = property.GetValue(expected, null) ?? "Null";
                    object actualPropertyValue = property.GetValue(actual, null) ?? "Null";

                    if (actualPropertyValue.ToString() == expectedPropertyValue.ToString())
                    {
                        propertyMessage = string.Format("{0} is correct.", property.Name);
                        LogPropertyInformation(
                            propertyMessage,
                            initialPropertyValue,
                            expectedPropertyValue,
                            actualPropertyValue);
                    }
                    else
                    {
                        if (propertyDelegate != null && propertyDelegate.ContainsKey(property))
                        {
                            if (propertyDelegate[property](expectedPropertyValue, actualPropertyValue))
                            {
                                propertyMessage = string.Format(
                                    "{0} is correct using customized comparison method.",
                                    property.Name);
                                LogPropertyWarning(
                                    propertyMessage,
                                    initialPropertyValue,
                                    expectedPropertyValue,
                                    actualPropertyValue);
                            }
                            else
                            {
                                isEqual = false;
                                propertyMessage = string.Format(
                                    "{0} is incorrect using customized comparison method.",
                                    property.Name);
                                LogPropertyError(
                                    propertyMessage,
                                    initialPropertyValue,
                                    expectedPropertyValue,
                                    actualPropertyValue);
                            }
                        }
                        else
                        {
                            if (ignoringProperties != null && ignoringProperties.Contains(property))
                            {
                                propertyMessage = string.Format("Comparison of {0} is ignored.", property.Name);
                                LogPropertyWarning(
                                    propertyMessage,
                                    initialPropertyValue,
                                    expectedPropertyValue,
                                    actualPropertyValue);
                            }
                            else
                            {
                                isEqual = false;
                                propertyMessage = string.Format("{0} is incorrect.", property.Name);
                                LogPropertyError(
                                    propertyMessage,
                                    initialPropertyValue,
                                    expectedPropertyValue,
                                    actualPropertyValue);
                            }
                        }
                    }
                }
            }
            return isEqual.Value;
        }

        public string GetFinalMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            StringBuilder stringBuilder = new StringBuilder(header);
            if (isEqual.Value)
            {
                if (warningMessage.Length == 0)
                {
                    stringBuilder.AppendLine("The actual value of the object is equal to the expected.");
                }
                else
                {
                    stringBuilder.AppendLine("The actual value of the object is equal to the expected, please notice the warning(s).");
                    stringBuilder.AppendLine(warningMessage.ToString());
                }
            }
            else
            {
                stringBuilder.AppendLine("The actual value of the object is not equal to the expected.");
                stringBuilder.AppendLine(errorMessage.ToString());
            }
            return stringBuilder.ToString();
        }

        public string GetInformationMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return informationMessage.ToString();
        }

        public string GetWarningMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return warningMessage.ToString();
        }

        public string GetErrorMessage()
        {
            if (!isEqual.HasValue)
            {
                return "Comparison is not performed, please call Compare() method of DataTableObjectComparer instance first.";
            }

            return errorMessage.ToString();
        }

        private void LogPropertyInformation(
            string header,
            object initialPropertyValue,
            object expectedPropertyValue,
            object actualPropertyValue)
        {
            LogProperty(
                header,
                initialPropertyValue,
                expectedPropertyValue,
                actualPropertyValue,
                informationMessage);
        }

        private void LogPropertyWarning(
            string header,
            object initialPropertyValue,
            object expectedPropertyValue,
            object actualPropertyValue)
        {
            LogPropertyInformation(
                header,
                initialPropertyValue,
                expectedPropertyValue,
                actualPropertyValue);
            LogProperty(
                header,
                initialPropertyValue,
                expectedPropertyValue,
                actualPropertyValue,
                warningMessage);
        }

        private void LogPropertyError(
            string header,
            object initialPropertyValue,
            object expectedPropertyValue,
            object actualPropertyValue)
        {
            LogPropertyInformation(
                header,
                initialPropertyValue,
                expectedPropertyValue,
                actualPropertyValue);
            LogProperty(
                header,
                initialPropertyValue,
                expectedPropertyValue,
                actualPropertyValue,
                errorMessage);
        }

        private void LogProperty(
            string header,
            object initialPropertyValue,
            object expectedPropertyValue,
            object actualPropertyValue,
            StringBuilder message)
        {
            message.AppendLine();
            message.AppendLine(header);
            message.AppendLine("Initial: " + initialPropertyValue);
            message.AppendLine("Expected: " + expectedPropertyValue);
            message.AppendLine("Actual: " + actualPropertyValue);
        }

        private string GetProperty()
        {
            StringBuilder message = new StringBuilder();
            properties.ToList().ForEach(i => message.Append(string.Format("{0}\t", i.Name)));
            return message.ToString();
        }

        private string GetValue(T dataTableObject)
        {
            if (dataTableObject == null)
            {
                return "The object is null.";
            }

            StringBuilder message = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(dataTableObject, null) ?? "Null";
                message.Append(string.Format("{0}\t", value));
            }
            return message.ToString();
        }
    }

    public class ComparisonMethod
    {
        public static bool CompareUpdateDate(object left, object right)
        {
            DateTime leftValue = System.Convert.ToDateTime(left.ToString());
            DateTime rightValue = System.Convert.ToDateTime(right.ToString());

            if (leftValue <= rightValue)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            PropertyInfo[] primaryKeys = new PropertyInfo[]
            {
                typeof(RatePlan_NoIdent).GetProperty("RatePlanID")
            };

            PropertyInfo[] ignoringProperties = new PropertyInfo[]
            {
                typeof(RatePlan_NoIdent).GetProperty("RatePlanCodeSupplier"),
                typeof(RatePlan_NoIdent).GetProperty("PersonCntIncluded"),
                typeof(RatePlan_NoIdent).GetProperty("ManageOnExtranetBool")
            };

            Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate;
            propertyDelegate = new Dictionary<PropertyInfo, IsEqualDelegate>();
            PropertyInfo property = typeof(RatePlan_NoIdent).GetProperty("UpdateDate");
            propertyDelegate[property] = ComparisonMethod.CompareUpdateDate;

            DataTableObjectComparer<RatePlan_NoIdent> objectComparer = new DataTableObjectComparer<RatePlan_NoIdent>(
                    TestData.testObjectComparisonSuccess1[0],
                    TestData.testObjectComparisonSuccess1[1],
                    TestData.testObjectComparisonSuccess1[2],
                    ignoringProperties,
                    propertyDelegate);
            bool isEqual = objectComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectComparer.GetFinalMessage());

            objectComparer = new DataTableObjectComparer<RatePlan_NoIdent>(
                    TestData.testObjectComparisonSuccess2[0],
                    TestData.testObjectComparisonSuccess2[1],
                    TestData.testObjectComparisonSuccess2[2],
                    ignoringProperties,
                    propertyDelegate);
            isEqual = objectComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectComparer.GetFinalMessage());

            objectComparer = new DataTableObjectComparer<RatePlan_NoIdent>(
                    TestData.testObjectComparisonWithError[0],
                    TestData.testObjectComparisonWithError[1],
                    TestData.testObjectComparisonWithError[2],
                    ignoringProperties,
                    propertyDelegate);
            isEqual = objectComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectComparer.GetFinalMessage());

            objectComparer = new DataTableObjectComparer<RatePlan_NoIdent>(
                    TestData.testObjectComparisonWithWarning[0],
                    TestData.testObjectComparisonWithWarning[1],
                    TestData.testObjectComparisonWithWarning[2],
                    ignoringProperties,
                    propertyDelegate);
            isEqual = objectComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectComparer.GetFinalMessage());

            objectComparer = new DataTableObjectComparer<RatePlan_NoIdent>(
                    null,
                    null,
                    TestData.testObjectComparisonWithWarningError[2],
                    ignoringProperties,
                    propertyDelegate);
            isEqual = objectComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectComparer.GetFinalMessage());

            DataTableObjectListComparer<RatePlan_NoIdent> objectListComparer = new DataTableObjectListComparer<RatePlan_NoIdent>(
                    TestData.testObjectListComparisonInitial,
                    TestData.testObjectListComparisonExpected,
                    TestData.testObjectListComparisonActual,
                    primaryKeys,
                    ignoringProperties,
                    propertyDelegate);
            isEqual = objectListComparer.Compare();
            if (isEqual)
            {
                System.Diagnostics.Debug.WriteLine("Pass");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Fail");
            }
            System.Diagnostics.Debug.WriteLine(objectListComparer.GetFinalMessage());
        }

        #region

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
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, origin);
            memoryStream.Seek(0, SeekOrigin.Begin);
            T result = (T)binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();
            return result;
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
            System.Diagnostics.Debug.WriteLine("Merging");
            table1.Merge(table2, false, MissingSchemaAction.Add);
            PrintValues(table1, "Merged With table1, schema added");
        }

        private static void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Row changed {0}\t{1}", e.Action, e.Row.ItemArray[0]);
        }

        private static void PrintValues(DataTable table, string label)
        {
            // Display the values in the supplied DataTable:
            System.Diagnostics.Debug.WriteLine(label);
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("\t " + row[col].ToString());
                }
                System.Diagnostics.Debug.WriteLine("");
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

}
