using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoftTestDesigner
{
    public partial class TestRunSetting : Form
    {
        public TestRunSetting(
            string[] softTestNameArray,
            List<string> itemNameList,
            List<string[]> valueArrayList)
        {
            SoftTestNameArray = softTestNameArray;
            ItemNameList = itemNameList;
            ValueArrayList = valueArrayList;

            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            new TestRun.TestRun(
                this.tbTestRunName.Text,
                this.tbManagerName.Text,
                this.tbBranchName.Text,
                this.tbVersion.Text,
                SoftTestNameArray,
                ItemNameList,
                ValueArrayList,
                this.tbMethodName.Text,
                null,
                null);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
