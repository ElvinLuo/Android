using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SoftTestDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SoftTestDesigner());
        }

        private static void TestInput()
        {
            string[] pricing = new string[] {
                "PricingModel",
                "PDP./OBP./PPP.",
                "PDP/OBP/PPP" };
            string[] lar = new string[] {
                "LAREnabled",
                "LAR./NonLAR.",
                "True/false" };
            string[] los = new string[] {
                "LOSEnabled", 
                "LOS_/",
                "True/False"};
            string[] ratePlanType = new string[] {
                "RatePlanType", 
                "/Package_/Corporate_",
                "Standalone/Package/Corporate"};
            string[] ratePlanContractType = new string[] {
                "RatePlanContractType",
                "Merchant/Agency/Flex",
                "Merchant/Agency/Flex"};
            string[] targetRatePlanContractType = new string[] {
                "RatePlanContractType",
                "ToMerchant/ToAgency/ToFlex",
                "Merchant/Agency/Flex"};

            List<string[]> valid = new List<string[]>();
            valid.Add(pricing);
            //valid.Add(lar);
            //valid.Add(los);
            //valid.Add(ratePlanType);
            valid.Add(ratePlanContractType);
            valid.Add(targetRatePlanContractType);

            //string[] invalidItem1 = new string[] { "PricingModel=OBP", "LOSEnabled=True" };
            //string[] invalidItem2 = new string[] { "PricingModel=PPP", "LOSEnabled=True" };
            //string[] invalidItem3 = new string[] { "RatePlanType=Corporate", "RatePlanContractType=Agency" };

            List<string[]> invalid = new List<string[]>();
            //invalid.Add(invalidItem1);
            //invalid.Add(invalidItem2);
            //invalid.Add(invalidItem3);

            Input input = new Input(valid, invalid);
            Dictionary<string, string> softCases = input.GetAllSoftCasesFromConfig();

            foreach (KeyValuePair<string, string> pair in softCases)
            {
            }
        }
    }
}
