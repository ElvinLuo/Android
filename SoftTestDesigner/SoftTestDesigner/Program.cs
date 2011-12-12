﻿using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SoftTestDesigner());

            //string[] pricing = new string[] {
            //    "PricingModel",
            //    "PDP/OBP/PPP",
            //    "PDP./OBP./PPP." };
            //string[] lar = new string[] {
            //    "LAREnabled",
            //    "True/false", 
            //    "LAR./NonLAR." };
            //string[] los = new string[] {
            //    "LOSEnabled", 
            //    "True/False",
            //    "LOS_/" };
            //string[] ratePlanType = new string[] {
            //    "RatePlanType", 
            //    "Standalone/Package/Corporate", 
            //    "/Package_/Corporate_" };
            //string[] ratePlanContractType = new string[] {
            //    "RatePlanContractType", 
            //    "Merchant/Agency/Flex",
            //    "Merchant/Agency/Flex" };
            //string[] targetRatePlanContractType = new string[] {
            //    "RatePlanContractType", 
            //    "Merchant/Agency/Flex",
            //    "ToMerchant/ToAgency/ToFlex" };

            //List<string[]> valid = new List<string[]>();
            //valid.Add(pricing);
            ////valid.Add(lar);
            ////valid.Add(los);
            ////valid.Add(ratePlanType);
            //valid.Add(ratePlanContractType);
            //valid.Add(targetRatePlanContractType);

            ////string[] invalidItem1 = new string[] { "PricingModel=OBP", "LOSEnabled=True" };
            ////string[] invalidItem2 = new string[] { "PricingModel=PPP", "LOSEnabled=True" };
            ////string[] invalidItem3 = new string[] { "RatePlanType=Corporate", "RatePlanContractType=Agency" };

            //List<string[]> invalid = new List<string[]>();
            ////invalid.Add(invalidItem1);
            ////invalid.Add(invalidItem2);
            ////invalid.Add(invalidItem3);

            //Input input = new Input(valid, invalid);
            //Dictionary<string, string> softCases = input.GetAllSoftCasesFromConfig();

            //foreach (KeyValuePair<string, string> pair in softCases)
            //{
            //    new SoftTest(pair.Key, input.configItemNames, pair.Value);
            //}

        }
    }
}