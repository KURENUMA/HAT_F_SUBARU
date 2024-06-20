using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly HatFContext _context;
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;
        private readonly ProcessService _processService;

        /// <summary>コンストラクタ</summary>
        /// <param name="context">DBコンテキスト</param>
        /// <param name="hatFApiExecutionContext">API実行コンテキスト</param>
        /// <param name="processService">プロセスサービス</param>
        public BatchController(HatFContext context,
                                HatFApiExecutionContext hatFApiExecutionContext,
                                ProcessService processService)
        {
            _context = context;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _processService = processService;
        }

        /// <summary>
        /// 売上情報の登録(Hat注文番号)
        /// </summary>
        /// <param name="hatOrderNo">Hat注文番号</param>
        /// <returns>登録結果</returns>
        [HttpPut("sales-details-hatorder")]
        public async Task<ActionResult<ApiResponse<int>>> PutSalesDetailByHatOrderAsync(string hatOrderNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 実行日を取得
                var sysdate = _hatFApiExecutionContext.ExecuteDateTimeJst;

                // 売上予定VIEWを取得
                List<ViewReadySalesBatch> insertRequest = await _context.ViewReadySalesBatches
                                                                .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.Hat注文番号 == hatOrderNo)
                                                                .Where(x => x.納期 < sysdate)
                                                                .OrderBy(x => x.明細SaveKey)
                                                                .ThenBy(x => x.明細DenSort)
                                                                .ThenBy(x => x.明細DenNoLine).ToListAsync();

                // 売上データ一覧を作成
                var datas = await _processService.ToSalesDatasAsync(insertRequest, hatOrderNo);


                // 売上データを登録
                return await _processService.PutSalesDatasAsync(datas);
            });
        }

        /// <summary>
        /// 売上情報の登録(受注番号)
        /// </summary>
        /// <param name="orderNo">受注番号</param>
        /// <returns>登録結果</returns>
        [HttpPut("sales-details-order")]
        public async Task<ActionResult<ApiResponse<int>>> PutSalesDetailByOrderAsync(string orderNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 実行日を取得
                var sysdate = _hatFApiExecutionContext.ExecuteDateTimeJst;

                // 売上予定VIEWを取得
                List<ViewReadySalesBatch> insertRequest = await _context.ViewReadySalesBatches
                                                                .Where(x => string.IsNullOrEmpty(orderNo) || x.受注番号 == orderNo)
                                                                .Where(x => x.納期 < sysdate)
                                                                .OrderBy(x => x.明細SaveKey)
                                                                .ThenBy(x => x.明細DenSort)
                                                                .ThenBy(x => x.明細DenNoLine).ToListAsync();

                // 売上データ一覧を作成
                var datas = await _processService.ToSalesDatasAsync(insertRequest, orderNo);


                // 売上データを登録
                return await _processService.PutSalesDatasAsync(datas);
            });
        }

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <returns>登録結果</returns>
        [HttpPut("sales-details")]
        public async Task<ActionResult<ApiResponse<int>>> PutSalesDetailAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 実行日を取得
                var sysdate = _hatFApiExecutionContext.ExecuteDateTimeJst;

                // 売上予定VIEWを取得
                List<ViewReadySalesBatch> insertRequest = await _context.ViewReadySalesBatches
                                                                .Where(x => x.納期 < sysdate)
                                                                .OrderBy(x => x.明細SaveKey)
                                                                .ThenBy(x => x.明細DenSort)
                                                                .ThenBy(x => x.明細DenNoLine).ToListAsync();

                // 売上データ一覧を作成
                var datas = await _processService.ToSalesDatasAsync(insertRequest);


                // 売上データを登録
                return await _processService.PutSalesDatasAsync(datas);
            });
        }

        /// <summary>
        /// 請求情報の登録
        /// </summary>
        /// <returns>登録結果</returns>
        [HttpPut("invoice-details")]
        public async Task<ActionResult<ApiResponse<int>>> PutInvoiceDetailAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 請求締めVIEWを取得
                List<ViewInvoiceBatch> insertRequest = await _context.ViewInvoiceBatches
                                                            .OrderBy(x => x.顧客請求区分)
                                                            .ThenBy(x => x.顧客コード)
                                                            .ThenBy(x => x.売上番号)
                                                            .ThenBy(x => x.売上行番号).ToListAsync();

                // 実行日を取得
                var sysdate = _hatFApiExecutionContext.ExecuteDateTimeJst.Date;
                // 実行日の日付のみを取得
                var date = sysdate.Day;

                // 請求データ格納用
                var invoiceDatas = new List<ProcessService.InvoiceData>();

                // 都度請求の請求データを作成(CUST_AR_FLAG = 1 )
                var unitRequest = insertRequest.Where(x => x.顧客請求区分 == 1 && x.売上日 < sysdate).ToList();
                if (unitRequest.Any())
                {
                    var unitDatas = await _processService.ToInvoiceDatasUnitAsync(unitRequest, sysdate);
                    invoiceDatas.AddRange(unitDatas);
                }

                // 締請求の請求データを作成(CUST_AR_FLAG = 2 )
                var summaryRequest = insertRequest.Where(x => x.顧客請求区分 == 2 && x.売上日 < sysdate).ToList();
                if (summaryRequest.Any())
                {
                    // 実行日が1日の場合
                    if (date == 1)
                    {
                        summaryRequest = summaryRequest.Where(x => x.顧客締日 == date - 1 || x.顧客締日 == 99).ToList();
                    }
                    else
                    {
                        summaryRequest = summaryRequest.Where(x => x.顧客締日 == date - 1).ToList();
                    }
                }

                if (summaryRequest.Any())
                {
                    var summaryDatas = await _processService.ToInvoiceDatasSummaryAsync(summaryRequest, sysdate);
                    invoiceDatas.AddRange(summaryDatas);
                }

                // 請求データを登録
                return await _processService.PutInvoiceDatasAsync(invoiceDatas);
            });
        }
    }
}
