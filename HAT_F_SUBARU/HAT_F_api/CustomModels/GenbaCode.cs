namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 現場(出荷先)
    /// </summary>
    public class GenbaCode
    {
        /// <summary>
        /// 現場コード
        /// </summary>
        public string GenbaCd { get; set; }
        /// <summary>
        /// 現場名
        /// </summary>
        public string GenbaNm { get; set; }
        /// <summary>
        /// 住所1
        /// </summary>
        public string Adrs1 { get; set; }
        /// <summary>
        /// 住所2
        /// </summary>
        public string Adrs2 { get; set; }
        /// <summary>
        /// 住所3
        /// </summary>
        public string Adrs3 { get; set; }
        /// <summary>
        /// 得意先コード
        /// </summary>
        public string TokuCd { get; set; }
        /// <summary>
        /// 電話番号
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// FAX番号
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCd { get; set; }
        /// <summary>
        /// OPSユーザー
        /// </summary>
        public string OpsUse { get; set; }
    }
}
