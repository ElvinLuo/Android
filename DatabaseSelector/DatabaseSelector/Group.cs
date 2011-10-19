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
        public string defaultGroup;
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
            defaultGroup = "CHE-RC01";
        }

        public GroupList(string defaultGroup)
        {
            this.defaultGroup = defaultGroup;
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
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), "Groups.xml");
        }

        public void GetGroups()
        {
            if (File.Exists(Serializer.CreateInstance().applicationFolder + "Groups.xml"))
            {
                GroupList gl = Serializer.CreateInstance().DeserializeFromXML(this.GetType(), "Groups.xml") as GroupList;
                List<string> groupList = gl.groups;
                defaultGroup = gl.defaultGroup;
                updateDate = gl.updateDate;

                if (groupList.Count == 0)
                {
                    GetGroupsFromWeb();
                    SaveListToXML();
                }
                groups = new List<string>();
                if (!groupList[0].Equals("PPE"))
                {

                    groups.Add("PPE");
                    foreach (string item in groupList)
                        groups.Add(item);
                    SaveListToXML();
                }
                else
                {
                    foreach (string item in groupList)
                        groups.Add(item);
                }
            }
            else
            {
                GetGroupsFromWeb();
                SaveListToXML();
            }
        }

        public void GetGroupsFromWeb()
        {
            InternetExplorer IE = new InternetExplorer();
            object Empty = 0;
            object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";

            IE.Visible = false;
            IE.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

            while (IE.Busy)
            {
                System.Threading.Thread.Sleep(1000);
            }

            IHTMLDocument3 document = (IHTMLDocument3)IE.Document;
            HTMLSelectElement selGroups = (HTMLSelectElement)document.getElementById("group");
            groups = new List<string>();
            groups.Add("PPE");
            for (int i = 0; i < selGroups.length; i++)
            {
                HTMLOptionElement option = (HTMLOptionElement)selGroups.item(i, i);
                if (option.text != null && !option.text.Equals(""))
                    groups.Add(option.text);
            }
            IE.Quit();
            updateDate = DateTime.Now;
            OnUpdated(EventArgs.Empty);
        }

    }

}
