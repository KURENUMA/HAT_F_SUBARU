namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報補完の結果</summary>
    public class CompleteHeaderResult 
    {
        /// <summary>担当者Cd</summary>
        public string TantoCd { get; set; }

        /// <summary>担当者名</summary>
        public string TantoName { get; set; }

        /// <summary>受注者（起票者）Cd</summary>
        public string JyuchuCd { get; set; }

        /// <summary>得意先名</summary>
        public string TokuiName { get; set; }

        /// <summary>キーマン名</summary>
        public string KeymanName { get; set; }

        /// <summary>工事店名</summary>
        public string KoujitenName { get; set; }

        /// <summary>倉庫名</summary>
        public string SokoName { get; set; }

        /// <summary>仕入れ先名</summary>
        public string ShiresakiName { get; set; }

        /// <summary>注文書（発注方法）</summary>
        public string Hkbn { get; set; }

        /// <summary>送信時用FAX番号</summary>
        public string Fax { get; set; }

        /// <summary>送信時用メールアドレス</summary>
        public string MailAddress { get; set; }

        /// <summary>扱便名</summary>
        public string BinName { get; set; }

        /// <summary>届先（宛先）１</summary>
        public string RecvName1 { get; set; }

        /// <summary>届先（宛先）２</summary>
        public string RecvName2 { get; set; }

        /// <summary>届先（TEL）</summary>
        public string RecvTel { get; set; }

        /// <summary>届先（郵便番号）</summary>
        public string RecvPostcode { get; set; }

        /// <summary>届先（住所）１</summary>
        public string RecvAddress1 { get; set; }

        /// <summary>届先（住所）２</summary>
        public string RecvAddress2 { get; set; }

        /// <summary>届先（住所）３</summary>
        public string RecvAddress3 { get; set; }

        /// <summary> 仕入課</summary>
        public string ShireKa { get; set; }

        /// <summary>伝No</summary>
        public string DenNo { get; set; }
    }
}
