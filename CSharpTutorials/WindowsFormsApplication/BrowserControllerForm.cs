using System;
using System.Windows.Forms;
using SHDocVw;

namespace WindowsFormsApplication
{
    public partial class BrowserControllerForm : Form
    {
        public BrowserControllerForm()
        {
            InitializeComponent();
        }

        private void btnStartBrowser_Click(object sender, EventArgs e)
        {
            object Empty = 0;
            object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";

            ie.Visible = true;
            ie.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);
        }

        void ie_ProgressChange(int Progress, int ProgressMax)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (pgbBrowser.Minimum <= Progress && Progress <= pgbBrowser.Maximum)
                {
                    pgbBrowser.Value = Progress;
                }
            });
        }

        private void BrowserControllerForm_Load(object sender, EventArgs e)
        {
            ie = new InternetExplorer();
            ie.ProgressChange += new DWebBrowserEvents2_ProgressChangeEventHandler(ie_ProgressChange);
        }

        private void BrowserControllerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ie.ProgressChange -= new DWebBrowserEvents2_ProgressChangeEventHandler(ie_ProgressChange);
            ie.Quit();
        }

        private void BrowserControllerForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
