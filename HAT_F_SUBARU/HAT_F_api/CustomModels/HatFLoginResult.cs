namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// ログイン結果
    /// </summary>
    public class HatFLoginResult
    {
        /// <summary>
        /// ログイン成否
        /// </summary>
        public bool LoginSucceeded { get; set; }

        /// <summary>
        /// ログイン処理失敗時エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 社員ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 社員番号
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// 社員名（漢字）
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 社員名（カナ）
        /// </summary>
        public string EmployeeNameKana { get; set; }

        /// <summary>
        /// 社員指定タグ
        /// </summary>
        public string EmployeeTag { get; set; }

        /// <summary>
        /// チームコード
        /// </summary>
        public string TeamCode { get; set; }

        /// <summary>
        /// ロール(カンマ区切り)
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 認証トークン
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HatFLoginResult() { }


        ///// <summary>
        ///// コンストラクタ(DI)
        ///// </summary>
        ///// <param name="httpContextAccessor"></param>
        //public HatFLoginResult(IHttpContextAccessor httpContextAccessor) 
        //{
        //    var loginResult = httpContextAccessor.HttpContext.Items["HatFLoginResult"] as HatFLoginResult;

        //    if (loginResult != null)
        //    {
        //        // JWT由来の値をthisにコピーする
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<HatFLoginResult, HatFLoginResult>();
        //        });
        //        var mapper = config.CreateMapper();
        //        mapper.Map(loginResult, this);
        //    }
        //    else
        //    {
        //        this.LoginSucceeded = false;
        //        this.ErrorMessage = "";
        //        this.EmployeeId = 0;
        //        this.EmployeeCode = "0000";
        //        this.EmployeeName = "ログインしていません";
        //        this.EmployeeNameKana = "";
        //        this.EmployeeTag = "";
        //    }

        //}

    }
}
