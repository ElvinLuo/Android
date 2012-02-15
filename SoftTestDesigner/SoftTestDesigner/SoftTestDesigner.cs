using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SoftTestPKG;

namespace SoftTestDesigner
{
    public partial class SoftTestDesigner : Form
    {
        public SoftTestDesigner()
        {
            InitializeComponent();
        }

        private void SoftTestDesigner_Load(object sender, EventArgs e)
        {
            dgvConfigItem.Controls.Add(cbSelectAllConfigItems);
            dgvRestriction.Controls.Add(cbSelectAllRestrictions);

            dgvConfigItem.Rows.Add(true, "HotelContractType", "Merchant./Agency./Dual.", "1/2/3", true, "12/11/180");
            dgvConfigItem.Rows.Add(true, "PricingModel", "PDP./OBP./PPP.", "PDP/OBP/PPP", true, "160/13/14");
            dgvConfigItem.Rows.Add(true, "LAREnabled", "LAR_/NonLAR_", "True/False", true, "59/15");
            dgvConfigItem.Rows.Add(true, "LARExtranetEnabled", "/", "True/False", true, "69/16");
            dgvConfigItem.Rows.Add(false, "ExtranetState", "Lite_/Std_/Adv_/HIMS_", "1/2/3/", true, "10/20/70/100");
            dgvConfigItem.Rows.Add(true, "DOAEnabled", "DOA_/NonDOA_/", "True/False/", true, "37/3/180");
            dgvConfigItem.Rows.Add(true, "LOSEnabled", "LOS_/NonLOS_/", "True/False/", true, "18/10/180");
            dgvConfigItem.Rows.Add(true, "RatePlanActiveStatusTypeID", "Active_/Inactive_/", "2/3/", true, "6/11/180");
            dgvConfigItem.Rows.Add(true, "RatePlanTypeMask", "Standalone_/Package_/Corporate_/", "524288/16777216/8388608/", true, "19/15/13/170");
            dgvConfigItem.Rows.Add(true, "ARIEnabled", "ARI_/NonARI_/", "True/False/", true, "21/3/160");
            dgvConfigItem.Rows.Add(false, "HotelARIEnabled", "HotelARI_/NonHotelARI_", "True/False", false, "1/7");
            dgvConfigItem.Rows.Add(true, "RatePlanContractType", "MerchantTo/AgencyTo/FlexTo", "1/2/3", false, "1/1/1");
            dgvConfigItem.Rows.Add(true, "TargetRatePlanContractType", "Merchant/Agency/Flex", "1/2/3", false, "1/1/1");

            dgvRestriction.Rows.Add(true, "RatePlanContractType=1 AND TargetRatePlanContractType=1");
            dgvRestriction.Rows.Add(true, "RatePlanContractType=2 AND TargetRatePlanContractType=2");
            dgvRestriction.Rows.Add(true, "RatePlanContractType=3 AND TargetRatePlanContractType=3");
            dgvRestriction.Rows.Add(true, "LAREnabled=True AND LARExtranetEnabled=False");
            dgvRestriction.Rows.Add(true, "LAREnabled=False AND LARExtranetEnabled=True");
            dgvRestriction.Rows.Add(true, "DOAEnabled=False AND LOSEnabled=True");
            dgvRestriction.Rows.Add(true, "PricingModel=OBP AND LOSEnabled=True");
            dgvRestriction.Rows.Add(true, "PricingModel=OBP AND HotelARIEnabled=True");
            dgvRestriction.Rows.Add(true, "PricingModel=PPP AND HotelARIEnabled=True");
            dgvRestriction.Rows.Add(true, "HotelContractType=2 AND ARIEnabled=True");
            dgvRestriction.Rows.Add(true, "EQCEnabled=True AND ARIEnabled=True");
            dgvRestriction.Rows.Add(true, "HotelContractType=2 AND RatePlanTypeMask=16777216");

        }

