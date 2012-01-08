using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
            dataGridView1.Controls.Add(checkBox1);
            dataGridView2.Controls.Add(checkBox2);

            dataGridView1.Rows.Add(true, "HotelContractType", "Merchant./Agency./Dual.", "1/2/3", false, "1/1/2");
            dataGridView1.Rows.Add(true, "PricingModel", "PDP./OBP./PPP.", "PDP/OBP/PPP", false, "3/1/1");
            dataGridView1.Rows.Add(true, "LAREnabled", "LAR_/NonLAR_", "True/False", false, "2/1");
            dataGridView1.Rows.Add(true, "LOSEnabled", "LOS_/NonLOS_", "True/False", true, "1/2");
            dataGridView1.Rows.Add(false, "ARIEnabled", "ARI_/NonARI_", "True/False", true, "1/3");
            dataGridView1.Rows.Add(false, "HotelARIEnabled", "HotelARI_/NonHotelARI_", "True/False", true, "1/7");

            dataGridView2.Rows.Add(true, "PricingModel=OBP AND LOSEnabled=TRUE");
            dataGridView2.Rows.Add(true, "PricingModel=OBP AND HotelARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "PricingModel=PPP AND HotelARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "HotelContractType=2 AND ARIEnabled=TRUE");
            dataGridView2.Rows.Add(false, "EQCEnabled=TRUE AND ARIEnabled=TRUE");
            dataGridView2.Rows.Add(false, "HotelContractType=2 AND RatePlanTypeMask=16777216");
            dataGridView2.Rows.Add(false, "DOAEnabled=FALSE AND LOSEnabled=TRUE");

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[0, i].Value = ((CheckBox)dataGridView1.Controls.Find("checkBox1", true)[0]).Checked;
            }
            dataGridView1.EndEdit();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2[0, i].Value = ((CheckBox)dataGridView2.Controls.Find("checkBox2", true)[0]).Checked;
            }
            dataGridView2.EndEdit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView3.Columns.Clear();
            SoftTestConfiguration sc = new SoftTestConfiguration(dataGridView1.Rows, dataGridView2.Rows);
            sc.GetResult();

            AddColumns(sc);
            AddRows(sc);
        }

        private void AddRows(SoftTestConfiguration sc)
        {
            string[] headerRow = new string[sc.itemNames.Count + 1];
            headerRow[0] = "Soft Test Name";

            for (int i = 0; i < sc.itemNames.Count; i++)
            { headerRow[i + 1] = sc.itemNames[i]; }

            dataGridView3.Rows.Add(headerRow);

            for (int i = 0; i < sc.softTestNameList.Count; i++)
            {
                List<string> finalRow = new List<string>();
                finalRow.Add(sc.softTestNameList.ElementAt(i));

                foreach (string value in sc.valueResultList.ElementAt(i))
                {
                    finalRow.Add(value);
                }

                dataGridView3.Rows.Add(finalRow.ToArray());
            }
        }

        private void AddColumns(SoftTestConfiguration sc)
        {
            dataGridView3.Columns.Add("Soft Test Name", "Soft Test Name");
            foreach (string columnName in sc.itemNames)
            {
                dataGridView3.Columns.Add(columnName, columnName);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.dataGridView3.Columns.Clear();

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
                    this.dataGridView3.Columns.Add(itemName, itemName);
                    row[i + 1] = itemName;
                }

                this.dataGridView3.Rows.Add(row);

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

                    this.dataGridView3.Rows.Add(row);
                }
            }
        }

        private string GetValue(string itemName, SoftTest softTest)
        {
            string result = string.Empty;

            foreach (Data data in softTest.testData)
            {
                if (data.dataName.Equals(itemName))
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex != dataGridView2.Rows.Count - 1 &&
                e.RowIndex != -1)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex != dataGridView1.Rows.Count - 1 &&
                e.RowIndex != -1)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        row.Selected = true;
                    }

                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(this.dataGridView3.GetClipboardContent());

                    foreach (DataGridViewRow row in dataGridView3.Rows)
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

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex == 0) return;

            string strRowNumber = e.RowIndex.ToString();

            while (strRowNumber.Length < dataGridView3.RowCount.ToString().Length)
            {
                strRowNumber = "0" + strRowNumber;
            }

            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);

            if (dataGridView3.RowHeadersWidth < (int)(size.Width + 20))
            {
                dataGridView3.RowHeadersWidth = (int)(size.Width + 20);
            }

            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(
                strRowNumber,
                this.Font,
                b,
                e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 &&
               dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
               e.RowIndex != dataGridView1.Rows.Count - 1 &&
               e.RowIndex != -1)
            {
                bool flag = (bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                EnableCell(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1], flag);
            }
        }

        private void btnCreateLabrun_Click(object sender, EventArgs e)
        {
            List<string> softTestNameList = new List<string>();
            List<string> itemNameList = new List<string>();
            List<string[]> valueArrayList = new List<string[]>();

            for (int i = 1; i < dataGridView3.Columns.Count; i++)
            {
                itemNameList.Add(dataGridView3.Rows[0].Cells[i].Value.ToString().Trim());
            }

            for (int i = 1; i < dataGridView3.Rows.Count - 1; i++)
            {
                softTestNameList.Add(dataGridView3.Rows[i].Cells[0].Value.ToString().Trim());

                string[] valueArray = new string[dataGridView3.Columns.Count - 1];
                for (int j = 1; j < dataGridView3.Columns.Count; j++)
                { valueArray[j - 1] = dataGridView3.Rows[i].Cells[j].Value.ToString().Trim(); }
                valueArrayList.Add(valueArray);
            }

            SoftTestSetting softTestSetting = new SoftTestSetting(
                softTestNameList,
                itemNameList,
                valueArrayList);
            softTestSetting.ShowDialog();
        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
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
            DataObject d = dataGridView3.GetClipboardContent();
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
                int iRow = dataGridView3.CurrentCell.RowIndex;
                int iCol = dataGridView3.CurrentCell.ColumnIndex;

                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        if (iRow == dataGridView3.RowCount - 1)
                        { dataGridView3.Rows.Add(1); }

                        sCells = line.Split('\t');

                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i == this.dataGridView3.ColumnCount)
                            { dataGridView3.Columns.Add("NewColumn", "NewColumn"); }

                            oCell = dataGridView3[iCol + i, iRow];

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
            for (int i = 1; i < dataGridView3.Columns.Count; i++)
            {
                itemNameList.Add(dataGridView3.Rows[0].Cells[i].Value.ToString().Trim());
            }

            string[] softTestNameArray = new string[dataGridView3.Rows.Count - 1];
            string[] valueArray;
            List<string[]> valueArrayList = new List<string[]>();
            for (int i = 1; i < dataGridView3.Rows.Count - 1; i++)
            {
                softTestNameArray[i - 1] = dataGridView3.Rows[i].Cells[0].Value.ToString().Trim();

                valueArray = new string[dataGridView3.Columns.Count - 1];
                for (int j = 1; j < dataGridView3.Columns.Count; j++)
                { valueArray[j - 1] = dataGridView3.Rows[i].Cells[j].Value.ToString().Trim(); }

                valueArrayList.Add(valueArray);
            }

            TestRunSetting testRunSetting = new TestRunSetting(
                softTestNameArray,
                itemNameList,
                valueArrayList);
            testRunSetting.ShowDialog();
        }

        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            this.dataGridView3.Columns.Clear();
            this.dataGridView3.Columns.Add("", "");
        }

        private void btnSaveRestrictions_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string file = this.saveFileDialog.FileName;
        }

    }

}
