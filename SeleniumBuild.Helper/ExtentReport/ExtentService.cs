using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using SeleniumBuild.Helper.Common;
using SeleniumBuild.Selenium.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBuild.Helper.ExtentReport
{
	public class ExtentService
	{
		private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
		public static ExtentReports Instance { get { return _lazy.Value; } }

		static ExtentService()
		{
		}

		private ExtentService()
		{
		}

		public static ExtentTest CreateTest(string testName, string reportPath, string description = null, string testSuite = "Default")
        {
			//testName = Utils.GetRandomValue(testName);
			Directory.CreateDirectory(reportPath);
			if (Instance.StartedReporterList != null)
            {
				var reporter = new ExtentHtmlReporter(reportPath);
				reporter.Config.ReportName = testName;
				reporter.Config.DocumentTitle = testName;
				reporter.Config.Theme = Theme.Standard;

				Instance.AttachReporter(reporter);
			}
			
			return Instance.CreateTest(testName, description);
		}

		public static void Flush(string reportPath)
        {
			var reportFilePath = reportPath + "index.html";
			var reportFiles = Directory.GetFiles(reportPath);
			foreach(var reportFile in reportFiles)
            {
				
            }
			//Instance.Flush();
        }
	}
}
