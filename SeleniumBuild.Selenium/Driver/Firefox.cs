using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SeleniumBuild.Selenium.DriverManagement;
using SeleniumBuild.Selenium.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBuild.Selenium.Driver
{
    public class Firefox : DriverManager
    {
        public override void CreateWebDriver(string key, string downloadLocation = null)
        {
            IWebDriver driver;
            var properties = GetProperty(key);
            var profile = new FirefoxProfile();
            var options = new FirefoxOptions();
            options.AddArguments(properties.GetArguments());

            if (properties.GetUserProfilePreference() != null)
            {
                foreach (var item in properties.GetUserProfilePreference())
                    profile.SetPreference(item.Key, item.Value.ToString());
            }

            profile.SetPreference("browser.download.folderlist", 2);
            

            if (!properties.IsRemote())
            {
                driver = new FirefoxDriver(options);
            }
            else
            {
                foreach (var cap in properties.GetCapabilities())
                    options.AddAdditionalCapability(cap.Key, cap.Value);

                options.AddAdditionalCapability("browser.download.dir", downloadLocation);
                driver = new RemoteWebDriver(new Uri(properties.GetRemoteUrl()), options.ToCapabilities());
            }

            SetDriver(key, driver);
        }
    }
}
