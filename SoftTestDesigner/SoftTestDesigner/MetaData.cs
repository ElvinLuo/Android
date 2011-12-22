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

            int idx;
            int random = new Random().Next(remainingIndexes.Count);
            idx = remainingIndexes.ElementAt(random);
            flags[idx] = true;
            remainingIndexes.RemoveAt(random);

            if (remainingIndexes.Count == 0)
            { flag = true; }

            return idx;
        }

        public bool Checked()
        {
            return flag;
        }

    }

    public class RestrictionItem
    {
        public int indexInConfigItemList;
        public int indexInConfigValues;

        public RestrictionItem(int i, int j)
        {
            indexInConfigItemList = i;
            indexInConfigValues = j;
        }
    }

    public class SoftTestConfiguration
    {
        private int configItemsCount;
        private ConfigItem[] configItems;
        private ConfigItem[] fullConfigItems;
        private ConfigItem[] randomConfigItems;
        private List<RestrictionItem> restrictionItems;
        private List<int[]> restrictionRules;
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
                configItems = new ConfigItem[configItemsCount];
                fullConfigItems = new ConfigItem[fullConfigItemList.Count];
                randomConfigItems = new ConfigItem[randomConfigItemList.Count];

                for (int j = 0; j < fullConfigItemList.Count; j++)
                {
                    configItems[j] = fullConfigItemList.ElementAt(j);
                    fullConfigItems[j] = fullConfigItemList.ElementAt(j);
                }

                for (int j = 0; j < randomConfigItemList.Count; j++)
                {
                    configItems[fullConfigItemList.Count + j] = randomConfigItemList.ElementAt(j);
                    randomConfigItems[j] = randomConfigItemList.ElementAt(j);
                }
            }

            //string restrictionString;
            //string[] restrictionItems;
            //this.restrictionItems = new List<RestrictionItem>();
            //foreach (DataGridViewRow rrow in restrictionRows)
            //{
            //    restrictionString = rrow.Cells[0].Value.ToString();
            //    restrictionItems = restrictionString.Split(" ".ToCharArray());
            //    foreach (string ri in restrictionItems)
            //    {
            //        string[] nameValue;
            //        int indexInConfigItemList, indexInConfigValues;

            //        if (ri.Contains("="))
            //        {
            //            nameValue = ri.Split("=".ToCharArray());
            //            for (int i = 0; i < configItemsCount; i++)
            //            {
            //                if (nameValue[0].Equals(fullConfigItems[i].item))
            //                {
            //                    indexInConfigItemList = i;
            //                    for (int j = 0; j < fullConfigItems[i].values.Length; j++)
            //                    {
            //                        indexInConfigValues = j;
            //                        RestrictionItem restrictionItem = new RestrictionItem(indexInConfigItemList, indexInConfigValues);
            //                        if (!this.restrictionItems.Contains(restrictionItem))
            //                        { this.restrictionItems.Add(restrictionItem); }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public void GetResult()
        {
            indexResultList = new List<int[]>();
            valueResultList = new List<string[]>();

            bool finished;
            int[] indexRow = new int[configItemsCount]; ;
            string[] valueRow;

            do
            {
                //indexRow = new int[configItemsCount];
                valueRow = new string[configItemsCount];

                for (int i = 0; i < configItemsCount; i++)
                {
                    valueRow[i] = configItems[i].values[indexRow[i]];
                }

                indexResultList.Add(indexRow.ToArray<int>());
                valueResultList.Add(valueRow);
                finished = PerformStep(indexRow, fullConfigItems.Length - 1);
            } while (!finished);
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

    }

}
