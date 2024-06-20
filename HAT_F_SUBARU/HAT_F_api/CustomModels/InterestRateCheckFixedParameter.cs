namespace HAT_F_api.CustomModels
{
    /// <summary>売上確定後利率異常チェック記録用パラメータ</summary>
    public class InterestRateCheckFixedParameter
    {
        /// <summary>売上番号</summary>
        public string SalesNo { get; set; }

        /// <summary>売上行番号</summary>
        public short RowNo { get; set; }

        /// <summary>コメント</summary>
        public string Comment { get; set; }
    }
}