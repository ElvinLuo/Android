﻿using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo.RegSvrEnum;
using Microsoft.SqlServer.Management.UI.VSIntegration;
using Microsoft.SqlServer.Management.UI.VSIntegration.Editors;
using Microsoft.SqlServer.Management.UI.VSIntegration.ObjectExplorer;
using SHDocVw;

namespace DatabaseSelector
{
    public partial class ServerInstanceSelector : Form
    {
        public ServerInstanceSelector()
        {
            InitializeComponent();
            index = Index.CreateInstance();
            tbGroupFilter.Text = index.groupFilter;
            tbWebServerFilter.Text = index.webServerFilter;
            tbTravelServerFilter.Text = index.travelServerFilter;
            tbDatabaseFilter.Text = index.databaseFilter;
            cbAutoOpenEditer.Checked = index.automaticallyOpenEditer;

            ut = UpdateThread.CreateInstance();
            if (ut.inProgress)
            { btnReloadAll.Enabled = false; }

            groupList = GroupList.instance;
            myGroupList = MyGroupList.instance;
            serverList = new ServerList();
            travelServer = new TravelServer();

            groupList.GetGroups();
            myGroupList.GetGroups();
            ReloadGroupListView(tbGroupFilter.Text);
            ReloadMyGroupListView();
        }

        void ServerInstanceSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            index.groupFilter = tbGroupFilter.Text;
            index.webServerFilter = tbWebServerFilter.Text;
            index.travelServerFilter = tbTravelServerFilter.Text;
            index.databaseFilter = tbDatabaseFilter.Text;
            index.automaticallyOpenEditer = cbAutoOpenEditer.Checked;
            index.SaveIndexToXml();
        }

