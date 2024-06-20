﻿namespace HAT_F_api.CustomModels
{
    /// <summary>売上確定後利率異常チェック結果</summary>
    public class InterestRateCheckFixedResult
    {
        /// <summary>売上番号</summary>
        public string SalesNo { get; set; }

        /// <summary>売上行番号</summary>
        public short RowNo { get; set; }

        /// <summary>チェック者名</summary>
        public string Checker { get; set; }

        /// <summary>チェック者役職</summary>
        public string CheckerPost { get; set; }

        /// <summary>コメント</summary>
        public string Comment { get; set; }
    }
}