using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;
using static HAT_F_api.CustomModels.ApprovalSuite;

namespace HAT_F_api.Services
{
    public enum ApprovalStatus
    {
        // 申請中
        Request = 0,
        // 承認中
        Approve = 1,
        // 承認済
        FinalApprove = 9
    }

    public enum ApprovalResult
    {
        // 申請
        Request = 0,
        // 差し戻し
        Reject = 1,
        // 承認
        Approve = 2,
        // 最終承認
        FinalApprove = 3
    }

    /// <summary>承認種別</summary>
    public enum ApprovalType
    {
        /// <summary>仕入売上訂正</summary>
        U1,
        /// <summary>返品入力</summary>
        R1,
        /// <summary>返品入庫</summary>
        R5,
        /// <summary>返品入庫入力完了</summary>
        R9,
        /// <summary>売上調整</summary>
        S1,
    }

    public enum PuType
    {
        // 仕入
        Purchase = 1,
        // 仕入返品
        PurchaseReturn = 2
    }

    public enum SalesType
    {
        // 売上
        Sales = 1,
        // 売上返品
        SalesReturn = 2
    }

    public class ApprovalService
    {

        private HatFContext _hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext;
        private SequenceNumberService _sequenceNumberService;
        private UpdateInfoSetter _updateInfoSetter;

        public ApprovalService(HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, SequenceNumberService sequenceNumberService, UpdateInfoSetter updateInfoSetter)
        {
            _hatFContext = hatFContext;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _sequenceNumberService = sequenceNumberService;
            _updateInfoSetter = updateInfoSetter;
        }

        public string GenerateApprovalId()
        {
            DateTime now = _hatFApiExecutionContext.ExecuteDateTimeJst;
            return string.Format("A{0}", now.ToString("yyyyMMddHHmmss"));
        }

        public string GenerateApprovalProcedureId()
        {
            DateTime now = _hatFApiExecutionContext.ExecuteDateTimeJst;
            return string.Format("AP{0}", now.ToString("yyyyMMddHHmmss"));
        }

        public string GenerateApprovalTargetId()
        {
            DateTime now = _hatFApiExecutionContext.ExecuteDateTimeJst;
            return string.Format("AT{0}", now.ToString("yyyyMMddHHmmss"));
        }

        public class ApprovalData
        {
            public Approval Approval { get; set; }
            public ApprovalProcedure ApprovalProcedure { get; set; }
            public List<ApprovalTarget> ApprovalTargets { get; set; } = new List<ApprovalTarget>();
            public ReturnData ReturnData { get; set; }
        }

        public class FinalApprovalData
        {
            public Approval Approval { get; set; }
            public ApprovalProcedure ApprovalProcedure { get; set; }
            public List<PurchaseData> PurchaseData { get; set; } = new List<PurchaseData>();
            public List<SalesData> SalesData { get; set; } = new List<SalesData>();
        }

        public class PurchaseData
        {
            public Pu Pu { get; set; }
            public List<PuDetail> PuDetails { get; set; } = new List<PuDetail>();
        }

        public class SalesData
        {
            public Sale Sales { get; set; }
            public List<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
        }

        public class ReturnData
        {
            public ReturningProduct Return { get; set; }
            public List<ReturningProductsDetail> ReturnDetails { get; set; } = new List<ReturningProductsDetail>();
        }

        public Approval CreateApproval(string approvalId, string approvalType, ApprovalRequest request)
        {
            Approval approval = new Approval();
            approval.ApprovalId = approvalId;
            approval.ApprovalType = approvalType;
            approval.ApprovalTargetId = GenerateApprovalTargetId();
            approval.ApprovalStatus = (int)ApprovalStatus.Request;
            approval.RequestorEmpId = request.EmpId;
            SetApplover(approval, request);

            return approval;
        }

        public void SetApplover(Approval approval, ApprovalRequest request)
        {
            // 承認者情報の変更
            if (request.Approver1EmpId != null)
            {
                approval.Approver1EmpId = request.Approver1EmpId.Value;
            }

            if (request.Approver2EmpId != null)
            {
                approval.Approver2EmpId = request.Approver2EmpId.Value;
            }

            if (request.FinalApproverEmpId != null)
            {
                approval.FinalApproverEmpId = request.FinalApproverEmpId.Value;
            }
        }

        public ApprovalProcedure CreateApprovalProcedure(string approvalId, ApprovalRequest request)
        {
            ApprovalProcedure approvalProcedure = new ApprovalProcedure();
            approvalProcedure.ApprovalProcedureId = GenerateApprovalProcedureId();
            approvalProcedure.ApprovalId = approvalId;
            approvalProcedure.EmpId = request.EmpId;
            approvalProcedure.ApprovalResult = (int)ApprovalResult.Request;
            approvalProcedure.ApprovalComment = request.ApprovalComment;

            return approvalProcedure;
        }

        public ApprovalData SetApprovalData(Approval approval, ApprovalProcedure approvalProcedure)
        {
            ApprovalData approvalData = new ApprovalData();
            approvalData.Approval = approval;
            approvalData.ApprovalProcedure = approvalProcedure;

            return approvalData;
        }

        public FinalApprovalData SetFinalApprovalData(Approval approval, ApprovalProcedure approvalProcedure)
        {
            FinalApprovalData approvalData = new FinalApprovalData();
            approvalData.Approval = approval;
            approvalData.ApprovalProcedure = approvalProcedure;

            return approvalData;
        }

