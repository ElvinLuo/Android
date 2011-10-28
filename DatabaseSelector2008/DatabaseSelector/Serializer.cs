using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace DatabaseSelector
{
    class Serializer
    {
        private static Serializer instance;
        public string applicationFolder = null;

        public static Serializer CreateInstance()
        {
            if (instance == null)
            {
                instance = new Serializer();
            }
            return instance;
        }

        public Serializer()
        {
            try
            {
                if (applicationFolder == null)
                {
                    string result = "";
                    RegistryKey dllKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.ClassesRoot, System.Environment.MachineName).OpenSubKey("DatabaseSelector.Connect\\CLSID");
                    result = dllKey.GetValue("").ToString();

                    dllKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.ClassesRoot, System.Environment.MachineName).OpenSubKey("\\Wow6432Node\\CLSID");
                    if (dllKey == null)
                        dllKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Environment.MachineName).OpenSubKey("SOFTWARE\\Classes\\CLSID");
                    if (dllKey == null)
                        dllKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, System.Environment.MachineName).OpenSubKey("SOFTWARE\\Classes\\CLSID");
                    if (dllKey == null)
                        dllKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.ClassesRoot, System.Environment.MachineName).OpenSubKey("CLSID");
                    foreach (string element in dllKey.GetSubKeyNames())
                    {
                        if (element.Equals(result))
                        {
                            result = dllKey.OpenSubKey(element).OpenSubKey("InprocServer32").GetValue("CodeBase").ToString();
                            applicationFolder = result.Substring(0, result.Length - 20);
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            { Console.WriteLine(exception.Message); }
        }

        public void SerializeToXML(object instance, Type type, string file)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(type);
                TextWriter text = new StreamWriter(applicationFolder + file);
                x.Serialize(text, instance);
                text.Flush();
                text.Close();
            }
            catch (Exception exception)
            { Console.WriteLine(exception.Message); }
        }

        public object DeserializeFromXML(Type type, string file)
        {
            XmlSerializer x = new XmlSerializer(type);
            TextReader text = new StreamReader(applicationFolder + file);
            object resut = x.Deserialize(text);
            text.Close();
            return resut;
        }
    }

    class TXTReader
    {
        string file = Index.CreateInstance().txtFile;
        private static TXTReader instance;
        Dictionary<string, int> serverPortPairs;

        public static TXTReader CreateInstance()
        {
            if (instance == null)
            {
                instance = new TXTReader();
            }
            return instance;
        }

        public void SetFile(string fileName)
        {
            file = fileName;
        }

        public Dictionary<string, int> GetServerPortPair()
        {
            string line;
            serverPortPairs = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(file))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf("dn.expeso.com") > -1)
                    {
                        string[] elements = line.Split('\t');
                        serverPortPairs.Add(elements[0].Substring(0, elements[0].IndexOf('.')), Convert.ToInt32(elements[1]));
                    }
                }
            }
            return serverPortPairs;
        }

        public int? GetPortByServerName(string serverName)
        {
            int? port = null;
            if (serverPortPairs.ContainsKey(serverName))
                port = serverPortPairs[serverName];
            return port;
        }
    }

    class XLSReader
    {
        string file = Index.CreateInstance().xlsFile;

        private static XLSReader instance;

        public static XLSReader CreateInstance()
        {
            if (instance == null)
            {
                instance = new XLSReader();
            }
            return instance;
        }

        public void SetFile(string fileName)
        {
            file = fileName;
        }

        public TravelServer GetTravelServerFromXLSAndChangeProgressBar(string machineName, ProgressBar pgb)
        {
            int max = 0;
            if (pgb != null) max = pgb.Maximum;
            int i = 2;
            System.Array myvalues;
            TravelServer travelServer = new TravelServer(machineName);
            travelServer.Databases = new List<DatabaseItem>();
            TXTReader txtReader = TXTReader.CreateInstance();
            txtReader.GetServerPortPair();

            Microsoft.Office.Interop.Excel.Application ExcelObj = null;
            ExcelObj = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open(file, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = theWorkbook.Worksheets;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range("A" + i.ToString(), "B" + i.ToString());
            if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = worksheet.Rows.Row; });
            while (!range.Text.Equals(""))
            {
                myvalues = (System.Array)range.Cells.Value2;
                if (machineName.Equals("All") || myvalues.GetValue(1, 1).ToString().Equals(machineName))
                {
                    DatabaseItem databaseItem = new DatabaseItem(myvalues.GetValue(1, 2).ToString(),
                        myvalues.GetValue(1, 2).ToString(), "PPE database", "CHC-APPWG01.idx.expedmz.com," +
                        txtReader.GetPortByServerName(myvalues.GetValue(1, 1).ToString()).Value.ToString(),
                        "Update8!", "expdev");
                    travelServer.Databases.Add(databaseItem);
                }
                i++;
                range = worksheet.get_Range("A" + i.ToString(), "B" + i.ToString());
                if (pgb != null)
                {
                    pgb.Invoke((MethodInvoker)delegate
                    {
                        pgb.PerformStep();
                        GlobalOperator.SetProgressBarText(pgb, "Reloading databases of " + machineName);
                    });
                }
            }

            theWorkbook.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlDoNotSaveChanges, Type.Missing, Type.Missing);
            ExcelObj.Quit();
            if (pgb != null) pgb.Invoke((MethodInvoker)delegate { pgb.Maximum = max; });
            return travelServer;
        }
    }

    public static class Wow
    {
        public static bool Is64BitProcess
        {
            get { return IntPtr.Size == 8; }
        }

        public static bool Is64BitOperatingSystem
        {
            get
            {
                if (Is64BitProcess)
                    return true;
                bool isWow64;
                return ModuleContainsFunction("kernel32.dll", "IsWow64Process") && IsWow64Process(GetCurrentProcess(), out isWow64) && isWow64;
            }
        }

        static bool ModuleContainsFunction(string moduleName, string methodName)
        {
            IntPtr hModule = GetModuleHandle(moduleName);
            if (hModule != IntPtr.Zero)
                return GetProcAddress(hModule, methodName) != IntPtr.Zero;
            return false;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        extern static bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static IntPtr GetCurrentProcess();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static IntPtr GetModuleHandle(string moduleName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        extern static IntPtr GetProcAddress(IntPtr hModule, string methodName);
    }

}
