namespace SoftCaseGenerator
{
    partial class SoftTestDesigner
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "PricingModel",
            "PDP./OBP.",
            "PDP/OBP",
            "80/20"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("PricingModel=OBP AND LOSEnabled=True");
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.configItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coverages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.restrictions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.configItem,
            this.configName,
            this.configValue,
            this.coverages});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(3, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(415, 207);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // listView2
            // 
            this.listView2.CheckBoxes = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.restrictions});
            listViewItem2.StateImageIndex = 0;
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView2.LabelEdit = true;
            this.listView2.Location = new System.Drawing.Point(3, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(419, 207);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(844, 284);
            this.dataGridView1.TabIndex = 2;
            // 
            // configItem
            // 
            this.configItem.Text = "Config Item";
            this.configItem.Width = 104;
            // 
            // configValue
            // 
            this.configValue.Text = "Available values";
            this.configValue.Width = 117;
            // 
            // coverages
            // 
            this.coverages.Text = "Coverages";
            this.coverages.Width = 93;
            // 
            // restrictions
            // 
            this.restrictions.Text = "Restrictions";
            this.restrictions.Width = 242;
            // 
            // configName
            // 
            this.configName.Text = "Config Name";
            this.configName.Width = 87;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(13, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(850, 504);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listView2);
            this.splitContainer2.Size = new System.Drawing.Size(850, 210);
            this.splitContainer2.SplitterDistance = 421;
            this.splitContainer2.TabIndex = 0;
            // 
            // SoftTestDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 538);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SoftTestDesigner";
            this.Text = "Generate soft case";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader configItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ColumnHeader configValue;
        private System.Windows.Forms.ColumnHeader coverages;
        private System.Windows.Forms.ColumnHeader restrictions;
        private System.Windows.Forms.ColumnHeader configName;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}

