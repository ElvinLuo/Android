using System.Collections.Generic;
namespace SoftTestDesigner
{
    partial class TestRunSetting
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
            this.lblTestRunName = new System.Windows.Forms.Label();
            this.tbTestRunName = new System.Windows.Forms.TextBox();
            this.lblManagerName = new System.Windows.Forms.Label();
            this.tbManagerName = new System.Windows.Forms.TextBox();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.tbBranchName = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.lblMethodName = new System.Windows.Forms.Label();
            this.tbMethodName = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTestRunName
            // 
            this.lblTestRunName.AutoSize = true;
            this.lblTestRunName.Location = new System.Drawing.Point(23, 13);
            this.lblTestRunName.Name = "lblTestRunName";
            this.lblTestRunName.Size = new System.Drawing.Size(89, 12);
            this.lblTestRunName.TabIndex = 0;
            this.lblTestRunName.Text = "Test run name:";
            this.lblTestRunName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTestRunName
            // 
            this.tbTestRunName.Location = new System.Drawing.Point(112, 9);
            this.tbTestRunName.Name = "tbTestRunName";
            this.tbTestRunName.Size = new System.Drawing.Size(562, 21);
            this.tbTestRunName.TabIndex = 1;
            this.tbTestRunName.Text = "ULP_CoreAdminPhase2_BulkEdit_LISQA8";
            // 
            // lblManagerName
            // 
            this.lblManagerName.AutoSize = true;
            this.lblManagerName.Location = new System.Drawing.Point(29, 34);
            this.lblManagerName.Name = "lblManagerName";
            this.lblManagerName.Size = new System.Drawing.Size(83, 12);
            this.lblManagerName.TabIndex = 2;
            this.lblManagerName.Text = "Manager name:";
            this.lblManagerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbManagerName
            // 
            this.tbManagerName.Location = new System.Drawing.Point(112, 30);
            this.tbManagerName.Name = "tbManagerName";
            this.tbManagerName.Size = new System.Drawing.Size(562, 21);
            this.tbManagerName.TabIndex = 3;
            this.tbManagerName.Text = "TFxIDXManager";
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Location = new System.Drawing.Point(35, 55);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(77, 12);
            this.lblBranchName.TabIndex = 4;
            this.lblBranchName.Text = "Branch name:";
            this.lblBranchName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBranchName
            // 
            this.tbBranchName.Location = new System.Drawing.Point(112, 51);
            this.tbBranchName.Name = "tbBranchName";
            this.tbBranchName.Size = new System.Drawing.Size(562, 21);
            this.tbBranchName.TabIndex = 5;
            this.tbBranchName.Text = "LFS000023";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(59, 76);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(53, 12);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(112, 72);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(562, 21);
            this.tbVersion.TabIndex = 7;
            this.tbVersion.Text = "2.0";
            // 
            // lblMethodName
            // 
            this.lblMethodName.AutoSize = true;
            this.lblMethodName.Location = new System.Drawing.Point(35, 97);
            this.lblMethodName.Name = "lblMethodName";
            this.lblMethodName.Size = new System.Drawing.Size(77, 12);
            this.lblMethodName.TabIndex = 8;
            this.lblMethodName.Text = "Method name:";
            this.lblMethodName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMethodName
            // 
            this.tbMethodName.Location = new System.Drawing.Point(112, 93);
            this.tbMethodName.Name = "tbMethodName";
            this.tbMethodName.Size = new System.Drawing.Size(562, 21);
            this.tbMethodName.TabIndex = 9;
            this.tbMethodName.Text = "Expedia.Automation.Test.Hotels.BMC.SingleRatePlanConversionUI.CheckUI";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(518, 121);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(599, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TestRunSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 156);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbMethodName);
            this.Controls.Add(this.lblMethodName);
            this.Controls.Add(this.tbVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.tbBranchName);
            this.Controls.Add(this.lblBranchName);
            this.Controls.Add(this.tbManagerName);
            this.Controls.Add(this.lblManagerName);
            this.Controls.Add(this.tbTestRunName);
            this.Controls.Add(this.lblTestRunName);
            this.Name = "TestRunSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TestRunSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestRunName;
        private System.Windows.Forms.TextBox tbTestRunName;
        private System.Windows.Forms.Label lblManagerName;
        private System.Windows.Forms.TextBox tbManagerName;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.TextBox tbBranchName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label lblMethodName;
        private System.Windows.Forms.TextBox tbMethodName;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;

        private List<string> SoftTestNameList { get; set; }
        private List<string> ItemNameList { get; set; }
        private List<string[]> ValueArrayList { get; set; }
    }
}