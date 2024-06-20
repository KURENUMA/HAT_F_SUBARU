namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報補完の入力パラメータ</summary>
    public class CompleteHeaderRequest
    {
        /// <summary>チームCd</summary>
        public string TeamCd { get; set; }

        /// <summary>得意先Cd</summary>
        public string TokuiCd { get; set; }

        /// <summary>現場Cd</summary>
        public string GenbaCd { get; set; }

        /// <summary>担当者Cd</summary>
        public string TantoCd { get; set; }

        /// <summary>キーマンCD</summary>
        public string KeymanCd { get; set; }

        /// <summary>工事店Cd</summary>
        public string KoujitenCd { get; set; }

        /// <summary>受注者（起票者）イニシャル</summary>
        public string JyuchuInitial { get; set; }

        /// <summary>倉庫Cd</summary>
        public string SokoCd { get; set; }

        /// <summary>仕入先Cd</summary>
        public string ShiresakiCd { get; set; }

        /// <summary>注文書（発注方法）</summary>
        public string Hkbn { get; set; }

        /// <summary>仕入先依頼者</summary>
        public string Sirainm { get; set; }

        /// <summary>扱便Cd</summary>
        public string BinCd { get; set; }

        /// <summary>HAT注番</summary>
        public string HatOrderNo { get; set; }

        /// <summary>伝No</summary>
        public string DenNo { get; set; }
    }
}