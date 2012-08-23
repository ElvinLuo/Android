using System;
using System.Collections.Generic;
using System.IO;
using mshtml;
using SHDocVw;

namespace DatabaseSelector
{
    public class GroupList
    {
        public static readonly GroupList instance = new GroupList();
        public string selectedGroup;
        public DateTime updateDate;
        public List<string> groups;
        public event EventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public GroupList()
        {
            selectedGroup = "CHE-RC01";
        }

        public GroupList(string defaultGroup)
        {
            this.selectedGroup = defaultGroup;
        }

        public int FindGroup(string groupName)
        {
            int index = -1;
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Equals(groupName))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void SaveListToXML()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultGroupsFileName);
        }

        public void GetGroups()
        {
            if (File.Exists(Global.defaultGroupsFile))
            {
                GroupList gl = Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultGroupsFileName) as GroupList;
                groups = gl.groups;
                selectedGroup = gl.selectedGroup;
                updateDate = gl.updateDate;

                //List<string> groupList = gl.groups;
                //if (groupList.Count == 0)
                //{ GetGroupsFromWebAndSaveToXML(); }
                //groups = new List<string>();
                //if (!groupList.Contains(Global.defaultALLGroupName))
                //{ groups.Add(Global.defaultALLGroupName); }
                //if (!groupList.Contains(Global.defaultPPEGroupName))
                //{ groups.Add(Global.defaultPPEGroupName); }
                //foreach (string item in groupList)
                //    groups.Add(item);
            }
            //else
            //{ GetGroupsFromWebAndSaveToXML(); }
        }

        public void GetGroupsFromWebAndSaveToXML()
        {
            InternetExplorer ie = new InternetExplorer();
            GetGroupsFromWeb(ie, false);
            ie.Quit();
            SaveListToXML();
        }

        public void GetGroupsFromWeb(InternetExplorer ie, bool visible)
        {
            try
            {
                groups = new List<string>();
                //groups.Add(Global.defaultALLGroupName);
                //groups.Add(Global.defaultPPEGroupName);

                object Empty = 0;
                object URL = Index.CreateInstance().TEMCURL;

                ie.Visible = visible;
                ie.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

                while (ie.Busy)
                { System.Threading.Thread.Sleep(1000); }

                IHTMLDocument3 document = (IHTMLDocument3)ie.Document;
                if (document != null)
                {
                    HTMLSelectElement selGroups = (HTMLSelectElement)document.getElementById("group");
                    if (selGroups != null)
                    {
                        for (int i = 0; i < selGroups.length; i++)
                        {
                            HTMLOptionElement option = (HTMLOptionElement)selGroups.item(i, i);
                            if (!string.IsNullOrEmpty(option.text))
                            { groups.Add(option.text); }
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