        /// <summary>
        /// 承認情報一式を取得
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        public async Task<ApprovalSuite> GetApprovalSuitesAsync(string approvalId)
        {
            ApprovalSuite suite = new ApprovalSuite();
            var approval = await _hatFContext.Approvals.Where(a => a.ApprovalId == approvalId).FirstAsync();
            if (approval != null)
            {
                var query = _hatFContext.ApprovalProcedures
                                         .Where(p => p.ApprovalId == approvalId)
                                         .OrderBy(p => p.ApprovalDate)
                                         .Join(
                                             _hatFContext.Employees,
                                             ap => ap.EmpId,
                                             e => e.EmpId,
                                             (ap, e) => new ApprovalProcedureEx
                                             {
                                                 ApprovalProcedureId = ap.ApprovalProcedureId,
                                                 ApprovalId = ap.ApprovalId,
                                                 EmpId = ap.EmpId,
                                                 ApprovalResult = ap.ApprovalResult,
                                                 ApprovalComment = ap.ApprovalComment,
                                                 ApprovalDate = ap.ApprovalDate,
                                                 CreateDate = ap.CreateDate,
                                                 Creator = ap.Creator,
                                                 UpdateDate = ap.UpdateDate,
                                                 Updater = ap.Updater,
                                                 EmpName = e.EmpName
                                             });

                List<ApprovalProcedureEx> approvalProcedures = await query.ToListAsync();

                suite.Approval = approval;
                if(approvalProcedures.Count == 0)
                {
                    suite.ApprovalProcedures = approvalProcedures;
                }
                else
                {
                    suite.ApprovalProcedures = approvalProcedures;
                    suite.LatestApprovalResult = approvalProcedures.Last().ApprovalResult;
                }
            }
            return suite;
        }

        /// <summary>
        /// 承認対象（新規）を作成
        /// </summary>
        /// <param name="approvalTargetId">承認対象ID</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public List<ApprovalTarget> SetApprovalTargetInsertAsync(string approvalTargetId, ApprovalRequest request)
        {

            var approvalTargets = new List<ApprovalTarget>();
            short rowNo = 1;

            foreach (var correction in request.PuSalesCorrection)
            {
                var approvalTarget = new ApprovalTarget();
                approvalTarget.ApprovalTargetId = approvalTargetId;
                approvalTarget.ApprovalTargetSub = rowNo;
                approvalTarget.PuNo = correction.PuNo;
                approvalTarget.PuRowNo = correction.PuRowNo;
                approvalTarget.SupCode = correction.SupCode;
                approvalTarget.SupSubNo = correction.SupSubNo;
                approvalTarget.PoPrice = correction.PoPrice;
                approvalTarget.PuQuantity = correction.PuQuantity;
                approvalTarget.SalesNo = correction.SalesNo;
                approvalTarget.RowNo = correction.RowNo;
                approvalTarget.Quantity = correction.Quantity;
                approvalTarget.Unitprice = correction.Unitprice;
                approvalTarget.CompCode = correction.CompCode;
                approvalTarget.Creator = request.EmpId;

                approvalTargets.Add(approvalTarget);

                rowNo++;

            }

            return approvalTargets;
        }

        /// <summary>
        /// 承認対象（更新）を作成
        /// </summary>
        /// <param name="approvalTargetId">承認対象ID</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<List<ApprovalTarget>> SetApprovalTargetUpdateAsync(string approvalTargetId, ApprovalRequest request)
        {

            var approvalTargets = new List<ApprovalTarget>();

            var query = await _hatFContext.ApprovalTargets.Where(x => x.ApprovalTargetId == approvalTargetId).ToListAsync();
            short rowNo = query.Max(x => x.ApprovalTargetSub);
            rowNo++;

            foreach (var correction in request.PuSalesCorrection)
            {
                var approvalTarget = new ApprovalTarget();

                if (string.IsNullOrEmpty(correction.ApprovalTargetId))
                {
                    approvalTarget.ApprovalTargetId = approvalTargetId;
                    approvalTarget.ApprovalTargetSub = rowNo;
                    approvalTarget.PuNo = correction.PuNo;
                    approvalTarget.PuRowNo = correction.PuRowNo;
                    approvalTarget.SupCode = correction.SupCode;
                    approvalTarget.SupSubNo = correction.SupSubNo;
                    approvalTarget.PoPrice = correction.PoPrice;
                    approvalTarget.PuQuantity = correction.PuQuantity;
                    approvalTarget.SalesNo = correction.SalesNo;
                    approvalTarget.RowNo = correction.RowNo;
                    approvalTarget.Quantity = correction.Quantity;
                    approvalTarget.Unitprice = correction.Unitprice;
                    approvalTarget.CompCode = correction.CompCode;
                    approvalTarget.Creator = request.EmpId;

                    approvalTargets.Add(approvalTarget);

                    rowNo++;

                }
                else
                {
                    approvalTarget = await _hatFContext.ApprovalTargets.Where(x => x.ApprovalTargetId == correction.ApprovalTargetId
                                                                                    && x.ApprovalTargetSub == correction.ApprovalTargetSub).FirstAsync();

                    approvalTarget.PuNo = correction.PuNo;
                    approvalTarget.PuRowNo = correction.PuRowNo;
                    approvalTarget.SupCode = correction.SupCode;
                    approvalTarget.SupSubNo = correction.SupSubNo;
                    approvalTarget.PoPrice = correction.PoPrice;
                    approvalTarget.PuQuantity = correction.PuQuantity;
                    approvalTarget.SalesNo = correction.SalesNo;
                    approvalTarget.RowNo = correction.RowNo;
                    approvalTarget.Quantity = correction.Quantity;
                    approvalTarget.Unitprice = correction.Unitprice;
                    approvalTarget.CompCode = correction.CompCode;
                    approvalTarget.Updater = request.EmpId;

                    approvalTargets.Add(approvalTarget);
                }
            }

            return approvalTargets;
        }

        /// <summary>
        /// 返品入力_返品対象（新規）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnInsertAsync(string approvalId, ApprovalRequest request)
        {

            var returnData = new ReturnData();
            bool isHeaderNew = true;
            short rowNo = 1;

            var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.ReturningProductsReturningProductsID);
            var strSeqNo = $"{seqNo:D10}";

