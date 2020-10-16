using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumBuild.Selenium.DriverManagement
{
    public abstract class DriverManager
    {
        private readonly Dictionary<string, IWebDriver> drivers;
        private readonly Dictionary<string, DriverProperties> properties;


        public DriverManager()
        {
            drivers = new Dictionary<string, IWebDriver>();
            properties = new Dictionary<string, DriverProperties>();
        }

        public void SetDriver(string key, IWebDriver driver)
        {
            drivers.Add(key, driver);
        }

        public IWebDriver GetDriver(string key)
        {
            return drivers[key];
        }

        public void SetProperty(string key, DriverProperties driverProperties, out string newKeyDriver)
        {
            if (properties.Count == 0)
                newKeyDriver = string.Format("{0}_0", key);
            else
            {
                do
                {
                    var count = properties.Count;
                    if (key.Contains("_"))
                        key = key.Split('_')[0];
                    newKeyDriver = string.Format("{0}_{1}", key, count);
                }
                while (properties.ContainsKey(newKeyDriver));
            }
            properties.Add(newKeyDriver, driverProperties);
        }

        public DriverProperties GetProperty(string key)
        {
            return properties[key];
        }

        public abstract void CreateWebDriver(string key, string downloadLocation = null);

        public void CloseWebDriver(string key)
        {
            drivers.Remove(key);
        }

        public void CloseAllWebDriver()
        {
            if(drivers != null)
            {
                foreach (var driver in drivers.Values)
                {
                    driver.Quit();
                }
                drivers.Clear();
            }
        }
    }
}