        private void cbSelectAllConfigItems_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvConfigItem.RowCount; i++)
            {
                dgvConfigItem[0, i].Value = ((CheckBox)dgvConfigItem.Controls.Find("cbSelectAllConfigItems", true)[0]).Checked;
            }
            dgvConfigItem.EndEdit();
        }

        private void cbSelectAllRestrictions_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvRestriction.RowCount; i++)
            {
                dgvRestriction[0, i].Value = ((CheckBox)dgvRestriction.Controls.Find("cbSelectAllRestrictions", true)[0]).Checked;
            }
            dgvRestriction.EndEdit();
        }

        private void AddRows(SoftTestConfiguration sc)
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

        private void AddColumns(SoftTestConfiguration sc)
        {
            dgvResult.Columns.Add("Soft Test Name", "Soft Test Name");
            foreach (string columnName in sc.itemNames)
            {
                dgvResult.Columns.Add(columnName, columnName);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.dgvResult.Columns.Clear();

                SoftTest softTest;
                List<SoftTest> softTestList = new List<SoftTest>();
                List<string> softTestNameList = new List<string>();
                List<string> itemNameList = new List<string>();
                List<List<string>> valueList = new List<List<string>>();
                string selectedPath = this.folderBrowserDialog.SelectedPath;

                string[] files = Directory.GetFiles(
                    selectedPath,
                    "*.xml",
                    SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    softTest = (SoftTest)Serializer.CreateInstance().DeserializeFromXML(typeof(SoftTest), file);
                    softTestList.Add(softTest);

                    foreach (Data data in softTest.testData)
                    {
                        if (!itemNameList.Contains(data.dataName))
                        { itemNameList.Add(data.dataName); }
                    }
                }

                itemNameList.Sort();
                string[] row = new string[itemNameList.Count + 1];
                row[0] = "Soft Test Name";

                for (int i = 0; i < itemNameList.Count; i++)
                {
                    string itemName = itemNameList.ElementAt(i);
                    this.dgvResult.Columns.Add(itemName, itemName);
                    row[i + 1] = itemName;
                }

                this.dgvResult.Rows.Add(row);

                for (int i = 0; i < files.Length; i++)
                {
                    row[0] = files[i].Replace(selectedPath.Substring(0, selectedPath.LastIndexOf("\\") + 1), "");
                    row[0] = row[0].Replace(".test.xml", "");
                    row[0] = row[0].Replace("\\", ".");
                    softTest = softTestList.ElementAt(i);

                    for (int j = 0; j < itemNameList.Count; j++)
                    {
                        row[j + 1] = GetValue(itemNameList.ElementAt(j), softTest);
                    }

                    this.dgvResult.Rows.Add(row);
                }
            }
        }

        private string GetValue(string itemName, SoftTest softTest)
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

        private bool NeedToContinue(List<ConfigItem> items)
        {
            foreach (ConfigItem item in items)
            {
                if (!item.Checked())
                    return true;
            }

            return false;
        }

        private void dgvRestriction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRestriction.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex != dgvRestriction.Rows.Count - 1 &&
                e.RowIndex != -1)
            {
                dgvRestriction.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void dgvConfigItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConfigItem.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex != dgvConfigItem.Rows.Count - 1 &&
                e.RowIndex != -1)
            {
                dgvConfigItem.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void EnableCell(DataGridViewCell dc, bool enabled)
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dgvResult.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dgvResult.Rows)
                    {
                        row.Selected = true;
                    }

                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(this.dgvResult.GetClipboardContent());

                    foreach (DataGridViewRow row in dgvResult.Rows)
                    {
                        row.Selected = false;
                    }
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    throw new Exception("Failed to copy selected cells to clipboard.");
                }
            }
        }

        private void dgvConfigItem_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView_RowPostPaint(e, 0, 1);
        }

        private void dgvRestriction_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView_RowPostPaint(e, 0, 1);
        }

        private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView_RowPostPaint(e, 1, 0);
        }

        private void DataGridView_RowPostPaint(DataGridViewRowPostPaintEventArgs e, int startIndex, int startValue)
        {
            if (e.RowIndex < startIndex) return;

            string strRowNumber = (e.RowIndex + startValue).ToString();

            while (strRowNumber.Length < dgvResult.RowCount.ToString().Length)
            {
                strRowNumber = "0" + strRowNumber;
            }

            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);

            if (dgvResult.RowHeadersWidth < (int)(size.Width + 20))
            {
                dgvResult.RowHeadersWidth = (int)(size.Width + 20);
            }

            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(
                strRowNumber,
                this.Font,
                b,
                e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dgvConfigItem_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvConfigItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 &&
               dgvConfigItem.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
               e.RowIndex != dgvConfigItem.Rows.Count - 1 &&
               e.RowIndex != -1)
            {
                bool flag = (bool)dgvConfigItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                EnableCell(dgvConfigItem.Rows[e.RowIndex].Cells[e.ColumnIndex + 1], flag);
            }
        }

        private void btnCreateLabrun_Click(object sender, EventArgs e)
        {
            List<string> softTestNameList = new List<string>();
            List<string> itemNameList = new List<string>();
            List<string[]> valueArrayList = new List<string[]>();

            for (int i = 1; i < dgvResult.Columns.Count; i++)
            {
                itemNameList.Add(dgvResult.Rows[0].Cells[i].Value.ToString().Trim());
            }

            for (int i = 1; i < dgvResult.Rows.Count - 1; i++)
            {
                softTestNameList.Add(dgvResult.Rows[i].Cells[0].Value.ToString().Trim());

                string[] valueArray = new string[dgvResult.Columns.Count - 1];
                for (int j = 1; j < dgvResult.Columns.Count; j++)
                {
                    valueArray[j - 1] =
                        dgvResult.Rows[i].Cells[j].Value == null ?
                        string.Empty :
                        dgvResult.Rows[i].Cells[j].Value.ToString().Trim();
                }
                valueArrayList.Add(valueArray);
            }

            SoftTestSetting softTestSetting = new SoftTestSetting(
                softTestNameList,
                itemNameList,
                valueArrayList);
            softTestSetting.ShowDialog();
        }

        private void dgvResult_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.C) ||
                (e.Control && e.KeyCode == Keys.Delete) ||
                (e.Shift && e.KeyCode == Keys.Delete))
            {
                CopyClipboard();
            }
            if ((e.Control && e.KeyCode == Keys.V) ||
                (e.Control && e.KeyCode == Keys.Insert) ||
                (e.Shift && e.KeyCode == Keys.Insert))
            {
                PasteClipboard();
            }
        }

        private void CopyClipboard()
        {
            DataObject d = dgvResult.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        private void PasteClipboard()
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
                            if (iCol + i == this.dgvResult.ColumnCount)
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

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            List<string> itemNameList = new List<string>();

            for (int i = 1; i < dgvResult.Columns.Count; i++)
            {
                itemNameList.Add(
                    dgvResult.Rows[0].Cells[i].Value == null ?
                    string.Empty :
                    dgvResult.Rows[0].Cells[i].Value.ToString().Trim());
            }

            string softTestName;
            string[] valueArray;
            List<string> softTestNameList = new List<string>();
            List<string[]> valueArrayList = new List<string[]>();

            for (int i = 1; i < dgvResult.Rows.Count - 1; i++)
            {
                softTestName =
                    dgvResult.Rows[i].Cells[0].Value == null ?
                    string.Empty :
                    dgvResult.Rows[i].Cells[0].Value.ToString().Trim();

                if (string.IsNullOrEmpty(softTestName) || softTestName.Equals("")) continue;

                valueArray = new string[dgvResult.Columns.Count - 1];

                for (int j = 1; j < dgvResult.Columns.Count; j++)
                {
                    valueArray[j - 1] =
                        dgvResult.Rows[i].Cells[j].Value == null ?
                        string.Empty :
                        dgvResult.Rows[i].Cells[j].Value.ToString().Trim();
                }

                softTestNameList.Add(softTestName);
                valueArrayList.Add(valueArray);
            }

            TestRunSetting testRunSetting = new TestRunSetting(
                softTestNameList,
                itemNameList,
                valueArrayList);
            testRunSetting.ShowDialog();
        }

        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            this.dgvResult.Columns.Clear();
            this.dgvResult.Columns.Add("", "");
        }

        private void btnSaveRestrictions_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string file = this.saveFileDialog.FileName;
        }

        private void btnGenerateCombination_Click(object sender, EventArgs e)
        {
            GenerateCombination();
        }

        private void GenerateCombination()
        {
            sc = new SoftTestConfiguration(dgvConfigItem.Rows, dgvRestriction.Rows);
            sc.GetResultWitoutRestrictions();
            ReloadResultFromSoftTestConfiguration(sc);
        }

        private void btnApplyRestrictions_Click(object sender, EventArgs e)
        {
            ApplyRestrictions();
        }

        private void ApplyRestrictions()
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

            ReloadResultFromSoftTestConfiguration(sc);
        }

        private void btnRemoveDuplicatedRows_Click(object sender, EventArgs e)
        {
            RemoveDuplicatedRows();
        }

        private void RemoveDuplicatedRows()
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

            ReloadResultFromSoftTestConfiguration(sc);
        }

        private void ReloadResultFromSoftTestConfiguration(SoftTestConfiguration sc)
        {
            dgvResult.Columns.Clear();
            AddColumns(sc);
            AddRows(sc);
        }

        private void btnOneClick_Click(object sender, EventArgs e)
        {
            DisableAll();
            int retryTimes = 0;
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();

            do
            {
                GenerateCombination();
                RemoveDuplicatedRows();
                ApplyRestrictions();
                retryTimes++;
            } while (retryTimes <= 5 && !GetResultStatistics(out result));

            EnableAll();
        }

        private void btnCoveragesMultiplyBy10_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvConfigItem.Rows)
            {
                if (row.Cells[5].Value == null) continue;

                row.Cells[5].Value = row.Cells[5].Value.ToString().Replace("/", "0/");
                row.Cells[5].Value = row.Cells[5].Value.ToString() + "0";
            }
        }

        private void btnShowResultStatistics_Click(object sender, EventArgs e)
        {
            Dictionary<string, Dictionary<string, int>> itemValueCountDictionary;
            bool pass = GetResultStatistics(out itemValueCountDictionary);

            StringBuilder statistics = new StringBuilder();
            statistics.AppendLine("Pass: " + pass + "\n");

            foreach (KeyValuePair<string, Dictionary<string, int>> pair in itemValueCountDictionary)
            {
                statistics.AppendLine(pair.Key + ": ");
                foreach (KeyValuePair<string, int> innerPair in itemValueCountDictionary[pair.Key])
                {
                    statistics.Append(string.Format("\t{0}({1})", innerPair.Key, innerPair.Value));
                }
                statistics.AppendLine("\n");
            }

            MessageBox.Show(statistics.ToString());
        }

        private bool GetResultStatistics(out Dictionary<string, Dictionary<string, int>> itemValueCountDictionary)
        {
            string itemName, cellValue;
            int columnCount = dgvResult.Rows[0].Cells.Count;
            itemValueCountDictionary = new Dictionary<string, Dictionary<string, int>>();

            for (int columnIndex = 0; columnIndex < sc.allConfigItems.Length; columnIndex++)
            {
                itemName = sc.allConfigItems[columnIndex].item;

                if (!itemValueCountDictionary.ContainsKey(itemName))
                { itemValueCountDictionary.Add(itemName, new Dictionary<string, int>()); }

                foreach (string value in sc.allConfigItems[columnIndex].values)
                {
                    itemValueCountDictionary[itemName].Add(value, 0);
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

            foreach (KeyValuePair<string, Dictionary<string, int>> pair in itemValueCountDictionary)
            {
                foreach (KeyValuePair<string, int> innerPair in itemValueCountDictionary[pair.Key])
                {
                    if (innerPair.Value == 0)
                        return false;
                }
            }

            return true;
        }

        private void DisableAll()
        {
            this.btnApplyRestrictions.Enabled = false;
            this.btnClearDataGridView.Enabled = false;
            this.btnCopy.Enabled = false;
            this.btnCoveragesMultiplyBy10.Enabled = false;
            this.btnCreateAssignment.Enabled = false;
            this.btnCreateLabrun.Enabled = false;
            this.btnGenerateCombination.Enabled = false;
            this.btnMoveDown.Enabled = false;
            this.btnMoveUp.Enabled = false;
            this.btnOneClick.Enabled = false;
            this.btnOpenConfigFile.Enabled = false;
            this.btnOpenRestrictions.Enabled = false;
            this.btnRemoveDuplicatedRows.Enabled = false;
            this.btnSaveRestrictions.Enabled = false;
            this.btnSaveToFile.Enabled = false;
            this.btnSelectFolder.Enabled = false;
            this.btnShowResultStatistics.Enabled = false;
            this.dgvConfigItem.Enabled = false;
            this.dgvRestriction.Enabled = false;
            this.dgvResult.Enabled = false;
        }

        private void EnableAll()
        {
            this.btnApplyRestrictions.Enabled = true;
            this.btnClearDataGridView.Enabled = true;
            this.btnCopy.Enabled = true;
            this.btnCoveragesMultiplyBy10.Enabled = true;
            this.btnCreateAssignment.Enabled = true;
            this.btnCreateLabrun.Enabled = true;
            this.btnGenerateCombination.Enabled = true;
            this.btnMoveDown.Enabled = true;
            this.btnMoveUp.Enabled = true;
            this.btnOneClick.Enabled = true;
            this.btnOpenConfigFile.Enabled = true;
            this.btnOpenRestrictions.Enabled = true;
            this.btnRemoveDuplicatedRows.Enabled = true;
            this.btnSaveRestrictions.Enabled = true;
            this.btnSaveToFile.Enabled = true;
            this.btnSelectFolder.Enabled = true;
            this.btnShowResultStatistics.Enabled = true;
            this.dgvConfigItem.Enabled = true;
            this.dgvRestriction.Enabled = true;
            this.dgvResult.Enabled = true;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvConfigItem.SelectedCells.Count == 0) return;
            int selectedIndex = dgvConfigItem.SelectedCells[0].RowIndex;
            if (selectedIndex == 0) return;
            int previousIndex = selectedIndex - 1;
            DataGridViewRow previousRow = (DataGridViewRow)dgvConfigItem.Rows[previousIndex].Clone();
            CopyDataGridViewRow(dgvConfigItem.Rows[previousIndex], previousRow);
            CopyDataGridViewRow(dgvConfigItem.Rows[selectedIndex], dgvConfigItem.Rows[previousIndex]);
            CopyDataGridViewRow(previousRow, dgvConfigItem.Rows[selectedIndex]);
            dgvConfigItem.Rows[selectedIndex].Selected = false;
            dgvConfigItem.Rows[previousIndex].Selected = true;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvConfigItem.SelectedCells.Count == 0) return;
            int selectedIndex = dgvConfigItem.SelectedCells[0].RowIndex;

            if (selectedIndex == dgvConfigItem.Rows.Count - 1 ||
                selectedIndex == dgvConfigItem.Rows.Count - 2)
            { return; }

            int nextIndex = selectedIndex + 1;
            DataGridViewRow nextRow = (DataGridViewRow)dgvConfigItem.Rows[nextIndex].Clone();
            CopyDataGridViewRow(dgvConfigItem.Rows[nextIndex], nextRow);
            CopyDataGridViewRow(dgvConfigItem.Rows[selectedIndex], dgvConfigItem.Rows[nextIndex]);
            CopyDataGridViewRow(nextRow, dgvConfigItem.Rows[selectedIndex]);
            dgvConfigItem.Rows[selectedIndex].Selected = false;
            dgvConfigItem.Rows[nextIndex].Selected = true;
        }

        private void CopyDataGridViewRow(DataGridViewRow source, DataGridViewRow target)
        {
            if (target.Cells.Count == source.Cells.Count)
            {
                for (int i = 0; i < source.Cells.Count; i++)
                {
                    target.Cells[i].Value = source.Cells[i].Value;
                }
            }
        }

    }

}
