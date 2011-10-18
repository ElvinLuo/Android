using System;
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
using System.Threading;

namespace DatabaseSelector
{
    public partial class ServerInstanceSelector : Form
    {
        public ServerInstanceSelector()
        {
            InitializeComponent();
            //IE = new InternetExplorer();
            index = Index.CreateInstance();
            //index.GetIndexFromXml();

            //lvGroups.Columns.Add("Groups", -2, HorizontalAlignment.Left);
            lvGroups.FullRowSelect = true;
            groupList = GroupList.instance;
            //groupList = new GroupList();
            groupList.GetGroups();
            ReloadGroupListView(this, EventArgs.Empty);

            lvServers.Columns.Add("Web server", -2, HorizontalAlignment.Left);
            lvServers.Columns.Add("Travel server", -2, HorizontalAlignment.Left);
            lvServers.FullRowSelect = true;
            serverList = new ServerList(GroupList.instance.defaultGroup);
            serverList.GetServers();
            ReloadServerListView(this, EventArgs.Empty);

            lvDatabases.Columns.Add("Database", -2, HorizontalAlignment.Left);
            lvDatabases.Columns.Add("Server", -2, HorizontalAlignment.Left);
            lvDatabases.Columns.Add("Instance", -2, HorizontalAlignment.Left);
            lvDatabases.Columns.Add("Authentication", -2, HorizontalAlignment.Left);
            lvDatabases.Columns.Add("User name", -2, HorizontalAlignment.Left);
            lvDatabases.Columns.Add("Password", -2, HorizontalAlignment.Left);
            //lvDatabases.Select();
            lvDatabases.FullRowSelect = true;
            travelServer = new TravelServer();
            travelServer.GetDatabases();
            ReloadDatabaseListView(this, EventArgs.Empty);

            btnConnect.Focus();
        }

        void ServerInstanceSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            index.SaveIndexToXml();
            //if (IE != null)
            //    IE.Quit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        void lvDatabases_DoubleClick(object sender, System.EventArgs e)
        {
            ConnectToServer();
            //throw new System.NotImplementedException();
        }

