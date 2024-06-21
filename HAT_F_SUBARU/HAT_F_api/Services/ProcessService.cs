using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
using System.Linq;

namespace HAT_F_api.Services
{
    // TODO 仮のサービス名
    public class ProcessService
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _hatFContext;

        /// <summary>起動時情報コンテキスト</summary>
        private readonly HatFApiExecutionContext _executionContext;

        /// <summary>連番サービス</summary>
        private readonly SequenceNumberService _sequenceNumberService;

        /// <summary>作成者/更新者などを設定するクラス</summary>
        private readonly UpdateInfoSetter _updateInfoSetter;

        /// <summary>ログイン情報</summary>
        private readonly HatFLoginResultAccesser _hatFLoginResultAccesser;

        /// <summary>コンストラクタ</summary>
        /// <param name="hatFContext">DBコンテキスト</param>
        /// <param name="hatFApiExecutionContext">起動時情報コンテキスト</param>
        /// <param name="sequenceNumberService">連番サービス</param>
        /// <param name="updateInfoSetter">作成者/更新者などを設定するクラス</param>
        /// <param name="hatFLoginResultAccesser">ログイン情報</param>
        public ProcessService(
            HatFContext hatFContext, 
            HatFApiExecutionContext hatFApiExecutionContext, 
            SequenceNumberService sequenceNumberService, 
            UpdateInfoSetter updateInfoSetter,
            HatFLoginResultAccesser hatFLoginResultAccesser)
        {
            _hatFContext = hatFContext;
            _executionContext = hatFApiExecutionContext;
            _sequenceNumberService = sequenceNumberService;
            _updateInfoSetter = updateInfoSetter;
            _hatFLoginResultAccesser = hatFLoginResultAccesser;
        }

        /// <summary>物件詳細情報用のクエリを取得</summary>
        /// <param name="constructionCode">物件コード</param>
        /// <returns>クエリ</returns>
        public IQueryable<ViewConstructionDetail> GetViewConstructionDetailQuery(string constructionCode)
        {
            return _hatFContext.ViewConstructionDetails
                .Where(x => string.IsNullOrEmpty(constructionCode) || x.物件コード == constructionCode);
        }

        /// <summary>売上確定前利率異常一覧のクエリを取得</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="uriTanZero">基本検索条件[単価がゼロ円]</param>
        /// <param name="searchItems">検索条件</param>
        /// <returns>利率異常一覧</returns>
        public IQueryable<ViewInterestRateBeforeFix> GetViewInterestRateBeforeFixesQuery(
                decimal? profitOver, decimal? profitUnder,
                int? suryoOver, int? suryoUnder,
                decimal? uriKinOver, decimal? uriKinUnder,
                bool uriTanZero,
                List<GenSearchItem> searchItems)
        {
            var query = GenSearchUtil.DoGenSearch(_hatFContext.ViewInterestRateBeforeFixes, searchItems)
                .Where(x =>
                    // 利率がX%以上または利率がX%以下
                    ((profitOver.HasValue && profitUnder.HasValue) && (profitOver <= x.利率 || x.利率 <= profitUnder)) ||
                    // X%以上だけが設定されている場合
                    ((profitOver.HasValue && !profitUnder.HasValue) && (profitOver <= x.利率)) ||
                    // X%以下だけが設定されている場合
                    ((!profitOver.HasValue && profitUnder.HasValue) && (x.利率 <= profitUnder)) || 
                    // 数量がX個以上または数量がX個以下
                    ((suryoOver.HasValue && suryoUnder.HasValue) && (suryoOver <= x.数量) || (x.数量 <= suryoUnder)) ||
                    // X個以上だけが設定されている場合
                    ((suryoOver.HasValue && !suryoUnder.HasValue) && (suryoOver <= x.数量)) || 
                    // X個以下だけが設定されている場合
                    ((!suryoOver.HasValue && suryoUnder.HasValue) && (x.数量 <= suryoUnder)) ||  
                    // 金額がX円以上または金額がX円以下
                    ((uriKinOver.HasValue && uriKinUnder.HasValue) && (uriKinOver <= x.売上額 || x.売上額 <= uriKinUnder)) ||
                   // X円以上だけが設定されている場合
                    ((uriKinOver.HasValue && !uriKinUnder.HasValue) && (uriKinOver <= x.売上額)) ||
                    // X円以下だけが設定されている場合
                    ((!uriKinOver.HasValue && uriKinUnder.HasValue) && (x.売上額 <= uriKinUnder)) || 
                    // 単価がゼロ
                    (uriTanZero && x.売上単価 == 0)
                );
            return query;
        }