            foreach (var salesReturn in request.SalesReturns)
            {

                if (isHeaderNew)
                {
                    var returnProduct = new ReturningProduct();

                    returnProduct.ReturningProductsId = strSeqNo;
                    returnProduct.ApprovalId = approvalId;
                    returnProduct.Creator = request.EmpId;
                    returnData.Return = returnProduct;

                    isHeaderNew = false;
                }

                var returnDetail = new ReturningProductsDetail();
                returnDetail.ReturningProductsId = strSeqNo;
                returnDetail.RowNo = rowNo;
                returnDetail.SalesNo = salesReturn.SalesNo;
                returnDetail.SalesRowNo = salesReturn.SalesRowNo;
                returnDetail.DenNo = salesReturn.DenNo;
                returnDetail.ProdCode = salesReturn.ProdCode;
                returnDetail.Quantity = salesReturn.Quantity;
                returnDetail.ReturnRequestQuantity = salesReturn.ReturnRequestQuantity;
                returnDetail.SellUnitPrice = salesReturn.SellUnitPrice;
                returnDetail.StockUnitPrice = salesReturn.StockUnitPrice;
                returnDetail.SalesCd = salesReturn.SalesCd;
                returnDetail.Creator = request.EmpId;
                returnData.ReturnDetails.Add(returnDetail);

                rowNo++;

            }

            return returnData;
        }

        /// <summary>
        /// 返品入力_返品対象（更新）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnUpdateAsync(string approvalId, ApprovalRequest request)
        {

            var returnData = new ReturnData();

            var query = request.SalesReturns.Where(x => !string.IsNullOrEmpty(x.ReturningProductsId)).FirstOrDefault();
            var returnId = query.ReturningProductsId;

            var rowQuery = await _hatFContext.ReturningProductsDetails.Where(x => x.ReturningProductsId == returnId).ToListAsync();
            short rowNo = (short)rowQuery.Max(x => x.RowNo);
            rowNo++;

            foreach (var salesReturn in request.SalesReturns)
            {

                var returnDetail = new ReturningProductsDetail();

                if (string.IsNullOrEmpty(salesReturn.ReturningProductsId))
                {
                    returnDetail.ReturningProductsId = returnId;
                    returnDetail.RowNo = rowNo;
                    returnDetail.SalesNo = salesReturn.SalesNo;
                    returnDetail.SalesRowNo = salesReturn.SalesRowNo;
                    returnDetail.DenNo = salesReturn.DenNo;
                    returnDetail.ProdCode = salesReturn.ProdCode;
                    returnDetail.Quantity = salesReturn.Quantity;
                    returnDetail.ReturnRequestQuantity = salesReturn.ReturnRequestQuantity;
                    returnDetail.SellUnitPrice = salesReturn.SellUnitPrice;
                    returnDetail.StockUnitPrice = salesReturn.StockUnitPrice;
                    returnDetail.SalesCd = salesReturn.SalesCd;
                    returnDetail.Creator = request.EmpId;

                    returnData.ReturnDetails.Add(returnDetail);

                    rowNo++;

                }
                else
                {

                    returnDetail = await _hatFContext.ReturningProductsDetails.Where(x => x.ReturningProductsId == salesReturn.ReturningProductsId
                                                                                            && x.RowNo == salesReturn.RowNo).FirstAsync();

                    returnDetail.SalesNo = salesReturn.SalesNo;
                    returnDetail.SalesRowNo = salesReturn.SalesRowNo;
                    returnDetail.DenNo = salesReturn.DenNo;
                    returnDetail.ProdCode = salesReturn.ProdCode;
                    returnDetail.Quantity = salesReturn.Quantity;
                    returnDetail.ReturnRequestQuantity = salesReturn.ReturnRequestQuantity;
                    returnDetail.SellUnitPrice = salesReturn.SellUnitPrice;
                    returnDetail.StockUnitPrice = salesReturn.StockUnitPrice;
                    returnDetail.SalesCd = salesReturn.SalesCd;
                    returnDetail.Updater = request.EmpId;

                    returnData.ReturnDetails.Add(returnDetail);
                }
            }

            return returnData;
        }

        /// <summary>
        /// 返品入庫_返品対象（更新）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnStockUpdateAsync(string approvalId, ApprovalRequest request)
        {
            var returnData = new ReturnData();
            var returnId = request.SalesReturns.FirstOrDefault().ReturningProductsId;
            var data = await _hatFContext.ReturningProducts.Where(x => x.ReturningProductsId == returnId).FirstOrDefaultAsync();

            data.StockApprovalId = approvalId;
            data.Updater = request.EmpId;

            returnData.Return = data;

            var detailData = await SetReturnStockDetailUpdateAsync(approvalId, request);
            returnData.ReturnDetails = detailData.ReturnDetails;

            return returnData;
        }

        /// <summary>
        /// 返品入庫_返品対象（更新）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnStockDetailUpdateAsync(string approvalId, ApprovalRequest request)
        {

            var returnData = new ReturnData();

            foreach (var salesReturn in request.SalesReturns)
            {
                var returnDetail = new ReturningProductsDetail();

                returnDetail = await _hatFContext.ReturningProductsDetails.Where(x => x.ReturningProductsId == salesReturn.ReturningProductsId
                                                                                        && x.RowNo == salesReturn.RowNo).FirstOrDefaultAsync();

                returnDetail.ReturnQuantity = salesReturn.ReturnQuantity;
                returnDetail.Updater = request.EmpId;

                returnData.ReturnDetails.Add(returnDetail);
            }

            return returnData;
        }

        /// <summary>
        /// 返品入庫入力完了_返品対象（更新）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnCompletedUpdateAsync(string approvalId, ApprovalRequest request)
        {

            var returnData = new ReturnData();
            var returnId = request.SalesReturns.FirstOrDefault().ReturningProductsId;
            var data = await _hatFContext.ReturningProducts.Where(x => x.ReturningProductsId == returnId).FirstOrDefaultAsync();

            data.RefundApprovalId = approvalId;
            data.Updater = request.EmpId;

            returnData.Return = data;

            var detailData = await SetReturnCompletedDetailUpdateAsync(approvalId, request);
            returnData.ReturnDetails = detailData.ReturnDetails;

            return returnData;
        }

