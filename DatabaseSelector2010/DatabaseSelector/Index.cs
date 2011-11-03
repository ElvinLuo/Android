using System.IO;

namespace DatabaseSelector
{
    public class Index
    {
        private static Index instance;

        private string temcurl;
        private string xlsFile;
        private string txtFile;

        public string groupFilter;
        public string webServerFilter;
        public string travelServerFilter;
        public string databaseFilter;

        public bool automaticallyOpenEditer;

        public int previousSelectedGroup;
        public int currentSelectedGroup;
        public int previousSelectedServer;
        public int currentSelectedServer;
        public int previousSelectedDatabase;
        public int currentSelectedDatabase;

        public static Index CreateInstance()
        {
            if (instance == null)
            {
                instance = new Index();
                instance = instance.GetIndexFromXml();
            }
            return instance;
        }

        public Index()
        {
        }

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

        public Index GetIndexFromXml()
        {
            if (File.Exists(Global.defaultIndexFile))
                return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), Global.defaultIndexFileName) as Index;
            else
                return this;
        }

        public void SaveIndexToXml()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), Global.defaultIndexFileName);
        }

    }
}
