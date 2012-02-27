// -----------------------------------------------------------------------
// <copyright file="MyGroupList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DatabaseSelector
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MyGroupList
    {
        public static readonly MyGroupList instance = new MyGroupList();
        public string selectedGroup;
        public DateTime updateDate;
        public List<string> groups;
        public event EventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public MyGroupList()
        {
            selectedGroup = "CHE-RC01";
        }

        public MyGroupList(string defaultGroup)
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
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultMyGroupsFileName);
        }

        public void GetGroups()
        {
            if (File.Exists(Global.defaultMyGroupsFile))
            {
                MyGroupList gl = Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultMyGroupsFileName) as MyGroupList;
                groups = gl.groups;
                groups.Sort();
                selectedGroup = gl.selectedGroup;
                updateDate = gl.updateDate;
            }
        }

    }
}
