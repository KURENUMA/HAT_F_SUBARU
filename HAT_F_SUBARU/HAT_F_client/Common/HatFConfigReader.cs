using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
namespace HatFClient.Common
{
    internal class HatFConfigReader
    {
        private static string GetConfigFileName()
        {
            string devConfig = ConfigurationManager.AppSettings["__DevAppConfigFile__"];
            devConfig = Environment.ExpandEnvironmentVariables(devConfig);
            return devConfig;
        }

        public static string GetAppSetting(string key, string defaultValue = "")
        {
#if DEBUG
            string devConfig = GetConfigFileName();
            if (!string.IsNullOrEmpty(devConfig) && System.IO.File.Exists(devConfig))
            {
                XDocument xdoc = XDocument.Load(devConfig);
                var addElements = xdoc.Root.XPathSelectElements("appSettings/add");
                var item = addElements.Where(w => string.Equals(w.Attribute("key")?.Value ?? "", key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (item != null)
                {
                    string devValue = item.Attribute("value")?.Value ?? "";
                    return devValue;
                }
            }
#endif

            string val = ConfigurationManager.AppSettings[key];
            return val;
        }

        public static string GetConnectionString(string name)
        {
#if DEBUG
            string devConfig = GetConfigFileName();
            if (!string.IsNullOrEmpty(devConfig) && System.IO.File.Exists(devConfig))
            {
                XDocument xdoc = XDocument.Load(devConfig);
                var addElements = xdoc.Root.XPathSelectElements("connectionStrings/add");
                var item = addElements.Where(w => string.Equals(w.Attribute("name")?.Value ?? "", name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (item != null)
                {
                    string devValue = item.Attribute("connectionString")?.Value ?? "";
                    return devValue;
                }
            }
#endif

            string val = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return val;
        }
    }

}
