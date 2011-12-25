// -----------------------------------------------------------------------
// <copyright file="MetaData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftCaseGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConfigItem
    {
        private int priviousPickedIndex;
        private int priviousPickedItem;
        private bool flag;
        public int count;
        public string item;
        public string[] names, values;
        public bool random;
        public int[] coverages;
        public bool[] flags;
        public List<int> indexes;
        public List<int> remainingIndexes;

        public ConfigItem(string item, string names, string values, string random, string coverages)
        {
            this.flag = false;
            this.count = 0;
            this.item = item;
            this.names = names.Split(new char[] { '/' });
            this.values = values.Split(new char[] { '/' });
            string[] temp = coverages.Split(new char[] { '/' });
            this.random = random.ToLower().Equals("true") ? true : false;
            this.coverages = new int[temp.Length];

            indexes = new List<int>();
            remainingIndexes = new List<int>();
            for (int i = 0; i < temp.Length; i++)
            {
                this.coverages[i] = Convert.ToInt32(temp[i]);

                for (int j = 0; j < this.coverages[i]; j++)
                {
                    indexes.Add(i);
                    remainingIndexes.Add(i);
                }

                count += this.coverages[i];
            }

            this.flags = new bool[temp.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                this.flags[i] = false;
            }
        }

        public int GetIndex()
        {
            if (remainingIndexes == null || remainingIndexes.Count == 0)
            {
                foreach (int element in indexes)
                {
                    remainingIndexes.Add(element);
                }
            }

            priviousPickedItem = new Random().Next(remainingIndexes.Count);
            priviousPickedIndex = remainingIndexes.ElementAt(priviousPickedItem);

            return priviousPickedIndex;
        }

        public void RemoveUsed()
        {
            flags[priviousPickedIndex] = true;
            remainingIndexes.RemoveAt(priviousPickedItem);

            if (remainingIndexes.Count == 0)
            { flag = true; }
        }

        public bool Checked()
        {
            return flag;
        }

    }

    public class RestrictionItem : IComparable
    {
        public int indexInConfigItemList;
        public int indexInConfigValues;

        public RestrictionItem(int i, int j)
        {
            indexInConfigItemList = i;
            indexInConfigValues = j;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            RestrictionItem that = obj as RestrictionItem;
            if (that == null)
                return false;

            return (this.indexInConfigItemList == that.indexInConfigItemList &&
                this.indexInConfigValues == that.indexInConfigValues);
        }

        public int CompareTo(object obj)
        {
            RestrictionItem that = obj as RestrictionItem;

            int hBit =
                (this.indexInConfigItemList == that.indexInConfigItemList) ? 0 :
                ((this.indexInConfigItemList < that.indexInConfigItemList) ? -1 : 1) * 2;
            int lBit =
                (this.indexInConfigValues == that.indexInConfigValues) ? 0 :
                (this.indexInConfigValues < that.indexInConfigValues) ? -1 : 1;

            return hBit + lBit;
        }
    }

    public class SoftTestConfiguration
    {
        private int configItemsCount;
        private ConfigItem[] configItems;
        private ConfigItem[] fullConfigItems;
        private ConfigItem[] randomConfigItems;
        private List<RestrictionItem> restrictionItems;
        private List<List<RestrictionItem>> restrictionRuleList;

        public List<string> itemNames;
        public List<int[]> indexResultList;
        public List<string[]> valueResultList;

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
                configItems = new ConfigItem[configItemsCount];
                fullConfigItems = new ConfigItem[fullConfigItemList.Count];
                randomConfigItems = new ConfigItem[randomConfigItemList.Count];

                for (int j = 0; j < fullConfigItemList.Count; j++)
                {
                    configItems[j] = fullConfigItemList.ElementAt(j);
                    itemNames.Add(fullConfigItemList.ElementAt(j).item);
                    fullConfigItems[j] = fullConfigItemList.ElementAt(j);
                }

                for (int j = 0; j < randomConfigItemList.Count; j++)
                {
                    configItems[fullConfigItemList.Count + j] = randomConfigItemList.ElementAt(j);
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
                            if (nameValue[0].ToLower().Equals(configItems[i].item.ToLower()))
                            {
                                indexInConfigItemList = i;
                                for (int j = 0; j < configItems[i].values.Length - 1; j++)
                                {
                                    if (!nameValue[1].ToLower().Equals(configItems[i].values[j].ToLower()))
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
            indexResultList = new List<int[]>();
            valueResultList = new List<string[]>();

            bool finished;
            int[] indexRow = new int[configItemsCount]; ;
            string[] valueRow = new string[configItemsCount];

            do
            {
                bool filtered = false;

                foreach (List<RestrictionItem> rule in this.restrictionRuleList)
                {
                    filtered = true;

                    foreach (RestrictionItem ri in rule)
                    {
                        if (indexRow[ri.indexInConfigItemList] != ri.indexInConfigValues ||
                            //ri.indexInConfigItemList > fullConfigItems.Length - 1)
                            ri.indexInConfigItemList > configItems.Length - 1)
                        {
                            filtered = false;
                            break;
                        }
                    }

                    if (filtered) break;
                }

                if (!filtered)
                {
                    for (int i = 0; i < configItemsCount; i++)
                    {
                        valueRow[i] = configItems[i].values[indexRow[i]];
                    }

                    indexResultList.Add(indexRow.ToArray<int>());
                    valueResultList.Add(valueRow.ToArray<string>());
                }

                //finished = PerformStep(indexRow, fullConfigItems.Length - 1);
                finished = PerformStep(indexRow, configItems.Length - 1);
            } while (!finished);

            //for (int j = 0; j < indexResultList.Count; j++)
            //{
            //    bool moveOn;
            //    do
            //    {
            //        moveOn = false;
            //        int[] indexResult = indexResultList.ElementAt(j);

            //        for (int i = fullConfigItems.Length; i < configItems.Length; i++)
            //        {
            //            indexResult[i] = configItems.ElementAt(i).GetIndex();
            //        }

            //        bool filtered = false;

            //        foreach (List<RestrictionItem> rule in this.restrictionRuleList)
            //        {
            //            filtered = true;

            //            foreach (RestrictionItem ri in rule)
            //            {
            //                if (indexResult[ri.indexInConfigItemList] != ri.indexInConfigValues)
            //                {
            //                    filtered = false;
            //                    break;
            //                }
            //            }

            //            if (filtered) break;
            //        }

            //        if (!filtered)
            //        {
            //            moveOn = true;

            //            for (int i = fullConfigItems.Length; i < configItems.Length; i++)
            //            {
            //                valueResultList.ElementAt(j)[i] = configItems[i].values[indexResult[i]];
            //                configItems[i].RemoveUsed();
            //            }

            //        }
            //    } while (!moveOn);

            //}
        }

        private bool PerformStep(int[] numbers, int index)
        {
            if (index < 0 || index > numbers.Length - 1) return true;

            numbers[index]++;

            for (int i = index; i > -1; i--)
            {
                if (numbers[i] == configItems[i].values.Length)
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
