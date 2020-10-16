using System;
using System.IO;

namespace SeleniumBuild.Selenium.Utilities
{
    public static class FileUtils
    {
        /// <summary>
        /// Get working directory folder
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingDir ()
        {
            var workingDir = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetParent(workingDir).FullName.Substring(0, workingDir.IndexOf("bin\\"));
        }

        public static void CreateDirectory (string path)
        {
            
        }
    }
}
