using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class ApplicationHelper
    {
        public static string GetAppVersionString()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            string versionString = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            return versionString;
        }
    }
}
