using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// テーブルDestinationsMstに表示項目用の拡張をしたものです。
    /// </summary>
    public class DestinationsMstEx : DestinationsMst
    {
        /// <summary>
        /// 顧客名称 (工事店名称)
        /// </summary>
        public string CustName {  get; set; }
    }
}