        /// <summary>売上確定後利率異常一覧のクエリを取得</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="uriTanZero">基本検索条件[単価がゼロ円]</param>
        /// <param name="searchItems">検索条件</param>
        /// <returns>利率異常一覧</returns>
        public IQueryable<ViewInterestRateFixed> GetViewInterestRateFixedQuery(
                decimal? profitOver, decimal? profitUnder,
                int? suryoOver, int? suryoUnder,
                decimal? uriKinOver, decimal? uriKinUnder,
                bool uriTanZero,
                List<GenSearchItem> searchItems)
        {
            var query = GenSearchUtil.DoGenSearch(_hatFContext.ViewInterestRateFixeds, searchItems)
                .Where(x =>
                    // 利率がX%以上または利率がX%以下
                    ((profitOver.HasValue && profitUnder.HasValue) && (profitOver <= x.利率 || x.利率 <= profitUnder)) ||
                    // X%以上だけが設定されている場合
                    ((profitOver.HasValue && !profitUnder.HasValue) && (profitOver <= x.利率)) ||
                    // X%以下だけが設定されている場合
                    ((!profitOver.HasValue && profitUnder.HasValue) && (x.利率 <= profitUnder)) ||
                    // 数量がX個以上または数量がX個以下
                    ((suryoOver.HasValue && suryoUnder.HasValue) && (suryoOver <= x.数量) || (x.数量 <= suryoUnder)) ||
                    // X個以上だけが設定されている場合
                    ((suryoOver.HasValue && !suryoUnder.HasValue) && (suryoOver <= x.数量)) ||
                    // X個以下だけが設定されている場合
                    ((!suryoOver.HasValue && suryoUnder.HasValue) && (x.数量 <= suryoUnder)) ||
                    // 金額がX円以上または金額がX円以下
                    ((uriKinOver.HasValue && uriKinUnder.HasValue) && (uriKinOver <= x.販売額 || x.販売額 <= uriKinUnder)) ||
                    // X円以上だけが設定されている場合
                    ((uriKinOver.HasValue && !uriKinUnder.HasValue) && (uriKinOver <= x.販売額)) ||
                    // X円以下だけが設定されている場合
                    ((!uriKinOver.HasValue && uriKinUnder.HasValue) && (x.販売額 <= uriKinUnder)) ||
                    // 単価がゼロ
                    (uriTanZero && x.販売単価 == 0)
                );
            return query;
        }

        /// <summary>売上確定前利率異常チェック結果を取得</summary>
        /// <param name="saveKey">SAVE_KEY</param>
        /// <param name="denSort">DEN_SORT</param>
        /// <param name="denNoLine">DEN_NO_LINE</param>
        /// <returns>利率異常チェック結果</returns>
        private Task<InterestRateCheckBeforeFix> GetInterestRateCheckBeforeFixAsync(string saveKey, string denSort, string denNoLine)
        {
            return _hatFContext.InterestRateCheckBeforeFixes
                .Where(x => string.IsNullOrEmpty(saveKey) || x.SaveKey == saveKey)
                .Where(x => string.IsNullOrEmpty(denSort) || x.DenSort == denSort)
                .Where(x => string.IsNullOrEmpty(denNoLine) || x.DenNoLine == denNoLine)
                .FirstOrDefaultAsync();
        }

        /// <summary>売上確定後利率異常チェック結果を取得</summary>
        /// <param name="salesNo">売上番号</param>
        /// <param name="rowNo">売上行番号</param>
        /// <returns>利率異常チェック結果</returns>
        private Task<InterestRateCheckFixed> GetInterestRateCheckFixedAsync(string salesNo, short rowNo)
        {
            return _hatFContext.InterestRateCheckFixeds
                .Where(x => string.IsNullOrEmpty(salesNo) || x.SalesNo == salesNo)
                .Where(x => x.RowNo == rowNo)
                .FirstOrDefaultAsync();
        }

