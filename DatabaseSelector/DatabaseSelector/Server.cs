using System;
using System.Collections.Generic;
using System.IO;
using mshtml;
using SHDocVw;

namespace DatabaseSelector
{
    public class ServerList
    {
        //private string selectedServer;
        public string groupName;
        public List<Server> servers;

        public ServerList()
        {
            //selectedServer = null;
            groupName = null;
            servers = null;
        }

        public ServerList(string groupName)
        {
            this.groupName = groupName;
        }

        public void GetServers()
        {
            GroupServerList gsl;
            if (File.Exists(Serializer.CreateInstance().applicationFolder + "Servers.xml"))
            {
                gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), "Servers.xml") as GroupServerList);
                if (gsl == null)
                {
                    gsl = GroupServerList.CreateInstance();
                    //gsl = new GroupServerList();
                    ServerList newServerList = new ServerList(groupName);
                    newServerList.GetServersFromWeb();
                    gsl.groupServers.Add(newServerList);
                    gsl.SaveListToXML();
                }
                if (gsl.GetServerList(groupName) == null)
                {
                    ServerList newServerList = new ServerList(groupName);
                    newServerList.GetServersFromWeb();
                    gsl.groupServers.Add(newServerList);
                    gsl.SaveListToXML();
                }
                if (gsl.GetServerList(groupName).servers == null)
                {
                    ServerList newServerList = new ServerList(groupName);
                    newServerList.GetServersFromWeb();
                    gsl.GetServerList(groupName).servers = newServerList.servers;
                    gsl.SaveListToXML();
                }
                //if (gsl.GetServerList(groupName).servers.Count == 0)
                //{
                //    gsl.GetServerList(groupName).GetServersFromWeb(ie);
                //    gsl.SaveListToXML();
                //}
            }
            else
            {
                try
                {
                    gsl = GroupServerList.CreateInstance();
                    //gsl = new GroupServerList();
                    ServerList newServerList = new ServerList(groupName);
                    newServerList.GetServersFromWeb();
                    gsl.groupServers.Add(newServerList);
                    gsl.SaveListToXML();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            this.servers = gsl.GetServerList(groupName).servers;
        }

        public void GetServersFromWeb()
        {
            if (!groupName.Equals("PPE"))
            {
                InternetExplorer IE = new InternetExplorer();
                try
                {
                    object Empty = 0;
                    object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi?query=ON&group=" + groupName;

                    IE.Visible = false;
                    IE.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

                    System.Threading.Thread.Sleep(5000);

                    while (IE.Busy)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }

                    IHTMLDocument3 document = (IHTMLDocument3)IE.Document;
                    HTMLTable queryTable = (HTMLTable)document.getElementById("query_table");
                    servers = new List<Server>();
                    for (int i = 1; i < queryTable.rows.length; i++)
                    {
                        HTMLTableRow row = (HTMLTableRow)queryTable.rows.item(i, i);
                        HTMLTableCell serverCell = (HTMLTableCell)row.cells.item(0, 0);
                        HTMLTableCell travelServerCell = (HTMLTableCell)row.cells.item(4, 4);
                        if (serverCell.innerText != null && !serverCell.innerText.Equals("") && travelServerCell.innerText != null && !travelServerCell.innerText.Equals(""))
                        {
                            servers.Add(new Server(serverCell.innerText, travelServerCell.innerText));
                        }
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.StackTrace); }
                finally
                { IE.Quit(); }
            }
            else
            {
                servers = new List<Server>();
                servers.Add(new Server("All", "All"));
                TXTReader txtReader = TXTReader.CreateInstance();
                Dictionary<string, int> pairs = txtReader.GetServerPortPair();
                foreach (string server in pairs.Keys)
                {
                    servers.Add(new Server(txtReader.GetPortByServerName(server).ToString(), server));
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
