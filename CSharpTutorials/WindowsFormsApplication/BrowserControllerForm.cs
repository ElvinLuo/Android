using System;
using System.Windows.Forms;
using SHDocVw;
using System.Drawing;

namespace WindowsFormsApplication
{
    public partial class BrowserControllerForm : Form
    {
        public BrowserControllerForm()
        {
            InitializeComponent();
            button1.Focus();
        }

        private void btnStartBrowser_Click(object sender, EventArgs e)
        {
            object Empty = 0;
            object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi?group=INTEGRATION&query=ON&serverlookup=ON&sites=ON&alternate_service=";

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
                    SetProgressBarText(pgbBrowser, "Loading", Color.Red,
                        new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
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

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void SetProgressBarText(ProgressBar Target, string Text, Color TextColor, Font TextFont)
        {
            if (Target == null) { throw new ArgumentException("Null Target"); }
            if (string.IsNullOrEmpty(Text))
            {
                int percent = (int)(((double)(Target.Value - Target.Minimum) / (double)(Target.Maximum - Target.Minimum)) * 100);
                Text = percent.ToString() + "%";
            }
            using (Graphics gr = Target.CreateGraphics())
            {
                gr.DrawString(Text, TextFont, new SolidBrush(TextColor),
                    new PointF(Target.Width / 2 - (gr.MeasureString(Text, TextFont).Width / 2.0F), Target.Height / 2 - (gr.MeasureString(Text, TextFont).Height / 2.0F)));
            }
        }

    }
}
