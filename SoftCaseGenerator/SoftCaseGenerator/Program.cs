using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SoftCaseGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SoftCaseGenerator());

            string[] ratePlanContractType = new string[] { "RatePlanContractType", "1/2/3", ".Merchan/.Agency/.Flex" };
            string[] ratePlanType = new string[] { "RatePlanType", "1/2/3", ".Standalone/.Package/.Corporate" };
            List<string[]> valid = new List<string[]>();
            valid.Add(ratePlanContractType);
            valid.Add(ratePlanType);
            
            Input input = new Input(valid, null);
        }
    }
}
