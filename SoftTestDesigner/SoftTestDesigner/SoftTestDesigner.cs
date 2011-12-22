using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SoftCaseGenerator
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

            dataGridView1.Rows.Add(true, "HotelContractType", "BMC.Merchant./BMC.Agency./BMC.Dual.", "1/2/3", false, "1/1/2");
            dataGridView1.Rows.Add(true, "PricingModel", "PDP./OBP./PPP.", "PDP/OBP/PPP", false, "3/1/1");
            dataGridView1.Rows.Add(true, "LAREnabled", "LAR_CheckUI/NonLAR_CheckUI", "True/False", true, "1/1");

            dataGridView2.Rows.Add(true, "PricingModel=OBP AND LOSEnabled=TRUE");
            dataGridView2.Rows.Add(true, "PricingModel=OBP AND HotelARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "PricingModel=PPP AND HotelARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "HotelContractType=2 AND ARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "EQCEnabled=TRUE AND ARIEnabled=TRUE");
            dataGridView2.Rows.Add(true, "HotelContractType=2 AND RatePlanTypeMask=16777216");
            dataGridView2.Rows.Add(true, "DOAEnabled=FALSE AND LOSEnabled=TRUE");

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
            SoftTestConfiguration sc = new SoftTestConfiguration(dataGridView1.Rows, dataGridView2.Rows);
            sc.GetResult();

            //DataGridViewRow row;
            //List<string[]> configRows = new List<string[]>();
            //List<string[]> restrictionRows = new List<string[]>();
            //string item, names, values;

            //dataGridView3.Columns.Clear();
            //dataGridView3.Columns.Add("Soft Test Name", "Soft Test Name");
            //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //{
            //    row = dataGridView1.Rows[i];
            //    item = row.Cells[1].Value.ToString();
            //    names = row.Cells[2].Value.ToString();
            //    values = row.Cells[3].Value.ToString();
            //    if ((bool)row.Cells[0].Value)
            //    {
            //        configRows.Add(new string[] { item, names, values });
            //        dataGridView3.Columns.Add(item, item);
            //    }
            //}

            //if (configRows.Count > 0)
            //{
            //    Input input = new Input(configRows, restrictionRows);
            //    Dictionary<string, string> softCases = input.GetAllSoftCasesFromConfig();

            //    List<string> cells = new List<string>();

            //    foreach (KeyValuePair<string, string> pair in softCases)
            //    {
            //        cells.Clear();
            //        cells.Add(pair.Key);

            //        string[] valueArray = pair.Value.Split(new char[] { '/' });
            //        foreach (string value in valueArray)
            //        {
            //            cells.Add(value);
            //        }

            //        dataGridView3.Rows.Add(cells.ToArray());
            //    }
            //}

            //dataGridView3.Rows[0].Selected = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row;
            List<ConfigItem> configItems = new List<ConfigItem>();

            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("Soft Test Name", "Soft Test Name");

            string item, names, values, random, coverages;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                row = dataGridView1.Rows[i];
                item = row.Cells[1].Value.ToString();
                names = row.Cells[2].Value.ToString();
                values = row.Cells[3].Value.ToString();
                random = row.Cells[4].Value.ToString();
                coverages = row.Cells[5].Value.ToString();

                if ((bool)row.Cells[0].Value)
                {
                    configItems.Add(new ConfigItem(item, names, values, random, coverages));
                    dataGridView3.Columns.Add(item, item);
                }
            }

            int index;
            string softTestName;
            ConfigItem configItem;
            List<string> cells = new List<string>();

            while (NeedToContinue(configItems))
            {
                cells.Clear();
                softTestName = "";

                for (int i = 0; i < configItems.Count; i++)
                {
                    configItem = configItems.ElementAt(i);
                    index = configItem.GetIndex();
                    softTestName += configItem.names[index];
                    cells.Add(configItem.values[index]);
                }
                cells.Insert(0, softTestName);
                dataGridView3.Rows.Add(cells.ToArray());
            }

            dataGridView3.Rows[0].Selected = false;
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
            string strRowNumber = (e.RowIndex + 1).ToString();

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

    }

}