        private void ServerInstanceSelector_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            { this.Close(); }
        }

        private void ServerInstanceSelector_Load(object sender, EventArgs e)
        {
            this.tbGroupFilter.TextChanged += new System.EventHandler(this.tbGroupFilter_TextChanged);
            this.tbWebServerFilter.TextChanged += new System.EventHandler(this.tbWebServerFilter_TextChanged);
            this.tbTravelServerFilter.TextChanged += new System.EventHandler(this.tbTravelServerFilter_TextChanged);
            this.tbDatabaseFilter.TextChanged += new System.EventHandler(this.tbDatabaseFilter_TextChanged);
            ut.Updated += new EventHandler(ut_Updated);
        }

        private void ReloadGroupListView(string filter)
        {
            lvGroups.Items.Clear();
            System.Windows.Forms.ListView.ListViewItemCollection lvicGroup = new ListView.ListViewItemCollection(lvGroups);
            if (IsMatchingFilter(Global.defaultALLGroupName, filter))
            { lvicGroup.Add(new ListViewItem(new string[] { Global.defaultALLGroupName })); }
            if (IsMatchingFilter(Global.defaultPPEGroupName, filter))
            { lvicGroup.Add(new ListViewItem(new string[] { Global.defaultPPEGroupName })); }
            if (groupList.groups != null && groupList.groups.Count != 0)
            {
                for (int i = 0; i < groupList.groups.Count; i++)
                {
                    if (IsMatchingFilter(groupList.groups[i], filter))
                    {
                        ListViewItem lviDatabase = new ListViewItem(new string[] { groupList.groups[i] });
                        lvicGroup.Add(lviDatabase);
                    }
                }
            }
            if (lvGroups.Items.Count == 0)
            { lvicGroup.Add(new ListViewItem(new string[] { Global.noGroupItemBanner })); }
            if (lvGroups.Items.Count != 0)
            {
                if (index.currentSelectedGroup >= lvGroups.Items.Count)
                { index.currentSelectedGroup = 0; }
                groupList.selectedGroup = lvGroups.Items[index.currentSelectedGroup].SubItems[0].Text;
                lvGroups.Items[index.currentSelectedGroup].Selected = true;
            }
            lblGroupsUpdateDate.Text = "Updated at: " + groupList.updateDate;
        }

        private void ReloadMyGroupListView()
        {
            dgvMyGroups.Rows.Clear();

            if (myGroupList.groups != null && myGroupList.groups.Count != 0)
            {
                for (int i = 0; i < myGroupList.groups.Count; i++)
                {
                    dgvMyGroups.Rows.Add(myGroupList.groups[i]);
                }
            }

            if (dgvMyGroups.Rows.Count != 0)
            {
                if (index.currentSelectedMyGroup >= dgvMyGroups.Rows.Count)
                { index.currentSelectedMyGroup = 0; }

                myGroupList.selectedGroup =
                    dgvMyGroups.Rows[index.currentSelectedMyGroup].Cells[0].Value.ToString();
                dgvMyGroups.Rows[index.currentSelectedMyGroup].Selected = true;
            }
        }

        private void ReloadServerListView(string webServerFilter, string travelServerFilter)
        {
            lvServers.Items.Clear();
            System.Windows.Forms.ListView.ListViewItemCollection lvic = new ListView.ListViewItemCollection(lvServers);
            if (serverList.groupName.Equals(Global.defaultALLGroupName) && IsMatchingFilter(Global.defaultALLWebServerName, webServerFilter) && IsMatchingFilter(Global.defaultALLTravelServerName, travelServerFilter))
            { lvic.Add(new ListViewItem(new string[] { Global.defaultALLWebServerName, Global.defaultALLTravelServerName })); }
            if (serverList.servers != null && serverList.servers.Count != 0)
            {
                for (int i = 0; i < serverList.servers.Count; i++)
                {
                    if (IsMatchingFilter(serverList.servers[i].serverName, webServerFilter) && IsMatchingFilter(serverList.servers[i].travelServer, travelServerFilter))
                    {
                        ListViewItem lviServer = new ListViewItem(new string[] { serverList.servers[i].serverName, serverList.servers[i].travelServer });
                        lvic.Add(lviServer);
                    }
                }
            }
            if (lvServers.Items.Count == 0)
            { lvic.Add(new ListViewItem(new string[] { Global.noWebServerItemBanner, Global.noTravelServerItemBanner })); }
            if (lvServers.Items.Count != 0)
            {
                if (index.currentSelectedServer >= lvServers.Items.Count)
                { index.currentSelectedServer = 0; }
                lvServers.Items[index.currentSelectedServer].Selected = true;
            }
            lblServersUpdateDate.Text = "Updated at: " + serverList.updateDate;
            if (groupList.selectedGroup.Equals(Global.defaultALLGroupName) || groupList.selectedGroup.Equals(Global.noGroupItemBanner))
            {
                btnReloadServers.Enabled = false;
                btnSaveServers.Enabled = false;
            }
            else
            {
                btnReloadServers.Enabled = true;
                btnSaveServers.Enabled = true;
            }
        }

        private void ReloadDatabaseListView(string filter)
        {
            lvDatabases.Items.Clear();
            System.Windows.Forms.ListView.ListViewItemCollection lvic = new ListView.ListViewItemCollection(lvDatabases);
            if (travelServer.Databases != null && travelServer.Databases.Count != 0)
            {
                foreach (DatabaseItem database in travelServer.Databases)
                {
                    if (IsMatchingFilter(database.DatabaseName, filter))
                    {
                        ListViewItem lviDatabase = new ListViewItem(new string[] { database.DatabaseName, database.Server, database.Database, "SQL Server Authentication", database.UserName, database.UserAuth });
                        lvic.Add(lviDatabase);
                    }
                }
            }
            if (lvDatabases.Items.Count == 0)
            { lvic.Add(new ListViewItem(new string[] { Global.noDatabaseItemBanner, Global.tryReloadDatabaseBanner, Global.emptyString, Global.emptyString, Global.emptyString, Global.emptyString })); }
            if (lvDatabases.Items.Count != 0)
            {
                if (index.currentSelectedDatabase >= lvDatabases.Items.Count)
                { index.currentSelectedDatabase = 0; }
                lvDatabases.Items[index.currentSelectedDatabase].Selected = true;
            }
            foreach (ColumnHeader ch in lvDatabases.Columns)
            {
                ch.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                if (ch.Width < 80)
                { ch.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); }
            }
            lblDatabasesUpdateDate.Text = "Updated at: " + travelServer.updateDate;
            if (CanReloadAndSaveDatabaseList())
            {
                btnReloadDatabases.Enabled = true;
                btnSaveDatabases.Enabled = true;
            }
            else
            {
                btnReloadDatabases.Enabled = false;
                btnSaveDatabases.Enabled = false;
            }
        }

        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            ut.CreateNewThread().Start();
        }

        void ut_Updated(object sender, EventArgs e)
        {
            if (btnReloadAll.IsHandleCreated)
            {
                if (ut.inProgress)
                { btnReloadAll.Invoke((MethodInvoker)delegate { btnReloadAll.Enabled = false; }); }
                else
                { btnReloadAll.Invoke((MethodInvoker)delegate { btnReloadAll.Enabled = true; }); }
            }
        }

        private void btnReloadGroups_Click(object sender, EventArgs e)
        {
            trigger = Global.reloadGroupButtonName;
            DisableAllButtons();
            pgbReloadGroups.Visible = true;
            bgwUpdate.RunWorkerAsync();
        }

        private void btnReloadServers_Click(object sender, EventArgs e)
        {
            trigger = Global.reloadServerButtonName;
            DisableAllButtons();
            pgbReloadServers.Visible = true;
            bgwUpdate.RunWorkerAsync();
        }

        private void btnReloadDatabases_Click(object sender, EventArgs e)
        {
            trigger = Global.reloadDatabaseButtonName;
            DisableAllButtons();
            pgbReloadDatabases.Visible = true;
            bgwUpdate.RunWorkerAsync();
        }

        private void bgwUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (trigger.Equals(Global.reloadDatabaseButtonName))
            { travelServer.GetDatabasesFromRegistryAndChangeProgressBar(pgbReloadDatabases); }
            else if (trigger.Equals(Global.reloadServerButtonName) && serverList.groupName.Equals(Global.defaultPPEGroupName))
            { serverList.GetServersFromFile(serverList.groupName, pgbReloadServers); }
            else
            {
                InternetExplorer ie = new InternetExplorer();
                ie.ProgressChange += new DWebBrowserEvents2_ProgressChangeEventHandler(ie_ProgressChange);

                if (trigger.Equals(Global.reloadGroupButtonName))
                { groupList.GetGroupsFromWeb(ie, false); }
                else if (trigger.Equals(Global.reloadServerButtonName))
                { serverList.GetServersFromWeb(ie, false); }

                ie.ProgressChange -= new DWebBrowserEvents2_ProgressChangeEventHandler(ie_ProgressChange);
                ie.Quit();
            }
        }

        void ie_ProgressChange(int Progress, int ProgressMax)
        {
            this.Invoke((MethodInvoker)delegate
            {
                ProgressBar pgb = null;
                string text = null;
                if (trigger.Equals(Global.reloadGroupButtonName))
                {
                    pgb = pgbReloadGroups;
                    text = Global.reloadingGroupBanner;
                }
                else if (trigger.Equals(Global.reloadServerButtonName))
                {
                    pgb = pgbReloadServers;
                    text = Global.reloadingServerBanner + groupList.selectedGroup;
                }
                if (pgbReloadGroups.Minimum <= Progress && Progress <= pgbReloadGroups.Maximum)
                {
                    pgb.Value = Progress;
                    Global.SetProgressBarText(pgb, text);
                }
            });
        }

        private void bgwUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Button btn = sender as Button;
            if (trigger.Equals(Global.reloadGroupButtonName))
            {
                ReloadGroupListView(tbGroupFilter.Text);
                pgbReloadGroups.Visible = false;
            }
            else if (trigger.Equals(Global.reloadServerButtonName))
            {
                ReloadServerListView(tbWebServerFilter.Text, tbTravelServerFilter.Text);
                pgbReloadServers.Visible = false;
            }
            else if (trigger.Equals(Global.reloadDatabaseButtonName))
            {
                ReloadDatabaseListView(tbDatabaseFilter.Text);
                pgbReloadDatabases.Visible = false;
            }
            trigger = null;
            EnableAllButtons();
        }

        private void btnSaveGroups_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            groupList.SaveListToXML();
            EnableAllButtons();
        }

        private void btnSaveServers_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            GroupServerList gsl;
            if (File.Exists(Global.defaultServersFile))
            {
                gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), Global.defaultServersFileName) as GroupServerList);
                gsl.groupServers.Remove(gsl.GetServerList(serverList.groupName));
            }
            else
            { gsl = GroupServerList.CreateInstance(); }
            gsl.groupServers.Add(serverList);
            gsl.SaveListToXML();
            EnableAllButtons();
        }

        private void btnSaveDatabases_Click(object sender, EventArgs e)
        {
            if (travelServer.MachineName.Equals(Global.defaultALLPPETravelServerName)) return;
            DisableAllButtons();
            TravelServerList tsl;
            if (File.Exists(Global.defaultDatabasesFile))
            {
                tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), Global.defaultDatabasesFileName) as TravelServerList);
                tsl.travelServers.Remove(tsl.GetTravelServer(travelServer.MachineName));
            }
            else
            { tsl = TravelServerList.CreateInstance(); }
            tsl.travelServers.Add(travelServer);
            tsl.SaveListToXml();
            EnableAllButtons();
        }

        private void tbGroupFilter_TextChanged(object sender, EventArgs e)
        {
            ReloadGroupListView(tbGroupFilter.Text);
        }

        private void tbWebServerFilter_TextChanged(object sender, EventArgs e)
        {
            ReloadServerListView(tbWebServerFilter.Text, tbTravelServerFilter.Text);
        }

        private void tbTravelServerFilter_TextChanged(object sender, EventArgs e)
        {
            ReloadServerListView(tbWebServerFilter.Text, tbTravelServerFilter.Text);
        }

        private void tbDatabaseFilter_TextChanged(object sender, EventArgs e)
        {
            ReloadDatabaseListView(tbDatabaseFilter.Text);
        }

        private void btnClearAllSearchText_Click(object sender, EventArgs e)
        {
            tbGroupFilter.Text = Global.emptyString;
            tbWebServerFilter.Text = Global.emptyString;
            tbTravelServerFilter.Text = Global.emptyString;
            tbDatabaseFilter.Text = Global.emptyString;
        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedIndices.Count != 0)
            {
                //Clear highlight color of original item
                index.previousSelectedGroup = index.currentSelectedGroup;
                index.currentSelectedGroup = lvGroups.SelectedIndices[0];

                if (index.previousSelectedGroup != -1 && index.previousSelectedGroup < lvGroups.Items.Count)
                {
                    lvGroups.Items[index.previousSelectedGroup].BackColor = SystemColors.Window;
                    lvGroups.Items[index.previousSelectedGroup].ForeColor = SystemColors.WindowText;
                }
                if (index.currentSelectedGroup != -1 && index.currentSelectedGroup < lvGroups.Items.Count)
                {
                    lvGroups.Items[index.currentSelectedGroup].BackColor = SystemColors.Highlight;
                    lvGroups.Items[index.currentSelectedGroup].ForeColor = Color.White;
                }
                groupList.selectedGroup = lvGroups.Items[lvGroups.SelectedIndices[0]].SubItems[0].Text;

                serverList.groupName = groupList.selectedGroup;
                serverList.GetServers();
                ReloadServerListView(tbWebServerFilter.Text, tbTravelServerFilter.Text);
            }
        }

        private void dgvMyGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index.previousSelectedMyGroup = index.currentSelectedMyGroup;
            index.currentSelectedMyGroup = e.RowIndex;
            myGroupList.selectedGroup = dgvMyGroups.Rows[e.RowIndex].Cells[0].Value.ToString();
            serverList.groupName = myGroupList.selectedGroup;
            serverList.GetServers();
            ReloadServerListView(tbWebServerFilter.Text, tbTravelServerFilter.Text);
        }

        private void lvServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvServers.SelectedIndices.Count != 0)
            {
                //Clear highlight color of original item
                index.previousSelectedServer = index.currentSelectedServer;
                index.currentSelectedServer = lvServers.SelectedIndices[0];
                if (index.previousSelectedServer != -1 && index.previousSelectedServer < lvServers.Items.Count)
                {
                    lvServers.Items[index.previousSelectedServer].BackColor = SystemColors.Window;
                    lvServers.Items[index.previousSelectedServer].ForeColor = SystemColors.WindowText;
                }
                if (index.currentSelectedServer != -1 && index.currentSelectedServer < lvServers.Items.Count)
                {
                    lvServers.Items[index.currentSelectedServer].BackColor = SystemColors.Highlight;
                    lvServers.Items[index.currentSelectedServer].ForeColor = Color.White;
                }
                if (!lvServers.Items[lvServers.SelectedIndices[0]].SubItems[0].Text.Contains(Global.noWebServerItemBanner))
                {
                    tbWebServer.Text = lvServers.Items[lvServers.SelectedIndices[0]].SubItems[0].Text;
                    tbTravelServer.Text = lvServers.Items[lvServers.SelectedIndices[0]].SubItems[1].Text;
                    travelServer.MachineName = lvServers.Items[lvServers.SelectedIndices[0]].SubItems[1].Text;
                }
                else
                {
                    tbTravelServer.Text = lvServers.Items[lvServers.SelectedIndices[0]].SubItems[0].Text;
                    travelServer.MachineName = lvServers.Items[lvServers.SelectedIndices[0]].SubItems[0].Text;
                }
                tbGroup.Text = serverList.groupName;

                travelServer.GetDatabases();
                ReloadDatabaseListView(tbDatabaseFilter.Text);
            }
        }

        private void lvDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDatabases.SelectedIndices.Count != 0)
            {
                //Clear highlight color of original item
                index.previousSelectedDatabase = index.currentSelectedDatabase;
                index.currentSelectedDatabase = lvDatabases.SelectedIndices[0];

                if (index.previousSelectedDatabase != -1 && index.previousSelectedDatabase < lvDatabases.Items.Count)
                {
                    lvDatabases.Items[index.previousSelectedDatabase].BackColor = SystemColors.Window;
                    lvDatabases.Items[index.previousSelectedDatabase].ForeColor = SystemColors.WindowText;
                }
                if (index.currentSelectedDatabase != -1 && index.currentSelectedDatabase < lvDatabases.Items.Count)
                {
                    lvDatabases.Items[index.currentSelectedDatabase].BackColor = SystemColors.Highlight;
                    lvDatabases.Items[index.currentSelectedDatabase].ForeColor = Color.White;
                }

                tbDatabase.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[0].Text;
                tbInstance.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[2].Text;

                targetDatabase = tbDatabase.Text;
                targetServer = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[1].Text;
                targetInstance = tbInstance.Text;

                if (!CanConnect())
                { btnConnect.Enabled = false; }
                else
                { btnConnect.Enabled = true; }

                if (serverList.groupName.Equals(Global.defaultPPEGroupName))
                {
                    cbConnectionType.SelectedIndex = 1;
                    tbUserName.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[4].Text;
                    tbPassword.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[5].Text;
                }
                else
                {
                    cbConnectionType.SelectedIndex = 0;
                    tbUserName.Enabled = false;
                    tbPassword.Enabled = false;
                    tbUserName.Text = System.Environment.UserDomainName + "\\" + System.Environment.UserName;
                    tbPassword.Text = Global.emptyString;
                }
                targetAuthentication = cbConnectionType.Text;
                targetAuthenticationIndex = cbConnectionType.SelectedIndex;
                targetUsername = tbUserName.Text;
                targetPassword = tbPassword.Text;
            }
        }

        void lvDatabases_DoubleClick(object sender, System.EventArgs e)
        {
            if (!CanConnect()) return;
            ConnectToServer();
        }

        private void cbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConnectionType.SelectedIndex == 0)
            {
                tbUserName.Enabled = false;
                tbPassword.Enabled = false;
                tbUserName.Text = System.Environment.UserDomainName + "\\" + System.Environment.UserName;
                tbPassword.Text = Global.emptyString;
            }
            else
            {
                tbUserName.Enabled = true;
                tbPassword.Enabled = true;
                tbUserName.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[4].Text;
                tbPassword.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[5].Text;
            }
            targetAuthentication = cbConnectionType.Text;
            targetAuthenticationIndex = cbConnectionType.SelectedIndex;
            targetUsername = tbUserName.Text;
            targetPassword = tbPassword.Text;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!CanConnect()) return;
            ConnectToServer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectToServer()
        {
            try
            {
                if (version == 2008)
                {
                    Assembly asm = Assembly.LoadFile(Global.dllFolderFor100 + "Microsoft.SqlServer.RegSvrEnum.dll");
                    connectionObject = System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Smo.RegSvrEnum.UIConnectionInfo"));
                    Type type = asm.GetType("Microsoft.SqlServer.Management.Smo.RegSvrEnum.UIConnectionInfo");
                    type.GetProperty("ServerType").SetValue(connectionObject, new Guid("8c91a03d-f9b4-46c0-a305-b5dcc79ff907"), null);
                    type.GetProperty("ServerName").SetValue(connectionObject, targetServer, null);
                    type.GetProperty("AuthenticationType").SetValue(connectionObject, targetAuthenticationIndex, null);
                    type.GetProperty("UserName").SetValue(connectionObject, targetUsername, null);
                    if (targetAuthenticationIndex == 1)
                    {
                        type.GetProperty("Password").SetValue(connectionObject, targetPassword, null);
                    }
                    asm = Assembly.LoadFile(Global.dllFolderFor100 + "Microsoft.SqlServer.SqlTools.VSIntegration.dll");
                    type = asm.GetType("Microsoft.SqlServer.Management.UI.VSIntegration.ServiceCache");
                    MethodInfo methodGetObjectExplorer = type.GetMethod("GetObjectExplorer");
                    object explorer = methodGetObjectExplorer.Invoke(null, null);
                    type = explorer.GetType();
                    Type[] parameters = new Type[1] { Type.GetType("System.Object") };
                    methodGetObjectExplorer = type.GetMethod("ConnectToServer", parameters);
                    methodGetObjectExplorer.Invoke(explorer, new object[] { connectionObject });
                    objectExplorerService = explorer;
                }
                else if (version == 2005)
                {
                    connection = new UIConnectionInfo();
                    connection.ServerType = new Guid("8c91a03d-f9b4-46c0-a305-b5dcc79ff907");
                    connection.ServerName = targetServer;
                    connection.AuthenticationType = targetAuthenticationIndex;
                    connection.UserName = targetUsername;
                    if (connection.AuthenticationType == 1)
                    {
                        connection.Password = targetPassword;
                    }
                    ServiceCache.GetObjectExplorer().ConnectToServer(connection);
                    objectExplorerService = ServiceCache.GetObjectExplorer();
                }

                //Below is to expand tree
                IExplorerHierarchy hierarchy;
                MethodInfo methodGetHierarchy = objectExplorerService.GetType().GetMethod("GetHierarchy", BindingFlags.Instance | BindingFlags.NonPublic);
                if (version == 2008)
                {
                    Assembly asm = Assembly.LoadFile(Global.dllFolderFor100 + "Microsoft.SqlServer.ConnectionInfo.dll");
                    object sqlConnectionInfoObject = targetAuthenticationIndex == 0 ?
                        System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Common.SqlConnectionInfo"), new object[] { targetServer }) :
                        System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Common.SqlConnectionInfo"), new object[] { targetServer, targetUsername, targetPassword }); ;
                    object hierarchyObject = methodGetHierarchy.Invoke(objectExplorerService, new object[] { sqlConnectionInfoObject, null });
                    object rootObject = hierarchyObject.GetType().GetProperty("Root").GetValue(hierarchyObject, null);
                    object treeViewObject = rootObject.GetType().GetProperty("TreeView").GetValue(rootObject, null);
                    tree = treeViewObject as TreeView;
                }
                else if (version == 2005)
                {
                    SqlConnectionInfo sqlConnectionInfo = targetAuthenticationIndex == 0 ?
                    new SqlConnectionInfo(targetServer) :
                    new SqlConnectionInfo(targetServer, targetUsername, targetPassword);
                    hierarchy = (IExplorerHierarchy)methodGetHierarchy.Invoke(objectExplorerService, new object[] { sqlConnectionInfo });
                    tree = hierarchy.Root.TreeView;
                    needRefresh = true;
                }
                //Prepare the tree to expand
                SelectAndExpandDatabasesNode();
            }
            catch (Exception exception)
            { Console.WriteLine(exception.Message); }
            finally
            { this.Close(); }
        }

        void SelectAndExpandDatabasesNode()
        {
            if (tree.SelectedNode.IsExpanded)
                SelectAndExpandDatabasesNode_AfterParentExpand();
            else
            {
                tree.AfterExpand += new TreeViewEventHandler(tree_AfterConnectionNodeExpand);
                tree.SelectedNode.Expand();
            }
        }

        void tree_AfterConnectionNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterConnectionNodeExpand);
            SelectAndExpandDatabasesNode_AfterParentExpand();
        }

        void SelectAndExpandDatabasesNode_AfterParentExpand()
        {
            TreeNode rootTreeNode = tree.SelectedNode;
            for (int i = 0; i < rootTreeNode.Nodes.Count; i++)
            {
                databaseObjectNode = rootTreeNode.Nodes[i];
                if (databaseObjectNode.Text.Equals("Databases"))
                {
                    tree.SelectedNode = databaseObjectNode;
                    databaseObjectNode.Expand();
                    SelectAndExpandInstanceNode();
                    break;
                }
            }
        }

        void SelectAndExpandInstanceNode()
        {
            if (databaseObjectNode.IsExpanded)
                SelectAndExpandInstanceNode_AfterParentExpand();
            else
            {
                tree.AfterExpand += new TreeViewEventHandler(tree_AfterDatabaseObjectNodeExpand);
            }
        }

        void tree_AfterDatabaseObjectNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterDatabaseObjectNodeExpand);
            SelectAndExpandInstanceNode_AfterParentExpand();
        }

        void SelectAndExpandInstanceNode_AfterParentExpand()
        {
            int index = 0, maxMatching = 0;
            for (int i = 0; i < databaseObjectNode.Nodes.Count; i++)
            {
                databaseInstanceNode = databaseObjectNode.Nodes[i];
                if (databaseInstanceNode.Text.ToLower().Equals(targetDatabase.ToLower()))
                {
                    index = i;
                    break;
                }
                int currentMatching = GetMatchingLength(databaseInstanceNode.Text.ToLower().ToCharArray(), targetInstance.ToLower().ToCharArray());
                if (currentMatching > maxMatching)
                {
                    index = i;
                    maxMatching = currentMatching;
                }
            }
            databaseInstanceNode = databaseObjectNode.Nodes[index];
            tree.SelectedNode = databaseInstanceNode;
            databaseInstanceNode.Expand();
            SelectAndExpandTablesNode();
        }

        void SelectAndExpandTablesNode()
        {
            if (databaseInstanceNode.IsExpanded)
                SelectAndExpandTablesNode_AfterParentExpand();
            else
            {
                tree.AfterExpand += new TreeViewEventHandler(tree_AfterDatabaseInstanceNodeExpand);
            }
        }

        void tree_AfterDatabaseInstanceNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterDatabaseInstanceNodeExpand);
            SelectAndExpandTablesNode_AfterParentExpand();
        }

        void SelectAndExpandTablesNode_AfterParentExpand()
        {
            for (int i = 0; i < databaseInstanceNode.Nodes.Count; i++)
            {
                tableNode = databaseInstanceNode.Nodes[i];
                if (tableNode.Text.Equals("Tables"))
                {
                    tree.AfterExpand += new TreeViewEventHandler(tree_AfterTablesNodeExpand);
                    tableNode.Expand();
                    break;
                }
            }
        }

        void tree_AfterTablesNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterTablesNodeExpand);
            if (cbAutoOpenEditer.Checked)
            { CreateNewScript(); }
        }

        void CreateNewScript()
        {
            try
            {
                if (needRefresh && version == 2005)
                {
                    databaseObjectNode.Collapse();
                    needRefresh = false;
                    ServiceCache.GetObjectExplorer().ConnectToServer(connection);
                    SelectAndExpandDatabasesNode();
                    return;
                }
                string strFullPath = Global.applicationFolder + "SQLFile.sql";
                if (version == 2008)
                {
                    connection = new UIConnectionInfo();
                    connection.ServerType = new Guid("8c91a03d-f9b4-46c0-a305-b5dcc79ff907");
                    connection.ServerName = targetServer;
                    connection.AuthenticationType = targetAuthenticationIndex;
                    connection.UserName = targetUsername;
                    if (connection.AuthenticationType == 1)
                    {
                        connection.Password = targetPassword;
                    }
                }
                SqlConnection sqlConnection = new SqlConnection(string.Format("Data Source=({0});Initial Catalog={1}{2}",
                    connection.ServerName, databaseInstanceNode.Text,
                    (connection.AuthenticationType == 0 ? ";Integrated Security=SSPI" : string.Format(";User Id={0};password={1}", connection.UserName, connection.Password))));

                //Show the connection information in the Text Editor
                using (StreamWriter streamWriter = new StreamWriter(strFullPath))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("/*****************************************************************************");
                    sb.AppendLine("--This Query Editer window is opened by Database Selector automatically");
                    sb.AppendLine("--Server		:" + connection.ServerName);
                    sb.AppendLine("--Database	    :" + sqlConnection.Database);
                    sb.AppendLine("--Authentication:" + targetAuthentication);
                    sb.AppendLine("--UserName	    :" + targetUsername);
                    sb.AppendLine("*****************************************************************************/");
                    sb.Append("USE " + sqlConnection.Database);
                    streamWriter.Write(sb.ToString());
                }
                if (version == 2008)
                {
                    Assembly asm = Assembly.LoadFile(Global.dllFolderFor100 + "SQLEditors.dll");
                    Type scriptFactoryType = asm.GetType("Microsoft.SqlServer.Management.UI.VSIntegration.Editors.ScriptFactory");
                    object sfiObject = scriptFactoryType.GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                    MethodInfo[] methodCreateNewScript = scriptFactoryType.GetMethods();
                    methodCreateNewScript[16].Invoke(sfiObject, new object[] { strFullPath, connectionObject, sqlConnection });
                }
                else if (version == 2005)
                {
                    ScriptFactory.Instance.CreateNewScript(strFullPath, connection, sqlConnection);
                }
            }
            catch (Exception exception)
            { Console.WriteLine(exception.Message); }
        }

        private void DisableAllButtons()
        {
            btnReloadDatabases.Enabled = false;
            btnReloadGroups.Enabled = false;
            btnReloadServers.Enabled = false;
            btnSaveDatabases.Enabled = false;
            btnSaveGroups.Enabled = false;
            btnSaveServers.Enabled = false;
            btnReloadAll.Enabled = false;
            btnClearAllSearchText.Enabled = false;
            btnConnect.Enabled = false;

            lvGroups.Enabled = false;
            lvServers.Enabled = false;
            lvDatabases.Enabled = false;
        }

        private void EnableAllButtons()
        {
            btnReloadGroups.Enabled = true;
            btnSaveGroups.Enabled = true;
            if (!groupList.selectedGroup.Equals(Global.defaultALLGroupName) && !groupList.selectedGroup.Equals(Global.noGroupItemBanner))
            {
                btnReloadServers.Enabled = true;
                btnSaveServers.Enabled = true;
            }
            if (CanReloadAndSaveDatabaseList())
            {
                btnReloadDatabases.Enabled = true;
                btnSaveDatabases.Enabled = true;
            }
            btnReloadAll.Enabled = true;
            btnClearAllSearchText.Enabled = true;
            btnConnect.Enabled = true;

            lvGroups.Enabled = true;
            lvServers.Enabled = true;
            lvDatabases.Enabled = true;
        }

        private bool CanReloadAndSaveDatabaseList()
        {
            return !(travelServer.MachineName.Equals(Global.defaultALLTravelServerName) ||
                  travelServer.MachineName.Equals(Global.defaultALLPPETravelServerName) ||
                  lvServers.Items[index.currentSelectedServer].Text.Contains(Global.noWebServerItemBanner));
        }

        private bool IsMatchingFilter(string item, string filter)
        { return (string.IsNullOrEmpty(filter) || item.ToLower().Contains(filter.ToLower())); }

        private bool CanConnect()
        { return !(targetDatabase.Equals(Global.noDatabaseItemBanner) || string.IsNullOrEmpty(targetServer)); }

        public int GetMatchingLength(char[] string1, char[] string2)
        {
            int len = 0, matched = 0;
            char[] source, pattern;
            if (string1.Length >= string2.Length)
            {
                source = string1;
                pattern = string2;
            }
            else
            {
                pattern = string1;
                source = string2;
            }
            for (int i = 0, j = 0; i < source.Length && j < pattern.Length; )
            {
                if (source[i] == pattern[j])
                {
                    i++;
                    j++;
                    matched++;
                    if (matched > len)
                        len = matched;
                }
                else
                {
                    i = i - matched + 1;
                    j = 0;
                    matched = 0;
                }
            }
            return len;
        }

    }
}
