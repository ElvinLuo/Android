namespace SoftTestDesigner
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dgvRestriction = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.dgvConfigItem = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnGenerateCombination = new System.Windows.Forms.Button();
            this.btnCreateAssignment = new System.Windows.Forms.Button();
            this.btnCreateLabrun = new System.Windows.Forms.Button();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnOpenRestrictions = new System.Windows.Forms.Button();
            this.btnClearDataGridView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cbSelectAllConfigItems = new System.Windows.Forms.CheckBox();
            this.collapsibleSplitter4 = new NJFLib.Controls.CollapsibleSplitter();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnOpenConfigFile = new System.Windows.Forms.Button();
            this.btnCoveragesMultiplyBy10 = new System.Windows.Forms.Button();
            this.collapsibleSplitter2 = new NJFLib.Controls.CollapsibleSplitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cbSelectAllRestrictions = new System.Windows.Forms.CheckBox();
            this.collapsibleSplitter5 = new NJFLib.Controls.CollapsibleSplitter();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnMoveDownRestriction = new System.Windows.Forms.Button();
            this.btnMoveUpRestriction = new System.Windows.Forms.Button();
            this.btnSaveRestrictions = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnShowResultStatistics = new System.Windows.Forms.Button();
            this.btnOneClick = new System.Windows.Forms.Button();
            this.btnRemoveDuplicatedRows = new System.Windows.Forms.Button();
            this.btnApplyRestrictions = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.collapsibleSplitter1 = new NJFLib.Controls.CollapsibleSplitter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRestriction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigItem)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select a folder";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "Text file|*.txt";
            this.saveFileDialog.Title = "Specify file name";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // dgvRestriction
            // 
            this.dgvRestriction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRestriction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRestriction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column11,
            this.dataGridViewTextBoxColumn1,
            this.Column6});
            this.dgvRestriction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRestriction.Location = new System.Drawing.Point(0, 0);
            this.dgvRestriction.MultiSelect = false;
            this.dgvRestriction.Name = "dgvRestriction";
            this.dgvRestriction.RowTemplate.Height = 23;
            this.dgvRestriction.Size = new System.Drawing.Size(741, 221);
            this.dgvRestriction.TabIndex = 2;
            this.dgvRestriction.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRestriction_CellContentClick);
            this.dgvRestriction.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvRestriction_RowPostPaint);
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            this.Column5.Width = 21;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column11.HeaderText = "";
            this.Column11.Items.AddRange(new object[] {
            "Need to filter",
            "Need to contain"});
            this.Column11.Name = "Column11";
            this.Column11.Width = 110;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 154.5455F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Restrictions";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6.FillWeight = 45.45454F;
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.Text = "Remove";
            this.Column6.UseColumnTextForButtonValue = true;
            this.Column6.Width = 50;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.ColumnHeadersVisible = false;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.ShowCellErrors = false;
            this.dgvResult.ShowCellToolTips = false;
            this.dgvResult.ShowEditingIcon = false;
            this.dgvResult.ShowRowErrors = false;
            this.dgvResult.Size = new System.Drawing.Size(741, 281);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResult_RowPostPaint);
            this.dgvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvResult_KeyDown);
            // 
            // dgvConfigItem
            // 
            this.dgvConfigItem.AllowUserToOrderColumns = true;
            this.dgvConfigItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConfigItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfigItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column9,
            this.Column4,
            this.Column8});
            this.dgvConfigItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConfigItem.Location = new System.Drawing.Point(0, 0);
            this.dgvConfigItem.MultiSelect = false;
            this.dgvConfigItem.Name = "dgvConfigItem";
            this.dgvConfigItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvConfigItem.RowTemplate.Height = 23;
            this.dgvConfigItem.ShowCellErrors = false;
            this.dgvConfigItem.ShowCellToolTips = false;
            this.dgvConfigItem.ShowEditingIcon = false;
            this.dgvConfigItem.ShowRowErrors = false;
            this.dgvConfigItem.Size = new System.Drawing.Size(741, 188);
            this.dgvConfigItem.TabIndex = 4;
            this.dgvConfigItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConfigItem_CellContentClick);
            this.dgvConfigItem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConfigItem_CellValueChanged);
            this.dgvConfigItem.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvConfigItem_CurrentCellDirtyStateChanged);
            this.dgvConfigItem.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvConfigItem_RowPostPaint);
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column7.HeaderText = "";
            this.Column7.Name = "Column7";
            this.Column7.Width = 18;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 46.875F;
            this.Column1.HeaderText = "Config item";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 46.875F;
            this.Column2.HeaderText = "Config name";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 46.875F;
            this.Column3.HeaderText = "Available values";
            this.Column3.Name = "Column3";
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.HeaderText = "Random?";
            this.Column9.Name = "Column9";
            this.Column9.Width = 59;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 46.875F;
            this.Column4.HeaderText = "Coverages";
            this.Column4.Name = "Column4";
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column8.FillWeight = 312.5F;
            this.Column8.HeaderText = "";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.Text = "Remove";
            this.Column8.UseColumnTextForButtonValue = true;
            this.Column8.Width = 50;
            // 
            // btnGenerateCombination
            // 
            this.btnGenerateCombination.Location = new System.Drawing.Point(0, 25);
            this.btnGenerateCombination.Name = "btnGenerateCombination";
            this.btnGenerateCombination.Size = new System.Drawing.Size(148, 25);
            this.btnGenerateCombination.TabIndex = 18;
            this.btnGenerateCombination.Text = "Generate combination";
            this.btnGenerateCombination.UseVisualStyleBackColor = true;
            this.btnGenerateCombination.Click += new System.EventHandler(this.btnGenerateCombination_Click);
            // 
            // btnCreateAssignment
            // 
            this.btnCreateAssignment.Location = new System.Drawing.Point(0, 225);
            this.btnCreateAssignment.Name = "btnCreateAssignment";
            this.btnCreateAssignment.Size = new System.Drawing.Size(148, 25);
            this.btnCreateAssignment.TabIndex = 21;
            this.btnCreateAssignment.Text = "Create Assignment";
            this.btnCreateAssignment.UseVisualStyleBackColor = true;
            this.btnCreateAssignment.Click += new System.EventHandler(this.btnCreateAssignment_Click);
            // 
            // btnCreateLabrun
            // 
            this.btnCreateLabrun.Location = new System.Drawing.Point(0, 200);
            this.btnCreateLabrun.Name = "btnCreateLabrun";
            this.btnCreateLabrun.Size = new System.Drawing.Size(148, 25);
            this.btnCreateLabrun.TabIndex = 20;
            this.btnCreateLabrun.Text = "Create Labrun";
            this.btnCreateLabrun.UseVisualStyleBackColor = true;
            this.btnCreateLabrun.Click += new System.EventHandler(this.btnCreateLabrun_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(0, 125);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(148, 25);
            this.btnSelectFolder.TabIndex = 19;
            this.btnSelectFolder.Text = "Select a folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(0, 150);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(148, 25);
            this.btnCopy.TabIndex = 22;
            this.btnCopy.Text = "Copy to clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnOpenRestrictions
            // 
            this.btnOpenRestrictions.Location = new System.Drawing.Point(0, 0);
            this.btnOpenRestrictions.Name = "btnOpenRestrictions";
            this.btnOpenRestrictions.Size = new System.Drawing.Size(148, 25);
            this.btnOpenRestrictions.TabIndex = 24;
            this.btnOpenRestrictions.Text = "Open restrictions";
            this.btnOpenRestrictions.UseVisualStyleBackColor = true;
            this.btnOpenRestrictions.Click += new System.EventHandler(this.btnOpenRestrictions_Click);
            // 
            // btnClearDataGridView
            // 
            this.btnClearDataGridView.Location = new System.Drawing.Point(0, 176);
            this.btnClearDataGridView.Name = "btnClearDataGridView";
            this.btnClearDataGridView.Size = new System.Drawing.Size(148, 25);
            this.btnClearDataGridView.TabIndex = 23;
            this.btnClearDataGridView.Text = "Clear";
            this.btnClearDataGridView.UseVisualStyleBackColor = true;
            this.btnClearDataGridView.Click += new System.EventHandler(this.btnClearDataGridView_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.collapsibleSplitter2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 417);
            this.panel1.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.collapsibleSplitter4);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(897, 188);
            this.panel4.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel8.Controls.Add(this.cbSelectAllConfigItems);
            this.panel8.Controls.Add(this.dgvConfigItem);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(741, 188);
            this.panel8.TabIndex = 10;
            // 
            // cbSelectAllConfigItems
            // 
            this.cbSelectAllConfigItems.AutoSize = true;
            this.cbSelectAllConfigItems.BackColor = System.Drawing.SystemColors.Control;
            this.cbSelectAllConfigItems.Location = new System.Drawing.Point(44, 3);
            this.cbSelectAllConfigItems.Name = "cbSelectAllConfigItems";
            this.cbSelectAllConfigItems.Size = new System.Drawing.Size(15, 14);
            this.cbSelectAllConfigItems.TabIndex = 8;
            this.cbSelectAllConfigItems.UseVisualStyleBackColor = false;
            this.cbSelectAllConfigItems.CheckedChanged += new System.EventHandler(this.cbSelectAllConfigItems_CheckedChanged);
            // 
            // collapsibleSplitter4
            // 
            this.collapsibleSplitter4.AnimationDelay = 20;
            this.collapsibleSplitter4.AnimationStep = 20;
            this.collapsibleSplitter4.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.collapsibleSplitter4.ControlToHide = this.panel7;
            this.collapsibleSplitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter4.ExpandParentForm = false;
            this.collapsibleSplitter4.Location = new System.Drawing.Point(741, 0);
            this.collapsibleSplitter4.Name = "collapsibleSplitter4";
            this.collapsibleSplitter4.TabIndex = 9;
            this.collapsibleSplitter4.TabStop = false;
            this.collapsibleSplitter4.UseAnimations = false;
            this.collapsibleSplitter4.VisualStyle = NJFLib.Controls.VisualStyles.XP;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnMoveDown);
            this.panel7.Controls.Add(this.btnMoveUp);
            this.panel7.Controls.Add(this.btnSaveToFile);
            this.panel7.Controls.Add(this.btnOpenConfigFile);
            this.panel7.Controls.Add(this.btnCoveragesMultiplyBy10);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(749, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(148, 188);
            this.panel7.TabIndex = 8;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(0, 100);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(148, 25);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "Move down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(0, 75);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(148, 25);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "Move up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(0, 50);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(148, 25);
            this.btnSaveToFile.TabIndex = 2;
            this.btnSaveToFile.Text = "Save to file";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnOpenConfigFile
            // 
            this.btnOpenConfigFile.Location = new System.Drawing.Point(0, 25);
            this.btnOpenConfigFile.Name = "btnOpenConfigFile";
            this.btnOpenConfigFile.Size = new System.Drawing.Size(148, 25);
            this.btnOpenConfigFile.TabIndex = 1;
            this.btnOpenConfigFile.Text = "Open config file";
            this.btnOpenConfigFile.UseVisualStyleBackColor = true;
            this.btnOpenConfigFile.Click += new System.EventHandler(this.btnOpenConfigFile_Click);
            // 
            // btnCoveragesMultiplyBy10
            // 
            this.btnCoveragesMultiplyBy10.Location = new System.Drawing.Point(0, 0);
            this.btnCoveragesMultiplyBy10.Name = "btnCoveragesMultiplyBy10";
            this.btnCoveragesMultiplyBy10.Size = new System.Drawing.Size(148, 25);
            this.btnCoveragesMultiplyBy10.TabIndex = 0;
            this.btnCoveragesMultiplyBy10.Text = "Coverages X10";
            this.btnCoveragesMultiplyBy10.UseVisualStyleBackColor = true;
            this.btnCoveragesMultiplyBy10.Click += new System.EventHandler(this.btnCoveragesMultiplyBy10_Click);
            // 
            // collapsibleSplitter2
            // 
            this.collapsibleSplitter2.AnimationDelay = 20;
            this.collapsibleSplitter2.AnimationStep = 20;
            this.collapsibleSplitter2.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.collapsibleSplitter2.ControlToHide = this.panel3;
            this.collapsibleSplitter2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.collapsibleSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter2.ExpandParentForm = false;
            this.collapsibleSplitter2.Location = new System.Drawing.Point(0, 188);
            this.collapsibleSplitter2.Name = "collapsibleSplitter2";
            this.collapsibleSplitter2.TabIndex = 1;
            this.collapsibleSplitter2.TabStop = false;
            this.collapsibleSplitter2.UseAnimations = false;
            this.collapsibleSplitter2.VisualStyle = NJFLib.Controls.VisualStyles.XP;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.collapsibleSplitter5);
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 196);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(897, 221);
            this.panel3.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.cbSelectAllRestrictions);
            this.panel10.Controls.Add(this.dgvRestriction);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(741, 221);
            this.panel10.TabIndex = 9;
            // 
            // cbSelectAllRestrictions
            // 
            this.cbSelectAllRestrictions.AutoSize = true;
            this.cbSelectAllRestrictions.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbSelectAllRestrictions.Location = new System.Drawing.Point(45, 4);
            this.cbSelectAllRestrictions.Name = "cbSelectAllRestrictions";
            this.cbSelectAllRestrictions.Size = new System.Drawing.Size(15, 14);
            this.cbSelectAllRestrictions.TabIndex = 7;
            this.cbSelectAllRestrictions.UseVisualStyleBackColor = false;
            this.cbSelectAllRestrictions.CheckedChanged += new System.EventHandler(this.cbSelectAllRestrictions_CheckedChanged);
            // 
            // collapsibleSplitter5
            // 
            this.collapsibleSplitter5.AnimationDelay = 20;
            this.collapsibleSplitter5.AnimationStep = 20;
            this.collapsibleSplitter5.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.collapsibleSplitter5.ControlToHide = this.panel9;
            this.collapsibleSplitter5.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter5.ExpandParentForm = false;
            this.collapsibleSplitter5.Location = new System.Drawing.Point(741, 0);
            this.collapsibleSplitter5.Name = "collapsibleSplitter5";
            this.collapsibleSplitter5.TabIndex = 8;
            this.collapsibleSplitter5.TabStop = false;
            this.collapsibleSplitter5.UseAnimations = false;
            this.collapsibleSplitter5.VisualStyle = NJFLib.Controls.VisualStyles.XP;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnMoveDownRestriction);
            this.panel9.Controls.Add(this.btnMoveUpRestriction);
            this.panel9.Controls.Add(this.btnOpenRestrictions);
            this.panel9.Controls.Add(this.btnSaveRestrictions);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(749, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(148, 221);
            this.panel9.TabIndex = 7;
            // 
            // btnMoveDownRestriction
            // 
            this.btnMoveDownRestriction.Location = new System.Drawing.Point(0, 75);
            this.btnMoveDownRestriction.Name = "btnMoveDownRestriction";
            this.btnMoveDownRestriction.Size = new System.Drawing.Size(148, 25);
            this.btnMoveDownRestriction.TabIndex = 27;
            this.btnMoveDownRestriction.Text = "Move down";
            this.btnMoveDownRestriction.UseVisualStyleBackColor = true;
            this.btnMoveDownRestriction.Click += new System.EventHandler(this.btnMoveDownRestriction_Click);
            // 
            // btnMoveUpRestriction
            // 
            this.btnMoveUpRestriction.Location = new System.Drawing.Point(0, 50);
            this.btnMoveUpRestriction.Name = "btnMoveUpRestriction";
            this.btnMoveUpRestriction.Size = new System.Drawing.Size(148, 25);
            this.btnMoveUpRestriction.TabIndex = 26;
            this.btnMoveUpRestriction.Text = "Move up";
            this.btnMoveUpRestriction.UseVisualStyleBackColor = true;
            this.btnMoveUpRestriction.Click += new System.EventHandler(this.btnMoveUpRestriction_Click);
            // 
            // btnSaveRestrictions
            // 
            this.btnSaveRestrictions.Location = new System.Drawing.Point(0, 25);
            this.btnSaveRestrictions.Name = "btnSaveRestrictions";
            this.btnSaveRestrictions.Size = new System.Drawing.Size(148, 25);
            this.btnSaveRestrictions.TabIndex = 25;
            this.btnSaveRestrictions.Text = "Save restrictions";
            this.btnSaveRestrictions.UseVisualStyleBackColor = true;
            this.btnSaveRestrictions.Click += new System.EventHandler(this.btnSaveRestrictions_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.collapsibleSplitter3);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 425);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 281);
            this.panel2.TabIndex = 27;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.dgvResult);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(741, 281);
            this.panel6.TabIndex = 2;
            // 
            // collapsibleSplitter3
            // 
            this.collapsibleSplitter3.AnimationDelay = 20;
            this.collapsibleSplitter3.AnimationStep = 20;
            this.collapsibleSplitter3.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter3.ControlToHide = this.panel5;
            this.collapsibleSplitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter3.ExpandParentForm = false;
            this.collapsibleSplitter3.Location = new System.Drawing.Point(741, 0);
            this.collapsibleSplitter3.Name = "collapsibleSplitter3";
            this.collapsibleSplitter3.TabIndex = 1;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.XP;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.btnShowResultStatistics);
            this.panel5.Controls.Add(this.btnOneClick);
            this.panel5.Controls.Add(this.btnRemoveDuplicatedRows);
            this.panel5.Controls.Add(this.btnApplyRestrictions);
            this.panel5.Controls.Add(this.btnGenerateCombination);
            this.panel5.Controls.Add(this.btnClearDataGridView);
            this.panel5.Controls.Add(this.btnCopy);
            this.panel5.Controls.Add(this.btnCreateAssignment);
            this.panel5.Controls.Add(this.btnSelectFolder);
            this.panel5.Controls.Add(this.btnCreateLabrun);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(749, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(148, 281);
            this.panel5.TabIndex = 0;
            // 
            // btnShowResultStatistics
            // 
            this.btnShowResultStatistics.Location = new System.Drawing.Point(0, 100);
            this.btnShowResultStatistics.Name = "btnShowResultStatistics";
            this.btnShowResultStatistics.Size = new System.Drawing.Size(148, 25);
            this.btnShowResultStatistics.TabIndex = 29;
            this.btnShowResultStatistics.Text = "Show result statistics";
            this.btnShowResultStatistics.UseVisualStyleBackColor = true;
            this.btnShowResultStatistics.Click += new System.EventHandler(this.btnShowResultStatistics_Click);
            // 
            // btnOneClick
            // 
            this.btnOneClick.Location = new System.Drawing.Point(0, 0);
            this.btnOneClick.Name = "btnOneClick";
            this.btnOneClick.Size = new System.Drawing.Size(148, 25);
            this.btnOneClick.TabIndex = 0;
            this.btnOneClick.Text = "One click to generate";
            this.btnOneClick.UseVisualStyleBackColor = true;
            this.btnOneClick.Click += new System.EventHandler(this.btnOneClick_Click);
            // 
            // btnRemoveDuplicatedRows
            // 
            this.btnRemoveDuplicatedRows.Location = new System.Drawing.Point(0, 75);
            this.btnRemoveDuplicatedRows.Name = "btnRemoveDuplicatedRows";
            this.btnRemoveDuplicatedRows.Size = new System.Drawing.Size(148, 25);
            this.btnRemoveDuplicatedRows.TabIndex = 27;
            this.btnRemoveDuplicatedRows.Text = "Remove duplicated rows";
            this.btnRemoveDuplicatedRows.UseVisualStyleBackColor = true;
            this.btnRemoveDuplicatedRows.Click += new System.EventHandler(this.btnRemoveDuplicatedRows_Click);
            // 
            // btnApplyRestrictions
            // 
            this.btnApplyRestrictions.Location = new System.Drawing.Point(0, 50);
            this.btnApplyRestrictions.Name = "btnApplyRestrictions";
            this.btnApplyRestrictions.Size = new System.Drawing.Size(148, 25);
            this.btnApplyRestrictions.TabIndex = 26;
            this.btnApplyRestrictions.Text = "Apply restrictions";
            this.btnApplyRestrictions.UseVisualStyleBackColor = true;
            this.btnApplyRestrictions.Click += new System.EventHandler(this.btnApplyRestrictions_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Text file|*.txt";
            this.openFileDialog.Title = "Choose a file";
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter1.ControlToHide = this.panel1;
            this.collapsibleSplitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.collapsibleSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(0, 417);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 26;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // SoftTestDesigner
            // 
            this.AcceptButton = this.btnOneClick;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 706);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.panel1);
            this.Name = "SoftTestDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soft test designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SoftTestDesigner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRestriction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigItem)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        SoftTestConfiguration sc;
        UIProcessor uiProcessor;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.DataGridView dgvRestriction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridView dgvConfigItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.Button btnGenerateCombination;
        private System.Windows.Forms.Button btnCreateAssignment;
        private System.Windows.Forms.Button btnCreateLabrun;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnOpenRestrictions;
        private System.Windows.Forms.Button btnClearDataGridView;
        private System.Windows.Forms.Panel panel1;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSaveRestrictions;
        private System.Windows.Forms.Button btnApplyRestrictions;
        private System.Windows.Forms.Button btnRemoveDuplicatedRows;
        private System.Windows.Forms.Button btnOneClick;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox cbSelectAllConfigItems;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnOpenConfigFile;
        private System.Windows.Forms.Button btnCoveragesMultiplyBy10;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox cbSelectAllRestrictions;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter5;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnShowResultStatistics;
        private System.Windows.Forms.Button btnMoveDownRestriction;
        private System.Windows.Forms.Button btnMoveUpRestriction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

