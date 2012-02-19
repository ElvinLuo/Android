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
            this.tbMethod = new System.Windows.Forms.TextBox();
            this.lblLOBMaskName = new System.Windows.Forms.Label();
            this.tbLOBMaskName = new System.Windows.Forms.TextBox();
            this.lblEnvironmentType = new System.Windows.Forms.Label();
            this.tbEnvironmentType = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbOverrideMethod = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTestTeam
            // 
            this.lblTestTeam.AutoSize = true;
            this.lblTestTeam.Location = new System.Drawing.Point(74, 12);
            this.lblTestTeam.Name = "lblTestTeam";
            this.lblTestTeam.Size = new System.Drawing.Size(65, 12);
            this.lblTestTeam.TabIndex = 0;
            this.lblTestTeam.Text = "Test team:";
            this.lblTestTeam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTestTeam
            // 
            this.tbTestTeam.Location = new System.Drawing.Point(139, 8);
            this.tbTestTeam.Name = "tbTestTeam";
            this.tbTestTeam.Size = new System.Drawing.Size(526, 21);
            this.tbTestTeam.TabIndex = 1;
            this.tbTestTeam.Text = "Lodging Inventory Systems";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(80, 33);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(59, 12);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(139, 29);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(526, 21);
            this.tbCategory.TabIndex = 3;
            this.tbCategory.Text = "Regression";
            // 
            // lblRisktier
            // 
            this.lblRisktier.AutoSize = true;
            this.lblRisktier.Location = new System.Drawing.Point(80, 54);
            this.lblRisktier.Name = "lblRisktier";
            this.lblRisktier.Size = new System.Drawing.Size(59, 12);
            this.lblRisktier.TabIndex = 4;
            this.lblRisktier.Text = "Risktier:";
            this.lblRisktier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbRisktier
            // 
            this.tbRisktier.Location = new System.Drawing.Point(139, 50);
            this.tbRisktier.Name = "tbRisktier";
            this.tbRisktier.Size = new System.Drawing.Size(526, 21);
            this.tbRisktier.TabIndex = 5;
            this.tbRisktier.Text = "2";
            // 
            // tbMethod
            // 
            this.tbMethod.Location = new System.Drawing.Point(139, 113);
            this.tbMethod.Name = "tbMethod";
            this.tbMethod.Size = new System.Drawing.Size(526, 21);
            this.tbMethod.TabIndex = 7;
            this.tbMethod.Text = "Expedia.Automation.Test.Hotels.BMC.SingleRatePlanConversionUI.CheckUI";
            // 
            // lblLOBMaskName
            // 
            this.lblLOBMaskName.AutoSize = true;
            this.lblLOBMaskName.Location = new System.Drawing.Point(56, 75);
            this.lblLOBMaskName.Name = "lblLOBMaskName";
            this.lblLOBMaskName.Size = new System.Drawing.Size(83, 12);
            this.lblLOBMaskName.TabIndex = 8;
            this.lblLOBMaskName.Text = "LOBMask name:";
            this.lblLOBMaskName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbLOBMaskName
            // 
            this.tbLOBMaskName.Location = new System.Drawing.Point(139, 71);
            this.tbLOBMaskName.Name = "tbLOBMaskName";
            this.tbLOBMaskName.Size = new System.Drawing.Size(526, 21);
            this.tbLOBMaskName.TabIndex = 9;
            this.tbLOBMaskName.Text = "Hotel";
            // 
            // lblEnvironmentType
            // 
            this.lblEnvironmentType.AutoSize = true;
            this.lblEnvironmentType.Location = new System.Drawing.Point(32, 96);
            this.lblEnvironmentType.Name = "lblEnvironmentType";
            this.lblEnvironmentType.Size = new System.Drawing.Size(107, 12);
            this.lblEnvironmentType.TabIndex = 10;
            this.lblEnvironmentType.Text = "Environment type:";
            this.lblEnvironmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbEnvironmentType
            // 
            this.tbEnvironmentType.Location = new System.Drawing.Point(139, 92);
            this.tbEnvironmentType.Name = "tbEnvironmentType";
            this.tbEnvironmentType.Size = new System.Drawing.Size(526, 21);
            this.tbEnvironmentType.TabIndex = 11;
            this.tbEnvironmentType.Text = "Lab";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(509, 140);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(590, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbOverrideMethod
            // 
            this.cbOverrideMethod.AutoSize = true;
            this.cbOverrideMethod.Location = new System.Drawing.Point(16, 115);
            this.cbOverrideMethod.Name = "cbOverrideMethod";
            this.cbOverrideMethod.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbOverrideMethod.Size = new System.Drawing.Size(114, 16);
            this.cbOverrideMethod.TabIndex = 14;
            this.cbOverrideMethod.Text = "Override method";
            this.cbOverrideMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbOverrideMethod.UseVisualStyleBackColor = true;
            this.cbOverrideMethod.CheckedChanged += new System.EventHandler(this.cbOverrideMethod_CheckedChanged);
            // 
            // SoftTestSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 172);
            this.Controls.Add(this.cbOverrideMethod);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbEnvironmentType);
            this.Controls.Add(this.lblEnvironmentType);
            this.Controls.Add(this.tbLOBMaskName);
            this.Controls.Add(this.lblLOBMaskName);
            this.Controls.Add(this.tbMethod);
            this.Controls.Add(this.tbRisktier);
            this.Controls.Add(this.lblRisktier);
            this.Controls.Add(this.tbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.tbTestTeam);
            this.Controls.Add(this.lblTestTeam);
            this.Name = "SoftTestSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SoftTestSetting";
            this.Load += new System.EventHandler(this.SoftTestSetting_Load);
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
        private System.Windows.Forms.TextBox tbMethod;
        private System.Windows.Forms.Label lblLOBMaskName;
        private System.Windows.Forms.TextBox tbLOBMaskName;
        private System.Windows.Forms.Label lblEnvironmentType;
        private System.Windows.Forms.TextBox tbEnvironmentType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbOverrideMethod;

        private List<string> SoftTestNameList { get; set; }
        private List<string> ItemNameList { get; set; }
        private List<string[]> ValueArrayList { get; set; }
    }
}