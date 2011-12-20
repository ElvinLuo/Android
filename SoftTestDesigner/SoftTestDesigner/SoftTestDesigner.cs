using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            dataGridView1.Rows.Add(true, "HotelContractType", "BMC.Merchant./BMC.Agency./BMC.Dual.", "1/2/3", "1/1/2");
            dataGridView1.Rows.Add(true, "PricingModel", "PDP./OBP./PPP.", "PDP/OBP/PPP", "3/1/1");
            dataGridView1.Rows.Add(true, "LAREnabled", "LAR_CheckUI/NonLAR_CheckUI", "True/False", "1/1");

            dataGridView2.Rows.Add(true, "PricingModel='OBP' AND LOSEnabled=TRUE");
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
            DataGridViewRow row;
            List<string[]> configRows = new List<string[]>();
            List<string[]> restrictionRows = new List<string[]>();
            string item, names, values;

            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("Soft Test Name", "Soft Test Name");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                row = dataGridView1.Rows[i];
                item = row.Cells[1].Value.ToString();
                names = row.Cells[2].Value.ToString();
                values = row.Cells[3].Value.ToString();
                if ((bool)row.Cells[0].Value)
                {
                    configRows.Add(new string[] { item, names, values });
                    dataGridView3.Columns.Add(item, item);
                }
            }

            if (configRows.Count > 0)
            {
                Input input = new Input(configRows, restrictionRows);
                Dictionary<string, string> softCases = input.GetAllSoftCasesFromConfig();

                List<string> cells = new List<string>();

                foreach (KeyValuePair<string, string> pair in softCases)
                {
                    cells.Clear();
                    cells.Add(pair.Key);

                    string[] valueArray = pair.Value.Split(new char[] { '/' });
                    foreach (string value in valueArray)
                    {
                        cells.Add(value);
                    }

                    dataGridView3.Rows.Add(cells.ToArray());
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row;
            List<ConfigItem> configItems = new List<ConfigItem>();

            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("Soft Test Name", "Soft Test Name");

            string item, names, values, coverages;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                row = dataGridView1.Rows[i];
                item = row.Cells[1].Value.ToString();
                names = row.Cells[2].Value.ToString();
                values = row.Cells[3].Value.ToString();
                coverages = row.Cells[4].Value.ToString();

                if ((bool)row.Cells[0].Value)
                {
                    configItems.Add(new ConfigItem(item, names, values, coverages));
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

    }

    public class ConfigItem
    {
        private bool flag;
        public int count;
        public string item;
        public string[] names, values;
        public int[] coverages;
        public bool[] flags;
        public List<int> indexes;
        public List<int> remainingIndexes;

        public ConfigItem(string item, string names, string values, string coverages)
        {
            this.flag = false;
            this.count = 0;
            this.item = item;
            this.names = names.Split(new char[] { '/' });
            this.values = values.Split(new char[] { '/' });
            string[] temp = coverages.Split(new char[] { '/' });
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
            //foreach (bool flag in this.flags)
            //{
            //    if (!flag)
            //        return false;
            //}
            return flag;
        }

    }
}
