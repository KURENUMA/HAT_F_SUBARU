namespace HAT_F_api.CustomModels
{
    /// <summary>与信チェック結果</summary>
    public enum CheckCreditResult
    {
        /// <summary>問題なし</summary>
        None = 0,

        /// <summary>指定の取引先が見つからない</summary>
        NoCompany,

        /// <summary>与信限度額超過</summary>
        CreditOver,
    }
}