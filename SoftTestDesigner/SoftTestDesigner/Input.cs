// -----------------------------------------------------------------------
// <copyright file="Input.cs" company="Expedia">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Input
    {
        private string prefix = "";
        private string suffix = "";

        /// <summary>
        /// Used to save all valid available config items, values and node
        /// </summary>
        private List<string[]> validConfigItems;

        /// <summary>
        /// Used to save all invalid available config items, values and node 
        /// </summary>
        private List<string[]> invalidConfigItems;

        public List<string> configItemNames;
        private Dictionary<string, string> softCases;

        Dictionary<string, string> SoftCases
        {
            get
            {
                if (softCases == null)
                { softCases = GetAllSoftCasesFromConfig(); }
                return softCases;
            }
            set { softCases = value; }
        }

        /// <summary>
        /// Initializes a new instance of the Input class.
        /// </summary>
        /// <param name="validConfigItems">Pass in valid</param>
        /// <param name="invalidConfigItems">Pass in invalid</param>
        public Input(
            List<string[]> validConfigItems,
            List<string[]> invalidConfigItems)
        {
            this.validConfigItems = validConfigItems;
            this.invalidConfigItems = invalidConfigItems;
        }

        public Dictionary<string, string> GetAllSoftCasesFromConfig()
        {
            if (softCases == null)
            {
                List<string> needToRemove = new List<string>();
                GetCombination(0);
                foreach (string[] conflict in invalidConfigItems)
                {
                    char splitCharacter = '=';
                    string[] firstCondition = conflict[0].Split(splitCharacter);
                    string firstConditionName = firstCondition[0];
                    string firstConditionValue = firstCondition[1];

                    string[] secondCondition = conflict[1].Split(splitCharacter);
                    string secondConditionName = secondCondition[0];
                    string secondConditionValue = secondCondition[1];

                    int firstIndex = configItemNames.IndexOf(firstConditionName);
                    int secondIndex = configItemNames.IndexOf(secondConditionName);

                    foreach (KeyValuePair<string, string> softCase in softCases)
                    {
                        string[] configValues = softCase.Value.Split(';');
                        if (configValues[firstIndex].ToLower().Equals(firstConditionValue.ToLower()) &&
                            configValues[secondIndex].ToLower().Equals(secondConditionValue.ToLower()))
                        {
                            needToRemove.Add(softCase.Key);
                        }
                    }
                }
                foreach (string key in needToRemove)
                {
                    softCases.Remove(key);
                }
            }
            return softCases;
        }

        /// <summary>
        /// Begin to generate
        /// </summary>
        public void GetCombination(int processingIndex)
        {
            Dictionary<string, string> temp;
            if (softCases == null)
            {
                temp = new Dictionary<string, string>();
            }
            else
            {
                temp = new Dictionary<string, string>(softCases);
                softCases.Clear();
            }

            char splitCharacter = '/';
            string[] configItemNodeNames = validConfigItems[processingIndex][1].Split(splitCharacter);
            string[] configItemValues = validConfigItems[processingIndex][2].Split(splitCharacter);

            if (temp.Count == 0)
            {
                configItemNames = new List<string>();
                softCases = new Dictionary<string, string>();
                for (int i = 0; i < configItemValues.Length; i++)
                {
                    string configItemNodeName = configItemNodeNames[i];
                    string configItemValue = configItemValues[i];
                    softCases.Add(prefix + configItemNodeName, configItemValue);
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> softCase in temp)
                {
                    for (int i = 0; i < configItemValues.Length; i++)
                    {
                        string configItemNodeName = configItemNodeNames[i];
                        string configItemValue = configItemValues[i];
                        string softCaseName;
                        if (processingIndex != validConfigItems.Count - 1)
                        {
                            softCaseName = softCase.Key + configItemNodeName;
                        }
                        else
                        {
                            softCaseName = softCase.Key + configItemNodeName + suffix;
                        }
                        softCases.Add(
                            softCaseName,
                            softCase.Value + "/" + configItemValue);
                    }
                }
            }

            configItemNames.Add(validConfigItems[processingIndex][0]);
            if (processingIndex != validConfigItems.Count - 1)
            {
                GetCombination(processingIndex + 1);
            }
        }

    }
}
