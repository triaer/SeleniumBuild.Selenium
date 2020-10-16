using Newtonsoft.Json.Linq;
using SeleniumBuild.Selenium.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumBuild.Selenium.DriverManagement
{
    public class DriverProperties
    {
        private RunMode runMode;
        private DriverType driverType;
        private string remoteUrl;
        private string propertyKey;
        private List<string> arguments;
        private Dictionary<string, object> capabilities;
        private Dictionary<string, object> userProfilePreference;
        private string defaultDownloadLocation;

        public DriverProperties(string driverConfigPath, string driver, string downloadLocation = null)
        {
            var jObject = JsonParser.ParseJsonFileToObject(driverConfigPath);
            var tokens = jObject.SelectToken(driver);
            SetRunMode(tokens.SelectToken("runMode").ToString());
            SetDriverType(tokens.SelectToken("driverType").ToString());
            defaultDownloadLocation = downloadLocation != null ? downloadLocation : Config.DEFAULT_DOWNLOAD_LOCATION;
            SetPropertyKey(driver);

            if (IsRemote())
            {
                SetRemoteUrl(tokens.SelectToken("remoteUrl").ToString());
            }
            if (tokens.SelectToken("arguments") != null)
            {
                SetArguments(tokens.SelectToken("arguments").ToObject<List<string>>());
            }
            if (tokens.SelectToken("capabilities") != null)
            {
                SetCapabilities(tokens.SelectToken("capabilities"));
            }
            if (tokens.SelectToken("userProfilePreference") != null)
            {
                SetUserProfilePreference(tokens.SelectToken("userProfilePreference"));
            }
        }

        public void SetRunMode(string runMode)
        {
            if (runMode.ToLower() == "remote")
                this.runMode = RunMode.Remote;
            else
                this.runMode = RunMode.Local;
        }

        public RunMode GetRunMode()
        {
            return runMode;
        }

        public bool IsRemote()
        {
            if (GetRunMode() == RunMode.Local)
                return false;

            return true;
        }

        public void SetDriverType(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chrome":
                    this.driverType = DriverType.Chrome;
                    break;

                case "firefox":
                    this.driverType = DriverType.Firefox;
                    break;

                case "ie":
                    this.driverType = DriverType.IE;
                    break;
            }
        }

        public DriverType GetDriverType()
        {
            return driverType;
        }

        public void SetRemoteUrl(string remoteUrl)
        {
            this.remoteUrl = remoteUrl;
        }

        public string GetRemoteUrl()
        {
            return remoteUrl;
        }

        public void SetPropertyKey(string propertyKey)
        {
            this.propertyKey = propertyKey;
        }

        public string GetPropertyKey()
        {
            return propertyKey;
        }

        public void SetArguments(List<string> arguments)
        {
            this.arguments = arguments;
        }

        public List<string> GetArguments()
        {
            return arguments;
        }

        public bool IsHeadless()
        {
            if (GetArguments().Contains("headless")) 
                return true;

            return false;
        }

        public void SetCapabilities(JToken capabilities)
        {
            if (capabilities != null)
                this.capabilities = capabilities.ToObject<Dictionary<string, object>>();
        }

        public Dictionary<string, object> GetCapabilities()
        {
            return capabilities;
        }

        public void SetUserProfilePreference(JToken userProfilePreference)
        {
            if (capabilities != null)
                this.userProfilePreference = userProfilePreference.ToObject<Dictionary<string, object>>();
        }

        public Dictionary<string, object> GetUserProfilePreference()
        {
            return userProfilePreference;
        }

        public void SetDownloadLocation(string path)
        {
            defaultDownloadLocation = path;
        }

        public string GetDownloadLocation()
        {
            return defaultDownloadLocation;
        }

        public enum RunMode
        {
            Remote,
            Local
        }

        public enum DriverType
        {
            Chrome,
            Firefox,
            IE
        }
    }
}
