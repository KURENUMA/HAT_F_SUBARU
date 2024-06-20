using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using System.Linq.Dynamic.Core;
using HAT_F_api.CustomModels;

namespace HAT_F_api.Controllers
{
    /// <summary>
    /// 汎用検索API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly HatFContext _context;
        private readonly NLog.ILogger _logger;

        /// <summary>
        /// 一度に返却できる最大行数
        /// </summary>
        private const int MAX_ROWS = 200;

        public SearchController(HatFContext context, NLog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// デバッグ用エントリWHERE句を生成して文字列で返却する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns></returns>
        [HttpPost("test")]
        public ActionResult<string> Test([FromBody] List<GenSearchItem> searchItems)
        {
            return GenSearchUtil.CreateConditionSql(searchItems);
        }

        /// <summary>
        /// FosJyuchuHを検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("FosJyuchuH")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuH>>>> FosJyuchuH([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.FosJyuchuHs, searchItems)
                    .OrderBy(x => x.SaveKey)
                    .ThenBy(x => x.DenSort);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// FosJyuchuDを検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("FosJyuchuD")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuD>>>> FosJyuchuD([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.FosJyuchuDs, searchItems)
                    .OrderBy(x => x.SaveKey)
                    .ThenBy(x => x.DenSort).ThenBy(x => x.DenNoLine);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 売上予定一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-ready-sale")]
        public async Task<ActionResult<ApiResponse<List<ViewReadySale>>>> PostSearchViewReadySale([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewReadySales, searchItems)
                    .OrderBy(x => x.Hat注文番号);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 売上予定一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-ready-sale-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewReadySaleCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewReadySales, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 売上予定一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-ready-sale-test")]
        public async Task<ActionResult<ApiResponse<List<ViewReadySale>>>> GetSearchViewReadySale([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewReadySales
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入請求一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-billing")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseBilling>>>> PostSearchViewPurchaseBilling([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseBillings, searchItems)
                    .OrderBy(x => x.Hat注文番号);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入請求一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-billing-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewPurchaseBillingCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseBillings, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 仕入請求一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-purchase-billing-test")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseBilling>>>> GetSearchViewPurchaseBilling([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewPurchaseBillings
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入納品確定一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-receiving")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseReceiving>>>> PostSearchViewPurchaseReceiving([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseReceivings, searchItems)
                    .OrderBy(x => x.Hat注文番号);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入納品確定一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-receiving-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewPurchaseReceivingCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseReceivings, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 仕入納品確定一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-purchase-receiving-test")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseReceiving>>>> GetSearchViewPurchaseReceiving([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewPurchaseReceivings
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入請求明細（仕入金額照合）を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-billing-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseBillingDetail>>>> PostSearchViewPurchaseBillingDetail([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseBillingDetails, searchItems)
                    .OrderBy(x => x.Hat注文番号);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入請求明細（仕入金額照合）を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-purchase-billing-detail-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewPurchaseBillingDetailCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseBillingDetails, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 仕入請求明細（仕入金額照合）を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-purchase-billing-detail-test")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseBillingDetail>>>> GetSearchViewPurchaseBillingDetail([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewPurchaseBillingDetails
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 物件一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-construction")]
        public async Task<ActionResult<ApiResponse<List<ViewConstruction>>>>
            PostSearchViewConstruction([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewConstructions, searchItems)
                    .OrderBy(x => x.物件コード);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 物件一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-construction-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewConstructionCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewConstructions, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 物件一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="物件コード">物件コード</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-construction-test")]
        public async Task<ActionResult<ApiResponse<List<ViewConstruction>>>>
            GetSearchViewConstruction([FromQuery] string 物件コード, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewConstructions
                    .Where(x => string.IsNullOrEmpty(物件コード) || x.物件コード == 物件コード)
                    .OrderBy(x => x.物件コード);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 請求一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-invoice")]
        public async Task<ActionResult<ApiResponse<List<ViewInvoice>>>> PostSearchViewInvoice([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInvoices, searchItems)
                    .OrderBy(x => x.得意先コード);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 請求一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-invoice-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewInvoiceCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInvoices, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 請求済一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-invoice-amount")]
        public async Task<ActionResult<ApiResponse<List<ViewInvoicedAmount>>>> PostSearchViewAmountInvoice([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInvoicedAmounts, searchItems);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 請求済一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-invoice-amount-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewAmountInvoiceCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInvoicedAmounts, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 納品一覧表（社内用）を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-internal-delivery")]
        public async Task<ActionResult<ApiResponse<List<ViewInternalDelivery>>>> PostSearchViewInternalDelivery([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInternalDeliveries, searchItems);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 納品一覧表（社内用）を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-internal-delivery-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewInternalDeliveryCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewInternalDeliveries, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 売上訂正一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-correction")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesCorrection>>>> PostSearchViewSalesCorrection([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewSalesCorrections, searchItems)
                    .OrderBy(x => x.Hat注文番号);  //TODO:ソート修正

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 売上訂正一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-correction-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewSalesCorrectionCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewPurchaseBillings, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 売上訂正一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("search-view-sales-correction-test")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesCorrection>>>> GetSearchViewSalesCorrection([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewSalesCorrections
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 納品一覧表(訂正・返品)を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-correction-delivery")]
        public async Task<ActionResult<ApiResponse<List<ViewCorrectionDelivery>>>> PostSearchViewCorrectionDelivery([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewCorrectionDeliveries, searchItems);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 納品一覧表(訂正・返品)を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-correction-delivery-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewCorrectionDeliveryCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewCorrectionDeliveries, searchItems)
                .OrderBy(x => x.訂正日);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 納品一覧表(訂正・返品)を検索すある(Swaggerテスト用)
        /// </summary>
        /// <param name="得意先コード">得意先コード</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-correction-delivery-test")]
        public async Task<ActionResult<ApiResponse<List<ViewCorrectionDelivery>>>> GetSearchViewCorrectionDelivery([FromQuery] string 得意先コード, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewCorrectionDeliveries
                    .Where(x => string.IsNullOrEmpty(得意先コード) || x.得意先コード == 得意先コード)
                    .OrderBy(x => x.訂正日);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 返品入力一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-return")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturn>>>> PostSearchViewSalesReturn([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewSalesReturns, searchItems)
                                            .GroupBy(x => new { x.Hat注文番号, x.伝票番号 })
                                            .OrderBy(x => x.Key.Hat注文番号)
                                            .ThenBy(x => x.Key.伝票番号)
                                            .Select(x => x.OrderByDescending(id => id.承認要求番号).First());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 返品入力一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-return-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewSalesReturnCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewSalesReturns, searchItems)
                                            .GroupBy(x => new { x.Hat注文番号, x.伝票番号 })
                                            .OrderBy(x => x.Key.Hat注文番号)
                                            .ThenBy(x => x.Key.伝票番号)
                                            .Select(x => x.OrderByDescending(id => id.承認要求番号).First());
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 返品入力一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return-test")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturn>>>> GetSearchViewSalesReturn([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewSalesReturns
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .GroupBy(x => new { x.Hat注文番号, x.伝票番号 })
                    .OrderBy(x => x.Key.伝票番号)
                    .Select(x => x.OrderByDescending(id => id.承認要求番号).First());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 返品入庫一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-return-receipt")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturnReceipt>>>> PostSearchViewSalesReturnReceipt([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewSalesReturnReceipts, searchItems)
                                            .Distinct()
                                            .OrderBy(x => x.Hat注文番号)
                                            .ThenBy(x => x.伝票番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 返品入庫一覧を検索する
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("search-view-sales-return-receipt-count")]
        public async Task<ActionResult<ApiResponse<int>>> PostSearchViewSalesReturnReceiptCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewSalesReturnReceipts, searchItems)
                                            .Distinct()
                                            .OrderBy(x => x.Hat注文番号)
                                            .ThenBy(x => x.伝票番号);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 返品入庫一覧を検索する(Swaggerテスト用)
        /// </summary>
        /// <param name="Hat注文番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return-receipt-test")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturnReceipt>>>> GetSearchViewSalesReturnReceipt([FromQuery] string Hat注文番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewSalesReturnReceipts
                    .Where(x => string.IsNullOrEmpty(Hat注文番号) || x.Hat注文番号 == Hat注文番号)
                    .Distinct()
                    .OrderBy(x => x.伝票番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

    }
}