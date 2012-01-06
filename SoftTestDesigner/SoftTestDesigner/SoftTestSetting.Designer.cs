using System.Collections.Generic;
namespace SoftTestDesigner
{
    partial class SoftTestSetting
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
            this.lblTestTeam = new System.Windows.Forms.Label();
            this.tbTestTeam = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.lblRisktier = new System.Windows.Forms.Label();
            this.tbRisktier = new System.Windows.Forms.TextBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.tbMethod = new System.Windows.Forms.TextBox();
            this.lblLOBMaskName = new System.Windows.Forms.Label();
            this.tbLOBMaskName = new System.Windows.Forms.TextBox();
            this.lblEnvironmentType = new System.Windows.Forms.Label();
            this.tbEnvironmentType = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTestTeam
            // 
            this.lblTestTeam.AutoSize = true;
            this.lblTestTeam.Location = new System.Drawing.Point(54, 10);
            this.lblTestTeam.Name = "lblTestTeam";
            this.lblTestTeam.Size = new System.Drawing.Size(54, 13);
            this.lblTestTeam.TabIndex = 0;
            this.lblTestTeam.Text = "Test team";
            // 
            // tbTestTeam
            // 
            this.tbTestTeam.Location = new System.Drawing.Point(121, 10);
            this.tbTestTeam.Name = "tbTestTeam";
            this.tbTestTeam.Size = new System.Drawing.Size(339, 20);
            this.tbTestTeam.TabIndex = 1;
            this.tbTestTeam.Text = "Lodging Inventory Systems";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(60, 33);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(121, 33);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(339, 20);
            this.tbCategory.TabIndex = 3;
            this.tbCategory.Text = "Regression";
            // 
            // lblRisktier
            // 
            this.lblRisktier.AutoSize = true;
            this.lblRisktier.Location = new System.Drawing.Point(60, 55);
            this.lblRisktier.Name = "lblRisktier";
            this.lblRisktier.Size = new System.Drawing.Size(42, 13);
            this.lblRisktier.TabIndex = 4;
            this.lblRisktier.Text = "Risktier";
            // 
            // tbRisktier
            // 
            this.tbRisktier.Location = new System.Drawing.Point(121, 55);
            this.tbRisktier.Name = "tbRisktier";
            this.tbRisktier.Size = new System.Drawing.Size(339, 20);
            this.tbRisktier.TabIndex = 5;
            this.tbRisktier.Text = "2";
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(72, 78);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(43, 13);
            this.lblMethod.TabIndex = 6;
            this.lblMethod.Text = "Method";
            // 
            // tbMethod
            // 
            this.tbMethod.Location = new System.Drawing.Point(121, 78);
            this.tbMethod.Name = "tbMethod";
            this.tbMethod.Size = new System.Drawing.Size(339, 20);
            this.tbMethod.TabIndex = 7;
            this.tbMethod.Text = "Expedia.Automation.Test.Hotels.BMC.SingleRatePlanConversionUI.CheckUI";
            // 
            // lblLOBMaskName
            // 
            this.lblLOBMaskName.AutoSize = true;
            this.lblLOBMaskName.Location = new System.Drawing.Point(36, 101);
            this.lblLOBMaskName.Name = "lblLOBMaskName";
            this.lblLOBMaskName.Size = new System.Drawing.Size(83, 13);
            this.lblLOBMaskName.TabIndex = 8;
            this.lblLOBMaskName.Text = "LOBMask name";
            // 
            // tbLOBMaskName
            // 
            this.tbLOBMaskName.Location = new System.Drawing.Point(121, 101);
            this.tbLOBMaskName.Name = "tbLOBMaskName";
            this.tbLOBMaskName.Size = new System.Drawing.Size(339, 20);
            this.tbLOBMaskName.TabIndex = 9;
            this.tbLOBMaskName.Text = "Hotel";
            // 
            // lblEnvironmentType
            // 
            this.lblEnvironmentType.AutoSize = true;
            this.lblEnvironmentType.Location = new System.Drawing.Point(12, 124);
            this.lblEnvironmentType.Name = "lblEnvironmentType";
            this.lblEnvironmentType.Size = new System.Drawing.Size(89, 13);
            this.lblEnvironmentType.TabIndex = 10;
            this.lblEnvironmentType.Text = "Environment type";
            // 
            // tbEnvironmentType
            // 
            this.tbEnvironmentType.Location = new System.Drawing.Point(121, 124);
            this.tbEnvironmentType.Name = "tbEnvironmentType";
            this.tbEnvironmentType.Size = new System.Drawing.Size(339, 20);
            this.tbEnvironmentType.TabIndex = 11;
            this.tbEnvironmentType.Text = "Lab";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(304, 155);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 25);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(385, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SoftTestSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 186);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbEnvironmentType);
            this.Controls.Add(this.lblEnvironmentType);
            this.Controls.Add(this.tbLOBMaskName);
            this.Controls.Add(this.lblLOBMaskName);
            this.Controls.Add(this.tbMethod);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.tbRisktier);
            this.Controls.Add(this.lblRisktier);
            this.Controls.Add(this.tbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.tbTestTeam);
            this.Controls.Add(this.lblTestTeam);
            this.Name = "SoftTestSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SoftTestSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestTeam;
        private System.Windows.Forms.TextBox tbTestTeam;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.Label lblRisktier;
        private System.Windows.Forms.TextBox tbRisktier;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.TextBox tbMethod;
        private System.Windows.Forms.Label lblLOBMaskName;
        private System.Windows.Forms.TextBox tbLOBMaskName;
        private System.Windows.Forms.Label lblEnvironmentType;
        private System.Windows.Forms.TextBox tbEnvironmentType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;

        private List<string> SoftTestNameList { get; set; }
        private List<string> ItemNameList { get; set; }
        private List<string[]> ValueArrayList { get; set; }
    }
}