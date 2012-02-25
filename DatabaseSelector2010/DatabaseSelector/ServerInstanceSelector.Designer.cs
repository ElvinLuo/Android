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
            this.lvGroups = new System.Windows.Forms.ListView();
            this.HeaderGroups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.pgbReloadGroups = new System.Windows.Forms.ProgressBar();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(99, 186);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 21);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(174, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 3;
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
            this.lvDatabases.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvDatabases.FullRowSelect = true;
            this.lvDatabases.Location = new System.Drawing.Point(0, 54);
            this.lvDatabases.Name = "lvDatabases";
            this.lvDatabases.Size = new System.Drawing.Size(583, 448);
            this.lvDatabases.TabIndex = 8;
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
            this.lblAuthentication.Location = new System.Drawing.Point(4, 113);
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(95, 12);
            this.lblAuthentication.TabIndex = 9;
            this.lblAuthentication.Text = "Authentication:";
            this.lblAuthentication.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(34, 132);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(65, 12);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "User Name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(40, 153);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 12);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbConnectionType
            // 
            this.cbConnectionType.FormattingEnabled = true;
            this.cbConnectionType.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cbConnectionType.Location = new System.Drawing.Point(99, 108);
            this.cbConnectionType.Name = "cbConnectionType";
            this.cbConnectionType.Size = new System.Drawing.Size(185, 20);
            this.cbConnectionType.TabIndex = 12;
            this.cbConnectionType.Text = "Windows Authentication";
            this.cbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cbConnectionType_SelectedIndexChanged);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(99, 128);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(185, 21);
            this.tbUserName.TabIndex = 13;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(99, 149);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(185, 21);
            this.tbPassword.TabIndex = 14;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(40, 70);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(59, 12);
            this.lblDatabase.TabIndex = 15;
            this.lblDatabase.Text = "Database:";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Enabled = false;
            this.tbDatabase.Location = new System.Drawing.Point(99, 66);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(185, 21);
            this.tbDatabase.TabIndex = 16;
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(40, 91);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(59, 12);
            this.lblInstance.TabIndex = 17;
            this.lblInstance.Text = "Instance:";
            this.lblInstance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbInstance
            // 
            this.tbInstance.Enabled = false;
            this.tbInstance.Location = new System.Drawing.Point(99, 87);
            this.tbInstance.Name = "tbInstance";
            this.tbInstance.Size = new System.Drawing.Size(185, 21);
            this.tbInstance.TabIndex = 18;
            // 
            // lvServers
            // 
            this.lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderWebServer,
            this.HeaderTravelServer});
            this.lvServers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvServers.FullRowSelect = true;
            this.lvServers.Location = new System.Drawing.Point(0, 54);
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(290, 234);
            this.lvServers.TabIndex = 19;
            this.lvServers.Tag = "";
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            this.lvServers.SelectedIndexChanged += new System.EventHandler(this.lvServers_SelectedIndexChanged);
            // 
            // HeaderWebServer
            // 
            this.HeaderWebServer.Text = "Web server";
            this.HeaderWebServer.Width = 144;
            // 
            // HeaderTravelServer
            // 
            this.HeaderTravelServer.Text = "Travel server";
            this.HeaderTravelServer.Width = 144;
            // 
            // lvGroups
            // 
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderGroups});
            this.lvGroups.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.Location = new System.Drawing.Point(0, 54);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(202, 448);
            this.lvGroups.TabIndex = 20;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // HeaderGroups
            // 
            this.HeaderGroups.Text = "Groups";
            this.HeaderGroups.Width = 198;
            // 
            // btnReloadGroups
            // 
            this.btnReloadGroups.Location = new System.Drawing.Point(0, 0);
            this.btnReloadGroups.Name = "btnReloadGroups";
            this.btnReloadGroups.Size = new System.Drawing.Size(101, 21);
            this.btnReloadGroups.TabIndex = 21;
            this.btnReloadGroups.Text = "Reload groups";
            this.btnReloadGroups.UseVisualStyleBackColor = true;
            this.btnReloadGroups.Click += new System.EventHandler(this.btnReloadGroups_Click);
            // 
            // btnSaveGroups
            // 
            this.btnSaveGroups.Location = new System.Drawing.Point(101, 0);
            this.btnSaveGroups.Name = "btnSaveGroups";
            this.btnSaveGroups.Size = new System.Drawing.Size(101, 21);
            this.btnSaveGroups.TabIndex = 22;
            this.btnSaveGroups.Text = "Save groups";
            this.btnSaveGroups.UseVisualStyleBackColor = true;
            this.btnSaveGroups.Click += new System.EventHandler(this.btnSaveGroups_Click);
            // 
            // btnReloadServers
            // 
            this.btnReloadServers.Location = new System.Drawing.Point(0, 0);
            this.btnReloadServers.Name = "btnReloadServers";
            this.btnReloadServers.Size = new System.Drawing.Size(145, 21);
            this.btnReloadServers.TabIndex = 23;
            this.btnReloadServers.Text = "Reload servers";
            this.btnReloadServers.UseVisualStyleBackColor = true;
            this.btnReloadServers.Click += new System.EventHandler(this.btnReloadServers_Click);
            // 
            // btnSaveServers
            // 
            this.btnSaveServers.Location = new System.Drawing.Point(145, 0);
            this.btnSaveServers.Name = "btnSaveServers";
            this.btnSaveServers.Size = new System.Drawing.Size(145, 21);
            this.btnSaveServers.TabIndex = 24;
            this.btnSaveServers.Text = "Save servers";
            this.btnSaveServers.UseVisualStyleBackColor = true;
            this.btnSaveServers.Click += new System.EventHandler(this.btnSaveServers_Click);
            // 
            // btnReloadDatabases
            // 
            this.btnReloadDatabases.Location = new System.Drawing.Point(0, 0);
            this.btnReloadDatabases.Name = "btnReloadDatabases";
            this.btnReloadDatabases.Size = new System.Drawing.Size(131, 21);
            this.btnReloadDatabases.TabIndex = 25;
            this.btnReloadDatabases.Text = "Reload databases";
            this.btnReloadDatabases.UseVisualStyleBackColor = true;
            this.btnReloadDatabases.Click += new System.EventHandler(this.btnReloadDatabases_Click);
            // 
            // btnSaveDatabases
            // 
            this.btnSaveDatabases.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveDatabases.Location = new System.Drawing.Point(131, 0);
            this.btnSaveDatabases.Name = "btnSaveDatabases";
            this.btnSaveDatabases.Size = new System.Drawing.Size(131, 21);
            this.btnSaveDatabases.TabIndex = 26;
            this.btnSaveDatabases.Text = "Save databases";
            this.btnSaveDatabases.UseVisualStyleBackColor = true;
            this.btnSaveDatabases.Click += new System.EventHandler(this.btnSaveDatabases_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(58, 7);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(41, 12);
            this.lblGroup.TabIndex = 27;
            this.lblGroup.Text = "Group:";
            // 
            // tbGroup
            // 
            this.tbGroup.Enabled = false;
            this.tbGroup.Location = new System.Drawing.Point(99, 3);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(185, 21);
            this.tbGroup.TabIndex = 28;
            // 
            // lblWebServer
            // 
            this.lblWebServer.AutoSize = true;
            this.lblWebServer.Location = new System.Drawing.Point(28, 28);
            this.lblWebServer.Name = "lblWebServer";
            this.lblWebServer.Size = new System.Drawing.Size(71, 12);
            this.lblWebServer.TabIndex = 29;
            this.lblWebServer.Text = "Web server:";
            // 
            // tbWebServer
            // 
            this.tbWebServer.Enabled = false;
            this.tbWebServer.Location = new System.Drawing.Point(99, 24);
            this.tbWebServer.Name = "tbWebServer";
            this.tbWebServer.Size = new System.Drawing.Size(185, 21);
            this.tbWebServer.TabIndex = 30;
            // 
            // lblTravelServer
            // 
            this.lblTravelServer.AutoSize = true;
            this.lblTravelServer.Location = new System.Drawing.Point(10, 49);
            this.lblTravelServer.Name = "lblTravelServer";
            this.lblTravelServer.Size = new System.Drawing.Size(89, 12);
            this.lblTravelServer.TabIndex = 31;
            this.lblTravelServer.Text = "Travel server:";
            // 
            // tbTravelServer
            // 
            this.tbTravelServer.Enabled = false;
            this.tbTravelServer.Location = new System.Drawing.Point(99, 45);
            this.tbTravelServer.Name = "tbTravelServer";
            this.tbTravelServer.Size = new System.Drawing.Size(185, 21);
            this.tbTravelServer.TabIndex = 32;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.ForeColor = System.Drawing.Color.Red;
            this.lblContact.Location = new System.Drawing.Point(262, 21);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(305, 12);
            this.lblContact.TabIndex = 33;
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
            this.lblGroupsUpdateDate.Location = new System.Drawing.Point(0, 21);
            this.lblGroupsUpdateDate.Name = "lblGroupsUpdateDate";
            this.lblGroupsUpdateDate.Size = new System.Drawing.Size(77, 12);
            this.lblGroupsUpdateDate.TabIndex = 34;
            this.lblGroupsUpdateDate.Text = "Updated at: ";
            // 
            // lblServersUpdateDate
            // 
            this.lblServersUpdateDate.AutoSize = true;
            this.lblServersUpdateDate.Location = new System.Drawing.Point(0, 21);
            this.lblServersUpdateDate.Name = "lblServersUpdateDate";
            this.lblServersUpdateDate.Size = new System.Drawing.Size(77, 12);
            this.lblServersUpdateDate.TabIndex = 35;
            this.lblServersUpdateDate.Text = "Updated at: ";
            // 
            // lblDatabasesUpdateDate
            // 
            this.lblDatabasesUpdateDate.AutoSize = true;
            this.lblDatabasesUpdateDate.Location = new System.Drawing.Point(0, 21);
            this.lblDatabasesUpdateDate.Name = "lblDatabasesUpdateDate";
            this.lblDatabasesUpdateDate.Size = new System.Drawing.Size(77, 12);
            this.lblDatabasesUpdateDate.TabIndex = 36;
            this.lblDatabasesUpdateDate.Text = "Updated at: ";
            // 
            // pgbReloadGroups
            // 
            this.pgbReloadGroups.Location = new System.Drawing.Point(0, 0);
            this.pgbReloadGroups.Maximum = 10000;
            this.pgbReloadGroups.Name = "pgbReloadGroups";
            this.pgbReloadGroups.Size = new System.Drawing.Size(202, 21);
            this.pgbReloadGroups.Step = 1;
            this.pgbReloadGroups.TabIndex = 37;
            this.pgbReloadGroups.Visible = false;
            // 
            // pgbReloadServers
            // 
            this.pgbReloadServers.Location = new System.Drawing.Point(0, 0);
            this.pgbReloadServers.Maximum = 10000;
            this.pgbReloadServers.Name = "pgbReloadServers";
            this.pgbReloadServers.Size = new System.Drawing.Size(290, 21);
            this.pgbReloadServers.Step = 1;
            this.pgbReloadServers.TabIndex = 38;
            this.pgbReloadServers.Visible = false;
            // 
            // pgbReloadDatabases
            // 
            this.pgbReloadDatabases.Location = new System.Drawing.Point(0, 0);
            this.pgbReloadDatabases.Maximum = 10000;
            this.pgbReloadDatabases.Name = "pgbReloadDatabases";
            this.pgbReloadDatabases.Size = new System.Drawing.Size(263, 21);
            this.pgbReloadDatabases.Step = 1;
            this.pgbReloadDatabases.TabIndex = 39;
            this.pgbReloadDatabases.Visible = false;
            // 
            // tbGroupFilter
            // 
            this.tbGroupFilter.Location = new System.Drawing.Point(0, 33);
            this.tbGroupFilter.Name = "tbGroupFilter";
            this.tbGroupFilter.Size = new System.Drawing.Size(202, 21);
            this.tbGroupFilter.TabIndex = 40;
            // 
            // tbWebServerFilter
            // 
            this.tbWebServerFilter.Location = new System.Drawing.Point(0, 33);
            this.tbWebServerFilter.Name = "tbWebServerFilter";
            this.tbWebServerFilter.Size = new System.Drawing.Size(145, 21);
            this.tbWebServerFilter.TabIndex = 41;
            // 
            // tbTravelServerFilter
            // 
            this.tbTravelServerFilter.Location = new System.Drawing.Point(145, 33);
            this.tbTravelServerFilter.Name = "tbTravelServerFilter";
            this.tbTravelServerFilter.Size = new System.Drawing.Size(145, 21);
            this.tbTravelServerFilter.TabIndex = 42;
            // 
            // tbDatabaseFilter
            // 
            this.tbDatabaseFilter.Location = new System.Drawing.Point(0, 33);
            this.tbDatabaseFilter.Name = "tbDatabaseFilter";
            this.tbDatabaseFilter.Size = new System.Drawing.Size(263, 21);
            this.tbDatabaseFilter.TabIndex = 43;
            // 
            // cbAutoOpenEditer
            // 
            this.cbAutoOpenEditer.AutoSize = true;
            this.cbAutoOpenEditer.Location = new System.Drawing.Point(79, 170);
            this.cbAutoOpenEditer.Name = "cbAutoOpenEditer";
            this.cbAutoOpenEditer.Size = new System.Drawing.Size(162, 16);
            this.cbAutoOpenEditer.TabIndex = 44;
            this.cbAutoOpenEditer.Text = "Open a new Query Editer";
            this.cbAutoOpenEditer.UseVisualStyleBackColor = true;
            // 
            // btnReloadAll
            // 
            this.btnReloadAll.ForeColor = System.Drawing.Color.Red;
            this.btnReloadAll.Location = new System.Drawing.Point(262, 0);
            this.btnReloadAll.Name = "btnReloadAll";
            this.btnReloadAll.Size = new System.Drawing.Size(282, 21);
            this.btnReloadAll.TabIndex = 45;
            this.btnReloadAll.Text = "Reload all lists and save, it may cost several hours";
            this.btnReloadAll.UseVisualStyleBackColor = true;
            this.btnReloadAll.Click += new System.EventHandler(this.btnReloadAll_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(24, 186);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 21);
            this.btnOptions.TabIndex = 46;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnClearAllSearchText
            // 
            this.btnClearAllSearchText.Location = new System.Drawing.Point(263, 33);
            this.btnClearAllSearchText.Name = "btnClearAllSearchText";
            this.btnClearAllSearchText.Size = new System.Drawing.Size(179, 21);
            this.btnClearAllSearchText.TabIndex = 47;
            this.btnClearAllSearchText.Text = "Clear all search text on the left";
            this.btnClearAllSearchText.UseVisualStyleBackColor = true;
            this.btnClearAllSearchText.Click += new System.EventHandler(this.btnClearAllSearchText_Click);
            // 
            // pgbReloadAllAndSave
            // 
            this.pgbReloadAllAndSave.Location = new System.Drawing.Point(262, 0);
            this.pgbReloadAllAndSave.Name = "pgbReloadAllAndSave";
            this.pgbReloadAllAndSave.Size = new System.Drawing.Size(282, 21);
            this.pgbReloadAllAndSave.Step = 1;
            this.pgbReloadAllAndSave.TabIndex = 48;
            this.pgbReloadAllAndSave.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lvGroups);
            this.panel1.Controls.Add(this.tbGroupFilter);
            this.panel1.Controls.Add(this.lblGroupsUpdateDate);
            this.panel1.Controls.Add(this.pgbReloadGroups);
            this.panel1.Controls.Add(this.btnReloadGroups);
            this.panel1.Controls.Add(this.btnSaveGroups);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 502);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Yellow;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(202, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.splitContainer1.Panel1.Controls.Add(this.lvServers);
            this.splitContainer1.Panel1.Controls.Add(this.tbWebServerFilter);
            this.splitContainer1.Panel1.Controls.Add(this.tbTravelServerFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lblServersUpdateDate);
            this.splitContainer1.Panel1.Controls.Add(this.pgbReloadServers);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadServers);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveServers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Teal;
            this.splitContainer1.Panel2.Controls.Add(this.btnOptions);
            this.splitContainer1.Panel2.Controls.Add(this.btnConnect);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.cbAutoOpenEditer);
            this.splitContainer1.Panel2.Controls.Add(this.tbPassword);
            this.splitContainer1.Panel2.Controls.Add(this.lblPassword);
            this.splitContainer1.Panel2.Controls.Add(this.tbUserName);
            this.splitContainer1.Panel2.Controls.Add(this.lblUserName);
            this.splitContainer1.Panel2.Controls.Add(this.cbConnectionType);
            this.splitContainer1.Panel2.Controls.Add(this.lblAuthentication);
            this.splitContainer1.Panel2.Controls.Add(this.tbInstance);
            this.splitContainer1.Panel2.Controls.Add(this.lblInstance);
            this.splitContainer1.Panel2.Controls.Add(this.tbDatabase);
            this.splitContainer1.Panel2.Controls.Add(this.lblDatabase);
            this.splitContainer1.Panel2.Controls.Add(this.lblGroup);
            this.splitContainer1.Panel2.Controls.Add(this.lblWebServer);
            this.splitContainer1.Panel2.Controls.Add(this.lblTravelServer);
            this.splitContainer1.Panel2.Controls.Add(this.tbTravelServer);
            this.splitContainer1.Panel2.Controls.Add(this.tbWebServer);
            this.splitContainer1.Panel2.Controls.Add(this.tbGroup);
            this.splitContainer1.Size = new System.Drawing.Size(290, 502);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 49;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Controls.Add(this.lvDatabases);
            this.panel2.Controls.Add(this.tbDatabaseFilter);
            this.panel2.Controls.Add(this.btnClearAllSearchText);
            this.panel2.Controls.Add(this.lblDatabasesUpdateDate);
            this.panel2.Controls.Add(this.btnReloadAll);
            this.panel2.Controls.Add(this.btnSaveDatabases);
            this.panel2.Controls.Add(this.pgbReloadAllAndSave);
            this.panel2.Controls.Add(this.btnReloadDatabases);
            this.panel2.Controls.Add(this.pgbReloadDatabases);
            this.panel2.Controls.Add(this.lblContact);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(492, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(583, 502);
            this.panel2.TabIndex = 50;
            // 
            // ServerInstanceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 502);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ServerInstanceSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database selector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerInstanceSelector_FormClosing);
            this.Load += new System.EventHandler(this.ServerInstanceSelector_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerInstanceSelector_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.ListView lvGroups;
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
        private System.Windows.Forms.ColumnHeader HeaderGroups;
        private System.Windows.Forms.Label lblContact;

        private string targetDatabase;
        private string targetInstance;
        private string targetServer;
        private string targetAuthentication;
        private int targetAuthenticationIndex;
        private string targetUsername;
        private string targetPassword;

        private GroupList groupList;
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
        private ProgressBar pgbReloadGroups;
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

    }
}