using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumBuild.Selenium.Driver;
using SeleniumBuild.Selenium.DriverManagement;
using SeleniumBuild.Selenium.Utilities;
using System;
using System.Threading;

namespace SeleniumBuild.Test.Base
{
    public static class Browser
    {
        private static readonly ThreadLocal<DriverManager> _driverManager = new ThreadLocal<DriverManager>();
        private static readonly ThreadLocal<string> _driverKey = new ThreadLocal<string>();

        /// <summary>
        /// Browser Initialize
        /// </summary>
        /// <param name="driverKey"></param>
        /// <param name="downloadLocation"></param>
        public static void Open(string driverKey, string downloadLocation = null)
        {
            DriverManager driverManager = null;
            var driverProperties = new DriverProperties(Config.DRIVER_CONFIG_LOCATION, driverKey, downloadLocation);

            switch (driverProperties.GetDriverType())
            {
                case DriverProperties.DriverType.Chrome:
                    driverManager = new Chrome();
                    break;
                case DriverProperties.DriverType.Firefox:
                    driverManager = new Firefox();
                    break;
                case DriverProperties.DriverType.IE:
                    driverManager = new IE();
                    break;
            }

            if (driverManager != null)
            {
                driverManager.SetProperty(driverKey, driverProperties, out driverKey);
                driverManager.CreateWebDriver(driverKey, downloadLocation);
                if (_driverManager.Value == null)
                {
                    _driverManager.Value = driverManager;
                }
                else
                {
                    string oldKey = driverKey;
                    _driverManager.Value.SetProperty(driverKey, driverProperties, out driverKey);
                    _driverManager.Value.SetDriver(driverKey, driverManager.GetDriver(oldKey));
                }

                _driverKey.Value = driverKey;
            }
            else
            {
                throw new Exception("Cannot create web driver");
            }
        }

        /// <summary>
        /// Get current key of web driver
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentKey()
        {
            return _driverKey.Value;
        }

        /// <summary>
        /// Get current web driver
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetCurrentWebDriver()
        {
            return _driverManager.Value.GetDriver(GetCurrentKey());
        }

        /// <summary>
        /// Navigate to url
        /// </summary>
        /// <param name="url"></param>
        public static void Navigate(string url)
        {
            GetCurrentWebDriver().Navigate().GoToUrl(url);
        }

        ///<summary>
        /// Return current URL.
        ///</summary>
        public static string GetCurrentUrl()
        {
            return GetCurrentWebDriver().Url;
        }

        /// <summary>
        /// Maximize web browser
        /// </summary>
        public static void Maximize()
        {
            try
            {
                GetCurrentWebDriver().Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when maximizing ", e.Message);
            }
        }

        /// <summary>
        /// Close current web driver 
        /// </summary>
        public static void CloseCurrent()
        {
            GetCurrentWebDriver().Quit();
        }

        /// <summary>
        /// CLose all web drivers
        /// </summary>
        public static void CloseAll()
        {
            _driverManager.Value.CloseAllWebDriver();
        }

        /// <summary>
        /// Create javascript executor.
        /// </summary>
        public static IJavaScriptExecutor GetJsExecutor()
        {
            return (IJavaScriptExecutor) GetCurrentWebDriver();
        }

        /// <summary>
        /// Get title
        /// </summary>
        /// <returns></returns>
        public static string Title()
        {
             return GetCurrentWebDriver().Title;
        }

        /// <summary>
        /// Switch to target browser
        /// </summary>
        /// <param name="browserName"></param>
        /// <param name="browserIndex"></param>
        public static void SwitchToTargetBrowser(string browserName, int browserIndex = 0)
        {
            try
            {
                string key = string.Format("{0}_{1}", browserName, browserIndex);
                _driverManager.Value.GetDriver(key);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when switch browser ", e.Message);
            }
        }

        ///<summary>
        /// Get Screenshot
        ///</summary>
        public static Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot) GetCurrentWebDriver()).GetScreenshot();
        }

        ///<summary>
        // /Switch to Iframe IWebElement.
        ///</summary>
        public static void SwitchToIframe(IWebElement iframe)
        {
            GetCurrentWebDriver().SwitchTo().Frame(iframe);
        }

        ///<summary>
        /// Switch to Previous frame 
        ///</summary>
        public static void SwitchToPrevious()
        {
            GetCurrentWebDriver().SwitchTo().ParentFrame();
        }

        ///<summary>
        ///Create WebDriverWait
        ///</summary>
        public static WebDriverWait Wait(int second = 30)
        {
            return new WebDriverWait(GetCurrentWebDriver(), TimeSpan.FromSeconds(second));
        }
    }
}
