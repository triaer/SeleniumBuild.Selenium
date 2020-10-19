using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumBuild.Helper.Common;
using SeleniumBuild.Helper.ExtentReport;
using SeleniumBuild.Selenium.Driver;
using SeleniumBuild.Selenium.DriverManagement;
using SeleniumBuild.Selenium.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumBuild.Test.Base
{
    [TestClass]
    public abstract class TestBase
    {
        public TestContext TestContext { get; set; }

        protected string driver = string.Empty;
        protected string environment = string.Empty;
        protected string reportPath = string.Empty;
        protected List<KeyValuePair<string, bool>> validations = new List<KeyValuePair<string, bool>>();

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.Properties.Contains("browser"))
            {
                driver = TestContext.Properties["browser"].ToString();
            }

            if (TestContext.Properties.Contains("environment")) {
                environment = TestContext.Properties["environment"].ToString();
            }

            if(!Directory.Exists(Config.DEFAULT_RESULTS_LOCATION))
            {
                Directory.CreateDirectory(Config.DEFAULT_RESULTS_LOCATION);
            }

            reportPath = Config.DEFAULT_RESULTS_LOCATION + Utils.GetRandomValue() + "\\";
            ExtentTestManager.CreateTest(TestContext.TestName, reportPath);
            ExtentTestManager.CreateMethod(TestContext.TestName, "Pre-condition");

            Browser.Open(driver);   
        }

        public void ReportResult(Status status, string reportFilePath)
        {
            var test = ExtentTestManager.CreateMethod(TestContext.TestName, "Summary");
            if(status == Status.Pass)
            {
                test.Pass(TestContext.TestName + " Passed");
                for (int i = 0; i < validations.Count; i++)
                {
                    test.Info(string.Join(Environment.NewLine, validations[i]));
                }
            }
            
            


        }


        [TestCleanup]
        public void TestCleanup()
        {
            
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                ReportResult(Status.Pass, reportPath);
            }
            else if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {

            }
            ExtentTestManager.Flush();
            Browser.CloseAll();
            return;
        }
    }
}
