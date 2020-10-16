﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SeleniumBuild.Helper.ExtentReport
{

	public class ExtentTestManager
	{

		private static Dictionary<string, ExtentTest> _parentTestMap = new Dictionary<string, ExtentTest>();
		private static ThreadLocal<ExtentTest> _parentTest = new ThreadLocal<ExtentTest>();
		private static ThreadLocal<ExtentTest> _childTest = new ThreadLocal<ExtentTest>();

		private static readonly object _synclock = new object();

		// creates a parent test
		public static ExtentTest CreateTest(string testName, string reportPath,string description = null)
		{
			lock (_synclock)
			{
				_parentTest.Value = ExtentService.CreateTest(testName, reportPath, description);
				return _parentTest.Value;
			}
		}

		// creates a node
		// node is added to the parent using the parentName
		// if the parent is not available, it will be created
		public static ExtentTest CreateMethod(string parentName, string testName, string description = null)
		{
			lock (_synclock)
			{
				ExtentTest parentTest = null;
				if (!_parentTestMap.ContainsKey(parentName))
				{
					parentTest = ExtentService.Instance.CreateTest(testName);
					_parentTestMap.Add(parentName, parentTest);
				}
				else
				{
					parentTest = _parentTestMap[parentName];
				}
				_parentTest.Value = parentTest;
				_childTest.Value = parentTest.CreateNode(testName, description);
				return _childTest.Value;
			}
		}

		public static ExtentTest CreateMethod(string testName)
		{
			lock (_synclock)
			{
				_childTest.Value = _parentTest.Value.CreateNode(testName);
				return _childTest.Value;
			}
		}

		public static ExtentTest GetMethod()
		{
			lock (_synclock)
			{
				return _childTest.Value;
			}
		}

		public static ExtentTest GetTest()
		{
			lock (_synclock)
			{
				return _parentTest.Value;
			}
		}

		public static void Flush()
        {
			ExtentService.Instance.Flush();
		}
	}
}
