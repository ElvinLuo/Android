using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace WindowsFormsApplication.File
{
    public class FileReader
    {
        public List<string> ReadFile(string fileName)
        {
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return lines;
        }

        public void ReadXML()
        {
            var runSettingsFile = XDocument.Load("../../RunSettings.xml");
            var map = runSettingsFile.Root.Elements().ToDictionary(
                runSettings => (string)runSettings.Attribute("key"),
                runSettings => (string)runSettings.Attribute("value"));
        }

    }
}
