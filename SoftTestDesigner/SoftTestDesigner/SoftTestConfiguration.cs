// -----------------------------------------------------------------------
// <copyright file="MetaData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftCaseGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoftTestConfiguration
    {
        #region
        private int configItemsCount;
        private ConfigItem[] allConfigItems;
        private ConfigItem[] fullConfigItems;
        private ConfigItem[] randomConfigItems;
        private List<RestrictionItem> restrictionItems;
        private List<List<RestrictionItem>> restrictionRuleList;

        public List<string> itemNames;
        public List<int[]> indexResultList;
        public List<string[]> valueResultList;
        #endregion

        public SoftTestConfiguration(
            DataGridViewRowCollection configItemRows,
            DataGridViewRowCollection restrictionRows)
        {
            DataGridViewRow row;
            List<ConfigItem> fullConfigItemList = new List<ConfigItem>();
            List<ConfigItem> randomConfigItemList = new List<ConfigItem>();

            string item, names, values, random, coverages;

            for (int i = 0; i < configItemRows.Count - 1; i++)
            {
                row = configItemRows[i];
                item = row.Cells[1].Value.ToString();
                names = row.Cells[2].Value.ToString();
                values = row.Cells[3].Value.ToString();
                random = row.Cells[4].Value.ToString();
                coverages = row.Cells[5].Value.ToString();

                if ((bool)row.Cells[0].Value)
                {
                    if ((bool)row.Cells[4].Value)
                    {
                        randomConfigItemList.Add(new ConfigItem(item, names, values, random, coverages));
                    }
                    else
                    {
                        fullConfigItemList.Add(new ConfigItem(item, names, values, random, coverages));
                    }
                }

                configItemsCount = fullConfigItemList.Count + randomConfigItemList.Count;
                itemNames = new List<string>();
                allConfigItems = new ConfigItem[configItemsCount];
                fullConfigItems = new ConfigItem[fullConfigItemList.Count];
                randomConfigItems = new ConfigItem[randomConfigItemList.Count];

                for (int j = 0; j < fullConfigItemList.Count; j++)
                {
                    allConfigItems[j] = fullConfigItemList.ElementAt(j);
                    itemNames.Add(fullConfigItemList.ElementAt(j).item);
                    fullConfigItems[j] = fullConfigItemList.ElementAt(j);
                }

                for (int j = 0; j < randomConfigItemList.Count; j++)
                {
                    allConfigItems[fullConfigItemList.Count + j] = randomConfigItemList.ElementAt(j);
                    itemNames.Add(randomConfigItemList.ElementAt(j).item);
                    randomConfigItems[j] = randomConfigItemList.ElementAt(j);
                }
            }

            string restrictionString;
            string[] restrictionItems;
            List<RestrictionItem> rule;
            this.restrictionItems = new List<RestrictionItem>();
            this.restrictionRuleList = new List<List<RestrictionItem>>();

            for (int rowIndex = 0; rowIndex < restrictionRows.Count - 1; rowIndex++)
            {
                DataGridViewRow rrow = restrictionRows[rowIndex];
                if (!(bool)rrow.Cells[0].Value) continue;

                if (rrow.Cells[1].Value == null) continue;

                int ruleItemCount = 0;
                rule = new List<RestrictionItem>();
                restrictionString = rrow.Cells[1].Value.ToString();
                restrictionItems = restrictionString.Split(" ".ToCharArray());
                foreach (string ri in restrictionItems)
                {
                    string[] nameValue;
                    int indexInConfigItemList, indexInConfigValues;

                    if (ri.Contains("="))
                    {
                        ruleItemCount++;
                        nameValue = ri.Split("=".ToCharArray());
                        for (int i = 0; i < configItemsCount; i++)
                        {
                            if (nameValue[0].ToLower().Equals(allConfigItems[i].item.ToLower()))
                            {
                                indexInConfigItemList = i;
                                for (int j = 0; j < allConfigItems[i].values.Length - 1; j++)
                                {
                                    if (!nameValue[1].ToLower().Equals(allConfigItems[i].values[j].ToLower()))
                                    { continue; }

                                    indexInConfigValues = j;
                                    RestrictionItem restrictionItem = new RestrictionItem(indexInConfigItemList, indexInConfigValues);

                                    if (!AlreadyExists<RestrictionItem>(this.restrictionItems, restrictionItem))
                                    { this.restrictionItems.Add(restrictionItem); }

                                    if (!AlreadyExists<RestrictionItem>(rule, restrictionItem))
                                    { rule.Add(restrictionItem); }
                                }
                            }
                        }
                    }
                }
                if (rule.Count == ruleItemCount)
                {
                    rule.Sort();
                    this.restrictionRuleList.Add(rule);
                }
            }
            this.restrictionItems.Sort();
        }

        public void GetResult()
        {
            valueResultList = new List<string[]>();
            GetMatrixWithFilters(allConfigItems, valueResultList, true);
        }

        private void ExpandMatrixWithRandomItems()
        {
            for (int j = 0; j < indexResultList.Count; j++)
            {
                bool moveOn;
                do
                {
                    moveOn = false;
                    int[] indexResult = indexResultList.ElementAt(j);

                    for (int i = fullConfigItems.Length; i < allConfigItems.Length; i++)
                    {
                        indexResult[i] = allConfigItems.ElementAt(i).GetIndex();
                    }

                    bool filtered = false;

                    foreach (List<RestrictionItem> rule in this.restrictionRuleList)
                    {
                        filtered = true;

                        foreach (RestrictionItem ri in rule)
                        {
                            if (indexResult[ri.indexInConfigItemList] != ri.indexInConfigValues)
                            {
                                filtered = false;
                                break;
                            }
                        }

                        if (filtered) break;
                    }

                    if (!filtered)
                    {
                        moveOn = true;

                        for (int i = fullConfigItems.Length; i < allConfigItems.Length; i++)
                        {
                            valueResultList.ElementAt(j)[i] = allConfigItems[i].values[indexResult[i]];
                            allConfigItems[i].RemoveUsed();
                        }

                    }
                } while (!moveOn);

            }
        }

        private List<int[]> GetMatrixWithFilters(
            ConfigItem[] items,
            List<string[]> valueMatrix,
            bool applyFilters)
        {
            List<int[]> indexMatrix = new List<int[]>();
            int itemsCount = items.Length;
            bool needValueMatrix = (valueMatrix == null) ? false : true;

            bool finished;
            int[] indexRow = new int[itemsCount]; ;
            string[] valueRow = needValueMatrix ? new string[itemsCount] : null;

            do
            {
                if (!(applyFilters && IsFiltered(indexRow)))
                {
                    if (needValueMatrix)
                    {
                        for (int i = 0; i < itemsCount; i++)
                        {
                            valueRow[i] = allConfigItems[i].values[indexRow[i]];
                        }
                        valueMatrix.Add(valueRow.ToArray<string>());
                    }

                    indexMatrix.Add(indexRow.ToArray<int>());
                }

                finished = PerformStep(indexRow);
            } while (!finished);

            return indexMatrix;
        }

        private bool IsFiltered(int[] indexRow)
        {
            bool filtered = false;

            foreach (List<RestrictionItem> rule in this.restrictionRuleList)
            {
                filtered = true;

                foreach (RestrictionItem ri in rule)
                {
                    if (indexRow[ri.indexInConfigItemList] != ri.indexInConfigValues ||
                        //ri.indexInConfigItemList > fullConfigItems.Length - 1)
                        ri.indexInConfigItemList > allConfigItems.Length - 1)
                    {
                        filtered = false;
                        break;
                    }
                }

                if (filtered) break;
            }
            return filtered;
        }

        private bool PerformStep(int[] numbers)
        {
            return PerformStep(numbers, numbers.Length - 1);
        }

        private bool PerformStep(int[] numbers, int index)
        {
            if (index < 0 || index > numbers.Length - 1) return true;

            numbers[index]++;

            for (int i = index; i > -1; i--)
            {
                if (numbers[i] == allConfigItems[i].values.Length)
                {
                    numbers[i] = 0;

                    if (i > 0)
                    { numbers[i - 1]++; }
                    else
                    { return true; }
                }
            }
            return false;
        }

        private bool AlreadyExists<T>(List<T> list, T obj)
        {
            foreach (T item in list)
            {
                if (item.Equals(obj))
                { return true; }
            }
            return false;
        }

    }

}
