using System.Collections.Generic;

namespace DatabaseSelector
{
    public class TravelServerList
    {
        private static TravelServerList instance;
        public List<TravelServer> travelServers = null;

        public static TravelServerList CreateInstance()
        {
            if (instance == null)
            {
                instance = new TravelServerList();
            }
            return instance;
        }

        public TravelServerList()
        {
            travelServers = new List<TravelServer>();
        }

        public TravelServer GetTravelServer(string machineName)
        {
            TravelServer result = new TravelServer(machineName);
            result.updateDate = System.DateTime.Now;
            result.Databases = new List<DatabaseItem>();

            if (machineName.Equals(Global.defaultALLTravelServerName))
            {
                foreach (TravelServer travelServer in travelServers)
                {
                    foreach (DatabaseItem di in travelServer.Databases)
                    { result.Databases.Add(di); }
                }
            }
            else if (machineName.Equals(Global.defaultALLPPETravelServerName))
            {
                TXTReader txtReader = TXTReader.CreateInstance();
                Dictionary<string, int> pairs = txtReader.GetServerPortPair();
                foreach (TravelServer travelServer in travelServers)
                {
                    if (pairs.ContainsKey(travelServer.MachineName))
                    {
                        foreach (DatabaseItem di in travelServer.Databases)
                        { result.Databases.Add(di); }
                    }
                }
            }
            else
            {
                foreach (TravelServer travelServer in travelServers)
                {
                    if (travelServer.MachineName.Equals(machineName))
                    {
                        result = travelServer;
                        break;
                    }
                }
            }
            return result;
        }

        public TravelServerList GetListFromXml()
        {
            return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultDatabasesFileName) as TravelServerList;
        }

        public void SaveListToXml()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultDatabasesFileName);
        }

    }

    public class GroupServerList
    {
        private static GroupServerList instance;
        public List<ServerList> groupServers = null;

        public static GroupServerList CreateInstance()
        {
            if (instance == null)
            {
                instance = new GroupServerList();
            }
            return instance;
        }

        public GroupServerList()
        {
            groupServers = new List<ServerList>();
        }

        public ServerList GetServerList(string groupName)
        {
            ServerList result = new ServerList();
            result.groupName = Global.defaultALLGroupName;
            result.updateDate = System.DateTime.Now;
            result.servers = new List<Server>();

            if (groupName.Equals(Global.defaultALLGroupName))
            {
                foreach (ServerList groupServer in groupServers)
                {
                    if (groupServer.updateDate < result.updateDate)
                    { result.updateDate = groupServer.updateDate; }
                    foreach (Server server in groupServer.servers)
                    { result.servers.Add(server); }
                }
            }
            else
            {
                foreach (ServerList groupServer in groupServers)
                {
                    if (groupServer.groupName.Equals(groupName))
                    {
                        result = groupServer;
                        break;
                    }
                }
            }
            return result;
        }

        public GroupServerList GetListFromXML()
        {
            return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultServersFileName) as GroupServerList;
        }

        public void SaveListToXML()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultServersFileName);
        }

    }

}
