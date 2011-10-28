using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SHDocVw;
using System.Threading;

namespace DatabaseSelector
{
    class GlobalOperator
    {
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

            foreach (string group in groupList.groups)
            {
                //Get server pairs from files or web site
                serverList.groupName = group;
                if (group.ToUpper().Equals("PPE"))
                { serverList.GetServersFromFile(null); }
                else
                { serverList.GetServersFromWeb(ie, false); }
                //Save server pairs to XML file
                if (File.Exists(Serializer.CreateInstance().applicationFolder + "Servers.xml"))
                {
                    gsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(GroupServerList), "Servers.xml") as GroupServerList);
                    gsl.groupServers.Remove(gsl.GetServerList(serverList.groupName));
                }
                else
                { gsl = GroupServerList.CreateInstance(); }
                gsl.groupServers.Add(serverList);
                gsl.SaveListToXML();

                foreach (Server serverPair in serverList.servers)
                {
                    //Get databases from registry
                    travelServer.MachineName = serverPair.travelServer;
                    travelServer.GetDatabasesFromRegistryAndChangeProgressBar(null);
                    //Save databases to XML file
                    if (File.Exists(Serializer.CreateInstance().applicationFolder + "Databases.xml"))
                    {
                        tsl = (Serializer.CreateInstance().DeserializeFromXML(typeof(TravelServerList), "Databases.xml") as TravelServerList);
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
