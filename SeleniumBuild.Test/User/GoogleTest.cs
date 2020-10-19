using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumBuild.Helper.ExtentReport;
using SeleniumBuild.Test.Base;
using System;
using static SeleniumBuild.Helper.ExtentReport.ExtentTestManager;

namespace SeleniumBuild.Test.User
{
    [TestClass]
    public class GoogleTest : TestBase
    {
        [TestMethod]
        public void TC001()
        {
            var a = 123;
            Console.WriteLine("123");
            CreateStepNode();
        }

        [TestMethod]
        public void TC002()
        {
            var a = 123;
            Console.WriteLine("123");
            CreateStepNode("123!@");
            CreateStepNode("!@32132!");
        }
    }
}
