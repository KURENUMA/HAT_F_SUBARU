namespace HAT_F_api.CustomModels
{
    public enum HatFUserRole : int
    {
        /// <summary>
        /// マスター編集
        /// </summary>
        MasterEdit = 1,

        /// <summary>
        /// 申請承認(廃止予定・申請の種類ごとに分ける)
        /// </summary>
        ApplicationApproval = 2,

        /// <summary>
        /// 売上仕入訂正申請の承認権限
        /// </summary>
        ApplicationSaPurchaseApproval = 11,

        /// <summary>
        /// 返品入力申請の承認権限
        /// </summary>
        ApplicationReturnProductApproval = 12,

        /// <summary>
        /// 返品商品の検品結果申請の承認権限
        /// </summary>
        ApplicationReturnProductInspectionApproval = 13,

        /// <summary>
        /// 返品商品の返金申請の承認権限
        /// </summary>
        ApplicationReturnProductReturnAmountApproval = 14,


#if DEBUG
        /// <summary>
        /// 開発用権限
        /// </summary>
        Developer = 99,
#endif
    }
}
