using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class FileHelper
    {
        /// <summary>
        /// 指定ファイルがStreamでオープン可能かを取得します。
        /// </summary>
        public static bool CanOpenStream(string fileName, FileAccess fileAccess)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Open, fileAccess))
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