        private void ConnectToServer()
        {
            try
            {
                if (version == 2008)
                {
                    Assembly asm = Assembly.LoadFile(Serializer.CreateInstance().applicationFolder + "dll/10.0.0.0/Microsoft.SqlServer.RegSvrEnum.dll");
                    connectionObject = System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Smo.RegSvrEnum.UIConnectionInfo"));
                    Type type = asm.GetType("Microsoft.SqlServer.Management.Smo.RegSvrEnum.UIConnectionInfo");
                    type.GetProperty("ServerType").SetValue(connectionObject, new Guid("8c91a03d-f9b4-46c0-a305-b5dcc79ff907"), null);
                    type.GetProperty("ServerName").SetValue(connectionObject, travelServer.Databases[lvDatabases.SelectedIndices[0]].Server, null);
                    type.GetProperty("AuthenticationType").SetValue(connectionObject, cbConnectionType.SelectedIndex, null);
                    type.GetProperty("UserName").SetValue(connectionObject, tbUserName.Text, null);
                    if (cbConnectionType.SelectedIndex == 1)
                    {
                        type.GetProperty("Password").SetValue(connectionObject, tbPassword.Text, null);
                    }
                    asm = Assembly.LoadFile(Serializer.CreateInstance().applicationFolder + "dll/10.0.0.0/Microsoft.SqlServer.SqlTools.VSIntegration.dll");
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
                    connection.ServerName = travelServer.Databases[lvDatabases.SelectedIndices[0]].Server;
                    connection.AuthenticationType = cbConnectionType.SelectedIndex;
                    connection.UserName = tbUserName.Text;
                    if (connection.AuthenticationType == 1)
                    {
                        connection.Password = tbPassword.Text;
                    }
                    ServiceCache.GetObjectExplorer().ConnectToServer(connection);
                    objectExplorerService = ServiceCache.GetObjectExplorer();
                }

                //Below is to expand tree
                IExplorerHierarchy hierarchy;
                MethodInfo methodGetHierarchy = objectExplorerService.GetType().GetMethod("GetHierarchy", BindingFlags.Instance | BindingFlags.NonPublic);
                if (version == 2008)
                {
                    Assembly asm = Assembly.LoadFile(Serializer.CreateInstance().applicationFolder + "dll/10.0.0.0/Microsoft.SqlServer.ConnectionInfo.dll");
                    object sqlConnectionInfoObject = cbConnectionType.SelectedIndex == 0 ?
                        System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Common.SqlConnectionInfo"), new object[] { travelServer.Databases[lvDatabases.SelectedIndices[0]].Server }) :
                        System.Activator.CreateInstance(asm.GetType("Microsoft.SqlServer.Management.Common.SqlConnectionInfo"), new object[] { travelServer.Databases[lvDatabases.SelectedIndices[0]].Server, tbUserName.Text, tbPassword.Text }); ;
                    object hierarchyObject = methodGetHierarchy.Invoke(objectExplorerService, new object[] { sqlConnectionInfoObject, null });
                    object rootObject = hierarchyObject.GetType().GetProperty("Root").GetValue(hierarchyObject, null);
                    object treeViewObject = rootObject.GetType().GetProperty("TreeView").GetValue(rootObject, null);
                    tree = treeViewObject as TreeView;
                }
                else if (version == 2005)
                {
                    SqlConnectionInfo sqlConnectionInfo = cbConnectionType.SelectedIndex == 0 ?
                    new SqlConnectionInfo(travelServer.Databases[lvDatabases.SelectedIndices[0]].Server) :
                    new SqlConnectionInfo(travelServer.Databases[lvDatabases.SelectedIndices[0]].Server, tbUserName.Text, tbPassword.Text);
                    hierarchy = (IExplorerHierarchy)methodGetHierarchy.Invoke(objectExplorerService, new object[] { sqlConnectionInfo });
                    tree = hierarchy.Root.TreeView;
                    needRefresh = true;
                }
                //Prepare the tree to expand
                SelectAndExpandDatabasesNode();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
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

        void SelectAndExpandInstanceNode_AfterParentExpand()
        {
            int index = 0, maxMatching = 0;
            for (int i = 0; i < databaseObjectNode.Nodes.Count; i++)
            {
                databaseInstanceNode = databaseObjectNode.Nodes[i];
                if (databaseInstanceNode.Text.ToLower().Equals(tbDatabase.Text.ToLower()))
                {
                    index = i;
                    break;
                }
                int currentMatching = GetMatchingLength(databaseInstanceNode.Text.ToLower().ToCharArray(), tbInstance.Text.ToLower().ToCharArray());
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

        void SelectAndExpandTablesNode_AfterParentExpand()
        {
            for (int i = 0; i < databaseInstanceNode.Nodes.Count; i++)
            {
                tableNode = databaseInstanceNode.Nodes[i];
                if (tableNode.Text.Equals("Tables"))
                {
                    //tree.SelectedNode = tableNode;
                    tree.AfterExpand += new TreeViewEventHandler(tree_AfterTablesNodeExpand);
                    tableNode.Expand();
                    break;
                }
            }
        }

        void tree_AfterConnectionNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterConnectionNodeExpand);
            SelectAndExpandDatabasesNode_AfterParentExpand();
        }

        void tree_AfterDatabaseObjectNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterDatabaseObjectNodeExpand);
            SelectAndExpandInstanceNode_AfterParentExpand();
        }

        void tree_AfterDatabaseInstanceNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterDatabaseInstanceNodeExpand);
            SelectAndExpandTablesNode_AfterParentExpand();
        }

        void tree_AfterTablesNodeExpand(object sender, TreeViewEventArgs e)
        {
            tree.AfterExpand -= new TreeViewEventHandler(tree_AfterTablesNodeExpand);
            CreateNewScript();
        }

        void CreateNewScript()
        {
            if (needRefresh && version == 2005)
            {
                databaseObjectNode.Collapse();
                needRefresh = false;
                ServiceCache.GetObjectExplorer().ConnectToServer(connection);
                SelectAndExpandDatabasesNode();
                return;
            }
            string strFullPath = Serializer.CreateInstance().applicationFolder + "SQLFile.sql";
            if (version == 2008)
            {
                connection = new UIConnectionInfo();
                connection.ServerType = new Guid("8c91a03d-f9b4-46c0-a305-b5dcc79ff907");
                connection.ServerName = travelServer.Databases[lvDatabases.SelectedIndices[0]].Server;
                connection.AuthenticationType = cbConnectionType.SelectedIndex;
                connection.UserName = tbUserName.Text;
                if (connection.AuthenticationType == 1)
                {
                    connection.Password = tbPassword.Text;
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
                sb.AppendLine("--This Text Editor window is opened by Database Selector automatically");
                sb.AppendLine("--Server		:" + connection.ServerName);
                sb.AppendLine("--Database	    :" + sqlConnection.Database);
                sb.AppendLine("--Authentication:" + cbConnectionType.Text);
                sb.AppendLine("--UserName	    :" + tbUserName.Text);
                sb.AppendLine("*****************************************************************************/");
                sb.Append("USE " + sqlConnection.Database);
                streamWriter.Write(sb.ToString());
            }
            if (version == 2008)
            {
                Assembly asm = Assembly.LoadFile(Serializer.CreateInstance().applicationFolder + "dll/10.0.0.0/SQLEditors.dll");
                Type scriptFactoryType = asm.GetType("Microsoft.SqlServer.Management.UI.VSIntegration.Editors.ScriptFactory");
                object sfiObject = scriptFactoryType.GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                MethodInfo[] methodCreateNewScript = scriptFactoryType.GetMethods();
                methodCreateNewScript[16].Invoke(sfiObject, new object[] { strFullPath, connectionObject, sqlConnection });
                //methodCreateNewScript.Invoke(sfiObject, new object[] { strFullPath, connection, sqlConnection });
            }
            else if (version == 2005)
            {
                ScriptFactory.Instance.CreateNewScript(strFullPath, connection, sqlConnection);
            }
        }

        private void ReloadGroupListView(object sender, EventArgs e)
        {
            //lvGroups.Items.Clear();
            //System.Windows.Forms.ListView.ListViewItemCollection lvicGroup = new ListView.ListViewItemCollection(lvGroups);
            //if (groupList.groups != null)
            //{
            //    if (groupList.groups.Count != 0)
            //    {
            //        for (int i = 0; i < groupList.groups.Count; i++)
            //        {
            //            ListViewItem lviDatabase = new ListViewItem(new string[] { groupList.groups[i] });
            //            lvicGroup.Add(lviDatabase);
            //        }
            //    }
            //    else
            //    { lvicGroup.Add(new ListViewItem(new string[] { "No group found, try to click 'Reload groups'" })); }
            //    if (lvGroups.Items.Count != 0)
            //    {
            //        if (index.currentSelectedGroup >= lvGroups.Items.Count)
            //        {
            //            index.currentSelectedGroup = 7;
            //        }
            //        lvGroups.Items[index.currentSelectedGroup].Selected = true;
            //        lvGroups.Items[index.currentSelectedGroup].BackColor = SystemColors.Highlight;
            //        lvGroups.Items[index.currentSelectedGroup].ForeColor = Color.White;
            //    }
            //}
            EnableAllButtons();
        }

        private void ReloadServerListView(object sender, EventArgs e)
        {
            //lvServers.Items.Clear();
            //System.Windows.Forms.ListView.ListViewItemCollection lvic = new ListView.ListViewItemCollection(lvServers);
            //if (serverList.servers != null)
            //{
            //    if (serverList.servers.Count != 0)
            //    {
            //        for (int i = 0; i < serverList.servers.Count; i++)
            //        {
            //            ListViewItem lviServer = new ListViewItem(new string[] { serverList.servers[i].serverName, serverList.servers[i].travelServer });
            //            lvic.Add(lviServer);
            //        }
            //    }
            //    else
            //    { lvic.Add(new ListViewItem(new string[] { "No server found", "Try to click 'Reload servers'" })); }
            //    if (lvServers.Items.Count != 0)
            //    {
            //        if (index.currentSelectedServer >= lvServers.Items.Count)
            //        {
            //            index.currentSelectedServer = 0;
            //        }
            //        lvServers.Items[index.currentSelectedServer].Selected = true;
            //        lvServers.Items[index.currentSelectedServer].BackColor = SystemColors.Highlight;
            //        lvServers.Items[index.currentSelectedServer].ForeColor = Color.White;
            //    }
            //}
            EnableAllButtons();
        }

        private void ReloadDatabaseListView(object sender, EventArgs e)
        {
            //lvDatabases.Items.Clear();
            //System.Windows.Forms.ListView.ListViewItemCollection lvic = new ListView.ListViewItemCollection(lvDatabases);
            //if (travelServer.Databases != null)
            //{
            //    if (travelServer.Databases.Count != 0)
            //    {
            //        foreach (DatabaseItem database in travelServer.Databases)
            //        {
            //            ListViewItem lviDatabase = new ListViewItem(new string[] { database.DatabaseName, database.Server, database.Database, "SQL Server Authentication", database.UserName, database.UserAuth });
            //            lvic.Add(lviDatabase);
            //        }
            //    }
            //    else
            //    { lvic.Add(new ListViewItem(new string[] { "No database found", "Try to click 'Reload databases'", "", "", "", "" })); }
            //    if (lvDatabases.Items.Count != 0)
            //    {
            //        if (index.currentSelectedDatabase >= lvDatabases.Items.Count)
            //        {
            //            index.currentSelectedDatabase = 0;
            //        }
            //        lvDatabases.Items[index.currentSelectedDatabase].Selected = true;
            //        lvDatabases.Items[index.currentSelectedDatabase].BackColor = SystemColors.Highlight;
            //        lvDatabases.Items[index.currentSelectedDatabase].ForeColor = Color.White;
            //    }
            //}
            EnableAllButtons();
        }

        private void ServerInstanceSelector_Load(object sender, EventArgs e)
        {
            this.groupList.Updated += new EventHandler(this.ReloadGroupListView);
            this.serverList.Updated += new EventHandler(this.ReloadServerListView);
            this.travelServer.Updated += new EventHandler(this.ReloadDatabaseListView);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConnectionType.SelectedIndex == 0)
            {
                tbUserName.Enabled = false;
                tbPassword.Enabled = false;
                tbUserName.Text = System.Environment.UserDomainName + "\\" + System.Environment.UserName;
                tbPassword.Text = "";
            }
            else
            {
                tbUserName.Enabled = true;
                tbPassword.Enabled = true;
                tbUserName.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[4].Text;
                tbPassword.Text = lvDatabases.Items[lvDatabases.SelectedIndices[0]].SubItems[5].Text;
            }
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

                tbGroup.Text = lvGroups.Items[lvGroups.SelectedIndices[0]].SubItems[0].Text;
                serverList.groupName = lvGroups.Items[lvGroups.SelectedIndices[0]].SubItems[0].Text;
                serverList.GetServers();
                ReloadServerListView(this, EventArgs.Empty);
            }
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
                if (!lvServers.Items[lvServers.SelectedIndices[0]].SubItems[0].Text.Contains("No server found"))
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
                travelServer.GetDatabases();
                ReloadDatabaseListView(this, EventArgs.Empty);
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

                if (lvGroups.Items[lvGroups.SelectedIndices[0]].SubItems[0].Text.Equals("PPE"))
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
                    tbPassword.Text = "";
                }
            }
        }

        private void btnSaveGroups_Click(object sender, EventArgs e)
        {
            groupList.SaveListToXML();
        }

        private void btnSaveServers_Click(object sender, EventArgs e)
        {
            GroupServerList gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), "Servers.xml") as GroupServerList);
            gsl.groupServers.Remove(gsl.GetServerList(serverList.groupName));
            gsl.groupServers.Add(serverList);
            gsl.SaveListToXML();
        }

        private void btnDatabases_Click(object sender, EventArgs e)
        {
            TravelServerList tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), "Databases.xml") as TravelServerList);
            tsl.travelServers.Remove(tsl.GetTravelServer(travelServer.MachineName));
            tsl.travelServers.Add(travelServer);
            tsl.SaveListToXml();
        }

        private void btnReloadGroups_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            Thread updateThread = new Thread(new ThreadStart(groupList.GetGroupsFromWeb));
            updateThread.Start();
            //groupList.GetGroupsFromWeb();
            //ReloadGroupListView();
        }

        private void btnReloadServers_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            Thread updateThread = new Thread(new ThreadStart(serverList.GetServersFromWeb));
            updateThread.Start();
            //serverList.GetServersFromWeb();
            //ReloadServerListView();
        }

        private void btnReloadDatabases_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            Thread updateThread = new Thread(new ThreadStart(travelServer.GetDatabasesFromRegistry));
            updateThread.Start();
            //travelServer.GetDatabasesFromRegistry();
            //ReloadDatabaseListView();
        }

        private void DisableAllButtons()
        {
            this.btnReloadDatabases.Enabled = false;
            this.btnReloadGroups.Enabled = false;
            this.btnReloadServers.Enabled = false;
            this.btnSaveDatabases.Enabled = false;
            this.btnSaveGroups.Enabled = false;
            this.btnSaveServers.Enabled = false;
        }

        private void EnableAllButtons()
        {
            this.btnReloadDatabases.Enabled = true;
            this.btnReloadGroups.Enabled = true;
            this.btnReloadServers.Enabled = true;
            this.btnSaveDatabases.Enabled = true;
            this.btnSaveGroups.Enabled = true;
            this.btnSaveServers.Enabled = true;
        }

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
