using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestRunPKG;

namespace SoftTestDesigner
{
    public partial class TestRunSetting : Form
    {
        public TestRunSetting(
            List<string> softTestNameList,
            List<string> itemNameList,
            List<string[]> valueArrayList)
        {
            SoftTestNameList = softTestNameList;
            ItemNameList = itemNameList;
            ValueArrayList = valueArrayList;

            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            new TestRun(
                this.tbTestRunName.Text,
                this.tbManagerName.Text,
                this.tbBranchName.Text,
                this.tbVersion.Text,
                SoftTestNameList,
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
