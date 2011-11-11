// -----------------------------------------------------------------------
// <copyright file="Input.cs" company="Expedia">
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
    public class Input
    {
        private string prefix = "CheckUI.";
        private string suffix = "";

        /// <summary>
        /// Used to save all valid available config items, values and node
        /// </summary>
        private List<string[]> validConfigItems;

        /// <summary>
        /// Used to save all invalid available config items, values and node 
        /// </summary>
        private List<string[]> invalidConfigItems;

        private Dictionary<string, string> softCases;

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
                GetCombination(0);
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
            string[] configItemValues = validConfigItems[processingIndex][1].Split(new char[] { '/' });
            string[] configItemNodeNames = validConfigItems[processingIndex][2].Split(new char[] { '/' });

            if (temp.Count == 0)
            {
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
                            softCase.Value + "\t" + configItemValue);
                    }
                }
            }

            if (processingIndex != validConfigItems.Count - 1)
            {
                GetCombination(processingIndex + 1);
            }
        }

    }
}
