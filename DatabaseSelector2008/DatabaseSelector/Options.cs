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

            tbTEMCDefault.Text = @"http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";
            tbPPEDSNListDefault.Text = Serializer.CreateInstance().applicationFolder + "PPE_DSN_List.xls";
            tbPortMappingsDefault.Text = Serializer.CreateInstance().applicationFolder + "WingatePortMappingsForRTT_PPE.txt";

            index = Index.CreateInstance();
            tbTEMC.Text = index.temcurl;
            tbPPEDSNList.Text = index.xlsFile;
            tbPortMappings.Text = index.txtFile;
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
            index.temcurl = tbTEMC.Text;
            index.xlsFile = tbPPEDSNList.Text;
            index.txtFile = tbPortMappings.Text;
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
