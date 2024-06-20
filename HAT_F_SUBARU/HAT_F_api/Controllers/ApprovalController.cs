using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        /// <summary>承認サービス</summary>
        private readonly ApprovalService _approvalService;

        /// <summary>更新系サービス</summary>
        private readonly HatFUpdateService _hatFUpdateService;

        /// <summary>コンストラクタ</summary>
        /// <param name="approvalService">承認サービス</param>
        /// <param name="hatFUpdateService"></param>
        public ApprovalController(ApprovalService approvalService, HatFUpdateService hatFUpdateService)
        {
            _approvalService = approvalService;
            _hatFUpdateService = hatFUpdateService;
        }

        /// <summary>
        /// 承認情報の取得
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet("{approvalId}")]
        public async Task<ActionResult<ApiResponse<ApprovalSuite>>> GetApprovalSuitesAsync(string approvalId)
        {
            var result = await _approvalService.GetApprovalSuitesAsync(approvalId);
            return new ApiOkResponse<ApprovalSuite>(result);
        }

        /// <summary>
        /// 承認情報の取得（返品用）
        /// </summary>
        /// <param name="approvalType">承認種別</param>
        /// <param name="returnId">返品ID</param>
        /// <returns></returns>
        [HttpGet("return/{returnId}")]
        public async Task<ActionResult<ApiResponse<ApprovalSuite>>> GetRetrunApprovalSuitesAsync(string approvalType, string returnId)
        {
            var result = await _approvalService.GetReturnApprovalSuitesAsync(approvalType, returnId);
            return new ApiOkResponse<ApprovalSuite>(result);
        }

        /// <summary>
        /// 承認の新規登録
        /// </summary>
        /// <param name="approvalType">承認種別</param>
        /// <param name="request">承認情報</param>
        /// <returns></returns>
        [HttpPut("{approvalType}")]
        public async Task<ActionResult<ApiResponse<ApprovalSuite>>> PutApprovalsAsync(string approvalType, [FromBody] ApprovalRequest request)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {

                // 承認申請データを作成
                var approvalId = _approvalService.GenerateApprovalId();

                Approval approval = _approvalService.CreateApproval(approvalId, approvalType, request);
                approval.ApprovalStatus = (int)ApprovalStatus.Request;
                approval.Creator = request.EmpId;

                ApprovalProcedure approvalProcedure = _approvalService.CreateApprovalProcedure(approvalId, request);
                approvalProcedure.Creator = request.EmpId;

                var isInsert = true;
                var approvalData = _approvalService.SetApprovalData(approval, approvalProcedure);
                if (approvalType.Equals(ApprovalType.U1.ToString()))
                {
                    // U1：仕入売上訂正
                    // 承認対象データを作成
                    List<ApprovalTarget> approvalTargets = _approvalService.SetApprovalTargetInsertAsync(approval.ApprovalTargetId, request);
                    approvalData.ApprovalTargets = approvalTargets;
                }
                else if (approvalType.Equals(ApprovalType.R1.ToString()))
                {
                    // R1：返品入力
                    // 返品対象データを作成
                    var returnData = await _approvalService.SetReturnInsertAsync(approvalId, request);
                    approvalData.ReturnData = returnData;
                }
                else if (approvalType.Equals(ApprovalType.R5.ToString()))
                {
                    // R5：返品入庫
                    // 返品対象データを更新

                    var returnData = await _approvalService.SetReturnStockUpdateAsync(approvalId, request);
                    approvalData.ReturnData = returnData;
                    isInsert = false;
                    await _approvalService.InsertReturnApprovalAsync(approvalData);
                }
                else if (approvalType.Equals(ApprovalType.R9.ToString()))
                {
                    // R9：返品入庫入力完了
                    // 返品対象データを更新
                    var returnData = await _approvalService.SetReturnCompletedUpdateAsync(approvalId, request);
                    approvalData.ReturnData = returnData;
                    isInsert = false;
                    await _approvalService.InsertReturnApprovalAsync(approvalData);
                }
                else if (approvalType.Equals(ApprovalType.S1.ToString()))
                {
                    // S1 : 売上調整
                    request.ViewSalesAdjustments.ForEach(x => x.承認要求番号 = approvalId);
                    await _hatFUpdateService.PutSalesAdjustmentsAsync(request.ViewSalesAdjustments);
                }

                // 承認申請データを登録
                if (isInsert)
                {
                    await _approvalService.InsertApprovalAsync(approvalData);
                }

                return await _approvalService.GetApprovalSuitesAsync(approval.ApprovalId);

            });
        }

        /// <summary>
        /// 承認の更新
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="approvalType">承認種別</param>
        /// <param name="request">承認情報</param>
        /// <returns></returns>
        [HttpPut("{approvalId}/{approvalType}")]
        public async Task<ActionResult<ApiResponse<ApprovalSuite>>> PutApprovalsAsync(string approvalId, string approvalType, [FromBody] ApprovalRequest request)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {

                // 現況の承認情報を取得
                var suite = await _approvalService.GetApprovalSuitesAsync(approvalId);

                // 新規登録用の承認処理を生成
                var approvalProcedure = _approvalService.CreateApprovalProcedure(approvalId, request);

                // リクエストを検証
                var approval = suite.Approval;
                var requestType = (ApprovalResult)request.RequestType;

                // 申請者からの「申請」リクエスト
                if (requestType == ApprovalResult.Request)
                {
                    // 申請中
                    approval.ApprovalStatus = (int)ApprovalStatus.Request;
                    // 申請者の変更
                    approval.RequestorEmpId = request.EmpId;
                    // 承認者情報の変更
                    _approvalService.SetApplover(approval, request);
                    approval.Updater = request.EmpId;

                    // 承認動作:申請
                    approvalProcedure.ApprovalResult = (int)ApprovalResult.Request;
                    approvalProcedure.Creator = request.EmpId;

                    var requestData = _approvalService.SetApprovalData(approval, approvalProcedure);
                    if (approvalType.Equals(ApprovalType.U1.ToString()))
                    {
                        // U1：仕入売上訂正
                        // 承認対象データを作成
                        List<ApprovalTarget> approvalTargets = await _approvalService.SetApprovalTargetUpdateAsync(approval.ApprovalTargetId, request);
                        requestData.ApprovalTargets = approvalTargets;
                    }
                    else if (approvalType.Equals(ApprovalType.R1.ToString()))
                    {
                        // R1：返品入力
                        // 返品対象データを作成
                        var returnData = await _approvalService.SetReturnUpdateAsync(approvalId, request);
                        requestData.ReturnData = returnData;
                    }
                    else if (approvalType.Equals(ApprovalType.R5.ToString()))
                    {
                        // R5：返品入庫
                        // 返品対象データを更新
                        var returnData = await _approvalService.SetReturnStockDetailUpdateAsync(approvalId, request);
                        requestData.ReturnData = returnData;
                    }
                    else if (approvalType.Equals(ApprovalType.R9.ToString()))
                    {
                        // R9：返品入庫入力完了
                        // 返品対象データを更新
                        var returnData = await _approvalService.SetReturnCompletedDetailUpdateAsync(approvalId, request);
                        requestData.ReturnData = returnData;
                    }
                    else if (approvalType.Equals(ApprovalType.S1.ToString()))
                    {
                        // S1 : 売上調整（とくにすることはない）
                    }

                    // 承認申請データを登録
                    await _approvalService.UpdateApprovalAsync(requestData);

                }
                // 承認者からの「差し戻し」リクエスト
                else if (requestType == ApprovalResult.Reject)
                {

                    // リクエストがどの承認者によるものかを計算
                    // 承認処理の「申請」「承認」を(+1)、「差し戻し」を(-1)としてカウント
                    // カウント結果が 0:申請者、N:第N承認者（ただし、N=承認者数の場合、最終承認者）
                    var requestedCount = suite.ApprovalProcedures.Where(a => a.ApprovalResult == (int)ApprovalResult.Request).Count();
                    var approvedCount = suite.ApprovalProcedures.Where(a => a.ApprovalResult == (int)ApprovalResult.Approve).Count();
                    var rejectedCount = suite.ApprovalProcedures.Where(a => a.ApprovalResult == (int)ApprovalResult.Reject).Count();
                    var requestor = (requestedCount + approvedCount) - rejectedCount;

                    // 第1承認者からの差し戻しは、申請者に戻す
                    if (requestor == 1)
                    {
                        approval.ApprovalStatus = null;
                    }
                    else
                    {
                        approval.ApprovalStatus = (int)ApprovalStatus.Approve;
                    }

                    // 承認者情報の変更
                    _approvalService.SetApplover(approval, request);
                    approval.Updater = request.EmpId;

                    // 承認動作:差し戻し
                    approvalProcedure.ApprovalResult = (int)ApprovalResult.Reject;
                    approvalProcedure.Creator = request.EmpId;

                    var rejectData = _approvalService.SetApprovalData(approval, approvalProcedure);

                    // 承認情報更新
                    await _approvalService.UpdateApprovalAsync(rejectData);

                }
                // 承認者からの「承認」リクエスト
                else if (requestType == ApprovalResult.Approve)
                {

                    // 承認者情報の変更
                    _approvalService.SetApplover(approval, request);

                    // 承認中
                    approval.ApprovalStatus = (int)ApprovalStatus.Approve;
                    approval.Updater = request.EmpId;

                    // 承認動作:承認済
                    approvalProcedure.ApprovalResult = (int)ApprovalResult.Approve;
                    approvalProcedure.Creator = request.EmpId;

                    var approveData = _approvalService.SetApprovalData(approval, approvalProcedure);

                    // 承認情報更新
                    await _approvalService.UpdateApprovalAsync(approveData);

                }
                // 最終承認者からの「承認」リクエスト
                else if (requestType == ApprovalResult.FinalApprove)
                {

                    // 承認者情報の変更
                    _approvalService.SetApplover(approval, request);

                    // 最終承認済
                    approval.ApprovalStatus = (int)ApprovalStatus.FinalApprove;
                    approval.Updater = request.EmpId;

                    // 承認動作:最終承認済
                    approvalProcedure.ApprovalResult = (int)ApprovalResult.FinalApprove;
                    approvalProcedure.Creator = request.EmpId;

                    var finalData = _approvalService.SetFinalApprovalData(approval, approvalProcedure);
                    if (approvalType.Equals(ApprovalType.U1.ToString()))
                    {
                        // U1：仕入売上訂正
                        // 仕入訂正データ作成
                        var puData = await _approvalService.CreatePurchaseCorrectionAsync(approval.ApprovalTargetId, approvalType, request.EmpId);
                        finalData.PurchaseData = puData;

                        // 売上訂正データ作成
                        var salesData = await _approvalService.CreateSalesCorrectionAsync(approval.ApprovalTargetId, approvalType, request.EmpId);
                        finalData.SalesData = salesData;
                    }
                    else if (approvalType.Equals(ApprovalType.R9.ToString()))
                    {
                        // R9：返品入庫入力完了
                        // 売上返品データ作成
                        var returnData = await _approvalService.CreateSalesReturnAsync(approval.ApprovalId, approvalType, request.EmpId);
                        finalData.SalesData = returnData;
                    }

                    // 最終承認の処理
                    await _approvalService.FinalApprovalAsync(finalData);

                }

                return await _approvalService.GetApprovalSuitesAsync(approvalId);

            });
        }
    }
}
