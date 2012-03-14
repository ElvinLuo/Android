// -----------------------------------------------------------------------
// <copyright file="MetaData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoftTestConfiguration
    {
        #region
        private int configItemsCount;
        public ConfigItem[] allConfigItems;
        private ConfigItem[] fullConfigItems;
        private ConfigItem[] randomConfigItems;
        private List<RestrictionItem> restrictionItems;
        public List<List<RestrictionItem>> restrictionRuleList;
        public List<string> restrictionTypeList;
        public List<bool> containResultList;

        public List<string> itemNames;
        public List<int[]> indexResultList;
        public List<string[]> valueResultList;
        public List<string> softTestNameList;
        #endregion

        public SoftTestConfiguration(
            DataGridViewRowCollection configItemRows,
            DataGridViewRowCollection restrictionRows)
        {
            DataGridViewRow row;
            List<ConfigItem> allConfigItemList = new List<ConfigItem>();

            ConfigItem ci;
            string item, names, values, random, coverages;
            int fullConfigItemCount = 0, randomConfigItemCount = 0;

            for (int i = 0; i < configItemRows.Count - 1; i++)
            {
                row = configItemRows[i];
                item = row.Cells[1].Value.ToString();
                names = row.Cells[2].Value.ToString();
                values = row.Cells[3].Value.ToString();
                random = row.Cells[4].Value.ToString();
                coverages = row.Cells[5].Value.ToString();

                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    ci = new ConfigItem(item, names, values, allConfigItemList.Count, random, coverages);

                    if (Convert.ToBoolean(row.Cells[4].Value))
                    {
                        randomConfigItemCount++;
                    }
                    else
                    {
                        fullConfigItemCount++;
                    }

                    allConfigItemList.Add(ci);
                }

                itemNames = new List<string>();
                configItemsCount = allConfigItemList.Count;
                allConfigItems = new ConfigItem[configItemsCount];
                fullConfigItems = new ConfigItem[fullConfigItemCount];
                randomConfigItems = new ConfigItem[randomConfigItemCount];

                for (int j = 0, fullIndex = 0, randomIndex = 0; j < allConfigItemList.Count; j++)
                {
                    ci = allConfigItemList.ElementAt(j);
                    allConfigItems[j] = ci;
                    itemNames.Add(ci.item);
                    if (ci.random)
                    {
                        randomConfigItems[randomIndex] = ci;
                        randomIndex++;
                    }
                    else
                    {
                        fullConfigItems[fullIndex] = ci;
                        fullIndex++;
                    }
                }
            }
        }

        public void LoadRestrictionFromDataGridView(DataGridViewRowCollection restrictionRows)
        {
            string restrictionString;
            string[] restrictionItems;
            List<RestrictionItem> rule;
            this.restrictionItems = new List<RestrictionItem>();
            this.restrictionRuleList = new List<List<RestrictionItem>>();
            this.restrictionTypeList = new List<string>();
            this.containResultList = new List<bool>();

            for (int rowIndex = 0; rowIndex < restrictionRows.Count - 1; rowIndex++)
            {
                DataGridViewRow rrow = restrictionRows[rowIndex];
                if (!Convert.ToBoolean(rrow.Cells[0].Value)) continue;

                if (rrow.Cells[2].Value == null) continue;

                restrictionTypeList.Add(rrow.Cells[1].Value.ToString());
                containResultList.Add(false);

                int ruleItemCount = 0;
                rule = new List<RestrictionItem>();
                restrictionString = rrow.Cells[2].Value.ToString();
                restrictionItems = restrictionString.Split(' ');
                foreach (string ri in restrictionItems)
                {
                    string[] nameValue;
                    int indexInConfigItemList = 0, indexInConfigValues = 0;

                    if (ri.Contains("="))
                    {
                        ruleItemCount++;
                        nameValue = ri.Split('=');
                        bool validItem = false;
                        for (int i = 0; i < configItemsCount; i++)
                        {
                            if (nameValue[0].ToLower().Equals(allConfigItems[i].item.ToLower()))
                            {
                                indexInConfigItemList = allConfigItems[i].priority;
                                for (int j = 0; j < allConfigItems[i].values.Length; j++)
                                {
                                    if (nameValue[1].ToLower().Equals(allConfigItems[i].values[j].ToLower()))
                                    {
                                        indexInConfigValues = j;
                                        validItem = true;
                                    }
                                }
                            }
                        }

                        if (!validItem)
                        {
                            indexInConfigItemList = configItemsCount;
                        }

                        RestrictionItem restrictionItem = new RestrictionItem(indexInConfigItemList, indexInConfigValues);

                        if (!AlreadyExists<RestrictionItem>(this.restrictionItems, restrictionItem))
                        { this.restrictionItems.Add(restrictionItem); }

                        if (!AlreadyExists<RestrictionItem>(rule, restrictionItem))
                        { rule.Add(restrictionItem); }
                    }
                }
                //if (rule.Count == ruleItemCount)
                {
                    rule.Sort();
                    this.restrictionRuleList.Add(rule);
                }
            }
            this.restrictionItems.Sort();
        }

        public void GetResultWitoutRestrictions()
        {
            indexResultList = new List<int[]>();
            valueResultList = new List<string[]>();
            softTestNameList = new List<string>();

            int round = 1;
            int fullConfigItemsCount = fullConfigItems.Length;
            int allConfigItemsCount = allConfigItems.Length;
            int[] fullIndexRow = new int[fullConfigItemsCount];
            int[] indexRow = new int[allConfigItemsCount];
            string[] valueRow = new string[allConfigItemsCount];

            do
            {
                for (int i = 0; i < fullConfigItemsCount; i++)
                {
                    indexRow[fullConfigItems.ElementAt(i).priority] = fullIndexRow[i];
                }

                for (int i = 0; i < randomConfigItems.Length; i++)
                {
                    ConfigItem randomConfigItem = randomConfigItems.ElementAt(i);
                    indexRow[randomConfigItem.priority] = randomConfigItem.GetIndex();
                    randomConfigItem.RemoveUsed();
                }

                int position = indexResultList.Count;
                for (int i = 0; i < indexResultList.Count; i++)
                {
                    if (CompareArray(indexRow, indexResultList.ElementAt(i)) == -1)
                    {
                        position = i;
                        break;
                    }
                }

                CopyIndexRowToValueRow(indexRow, valueRow);
                indexResultList.Insert(position, indexRow.ToArray());
                valueResultList.Insert(position, valueRow.ToArray());
                softTestNameList.Insert(position, CopyIndexRowToNameRow(indexRow));

                if (PerformStepOnFullConfigItems(fullIndexRow)) round++;
            } while (round == 1 || NeedToContinue());
        }

        public void GetResult()
        {
            indexResultList = new List<int[]>();
            valueResultList = new List<string[]>();
            softTestNameList = new List<string>();
            List<int[]> allAvailMatrix = GetMatrixWithFilters(allConfigItems, valueResultList, true);

            List<int[]> fullAvailMatrix = new List<int[]>();
            List<List<int>> fullAllAvailMatrixMapping = new List<List<int>>();
            List<int[]> randomAvailMatrix = new List<int[]>();
            List<List<int>> randomAllAvailMatrixMapping = new List<List<int>>();

            GetMappingMatrix(
                allAvailMatrix,
                fullAvailMatrix,
                fullAllAvailMatrixMapping,
                randomAvailMatrix,
                randomAllAvailMatrixMapping);

            string[] valueRow = new string[allConfigItems.Length];

            if (randomConfigItems.Length > 0)
            {
                int round = 1;
                bool needed, needToContinue = false;
                valueResultList = new List<string[]>();

                do
                {
                    foreach (int[] row in allAvailMatrix)
                    {
                        needed = true;

                        for (int i = fullConfigItems.Length; i < allConfigItems.Length; i++)
                        {
                            ConfigItem ci = allConfigItems.ElementAt(i);
                            if (!ci.PickOneInRemainingIndexes(row[i]))
                            {
                                needed = false;
                                break;
                            }
                        }

                        if (needed)
                        {
                            foreach (ConfigItem ci in randomConfigItems)
                            {
                                ci.RemoveUsed();
                                needToContinue = NeedToContinue();
                            }
                            CopyIndexRowToValueRow(row, valueRow);
                            indexResultList.Add(row.ToArray());
                            valueResultList.Add(valueRow.ToArray());
                            softTestNameList.Add(CopyIndexRowToNameRow(row));

                            if (round > 1 && !needToContinue) break;
                        }
                    }

                    round++;
                } while (needToContinue);
            }
            else
            {
                foreach (int[] row in allAvailMatrix)
                {
                    CopyIndexRowToValueRow(row, valueRow);
                    indexResultList.Add(row.ToArray());
                    valueResultList.Add(valueRow.ToArray());
                    softTestNameList.Add(CopyIndexRowToNameRow(row));
                }
            }
        }

        private void GetMappingMatrix(
            List<int[]> allAvailMatrix,
            List<int[]> fullAvailMatrix,
            List<List<int>> fullAllAvailMatrixMapping,
            List<int[]> randomAvailMatrix,
            List<List<int>> randomAllAvailMatrixMapping)
        {
            int foundIndex;
            int allConfigItemCount = allConfigItems.Length;
            int fullConfigItemCount = fullConfigItems.Length;
            int[] fullAvailMatrixRow;
            int[] randomAvailMatrixRow;
            List<int> temp;

            for (int i = 0; i < allAvailMatrix.Count; i++)
            {
                int[] allAvailMatrixRow = allAvailMatrix.ElementAt(i);

                for (int j = fullConfigItemCount; j < allConfigItemCount; j++)
                {
                    allConfigItems.ElementAt(j).indexInAllAvailMatrix.ElementAt(allAvailMatrixRow[j]).Add(i);
                }

                fullAvailMatrixRow = new int[fullConfigItemCount];
                CopyArray(fullAvailMatrixRow, allAvailMatrixRow, 0);
                randomAvailMatrixRow = new int[allConfigItemCount - fullConfigItemCount];
                CopyArray(randomAvailMatrixRow, allAvailMatrixRow, fullConfigItemCount);

                if (AlreadyExists<int[]>(fullAvailMatrix, fullAvailMatrixRow, out foundIndex))
                {
                    fullAllAvailMatrixMapping.ElementAt(foundIndex).Add(i);
                }
                else
                {
                    fullAvailMatrix.Add(fullAvailMatrixRow);
                    temp = new List<int>();
                    temp.Add(i);
                    fullAllAvailMatrixMapping.Add(temp);
                }

                if (AlreadyExists<int[]>(randomAvailMatrix, randomAvailMatrixRow, out foundIndex))
                {
                    randomAllAvailMatrixMapping.ElementAt(foundIndex).Add(i);
                }
                else
                {
                    randomAvailMatrix.Add(randomAvailMatrixRow);
                    temp = new List<int>();
                    temp.Add(i);
                    randomAllAvailMatrixMapping.Add(temp);
                }
            }

            foreach (ConfigItem ci in randomConfigItems)
            {
                ci.Adjust();
            }

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
                        //for (int i = 0; i < itemsCount; i++)
                        //{
                        //    valueRow[i] = allConfigItems[i].values[indexRow[i]];
                        //}
                        CopyIndexRowToValueRow(indexRow, valueRow);
                        valueMatrix.Add(valueRow.ToArray<string>());
                    }

                    indexMatrix.Add(indexRow.ToArray<int>());
                }

                finished = PerformStep(indexRow);
            } while (!finished);

            return indexMatrix;
        }

        private void CopyIndexRowToValueRow(int[] indexRow, string[] valueRow)
        {
            for (int i = 0; i < indexRow.Length; i++)
            {
                valueRow[i] = allConfigItems[i].values[indexRow[i]];
            }
        }

        private string CopyIndexRowToNameRow(int[] indexRow)
        {
            string name = null;

            for (int i = 0; i < indexRow.Length; i++)
            {
                name += allConfigItems[i].names[indexRow[i]];
            }

            return name;
        }

        public bool IsFiltered(int[] indexRow)
        {
            bool filtered = false;

            for (int i = 0; i < this.restrictionRuleList.Count; i++)
            {
                if (restrictionTypeList.ElementAt(i).Equals(GlobalConsts.needToFilter))
                {
                    List<RestrictionItem> rule = this.restrictionRuleList.ElementAt(i);
                    filtered = true;

                    foreach (RestrictionItem ri in rule)
                    {
                        if (ri.indexInConfigItemList > allConfigItems.Length - 1 ||
                            indexRow[ri.indexInConfigItemList] != ri.indexInConfigValues)
                        {
                            filtered = false;
                            break;
                        }
                    }

                    if (filtered) break;
                }
            }
            return filtered;
        }

        public bool IsMatching(int index, int[] indexRow)
        {
            if (restrictionTypeList.ElementAt(index).Equals(GlobalConsts.needToContain))
            {
                List<RestrictionItem> rule = this.restrictionRuleList.ElementAt(index);

                foreach (RestrictionItem ri in rule)
                {
                    if (ri.indexInConfigItemList > allConfigItems.Length - 1 ||
                        indexRow[ri.indexInConfigItemList] != ri.indexInConfigValues)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return true;
            }
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

        private bool PerformStepOnFullConfigItems(int[] numbers)
        {
            int index = numbers.Length - 1;
            if (index < 0 || index > numbers.Length - 1) return true;
            numbers[index]++;

            for (int i = index; i > -1; i--)
            {
                if (numbers[i] == fullConfigItems[i].values.Length)
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
            if (obj.GetType() == typeof(int[]))
            {
                foreach (T that in list)
                {
                    if (IsSameArray(obj as int[], that as int[]))
                    { return true; }
                }
            }
            else
            {
                foreach (T item in list)
                {
                    if (item.Equals(obj))
                    { return true; }
                }
            }
            return false;
        }

        private bool AlreadyExists<T>(List<T> list, T obj, out int index)
        {
            if (obj.GetType() == typeof(int[]))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T that = list.ElementAt(i);
                    if (IsSameArray(obj as int[], that as int[]))
                    {
                        index = i;
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T item = list.ElementAt(i);
                    if (item.Equals(obj))
                    {
                        index = i;
                        return true;
                    }
                }
            }

            index = 0;
            return false;
        }

        private bool CopyArray(int[] target, int[] source, int startIndex)
        {
            if (target.Length + startIndex > source.Length)
            { return false; }
            else
            {
                for (int i = 0; i < target.Length; i++)
                {
                    target[i] = source[startIndex + i];
                }
                return true;
            }
        }

        private bool IsSameArray(int[] left, int[] right)
        {
            if (left.Length != right.Length) return false;

            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                    return false;
            }

            return true;
        }

        private bool NeedToContinue()
        {
            foreach (ConfigItem item in randomConfigItems)
            {
                if (!item.Checked())
                    return true;
            }

            return false;
        }

        public string GetExpression(DataGridViewRowCollection restrictionRows)
        {
            StringBuilder expression = new StringBuilder();

            for (int rowIndex = 0; rowIndex < restrictionRows.Count - 1; rowIndex++)
            {
                DataGridViewRow row = restrictionRows[rowIndex];

                if (!(bool)row.Cells[0].Value) continue;
                if (row.Cells[1].Value == null) continue;

                string restrictionString = row.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(restrictionString)) continue;

                expression.AppendLine(string.Format("NOT({0}) AND", restrictionString));
            }

            expression.Remove(expression.Length - 6, 6);
            return expression.ToString();
        }

        private int CompareArray(int[] left, int[] right)
        {
            if (left.Length == 0 || right.Length == 0 || left.Length != right.Length)
            {
                throw new Exception("Invalid arrays");
            }

            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] < right[i])
                {
                    return -1;
                }
                else if (left[i] > right[i])
                {
                    return 1;
                }
                else if (i == left.Length - 1)
                {
                    return 0;
                }
            }

            return 0;
        }

    }

}
