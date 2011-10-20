using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            InternetExplorer ie = new InternetExplorer();
            ie.ProgressChange += new DWebBrowserEvents2_ProgressChangeEventHandler(ie_ProgressChange);
            object Empty = 0;
            object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi?";

            ie.Visible = true;
            ie.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

            while (ie.Busy)
            {
                System.Threading.Thread.Sleep(1000);
            }

            ie.Quit();
        }

        void ie_ProgressChange(int Progress, int ProgressMax)
        {
            Console.Write(Progress);
        }
    }
}
