namespace HAT_F_api.CustomModels
{
    /// <summary>納品一覧表（社内用）チェック記録用パラメータ</summary>
    public class InternalDeliveryCheckParameter
    {
        /// <summary>売上番号</summary>
        public string SalesNo { get; set; }

        /// <summary>売上行番号</summary>
        public short RowNo { get; set; }

        /// <summary>コメント</summary>
        public string Comment { get; set; }
    }
}