        /// <summary>
        /// 返品入庫入力完了_返品対象（更新）を作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="request">承認データ</param>
        /// <returns></returns>
        public async Task<ReturnData> SetReturnCompletedDetailUpdateAsync(string approvalId, ApprovalRequest request)
        {

            var returnData = new ReturnData();

            foreach (var salesReturn in request.SalesReturns)
            {
                var returnDetail = new ReturningProductsDetail();

                returnDetail = await _hatFContext.ReturningProductsDetails.Where(x => x.ReturningProductsId == salesReturn.ReturningProductsId
                                                                                        && x.RowNo == salesReturn.RowNo).FirstOrDefaultAsync();

                returnDetail.RefundUnitPrice = salesReturn.RefundUnitPrice;
                returnDetail.Updater = request.EmpId;

                returnData.ReturnDetails.Add(returnDetail);
            }

            return returnData;
        }

        /// <summary>
        /// 仕入訂正データ作成
        /// </summary>
        /// <param name="approvalTargetId">承認対象ID</param>
        /// <param name="approvalType">承認種別</param>
        /// <param name="empId">社員ID</param>
        /// <returns></returns>
        public async Task<List<PurchaseData>> CreatePurchaseCorrectionAsync(string approvalTargetId, string approvalType, int empId)
        {

            Dictionary<string, PurchaseData> dataMap = [];
            
            // 仕入訂正対象を取得
            var approvalTargets = await _hatFContext.ApprovalTargets.Where(x => x.ApprovalTargetId == approvalTargetId 
                                                                            && !string.IsNullOrEmpty(x.PuNo)).ToListAsync();

            foreach (var target in approvalTargets)
            {

                bool isHeaderNew = true;
                string puKey = target.PuNo + "1";
                string returnKey = target.PuNo + "2";
                Pu pu = new();
                List<PuDetail> puDetails = [];
                Pu puReturn = new();
                List<PuDetail> puReturnDetails = [];
                short rowNo = 1;
                short returnRowNo = 1;

                if (dataMap.TryGetValue(puKey, out PurchaseData value))
                {
                    isHeaderNew = false;
                    pu = value.Pu;
                    puDetails = value.PuDetails;

                    rowNo = puDetails.Max(x => x.PuRowNo);
                    rowNo++;
                }

                if (dataMap.TryGetValue(returnKey, out PurchaseData returnValue))
                {
                    puReturn = returnValue.Pu;
                    puReturnDetails = returnValue.PuDetails;

                    returnRowNo = puReturnDetails.Max(x => x.PuRowNo);
                    returnRowNo++;
                }

                if (isHeaderNew)
                {

                    // 元仕入
                    var orgPu = await _hatFContext.Pus.Where(x => x.PuNo == target.PuNo).FirstOrDefaultAsync();

                    var seqNoReturn = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                    puReturn.PuNo = $"{seqNoReturn:D10}";
                    puReturn.DenNo = orgPu.DenNo;
                    puReturn.PuDate = orgPu.PuDate;
                    puReturn.PuType = (short)PuType.PurchaseReturn;
                    puReturn.SupCode =orgPu.SupCode;
                    puReturn.SupSubNo = orgPu.SupSubNo;
                    puReturn.EmpId = orgPu.EmpId;
                    puReturn.StartDate = orgPu.StartDate;
                    puReturn.PoNo = orgPu.PoNo;
                    puReturn.DeptCode = orgPu.DeptCode;
                    puReturn.PuAmmount = 0;
                    puReturn.CmpTax = 0;
                    puReturn.HatOrderNo = orgPu.HatOrderNo;
                    puReturn.DenFlg = orgPu.DenFlg;
                    puReturn.Creator = empId;

                    var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                    pu.PuNo = $"{seqNo:D10}";
                    pu.DenNo = orgPu.DenNo;
                    pu.PuDate = orgPu.PuDate;
                    pu.PuType = (short)PuType.Purchase;
                    pu.SupCode = string.IsNullOrEmpty(target.SupCode) ? orgPu.SupCode : target.SupCode;
                    pu.SupSubNo = !target.SupSubNo.HasValue ? orgPu.SupSubNo : target.SupSubNo;
                    pu.EmpId = orgPu.EmpId;
                    pu.StartDate = orgPu.StartDate;
                    pu.PoNo = orgPu.PoNo;
                    pu.DeptCode = orgPu.DeptCode;
                    pu.PuAmmount = 0;
                    pu.CmpTax = 0;
                    pu.HatOrderNo = orgPu.HatOrderNo;
                    pu.DenFlg = orgPu.DenFlg;
                    pu.Creator = empId;

                }

                // 明細
                var orgPuDetail = await _hatFContext.PuDetails.Where(x => x.PuNo == target.PuNo && x.PuRowNo == target.PuRowNo).FirstOrDefaultAsync();

                var returnDetail = new PuDetail();
                returnDetail.PuNo = puReturn.PuNo;
                returnDetail.PuRowNo = returnRowNo;
                returnDetail.PuRowDspNo = orgPuDetail.PuRowDspNo;
                returnDetail.PoRowNo = orgPuDetail.PoRowNo;
                returnDetail.ProdCode = orgPuDetail.ProdCode;
                returnDetail.ProdName = orgPuDetail.ProdName;
                returnDetail.WhCode = orgPuDetail.WhCode;
                returnDetail.PoPrice = orgPuDetail.PoPrice * -1;
                returnDetail.PuQuantity = (short)(orgPuDetail.PuQuantity * -1);
                returnDetail.CheckStatus = orgPuDetail.CheckStatus;
                returnDetail.PuPayYearMonth = orgPuDetail.PuPayYearMonth;
                returnDetail.Chuban = orgPuDetail.Chuban;
                returnDetail.PuCorrectionType = approvalType;
                returnDetail.OriginalPuNo = target.PuNo;
                returnDetail.OriginalPuRowNo = target.PuRowNo;
                returnDetail.Creator = empId;
                puReturnDetails.Add(returnDetail);

                var puDetail = new PuDetail();
                puDetail.PuNo = pu.PuNo;
                puDetail.PuRowNo = rowNo;
                puDetail.PuRowDspNo = orgPuDetail.PuRowDspNo;
                puDetail.PoRowNo = orgPuDetail.PoRowNo;
                puDetail.ProdCode = orgPuDetail.ProdCode;
                puDetail.ProdName = orgPuDetail.ProdName;
                puDetail.WhCode = orgPuDetail.WhCode;
                puDetail.PoPrice = !target.PoPrice.HasValue ? orgPuDetail.PoPrice : target.PoPrice;
                puDetail.PuQuantity = !target.PuQuantity.HasValue ? orgPuDetail.PuQuantity : (short)target.PuQuantity;
                puDetail.CheckStatus = orgPuDetail.CheckStatus;
                puDetail.PuPayYearMonth = orgPuDetail.PuPayYearMonth;
                puDetail.Chuban = orgPuDetail.Chuban;
                puDetail.PuCorrectionType = approvalType;
                puDetail.OriginalPuNo = target.PuNo;
                puDetail.OriginalPuRowNo = target.PuRowNo;
                puDetail.Creator = empId;
                puDetails.Add(puDetail);

                if (isHeaderNew)
                {
                    PurchaseData data = new()
                    {
                        Pu = pu,
                        PuDetails = puDetails
                    };
                    dataMap.Add(puKey, data);

                    PurchaseData returnData = new()
                    {
                        Pu = puReturn,
                        PuDetails = puReturnDetails
                    };
                    dataMap.Add(returnKey, returnData);
                }
            }

            return CorrectPurchase([.. dataMap.Values]);
        }

