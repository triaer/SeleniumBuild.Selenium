using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBuild.Selenium.Utilities
{
    public static class Config
    {
        public static string DRIVER_CONFIG_LOCATION = Directory.GetParent(FileUtils.GetWorkingDir()).FullName + "\\Resources\\DriverConfig.json";
        public static string DEFAULT_DOWNLOAD_LOCATION = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\" + Environment.UserName + "\\Downloads";
        public static string DEFAULT_RESULTS_LOCATION = "C:\\temp\\testresults\\";
    }
}
