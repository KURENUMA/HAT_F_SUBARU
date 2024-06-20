namespace HatFClient.HatApi.Models.Hat
{
    /// <summary>ユーザ情報</summary>
    public class UserInfo
    {
        /// <summary>アクセストークン</summary>
        public string AccessToken { get; set; }

        /// <summary>社員コード</summary>
        public string ShainCd { get; set; }

        /// <summary>社員名</summary>
        public string Shainnm1 { get; set; }

        /// <summary>部署コード</summary>
        public string Bscd { get; set; }

        /// <summary>チームコード</summary>
        public string TeamCd { get; set; }

        /// <summary></summary>
        public string TypeCd { get; set; }

        /// <summary></summary>
        public int managerFlg { get; set; }
    }
}