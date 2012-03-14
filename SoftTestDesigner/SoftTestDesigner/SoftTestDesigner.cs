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
            dgvConfigItem.Rows.Add(true, "DOAEnabled", "DOA_/NonDOA_/", "True/False/", true, "9/11/180");
            dgvConfigItem.Rows.Add(true, "LOSEnabled", "LOS_/NonLOS_/", "True/False/", true, "18/10/180");
            dgvConfigItem.Rows.Add(true, "RatePlanActiveStatusTypeID", "Active_/Inactive_/", "2/3/", true, "6/11/180");
            dgvConfigItem.Rows.Add(true, "RatePlanTypeMask", "Standalone_/Package_/Corporate_/", "524288/16777216/8388608/", true, "19/15/13/170");
            dgvConfigItem.Rows.Add(true, "ARIEnabled", "ARI_/NonARI_/", "True/False/", true, "21/9/160");
            dgvConfigItem.Rows.Add(false, "HotelARIEnabled", "HotelARI_/NonHotelARI_", "True/False", false, "1/7");
            dgvConfigItem.Rows.Add(true, "RatePlanContractType", "MerchantTo/AgencyTo/FlexTo", "1/2/3", false, "1/1/1");
            dgvConfigItem.Rows.Add(true, "TargetRatePlanContractType", "Merchant/Agency/Flex", "1/2/3", false, "1/1/1");

            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "ARIEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=1 AND TargetRatePlanContractType=2");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=1 AND TargetRatePlanContractType=3");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=2 AND TargetRatePlanContractType=1");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=2 AND TargetRatePlanContractType=3");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=3 AND TargetRatePlanContractType=1");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToContain, "RatePlanContractType=3 AND TargetRatePlanContractType=2");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "RatePlanContractType=1 AND TargetRatePlanContractType=1");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "RatePlanContractType=2 AND TargetRatePlanContractType=2");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "RatePlanContractType=3 AND TargetRatePlanContractType=3");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "LAREnabled=True AND LARExtranetEnabled=False");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "LAREnabled=False AND LARExtranetEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "DOAEnabled=False AND LOSEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "PricingModel=OBP AND LOSEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "PricingModel=OBP AND HotelARIEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "PricingModel=PPP AND HotelARIEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "HotelContractType=2 AND ARIEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "EQCEnabled=True AND ARIEnabled=True");
            dgvRestriction.Rows.Add(true, GlobalConsts.needToFilter, "HotelContractType=2 AND RatePlanTypeMask=16777216");

            btnOneClick.Focus();
            uiProcessor = UIProcessor.CreateInstance();

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
                        row[j + 1] = uiProcessor.GetValue(itemNameList.ElementAt(j), softTest);
                    }

                    this.dgvResult.Rows.Add(row);
                }
            }
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
                bool flag = Convert.ToBoolean(dgvConfigItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                uiProcessor.EnableCell(dgvConfigItem.Rows[e.RowIndex].Cells[e.ColumnIndex + 1], flag);
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
                uiProcessor.CopyClipboard(dgvResult);
            }
            if ((e.Control && e.KeyCode == Keys.V) ||
                (e.Control && e.KeyCode == Keys.Insert) ||
                (e.Shift && e.KeyCode == Keys.Insert))
            {
                uiProcessor.PasteClipboard(dgvResult);
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

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string file = this.saveFileDialog.FileName;
        }

        private void btnGenerateCombination_Click(object sender, EventArgs e)
        {
            uiProcessor.GenerateCombination(out sc, dgvConfigItem, dgvRestriction, dgvResult, true);
        }

        private void btnApplyRestrictions_Click(object sender, EventArgs e)
        {
            uiProcessor.ApplyRestrictions(sc, dgvRestriction, dgvResult, true);
        }

        private void btnRemoveDuplicatedRows_Click(object sender, EventArgs e)
        {
            uiProcessor.RemoveDuplicatedRows(sc, dgvResult, true);
        }

        private void btnOneClick_Click(object sender, EventArgs e)
        {
            DisableAll();
            int retryTimes = 0;
            string output;
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();

            do
            {
                uiProcessor.GenerateCombination(out sc, dgvConfigItem, dgvRestriction, dgvResult, false);
                uiProcessor.ApplyRestrictions(sc, dgvRestriction, dgvResult, false);
                uiProcessor.RemoveDuplicatedRows(sc, dgvResult, false);

                if (uiProcessor.GetResultStatistics(out result, out output, dgvRestriction, dgvResult, sc))
                { break; }
            } while (++retryTimes <= 5);

            dgvResult.Enabled = true;
            uiProcessor.ReloadResultFromSoftTestConfiguration(sc, dgvResult);

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
            //sc = new SoftTestConfiguration(dgvConfigItem.Rows, dgvRestriction.Rows);
            string output;
            Dictionary<string, Dictionary<string, int>> itemValueCountDictionary;
            bool pass = uiProcessor.GetResultStatistics(out itemValueCountDictionary, out output, dgvRestriction, dgvResult, sc);

            StringBuilder statistics = new StringBuilder();
            statistics.AppendLine("Pass: " + pass + "\n");

            if (output != null)
            { statistics.AppendLine(output + "\n"); }

            foreach (KeyValuePair<string, Dictionary<string, int>> pair in itemValueCountDictionary)
            {
                statistics.AppendLine(pair.Key + ": ");
                foreach (KeyValuePair<string, int> innerPair in itemValueCountDictionary[pair.Key])
                {
                    statistics.Append(string.Format("\t{0}({1})", innerPair.Key, innerPair.Value));
                }
                statistics.AppendLine("\n");
            }

            MessageBox.Show(statistics.ToString(), "Statistics");
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            uiProcessor.MoveUp(dgvConfigItem);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            uiProcessor.MoveDown(dgvConfigItem);
        }

        private void btnMoveUpRestriction_Click(object sender, EventArgs e)
        {
            uiProcessor.MoveUp(dgvRestriction);
        }

        private void btnMoveDownRestriction_Click(object sender, EventArgs e)
        {
            uiProcessor.MoveDown(dgvRestriction);
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
            this.btnMoveDownRestriction.Enabled = false;
            this.btnMoveUp.Enabled = false;
            this.btnMoveUpRestriction.Enabled = false;
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
            this.btnMoveDownRestriction.Enabled = true;
            this.btnMoveUp.Enabled = true;
            this.btnMoveUpRestriction.Enabled = true;
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

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            uiProcessor.SaveGridToFile(dgvConfigItem, saveFileDialog);
        }

        private void btnOpenConfigFile_Click(object sender, EventArgs e)
        {
            uiProcessor.LoadGridFromFile(dgvConfigItem, openFileDialog);
        }

        private void btnSaveRestrictions_Click(object sender, EventArgs e)
        {
            uiProcessor.SaveGridToFile(dgvRestriction, saveFileDialog);
        }

        private void btnOpenRestrictions_Click(object sender, EventArgs e)
        {
            uiProcessor.LoadGridFromFile(dgvRestriction, openFileDialog);
        }

    }

}
