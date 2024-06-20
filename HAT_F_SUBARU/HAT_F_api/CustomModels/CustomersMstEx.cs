using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// テーブルCustomersMstに表示項目用の拡張をしたものです。
    /// </summary>
    public class CustomersMstEx : CustomersMst
    {
        /// <summary>
        /// 請求先（名称）
        /// </summary>
        public string ArName { get; set; }

        /// <summary>
        /// 自社担当者名
        /// </summary>
        public string EmpName { get; set; }
    }
}
