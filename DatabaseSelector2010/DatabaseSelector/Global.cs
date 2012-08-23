using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SHDocVw;

namespace DatabaseSelector
{
    public class Global
    {
        #region Fileds & Properties
        public static readonly string applicationFolder = Serializer.CreateInstance().applicationFolder;
        public static readonly string logFile = "log.txt";
        public static readonly string emptyString = "";

        public static readonly string defaultALLGroupName = "ALL";
        public static readonly string defaultALLWebServerName = "ALL";
        public static readonly string defaultALLTravelServerName = "ALL";
        public static readonly string defaultPPEGroupName = "PPE";
        public static readonly string defaultALLPPEWebServerName = "ALL Ports";
        public static readonly string defaultALLPPETravelServerName = "ALL Servers";

        public static readonly string noGroupItemBanner = "No group found, try to click 'Reload groups'";
        public static readonly string noWebServerItemBanner = "No server found";
        public static readonly string noTravelServerItemBanner = "Try to click 'Reload servers'";
        public static readonly string noDatabaseItemBanner = "No database found";
        public static readonly string tryReloadDatabaseBanner = "Try to click 'Reload databases'";

        public static readonly string reloadGroupButtonName = "btnReloadGroups";
        public static readonly string reloadServerButtonName = "btnReloadServers";
        public static readonly string reloadDatabaseButtonName = "btnReloadDatabases";
        public static readonly string reloadAllButtonName = "btnReloadAll";

        public static readonly string reloadingGroupBanner = "Reloading groups";
        public static readonly string reloadingServerBanner = "Reloading server pairs of ";
        public static readonly string reloadingDatabaseBanner = "Reloading databases of ";

        public static readonly string defaultGroupsFileName = "Groups.xml";
        public static readonly string defaultMyGroupsFileName = "MyGroups.xml";
        public static readonly string defaultServersFileName = "Servers.xml";
        public static readonly string defaultDatabasesFileName = "Databases.xml";
        public static readonly string defaultGroupsFile = applicationFolder + defaultGroupsFileName;
        public static readonly string defaultMyGroupsFile = applicationFolder + defaultMyGroupsFileName;
        public static readonly string defaultServersFile = applicationFolder + defaultServersFileName;
        public static readonly string defaultDatabasesFile = applicationFolder + defaultDatabasesFileName;

        public static readonly string defaultTEMCURL = @"http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";
        public static readonly string defaultXLSFileName = "PPE_DSN_List.xls";
        public static readonly string defaultTXTFileName = "WingatePortMappingsForRTT_PPE.txt";
        public static readonly string defaultIndexFileName = "Index.xml";
        public static readonly string defaultXLSFile = applicationFolder + defaultXLSFileName;
        public static readonly string defaultTXTFile = applicationFolder + defaultTXTFileName;
        public static readonly string defaultIndexFile = applicationFolder + defaultIndexFileName;

        public static readonly string dllFolderFor90 = applicationFolder + "dll/9.0.242.0/";
        public static readonly string dllFolderFor100 = applicationFolder + "dll/10.0.0.0/";
        #endregion

        public static void WriteLog(string lines)
        {
            try
            {
                File.AppendAllText(applicationFolder + logFile, DateTime.Now + "\n" + lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void SetProgressBarText(ProgressBar target, string text)
        {
            if (target == null) { throw new ArgumentException("Null Target"); }
            if (string.IsNullOrEmpty(text))
            {
                int percent = (int)(((double)(target.Value - target.Minimum) / (double)(target.Maximum - target.Minimum)) * 100);
                text = percent.ToString() + "%";
            }
            using (Graphics graphics = target.CreateGraphics())
            {
                graphics.DrawString(text, ProgressBar.DefaultFont, new SolidBrush(Color.Red),
                    new PointF(target.Width / 2 - (graphics.MeasureString(text, ProgressBar.DefaultFont).Width / 2.0F),
                        target.Height / 2 - (graphics.MeasureString(text, ProgressBar.DefaultFont).Height / 2.0F)));
            }
        }
    }

    class UpdateThread
    {
        private static UpdateThread instance;

        public bool inProgress;
        public Thread thread;

        public event EventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public static UpdateThread CreateInstance()
        {
            if (instance == null)
            {
                instance = new UpdateThread();
            }
            return instance;
        }

        public UpdateThread()
        {
            inProgress = false;
            thread = null;
        }

        public Thread CreateNewThread()
        {
            thread = new Thread(new ThreadStart(ReloadGroupServerDatabaseListAndSaveToXML));
            return thread;
        }

        public void ReloadGroupServerDatabaseListAndSaveToXML()
        {
            inProgress = true;
            OnUpdated(EventArgs.Empty);

            GroupList groupList = GroupList.instance;
            GroupServerList gsl;
            ServerList serverList = new ServerList();
            TravelServerList tsl;
            TravelServer travelServer = new TravelServer();

            InternetExplorer ie = new InternetExplorer();
            groupList.GetGroupsFromWeb(ie, false);
            groupList.SaveListToXML();
            groupList.groups.Add(Global.defaultPPEGroupName);

            foreach (string group in groupList.groups)
            {
                //Get server pairs from files or web site
                serverList.groupName = group;
                if (group.Equals(Global.defaultALLGroupName)) continue;
                else if (group.Equals(Global.defaultPPEGroupName))
                { serverList.GetServersFromFile(group, null); }
                else
                { serverList.GetServersFromWeb(ie, false); }
                //Save server pairs to XML file
                if (File.Exists(Global.defaultServersFile))
                {
                    gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), Global.defaultServersFileName) as GroupServerList);
                    gsl.groupServers.Remove(gsl.GetServerList(serverList.groupName));
                }
                else
                { gsl = GroupServerList.CreateInstance(); }
                gsl.groupServers.Add(serverList);
                gsl.SaveListToXML();

                foreach (Server serverPair in serverList.servers)
                {
                    //No need to save databases for 'ALL Servers'
                    if (serverPair.travelServer.Equals(Global.defaultALLPPETravelServerName)) continue;

                    //Get databases from registry
                    travelServer.MachineName = serverPair.travelServer;
                    travelServer.GetDatabasesFromRegistryAndChangeProgressBar(null);
                    //Save databases to XML file
                    if (File.Exists(Global.defaultDatabasesFile))
                    {
                        tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), Global.defaultDatabasesFileName) as TravelServerList);
                        tsl.travelServers.Remove(tsl.GetTravelServer(travelServer.MachineName));
                    }
                    else
                    { tsl = TravelServerList.CreateInstance(); }
                    tsl.travelServers.Add(travelServer);
                    tsl.SaveListToXml();
                }
            }

            ie.Quit();
            inProgress = false;
            OnUpdated(EventArgs.Empty);
        }

    }
}
