using System;
using Microsoft.Win32;
using mshtml;
using SHDocVw;

namespace DatabaseSelector
{
    class Registry
    {
        public static string GetDBServer(string environment)
        {
            InternetExplorer IE = new InternetExplorer();
            object Empty = 0;
            object URL = "http://bdtools.sb.karmalab.net/envstatus/envstatus.cgi";

            IE.Visible = true;
            IE.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);

            System.Threading.Thread.Sleep(10000);

            while (IE.Busy)
            {
                System.Threading.Thread.Sleep(100);
            }

            IHTMLDocument3 document = (IHTMLDocument3)IE.Document;

            HTMLSelectElement selGroups = (HTMLSelectElement)document.getElementById("group");
            HTMLDivElement divSubmit = (HTMLDivElement)document.getElementById("submitbutton");
            HTMLButtonElement btnSubmit = (HTMLButtonElement)divSubmit.firstChild;

            if (string.IsNullOrEmpty(environment))
            {
                selGroups.value = "CHE-RC01";
            }
            else
            {
                selGroups.value = environment;
            }
            Console.WriteLine("environment: {0}", selGroups.value);
            btnSubmit.click();

            System.Threading.Thread.Sleep(10000);

            while (IE.Busy)
            {
                System.Threading.Thread.Sleep(100);
            }

            HTMLDivElement divSitesTable = (HTMLDivElement)document.getElementById("sitestable");
            string targetWebServerName = Global.emptyString;
            foreach (HTMLDTElement cell in divSitesTable.getElementsByTagName("td"))
            {
                bool isServerName = false;
                bool containHIMS = false;

                HTMLDTElement webServerName = (HTMLDTElement)cell.firstChild;
                if (webServerName.innerText.Contains("CHELWEB"))
                {
                    isServerName = true;

                    foreach (HTMLAnchorElement link in cell.getElementsByTagName("a"))
                    {
                        if (link.innerText.Equals("everestadmintools.com"))
                        {
                            containHIMS = true;
                            targetWebServerName = webServerName.innerText;
                            Console.WriteLine("web server: {0}", webServerName.innerText);
                            break;
                        }
                    }
                }
                if (isServerName && containHIMS)
                    break;
            }

            HTMLAreaElement targetWebServerSpan = (HTMLAreaElement)document.getElementById(targetWebServerName);
            HTMLTableCell targetCell = (HTMLTableCell)targetWebServerSpan.parentElement.parentElement;
            HTMLTableCell dbServerName = (HTMLTableCell)targetCell.nextSibling.nextSibling.nextSibling.nextSibling;
            string targetDBServerName = dbServerName.innerText;
            Console.WriteLine("database server: {0}", targetDBServerName);
            IE.Quit();

            return targetDBServerName;
        }

        public static void PrintDBNames(string server)
        {
            RegistryKey expDsnKey;
            RegistryKey expDsnSubKey;
            string remoteName = server;

            try
            {
                expDsnKey = RegistryKey.OpenRemoteBaseKey(
                    RegistryHive.LocalMachine, remoteName).OpenSubKey(
                    "SOFTWARE\\Expedia\\shared\\Database\\ExpDsn");
                foreach (string subKeyName in expDsnKey.GetSubKeyNames())
                {
                    expDsnSubKey = expDsnKey.OpenSubKey(subKeyName);
                    Console.WriteLine("{0}: {1}", subKeyName, expDsnSubKey.GetValue("Server"));
                }
                if (expDsnKey != null)
                    expDsnKey.Close();
            }
            catch (Exception exception)
            {
                Global.WriteLog(exception.StackTrace);
            }
        }
    }
}
