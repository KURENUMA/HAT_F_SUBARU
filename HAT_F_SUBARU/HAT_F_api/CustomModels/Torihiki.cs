namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 取引先情報
    /// </summary>
    public class Torihiki
    {
        /// <summary>
        /// 得意先コード
        /// </summary>
        public string TorihikiCd { get; set; } = "";

        /// <summary>
        /// 得意先名_漢字
        /// </summary>
        public string TokuZ { get; set; } = "";

        /// <summary>
        /// 得意先名_カナ
        /// </summary>
        public string TokuH { get; set; } = "";

        /// <summary>
        /// FAX番号
        /// </summary>
        public string AFax { get; set; } = "";

        /// <summary>
        /// 電話番号
        /// </summary>
        public string ATel { get; set; } = "";
    }
}
