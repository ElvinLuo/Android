using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace DatabaseSelector
{
    public class DatabaseItem
    {
        public string DatabaseName;
        public string Database;
        public string Description;
        public string Server;
        public string UserAuth;
        public string UserName;

        public DatabaseItem()
        {
            DatabaseName = "LodgingInventory";
            Database = "LodgingInventory";
            Description = "Travel Group Database";
            Server = "LodgingInventoryDB.CHELTVMAPC01.bgb.karmalab.net,1433";
            UserAuth = "travel";
            UserName = "TravInventory";
        }

        public DatabaseItem(string databaseName, string database, string description, string server, string userAuth, string userName)
        {
            DatabaseName = databaseName;
            Database = database;
            Description = description;
            Server = server;
            UserAuth = userAuth;
            UserName = userName;
        }
    }

    public class TravelServer
    {
        public string MachineName;
        public List<DatabaseItem> Databases;

        public TravelServer()
        {
            MachineName = "CHELTVHHTL01";
            Databases = null;
        }

        public TravelServer(string server)
        {
            MachineName = server;
            Databases = null;
        }

        public void GetDatabases()
        {
            TravelServerList tsl;
            if (File.Exists(Serializer.CreateInstance().applicationFolder + "Databases.xml"))
            {
                tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), "Databases.xml") as TravelServerList);
                if (tsl == null)
                {
                    tsl = TravelServerList.CreateInstance();
                    //tsl = new TravelServerList();
                    TravelServer newTravelServer = new TravelServer(MachineName);
                    newTravelServer.GetDatabasesFromRegistry();
                    tsl.travelServers.Add(newTravelServer);
                    tsl.SaveListToXml();
                }
                if (tsl.GetTravelServer(MachineName) == null)
                {
                    TravelServer newTravelServer = new TravelServer(MachineName);
                    newTravelServer.GetDatabasesFromRegistry();
                    tsl.travelServers.Add(newTravelServer);
                    tsl.SaveListToXml();
                }
                if (tsl.GetTravelServer(MachineName).Databases == null)
                {
                    TravelServer newTravelServer = new TravelServer(MachineName);
                    newTravelServer.GetDatabasesFromRegistry();
                    tsl.travelServers.Add(newTravelServer);
                    tsl.SaveListToXml();
                }
                //if (tsl.GetTravelServer(MachineName).Databases.Count == 0)
                //{
                //    tsl.GetTravelServer(MachineName).GetDatabasesFromRegistry();
                //    tsl.SaveListToXml();
                //}
            }
            else
            {
                try
                {
                    tsl = TravelServerList.CreateInstance();
                    //tsl = new TravelServerList();
                    TravelServer newTravelServer = new TravelServer(MachineName);
                    newTravelServer.GetDatabasesFromRegistry();
                    tsl.travelServers.Add(newTravelServer);
                    tsl.SaveListToXml();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            this.Databases = tsl.GetTravelServer(MachineName).Databases;
        }

        public void GetDatabasesFromRegistry()
        {
            TXTReader txtReader = TXTReader.CreateInstance();
            Dictionary<string, int> pairs = txtReader.GetServerPortPair();
            if (MachineName.Equals("All") || pairs.ContainsKey(MachineName))
            {
                XLSReader xlsReader = XLSReader.CreateInstance();
                Databases = xlsReader.GetTravelServerFromXLS(MachineName).Databases;
            }
            else
            {
                Databases = new List<DatabaseItem>();

                RegistryKey expDsnKey;
                RegistryKey expDsnSubKey;

                try
                {
                    expDsnKey = RegistryKey.OpenRemoteBaseKey(
                        RegistryHive.LocalMachine, MachineName.Trim()).OpenSubKey(
                        "SOFTWARE\\Expedia\\shared\\Database\\ExpDsn");
                }
                catch (IOException e)
                {
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                    return;
                }

                foreach (string subKeyName in expDsnKey.GetSubKeyNames())
                {
                    expDsnSubKey = expDsnKey.OpenSubKey(subKeyName);
                    string[] items = expDsnSubKey.GetValueNames();
                    bool[] matching = Match(new string[] { "Database", "Description", "Server", "UserAuth", "UserName" }, items);
                    DatabaseItem databaseItem = new DatabaseItem(
                        subKeyName,
                        matching[0] ? expDsnSubKey.GetValue("Database").ToString() : "",
                        matching[1] ? expDsnSubKey.GetValue("Description").ToString() : "",
                        matching[2] ? expDsnSubKey.GetValue("Server").ToString() : "",
                        matching[3] ? expDsnSubKey.GetValue("UserAuth").ToString() : "",
                        matching[4] ? expDsnSubKey.GetValue("UserName").ToString() : "");
                    //DatabaseItem databaseItem = new DatabaseItem();
                    Databases.Add(databaseItem);
                }

                expDsnKey.Close();
            }
        }

        public bool[] Match(string[] first, string[] second)
        {
            bool[] result = new bool[first.Length];
            for (int i = 0; i < first.Length; i++)
            {
                result[i] = false;
                for (int j = 0; j < second.Length; j++)
                {
                    if (second[j].Equals(first[i]))
                    {
                        result[i] = true;
                        break;
                    }
                }
            }
            return result;
        }

        public void SerializeToXML(string file)
        {
            XmlSerializer x = new XmlSerializer(this.GetType());
            TextWriter text = new StreamWriter(file);
            x.Serialize(text, this);
            text.Flush();
            text.Close();
        }

        public TravelServer DeserializeFromXML(string file)
        {
            XmlSerializer x = new XmlSerializer(this.GetType());
            TextReader text = new StreamReader(file);
            TravelServer instance = x.Deserialize(text) as TravelServer;
            return instance;
        }

    }
}
