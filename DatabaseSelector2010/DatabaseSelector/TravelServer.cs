using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
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
        public DateTime updateDate;
        public List<DatabaseItem> Databases;
        public event EventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

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
            try
            {
                TravelServerList tsl = null;
                updateDate = DateTime.MinValue;
                Databases = null;
                if (File.Exists(Global.defaultDatabasesFile))
                { tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), Global.defaultDatabasesFileName) as TravelServerList); }
                if (tsl == null)
                { tsl = TravelServerList.CreateInstance(); }
                if (tsl.GetTravelServer(MachineName) != null && tsl.GetTravelServer(MachineName).Databases.Count != 0)
                {
                    updateDate = tsl.GetTravelServer(MachineName).updateDate;
                    Databases = tsl.GetTravelServer(MachineName).Databases;
                }
            }
            catch (Exception exception)
            {
                Global.WriteLog(exception.StackTrace);
            }
        }

        public void GetDatabasesFromRegistryAndChangeProgressBar(ProgressBar pgb)
        {
            try
            {
                TXTReader txtReader = TXTReader.CreateInstance();
                Dictionary<string, int> pairs = txtReader.GetServerPortPair();
                if (MachineName.Equals(Global.defaultALLTravelServerName)) return;
                else if (MachineName.Equals(Global.defaultALLPPETravelServerName) || pairs.ContainsKey(MachineName))
                {
                    XLSReader xlsReader = XLSReader.CreateInstance();
                    Databases = xlsReader.GetTravelServerFromXLSAndChangeProgressBar(MachineName, pgb).Databases;
                }
                else
                {
                    int max = 0;
                    if (pgb != null) max = pgb.Maximum;
                    Databases = new List<DatabaseItem>();
                    RegistryKey expDsnKey;
                    RegistryKey expDsnSubKey;
                    expDsnKey = RegistryKey.OpenRemoteBaseKey(
                        RegistryHive.LocalMachine, MachineName.Trim()).OpenSubKey(
                        "SOFTWARE\\Expedia\\shared\\Database\\ExpDsn");
                    if (expDsnKey != null)
                    {
                        if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = expDsnKey.ValueCount; });
                        foreach (string subKeyName in expDsnKey.GetSubKeyNames())
                        {
                            expDsnSubKey = expDsnKey.OpenSubKey(subKeyName);
                            if (expDsnSubKey != null)
                            {
                                string[] items = expDsnSubKey.GetValueNames();
                                bool[] matching = Match(new string[] { "Database", "Description", "Server", "UserAuth", "UserName" }, items);
                                DatabaseItem databaseItem = new DatabaseItem(
                                    subKeyName,
                                    matching[0] ? expDsnSubKey.GetValue("Database").ToString() : Global.emptyString,
                                    matching[1] ? expDsnSubKey.GetValue("Description").ToString() : Global.emptyString,
                                    matching[2] ? expDsnSubKey.GetValue("Server").ToString() : Global.emptyString,
                                    matching[3] ? expDsnSubKey.GetValue("UserAuth").ToString() : Global.emptyString,
                                    matching[4] ? expDsnSubKey.GetValue("UserName").ToString() : Global.emptyString);
                                Databases.Add(databaseItem);
                                if (pgb != null)
                                {
                                    pgb.Invoke((MethodInvoker)delegate
                                        {
                                            pgb.PerformStep();
                                            Global.SetProgressBarText(pgb, Global.reloadingDatabaseBanner + MachineName);
                                        });
                                }
                            }
                        }
                        expDsnKey.Close();
                    }
                    if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = max; });
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