        /// <summary>
        /// 支払データの補正
        /// </summary>
        /// <param name="dataMap"></param>
        public List<PurchaseData> CorrectPurchase(List<PurchaseData> dataMap)
        {
            foreach (var data in dataMap)
            {
                var puDetails = data.PuDetails;
                var puAmmount = puDetails.Select(x => x.PoPrice * x.PuQuantity).Sum();
                if (data.Pu.PuType == (short)PuType.PurchaseReturn)
                {
                    puAmmount = puAmmount * -1;
                }
                data.Pu.PuAmmount = puAmmount;      // 仕入金額合計

                // TODO:消費税金額
            }

            return dataMap;
        }

        /// <summary>
        /// 売上訂正データ作成
        /// </summary>
        /// <param name="approvalTargetId">承認対象ID</param>
        /// <param name="approvalType">承認種別</param>
        /// <param name="empId">社員ID</param>
        /// <returns></returns>
        public async Task<List<SalesData>> CreateSalesCorrectionAsync(string approvalTargetId, string approvalType, int empId)
        {

            // 売上訂正データ作成
            Dictionary<string, SalesData> dataMap = [];

            var approvalTargets = await _hatFContext.ApprovalTargets.Where(x => x.ApprovalTargetId == approvalTargetId
                                                                            && !string.IsNullOrEmpty(x.SalesNo)).ToListAsync();

            foreach (var target in approvalTargets)
            {

                bool isHeaderNew = true;
                string salesKey = target.SalesNo + "1";
                string returnKey = target.SalesNo + "2";
                Sale sales = new();
                List<SalesDetail> salesDetails = [];
                Sale salesReturn = new();
                List<SalesDetail> salesReturnDetails = [];
                short rowNo = 1;
                short returnRowNo = 1;

                if (dataMap.TryGetValue(salesKey, out SalesData value))
                {
                    isHeaderNew = false;
                    sales = value.Sales;
                    salesDetails = value.SalesDetails;

                    rowNo = salesDetails.Max(x => x.RowNo);
                    rowNo++;
                }

                if (dataMap.TryGetValue(returnKey, out SalesData returnValue))
                {
                    salesReturn = returnValue.Sales;
                    salesReturnDetails = returnValue.SalesDetails;

                    returnRowNo = salesReturnDetails.Max(x => x.RowNo);
                    returnRowNo++;
                }

                if (isHeaderNew)
                {

                    // 元売上
                    var orgSales = await _hatFContext.Sales.Where(x => x.SalesNo == target.SalesNo).FirstOrDefaultAsync();

                    var seqNoReturn = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.SalesSalesNo);
                    salesReturn.SalesNo = $"{seqNoReturn:D10}";
                    salesReturn.ConstructionCode = orgSales.ConstructionCode;
                    salesReturn.OrderNo = orgSales.OrderNo;
                    salesReturn.SalesType = (short)SalesType.SalesReturn;
                    salesReturn.DeptCode = orgSales.DeptCode;
                    salesReturn.StartDate = orgSales.StartDate;
                    salesReturn.CompCode = orgSales.CompCode;
                    salesReturn.EmpId = orgSales.EmpId;
                    salesReturn.SalesAmnt = 0;
                    salesReturn.CmpTax = 0;
                    salesReturn.Creator = empId;

                    var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.SalesSalesNo);
                    sales.SalesNo = $"{seqNo:D10}";
                    sales.ConstructionCode = orgSales.ConstructionCode;
                    sales.OrderNo = orgSales.OrderNo;
                    sales.SalesType = (short)SalesType.Sales;
                    sales.DeptCode = orgSales.DeptCode;
                    sales.StartDate = orgSales.StartDate;
                    sales.CompCode = string.IsNullOrEmpty(target.CompCode) ? orgSales.CompCode : target.CompCode;
                    sales.EmpId = orgSales.EmpId;
                    sales.SalesAmnt = 0;
                    sales.CmpTax = 0;
                    sales.Creator = empId;

                }

