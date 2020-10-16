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

		public static ExtentTest CreateTest(string testName, string reportPath, string description = null)
        {
			testName = Utils.GetRandomValue(testName);
			Directory.CreateDirectory(reportPath);

			var reporter = new ExtentHtmlReporter(reportPath);
			reporter.Config.Theme = Theme.Standard;

			Instance.AttachReporter(reporter);
			return Instance.CreateTest(testName, description);
		}
	}

}
