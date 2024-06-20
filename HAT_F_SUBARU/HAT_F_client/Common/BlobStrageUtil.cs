using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class BlobStrageUtil
    {
        private static readonly string TempOutputPath = HatFConfigReader.GetAppSetting("BlobStrage:TempOutputPath");

        /// <summary>
        /// 一時フォルダを返す
        /// </summary>
        /// <returns></returns>
        public static string GetTempOutputPath()
        {
            string basePath = Environment.ExpandEnvironmentVariables(TempOutputPath);
            var path = Path.Combine(basePath, DateTime.Now.ToString("yyyyMMddHHmmddssfff"));
            if (File.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }


    }
}
