using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class AppLauncher
    {
        public static void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public static void OpenExcel(string fileName)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }
    }
}
