using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumBuild.Selenium.DriverManagement;
using SeleniumBuild.Selenium.Utilities;
using System;

namespace SeleniumBuild.Selenium.Driver
{
    public class Chrome : DriverManager
    {
        public override void CreateWebDriver(string key, string downloadLocation = null)
        {
            IWebDriver driver;
            var properties = GetProperty(key);

            var options = new ChromeOptions();
            options.AddArguments(properties.GetArguments());

            if(properties.IsHeadless())
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu"); 
            }

            if (properties.GetUserProfilePreference() != null)
            {
                foreach (var item in properties.GetUserProfilePreference())
                    options.AddUserProfilePreference(item.Key, item.Value);
            }

            options.AddUserProfilePreference("download.default_directory", downloadLocation);

            if (!properties.IsRemote())
            {
                driver = new ChromeDriver(options);
            }
            else
            {
                foreach (var cap in properties.GetCapabilities())
                    options.AddAdditionalCapability(cap.Key, cap.Value);

                driver = new RemoteWebDriver(new Uri(properties.GetRemoteUrl()), options.ToCapabilities());
            }
            SetDriver(key, driver);
        }
    }
}
