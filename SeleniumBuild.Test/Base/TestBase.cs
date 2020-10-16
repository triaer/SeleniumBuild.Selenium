using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumBuild.Helper.Common;
using SeleniumBuild.Helper.ExtentReport;
using SeleniumBuild.Selenium.Driver;
using SeleniumBuild.Selenium.DriverManagement;
using SeleniumBuild.Selenium.Utilities;
using System;
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

            reportPath = Config.DEFAULT_RESULTS_LOCATION + Utils.GetRandomValue(TestContext.TestName) + "\\";
            ExtentTestManager.CreateTest(TestContext.TestName, reportPath);







            Browser.Open(driver);

            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ExtentTestManager.Flush();
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {

            }
            else if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {

            }

            Browser.CloseAll();
            return;
        }
    }
}
