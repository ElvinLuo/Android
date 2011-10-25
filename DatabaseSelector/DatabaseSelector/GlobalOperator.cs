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

        public bool uiVisiable;
        public bool inProgress;
        public int maxValue;
        public int currentValue;
        public Button button;
        public ProgressBar progressBar;
        public Thread thread;

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
            uiVisiable = false;
            inProgress = false;
            maxValue = 100;
            currentValue = 0;
            button = null;
            progressBar = null;
            thread = new Thread(new ThreadStart(ReloadGroupServerDatabaseListAndSaveToXML));
        }

        public UpdateThread(Button btn, ProgressBar pgb)
        {
            button = btn;
            progressBar = pgb;
        }

        public void ReloadGroupServerDatabaseListAndSaveToXML()
        {
            inProgress = true;
            int stopCount = 0;  //To be removed.

            if (uiVisiable && button != null) button.Invoke((MethodInvoker)delegate { button.Enabled = false; });
            if (uiVisiable && progressBar != null) progressBar.Invoke((MethodInvoker)delegate { progressBar.Visible = true; });

            GroupList groupList = GroupList.instance;
            GroupServerList gsl;
            ServerList serverList = new ServerList();
            TravelServerList tsl;
            TravelServer travelServer = new TravelServer();

            InternetExplorer ie = new InternetExplorer();
            //GlobalOperator.SetProgressBarText(progressBar, "Reloading groups");
            groupList.GetGroupsFromWeb(ie, true);
            groupList.SaveListToXML();

            maxValue = groupList.groups.Count;
            if (uiVisiable && progressBar != null) { progressBar.Invoke((MethodInvoker)delegate { progressBar.Maximum = groupList.groups.Count; }); }

            foreach (string group in groupList.groups)
            {
                stopCount += 1; //To be removed.
                if (uiVisiable && progressBar != null) { progressBar.Invoke((MethodInvoker)delegate { progressBar.PerformStep(); }); }
                if (progressBar != null) currentValue = progressBar.Value;

                GlobalOperator.SetProgressBarText(progressBar, "Reloading server pairs of " + group);
                //Get server pairs from files or web site
                serverList.groupName = group;
                if (group.ToUpper().Equals("PPE"))
                { serverList.GetServersFromFile(null); }
                else
                { serverList.GetServersFromWeb(ie, true); }
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
                    //GlobalOperator.SetProgressBarText(progressBar, "Reloading databases of " + serverPair.travelServer);
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

                if (stopCount == 3) break;  //To be removed.
            }
            ie.Quit();
            if (uiVisiable && progressBar != null) progressBar.Invoke((MethodInvoker)delegate { progressBar.Visible = false; });
            if (uiVisiable && button != null) button.Invoke((MethodInvoker)delegate { button.Enabled = true; });
            inProgress = false;
        }

    }
}
