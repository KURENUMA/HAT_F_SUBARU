using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class HatFAppConfig
    {
        public static DateTime GetBaseDate()
        {
            string baseDateValue = HatFConfigReader.GetAppSetting("AppConfig:BaseDate");
            var baseDate = HatFComParts.DoParseDateTime(baseDateValue);
            if (baseDate.HasValue)
            {
                return baseDate.Value;
            }
            else
            {
                return HatFComParts.DoParseDateTime2("2023/01/01");
            }
        }
    }
}
