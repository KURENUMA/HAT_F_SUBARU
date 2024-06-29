using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// テーブルConstructionDetailに表示項目用の拡張をしたものです。
    /// </summary>
    public class ConstructionDetailEx : ConstructionDetail
    {
        /// <summary>
        /// 仕入先名
        /// </summary>
        public string ShiresakiName {  get; set; }
    }
}
