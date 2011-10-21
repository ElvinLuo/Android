using SHDocVw;
namespace WindowsFormsApplication
{
    partial class BrowserControllerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartBrowser = new System.Windows.Forms.Button();
            this.pgbBrowser = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnStartBrowser
            // 
            this.btnStartBrowser.Location = new System.Drawing.Point(13, 12);
            this.btnStartBrowser.Name = "btnStartBrowser";
            this.btnStartBrowser.Size = new System.Drawing.Size(75, 21);
            this.btnStartBrowser.TabIndex = 0;
            this.btnStartBrowser.Text = "Start";
            this.btnStartBrowser.UseVisualStyleBackColor = true;
            this.btnStartBrowser.Click += new System.EventHandler(this.btnStartBrowser_Click);
            // 
            // pgbBrowser
            // 
            this.pgbBrowser.Location = new System.Drawing.Point(95, 11);
            this.pgbBrowser.Maximum = 10000;
            this.pgbBrowser.Name = "pgbBrowser";
            this.pgbBrowser.Size = new System.Drawing.Size(185, 21);
            this.pgbBrowser.TabIndex = 1;
            // 
            // BrowserControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 45);
            this.Controls.Add(this.pgbBrowser);
            this.Controls.Add(this.btnStartBrowser);
            this.KeyPreview = true;
            this.Name = "BrowserControllerForm";
            this.Text = "Windows Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowserControllerForm_FormClosed);
            this.Load += new System.EventHandler(this.BrowserControllerForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrowserControllerForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartBrowser;
        private System.Windows.Forms.ProgressBar pgbBrowser;
        InternetExplorer ie;
    }
}