using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WindowsFormsApplication.Comparison
{
    public delegate bool IsEqualDelegate(object left, object right);

    public class DataTableObjectComparer<T>
    {
        private readonly T initial;
        private readonly T expected;
        private readonly T actual;
        private List<PropertyInfo> properties;
        private List<PropertyInfo> ignoringProperties;
        private Dictionary<PropertyInfo, IsEqualDelegate> propertyDelegate;

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
            List<string> ignoringProperties,
            Dictionary<string, IsEqualDelegate> propertyDelegate)
        {
            this.expected = expected;
            this.actual = actual;
            Init(ignoringProperties, propertyDelegate);
        }

        public DataTableObjectComparer(
            T initial,
            T expected,
            T actual,
            List<string> ignoringProperties,
            Dictionary<string, IsEqualDelegate> propertyDelegate)
        {
            this.initial = initial;
            this.expected = expected;
            this.actual = actual;
            Init(ignoringProperties, propertyDelegate);
        }

        private void Init(
            List<string> ignoringProperties,
            Dictionary<string, IsEqualDelegate> propertyDelegate)
        {
            this.properties = typeof(T).GetProperties().ToList();
            this.informationMessage = new StringBuilder();
            this.warningMessage = new StringBuilder();
            this.errorMessage = new StringBuilder();

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

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Comparing 2 objects of " + typeof(T).Name);
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
}
