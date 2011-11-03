using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatabaseSelector
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();

            tbTEMCDefault.Text = Global.defaultTEMCURL;
            tbPPEDSNListDefault.Text = Global.defaultXLSFile;
            tbPortMappingsDefault.Text = Global.defaultTXTFile;

            index = Index.CreateInstance();
            tbTEMC.Text = index.TEMCURL;
            tbPPEDSNList.Text = index.XLSFile;
            tbPortMappings.Text = index.TXTFile;
        }

        private void btnOpenXLS_Click(object sender, EventArgs e)
        {
            ofdXLS.ShowDialog();
        }

        private void btnOpenTXT_Click(object sender, EventArgs e)
        {
            ofdTXT.ShowDialog();
        }

        private void ofdXLS_FileOk(object sender, CancelEventArgs e)
        {
            tbPPEDSNList.Text = ofdXLS.FileName;
        }

        private void ofdTXT_FileOk(object sender, CancelEventArgs e)
        {
            tbPortMappings.Text = ofdTXT.FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            index.TEMCURL = tbTEMC.Text;
            index.XLSFile = tbPPEDSNList.Text;
            index.TXTFile = tbPortMappings.Text;
            index.SaveIndexToXml();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Options_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            { btnCancel_Click(sender, e); }
        }

        private void btnAllResetToDefault_Click(object sender, EventArgs e)
        {
            tbTEMC.Text = tbTEMCDefault.Text;
            tbPPEDSNList.Text = tbPPEDSNListDefault.Text;
            tbPortMappings.Text = tbPortMappingsDefault.Text;
        }
    }
}
