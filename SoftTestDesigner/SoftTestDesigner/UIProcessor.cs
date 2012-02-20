// -----------------------------------------------------------------------
// <copyright file="UIProcessor.cs" company="Expedia, Inc.">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftTestDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using SoftTestPKG;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UIProcessor
    {
        private static UIProcessor instance;

        public static UIProcessor CreateInstance()
        {
            if (instance == null)
            {
                instance = new UIProcessor();
            }
            return instance;
        }

        public void AddColumns(SoftTestConfiguration sc, DataGridView dgvResult)
        {
            dgvResult.Columns.Add("Soft Test Name", "Soft Test Name");
            foreach (string columnName in sc.itemNames)
            {
                dgvResult.Columns.Add(columnName, columnName);
            }
        }

        public void AddRows(SoftTestConfiguration sc, DataGridView dgvResult)
        {
            string[] headerRow = new string[sc.itemNames.Count + 1];
            headerRow[0] = "Soft Test Name";

            for (int i = 0; i < sc.itemNames.Count; i++)
            { headerRow[i + 1] = sc.itemNames[i]; }

            dgvResult.Rows.Add(headerRow);

            for (int i = 0; i < sc.softTestNameList.Count; i++)
            {
                List<string> finalRow = new List<string>();
                finalRow.Add(sc.softTestNameList.ElementAt(i));

                foreach (string value in sc.valueResultList.ElementAt(i))
                {
                    finalRow.Add(value);
                }

                dgvResult.Rows.Add(finalRow.ToArray());
            }
        }

        public void ApplyRestrictions(
            SoftTestConfiguration sc,
            DataGridView dgvRestriction,
            DataGridView dgvResult,
            bool needReload)
        {
            sc.LoadRestrictionFromDataGridView(dgvRestriction.Rows);

            for (int i = sc.indexResultList.Count - 1; i > -1; i--)
            {
                if (sc.IsFiltered(sc.indexResultList.ElementAt(i)))
                {
                    sc.indexResultList.RemoveAt(i);
                    sc.valueResultList.RemoveAt(i);
                    sc.softTestNameList.RemoveAt(i);
                }
            }

            if (needReload)
            {
                ReloadResultFromSoftTestConfiguration(sc, dgvResult);
            }
        }

        public void CopyClipboard(DataGridView dgvResult)
        {
            DataObject d = dgvResult.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        public void CopyDataGridViewRow(DataGridViewRow source, DataGridViewRow target)
        {
            if (target.Cells.Count == source.Cells.Count)
            {
                for (int i = 0; i < source.Cells.Count; i++)
                {
                    target.Cells[i].Value = source.Cells[i].Value;
                }
            }
        }

        public void EnableCell(DataGridViewCell dc, bool enabled)
        {
            dc.ReadOnly = !enabled;
            if (enabled)
            {
                dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
                dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
            }
            else
            {
                dc.Style.BackColor = Color.DarkGray;
                dc.Style.ForeColor = Color.LightGray;
            }
        }

        public void GenerateCombination(
            out SoftTestConfiguration sc,
            DataGridView dgvConfigItem,
            DataGridView dgvRestriction,
            DataGridView dgvResult,
            bool needReload)
        {
            sc = new SoftTestConfiguration(dgvConfigItem.Rows, dgvRestriction.Rows);
            sc.GetResultWitoutRestrictions();
            if (needReload)
            {
                ReloadResultFromSoftTestConfiguration(sc, dgvResult);
            }
        }

        public bool GetResultStatistics(
            out Dictionary<string, Dictionary<string, int>> itemValueCountDictionary,
            out string output,
            DataGridView dgvRestriction,
            DataGridView dgvResult,
            SoftTestConfiguration sc)
        {
            output = null;
            bool pass = true;
            string itemName, cellValue, unContainedLines = null;
            int columnCount = dgvResult.Rows[0].Cells.Count;
            itemValueCountDictionary = new Dictionary<string, Dictionary<string, int>>();

            if (sc.restrictionRuleList == null)
            {
                sc.LoadRestrictionFromDataGridView(dgvRestriction.Rows);
            }

            for (int columnIndex = 0; columnIndex < sc.allConfigItems.Length; columnIndex++)
            {
                itemName = sc.allConfigItems[columnIndex].item;

                if (!itemValueCountDictionary.ContainsKey(itemName))
                { itemValueCountDictionary.Add(itemName, new Dictionary<string, int>()); }

                foreach (string value in sc.allConfigItems[columnIndex].values)
                {
                    if (!itemValueCountDictionary[itemName].ContainsKey(value))
                    {
                        itemValueCountDictionary[itemName].Add(value, 0);
                    }
                }
            }

            for (int rowIndex = 1; rowIndex < dgvResult.Rows.Count - 1; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex < columnCount; columnIndex++)
                {
                    itemName = dgvResult.Rows[0].Cells[columnIndex].Value.ToString().Trim();
                    cellValue = dgvResult.Rows[rowIndex].Cells[columnIndex].Value.ToString().Trim();

                    if (!itemValueCountDictionary.ContainsKey(itemName))
                    { itemValueCountDictionary.Add(itemName, new Dictionary<string, int>()); }

                    if (itemValueCountDictionary[itemName].ContainsKey(cellValue))
                    {
                        itemValueCountDictionary[itemName][cellValue] += 1;
                    }
                }
            }

            for (int j = 0; j < sc.restrictionRuleList.Count; j++)
            {
                for (int i = 0; i < sc.indexResultList.Count; i++)
                {
                    if (sc.restrictionTypeList.ElementAt(j).Equals(GlobalConsts.needToContain))
                    {
                        if (sc.IsMatching(j, sc.indexResultList.ElementAt(i)))
                        {
                            sc.containResultList[j] = true;
                        }
                    }
                }
            }

            for (int j = 0; j < sc.containResultList.Count; j++)
            {
                if (sc.restrictionTypeList.ElementAt(j).Equals(GlobalConsts.needToContain) &&
                    !sc.containResultList.ElementAt(j))
                {
                    unContainedLines += j + 1 + ", ";
                    pass = false;
                }
            }

            if (pass)
            {
                foreach (KeyValuePair<string, Dictionary<string, int>> pair in itemValueCountDictionary)
                {
                    foreach (KeyValuePair<string, int> innerPair in itemValueCountDictionary[pair.Key])
                    {
                        if (innerPair.Value == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                output = "Not contained: " + unContainedLines;
            }

            return pass;
        }

        public string GetValue(string itemName, SoftTest softTest)
        {
            string result = string.Empty;

            foreach (Data data in softTest.testData)
            {
                if (data.dataName.ToLower().Equals(itemName.ToLower()))
                {
                    result = data.defaultValue;
                    break;
                }
            }

            return result;
        }

        public void LoadGridFromFile(DataGridView dgvConfigItem, OpenFileDialog openFileDialog)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader file = new StreamReader(openFileDialog.FileName))
                    {
                        string line;
                        string[] cells;
                        dgvConfigItem.Rows.Clear();

                        while ((line = file.ReadLine()) != null)
                        {
                            cells = line.Split('\t');
                            dgvConfigItem.Rows.Add(cells);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void MoveDown(DataGridView dgv)
        {
            if (dgv.SelectedCells.Count == 0) return;
            int selectedIndex = dgv.SelectedCells[0].RowIndex;

            if (selectedIndex == dgv.Rows.Count - 1 ||
                selectedIndex == dgv.Rows.Count - 2)
            { return; }

            int nextIndex = selectedIndex + 1;
            DataGridViewRow nextRow = (DataGridViewRow)dgv.Rows[nextIndex].Clone();
            CopyDataGridViewRow(dgv.Rows[nextIndex], nextRow);
            CopyDataGridViewRow(dgv.Rows[selectedIndex], dgv.Rows[nextIndex]);
            CopyDataGridViewRow(nextRow, dgv.Rows[selectedIndex]);
            dgv.Rows[selectedIndex].Selected = false;
            dgv.Rows[nextIndex].Selected = true;
        }

        public void MoveUp(DataGridView dgv)
        {
            if (dgv.SelectedCells.Count == 0) return;
            int selectedIndex = dgv.SelectedCells[0].RowIndex;
            if (selectedIndex == 0) return;
            int previousIndex = selectedIndex - 1;
            DataGridViewRow previousRow = (DataGridViewRow)dgv.Rows[previousIndex].Clone();
            CopyDataGridViewRow(dgv.Rows[previousIndex], previousRow);
            CopyDataGridViewRow(dgv.Rows[selectedIndex], dgv.Rows[previousIndex]);
            CopyDataGridViewRow(previousRow, dgv.Rows[selectedIndex]);
            dgv.Rows[selectedIndex].Selected = false;
            dgv.Rows[previousIndex].Selected = true;
        }

        public bool NeedToContinue(List<ConfigItem> items)
        {
            foreach (ConfigItem item in items)
            {
                if (!item.Checked())
                    return true;
            }

            return false;
        }

        public void PasteClipboard(DataGridView dgvResult)
        {
            try
            {
                string[] sCells;
                DataGridViewCell oCell;
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                int iRow = dgvResult.CurrentCell.RowIndex;
                int iCol = dgvResult.CurrentCell.ColumnIndex;

                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        if (iRow == dgvResult.RowCount - 1)
                        { dgvResult.Rows.Add(1); }

                        sCells = line.Split('\t');

                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i == dgvResult.ColumnCount)
                            { dgvResult.Columns.Add("NewColumn", "NewColumn"); }

                            oCell = dgvResult[iCol + i, iRow];

                            if (oCell.Value == null || oCell.Value.ToString() != sCells[i])
                            { oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType); }
                        }

                        iRow++;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("The data you pasted is in the wrong format for the cell");
            }
        }

        public void ReloadResultFromSoftTestConfiguration(SoftTestConfiguration sc, DataGridView dgvResult)
        {
            dgvResult.Columns.Clear();
            AddColumns(sc, dgvResult);
            AddRows(sc, dgvResult);
        }

        public void RemoveDuplicatedRows(SoftTestConfiguration sc, DataGridView dgvResult, bool needReload)
        {
            for (int i = sc.softTestNameList.Count - 1; i >= 0; i--)
            {
                if (sc.softTestNameList.IndexOf(sc.softTestNameList.ElementAt(i)) != i)
                {
                    sc.indexResultList.RemoveAt(i);
                    sc.valueResultList.RemoveAt(i);
                    sc.softTestNameList.RemoveAt(i);
                }
            }

            if (needReload)
            {
                ReloadResultFromSoftTestConfiguration(sc, dgvResult);
            }
        }

        public void SaveGridToFile(DataGridView dgvConfigItem, SaveFileDialog saveFileDialog)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
                {
                    string line = null, cellValue;
                    DataGridViewRow row;

                    for (int rowIndex = 0; rowIndex < dgvConfigItem.Rows.Count - 1; rowIndex++)
                    {
                        line = null;
                        row = dgvConfigItem.Rows[rowIndex];

                        for (int columnIndex = 0; columnIndex < row.Cells.Count - 1; columnIndex++)
                        {
                            cellValue =
                                (row.Cells[columnIndex].Value == null) ?
                                string.Empty : row.Cells[columnIndex].Value.ToString();

                            if (string.IsNullOrEmpty(cellValue))
                            {
                                if (dgvConfigItem.Columns[columnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
                                {
                                    cellValue = false.ToString();
                                }
                                else if (dgvConfigItem.Columns[columnIndex].GetType() == typeof(DataGridViewComboBoxColumn))
                                {
                                    cellValue = GlobalConsts.needToFilter;
                                }
                            }

                            line += cellValue + "\t";
                        }

                        file.WriteLine(line);
                    }
                }
            }
        }

    }
}