                // 明細
                var orgSalesDetail = await _hatFContext.SalesDetails.Where(x => x.SalesNo == target.SalesNo && x.RowNo == target.RowNo).FirstOrDefaultAsync();

                var returnDetail = new SalesDetail();
                returnDetail.SalesNo = salesReturn.SalesNo;
                returnDetail.RowNo = returnRowNo;
                returnDetail.ProdCode = orgSalesDetail.ProdCode;
                returnDetail.ProdName = orgSalesDetail.ProdName;
                returnDetail.Unitprice = orgSalesDetail.Unitprice * -1;
                returnDetail.DeliveredQty = orgSalesDetail.DeliveredQty;
                returnDetail.Quantity = orgSalesDetail.Quantity * -1;
                returnDetail.Discount = orgSalesDetail.Discount;
                returnDetail.AutoJournalDate = orgSalesDetail.AutoJournalDate;
                returnDetail.DenNo = orgSalesDetail.DenNo;
                returnDetail.DenFlg = orgSalesDetail.DenFlg;
                returnDetail.HatOrderNo = orgSalesDetail.HatOrderNo;
                returnDetail.Chuban = orgSalesDetail.Chuban;
                returnDetail.SalesCorrectionType = approvalType;
                returnDetail.OriginalSalesNo = target.SalesNo;
                returnDetail.OriginalRowNo = target.RowNo;
                returnDetail.Creator = empId;
                salesReturnDetails.Add(returnDetail);

                var salesDetail = new SalesDetail();
                salesDetail.SalesNo = sales.SalesNo;
                salesDetail.RowNo = rowNo;
                salesDetail.ProdCode = orgSalesDetail.ProdCode;
                salesDetail.ProdName = orgSalesDetail.ProdName;
                salesDetail.Unitprice = !target.Unitprice.HasValue ? orgSalesDetail.Unitprice : (decimal)target.Unitprice;
                salesDetail.DeliveredQty = orgSalesDetail.DeliveredQty;
                salesDetail.Quantity = !target.Quantity.HasValue ? orgSalesDetail.Quantity : (int)target.Quantity;
                salesDetail.Discount = orgSalesDetail.Discount;
                salesDetail.AutoJournalDate = orgSalesDetail.AutoJournalDate;
                salesDetail.DenNo = orgSalesDetail.DenNo;
                salesDetail.DenFlg = orgSalesDetail.DenFlg;
                salesDetail.HatOrderNo = orgSalesDetail.HatOrderNo;
                salesDetail.Chuban = orgSalesDetail.Chuban;
                salesDetail.SalesCorrectionType = approvalType;
                salesDetail.OriginalSalesNo = target.SalesNo;
                salesDetail.OriginalRowNo = target.RowNo;
                salesDetail.Creator = empId;
                salesDetails.Add(salesDetail);

