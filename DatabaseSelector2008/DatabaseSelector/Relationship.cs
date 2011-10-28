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
            TravelServer result = null;
            foreach (TravelServer travelServer in travelServers)
            {
                if (travelServer.MachineName.Equals(machineName))
                {
                    result = travelServer;
                    break;
                }
            }
            return result;
        }

        public TravelServerList GetListFromXml()
        {
            return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), "Databases.xml") as TravelServerList;
        }

        public void SaveListToXml()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), "Databases.xml");
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
            ServerList result = null;
            foreach (ServerList groupServer in groupServers)
            {
                if (groupServer.groupName.Equals(groupName))
                {
                    result = groupServer;
                    break;
                }
            }
            return result;
        }

        public GroupServerList GetListFromXML()
        {
            return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), "Servers.xml") as GroupServerList;
        }

        public void SaveListToXML()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), "Servers.xml");
        }

    }

}
