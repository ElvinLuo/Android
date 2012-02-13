// -----------------------------------------------------------------------
// <copyright file="ConfigItem.cs" company="Expedia, Inc.">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConfigItem
    {
        private int priviousPickedIndex;
        private int priviousPickedItem;
        private bool flag;
        public int count;
        public int priority;
        public string item;
        public string[] names, values;
        public List<List<int>> indexInAllAvailMatrix;
        public bool random;
        public int[] coverages;
        public bool[] canExceed;
        public bool[] flags;
        public List<int> indexes;
        public List<int> remainingIndexes;

        public ConfigItem(
            string item,
            string names,
            string values,
            int priority,
            string random,
            string coverages)
        {
            char splitCharacter = '/';
            this.flag = false;
            this.count = 0;
            this.item = item;
            string[] namesBeforeSorting = names.Split(splitCharacter);
            string[] valuesBeforeSorting = values.Split(splitCharacter);
            string[] coveragesBeforeSorting = coverages.Split(splitCharacter);
            int[] order = new int[coveragesBeforeSorting.Length];
            this.priority = priority;
            this.random = random.ToLower().Equals("true") ? true : false;
            this.coverages = new int[coveragesBeforeSorting.Length];

            for (int i = 0; i < coveragesBeforeSorting.Length; i++)
            {
                order[i] = i;
                this.coverages[i] = Convert.ToInt32(coveragesBeforeSorting[i]);

                for (int j = 0; j < i; j++)
                {
                    if (this.coverages[i] > this.coverages[j])
                    {
                        int cvrg = this.coverages[i];

                        for (int k = i; k > j; k--)
                        {
                            this.coverages[k] = this.coverages[k - 1];
                            order[k] = order[k - 1];
                        }

                        this.coverages[j] = cvrg;
                        order[j] = i;
                    }
                }
            }

            this.names = new string[namesBeforeSorting.Length];
            this.values = new string[valuesBeforeSorting.Length];

            for (int i = 0; i < order.Length; i++)
            {
                this.names[i] = namesBeforeSorting[order[i]];
                this.values[i] = valuesBeforeSorting[order[i]];
            }

            this.canExceed = new bool[this.values.Length];
            indexInAllAvailMatrix = new List<List<int>>();

            for (int i = 0; i < this.values.Length; i++)
            {
                List<int> availList = new List<int>();
                indexInAllAvailMatrix.Add(availList);
            }

            indexes = new List<int>();
            remainingIndexes = new List<int>();
            this.flags = new bool[coveragesBeforeSorting.Length];

            for (int i = 0; i < coveragesBeforeSorting.Length; i++)
            {
                for (int j = 0; j < this.coverages[i]; j++)
                {
                    indexes.Add(i);
                    remainingIndexes.Add(i);
                }

                count += this.coverages[i];
                this.flags[i] = false;
            }
        }

        public int GetIndex()
        {
            CreateNewRemainingIndexes();
            priviousPickedItem = new Random().Next(remainingIndexes.Count);
            priviousPickedIndex = remainingIndexes.ElementAt(priviousPickedItem);
            return priviousPickedIndex;
        }

        public bool PickOneInRemainingIndexes(int position)
        {
            CreateNewRemainingIndexes();
            priviousPickedItem = -1;

            for (int i = 0; i < remainingIndexes.Count; i++)
            {
                if (remainingIndexes[i] == position)
                {
                    priviousPickedItem = i;
                    break;
                }
            }

            if (priviousPickedItem > -1)
            {
                priviousPickedIndex = position;
                return true;
            }

            return false;
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

        public void Adjust()
        {
            for (int i = 0; i < indexInAllAvailMatrix.Count; i++)
            {
                if (indexInAllAvailMatrix.ElementAt(i).Count == 0)
                {
                    coverages[i] = 0;
                }
            }

            count = 0;

            indexes.Clear();
            remainingIndexes.Clear();

            for (int i = 0; i < coverages.Length; i++)
            {
                if (indexInAllAvailMatrix.ElementAt(i).Count < coverages[i])
                {
                    canExceed[i] = true;
                }

                for (int j = 0; j < coverages[i]; j++)
                {
                    indexes.Add(i);
                    remainingIndexes.Add(i);
                }

                count += coverages[i];
            }
        }

        public void CreateNewRemainingIndexes()
        {
            if (remainingIndexes == null || remainingIndexes.Count == 0)
            {
                foreach (int element in indexes)
                {
                    remainingIndexes.Add(element);
                }
            }
        }

    }
}
