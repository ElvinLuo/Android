using System.IO;

namespace DatabaseSelector
{
    public class Index
    {
        private static Index instance;
        private string fileName = "Index.xml";

        public string temcurl;
        public string xlsFile;
        public string txtFile;

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

        public Index GetIndexFromXml()
        {
            if (File.Exists(Serializer.CreateInstance().applicationFolder + fileName))
                return Serializer.CreateInstance().DeserializeFromXML(this.GetType(), fileName) as Index;
            else
                return this;
        }

        public void SaveIndexToXml()
        {
            Serializer.CreateInstance().SerializeToXML(this, this.GetType(), fileName);
        }

    }
}
