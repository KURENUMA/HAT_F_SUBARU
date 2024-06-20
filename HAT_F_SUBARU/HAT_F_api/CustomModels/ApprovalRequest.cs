using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class ApprovalRequest
    {
        /// <summary>
        /// 社員ID
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 承認動作,0:申請 1:差し戻し 2:承認 3:最終承認
        /// </summary>
        public int RequestType { get; set; }

        /// <summary>
        /// 承認コメント
        /// </summary>
        public string ApprovalComment { get; set; }

        /// <summary>
        /// 承認者1:社員ID
        /// </summary>
        public int? Approver1EmpId { get; set; }

        /// <summary>
        /// 承認者2:社員ID
        /// </summary>
        public int? Approver2EmpId { get; set; }

        /// <summary>
        /// 最終承認者:社員ID
        /// </summary>
        public int? FinalApproverEmpId { get; set; }

        /// <summary>
        /// 仕入売上訂正対象データ
        /// </summary>
        public List<PurchaseSalesCorrection> PuSalesCorrection { get; set; }

        /// <summary>
        /// 返品入力データ
        /// </summary>
        public List<SalesReturn> SalesReturns { get; set; }

        /// <summary>
        /// 売上調整データ
        /// </summary>
        public List<ViewSalesAdjustment> ViewSalesAdjustments { get; set; }
    }
}
