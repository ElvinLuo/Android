// -----------------------------------------------------------------------
// <copyright file="ConfigItem.cs" company="Expedia, Inc.">
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
    public class ConfigItem
    {
        private int priviousPickedIndex;
        private int priviousPickedItem;
        private bool flag;
        public int count;
        public string item;
        public string[] names, values;
        public List<List<int>> indexInAllAvailMatrix;
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
            indexInAllAvailMatrix = new List<List<int>>();
            string[] temp = coverages.Split(new char[] { '/' });
            this.random = random.ToLower().Equals("true") ? true : false;
            this.coverages = new int[temp.Length];

            for (int i = 0; i < values.Length; i++)
            {
                List<int> availList = new List<int>();
                indexInAllAvailMatrix.Add(availList);
            }

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
}
