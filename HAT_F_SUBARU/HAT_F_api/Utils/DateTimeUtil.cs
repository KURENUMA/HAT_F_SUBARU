namespace HAT_F_api.Utils
{
    public class DateTimeUtil
    {
        /// <summary>締日や支払月、支払日から日付を算出する</summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="closeDate">締日</param>
        /// <param name="payMonths">支払月（0:当月,1:翌月,2:翌々月）</param>
        /// <param name="payDates">支払日（N:N日払い,99:末日）</param>
        /// <returns>算出結果</returns>
        public static DateTime? GetPayDate(DateTime? baseDate, int? closeDate, int? payMonths, int? payDates)
        {
            // BaseDateがNULLの場合、NULLを返す
            if (baseDate == null)
            {
                return null;
            }

            // 締日がNULLまたは1～31ではない場合は月末締めとする
            int adjustedCloseDate = closeDate ?? DateTime.DaysInMonth(baseDate.Value.Year, baseDate.Value.Month);
            if (adjustedCloseDate < 1 || adjustedCloseDate > 31)
            {
                adjustedCloseDate = DateTime.DaysInMonth(baseDate.Value.Year, baseDate.Value.Month);
            }

            // 支払月がNULLまたは0～2ではない場合は当月とする
            int adjustedPayMonths = payMonths ?? 0;
            if (adjustedPayMonths < 0 || adjustedPayMonths > 2)
            {
                adjustedPayMonths = 0;
            }

            // 支払日がNULLまたは1～31ではない場合は月末日とする
            bool isPayDateEom = payDates == null || payDates < 1 || payDates > 31;
            int adjustedPayDates = isPayDateEom ? DateTime.DaysInMonth(baseDate.Value.Year, baseDate.Value.Month) : payDates.Value;

            // 当月の締日を計算
            DateTime closingDateOfMonth = new DateTime(baseDate.Value.Year, baseDate.Value.Month, 1).AddDays(adjustedCloseDate - 1);

            // 締日が基準日より前の場合、次の月の締日に変更
            if (baseDate > closingDateOfMonth)
            {
                closingDateOfMonth = closingDateOfMonth.AddMonths(1);
            }

            // 締日が月末締めかつ支払月が0:当月支払の場合、月末締めかつ翌月支払とする
            if (adjustedCloseDate == DateTime.DaysInMonth(baseDate.Value.Year, baseDate.Value.Month) && adjustedPayMonths == 0)
            {
                adjustedPayMonths = 1;
            }

            // 支払予定日の月を計算
            DateTime paymentDate = closingDateOfMonth.AddMonths(adjustedPayMonths);

            // 支払予定日の日を設定（月末日を考慮）
            if (adjustedPayDates > DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month))
            {
                paymentDate = new DateTime(paymentDate.Year, paymentDate.Month, DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month));
            }
            else
            {
                paymentDate = new DateTime(paymentDate.Year, paymentDate.Month, adjustedPayDates);
            }

            if (isPayDateEom)
            {
                paymentDate = new DateTime(paymentDate.Year, paymentDate.Month, DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month));
            }

            // 支払予定日が基準日より前の場合、次の月の支払日に変更
            if (paymentDate < baseDate)
            {
                paymentDate = paymentDate.AddMonths(1);
                if (adjustedPayDates > DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month))
                {
                    paymentDate = new DateTime(paymentDate.Year, paymentDate.Month, DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month));
                }
                else
                {
                    paymentDate = new DateTime(paymentDate.Year, paymentDate.Month, adjustedPayDates);
                }
            }

            return paymentDate;
        }

        /// <summary>
        /// 日が、年月内に存在するかを確認する
        /// </summary>
        /// <param name="ym">年月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public static bool DoesDayExist(DateTime ym, int day)
        {
            return DoesDayExist(ym.Year, ym.Month, day);
        }

        /// <summary>
        /// 日が、年月内に存在するかを確認する
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public static bool DoesDayExist(int year, int month, int day)
        {
            // 指定した年月の月の日数を取得
            int daysInMonth = DateTime.DaysInMonth(year, month);
            // 指定した日が月の日数内にあるかを確認
            return day >= 1 && day <= daysInMonth;
        }

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

        /// <summary>
        /// 月末日付を返します
        /// </summary>
        /// <param name="source"></param>
        /// <param name="addMonths"></param>
        /// <returns></returns>
        public static DateTime GetEndOfMonth(DateTime source, int addMonths)
        {
            var d = GetFirstOfMonth(source);
            d = d.AddMonths(addMonths);
            return d.AddMonths(1).AddDays(-1);
        }
    }
}
