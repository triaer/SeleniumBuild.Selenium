using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBuild.Selenium.Utilities
{
    public class JsonParser
    {
        public static JObject ParseJsonFileToObject(string filePath)
        {
            using (var r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                return jobj;
            }
        }
    }
}