                if (isHeaderNew)
                {
                    SalesData data = new()
                    {
                        Sales = sales,
                        SalesDetails = salesDetails
                    };
                    dataMap.Add(salesKey, data);

                    SalesData returnData = new()
                    {
                        Sales = salesReturn,
                        SalesDetails = salesReturnDetails
                    };
                    dataMap.Add(returnKey, returnData);
                }
            }

            return CorrectSales([.. dataMap.Values]);

        }

        /// <summary>
        /// 売上返品データ作成
        /// </summary>
        /// <param name="approvalId">承認要求番号</param>
        /// <param name="approvalType">承認種別</param>
        /// <param name="empId">社員ID</param>
        /// <returns></returns>
        public async Task<List<SalesData>> CreateSalesReturnAsync(string approvalId, string approvalType, int empId)
        {

            // 売上返品データ作成
            Dictionary<string, SalesData> dataMap = [];

            var salesReturnData = await _hatFContext.ReturningProducts.Where(x => x.RefundApprovalId == approvalId)
                                                                        .Join(_hatFContext.ReturningProductsDetails,
                                                                            h => h.ReturningProductsId,
                                                                            d => d.ReturningProductsId,
                                                                            (h, d) => new
                                                                            {
                                                                                ApprovalId = h.ApprovalId,
                                                                                SalesNo = d.SalesNo,
                                                                                SalesRowNo = d.SalesRowNo,
                                                                                ReturnQuantity = d.ReturnQuantity,
                                                                                RefundUnitPrice = d.RefundUnitPrice,
                                                                                SalesCd = d.SalesCd
                                                                            })
                                                                        .Select(x => new
                                                                        {
                                                                            ApprovalId = x.ApprovalId,
                                                                            SalesNo = x.SalesNo,
                                                                            SalesRowNo = x.SalesRowNo,
                                                                            ReturnQuantity = x.ReturnQuantity,
                                                                            RefundUnitPrice = x.RefundUnitPrice,
                                                                            SalesCd = x.SalesCd
                                                                        }).ToListAsync();

            foreach (var detail in salesReturnData)
            {

                bool isHeaderNew = true;
                string key = detail.SalesNo;
                Sale sales = new();
                List<SalesDetail> salesDetails = [];
                short rowNo = 1;

                if (dataMap.TryGetValue(key, out SalesData value))
                {
                    isHeaderNew = false;
                    sales = value.Sales;
                    salesDetails = value.SalesDetails;

                    rowNo = salesDetails.Max(x => x.RowNo);
                    rowNo++;
                }

                if (isHeaderNew)
                {

                    // 元売上
                    var orgSales = await _hatFContext.Sales.Where(x => x.SalesNo == detail.SalesNo).FirstOrDefaultAsync();

                    var seqNoReturn = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.SalesSalesNo);
                    sales.SalesNo = $"{seqNoReturn:D10}";
                    sales.ConstructionCode = orgSales.ConstructionCode;
                    sales.OrderNo = orgSales.OrderNo;
                    sales.SalesType = (short)SalesType.SalesReturn;
                    sales.DeptCode = orgSales.DeptCode;
                    sales.StartDate = orgSales.StartDate;
                    sales.CompCode = orgSales.CompCode;
                    sales.EmpId = orgSales.EmpId;
                    sales.SalesAmnt = 0;
                    sales.CmpTax = 0;
                    sales.Creator = empId;

                }

                // 明細
                var orgSalesDetail = await _hatFContext.SalesDetails.Where(x => x.SalesNo == detail.SalesNo && x.RowNo == detail.SalesRowNo).FirstOrDefaultAsync();

                var salesDetail = new SalesDetail();
                salesDetail.SalesNo = sales.SalesNo;
                salesDetail.RowNo = rowNo;
                salesDetail.ProdCode = orgSalesDetail.ProdCode;
                salesDetail.ProdName = orgSalesDetail.ProdName;
                salesDetail.Unitprice = (decimal)detail.RefundUnitPrice * -1;
                salesDetail.DeliveredQty = orgSalesDetail.DeliveredQty;
                salesDetail.Quantity = (int)detail.ReturnQuantity * -1;
                salesDetail.Discount = orgSalesDetail.Discount;
                salesDetail.AutoJournalDate = orgSalesDetail.AutoJournalDate;
                salesDetail.DenNo = orgSalesDetail.DenNo;
                salesDetail.DenFlg = orgSalesDetail.DenFlg;
                salesDetail.HatOrderNo = orgSalesDetail.HatOrderNo;
                salesDetail.Chuban = orgSalesDetail.Chuban;
                salesDetail.SalesCd = detail.SalesCd;
                salesDetail.SalesCorrectionType = approvalType;
                salesDetail.OriginalSalesNo = detail.SalesNo;
                salesDetail.OriginalRowNo = detail.SalesRowNo;
                salesDetail.Creator = empId;
                salesDetails.Add(salesDetail);

                if (isHeaderNew)
                {
                    SalesData data = new()
                    {
                        Sales = sales,
                        SalesDetails = salesDetails
                    };
                    dataMap.Add(key, data);
                }

            }

            return CorrectSales([.. dataMap.Values]);

        }

        /// <summary>
        /// 売上データの補正
        /// </summary>
        /// <param name="datas"></param>
        public List<SalesData> CorrectSales(List<SalesData> datas)
        {
            foreach (SalesData data in datas)
            {
                var details = data.SalesDetails;
                var salesAmnt = details.Select(x => x.Unitprice * x.Quantity).Sum();
                if (data.Sales.SalesType == (short)SalesType.SalesReturn)
                {
                    salesAmnt = salesAmnt * -1;
                }
                data.Sales.SalesAmnt = salesAmnt;   // 売上金額合計

                data.Sales.CmpTax = 0;          // TODO 消費税合計
            }
            return datas;
        }

        /// <summary>
        /// 新規に承認情報を登録
        /// </summary>
        /// <param name="approvalData">承認データ</param>
        /// <returns></returns>
        public async Task<int> InsertApprovalAsync(ApprovalData approvalData)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;

            approvalData.Approval.CreateDate = now;
            _hatFContext.Approvals.Add(approvalData.Approval);

            approvalData.ApprovalProcedure.ApprovalDate = now;
            approvalData.ApprovalProcedure.CreateDate = now;
            _hatFContext.ApprovalProcedures.Add(approvalData.ApprovalProcedure);

            if (approvalData.ApprovalTargets?.Count > 0)
            {
                foreach (var targetData in approvalData.ApprovalTargets)
                {
                    targetData.CreateDate = now;
                    _hatFContext.ApprovalTargets.Add(targetData);

                    if (!string.IsNullOrEmpty(targetData.PuNo) && targetData.PuRowNo.HasValue)
                    {
                        var puDetail = await _hatFContext.PuDetails.FirstOrDefaultAsync(x => x.PuNo == targetData.PuNo && x.PuRowNo == targetData.PuRowNo);
                        puDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                        puDetail.Updater = targetData.Creator;
                        puDetail.UpdateDate = now;
                        _hatFContext.PuDetails.Update(puDetail);
                    }

                    if (!string.IsNullOrEmpty(targetData.SalesNo) && targetData.RowNo.HasValue)
                    {
                        var salesDetail = await _hatFContext.SalesDetails.FirstOrDefaultAsync(x => x.SalesNo == targetData.SalesNo && x.RowNo == targetData.RowNo);
                        salesDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                        salesDetail.Updater = targetData.Creator;
                        salesDetail.UpdateDate = now;
                        _hatFContext.SalesDetails.Update(salesDetail);
                    }

                }
            }

            if (approvalData.ReturnData != null)
            {
                approvalData.ReturnData.Return.ReturnDate = now;
                approvalData.ReturnData.Return.CreateDate = now;
                _hatFContext.ReturningProducts.Add(approvalData.ReturnData.Return);

                foreach (var returnDetail in approvalData.ReturnData.ReturnDetails)
                {
                    returnDetail.CreateDate = now;
                    _hatFContext.ReturningProductsDetails.Add(returnDetail);
                }
            }

            return await _hatFContext.SaveChangesAsync();
        }

        /// <summary>
        /// 新規に承認情報を登録
        /// </summary>
        /// <param name="approvalData">承認データ</param>
        /// <returns></returns>
        public async Task<int> InsertReturnApprovalAsync(ApprovalData approvalData)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;

            approvalData.Approval.CreateDate = now;
            _hatFContext.Approvals.Add(approvalData.Approval);

            approvalData.ApprovalProcedure.ApprovalDate = now;
            approvalData.ApprovalProcedure.CreateDate = now;
            _hatFContext.ApprovalProcedures.Add(approvalData.ApprovalProcedure);

            if (approvalData.ReturnData != null)
            {
                approvalData.ReturnData.Return.UpdateDate = now;
                _hatFContext.ReturningProducts.Update(approvalData.ReturnData.Return);

                foreach (var returnDetail in approvalData.ReturnData.ReturnDetails)
                {
                    returnDetail.UpdateDate = now;
                    _hatFContext.ReturningProductsDetails.Update(returnDetail);
                }
            }

            return await _hatFContext.SaveChangesAsync();
        }

        /// <summary>
        /// 承認情報を更新
        /// </summary>
        /// <param name="approvalData">承認データ</param>
        /// <returns></returns>
        public async Task<int> UpdateApprovalAsync(ApprovalData approvalData)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;

            approvalData.Approval.UpdateDate = now;
            _hatFContext.Approvals.Update(approvalData.Approval);

            approvalData.ApprovalProcedure.ApprovalDate = now;
            approvalData.ApprovalProcedure.CreateDate = now;
            _hatFContext.ApprovalProcedures.Add(approvalData.ApprovalProcedure);

            if (approvalData.ApprovalTargets?.Count > 0)
            {
                foreach (var targetData in approvalData.ApprovalTargets)
                {
                    if (_hatFContext.ApprovalTargets.Any(x => x.ApprovalTargetId == targetData.ApprovalTargetId && x.ApprovalTargetSub == targetData.ApprovalTargetSub))
                    {
                        targetData.UpdateDate = now;
                        _hatFContext.ApprovalTargets.Update(targetData);

                        if (!string.IsNullOrEmpty(targetData.PuNo) && targetData.PuRowNo.HasValue)
                        {
                            var puDetail = await _hatFContext.PuDetails.FirstOrDefaultAsync(x => x.PuNo == targetData.PuNo && x.PuRowNo == targetData.PuRowNo);

                            if (string.IsNullOrEmpty(puDetail.ApprovalTargetId))
                            {
                                puDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                                puDetail.Updater = targetData.Creator;
                                puDetail.UpdateDate = now;
                                _hatFContext.PuDetails.Update(puDetail);
                            }
                        }

                        if (!string.IsNullOrEmpty(targetData.SalesNo) && targetData.RowNo.HasValue)
                        {
                            var salesDetail = await _hatFContext.SalesDetails.FirstOrDefaultAsync(x => x.SalesNo == targetData.SalesNo && x.RowNo == targetData.RowNo);

                            if (string.IsNullOrEmpty(salesDetail.ApprovalTargetId))
                            {
                                salesDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                                salesDetail.Updater = targetData.Creator;
                                salesDetail.UpdateDate = now;
                                _hatFContext.SalesDetails.Update(salesDetail);
                            }
                        }

                    }
                    else
                    {
                        targetData.CreateDate = now;
                        _hatFContext.ApprovalTargets.Add(targetData);

                        if (!string.IsNullOrEmpty(targetData.PuNo) && targetData.PuRowNo.HasValue)
                        {
                            var puDetail = await _hatFContext.PuDetails.FirstOrDefaultAsync(x => x.PuNo == targetData.PuNo && x.PuRowNo == targetData.PuRowNo);
                            puDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                            puDetail.Updater = targetData.Creator;
                            puDetail.UpdateDate = now;
                            _hatFContext.PuDetails.Update(puDetail);
                        }

                        if (!string.IsNullOrEmpty(targetData.SalesNo) && targetData.RowNo.HasValue)
                        {
                            var salesDetail = await _hatFContext.SalesDetails.FirstOrDefaultAsync(x => x.SalesNo == targetData.SalesNo && x.RowNo == targetData.RowNo);
                            salesDetail.ApprovalTargetId = targetData.ApprovalTargetId;
                            salesDetail.Updater = targetData.Creator;
                            salesDetail.UpdateDate = now;
                            _hatFContext.SalesDetails.Update(salesDetail);
                        }
                    }
                }
            }

            if (approvalData.ReturnData != null)
            {
                foreach (var detail in approvalData.ReturnData.ReturnDetails)
                {
                    if (_hatFContext.ReturningProductsDetails.Any(x => x.ReturningProductsId == detail.ReturningProductsId && x.RowNo == detail.RowNo))
                    {
                        detail.UpdateDate = now;
                        _hatFContext.ReturningProductsDetails.Update(detail);
                    }
                    else
                    {
                        detail.CreateDate = now;
                        _hatFContext.ReturningProductsDetails.Add(detail);
                    }
                }
            }

            return await _hatFContext.SaveChangesAsync();

        }

        /// <summary>
        /// 最終承認情報を更新
        /// </summary>
        /// <param name="approvalData">承認データ</param>
        /// <returns></returns>
        public async Task<int> FinalApprovalAsync(FinalApprovalData approvalData)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;

            approvalData.Approval.UpdateDate = now;
            _hatFContext.Approvals.Update(approvalData.Approval);

            approvalData.ApprovalProcedure.ApprovalDate = now;
            approvalData.ApprovalProcedure.CreateDate = now;
            _hatFContext.ApprovalProcedures.Add(approvalData.ApprovalProcedure);

            // 仕入訂正
            if (approvalData.PurchaseData?.Count > 0)
            {
                foreach (PurchaseData data in approvalData.PurchaseData)
                {
                    data.Pu.CreateDate = now;
                    _hatFContext.Pus.Add(data.Pu);

                    foreach (var detail in data.PuDetails)
                    {
                        detail.CreateDate = now;
                        _hatFContext.PuDetails.Add(detail);
                    }
                }
            }

            // 売上訂正
            if (approvalData.SalesData?.Count > 0)
            {
                foreach (SalesData data in approvalData.SalesData)
                {
                    data.Sales.SalesDate = now.Date;
                    data.Sales.CreateDate = now;
                    _hatFContext.Sales.Add(data.Sales);

                    foreach (var detail in data.SalesDetails)
                    {
                        detail.CreateDate = now;
                        _hatFContext.SalesDetails.Add(detail);
                    }
                }
            }

            return await _hatFContext.SaveChangesAsync();

        }
    }
}
