namespace HAT_F_api.CustomModels
{
    public enum ApiResultType
    {
        /// <summary>
        /// 未設定
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// API呼び出しは成功しました。
        /// </summary>
        Success = 1,

        /// <summary>
        /// クライアントでネットワークエラーが発生しました。
        /// </summary>
        ClientNetworkError = 2,

        /// <summary>
        /// API 呼出が拒否されました。
        /// </summary>
        ServerForbidden = 3,

        /// <summary>
        /// サーバー内部エラーが発生しました。
        /// </summary>
        ServerInternalError = 4,

        /// <summary>
        /// ログインが拒否されました。
        /// </summary>
        LoginDenied = 5,

        /// <summary>
        /// エラー全般（使用箇所はコード修正対象）
        /// </summary>
        [Obsolete]
        ApiGenericError = 0xFFFF + 1,

        /// <summary>
        /// ビジネスロジックでの入力チェックエラーが発生しました。
        /// </summary>
        ApiBuissnessLogicValidationError = 0xFFFF + 2,

        /// <summary>
        /// 楽観的排他ロックでエラーが発生しました。
        /// </summary>
        ApiDbUpdateConcurrencyError = 0xFFFF + 3,
    }
}
