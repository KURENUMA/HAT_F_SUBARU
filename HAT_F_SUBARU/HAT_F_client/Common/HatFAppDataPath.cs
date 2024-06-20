using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class HatFAppDataPath
    {
        public static string GetLocalDataSavePath(string category) 
        {
            string directoryPath = HatFConfigReader.GetAppSetting("Client:LocalDataSavePath");
            directoryPath = Environment.ExpandEnvironmentVariables(directoryPath);
            directoryPath = Path.Combine(directoryPath, category);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }
    }
}
