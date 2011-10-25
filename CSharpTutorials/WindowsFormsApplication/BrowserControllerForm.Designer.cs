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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartBrowser
            // 
            this.btnStartBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartBrowser.Location = new System.Drawing.Point(13, 13);
            this.btnStartBrowser.Name = "btnStartBrowser";
            this.btnStartBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnStartBrowser.TabIndex = 3;
            this.btnStartBrowser.Text = "Start";
            this.btnStartBrowser.UseVisualStyleBackColor = true;
            this.btnStartBrowser.Click += new System.EventHandler(this.btnStartBrowser_Click);
            // 
            // pgbBrowser
            // 
            this.pgbBrowser.Location = new System.Drawing.Point(95, 12);
            this.pgbBrowser.Maximum = 10000;
            this.pgbBrowser.Name = "pgbBrowser";
            this.pgbBrowser.Size = new System.Drawing.Size(195, 23);
            this.pgbBrowser.Step = 1;
            this.pgbBrowser.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(95, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BrowserControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 92);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pgbBrowser);
            this.Controls.Add(this.btnStartBrowser);
            this.KeyPreview = true;
            this.Name = "BrowserControllerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowserControllerForm_FormClosed);
            this.Load += new System.EventHandler(this.BrowserControllerForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrowserControllerForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartBrowser;
        private System.Windows.Forms.ProgressBar pgbBrowser;
        InternetExplorer ie;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}