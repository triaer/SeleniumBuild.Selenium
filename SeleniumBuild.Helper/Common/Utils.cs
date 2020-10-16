using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBuild.Helper.Common
{
    public static class Utils
    {
        public static string GetRandomValue(string value)
        {
            value = string.Format("{0}_{1}", value.Replace(' ', '_'), DateTime.Now.ToString("yyyyMMddhhmmss"));
            return value;
        }
    }
}
