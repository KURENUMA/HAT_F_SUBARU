namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// ログイン要求情報
    /// </summary>
    public class HatFLoginRequest
    {
        /// <summary>
        /// 社員番号
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// ログインパスワード
        /// </summary>
        public string LoginPassword { get; set; }
    }
}
