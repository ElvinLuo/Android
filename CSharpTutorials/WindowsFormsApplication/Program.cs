using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Dictionary<string, string> expected = new Dictionary<string, string>
            { { "200021792", "289400" },
            { "200021030", "Automation 000711182612" } };

            Dictionary<string, string> actual = new Dictionary<string, string>
            {{ "200021030", "Automation 000711182612" },
            { "200021792", "289400" }};

            bool result = IsEquals(expected, actual);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new BrowserControllerForm());
        }

        static bool IsEquals(Dictionary<string, string> actual, Dictionary<string, string> expected)
        {
            if (actual.Count != expected.Count)
            {
                return false;
            }
            else
            {
                foreach (KeyValuePair<string, string> pair in actual)
                {
                    if (!expected.Contains(pair))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }

}