        /// <summary>売上確定前利率異常チェック結果を記録</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>INSERTしたレコード数</returns>
        public async Task<List<InterestRateCheckBeforeFixResult>> PutInterestRateCheckBeforeFixAsync(IEnumerable<InterestRateCheckBeforeFixParameter> parameters)
        {
            // TODO ログインIDで比較する
            var checker = await _hatFContext.Employees
                .FirstOrDefaultAsync(x => x.EmpId == 0);
            var result = new List<InterestRateCheckBeforeFixResult>();
            foreach (var p in parameters)
            {
                var target = (await GetInterestRateCheckBeforeFixAsync(p.SaveKey, p.DenSort, p.DenNoLine));
                if (target is null)
                {
                    var newEntity = new InterestRateCheckBeforeFix()
                    {
                        CheckDatetime = _executionContext.ExecuteDateTimeJst,
                        CheckerId = 0,      // TODO ログインID
                        CheckerPost = "★課長",    // TODO 役職
                        SaveKey = p.SaveKey,
                        DenSort = p.DenSort,
                        DenNoLine = p.DenNoLine,
                        Comment = p.Comment,
                        CreateDate = _executionContext.ExecuteDateTimeJst,
                        Creator = 0,        // TODO ログインID
                    };
                    await _hatFContext.InterestRateCheckBeforeFixes.AddAsync(newEntity);
                }
                else
                {
                    target.CheckDatetime = _executionContext.ExecuteDateTimeJst;
                    target.CheckerId = 0;   // TODO ログインID
                    target.CheckerPost = "★課長";    // TODO 役職
                    target.Comment = p.Comment;
                    target.UpdateDate = _executionContext.ExecuteDateTimeJst;
                    target.Updater = 0;     // TODO ログインID
                }
                result.Add(new InterestRateCheckBeforeFixResult()
                {
                    SaveKey = p.SaveKey,
                    DenSort = p.DenSort,
                    DenNoLine = p.DenNoLine,
                    Comment = p.Comment,
                    Checker = checker?.EmpName,
                    CheckerPost = "★課長" // TODO 役職
                });
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>売上確定後利率異常チェック結果を記録</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>INSERTしたレコード数</returns>
        public async Task<List<InterestRateCheckFixedResult>> PutInterestRateCheckFixedAsync(IEnumerable<InterestRateCheckFixedParameter> parameters)
        {
            // TODO ログインIDで比較する
            var checker = await _hatFContext.Employees
                .FirstOrDefaultAsync(x => x.EmpId == 0);
            var result = new List<InterestRateCheckFixedResult>();
            foreach (var p in parameters)
            {
                var target = (await GetInterestRateCheckFixedAsync(p.SalesNo, p.RowNo));
                if (target is null)
                {
                    var newEntity = new InterestRateCheckFixed()
                    {
                        CheckDatetime = _executionContext.ExecuteDateTimeJst,
                        CheckerId = 0,      // TODO ログインID
                        CheckerPost = "★課長",    // TODO 役職
                        SalesNo = p.SalesNo,
                        RowNo = p.RowNo,
                        Comment = p.Comment,
                        CreateDate = _executionContext.ExecuteDateTimeJst,
                        Creator = 0,        // TODO ログインID
                    };
                    await _hatFContext.InterestRateCheckFixeds.AddAsync(newEntity);
                }
                else
                {
                    target.CheckDatetime = _executionContext.ExecuteDateTimeJst;
                    target.CheckerId = 0;   // TODO ログインID
                    target.CheckerPost = "★課長";    // TODO 役職
                    target.Comment = p.Comment;
                    target.UpdateDate = _executionContext.ExecuteDateTimeJst;
                    target.Updater = 0;     // TODO ログインID
                }
                result.Add(new InterestRateCheckFixedResult()
                {
                    SalesNo = p.SalesNo,
                    RowNo = p.RowNo,
                    Comment = p.Comment,
                    Checker = checker?.EmpName,
                    CheckerPost = "★課長" // TODO 役職
                });
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>納品一覧表（社内用）チェック結果を記録</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>INSERTしたレコード数</returns>
        public async Task<List<InternalDeliveryCheckResult>> PutInternalDeliveryCheckAsync(IEnumerable<InternalDeliveryCheckParameter> parameters)
        {
            var employee = await _hatFContext.Employees
                .SingleOrDefaultAsync(x => x.EmpId == _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
            var result = new List<InternalDeliveryCheckResult>();
            foreach (var p in parameters)
            {
                var target = await _hatFContext.InternalDeliveryChecks
                    .Where(x => x.SalesNo == p.SalesNo)
                    .Where(x => x.RowNo == p.RowNo)
                    .FirstOrDefaultAsync();
                if (target is null)
                {
                    var newEntity = new InternalDeliveryCheck()
                    {
                        CheckDatetime = _executionContext.ExecuteDateTimeJst,
                        CheckerId = employee.EmpId,
                        CheckerPost = employee.OccuCode,
                        SalesNo = p.SalesNo,
                        RowNo = p.RowNo,
                        Comment = p.Comment,
                    };
                    _updateInfoSetter.SetUpdateInfo(newEntity);
                    await _hatFContext.InternalDeliveryChecks.AddAsync(newEntity);
                }
                else
                {
                    target.CheckDatetime = _executionContext.ExecuteDateTimeJst;
                    target.CheckerId = employee.EmpId;
                    target.CheckerPost = employee.OccuCode;
                    target.Comment = p.Comment;
                    _updateInfoSetter.SetUpdateInfo(target);
                }
                result.Add(new InternalDeliveryCheckResult()
                {
                    SalesNo = p.SalesNo,
                    RowNo = p.RowNo,
                    Comment = p.Comment,
                    Checker = employee.EmpName,
                    CheckerPost = employee.OccuCode,
                });
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        public class SalesData
        {
            public Sale Sales { get; set; }
            public List<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
            public List<ViewReadySalesBatch> View { get; set; } = new List<ViewReadySalesBatch>();
        }

        /// <summary>
        /// 売上予定VIEW(バッチ用)から売上データ一覧を作成
        /// </summary>
        /// <param name="viewReadySalesBatch">売上予定VIEW</param>
        /// <param name="orderNo">HAT注文番号 or 受注番号</param>
        /// <returns></returns>
        public async Task<List<SalesData>> ToSalesDatasAsync(List<ViewReadySalesBatch> viewReadySalesBatch, string orderNo = null)
        {
            var now = _executionContext.ExecuteDateTimeJst;
            Dictionary<string, SalesData> dataMap = [];
            short rowNo = 1;

            foreach (var d in viewReadySalesBatch)
            {
                bool isHeaderNew = true;
                Sale sales = new();
                List<SalesDetail> salesDetails = [];
                List<ViewReadySalesBatch> viewList = [];
                string key = string.IsNullOrEmpty(orderNo) ? d.受注番号 : orderNo;

                if (dataMap.TryGetValue(key, out SalesData value))
                {
                    isHeaderNew = false;
                    sales = value.Sales;
                    salesDetails = value.SalesDetails;
                    viewList = value.View;

                    rowNo = salesDetails.Max(x => x.RowNo);
                    rowNo++;
                }

                // ヘッダ
                if (isHeaderNew)
                {
                    var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.SalesSalesNo);
                    sales.SalesNo = $"{seqNo:D10}";
                    sales.ConstructionCode = d.物件コード;
                    sales.OrderNo = d.受注番号;
                    sales.SalesDate = now;
                    sales.SalesType = 1;
                    sales.DeptCode = d.部門コード;
                    sales.StartDate = DateTime.Parse(d.部門開始日.ToString());
                    sales.CompCode = d.得意先コード;
                    sales.EmpId = int.Parse(d.社員id.ToString());
                    sales.CreateDate = now;
                    sales.Creator = 0; // TODO 作成者名

                    // 売上行番号 初期化
                    rowNo = 1;
                }

                // 明細
                var detail = new SalesDetail();
                detail.SalesNo = sales.SalesNo;
                detail.RowNo = rowNo;
                detail.ProdCode = d.商品コード;
                detail.ProdName = d.商品名;
                detail.Unitprice = decimal.Parse(d.売上単価.ToString());
                detail.Quantity = int.Parse(d.数量.ToString());
                detail.Discount = 0;
                detail.DenNo = d.伝票番号;
                detail.DenFlg = d.伝票区分;
                detail.HatOrderNo = d.Hat注文番号;
                detail.Chuban = d.H注番;
                detail.CreateDate = now;
                detail.Creator = 0; // TODO 作成者名

                salesDetails.Add(detail);

                // 受注明細テーブルの更新対象値を設定
                var view = new ViewReadySalesBatch();
                view.明細SaveKey = d.明細SaveKey;
                view.明細DenSort = d.明細DenSort;
                view.明細DenNoLine = d.明細DenNoLine;

                viewList.Add(view);

                if (isHeaderNew)
                {
                    SalesData data = new()
                    {
                        Sales = sales,
                        SalesDetails = salesDetails,
                        View = viewList
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
                data.Sales.SalesAmnt = details.Select(e => e.Unitprice * e.Quantity).Sum();     // 売上金額合計
                data.Sales.CmpTax = 0;          // TODO 消費税合計
            }
            return datas;
        }

        /// <summary>
        /// 売上データの登録
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public async Task<int> PutSalesDatasAsync(List<SalesData> datas)
        {
            var now = _executionContext.ExecuteDateTimeJst;

            foreach (SalesData data in datas)
            {

                _hatFContext.Sales.Add(data.Sales);

                foreach (var detail in data.SalesDetails)
                {
                    _hatFContext.SalesDetails.Add(detail);
                }

                foreach (var view in data.View)
                {
                    var fosJyuchuD = await _hatFContext.FosJyuchuDs.FirstOrDefaultAsync(x => x.SaveKey == view.明細SaveKey && x.DenSort == view.明細DenSort && x.DenNoLine == view.明細DenNoLine);
                    fosJyuchuD.GCompleteFlg = 1;
                    fosJyuchuD.Updater = 0; // TODO 作成者名
                    fosJyuchuD.UpdateDate = now;
                    _hatFContext.FosJyuchuDs.Update(fosJyuchuD);
                }
            }

            return await _hatFContext.SaveChangesAsync();
        }

        public class InvoiceData
        {
            public Invoice Invoice { get; set; }
            public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        }

        /// <summary>
        /// 請求締めVIEW(バッチ用)から都度請求データ一覧を作成
        /// </summary>
        /// <param name="viewInvoiceBatch">請求締めVIEW</param>
        /// <param name="sysdate">実行日(yyyyMMdd)</param>
        /// <returns></returns>
        public async Task<List<InvoiceData>> ToInvoiceDatasUnitAsync(List<ViewInvoiceBatch> viewInvoiceBatch, DateTime sysdate)
        {
            var now = _executionContext.ExecuteDateTimeJst;
            Dictionary<string, InvoiceData> dataMap = [];
            string key = string.Empty;

            foreach (var d in viewInvoiceBatch)
            {
                bool isHeaderNew = true;
                key = d.売上番号;
                Invoice invoice = new();
                List<InvoiceDetail> invoiceDetails = [];

                if (dataMap.TryGetValue(key, out InvoiceData value))
                {
                    isHeaderNew = false;
                    invoice = value.Invoice;
                    invoiceDetails = value.InvoiceDetails;
                }

                // ヘッダ
                if (isHeaderNew)
                {
                    var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.InvoiceInvoiceNo);
                    invoice.InvoiceNo = $"{seqNo:D10}";
                    invoice.InvoicedDate = sysdate;
                    invoice.CompCode = d.顧客コード;
                    invoice.CustSubNo = d.顧客枝番;
                    invoice.InvoiceState = 0;   // 0:未請求
                    invoice.LastReceived = string.IsNullOrEmpty(d.前回入金額.ToString()) ? 0 : d.前回入金額;  // 前回入金額
                    var salesAmt = viewInvoiceBatch.Where(x => x.売上番号 == d.売上番号 && x.売上区分 == 1)
                                                    .Sum(x => x.売上単価 * x.売上数量);
                    var returnAmt = viewInvoiceBatch.Where(x => x.売上番号 == d.売上番号 && x.売上区分 == 2)
                                                    .Sum(x => (x.売上単価 * x.売上数量) * -1);
                    invoice.MonthSales = salesAmt + returnAmt;  // 当月売上額
                    invoice.MonthReceived = 0;      // 当月入金額
                    invoice.MonthInvoice = 0;       // 当月請求額
                    invoice.CmpTax = 0;             // 消費税金額
                    invoice.InvoiceReceived = 0;    // 請求消込金額
                    invoice.PreviousInvoice = string.IsNullOrEmpty(d.前回請求残高.ToString()) ? 0 : d.前回請求残高; // 前回請求残高
                    invoice.CurrentBalance = 0;     // 当月残高
                    invoice.CreateDate = now;
                    invoice.Creator = 0; // TODO 作成者名

                }

                // 明細
                var detail = new InvoiceDetail();
                detail.InvoiceNo = invoice.InvoiceNo;
                detail.SalesNo = d.売上番号;
                detail.RowNo = d.売上行番号;
                detail.CreateDate = now;
                detail.Creator = 0; // TODO 作成者名

                invoiceDetails.Add(detail);

                if (isHeaderNew)
                {
                    InvoiceData data = new()
                    {
                        Invoice = invoice,
                        InvoiceDetails = invoiceDetails
                    };
                    dataMap.Add(key, data);
                }
            }

            return [.. dataMap.Values];
        }

        /// <summary>
        /// 請求締めVIEW(バッチ用)から締日請求データ一覧を作成
        /// </summary>
        /// <param name="viewInvoiceBatch">請求締めVIEW</param>
        /// <param name="sysdate">実行日(yyyyMMdd)</param>
        /// <returns></returns>
        public async Task<List<InvoiceData>> ToInvoiceDatasSummaryAsync(List<ViewInvoiceBatch> viewInvoiceBatch, DateTime sysdate)
        {
            var now = _executionContext.ExecuteDateTimeJst;
            Dictionary<string, InvoiceData> dataMap = [];
            string key = string.Empty;

            foreach (var d in viewInvoiceBatch)
            {
                bool isHeaderNew = true;
                key = d.顧客コード;
                Invoice invoice = new();
                List<InvoiceDetail> invoiceDetails = [];

                if (dataMap.TryGetValue(key, out InvoiceData value))
                {
                    isHeaderNew = false;
                    invoice = value.Invoice;
                    invoiceDetails = value.InvoiceDetails;
                }

                // ヘッダ
                if (isHeaderNew)
                {
                    var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.InvoiceInvoiceNo);
                    invoice.InvoiceNo = $"{seqNo:D10}";
                    invoice.InvoicedDate = sysdate;
                    invoice.CompCode = d.顧客コード;
                    invoice.CustSubNo = d.顧客枝番;
                    invoice.InvoiceState = 0;   // 0:未請求
                    invoice.LastReceived = string.IsNullOrEmpty(d.前回入金額.ToString()) ? 0 : d.前回入金額;  // 前回入金額
                    var salesAmt = viewInvoiceBatch.Where(x => x.顧客コード == d.顧客コード && x.売上区分 == 1)
                                                    .Sum(x => x.売上単価 * x.売上数量);
                    var returnAmt = viewInvoiceBatch.Where(x => x.顧客コード == d.顧客コード && x.売上区分 == 2)
                                                    .Sum(x => (x.売上単価 * x.売上数量) * -1);
                    invoice.MonthSales = salesAmt + returnAmt;  // 当月売上額
                    invoice.MonthReceived = 0;      // 当月入金額
                    invoice.MonthInvoice = 0;       // 当月請求額
                    invoice.CmpTax = 0;             // 消費税金額
                    invoice.InvoiceReceived = 0;    // 請求消込金額
                    invoice.PreviousInvoice = string.IsNullOrEmpty(d.前回請求残高.ToString()) ? 0 : d.前回請求残高; // 前回請求残高
                    invoice.CurrentBalance = 0;     // 当月残高
                    invoice.CreateDate = now;
                    invoice.Creator = 0; // TODO 作成者名

                }

                // 明細
                var detail = new InvoiceDetail();
                detail.InvoiceNo = invoice.InvoiceNo;
                detail.SalesNo = d.売上番号;
                detail.RowNo = d.売上行番号;
                detail.CreateDate = now;
                detail.Creator = 0; // TODO 作成者名

                invoiceDetails.Add(detail);

                if (isHeaderNew)
                {
                    InvoiceData data = new()
                    {
                        Invoice = invoice,
                        InvoiceDetails = invoiceDetails
                    };
                    dataMap.Add(key, data);
                }
            }

            return [.. dataMap.Values];
        }

        /// <summary>
        /// 請求データの登録
        /// </summary>
        /// <param name="datas">請求データ</param>
        /// <returns></returns>
        public async Task<int> PutInvoiceDatasAsync(List<InvoiceData> datas)
        {
            var invoicedDate = new DateTime();

            foreach (InvoiceData data in datas)
            {
                _hatFContext.Invoices.Add(data.Invoice);

                invoicedDate = DateTime.Parse(data.Invoice.InvoicedDate.ToString());

                foreach (var detail in data.InvoiceDetails)
                {
                    _hatFContext.InvoiceDetails.Add(detail);

                    var salesDetails = await _hatFContext.SalesDetails.FirstOrDefaultAsync(x => x.SalesNo == detail.SalesNo && x.RowNo == detail.RowNo);
                    salesDetails.InvoicedDate = invoicedDate;
                    salesDetails.InvoiceNo = detail.InvoiceNo;
                    salesDetails.InvoiceDelayType = 0;
                    salesDetails.Updater = 0; // TODO 作成者名
                    salesDetails.UpdateDate = detail.CreateDate;
                    _hatFContext.SalesDetails.Update(salesDetails);
                }
            }

            return await _hatFContext.SaveChangesAsync();
        }

        /// <summary>請求詳細情報をリストで取得</summary>
        /// <param name="companyCode">得意先コード</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewInvoiceDetail>> GetInvoiceDetailAsync(string companyCode)
        {
            if (companyCode == null || !companyCode.Any())
            {
                return new List<ViewInvoiceDetail>();
            }

            return await _hatFContext.ViewInvoiceDetails
                .Where(x => companyCode.Contains(x.得意先コード))
                .ToListAsync();
        }

        /// <summary>得意先会社情報の取得</summary>
        /// <param name="companyCode">得意先コード</param>
        /// <returns>クエリ</returns>
        public IQueryable<CompanysMst> GetCompanyInfoQuery(string companyCode)
        {
            return _hatFContext.CompanysMsts
                .Where(x => x.CompCode == companyCode);
        }

        /// <summary>顧客の入金口座情報の取得</summary>
        /// <param name="companyCode">顧客の得意先コード</param>
        /// <returns>クエリ</returns>
        public IQueryable<BankAcutMst> GetInvoiceBankQuery(string companyCode)
        {
            return _hatFContext.BankAcutMsts
                .Where(x => x.DeptCode == companyCode);
        }

        /// <summary>仕入売上訂正データの取得</summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewPurchaseSalesCorrection>> GetPurchaseSalesCorrectionAsync(string hatOrderNo)
        {
            return await _hatFContext.ViewPurchaseSalesCorrections
                                        .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.Hat注文番号 == hatOrderNo)
                                        .OrderBy(x => x.伝票番号)
                                        .ThenBy(x => x.伝票区分)
                                        .ThenBy(x => x.仕入先コード)
                                        .ThenBy(x => x.仕入先枝番).ToListAsync();
        }

        /// <summary>返品入力一覧データの取得</summary>
        /// <returns>クエリ</returns>
        public async Task<List<ViewSalesReturn>> GetSalesReturnAsync()
        {

            var query = await _hatFContext.ViewSalesReturns
                                            .GroupBy(x => new { x.Hat注文番号, x.伝票番号, DispId = x.承認ステータス区分 == 6 ? x.返品id : null })
                                            .OrderBy(x => x.Key.Hat注文番号)
                                            .ThenBy(x => x.Key.伝票番号)
                                            .Select(x => x.OrderByDescending(id => id.返品id).First())
                                            .ToListAsync();

            return query;
        }

        /// <summary>返品入力明細データの取得</summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewSalesReturnDetail>> GetSalesReturnDetailAsync(string hatOrderNo, string denNo)
        {

            return await _hatFContext.ViewSalesReturnDetails
                                        .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.Hat注文番号 == hatOrderNo)
                                        .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                                        .OrderBy(x => x.伝票番号)
                                        .ThenBy(x => x.売上番号)
                                        .ThenBy(x => x.売上行番号)
                                        .GroupJoin(_hatFContext.SalesDetails.GroupBy(x => new { x.OriginalSalesNo, x.OriginalRowNo })
                                                                                        .Select(x => new
                                                                                        {
                                                                                            OriginalSalesNo = x.Key.OriginalSalesNo,
                                                                                            OriginalRowNo = x.Key.OriginalRowNo,
                                                                                            Quantity = x.Sum(x => x.Quantity)
                                                                                        }
                                                                                        ),
                                                                                    view => new { salesNo = view.売上番号, rowNo = view.売上行番号 },
                                                                                    sales => new { salesNo = sales.OriginalSalesNo, rowNo = (short)sales.OriginalRowNo },
                                                                                    (view, sales) => new { View = view, Sales = sales }
                                        ).SelectMany(x => x.Sales.DefaultIfEmpty(),
                                        (x, detail) => new ViewSalesReturnDetail()
                                        {
                                            Hat注文番号 = x.View.Hat注文番号,
                                            伝票番号 = x.View.伝票番号,
                                            売上番号 = x.View.売上番号,
                                            売上行番号 = x.View.売上行番号,
                                            商品コード = x.View.商品コード,
                                            商品名 = x.View.商品名,
                                            元数量 = x.View.元数量,
                                            現在数量 = x.View.元数量 + (!string.IsNullOrEmpty(detail.OriginalSalesNo) ? detail.Quantity : 0),
                                            単価 = x.View.単価,
                                            元合計金額 = x.View.元合計金額,
                                            現在合計金額 = (x.View.元数量 + (!string.IsNullOrEmpty(detail.OriginalSalesNo) ? detail.Quantity : 0)) * x.View.単価,
                                            売区 = x.View.売区,
                                            売区名 = x.View.売区名,
                                            返品依頼数量 = x.View.返品後数量,
                                            返品後数量 = x.View.返品後数量,
                                            在庫単価 = x.View.在庫単価,
                                            返品id = x.View.返品id,
                                            返品行番号 = x.View.返品行番号,
                                            承認要求番号 = x.View.承認要求番号,

                                        }).ToListAsync();

        }

        /// <summary>返品入庫一覧データの取得</summary>
        /// <returns>クエリ</returns>
        public async Task<List<ViewSalesReturnReceipt>> GetSalesReturnReceiptAsync()
        {
            return await _hatFContext.ViewSalesReturnReceipts
                                        .Distinct()
                                        .OrderBy(x => x.Hat注文番号)
                                        .ThenBy(x => x.伝票番号).ToListAsync();
        }

        /// <summary>返品入庫明細データの取得</summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewSalesReturnReceiptDetail>> GetSalesReturnReceiptDetailAsync(string hatOrderNo, string denNo)
        {
            return await _hatFContext.ViewSalesReturnReceiptDetails
                                        .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.Hat注文番号 == hatOrderNo)
                                        .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                                        .OrderBy(x => x.伝票番号)
                                        .ThenBy(x => x.返品id)
                                        .ThenBy(x => x.返品行番号).ToListAsync();
        }

        /// <summary>返品入庫入力完了データの取得</summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewSalesRefundDetail>> GetSalesRefundDetailAsync(string hatOrderNo, string denNo)
        {
            return await _hatFContext.ViewSalesRefundDetails
                                        .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.Hat注文番号 == hatOrderNo)
                                        .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                                        .OrderBy(x => x.伝票番号)
                                        .ThenBy(x => x.返品id)
                                        .ThenBy(x => x.返品行番号).ToListAsync();
        }

        /// <summary>納品一覧(訂正・返品)データの取得</summary>
        /// <param name="fromDate">検索日付(FROM)</param>
        /// <param name="toDate">検索日付(TO)</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewCorrectionDelivery>> GetCorrectionDeliveryAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = await _hatFContext.ViewCorrectionDeliveries
                                            .Where(x => !fromDate.HasValue || x.訂正日 >= fromDate.Value.Date)
                                            .Where(x => !toDate.HasValue || x.訂正日 < toDate.Value.Date.AddDays(1))
                                            .GroupBy(x => new { CompCode = x.得意先コード, CompName = x.得意先名 })
                                            .Select(x => new
                                            {
                                                CompCode = x.Key.CompCode,
                                                CompName = x.Key.CompName,
                                                Amount = x.Sum(x => x.売上金額.HasValue ? x.売上金額 : 0)
                                            }).ToListAsync();

            List<ViewCorrectionDelivery> result = new List<ViewCorrectionDelivery>();

            foreach (var item in query)
            {
                var view = new ViewCorrectionDelivery();
                view.得意先コード = item.CompCode;
                view.得意先名 = item.CompName;
                view.売上金額 = item.Amount;
                result.Add(view);
            }

            return result;
        }

        /// <summary>納品明細(訂正・返品)データの取得</summary>
        /// <param name="compCode">得意先コード</param>
        /// <param name="fromDate">検索日付(FROM)</param>
        /// <param name="toDate">検索日付(TO)</param>
        /// <returns>クエリ</returns>
        public async Task<List<ViewCorrectionDeliveryDetail>> GetCorrectionDeliveryDetailAsync(string compCode, DateTime? fromDate, DateTime? toDate)
        {
            return await _hatFContext.ViewCorrectionDeliveryDetails
                                        .Where(x => x.得意先コード == compCode)
                                        .Where(x => !fromDate.HasValue || x.訂正日 >= fromDate.Value.Date)
                                        .Where(x => !toDate.HasValue || x.訂正日 < toDate.Value.Date.AddDays(1))
                                        .OrderBy(x => x.元売上番号)
                                        .ThenBy(x => x.元売上行番号).ToListAsync();

        }

    }
}
