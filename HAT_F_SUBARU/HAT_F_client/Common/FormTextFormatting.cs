using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    /// <summary>
    /// 画面用デフォルト書式
    /// </summary>
    internal static class FormTextFormatting
    {
        public static readonly string DefaultDateFormat = "yy/MM/dd";
        public static readonly string DefaultTimeFormat = "HH:mm";
        public static readonly string DefaultDateTimeFormat = "yy/MM/dd HH:mm";

        public static string ToDateText(DateTime dateTime)
        {
            string val = dateTime.ToString(DefaultDateFormat);
            return val;
        }

        public static string ToTimeText(DateTime dateTime)
        {
            string val = dateTime.ToString(DefaultTimeFormat);
            return val;
        }

        public static string ToDateTimeText(DateTime dateTime)
        {
            string val = dateTime.ToString(DefaultDateTimeFormat);
            return val;
        }
    }
}
