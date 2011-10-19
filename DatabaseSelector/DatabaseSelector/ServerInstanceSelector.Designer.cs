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
            this.lvGroups = new System.Windows.Forms.ListView();
            this.Groups = new System.Windows.Forms.ColumnHeader();
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
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(280, 506);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(355, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvDatabases
            // 
            this.lvDatabases.Location = new System.Drawing.Point(465, 42);
            this.lvDatabases.Name = "lvDatabases";
            this.lvDatabases.Size = new System.Drawing.Size(701, 487);
            this.lvDatabases.TabIndex = 8;
            this.lvDatabases.UseCompatibleStateImageBehavior = false;
            this.lvDatabases.View = System.Windows.Forms.View.Details;
            this.lvDatabases.SelectedIndexChanged += new System.EventHandler(this.lvDatabases_SelectedIndexChanged);
            this.lvDatabases.DoubleClick += new System.EventHandler(this.lvDatabases_DoubleClick);
            // 
            // lblAuthentication
            // 
            this.lblAuthentication.AutoSize = true;
            this.lblAuthentication.Location = new System.Drawing.Point(202, 447);
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(78, 13);
            this.lblAuthentication.TabIndex = 9;
            this.lblAuthentication.Text = "Authentication:";
            this.lblAuthentication.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(217, 468);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 13);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "User Name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(224, 488);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
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
            this.cbConnectionType.Location = new System.Drawing.Point(280, 443);
            this.cbConnectionType.Name = "cbConnectionType";
            this.cbConnectionType.Size = new System.Drawing.Size(185, 21);
            this.cbConnectionType.TabIndex = 12;
            this.cbConnectionType.Text = "Windows Authentication";
            this.cbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cbConnectionType_SelectedIndexChanged);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(280, 464);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(185, 20);
            this.tbUserName.TabIndex = 13;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(280, 484);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(185, 20);
            this.tbPassword.TabIndex = 14;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(224, 407);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 13);
            this.lblDatabase.TabIndex = 15;
            this.lblDatabase.Text = "Database:";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Enabled = false;
            this.tbDatabase.Location = new System.Drawing.Point(280, 403);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(185, 20);
            this.tbDatabase.TabIndex = 16;
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(229, 427);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(51, 13);
            this.lblInstance.TabIndex = 17;
            this.lblInstance.Text = "Instance:";
            this.lblInstance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbInstance
            // 
            this.tbInstance.Enabled = false;
            this.tbInstance.Location = new System.Drawing.Point(280, 423);
            this.tbInstance.Name = "tbInstance";
            this.tbInstance.Size = new System.Drawing.Size(185, 20);
            this.tbInstance.TabIndex = 18;
            // 
            // lvServers
            // 
            this.lvServers.Location = new System.Drawing.Point(202, 42);
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(263, 295);
            this.lvServers.TabIndex = 19;
            this.lvServers.Tag = "";
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            this.lvServers.SelectedIndexChanged += new System.EventHandler(this.lvServers_SelectedIndexChanged);
            // 
            // lvGroups
            // 
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Groups});
            this.lvGroups.Location = new System.Drawing.Point(0, 42);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(202, 487);
            this.lvGroups.TabIndex = 20;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // Groups
            // 
            this.Groups.Text = "Groups";
            this.Groups.Width = 175;
            // 
            // btnReloadGroups
            // 
            this.btnReloadGroups.Location = new System.Drawing.Point(0, 0);
            this.btnReloadGroups.Name = "btnReloadGroups";
            this.btnReloadGroups.Size = new System.Drawing.Size(101, 23);
            this.btnReloadGroups.TabIndex = 21;
            this.btnReloadGroups.Text = "Reload groups";
            this.btnReloadGroups.UseVisualStyleBackColor = true;
            this.btnReloadGroups.Click += new System.EventHandler(this.btnReloadGroups_Click);
            // 
            // btnSaveGroups
            // 
            this.btnSaveGroups.Location = new System.Drawing.Point(101, 0);
            this.btnSaveGroups.Name = "btnSaveGroups";
            this.btnSaveGroups.Size = new System.Drawing.Size(101, 23);
            this.btnSaveGroups.TabIndex = 22;
            this.btnSaveGroups.Text = "Save groups";
            this.btnSaveGroups.UseVisualStyleBackColor = true;
            this.btnSaveGroups.Click += new System.EventHandler(this.btnSaveGroups_Click);
            // 
            // btnReloadServers
            // 
            this.btnReloadServers.Location = new System.Drawing.Point(202, 0);
            this.btnReloadServers.Name = "btnReloadServers";
            this.btnReloadServers.Size = new System.Drawing.Size(101, 23);
            this.btnReloadServers.TabIndex = 23;
            this.btnReloadServers.Text = "Reload servers";
            this.btnReloadServers.UseVisualStyleBackColor = true;
            this.btnReloadServers.Click += new System.EventHandler(this.btnReloadServers_Click);
            // 
            // btnSaveServers
            // 
            this.btnSaveServers.Location = new System.Drawing.Point(303, 0);
            this.btnSaveServers.Name = "btnSaveServers";
            this.btnSaveServers.Size = new System.Drawing.Size(101, 23);
            this.btnSaveServers.TabIndex = 24;
            this.btnSaveServers.Text = "Save servers";
            this.btnSaveServers.UseVisualStyleBackColor = true;
            this.btnSaveServers.Click += new System.EventHandler(this.btnSaveServers_Click);
            // 
            // btnReloadDatabases
            // 
            this.btnReloadDatabases.Location = new System.Drawing.Point(465, 0);
            this.btnReloadDatabases.Name = "btnReloadDatabases";
            this.btnReloadDatabases.Size = new System.Drawing.Size(101, 23);
            this.btnReloadDatabases.TabIndex = 25;
            this.btnReloadDatabases.Text = "Reload databases";
            this.btnReloadDatabases.UseVisualStyleBackColor = true;
            this.btnReloadDatabases.Click += new System.EventHandler(this.btnReloadDatabases_Click);
            // 
            // btnSaveDatabases
            // 
            this.btnSaveDatabases.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveDatabases.Location = new System.Drawing.Point(566, 0);
            this.btnSaveDatabases.Name = "btnSaveDatabases";
            this.btnSaveDatabases.Size = new System.Drawing.Size(101, 23);
            this.btnSaveDatabases.TabIndex = 26;
            this.btnSaveDatabases.Text = "Save databases";
            this.btnSaveDatabases.UseVisualStyleBackColor = true;
            this.btnSaveDatabases.Click += new System.EventHandler(this.btnDatabases_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(241, 347);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 27;
            this.lblGroup.Text = "Group:";
            // 
            // tbGroup
            // 
            this.tbGroup.Enabled = false;
            this.tbGroup.Location = new System.Drawing.Point(280, 343);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(185, 20);
            this.tbGroup.TabIndex = 28;
            // 
            // lblWebServer
            // 
            this.lblWebServer.AutoSize = true;
            this.lblWebServer.Location = new System.Drawing.Point(215, 367);
            this.lblWebServer.Name = "lblWebServer";
            this.lblWebServer.Size = new System.Drawing.Size(65, 13);
            this.lblWebServer.TabIndex = 29;
            this.lblWebServer.Text = "Web server:";
            // 
            // tbWebServer
            // 
            this.tbWebServer.Enabled = false;
            this.tbWebServer.Location = new System.Drawing.Point(280, 363);
            this.tbWebServer.Name = "tbWebServer";
            this.tbWebServer.Size = new System.Drawing.Size(185, 20);
            this.tbWebServer.TabIndex = 30;
            // 
            // lblTravelServer
            // 
            this.lblTravelServer.AutoSize = true;
            this.lblTravelServer.Location = new System.Drawing.Point(208, 387);
            this.lblTravelServer.Name = "lblTravelServer";
            this.lblTravelServer.Size = new System.Drawing.Size(72, 13);
            this.lblTravelServer.TabIndex = 31;
            this.lblTravelServer.Text = "Travel server:";
            // 
            // tbTravelServer
            // 
            this.tbTravelServer.Enabled = false;
            this.tbTravelServer.Location = new System.Drawing.Point(280, 384);
            this.tbTravelServer.Name = "tbTravelServer";
            this.tbTravelServer.Size = new System.Drawing.Size(185, 20);
            this.tbTravelServer.TabIndex = 32;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.ForeColor = System.Drawing.Color.Red;
            this.lblContact.Location = new System.Drawing.Point(667, 5);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(220, 13);
            this.lblContact.TabIndex = 33;
            this.lblContact.Text = "(Any suggestion, email v-elluo@expedia.com)";
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
            this.lblGroupsUpdateDate.Location = new System.Drawing.Point(-3, 26);
            this.lblGroupsUpdateDate.Name = "lblGroupsUpdateDate";
            this.lblGroupsUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblGroupsUpdateDate.TabIndex = 34;
            this.lblGroupsUpdateDate.Text = "Updated at: ";
            // 
            // lblServersUpdateDate
            // 
            this.lblServersUpdateDate.AutoSize = true;
            this.lblServersUpdateDate.Location = new System.Drawing.Point(199, 26);
            this.lblServersUpdateDate.Name = "lblServersUpdateDate";
            this.lblServersUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblServersUpdateDate.TabIndex = 35;
            this.lblServersUpdateDate.Text = "Updated at: ";
            // 
            // lblDatabasesUpdateDate
            // 
            this.lblDatabasesUpdateDate.AutoSize = true;
            this.lblDatabasesUpdateDate.Location = new System.Drawing.Point(465, 26);
            this.lblDatabasesUpdateDate.Name = "lblDatabasesUpdateDate";
            this.lblDatabasesUpdateDate.Size = new System.Drawing.Size(66, 13);
            this.lblDatabasesUpdateDate.TabIndex = 36;
            this.lblDatabasesUpdateDate.Text = "Updated at: ";
            // 
            // ServerInstanceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 529);
            this.Controls.Add(this.lblDatabasesUpdateDate);
            this.Controls.Add(this.lblServersUpdateDate);
            this.Controls.Add(this.lblGroupsUpdateDate);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.tbTravelServer);
            this.Controls.Add(this.lblTravelServer);
            this.Controls.Add(this.tbWebServer);
            this.Controls.Add(this.lblWebServer);
            this.Controls.Add(this.tbGroup);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.btnSaveDatabases);
            this.Controls.Add(this.btnReloadDatabases);
            this.Controls.Add(this.btnSaveServers);
            this.Controls.Add(this.btnReloadServers);
            this.Controls.Add(this.btnSaveGroups);
            this.Controls.Add(this.btnReloadGroups);
            this.Controls.Add(this.lvGroups);
            this.Controls.Add(this.lvServers);
            this.Controls.Add(this.tbInstance);
            this.Controls.Add(this.lblInstance);
            this.Controls.Add(this.tbDatabase);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.cbConnectionType);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblAuthentication);
            this.Controls.Add(this.lvDatabases);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.MaximizeBox = false;
            this.Name = "ServerInstanceSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database selector";
            this.Load += new System.EventHandler(this.ServerInstanceSelector_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerInstanceSelector_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ColumnHeader Groups;
        private System.Windows.Forms.Label lblContact;

        private GroupList groupList;
        private ServerList serverList;
        private TravelServer travelServer;
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

    }
}