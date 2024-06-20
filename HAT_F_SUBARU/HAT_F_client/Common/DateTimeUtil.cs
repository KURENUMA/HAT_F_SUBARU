using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class DateTimeUtil
    {
        /// <summary>
        /// 現在日付の月初めの日付を返します
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFirstOfMonth()
        {
            return GetFirstOfMonth(DateTime.Now);
        }

        /// <summary>
        /// 月初めの日付を返します
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime GetFirstOfMonth(DateTime source)
        {
            return new DateTime(source.Year, source.Month, 1);
        }

        /// <summary>
        /// 現在日付の月末日付を返します
        /// </summary>
        /// <returns></returns>
        public static DateTime GetEndOfMonth()
        {
            return GetEndOfMonth(DateTime.Now);
        }

        /// <summary>
        /// 月末日付を返します
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime GetEndOfMonth(DateTime source)
        {
            var d = GetFirstOfMonth(source);
            return d.AddMonths(1).AddDays(-1);
        }
    }
}
