using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo.RegSvrEnum;

namespace DatabaseSelector
{
    partial class ServerInstanceSelector
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvDatabases = new System.Windows.Forms.ListView();
            this.HeaderDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderInstance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderAuthentication = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAuthentication = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cbConnectionType = new System.Windows.Forms.ComboBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.lblInstance = new System.Windows.Forms.Label();
            this.tbInstance = new System.Windows.Forms.TextBox();
            this.lvServers = new System.Windows.Forms.ListView();
            this.HeaderWebServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderTravelServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReloadGroups = new System.Windows.Forms.Button();
            this.btnSaveGroups = new System.Windows.Forms.Button();
            this.btnReloadServers = new System.Windows.Forms.Button();
            this.btnSaveServers = new System.Windows.Forms.Button();
            this.btnReloadDatabases = new System.Windows.Forms.Button();
            this.btnSaveDatabases = new System.Windows.Forms.Button();
            this.lblGroup = new System.Windows.Forms.Label();
            this.tbGroup = new System.Windows.Forms.TextBox();
            this.lblWebServer = new System.Windows.Forms.Label();
            this.tbWebServer = new System.Windows.Forms.TextBox();
            this.lblTravelServer = new System.Windows.Forms.Label();
            this.tbTravelServer = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.bgwUpdate = new System.ComponentModel.BackgroundWorker();
            this.lblGroupsUpdateDate = new System.Windows.Forms.Label();
            this.lblServersUpdateDate = new System.Windows.Forms.Label();
            this.lblDatabasesUpdateDate = new System.Windows.Forms.Label();
            this.pgbReloadServers = new System.Windows.Forms.ProgressBar();
            this.pgbReloadDatabases = new System.Windows.Forms.ProgressBar();
            this.tbGroupFilter = new System.Windows.Forms.TextBox();
            this.tbWebServerFilter = new System.Windows.Forms.TextBox();
            this.tbTravelServerFilter = new System.Windows.Forms.TextBox();
            this.tbDatabaseFilter = new System.Windows.Forms.TextBox();
            this.cbAutoOpenEditer = new System.Windows.Forms.CheckBox();
            this.btnReloadAll = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnClearAllSearchText = new System.Windows.Forms.Button();
            this.pgbReloadAllAndSave = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tcGroups = new System.Windows.Forms.TabControl();
            this.tpAllGroups = new System.Windows.Forms.TabPage();
            this.dgvAllGroups = new System.Windows.Forms.DataGridView();
            this.columnGroupNameInAllGroups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tpMyGroups = new System.Windows.Forms.TabPage();
            this.dgvMyGroups = new System.Windows.Forms.DataGridView();
            this.columnGroupNameInMyGroups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRemoveFromMyGroups = new System.Windows.Forms.DataGridViewLinkColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pgbReloadGroups = new System.Windows.Forms.ProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tcGroups.SuspendLayout();
            this.tpAllGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllGroups)).BeginInit();
            this.tpMyGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyGroups)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(59, 184);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(134, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvDatabases
            // 
            this.lvDatabases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderDatabase,
            this.HeaderServer,
            this.HeaderInstance,
            this.HeaderAuthentication,
            this.HeaderUsername,
            this.HeaderPassword});
            this.lvDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDatabases.FullRowSelect = true;
            this.lvDatabases.Location = new System.Drawing.Point(0, 0);
            this.lvDatabases.Name = "lvDatabases";
            this.lvDatabases.Size = new System.Drawing.Size(595, 384);
            this.lvDatabases.TabIndex = 200;
            this.lvDatabases.TabStop = false;
            this.lvDatabases.UseCompatibleStateImageBehavior = false;
            this.lvDatabases.View = System.Windows.Forms.View.Details;
            this.lvDatabases.SelectedIndexChanged += new System.EventHandler(this.lvDatabases_SelectedIndexChanged);
            this.lvDatabases.DoubleClick += new System.EventHandler(this.lvDatabases_DoubleClick);
            // 
            // HeaderDatabase
            // 
            this.HeaderDatabase.Text = "Database";
            // 
            // HeaderServer
            // 
            this.HeaderServer.Text = "Server";
            // 
            // HeaderInstance
            // 
            this.HeaderInstance.Text = "Instance";
            // 
            // HeaderAuthentication
            // 
            this.HeaderAuthentication.Text = "Authentication";
            this.HeaderAuthentication.Width = 80;
            // 
            // HeaderUsername
            // 
            this.HeaderUsername.Text = "User name";
            // 
            // HeaderPassword
            // 
            this.HeaderPassword.Text = "Password";
            // 
            // lblAuthentication
            // 
            this.lblAuthentication.AutoSize = true;
            this.lblAuthentication.Location = new System.Drawing.Point(5, 107);
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(78, 13);
            this.lblAuthentication.TabIndex = 200;
            this.lblAuthentication.Text = "Authentication:";
            this.lblAuthentication.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(20, 128);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 13);
            this.lblUserName.TabIndex = 200;
            this.lblUserName.Text = "User Name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(27, 148);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 200;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbConnectionType
            // 
            this.cbConnectionType.FormattingEnabled = true;
            this.cbConnectionType.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cbConnectionType.Location = new System.Drawing.Point(83, 103);
            this.cbConnectionType.Name = "cbConnectionType";
            this.cbConnectionType.Size = new System.Drawing.Size(201, 21);
            this.cbConnectionType.TabIndex = 5;
            this.cbConnectionType.Text = "Windows Authentication";
            this.cbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cbConnectionType_SelectedIndexChanged);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(83, 124);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(201, 20);
            this.tbUserName.TabIndex = 6;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(83, 144);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(201, 20);
            this.tbPassword.TabIndex = 7;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(27, 67);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 13);
            this.lblDatabase.TabIndex = 200;
            this.lblDatabase.Text = "Database:";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Enabled = false;
            this.tbDatabase.Location = new System.Drawing.Point(83, 63);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(201, 20);
            this.tbDatabase.TabIndex = 3;
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(32, 87);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(51, 13);
            this.lblInstance.TabIndex = 200;
            this.lblInstance.Text = "Instance:";
            this.lblInstance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbInstance
            // 
            this.tbInstance.Enabled = false;
            this.tbInstance.Location = new System.Drawing.Point(83, 83);
            this.tbInstance.Name = "tbInstance";
            this.tbInstance.Size = new System.Drawing.Size(201, 20);
            this.tbInstance.TabIndex = 4;
            // 
            // lvServers
            // 
            this.lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderWebServer,
            this.HeaderTravelServer});
            this.lvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvServers.FullRowSelect = true;
            this.lvServers.Location = new System.Drawing.Point(0, 0);
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(290, 166);
            this.lvServers.TabIndex = 200;
            this.lvServers.TabStop = false;
            this.lvServers.Tag = "";
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            this.lvServers.SelectedIndexChanged += new System.EventHandler(this.lvServers_SelectedIndexChanged);
            // 
            // HeaderWebServer
            // 
            this.HeaderWebServer.Text = "Web server";
            this.HeaderWebServer.Width = 143;
            // 
            // HeaderTravelServer
            // 
            this.HeaderTravelServer.Text = "Travel server";
            this.HeaderTravelServer.Width = 143;
            // 
            // btnReloadGroups
            // 
            this.btnReloadGroups.Location = new System.Drawing.Point(0, 0);
            this.btnReloadGroups.Name = "btnReloadGroups";
            this.btnReloadGroups.Size = new System.Drawing.Size(101, 23);
            this.btnReloadGroups.TabIndex = 0;
            this.btnReloadGroups.Text = "Reload groups";
            this.btnReloadGroups.UseVisualStyleBackColor = true;
            this.btnReloadGroups.Click += new System.EventHandler(this.btnReloadGroups_Click);
            // 
            // btnSaveGroups
            // 
            this.btnSaveGroups.Location = new System.Drawing.Point(101, 0);
            this.btnSaveGroups.Name = "btnSaveGroups";
            this.btnSaveGroups.Size = new System.Drawing.Size(101, 23);
            this.btnSaveGroups.TabIndex = 1;
            this.btnSaveGroups.Text = "Save groups";
            this.btnSaveGroups.UseVisualStyleBackColor = true;
            this.btnSaveGroups.Click += new System.EventHandler(this.btnSaveGroups_Click);
            // 
            // btnReloadServers
            // 
            this.btnReloadServers.Location = new System.Drawing.Point(202, 0);
            this.btnReloadServers.Name = "btnReloadServers";
            this.btnReloadServers.Size = new System.Drawing.Size(145, 23);
            this.btnReloadServers.TabIndex = 2;
            this.btnReloadServers.Text = "Reload servers";
            this.btnReloadServers.UseVisualStyleBackColor = true;
            this.btnReloadServers.Click += new System.EventHandler(this.btnReloadServers_Click);
            // 
            // btnSaveServers
            // 
            this.btnSaveServers.Location = new System.Drawing.Point(347, 0);
            this.btnSaveServers.Name = "btnSaveServers";
            this.btnSaveServers.Size = new System.Drawing.Size(145, 23);
            this.btnSaveServers.TabIndex = 3;
            this.btnSaveServers.Text = "Save servers";
            this.btnSaveServers.UseVisualStyleBackColor = true;
            this.btnSaveServers.Click += new System.EventHandler(this.btnSaveServers_Click);
            // 
            // btnReloadDatabases
            // 
            this.btnReloadDatabases.Location = new System.Drawing.Point(492, 0);
            this.btnReloadDatabases.Name = "btnReloadDatabases";
            this.btnReloadDatabases.Size = new System.Drawing.Size(131, 23);
            this.btnReloadDatabases.TabIndex = 4;
            this.btnReloadDatabases.Text = "Reload databases";
            this.btnReloadDatabases.UseVisualStyleBackColor = true;
            this.btnReloadDatabases.Click += new System.EventHandler(this.btnReloadDatabases_Click);
            // 
            // btnSaveDatabases
            // 
            this.btnSaveDatabases.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveDatabases.Location = new System.Drawing.Point(623, 0);
            this.btnSaveDatabases.Name = "btnSaveDatabases";
            this.btnSaveDatabases.Size = new System.Drawing.Size(131, 23);
            this.btnSaveDatabases.TabIndex = 5;
            this.btnSaveDatabases.Text = "Save databases";
            this.btnSaveDatabases.UseVisualStyleBackColor = true;
            this.btnSaveDatabases.Click += new System.EventHandler(this.btnSaveDatabases_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(44, 7);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 200;
            this.lblGroup.Text = "Group:";
            // 
            // tbGroup
            // 
            this.tbGroup.Enabled = false;
            this.tbGroup.Location = new System.Drawing.Point(83, 3);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(201, 20);
            this.tbGroup.TabIndex = 0;
            // 
            // lblWebServer
            // 
            this.lblWebServer.AutoSize = true;
            this.lblWebServer.Location = new System.Drawing.Point(18, 27);
            this.lblWebServer.Name = "lblWebServer";
            this.lblWebServer.Size = new System.Drawing.Size(65, 13);
            this.lblWebServer.TabIndex = 200;
            this.lblWebServer.Text = "Web server:";
            // 
            // tbWebServer
            // 
            this.tbWebServer.Enabled = false;
            this.tbWebServer.Location = new System.Drawing.Point(83, 23);
            this.tbWebServer.Name = "tbWebServer";
            this.tbWebServer.Size = new System.Drawing.Size(201, 20);
            this.tbWebServer.TabIndex = 1;
            // 
            // lblTravelServer
            // 
            this.lblTravelServer.AutoSize = true;
            this.lblTravelServer.Location = new System.Drawing.Point(11, 47);
            this.lblTravelServer.Name = "lblTravelServer";
            this.lblTravelServer.Size = new System.Drawing.Size(72, 13);
            this.lblTravelServer.TabIndex = 200;
            this.lblTravelServer.Text = "Travel server:";
            // 
            // tbTravelServer
            // 
            this.tbTravelServer.Enabled = false;
            this.tbTravelServer.Location = new System.Drawing.Point(83, 43);
            this.tbTravelServer.Name = "tbTravelServer";
            this.tbTravelServer.Size = new System.Drawing.Size(201, 20);
            this.tbTravelServer.TabIndex = 2;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.ForeColor = System.Drawing.Color.Red;
            this.lblContact.Location = new System.Drawing.Point(768, 23);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(254, 13);
            this.lblContact.TabIndex = 200;
            this.lblContact.Text = "(Any suggestion, please email v-elluo@expedia.com)";
            // 
            // bgwUpdate
            // 
            this.bgwUpdate.WorkerSupportsCancellation = true;
            this.bgwUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdate_DoWork);
            this.bgwUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdate_RunWorkerCompleted);
            // 
            // lblGroupsUpdateDate
            // 
            this.lblGroupsUpdateDate.AutoSize = true;
            this.lblGroupsUpdateDate.Location = new System.Drawing.Point(0, 23);
            this.lblGroupsUpdateDate.Name = "lblGroupsUpdateDate";
            this.lblGroupsUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblGroupsUpdateDate.TabIndex = 200;
            this.lblGroupsUpdateDate.Text = "Updated at: ";
            // 
            // lblServersUpdateDate
            // 
            this.lblServersUpdateDate.AutoSize = true;
            this.lblServersUpdateDate.Location = new System.Drawing.Point(202, 23);
            this.lblServersUpdateDate.Name = "lblServersUpdateDate";
            this.lblServersUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblServersUpdateDate.TabIndex = 200;
            this.lblServersUpdateDate.Text = "Updated at: ";
            // 
            // lblDatabasesUpdateDate
            // 
            this.lblDatabasesUpdateDate.AutoSize = true;
            this.lblDatabasesUpdateDate.Location = new System.Drawing.Point(492, 23);
            this.lblDatabasesUpdateDate.Name = "lblDatabasesUpdateDate";
            this.lblDatabasesUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblDatabasesUpdateDate.TabIndex = 200;
            this.lblDatabasesUpdateDate.Text = "Updated at: ";
            // 
            // pgbReloadServers
            // 
            this.pgbReloadServers.Location = new System.Drawing.Point(202, 0);
            this.pgbReloadServers.Maximum = 10000;
            this.pgbReloadServers.Name = "pgbReloadServers";
            this.pgbReloadServers.Size = new System.Drawing.Size(290, 23);
            this.pgbReloadServers.Step = 1;
            this.pgbReloadServers.TabIndex = 200;
            this.pgbReloadServers.Visible = false;
            // 
            // pgbReloadDatabases
            // 
            this.pgbReloadDatabases.Location = new System.Drawing.Point(492, 0);
            this.pgbReloadDatabases.Maximum = 10000;
            this.pgbReloadDatabases.Name = "pgbReloadDatabases";
            this.pgbReloadDatabases.Size = new System.Drawing.Size(263, 23);
            this.pgbReloadDatabases.Step = 1;
            this.pgbReloadDatabases.TabIndex = 200;
            this.pgbReloadDatabases.Visible = false;
            // 
            // tbGroupFilter
            // 
            this.tbGroupFilter.Location = new System.Drawing.Point(0, 37);
            this.tbGroupFilter.Name = "tbGroupFilter";
            this.tbGroupFilter.Size = new System.Drawing.Size(202, 20);
            this.tbGroupFilter.TabIndex = 8;
            // 
            // tbWebServerFilter
            // 
            this.tbWebServerFilter.Location = new System.Drawing.Point(202, 37);
            this.tbWebServerFilter.Name = "tbWebServerFilter";
            this.tbWebServerFilter.Size = new System.Drawing.Size(145, 20);
            this.tbWebServerFilter.TabIndex = 9;
            // 
            // tbTravelServerFilter
            // 
            this.tbTravelServerFilter.Location = new System.Drawing.Point(347, 37);
            this.tbTravelServerFilter.Name = "tbTravelServerFilter";
            this.tbTravelServerFilter.Size = new System.Drawing.Size(145, 20);
            this.tbTravelServerFilter.TabIndex = 10;
            // 
            // tbDatabaseFilter
            // 
            this.tbDatabaseFilter.Location = new System.Drawing.Point(492, 37);
            this.tbDatabaseFilter.Name = "tbDatabaseFilter";
            this.tbDatabaseFilter.Size = new System.Drawing.Size(263, 20);
            this.tbDatabaseFilter.TabIndex = 11;
            // 
            // cbAutoOpenEditer
            // 
            this.cbAutoOpenEditer.AutoSize = true;
            this.cbAutoOpenEditer.Location = new System.Drawing.Point(63, 165);
            this.cbAutoOpenEditer.Name = "cbAutoOpenEditer";
            this.cbAutoOpenEditer.Size = new System.Drawing.Size(145, 17);
            this.cbAutoOpenEditer.TabIndex = 8;
            this.cbAutoOpenEditer.Text = "Open a new Query Editer";
            this.cbAutoOpenEditer.UseVisualStyleBackColor = true;
            // 
            // btnReloadAll
            // 
            this.btnReloadAll.ForeColor = System.Drawing.Color.Red;
            this.btnReloadAll.Location = new System.Drawing.Point(754, 0);
            this.btnReloadAll.Name = "btnReloadAll";
            this.btnReloadAll.Size = new System.Drawing.Size(282, 23);
            this.btnReloadAll.TabIndex = 6;
            this.btnReloadAll.Text = "Reload all lists and save, it may cost several hours";
            this.btnReloadAll.UseVisualStyleBackColor = true;
            this.btnReloadAll.Click += new System.EventHandler(this.btnReloadAll_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(209, 184);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 11;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnClearAllSearchText
            // 
            this.btnClearAllSearchText.Location = new System.Drawing.Point(754, 36);
            this.btnClearAllSearchText.Name = "btnClearAllSearchText";
            this.btnClearAllSearchText.Size = new System.Drawing.Size(179, 23);
            this.btnClearAllSearchText.TabIndex = 7;
            this.btnClearAllSearchText.Text = "Clear all search text on the left";
            this.btnClearAllSearchText.UseVisualStyleBackColor = true;
            this.btnClearAllSearchText.Click += new System.EventHandler(this.btnClearAllSearchText_Click);
            // 
            // pgbReloadAllAndSave
            // 
            this.pgbReloadAllAndSave.Location = new System.Drawing.Point(754, 0);
            this.pgbReloadAllAndSave.Name = "pgbReloadAllAndSave";
            this.pgbReloadAllAndSave.Size = new System.Drawing.Size(282, 23);
            this.pgbReloadAllAndSave.Step = 1;
            this.pgbReloadAllAndSave.TabIndex = 200;
            this.pgbReloadAllAndSave.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tcGroups);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 384);
            this.panel1.TabIndex = 200;
            // 
            // tcGroups
            // 
            this.tcGroups.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcGroups.Controls.Add(this.tpAllGroups);
            this.tcGroups.Controls.Add(this.tpMyGroups);
            this.tcGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcGroups.Location = new System.Drawing.Point(0, 0);
            this.tcGroups.Margin = new System.Windows.Forms.Padding(0);
            this.tcGroups.Multiline = true;
            this.tcGroups.Name = "tcGroups";
            this.tcGroups.Padding = new System.Drawing.Point(0, 0);
            this.tcGroups.SelectedIndex = 0;
            this.tcGroups.Size = new System.Drawing.Size(202, 384);
            this.tcGroups.TabIndex = 200;
            this.tcGroups.TabStop = false;
            this.tcGroups.SelectedIndexChanged += new System.EventHandler(this.tcGroups_SelectedIndexChanged);
            // 
            // tpAllGroups
            // 
            this.tpAllGroups.Controls.Add(this.dgvAllGroups);
            this.tpAllGroups.Location = new System.Drawing.Point(4, 25);
            this.tpAllGroups.Name = "tpAllGroups";
            this.tpAllGroups.Padding = new System.Windows.Forms.Padding(3);
            this.tpAllGroups.Size = new System.Drawing.Size(194, 355);
            this.tpAllGroups.TabIndex = 200;
            this.tpAllGroups.Text = "All groups";
            this.tpAllGroups.UseVisualStyleBackColor = true;
            // 
            // dgvAllGroups
            // 
            this.dgvAllGroups.AllowUserToAddRows = false;
            this.dgvAllGroups.AllowUserToDeleteRows = false;
            this.dgvAllGroups.AllowUserToResizeColumns = false;
            this.dgvAllGroups.AllowUserToResizeRows = false;
            this.dgvAllGroups.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAllGroups.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAllGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvAllGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllGroups.ColumnHeadersVisible = false;
            this.dgvAllGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnGroupNameInAllGroups,
            this.Add});
            this.dgvAllGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllGroups.GridColor = System.Drawing.SystemColors.Window;
            this.dgvAllGroups.Location = new System.Drawing.Point(3, 3);
            this.dgvAllGroups.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAllGroups.MultiSelect = false;
            this.dgvAllGroups.Name = "dgvAllGroups";
            this.dgvAllGroups.ReadOnly = true;
            this.dgvAllGroups.RowHeadersVisible = false;
            this.dgvAllGroups.RowTemplate.Height = 14;
            this.dgvAllGroups.RowTemplate.ReadOnly = true;
            this.dgvAllGroups.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllGroups.ShowCellErrors = false;
            this.dgvAllGroups.ShowCellToolTips = false;
            this.dgvAllGroups.ShowEditingIcon = false;
            this.dgvAllGroups.ShowRowErrors = false;
            this.dgvAllGroups.Size = new System.Drawing.Size(188, 349);
            this.dgvAllGroups.TabIndex = 200;
            this.dgvAllGroups.TabStop = false;
            this.dgvAllGroups.SelectionChanged += new System.EventHandler(this.dgvAllGroups_SelectionChanged);
            // 
            // columnGroupNameInAllGroups
            // 
            this.columnGroupNameInAllGroups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnGroupNameInAllGroups.HeaderText = "Group name";
            this.columnGroupNameInAllGroups.Name = "columnGroupNameInAllGroups";
            this.columnGroupNameInAllGroups.ReadOnly = true;
            // 
            // Add
            // 
            this.Add.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Add.HeaderText = "Add";
            this.Add.LinkColor = System.Drawing.Color.Red;
            this.Add.Name = "Add";
            this.Add.ReadOnly = true;
            this.Add.Text = "Add";
            this.Add.UseColumnTextForLinkValue = true;
            this.Add.VisitedLinkColor = System.Drawing.Color.Red;
            this.Add.Width = 5;
            // 
            // tpMyGroups
            // 
            this.tpMyGroups.Controls.Add(this.dgvMyGroups);
            this.tpMyGroups.Location = new System.Drawing.Point(4, 25);
            this.tpMyGroups.Name = "tpMyGroups";
            this.tpMyGroups.Padding = new System.Windows.Forms.Padding(3);
            this.tpMyGroups.Size = new System.Drawing.Size(194, 355);
            this.tpMyGroups.TabIndex = 200;
            this.tpMyGroups.Text = "My groups";
            this.tpMyGroups.UseVisualStyleBackColor = true;
            // 
            // dgvMyGroups
            // 
            this.dgvMyGroups.AllowUserToAddRows = false;
            this.dgvMyGroups.AllowUserToDeleteRows = false;
            this.dgvMyGroups.AllowUserToResizeColumns = false;
            this.dgvMyGroups.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMyGroups.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMyGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMyGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyGroups.ColumnHeadersVisible = false;
            this.dgvMyGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnGroupNameInMyGroups,
            this.columnRemoveFromMyGroups});
            this.dgvMyGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyGroups.GridColor = System.Drawing.SystemColors.Window;
            this.dgvMyGroups.Location = new System.Drawing.Point(3, 3);
            this.dgvMyGroups.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMyGroups.MultiSelect = false;
            this.dgvMyGroups.Name = "dgvMyGroups";
            this.dgvMyGroups.ReadOnly = true;
            this.dgvMyGroups.RowHeadersVisible = false;
            this.dgvMyGroups.RowTemplate.Height = 14;
            this.dgvMyGroups.RowTemplate.ReadOnly = true;
            this.dgvMyGroups.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMyGroups.ShowCellErrors = false;
            this.dgvMyGroups.ShowCellToolTips = false;
            this.dgvMyGroups.ShowEditingIcon = false;
            this.dgvMyGroups.ShowRowErrors = false;
            this.dgvMyGroups.Size = new System.Drawing.Size(188, 349);
            this.dgvMyGroups.TabIndex = 200;
            this.dgvMyGroups.TabStop = false;
            this.dgvMyGroups.SelectionChanged += new System.EventHandler(this.dgvMyGroups_SelectionChanged);
            // 
            // columnGroupNameInMyGroups
            // 
            this.columnGroupNameInMyGroups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnGroupNameInMyGroups.HeaderText = "Group name";
            this.columnGroupNameInMyGroups.Name = "columnGroupNameInMyGroups";
            this.columnGroupNameInMyGroups.ReadOnly = true;
            // 
            // columnRemoveFromMyGroups
            // 
            this.columnRemoveFromMyGroups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.columnRemoveFromMyGroups.HeaderText = "Remove";
            this.columnRemoveFromMyGroups.LinkColor = System.Drawing.Color.Red;
            this.columnRemoveFromMyGroups.Name = "columnRemoveFromMyGroups";
            this.columnRemoveFromMyGroups.ReadOnly = true;
            this.columnRemoveFromMyGroups.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnRemoveFromMyGroups.Text = "Remove";
            this.columnRemoveFromMyGroups.ToolTipText = "Remove from my group list";
            this.columnRemoveFromMyGroups.UseColumnTextForLinkValue = true;
            this.columnRemoveFromMyGroups.VisitedLinkColor = System.Drawing.Color.Red;
            this.columnRemoveFromMyGroups.Width = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(202, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvServers);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblGroup);
            this.splitContainer2.Panel2.Controls.Add(this.btnOptions);
            this.splitContainer2.Panel2.Controls.Add(this.tbDatabase);
            this.splitContainer2.Panel2.Controls.Add(this.btnConnect);
            this.splitContainer2.Panel2.Controls.Add(this.lblInstance);
            this.splitContainer2.Panel2.Controls.Add(this.lblDatabase);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer2.Panel2.Controls.Add(this.tbInstance);
            this.splitContainer2.Panel2.Controls.Add(this.lblWebServer);
            this.splitContainer2.Panel2.Controls.Add(this.cbAutoOpenEditer);
            this.splitContainer2.Panel2.Controls.Add(this.lblAuthentication);
            this.splitContainer2.Panel2.Controls.Add(this.tbPassword);
            this.splitContainer2.Panel2.Controls.Add(this.lblTravelServer);
            this.splitContainer2.Panel2.Controls.Add(this.cbConnectionType);
            this.splitContainer2.Panel2.Controls.Add(this.lblPassword);
            this.splitContainer2.Panel2.Controls.Add(this.tbTravelServer);
            this.splitContainer2.Panel2.Controls.Add(this.tbGroup);
            this.splitContainer2.Panel2.Controls.Add(this.lblUserName);
            this.splitContainer2.Panel2.Controls.Add(this.tbUserName);
            this.splitContainer2.Panel2.Controls.Add(this.tbWebServer);
            this.splitContainer2.Size = new System.Drawing.Size(290, 384);
            this.splitContainer2.SplitterDistance = 166;
            this.splitContainer2.TabIndex = 200;
            this.splitContainer2.TabStop = false;
            // 
            // pgbReloadGroups
            // 
            this.pgbReloadGroups.Location = new System.Drawing.Point(0, 0);
            this.pgbReloadGroups.Maximum = 10000;
            this.pgbReloadGroups.Name = "pgbReloadGroups";
            this.pgbReloadGroups.Size = new System.Drawing.Size(202, 23);
            this.pgbReloadGroups.Step = 1;
            this.pgbReloadGroups.TabIndex = 200;
            this.pgbReloadGroups.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.pgbReloadServers);
            this.splitContainer1.Panel1.Controls.Add(this.pgbReloadGroups);
            this.splitContainer1.Panel1.Controls.Add(this.pgbReloadDatabases);
            this.splitContainer1.Panel1.Controls.Add(this.pgbReloadAllAndSave);
            this.splitContainer1.Panel1.Controls.Add(this.tbDatabaseFilter);
            this.splitContainer1.Panel1.Controls.Add(this.tbWebServerFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lblDatabasesUpdateDate);
            this.splitContainer1.Panel1.Controls.Add(this.lblServersUpdateDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnClearAllSearchText);
            this.splitContainer1.Panel1.Controls.Add(this.lblContact);
            this.splitContainer1.Panel1.Controls.Add(this.tbGroupFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadDatabases);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadServers);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveServers);
            this.splitContainer1.Panel1.Controls.Add(this.tbTravelServerFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveGroups);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadGroups);
            this.splitContainer1.Panel1.Controls.Add(this.lblGroupsUpdateDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveDatabases);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadAll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1087, 448);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 200;
            this.splitContainer1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lvDatabases);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(492, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 384);
            this.panel2.TabIndex = 200;
            // 
            // ServerInstanceSelector
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1087, 448);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "ServerInstanceSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database selector";
            this.Activated += new System.EventHandler(this.ServerInstanceSelector_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerInstanceSelector_FormClosing);
            this.Load += new System.EventHandler(this.ServerInstanceSelector_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerInstanceSelector_KeyUp);
            this.panel1.ResumeLayout(false);
            this.tcGroups.ResumeLayout(false);
            this.tpAllGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllGroups)).EndInit();
            this.tpMyGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyGroups)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvDatabases;
        private System.Windows.Forms.Label lblAuthentication;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ComboBox cbConnectionType;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label lblInstance;
        private System.Windows.Forms.TextBox tbInstance;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.Button btnReloadGroups;
        private System.Windows.Forms.Button btnSaveGroups;
        private System.Windows.Forms.Button btnReloadServers;
        private System.Windows.Forms.Button btnSaveServers;
        private System.Windows.Forms.Button btnReloadDatabases;
        private System.Windows.Forms.Button btnSaveDatabases;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.TextBox tbGroup;
        private System.Windows.Forms.Label lblWebServer;
        private System.Windows.Forms.TextBox tbWebServer;
        private System.Windows.Forms.Label lblTravelServer;
        private System.Windows.Forms.TextBox tbTravelServer;
        private System.Windows.Forms.Label lblContact;

        private string targetDatabase;
        private string targetInstance;
        private string targetServer;
        private string targetAuthentication;
        private int targetAuthenticationIndex;
        private string targetUsername;
        private string targetPassword;

        private GroupList groupList;
        private MyGroupList myGroupList;
        private ServerList serverList;
        private TravelServer travelServer;
        private UpdateThread ut;
        private string trigger;
        private Index index;
        public int version;

        private TreeView tree;
        private TreeNode databaseObjectNode;
        private TreeNode databaseInstanceNode;
        private TreeNode tableNode;
        private UIConnectionInfo connection = null;
        object objectExplorerService;
        object connectionObject;
        bool needRefresh;
        private System.ComponentModel.BackgroundWorker bgwUpdate;
        private Label lblGroupsUpdateDate;
        private Label lblServersUpdateDate;
        private Label lblDatabasesUpdateDate;
        private ProgressBar pgbReloadServers;
        private ProgressBar pgbReloadDatabases;
        private TextBox tbGroupFilter;
        private TextBox tbWebServerFilter;
        private TextBox tbTravelServerFilter;
        private TextBox tbDatabaseFilter;
        private CheckBox cbAutoOpenEditer;
        private Button btnReloadAll;
        private Button btnOptions;
        private Button btnClearAllSearchText;
        private ProgressBar pgbReloadAllAndSave;
        private ColumnHeader HeaderWebServer;
        private ColumnHeader HeaderTravelServer;
        private ColumnHeader HeaderDatabase;
        private ColumnHeader HeaderServer;
        private ColumnHeader HeaderInstance;
        private ColumnHeader HeaderAuthentication;
        private ColumnHeader HeaderUsername;
        private ColumnHeader HeaderPassword;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private Panel panel2;
        private SplitContainer splitContainer2;
        private TabControl tcGroups;
        private TabPage tpAllGroups;
        private TabPage tpMyGroups;
        private DataGridView dgvMyGroups;
        private DataGridView dgvAllGroups;
        private ProgressBar pgbReloadGroups;
        private DataGridViewTextBoxColumn columnGroupNameInAllGroups;
        private DataGridViewLinkColumn Add;
        private DataGridViewTextBoxColumn columnGroupNameInMyGroups;
        private DataGridViewLinkColumn columnRemoveFromMyGroups;

    }
}