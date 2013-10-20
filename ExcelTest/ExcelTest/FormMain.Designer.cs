namespace ExcelTest
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRange = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSelect = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.checkBoxHeader = new System.Windows.Forms.CheckBox();
            this.checkBoxIntermixed = new System.Windows.Forms.CheckBox();
            this.comboBoxSheet = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxConnectionString = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Enabled = false;
            this.buttonExecute.Location = new System.Drawing.Point(12, 140);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 0;
            this.buttonExecute.Text = "&Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(885, 466);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sheet name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Range (optional):";
            // 
            // textBoxRange
            // 
            this.textBoxRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRange.Location = new System.Drawing.Point(118, 88);
            this.textBoxRange.Name = "textBoxRange";
            this.textBoxRange.Size = new System.Drawing.Size(779, 20);
            this.textBoxRange.TabIndex = 4;
            this.textBoxRange.TextChanged += new System.EventHandler(this.textBoxRange_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SELECT statement:";
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelect.Location = new System.Drawing.Point(118, 114);
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(779, 20);
            this.textBoxSelect.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.Filter = "Excel 2007 XML (*.xlsx)|*.xlsx|Excel 2007 Binary (*.xlsb)|*.xlsb|Excel 2007 Macro" +
                "-enabled (*.xlsm)|*.xlsm|Excel 97/2000/2003 (*.xls)|*.xls|Excel 5.0/95 (*.xls)|*" +
                ".xls";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.Title = "Open Excel Worksheet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Source file:";
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilename.Location = new System.Drawing.Point(159, 12);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.ReadOnly = true;
            this.textBoxFilename.Size = new System.Drawing.Size(738, 20);
            this.textBoxFilename.TabIndex = 8;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(78, 10);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 10;
            this.buttonOpen.Text = "&Open...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // checkBoxHeader
            // 
            this.checkBoxHeader.AutoSize = true;
            this.checkBoxHeader.Checked = true;
            this.checkBoxHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeader.Location = new System.Drawing.Point(12, 39);
            this.checkBoxHeader.Name = "checkBoxHeader";
            this.checkBoxHeader.Size = new System.Drawing.Size(140, 17);
            this.checkBoxHeader.TabIndex = 11;
            this.checkBoxHeader.Text = "Treat first row as header";
            this.checkBoxHeader.UseVisualStyleBackColor = true;
            // 
            // checkBoxIntermixed
            // 
            this.checkBoxIntermixed.AutoSize = true;
            this.checkBoxIntermixed.Checked = true;
            this.checkBoxIntermixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIntermixed.Location = new System.Drawing.Point(158, 39);
            this.checkBoxIntermixed.Name = "checkBoxIntermixed";
            this.checkBoxIntermixed.Size = new System.Drawing.Size(135, 17);
            this.checkBoxIntermixed.TabIndex = 12;
            this.checkBoxIntermixed.Text = "Treat intermixed as text";
            this.checkBoxIntermixed.UseVisualStyleBackColor = true;
            // 
            // comboBoxSheet
            // 
            this.comboBoxSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSheet.FormattingEnabled = true;
            this.comboBoxSheet.Location = new System.Drawing.Point(118, 61);
            this.comboBoxSheet.Name = "comboBoxSheet";
            this.comboBoxSheet.Size = new System.Drawing.Size(779, 21);
            this.comboBoxSheet.TabIndex = 13;
            this.comboBoxSheet.SelectedIndexChanged += new System.EventHandler(this.comboBoxSheet_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Connection string used:";
            // 
            // textBoxConnectionString
            // 
            this.textBoxConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConnectionString.Location = new System.Drawing.Point(217, 140);
            this.textBoxConnectionString.Name = "textBoxConnectionString";
            this.textBoxConnectionString.ReadOnly = true;
            this.textBoxConnectionString.Size = new System.Drawing.Size(680, 20);
            this.textBoxConnectionString.TabIndex = 15;
            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 647);
            this.Controls.Add(this.textBoxConnectionString);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSheet);
            this.Controls.Add(this.checkBoxIntermixed);
            this.Controls.Add(this.checkBoxHeader);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxFilename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonExecute);
            this.Name = "FormMain";
            this.Text = "Excel via ADO.NET";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSelect;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.CheckBox checkBoxHeader;
        private System.Windows.Forms.CheckBox checkBoxIntermixed;
        private System.Windows.Forms.ComboBox comboBoxSheet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxConnectionString;
    }
}

