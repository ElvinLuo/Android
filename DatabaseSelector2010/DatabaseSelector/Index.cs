using System.IO;

namespace DatabaseSelector
{
    public class Index
    {
        private static Index instance;

        #region Fileds & Properties

        private string temcurl;
        /// <summary>
        /// Gets or sets the url of temc
        /// </summary>
        public string TEMCURL
        {
            get
            {
                if (string.IsNullOrEmpty(temcurl))
                { return Global.defaultTEMCURL; }
                else
                { return temcurl; }
            }
            set { temcurl = value; }
        }

        private string xlsFile;
        /// <summary>
        /// Gets or sets xlsFile
        /// </summary>
        public string XLSFile
        {
            get
            {
                if (string.IsNullOrEmpty(xlsFile))
                { return Global.defaultXLSFile; }
                else
                { return xlsFile; }
            }
            set { xlsFile = value; }
        }

        private string txtFile;
        /// <summary>
        /// Gets or sets txtFile
        /// </summary>
        public string TXTFile
        {
            get
            {
                if (string.IsNullOrEmpty(txtFile))
                { return Global.defaultTXTFile; }
                else
                { return txtFile; }
            }
            set { txtFile = value; }
        }

        public string groupFilter;
        public string webServerFilter;
        public string travelServerFilter;
        public string databaseFilter;

        public int connectionType;
        public string username;
        public string password;
        public bool automaticallyOpenEditer;

        public int selectedTab;
        public int previousSelectedGroup;
        public int currentSelectedGroup;
        public int previousSelectedMyGroup;
        public int currentSelectedMyGroup;
        public int previousSelectedServer;
        public int currentSelectedServer;
        public int previousSelectedDatabase;
        public int currentSelectedDatabase;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Index CreateInstance()
        {
            if (instance == null)
            {
                instance = new Index();
                instance = instance.GetIndexFromXml();
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public Index()
        {
        }

        /// <summary>
        /// Get Index instacne from xml file
        /// </summary>
        /// <returns></returns>
        public Index GetIndexFromXml()
        {
            if (File.Exists(Global.defaultIndexFile))
                return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultIndexFileName) as Index;
            else
                return this;
        }

        /// <summary>
        /// Save Index instacne from xml file
        /// </summary>
        public void SaveIndexToXml()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultIndexFileName);
        }

    }
}
