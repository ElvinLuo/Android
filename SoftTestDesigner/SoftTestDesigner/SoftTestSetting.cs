using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SoftTestDesigner
{
    public partial class SoftTestSetting : Form
    {
        public SoftTestSetting(
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
            for (int i = 0; i < SoftTestNameList.Count; i++)
            {
                new SoftTestPKG.SoftTest(
                    i,
                    this.tbTestTeam.Text,
                    this.tbCategory.Text,
                    this.tbRisktier.Text,
                    this.cbOverrideMethod.Checked,
                    this.tbMethod.Text,
                    this.tbLOBMaskName.Text,
                    this.tbEnvironmentType.Text,
                    SoftTestNameList.ElementAt(i),
                    ItemNameList,
                    ValueArrayList.ElementAt(i));
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SoftTestSetting_Load(object sender, EventArgs e)
        {
            this.tbMethod.Enabled = false;
        }

        private void cbOverrideMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbOverrideMethod.Checked)
            {
                this.tbMethod.Enabled = true;
            }
            else
            {
                this.tbMethod.Enabled = false;
            }
        }
    }
}
