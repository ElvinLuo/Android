namespace DatabaseSelector
{
    partial class Options
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
            this.lbTEMC = new System.Windows.Forms.Label();
            this.tbTEMC = new System.Windows.Forms.TextBox();
            this.lbTEMCDefault = new System.Windows.Forms.Label();
            this.tbTEMCDefault = new System.Windows.Forms.TextBox();
            this.lbPPEDSNList = new System.Windows.Forms.Label();
            this.lbPPEDSNListDefault = new System.Windows.Forms.Label();
            this.lbPortMappings = new System.Windows.Forms.Label();
            this.lbPortMappingsDefault = new System.Windows.Forms.Label();
            this.tbPPEDSNListDefault = new System.Windows.Forms.TextBox();
            this.tbPortMappingsDefault = new System.Windows.Forms.TextBox();
            this.ofdXLS = new System.Windows.Forms.OpenFileDialog();
            this.ofdTXT = new System.Windows.Forms.OpenFileDialog();
            this.tbPPEDSNList = new System.Windows.Forms.TextBox();
            this.tbPortMappings = new System.Windows.Forms.TextBox();
            this.btnOpenXLS = new System.Windows.Forms.Button();
            this.btnOpenTXT = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAllResetToDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTEMC
            // 
            this.lbTEMC.AutoSize = true;
            this.lbTEMC.Location = new System.Drawing.Point(141, 11);
            this.lbTEMC.Name = "lbTEMC";
            this.lbTEMC.Size = new System.Drawing.Size(68, 13);
            this.lbTEMC.TabIndex = 14;
            this.lbTEMC.Text = "TEMC URL: ";
            // 
            // tbTEMC
            // 
            this.tbTEMC.Location = new System.Drawing.Point(209, 7);
            this.tbTEMC.Name = "tbTEMC";
            this.tbTEMC.Size = new System.Drawing.Size(510, 20);
            this.tbTEMC.TabIndex = 3;
            // 
            // lbTEMCDefault
            // 
            this.lbTEMCDefault.AutoSize = true;
            this.lbTEMCDefault.Location = new System.Drawing.Point(162, 30);
            this.lbTEMCDefault.Name = "lbTEMCDefault";
            this.lbTEMCDefault.Size = new System.Drawing.Size(47, 13);
            this.lbTEMCDefault.TabIndex = 2;
            this.lbTEMCDefault.Text = "Default: ";
            // 
            // tbTEMCDefault
            // 
            this.tbTEMCDefault.Enabled = false;
            this.tbTEMCDefault.Location = new System.Drawing.Point(209, 26);
            this.tbTEMCDefault.Name = "tbTEMCDefault";
            this.tbTEMCDefault.Size = new System.Drawing.Size(510, 20);
            this.tbTEMCDefault.TabIndex = 4;
            this.tbTEMCDefault.Text = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";
            // 
            // lbPPEDSNList
            // 
            this.lbPPEDSNList.AutoSize = true;
            this.lbPPEDSNList.Location = new System.Drawing.Point(109, 65);
            this.lbPPEDSNList.Name = "lbPPEDSNList";
            this.lbPPEDSNList.Size = new System.Drawing.Size(100, 13);
            this.lbPPEDSNList.TabIndex = 4;
            this.lbPPEDSNList.Text = "PPE_DSN_List.xls: ";
            // 
            // lbPPEDSNListDefault
            // 
            this.lbPPEDSNListDefault.AutoSize = true;
            this.lbPPEDSNListDefault.Location = new System.Drawing.Point(162, 85);
            this.lbPPEDSNListDefault.Name = "lbPPEDSNListDefault";
            this.lbPPEDSNListDefault.Size = new System.Drawing.Size(47, 13);
            this.lbPPEDSNListDefault.TabIndex = 5;
            this.lbPPEDSNListDefault.Text = "Default: ";
            // 
            // lbPortMappings
            // 
            this.lbPortMappings.AutoSize = true;
            this.lbPortMappings.Location = new System.Drawing.Point(13, 120);
            this.lbPortMappings.Name = "lbPortMappings";
            this.lbPortMappings.Size = new System.Drawing.Size(196, 13);
            this.lbPortMappings.TabIndex = 6;
            this.lbPortMappings.Text = "WingatePortMappingsForRTT_PPE.txt: ";
            // 
            // lbPortMappingsDefault
            // 
            this.lbPortMappingsDefault.AutoSize = true;
            this.lbPortMappingsDefault.Location = new System.Drawing.Point(162, 141);
            this.lbPortMappingsDefault.Name = "lbPortMappingsDefault";
            this.lbPortMappingsDefault.Size = new System.Drawing.Size(47, 13);
            this.lbPortMappingsDefault.TabIndex = 7;
            this.lbPortMappingsDefault.Text = "Default: ";
            // 
            // tbPPEDSNListDefault
            // 
            this.tbPPEDSNListDefault.Enabled = false;
            this.tbPPEDSNListDefault.Location = new System.Drawing.Point(209, 81);
            this.tbPPEDSNListDefault.Name = "tbPPEDSNListDefault";
            this.tbPPEDSNListDefault.Size = new System.Drawing.Size(510, 20);
            this.tbPPEDSNListDefault.TabIndex = 6;
            this.tbPPEDSNListDefault.Text = "C:\\Program Files (x86)\\DatabaseSelectorSetup\\PPE_DSN_List.xls";
            // 
            // tbPortMappingsDefault
            // 
            this.tbPortMappingsDefault.Enabled = false;
            this.tbPortMappingsDefault.Location = new System.Drawing.Point(209, 137);
            this.tbPortMappingsDefault.Name = "tbPortMappingsDefault";
            this.tbPortMappingsDefault.Size = new System.Drawing.Size(510, 20);
            this.tbPortMappingsDefault.TabIndex = 8;
            this.tbPortMappingsDefault.Text = "C:\\Program Files (x86)\\DatabaseSelectorSetup\\WingatePortMappingsForRTT_PPE.txt";
            // 
            // ofdXLS
            // 
            this.ofdXLS.FileName = "PPE_DSN_List.xls";
            this.ofdXLS.Filter = "Excel Files|*.xls";
            this.ofdXLS.Title = "Select PPE_DSN_List.xls";
            this.ofdXLS.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdXLS_FileOk);
            // 
            // ofdTXT
            // 
            this.ofdTXT.FileName = "WingatePortMappingsForRTT_PPE.txt";
            this.ofdTXT.Filter = "Text Files|*.txt";
            this.ofdTXT.Title = "Select WingatePortMappingsForRTT_PPE.txt";
            this.ofdTXT.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdTXT_FileOk);
            // 
            // tbPPEDSNList
            // 
            this.tbPPEDSNList.Location = new System.Drawing.Point(209, 61);
            this.tbPPEDSNList.Name = "tbPPEDSNList";
            this.tbPPEDSNList.Size = new System.Drawing.Size(435, 20);
            this.tbPPEDSNList.TabIndex = 5;
            // 
            // tbPortMappings
            // 
            this.tbPortMappings.Location = new System.Drawing.Point(209, 116);
            this.tbPortMappings.Name = "tbPortMappings";
            this.tbPortMappings.Size = new System.Drawing.Size(435, 20);
            this.tbPortMappings.TabIndex = 7;
            // 
            // btnOpenXLS
            // 
            this.btnOpenXLS.Location = new System.Drawing.Point(644, 59);
            this.btnOpenXLS.Name = "btnOpenXLS";
            this.btnOpenXLS.Size = new System.Drawing.Size(75, 23);
            this.btnOpenXLS.TabIndex = 9;
            this.btnOpenXLS.Text = "Browse...";
            this.btnOpenXLS.UseVisualStyleBackColor = true;
            this.btnOpenXLS.Click += new System.EventHandler(this.btnOpenXLS_Click);
            // 
            // btnOpenTXT
            // 
            this.btnOpenTXT.Location = new System.Drawing.Point(644, 113);
            this.btnOpenTXT.Name = "btnOpenTXT";
            this.btnOpenTXT.Size = new System.Drawing.Size(75, 23);
            this.btnOpenTXT.TabIndex = 10;
            this.btnOpenTXT.Text = "Browse...";
            this.btnOpenTXT.UseVisualStyleBackColor = true;
            this.btnOpenTXT.Click += new System.EventHandler(this.btnOpenTXT_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(209, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(284, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAllResetToDefault
            // 
            this.btnAllResetToDefault.Location = new System.Drawing.Point(359, 171);
            this.btnAllResetToDefault.Name = "btnAllResetToDefault";
            this.btnAllResetToDefault.Size = new System.Drawing.Size(165, 23);
            this.btnAllResetToDefault.TabIndex = 2;
            this.btnAllResetToDefault.Text = "Reset all to default values";
            this.btnAllResetToDefault.UseVisualStyleBackColor = true;
            this.btnAllResetToDefault.Click += new System.EventHandler(this.btnAllResetToDefault_Click);
            // 
            // Options
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(723, 198);
            this.Controls.Add(this.btnAllResetToDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpenTXT);
            this.Controls.Add(this.btnOpenXLS);
            this.Controls.Add(this.tbPortMappings);
            this.Controls.Add(this.tbPPEDSNList);
            this.Controls.Add(this.tbPortMappingsDefault);
            this.Controls.Add(this.tbPPEDSNListDefault);
            this.Controls.Add(this.lbPortMappingsDefault);
            this.Controls.Add(this.lbPortMappings);
            this.Controls.Add(this.lbPPEDSNListDefault);
            this.Controls.Add(this.lbPPEDSNList);
            this.Controls.Add(this.tbTEMCDefault);
            this.Controls.Add(this.lbTEMCDefault);
            this.Controls.Add(this.tbTEMC);
            this.Controls.Add(this.lbTEMC);
            this.KeyPreview = true;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Options_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTEMC;
        private System.Windows.Forms.TextBox tbTEMC;
        private System.Windows.Forms.Label lbTEMCDefault;
        private System.Windows.Forms.TextBox tbTEMCDefault;
        private System.Windows.Forms.Label lbPPEDSNList;
        private System.Windows.Forms.Label lbPPEDSNListDefault;
        private System.Windows.Forms.Label lbPortMappings;
        private System.Windows.Forms.Label lbPortMappingsDefault;
        private System.Windows.Forms.TextBox tbPPEDSNListDefault;
        private System.Windows.Forms.TextBox tbPortMappingsDefault;
        private System.Windows.Forms.OpenFileDialog ofdXLS;
        private System.Windows.Forms.OpenFileDialog ofdTXT;
        private System.Windows.Forms.TextBox tbPPEDSNList;
        private System.Windows.Forms.TextBox tbPortMappings;
        private System.Windows.Forms.Button btnOpenXLS;
        private System.Windows.Forms.Button btnOpenTXT;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private Index index;
        private System.Windows.Forms.Button btnAllResetToDefault;
    }
}