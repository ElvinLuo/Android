using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using mshtml;
using SHDocVw;

namespace DatabaseSelector
{
    public class ServerList
    {
        public string groupName;
        public DateTime updateDate;
        public List<Server> servers;
        public event EventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public ServerList()
        {
            groupName = null;
            servers = null;
        }

        public ServerList(string groupName)
        {
            this.groupName = groupName;
        }

        public void GetServers()
        {
            try
            {
                GroupServerList gsl = null;
                updateDate = DateTime.MinValue;
                servers = null;
                if (File.Exists(Global.defaultServersFile))
                { gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), Global.defaultServersFileName) as GroupServerList); }
                if (gsl == null)
                { gsl = GroupServerList.CreateInstance(); }
                if (gsl.GetServerList(groupName) != null && gsl.GetServerList(groupName).servers.Count != 0)
                {
                    updateDate = gsl.GetServerList(groupName).updateDate;
                    servers = gsl.GetServerList(groupName).servers;
                }
            }
            catch (Exception exception)
            {
                Global.WriteLog(exception.StackTrace);
            }
        }

        public void GetServersFromFile(string groupName, ProgressBar pgb)
        {
            int max = 0;
            if (pgb != null) max = pgb.Maximum;

            servers = new List<Server>();
            if (groupName.Equals(Global.defaultALLGroupName))
            {
                return;
                //    if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = 1; });
                //    servers.Add(new Server(Global.defaultALLWebServerName, Global.defaultALLTravelServerName));
                //    if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.PerformStep(); });
            }
            else if (groupName.Equals(Global.defaultPPEGroupName))
            {
                servers.Add(new Server(Global.defaultALLPPEWebServerName, Global.defaultALLPPETravelServerName));
                TXTReader txtReader = TXTReader.CreateInstance();
                Dictionary<string, int> pairs = txtReader.GetServerPortPair();
                if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = pairs.Count; });
                foreach (string server in pairs.Keys)
                {
                    servers.Add(new Server(txtReader.GetPortByServerName(server).ToString(), server));
                    if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.PerformStep(); });
                }
            }
            updateDate = DateTime.Now;
            OnUpdated(EventArgs.Empty);

            if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = max; });
        }

        public void GetServersFromWeb(InternetExplorer ie, bool visible)
        {
            if (!groupName.Equals(Global.defaultPPEGroupName) && !groupName.Equals(Global.defaultALLGroupName))
            {
                try
                {
                    object Empty = 0;
                    object URL = Index.CreateInstance().TEMCURL + "?query=ON&group=" + groupName;

                    ie.Visible = visible;
                    ie.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

                    System.Threading.Thread.Sleep(5000);

                    while (ie.Busy)
                    { System.Threading.Thread.Sleep(1000); }

                    IHTMLDocument3 document = (IHTMLDocument3)ie.Document;
                    if (document != null)
                    {
                        HTMLTable queryTable = (HTMLTable)document.getElementById("query_table");
                        if (queryTable != null && queryTable.rows != null && queryTable.rows.length > 1)
                        {
                            servers = new List<Server>();
                            for (int i = 1; i < queryTable.rows.length; i++)
                            {
                                HTMLTableRow row = (HTMLTableRow)queryTable.rows.item(i, i);
                                if (row != null && row.cells != null && row.cells.length > 4)
                                {
                                    HTMLTableCell serverCell = (HTMLTableCell)row.cells.item(0, 0);
                                    HTMLTableCell travelServerCell = (HTMLTableCell)row.cells.item(4, 4);
                                    if (serverCell != null && !string.IsNullOrEmpty(serverCell.innerText) &&
                                        travelServerCell != null && !string.IsNullOrEmpty(travelServerCell.innerText))
                                    {
                                        foreach (string singleTravelServer in travelServerCell.innerText.Split(new char[] { ',' }))
                                        { servers.Add(new Server(serverCell.innerText, singleTravelServer)); }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Global.WriteLog(exception.StackTrace);
                }
                finally
                {
                    updateDate = DateTime.Now;
                    OnUpdated(EventArgs.Empty);
                }
            }
        }

    }

    public class Server
    {
        public string serverName;
        public string travelServer;

        public Server()
        {
        }

        public Server(string serverName)
        {
            this.serverName = serverName;
        }

        public Server(string serverName, string travelServer)
        {
            this.serverName = serverName;
            this.travelServer = travelServer;
        }

    }
}
