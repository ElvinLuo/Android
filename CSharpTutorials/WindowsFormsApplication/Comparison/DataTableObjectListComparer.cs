using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WindowsFormsApplication.Comparison
{
    public class DataTableObjectListComparer<T>
    {
        private List<T> initialList;
        private List<T> expectedList;
        private List<T> actualList;
        private List<PropertyInfo> properties;
        private List<PropertyInfo> primaryKeys;
        private List<string> ignoringPropertyName;
        private List<PropertyInfo> ignoringProperties;
        private Dictionary<string, IsEqualDelegate> propertyNameDelegate;
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
            List<string> primaryKeys,
            List<string> ignoringProperties,
            Dictionary<string, IsEqualDelegate> propertyDelegate)
        {
            this.initialList = initialList;
            this.expectedList = expectedList;
            this.actualList = actualList;
            this.properties = typeof(T).GetProperties().ToList();

            if (primaryKeys != null && primaryKeys.Count > 0)
            {
                this.primaryKeys = new List<PropertyInfo>();
                AddValidPropertyToList(primaryKeys, this.primaryKeys);
            }
            if (this.primaryKeys == null || this.primaryKeys.Count == 0)
            {
                this.primaryKeys = this.properties;
            }

            this.ignoringPropertyName = ignoringProperties;
            this.propertyNameDelegate = propertyDelegate;

            this.ignoringProperties = new List<PropertyInfo>();
            if (ignoringProperties != null && ignoringProperties.Count != 0)
            {
                foreach (string propertyName in ignoringProperties)
                {
                    PropertyInfo property = typeof(T).GetProperty(propertyName);
                    if (property != null)
                    {
                        this.ignoringProperties.Add(property);
                    }
                }
            }

            this.propertyDelegate = new Dictionary<PropertyInfo, IsEqualDelegate>();
            if (propertyDelegate != null && propertyDelegate.Count() != 0)
            {
                foreach (string propertyName in propertyDelegate.Keys)
                {
                    PropertyInfo property = typeof(T).GetProperty(propertyName);
                    if (property != null)
                    {
                        this.propertyDelegate[property] = propertyDelegate[propertyName];
                    }
                }
            }

            this.informationMessage = new StringBuilder();
            this.warningMessage = new StringBuilder();
            this.errorMessage = new StringBuilder();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Comparing 2 lists of " + typeof(T).Name);
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
                int foundCount = (foundItems == null) ? 0 : foundItems.Count();

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
                        ignoringPropertyName,
                        propertyNameDelegate);
                }
                else
                {
                    objectComparer = new DataTableObjectComparer<T>(
                        expected,
                        actual,
                        ignoringPropertyName,
                        propertyNameDelegate);
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

        private void AddValidPropertyToList(List<string> propertyNameList, List<PropertyInfo> propertyList)
        {
            foreach (string propertyName in propertyNameList)
            {
                PropertyInfo property = typeof(T).GetProperty(propertyName);
                if (property != null)
                {
                    propertyList.Add(property);
                }
            }
        }
    }
}
