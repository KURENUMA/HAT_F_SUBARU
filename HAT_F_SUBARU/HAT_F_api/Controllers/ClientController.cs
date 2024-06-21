    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Web;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using HAT_F_api.Services.Authentication;
using HAT_F_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly HatFContext _context;
        private readonly HatFSearchService _hatFSearchService;
        private readonly HatFUpdateService _hatFUpdateService;
        private readonly SequenceNumberService _sequenceNumberService;
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;
        private readonly ProductSuggestion _productSuggestion;
        private readonly ConstructionLockService _constructionLockService;
        private readonly AmountCheckLockService _amountCheckLockService;
        private readonly SalesEditLockService _salesEditLockService;

        // デフォルト最大返却件数
        private const int MAX_ROWS = 200;

        /// <summary>受注情報補完</summary>
        private readonly CompleteOrderService _completeOrderService;

        // TODO 〇〇サービス
        /// <summary>〇〇サービス</summary>
        private readonly ProcessService _processService;

        private readonly PurchaseService _purchaseService;

        /// <summary>在庫サービス</summary>
        private readonly StockService _stockService;

        /// <summary>与信額サービス</summary>
        private readonly CreditService _creditService;

        /// <summary>更新用データのCREATE_DATE等セット機能</summary>
        private readonly UpdateInfoSetter _updateInfoSetter;


        private readonly NLog.ILogger _logger;

        /// <summary>コンストラクタ</summary>
        /// <param name="context">DBコンテキスト</param>
        /// <param name="logger">NLog.ILogger</param>
        /// <param name="hatFApiExecutionContext">API実行コンテキスト</param>
        /// <param name="sequenceNumberService">連番</param>
        /// <param name="hatFSearchService">検索</param>
        /// <param name="hatFUpdateService">受注情報更新</param>
        /// <param name="productSuggestion">サジェスト</param>
        /// <param name="completeOrderService">受注情報補完</param>
        /// <param name="processService">〇〇サービス</param>
        /// <param name="purchaseService"></param>
        /// <param name="constructionLockService">物件詳細ロックサービス</param>
        /// <param name="amountCheckLockService">仕入金額照合ロックサービス</param>
        /// <param name="salesEditLockService">売上額訂正ロックサービス</param>
        /// <param name="stockService">在庫サービス</param>
        /// <param name="creditService">与信額サービス</param>
        /// <param name="updateInfoSetter">エンティティ更新日時設定サービス</param>
        public ClientController(HatFContext context,
            NLog.ILogger logger,
            HatFApiExecutionContext hatFApiExecutionContext,
            SequenceNumberService sequenceNumberService,
            HatFSearchService hatFSearchService,
            HatFUpdateService hatFUpdateService,
            ProductSuggestion productSuggestion,
            CompleteOrderService completeOrderService,
            ProcessService processService,
            PurchaseService purchaseService,
            ConstructionLockService constructionLockService,
            AmountCheckLockService amountCheckLockService,
            SalesEditLockService salesEditLockService,
            StockService stockService,
            CreditService creditService,
            UpdateInfoSetter updateInfoSetter
            )
        {
            _context = context;
            _logger = logger;
            _hatFSearchService = hatFSearchService;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _sequenceNumberService = sequenceNumberService;
            _hatFUpdateService = hatFUpdateService;
            _productSuggestion = productSuggestion;
            _completeOrderService = completeOrderService;
            _processService = processService;
            _purchaseService = purchaseService;
            _constructionLockService = constructionLockService;
            _amountCheckLockService = amountCheckLockService;
            _salesEditLockService = salesEditLockService;
            _stockService = stockService;
            _creditService = creditService;
            _updateInfoSetter = updateInfoSetter;
        }

        // GET: api/FosJyuchuH
        [HttpGet("init")]
        public async Task<ActionResult<ApiResponse<ClientInit>>> initClient()
        {
            if (_context.FosJyuchuHs == null)
            {
                return NotFound();
            }

            var init = await _hatFSearchService.initClient();
            return new ApiOkResponse<ClientInit>(init);
        }

        /// <summary>
        /// 受注情報検索
        /// </summary>
        /// <param name="condition">検索条件</param>
        /// <returns>検索結果</returns>
        /// <remarks>HttpGetとしたいがConditionクラスでのやりとりを可能とするためPostとする</remarks>
        [HttpPost("orders")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FosJyuchuSearch>>>> SearchFosJyuchuAsync([FromBody] FosJyuchuSearchCondition condition)
        {
            var results = await _hatFSearchService.FosJyuchuSearchAsync(condition);
            return new ApiOkResponse<IEnumerable<FosJyuchuSearch>>(results);
        }

        /// <summary>
        /// 受注情報削除
        /// </summary>
        /// <param name="saveKey">受注キー値</param>
        /// <param name="denSort">ページ番号</param>
        /// <returns>更新行数（受注ヘッダ＋受注明細）</returns>
        [HttpDelete("orders/{saveKey}/{denSort}")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteFosJyuchuAsync(string saveKey, string denSort)
        {
            var result = await _hatFUpdateService.FosJyuchuDeleteAsync(saveKey, denSort);
            return new ApiOkResponse<int>(result.Data);
        }

        [HttpPost("orders/search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FosJyuchuSearchResult>>>> SearchFosJyuchuHAsync([FromQuery] int rows = 30)
        {
            var results = await _hatFSearchService.SearchFosJyuchuHAsync(rows);
            return new ApiOkResponse<IEnumerable<FosJyuchuSearchResult>>(results);
        }

        [HttpGet("orders/{saveKey}")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuPage>>>> GetPages(string saveKey)
        {
            var results = await _hatFSearchService.GetPages(saveKey);
            return new ApiOkResponse<List<FosJyuchuPage>>(results.ToList());
        }

        /// <summary>受注情報保存</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>保存に際して補正された受注情報</returns>
        [HttpPut("orders/{saveKey}")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuPage>>>> DeleteInsertPagesAsync([FromBody] FosJyuchuPages pages)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFUpdateService.SavePagesAsync(pages);
            });
        }

        /// <summary>受注情報確定</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>確定に際して補正された受注情報</returns>
        [HttpPut("orders/commit")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuPage>>>> CommitPagesAsync([FromBody] FosJyuchuPages pages)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                var result = await _hatFUpdateService.CommitPagesAsync(pages);
                await _stockService.ReserveProductAsync(pages);
                await _stockService.RefleshReserveAsync(pages);
                await transaction.CommitAsync();
                return result;
            });
        }

        /// <summary>発注照合</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>照合に際して補正された受注情報</returns>
        [HttpPut("orders/collation")]
        public async Task<ActionResult<ApiResponse<List<FosJyuchuPage>>>> CollationPagesAsync([FromBody] FosJyuchuPages pages)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                var result = await _hatFUpdateService.CollationPagesAsync(pages);
                await _stockService.ReserveProductAsync(pages);
                await _stockService.RefleshReserveAsync(pages);
                await transaction.CommitAsync();
                return result;
            });
        }

        private DateTime GetPeriodBeginningDate()
        {
            // 指定年の期初を作成
            return new DateTime(_hatFApiExecutionContext.ExecuteDateTimeJst.Year, 4, 1);
        }

        /// <summary>
        /// 商品在庫取得 (新)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet("product-stock/")]
        public async Task<ActionResult<ApiResponse<List<ViewProductStock>>>> GetProductStockAsync([FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 期初日
                DateTime periodBeginningDate = GetPeriodBeginningDate();

                // 商品在庫取得
                var result = _hatFSearchService.GetProductStock(periodBeginningDate);
                return await result
                    .OrderBy(x => x.商品コード)
                    .Skip((page - 1) * rows)
                    .Take(rows)
                    .ToListAsync();
            });
        }

        /// <summary>
        /// 商品在庫取得:件数
        /// </summary>
        /// <returns></returns>
        [HttpGet("product-stock-count/")]
        public async Task<ActionResult<ApiResponse<int>>> GetProductStockCountAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 期初日
                DateTime periodBeginningDate = GetPeriodBeginningDate();

                // 商品在庫取得
                var result = _hatFSearchService.GetProductStock(periodBeginningDate);
                return await result.CountAsync();
            });
        }

        /// <summary>
        /// 商品在庫全体 (新)
        /// </summary>
        /// <returns></returns>
        [HttpGet("product-stock-summary/")]

        public async Task<ActionResult<ApiResponse<ViewProductStock>>> GetProductStockSummaryAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 期初日
                DateTime periodBeginningDate = GetPeriodBeginningDate();

                // 商品在庫取得
                var result = _hatFSearchService.GetProductStockSummary(periodBeginningDate);
                return await result.SingleAsync();
            });
        }


        /*
        /// <summary>
        /// 商品在庫取得 (旧)
        /// </summary>
        /// <param name="page">取得ページ</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("product-stock-old/")]
        public async Task<ActionResult<ApiResponse<List<ProductStock>>>> GetProductStockOldAsync([FromQuery] int page = 0, [FromQuery] int rows = 100)
        {
            // Entity Framework で実行する SQL のイメージ
            FormattableString sql = $"""
            SELECT *
            FROM table_name
            WHERE any constions
            ORDER BY col_name
            OFFSET {rows * page}
            ROWS FETCH NEXT {rows} ROWS ONLY
        """;

            // 仮
            var data = new List<ProductStock>();
            ProductStock ps;

            ps = new ProductStock();
            ps.ProdCode = "10010";
            ps.ProdName = "INSPARON5408MJK8024D";
            ps.ProdFullName = "ＩＮＳＰＡＲＯＮ５４０８ＭＪＫ８０２４Ｄ";
            ps.CategoryCode = "1002";
            ps.ProdCategoryName = "ノートパソコン";
            ps.BeginningBalance = 15;
            ps.StockReceiptTotal = 0;
            ps.StockOutputTotal = 10;
            ps.PeriodEndEvaluationPrice = 25_600;
            data.Add(ps);

            ps = new ProductStock();
            ps.ProdCode = "10013";
            ps.ProdName = "INSPARON3218JL8021MKL";
            ps.ProdFullName = "ＩＮＳＰＡＲＯＮ３２１８ＪＬ８０２１ＭＫＬ";
            ps.CategoryCode = "1001";
            ps.ProdCategoryName = "デスクトップパソコン";
            ps.BeginningBalance = 0;
            ps.StockReceiptTotal = 0;
            ps.StockOutputTotal = 0;
            ps.PeriodEndEvaluationPrice = 36_500;
            data.Add(ps);

            ps = new ProductStock();
            ps.ProdCode = "10014";
            ps.ProdName = "INSPERON3023MKL9066NT";
            ps.ProdFullName = "ＩＮＳＰＥＲＯＮ３０２３ＭＫＬ９０６６ＮＴ";
            ps.CategoryCode = "1001";
            ps.ProdCategoryName = "デスクトップパソコン";
            ps.BeginningBalance = 16;
            ps.StockReceiptTotal = 0;
            ps.StockOutputTotal = 9;
            ps.PeriodEndEvaluationPrice = 65_000;
            data.Add(ps);

            ps = new ProductStock();
            ps.ProdCode = "10033";
            ps.ProdName = "SMV-DISKPOWER CE500GW";
            ps.ProdFullName = "ＳＭＶ－ＤＩＳＫＰＯＷＥＲ　ＣＥ５００ＧＷ";
            ps.CategoryCode = "1001";
            ps.ProdCategoryName = "デスクトップパソコン";
            ps.BeginningBalance = 6;
            ps.StockReceiptTotal = 14;
            ps.StockOutputTotal = 20;
            ps.PeriodEndEvaluationPrice = 115_000;
            data.Add(ps);

            // 本来は SQL Server にアクセスして取ってくる
            await Task.Delay(0);

            return new ApiOkResponse<List<ProductStock>>(data);
        }

        /// <summary>
        /// 商品在庫全体 (旧)
        /// </summary>
        /// <returns></returns>
        [HttpGet("product-stock-summary-old/")]

        public async Task<ActionResult<ApiResponse<ProductStockSummary>>> GetProductStockSummaryOldAsync()
        {
            // 実際にはDBにクエリーを投げて全体金額を算出する
            var data = (await GetProductStockOldAsync()).Value!;

            var ps = new ProductStockSummary();
            ps.BeginningBalance = data.Data.Sum(s => s.BeginningBalance);
            ps.StockReceiptTotal = data.Data.Sum(s => s.StockReceiptTotal);
            ps.StockOutputTotal = data.Data.Sum(s => s.StockOutputTotal);
            ps.Price = data.Data.Sum(s => CalcPrice(s));

            return new ApiOkResponse<ProductStockSummary>(ps);

            // ProductStock.Price を計算
            decimal CalcPrice(ProductStock ps)
            {
                return ps.PeriodEndEvaluationPrice * (ps.BeginningBalance - ps.StockReceiptTotal + ps.StockOutputTotal);
            };
        }
        */

        /// <summary>
        /// 得意先検索
        /// </summary>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="torihikiName">得意先名</param>
        /// <param name="torihikiNameKana">得意先名カナ</param>
        /// <param name="teamCd">チームCD</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("torihiki")]
        public async Task<ActionResult<ApiResponse<List<Torihiki>>>> GetTorihikiAsync(
            [FromQuery] string torihikiCd = null,
            [FromQuery] string torihikiName = null,
            [FromQuery] string torihikiNameKana = null,
            [FromQuery] string teamCd = null,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetTorihikiAsync(torihikiCd, torihikiName, torihikiNameKana, rows);
            });
        }

        /// <summary>
        /// キーマン検索
        /// </summary>
        /// <param name="teamCd">チームCD</param>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="keymanCd">キーマンCD</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("keyman")]
        public async Task<ActionResult<ApiResponse<List<Keyman>>>> GetKeymenAsync(
            [FromQuery] string teamCd,
            [FromQuery] string torihikiCd,
            [FromQuery] string keymanCd,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetKeymenAsync(teamCd, torihikiCd, keymanCd, rows);
            });
        }

        /// <summary>
        /// 仕入先検索
        /// </summary>
        /// <param name="supplierCd">仕入先CD</param>
        /// <param name="supplierName">仕入先名</param>
        /// <param name="supplierNameKana">仕入先名カナ</param>
        /// <param name="teamCd">チームCD</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("supplier")]
        public async Task<ActionResult<ApiResponse<List<Supplier>>>> GetSuppliersAsync(
            [FromQuery] string supplierCd = null,
            [FromQuery] string supplierName = null,
            [FromQuery] string supplierNameKana = null,
            [FromQuery] string teamCd = null,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetSuppliersAsync(supplierCd, supplierName, supplierNameKana, teamCd, rows);
            });
        }

        /// <summary>
        /// 仕入先分類CD検索
        /// </summary>
        /// <param name="supplierCd">仕入先CD</param>
        /// <param name="supplierName">仕入先名</param>
        /// <param name="supplierNameKana">仕入先名カナ</param>
        /// <param name="teamCd">チームCD</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("supplier-category")]
        public async Task<ActionResult<ApiResponse<List<SupplierCategory>>>> GetSupplierCategoriesAsync(
            [FromQuery] string supplierCd = null,
            [FromQuery] string supplierName = null,
            [FromQuery] string supplierNameKana = null,
            [FromQuery] string teamCd = null,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetSupplierCategoriesAsync(supplierCd, supplierName, supplierNameKana, teamCd, rows);
            });
        }

        /// <summary>
        /// 郵便番号検索
        /// </summary>
        /// <param name="postCode">郵便番号</param>
        /// <param name="address">住所</param>
        /// <param name="rows">取得件数</param>
        /// <returns>郵便番号、住所に基づいて取得した住所のリスト</returns>
        [HttpGet("post-address/")]
        public async Task<ActionResult<ApiResponse<List<PostAddress>>>> GetPostAddressesAsync([FromQuery] string postCode = null, [FromQuery] string address = null, [FromQuery] int rows = 200)
        {
            var results = await _hatFSearchService.GetPostAddressesAsync(postCode, HttpUtility.UrlDecode(address), rows);
            return new ApiOkResponse<List<PostAddress>>(results);
        }

        /// <summary>
        /// 現場検索
        /// </summary>
        /// <param name="custCode">顧客コード</param>
        /// <param name="custSubNo">顧客枝番</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="genbaName">現場名</param>
        /// <param name="address">住所</param>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="keymanCode">キーマンCD</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致した現場情報のリスト</returns>
        [HttpGet("genba/")]
        public async Task<ActionResult<ApiResponse<List<SearchGenbaResult>>>> GetDestinationsAsync(
            [FromQuery] string custCode = null,
            [FromQuery] short? custSubNo = null,
            [FromQuery] string genbaCd = null,
            [FromQuery] string genbaName = null,
            [FromQuery] string address = null,
            [FromQuery] string torihikiCd = null,
            [FromQuery] string keymanCode = null,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetDestinationsAsync(
                    custCode, custSubNo, genbaCd, genbaName, address, torihikiCd, keymanCode, rows);
            });
        }

        /// <summary>
        /// 工事店検索
        /// </summary>
        /// <param name="koujitenCd">工事店CD</param>
        /// <param name="koujitenName">工事店名</param>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致した工事店情報のリスト</returns>
        [HttpGet("koujiten/")]
        public async Task<ActionResult<ApiResponse<List<Koujiten>>>> GetKoujitenAsync(
            [FromQuery] string koujitenCd = null,
            [FromQuery] string koujitenName = null,
            [FromQuery] string torihikiCd = null,
            [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetKoujitenAsync(koujitenCd, koujitenName, torihikiCd, rows);
            });
        }

        /// <summary>
        /// 商品検索（HAT商品）
        /// </summary>
        /// <param name="supplierCd">仕入先CD</param>
        /// <param name="productNoOrProductName">品番/名（規格）</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致したHAT商品のリスト</returns>
        [HttpGet("product-hat/")]
        public async Task<ActionResult<ApiResponse<List<Product>>>> GetProductsOfHatAsync(
            [FromQuery] string supplierCd = null,
            [FromQuery] string productNoOrProductName = null,
            [FromQuery] int rows = 200)
        {
            var results = await _hatFSearchService.GetProductsOfHatAsync(supplierCd, productNoOrProductName, rows);
            return new ApiOkResponse<List<Product>>(results);
        }

        /// <summary>
        /// 商品検索（メーカー商品）
        /// </summary>
        /// <param name="supplierCd">仕入先CD</param>
        /// <param name="productNoOrProductName">品番/名（規格）</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致したHAT商品のリスト</returns>
        [HttpGet("product-maker/")]
        public async Task<ActionResult<ApiOkResponse<List<Product>>>> GetProductsOfMakerAsync(
            [FromQuery] string supplierCd = null,
            [FromQuery] string productNoOrProductName = null,
            [FromQuery] int rows = 200)
        {
            var data = new List<Product>();
            Product pMaker;

            // Data1
            pMaker = new Product();
            pMaker.Cd24 = "Maker XXXX1";
            pMaker.MKey = "JPｼﾞﾖｲﾝﾄ 90E 10";
            pMaker.Code5 = "Mk10";
            pMaker.Nnm = "ＪＰジョイント";
            pMaker.Anm = "ﾍﾞﾈｯｸｽ JPｼﾞﾖｲﾝﾄ 90E 10";
            pMaker.MkCd = "BNX";
            pMaker.MInfo = "";
            data.Add(pMaker);

            // Data2
            pMaker = new Product();
            pMaker.Cd24 = "Maker XXXX2";
            pMaker.MKey = "JPｼﾞﾖｲﾝﾄ 90E 20";
            pMaker.Code5 = "Mk20";
            pMaker.Nnm = "ＪＰジョイント2";
            pMaker.Anm = "ﾍﾞﾈｯｸｽ JPｼﾞﾖｲﾝﾄ 90E 20";
            pMaker.MkCd = "BNX";
            pMaker.MInfo = "";
            data.Add(pMaker);

            // Data3
            pMaker = new Product();
            pMaker.Cd24 = "Maker XXXX3";
            pMaker.MKey = "JPｼﾞﾖｲﾝﾄ 90E 30";
            pMaker.Code5 = "Mk30";
            pMaker.Nnm = "ＪＰジョイント3";
            pMaker.Anm = "ﾍﾞﾈｯｸｽ JPｼﾞﾖｲﾝﾄ 90E 30";
            pMaker.MkCd = "BNX";
            pMaker.MInfo = "";
            data.Add(pMaker);

            await Task.Delay(0);

            return new ApiOkResponse<List<Product>>(data);
        }

        /// <summary>
        /// 仕入先請求情報取得
        /// </summary>
        [HttpGet("supplier-invoices/")]
        public async Task<ActionResult<ApiResponse<IEnumerable<SupplierInvoices>>>> GetSupplierInvoicesAsync()
        {
            var data = new List<SupplierInvoices>();
            SupplierInvoices si;

            // Data1
            si = new SupplierInvoices();
            si.OrderNo = "2305O6406762";
            si.denNo = "798896";
            si.ShiresakiCd = "842A16";
            si.ShiresakiName = "㈱ヤマダデンキ なんば営業所";
            si.OrderAmnt = 2564.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2022, 9, 12);
            si.InpDate = new DateTime(2022, 9, 1);
            si.InpName = "山本　雄大";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "㈱オーテック 東京支店仙台営業所";
            si.TokuisakiSales = "井出　晶子";
            si.InvoiceConfirmation = 0;
            si.ShiresakiPaymentMonth = 0;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 1;
            data.Add(si);

            // Data2
            si = new SupplierInvoices();
            si.OrderNo = "2305O6413111";
            si.denNo = "841916";
            si.ShiresakiCd = "5669H9";
            si.ShiresakiName = "ＴＯＴＯ㈱ 大阪特販部";
            si.OrderAmnt = 6280.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2022, 9, 12);
            si.InpDate = new DateTime(2022, 9, 12);
            si.InpName = "山本　雄大";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "パナソニックリビング中部㈱ 岐阜支店　岐阜住建";
            si.TokuisakiSales = "高見　彩子";
            si.InvoiceConfirmation = 1;
            si.ShiresakiPaymentMonth = 1;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 1;
            data.Add(si);

            // Data3
            si = new SupplierInvoices();
            si.OrderNo = "2307O6768992";
            si.denNo = "119258";
            si.ShiresakiCd = "5669B9";
            si.ShiresakiName = "ＴＯＴＯ㈱ 大阪支社部";
            si.OrderAmnt = 3950.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2023, 8, 2);
            si.InpDate = new DateTime(2023, 7, 26);
            si.InpName = "宿里　和男";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "パナソニックリビング中部㈱ 岐阜支店　岐阜住建";
            si.TokuisakiSales = "奥田　真由美";
            si.InvoiceConfirmation = 2;
            si.ShiresakiPaymentMonth = 2;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 2;
            data.Add(si);

            // Data4
            si = new SupplierInvoices();
            si.OrderNo = "2309O6995418";
            si.denNo = "734600";
            si.ShiresakiCd = "745812";
            si.ShiresakiName = "福西電機㈱ 中部第二営業所";
            si.OrderAmnt = 2564.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2023, 4, 20);
            si.InpDate = new DateTime(2023, 4, 19);
            si.InpName = "岡野　誠司";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "パナソニックリビング中部㈱ 北陸支店　石川（営）";
            si.TokuisakiSales = "桂";
            si.InvoiceConfirmation = 3;
            si.ShiresakiPaymentMonth = 1;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 1;
            data.Add(si);

            // Data5
            si = new SupplierInvoices();
            si.OrderNo = "2309O6990301";
            si.denNo = "739469";
            si.ShiresakiCd = "808901";
            si.ShiresakiName = "未来工業㈱\"";
            si.OrderAmnt = 6280.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2022, 9, 12);
            si.InpDate = new DateTime(2022, 9, 5);
            si.InpName = "岡野　誠司";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "ＤＡＥＬＥＣＯ";
            si.TokuisakiSales = "";
            si.InvoiceConfirmation = 0;
            si.ShiresakiPaymentMonth = 1;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 1;
            data.Add(si);

            // Data6
            si = new SupplierInvoices();
            si.OrderNo = "2309O6037524";
            si.denNo = "762948";
            si.ShiresakiCd = "771718";
            si.ShiresakiName = "パナソニックＨＳ㈱ 名古屋住設建材営業所";
            si.OrderAmnt = 4590.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2023, 10, 2);
            si.InpDate = new DateTime(2023, 9, 29);
            si.InpName = "岡野　誠司";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "㈱三晃空調 東北支店";
            si.TokuisakiSales = "";
            si.InvoiceConfirmation = 1;
            si.ShiresakiPaymentMonth = 1;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 2;
            data.Add(si);

            // Data7
            si = new SupplierInvoices();
            si.OrderNo = "2211O6639206";
            si.denNo = "236862";
            si.ShiresakiCd = "577501";
            si.ShiresakiName = "㈱東洋機工 本社";
            si.OrderAmnt = 6050000.00m;
            si.CmpTax = "E";
            si.Nouki = new DateTime(2023, 8, 2);
            si.InpDate = new DateTime(2023, 7, 26);
            si.InpName = "山本　雄大";
            si.HatSales = "営業担当A";
            si.Tokuisaki = "パナソニックリビング中部㈱ 岐阜支店　岐阜住建";
            si.TokuisakiSales = "高見　彩子";
            si.InvoiceConfirmation = 2;
            si.ShiresakiPaymentMonth = 0;
            si.ShiresakiPaymentDay = 99;
            si.ShiresakiPaymentClassification = 1;
            data.Add(si);

            await Task.Delay(0);

            return new ApiOkResponse<IEnumerable<SupplierInvoices>>(data);
        }

        /// <summary>
        /// 受注番号取得
        /// </summary>
        /// <returns></returns>
        [HttpPost("order-no/next")]
        public async Task<ActionResult<ApiResponse<int>>> PostOrderNoNextAsync()
        {
            int seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHOrderNo);
            return new ApiOkResponse<int>(seqNo);
        }

        /// <summary>
        /// DSEQ取得
        /// </summary>
        /// <returns></returns>
        [HttpPost("d-seq/next")]
        public async Task<ActionResult<ApiResponse<int>>> PostDSeqNextAsync()
        {
            int seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHDSeq);
            return new ApiOkResponse<int>(seqNo);
        }

        /// <summary>
        /// 伝票番号取得
        /// </summary>
        /// <returns></returns>
        [HttpPost("den-no/next")]
        public async Task<ActionResult<ApiResponse<int>>> PostDenNoNextAsync()
        {
            int seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHDenNo);
            return new ApiOkResponse<int>(seqNo);
        }

        /// <summary>HAT-F注文番号取得</summary>
        /// <param name="key">H注番のキー</param>
        /// <param name="denFlg">伝区</param>
        /// <returns></returns>
        [HttpPost("hat-order-no/next")]
        public async Task<ActionResult<ApiResponse<string>>> GetNextHatOrderNoAsync([FromQuery] string key, [FromQuery] string denFlg)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _sequenceNumberService.GetNextHatOrderNoAsync(key, denFlg);
            });
        }

        /// <summary>
        /// 商品サジェスト情報取得
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("product-suggestion/")]
        public ActionResult<ApiResponse<List<string>>> GetProductSuggestion([FromQuery] string keyword, [FromQuery] int rows = 200)
        {
            var results = _productSuggestion.GetProductSuggestion(keyword, rows);
            return new ApiOkResponse<List<string>>(results);
        }

        /// <summary>
        /// 部門付き社員検索
        /// </summary>
        /// <param name="empCode">社員コード</param>
        /// <returns></returns>
        /// <response code="404">社員が見つからない場合</response>
        [HttpGet("employee-depts")]
        public async Task<ActionResult<ApiResponse<EmployeeDept>>> GetEmployeeDeptAsync([FromQuery][Required] string empCode)
        {
            var result = await _hatFSearchService.GetEmployeeDeptAsync(empCode);
            if (result.Employee == null)
            {
                return new ApiErrorResponse<EmployeeDept>(ApiResultType.ApiGenericError, "Not Found");
            }
            return new ApiOkResponse<EmployeeDept>(result);
        }

        /// <summary>
        /// メール送信可能な社員検索
        /// </summary>
        /// <returns></returns>
        [HttpGet("sendable-employees")]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetSendableEmployeesAsync()
            => new ApiOkResponse<List<Employee>>(await _hatFSearchService.GetSendableEmployeesAsync());

        /// <summary>
        /// 仕入金額照合一覧取得
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet("purchase-billing-details")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseBillingDetail>>>> GetPurchaseBillingDetailAsync([FromQuery] ViewPurchaseBillingDetailCondition condition, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetViewPurchaseBillingDetailAsync(condition, rows);
                return results;
            });
        }

        /// <summary>
        /// 仕入金額照合の変更
        /// </summary>
        /// <param name="updateRequest">更新内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("purchase-billing-details")]
        public async Task<ActionResult<ApiResponse<int>>> PutPurchaseBillingDetailAsync(
            [FromBody] List<PurchaseBillingDetail> updateRequest)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var datas = await _purchaseService.ToPurchaseDatasAsync(updateRequest);

                return await _purchaseService.PutPurchaseDatasAsync(datas);
            });
        }

        /// <summary>
        /// 仕入金額照合の変更(Mデータ用)
        /// </summary>
        /// <param name="updateRequest">更新内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("purchase-billing-details-Delivery")]
        public async Task<ActionResult<ApiResponse<int>>> PutPurchaseBillingDetailDeliveryAsync([FromBody] List<PurchaseBillingDetail> updateRequest)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var datas = await _purchaseService.ToDeliveryPurchaseDatasAsync(updateRequest);

                return await _purchaseService.PutPurchaseDatasAsync(datas);
            });
        }

        /// <summary>
        /// 仕入納品確認の一覧取得
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet("purchase-receiving-details")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseReceivingDetail>>>> GetPurchaseReceivingDetailAsync([FromQuery] ViewPurchaseReceivingDetail condition, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetViewPurchaseReceivingDetailAsync(condition.Hat注文番号, rows);
                return results;
            });
        }

        /// <summary>
        /// 仕入納品確認の変更
        /// </summary>
        /// <param name="updateRequest">更新内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("purchase-receiving-details")]
        public async Task<ActionResult<ApiResponse<int>>> PutPurchaseReceivingDetailAsync(
            [FromBody] List<PurchaseBillingDetail> updateRequest)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var datas = await _purchaseService.ToPurchaseDatasAsync(updateRequest);

                return await _purchaseService.PutPurchaseDatasAsync(datas);
            });
        }

        /// <summary>受注情報のヘッダー部分を補完する</summary>
        /// <param name="request">補完のキー</param>
        /// <returns>補完結果</returns>
        [HttpPost("complete-header")]
        public async Task<ActionResult<ApiResponse<CompleteHeaderResult>>> CompleteHeaderAsync([FromBody] CompleteHeaderRequest request)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _completeOrderService.CompleteHeaderAsync(request);
            });
        }

        /// <summary>受注情報の明細部分の金額について補完する</summary>
        /// <param name="request">入力パラメータ</param>
        /// <returns>補完結果</returns>
        [HttpPost("complete-details")]
        public async Task<ActionResult<ApiResponse<CompleteDetailsResult>>> CompleteDetailsAsync([FromBody] CompleteDetailsRequest request)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _completeOrderService.CompleteDetailsAsync(request);
            });
        }

        /// <summary>
        /// 返品入庫一覧取得
        /// </summary>
        /// <param name="denNo">伝No</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("return-receipts/")]
        public async Task<ActionResult<ApiResponse<List<ViewReturnReceipt>>>> GetReturnReceiptsAsync([FromQuery] string denNo, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetReturnReceiptsAsync(denNo, rows);
                return results;
            });
        }



        /// <summary>
        /// 入庫確認一覧取得
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("warehousing-receivings-2")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingReceiving>>>> PostViewWarehousingReceivings([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewWarehousingReceivings, searchItems)
                    .OrderBy(x => x.伝票番号);  //TODO:ソート修正

                System.Diagnostics.Debug.WriteLine(query.ToQueryString());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 入庫確認一覧取得(件数)
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("warehousing-receivings-count-2")]
        public async Task<ActionResult<ApiResponse<int>>> PostViewWarehousingReceivingsCount([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewWarehousingReceivings, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 入庫確認一覧取得(Swaggerテスト用)
        /// </summary>
        /// <param name="伝票番号">Hat注文番号</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpGet("warehousing-receivings-test")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingReceiving>>>> GetViewWarehousingReceivingsTest([FromQuery] string 伝票番号, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _context.ViewWarehousingReceivings
                    .Where(x => string.IsNullOrEmpty(伝票番号) || x.伝票番号 == 伝票番号)
                    .OrderBy(x => x.伝票番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }


        /// <summary>
        /// 入庫確認一覧取得
        /// </summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("warehousing-receivings")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingReceiving>>>> GetWarehousingReceivingsAsync([FromQuery] string denNo, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetWarehousingReceivings(denNo, rows)
                    .OrderBy(x => x.伝票番号)
                    .Take(rows)
                    .ToListAsync();

                return results;
            });
        }

        /// <summary>
        /// 入庫確認一覧取得：件数
        /// </summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("warehousing-receivings-count")]
        public async Task<ActionResult<ApiResponse<int>>> GetWarehousingReceivingsCountAsync([FromQuery] string denNo, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetWarehousingReceivings(denNo, rows)
                    .CountAsync();

                return results;
            });
        }

        /// <summary>
        /// 入庫確認詳細取得
        /// </summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("warehousing-receiving-details")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingReceivingDetail>>>> GetWarehousingReceivingDetailsAsync([FromQuery] string denNo, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetWarehousingReceivingDetails(denNo, rows)
                    .Take(rows)
                    .ToListAsync();

                return results;
            });
        }

        /// <summary>
        /// 入庫確認詳細更新
        /// </summary>
        /// <returns></returns>
        [HttpPut("warehousing-receiving-details")]
        public async Task<ActionResult<ApiResponse<int>>> PutWarehousingReceivingDetailsAsync([FromBody] List<ViewWarehousingReceivingDetail> data)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = _hatFSearchService.PutWarehousingReceivingDetails(data);
                await _context.SaveChangesAsync();
                return results;
            });
        }





        //------------------------


        /// <summary>
        /// 出庫指示一覧取得
        /// </summary>
        /// <param name="searchItems">検索条件リスト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="page">取得ページ番号(1開始)</param>
        /// <returns>検索結果</returns>
        [HttpPost("warehousing-shippings-2")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingShipping>>>> GetWarehousingShippings2Async([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewWarehousingShippings, searchItems)
                    .OrderBy(x => x.伝票番号);  //TODO:ソート修正

                System.Diagnostics.Debug.WriteLine(query.ToQueryString());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        [HttpPost("warehousing-shippings-count-2")]
        public async Task<ActionResult<ApiResponse<int>>> GetWarehousingShippingsCount2Async([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewWarehousingShippings, searchItems);
                return await query.CountAsync();
            });
        }

        //---------------------






        /// <summary>
        /// 出荷指示一覧取得
        /// </summary>
        /// <param name="includeOrderPrinted">出荷指示書印刷済の分を含めるか</param>
        /// <param name="shippedDateFrom">出荷日From</param>
        /// <param name="shippedDateTo">出荷日To</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("warehousing-shippings")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingShipping>>>> GetWarehousingShippingsAsync([FromQuery] bool includeOrderPrinted = false, [FromQuery] DateTime? shippedDateFrom = null, [FromQuery] DateTime? shippedDateTo = null, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetWarehousingShippings(includeOrderPrinted, shippedDateFrom, shippedDateTo, rows);
                var results = await query.ToListAsync();
                return results;
            });
        }

        /// <summary>
        /// 出荷指示一覧更新
        /// </summary>
        [HttpPut("warehousing-shippings")]
        public async Task<ActionResult<ApiResponse<int>>> PutWarehousingShippingsAsync([FromBody] List<ViewWarehousingShipping> data)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.PutWarehousingShippingsAsync(data);
                return results;
            });
        }

        /// <summary>
        /// 出荷指示詳細取得
        /// </summary>
        /// <param name="saveKey"></param>
        /// <param name="denSort"></param>
        /// <param name="denNoLine"></param>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        [HttpGet("warehousing-shipping-details")]
        public async Task<ActionResult<ApiResponse<List<ViewWarehousingShippingDetail>>>> GetWarehousingShippingDetailsAsync([FromQuery] string saveKey, [FromQuery] string denSort, [FromQuery] string denNoLine, [FromQuery] string denNo, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var results = await _hatFSearchService.GetWarehousingShippingDetailsAsync(saveKey, denSort, denNoLine, denNo, rows);
                return results;
            });
        }

        /// <summary>売上予定一覧の件数を取得する</summary>
        /// <param name="searchItems">検索条件</param>
        /// <returns>件数</returns>
        [HttpPost("view-ready-sales-count")]
        public async Task<ActionResult<ApiResponse<int>>> GetViewReadySalesCountAsync(List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await GenSearchUtil.DoGenSearch(_context.ViewReadySales, searchItems).CountAsync();
            });
        }

        /// <summary>売上予定一覧を検索</summary>
        /// <param name="searchItems">検索条件</param>
        /// <param name="rows">１ページあたりの件数</param>
        /// <param name="page">ページ位置</param>
        /// <returns>検索結果</returns>
        [HttpPost("view-ready-sales")]
        public async Task<ActionResult<ApiResponse<List<ViewReadySale>>>>
            GetViewReadySalesAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewReadySales, searchItems)
                    // 発注先種別(SUPPLIER_TYPE)に応じた文字列を設定
                    .Select(v => new ViewReadySale()
                    {
                        物件コード = v.物件コード,
                        物件名 = v.物件名,
                        得意先コード = v.得意先コード,
                        得意先 = v.得意先,
                        発注先 = v.発注先,
                        発注先種別名 = v.発注先 == 0 ? "HAT" : v.発注先 == 1 ? "HAT以外" : null,
                        Hat注文番号 = v.Hat注文番号,
                        営業担当者名 = v.営業担当者名,
                        受注合計金額 = v.受注合計金額,
                        利率 = v.利率,
                        伝票番号 = v.伝票番号,
                        伝票区分 = v.伝票区分,
                        伝票区分名 = v.伝票区分名,
                        納期 = v.納期,
                        仕入先コード = v.仕入先コード,
                        仕入先名 = v.仕入先名,
                        入荷日 = v.入荷日,
                        商品コード = v.商品コード,
                        商品名 = v.商品名,
                        数量 = v.数量,
                        売上記号 = v.売上記号,
                        売上単価 = v.売上単価,
                        売上額 = v.売上額,
                        売上掛率 = v.売上掛率,
                        仕入記号 = v.仕入記号,
                        仕入単価 = v.仕入単価,
                        仕入額 = v.仕入額,
                        仕入掛率 = v.仕入掛率,
                        定価 = v.定価,
                    })
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>売上確定一覧の件数を取得する</summary>
        /// <param name="searchItems">検索条件</param>
        /// <returns>件数</returns>
        [HttpPost("view-fixed-sales-count")]
        public async Task<ActionResult<ApiResponse<int>>> GetViewFixedSalesCountAsync(List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await GenSearchUtil.DoGenSearch(_context.ViewFixedSales, searchItems).CountAsync();
            });
        }

        /// <summary>売上確定一覧を検索</summary>
        /// <param name="searchItems">検索条件</param>
        /// <param name="rows">１ページあたりの件数</param>
        /// <param name="page">ページ位置</param>
        /// <returns>検索結果</returns>
        [HttpPost("view-fixed-sales")]
        public async Task<ActionResult<ApiResponse<List<ViewFixedSale>>>>
            GetViewFixedSalesAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewFixedSales, searchItems)
                    .OrderBy(x => x.Hat注文番号);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>売上確定前利率異常一覧を検索</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="uriTanZero">基本検索条件[単価がゼロ円]</param>
        /// <param name="searchItems">検索条件</param>
        /// <param name="rows">１ページあたりの件数</param>
        /// <param name="page">ページ位置</param>
        /// <returns>検索結果</returns>
        [HttpPost("view-interest-rate-before-fix")]
        public async Task<ActionResult<ApiResponse<List<ViewInterestRateBeforeFix>>>>
            GetViewInterestRateBeforeFixesAsync(
                [FromQuery] decimal? profitOver, [FromQuery] decimal? profitUnder,
                [FromQuery] int? suryoOver, [FromQuery] int? suryoUnder,
                [FromQuery] decimal? uriKinOver, [FromQuery] decimal? uriKinUnder,
                [FromQuery] bool uriTanZero,
                [FromBody] List<GenSearchItem> searchItems,
                [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _processService.GetViewInterestRateBeforeFixesQuery(
                    profitOver, profitUnder, suryoOver, suryoUnder, uriKinOver, uriKinUnder, uriTanZero, searchItems);
                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>売上確定後利率異常一覧を検索</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="uriTanZero">基本検索条件[単価がゼロ円]</param>
        /// <param name="searchItems">検索条件</param>
        /// <param name="rows">１ページあたりの件数</param>
        /// <param name="page">ページ位置</param>
        /// <returns>検索結果</returns>
        [HttpPost("view-interest-rate-fixed")]
        public async Task<ActionResult<ApiResponse<List<ViewInterestRateFixed>>>>
            GetViewInterestRateFixedAsync(
                [FromQuery] decimal? profitOver, [FromQuery] decimal? profitUnder,
                [FromQuery] int? suryoOver, [FromQuery] int? suryoUnder,
                [FromQuery] decimal? uriKinOver, [FromQuery] decimal? uriKinUnder,
                [FromQuery] bool uriTanZero,
                [FromBody] List<GenSearchItem> searchItems,
                [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(() =>
            {
                var query = _processService.GetViewInterestRateFixedQuery(
                    profitOver, profitUnder, suryoOver, suryoUnder, uriKinOver, uriKinUnder, uriTanZero, searchItems);
                return GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>物件詳細情報を取得</summary>
        /// <param name="constructionCode">物件コード</param>
        /// <returns>物件詳細情報</returns>
        [HttpGet("view-construction-detail")]
        public async Task<ActionResult<ApiResponse<ViewConstructionDetail>>>
            GetViewConstructionDetailAsync([FromQuery] string constructionCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetViewConstructionDetailQuery(constructionCode)
                    .FirstOrDefaultAsync();
            });
        }

        /// <summary>
        /// 物件詳細情報を更新
        /// </summary>
        /// <param name="updateRequest">更新内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("update-construction-detail")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateConstructionDetailAsync(
            [FromBody] Construction updateRequest)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFUpdateService.UpdateViewConstructionDetailAsync(updateRequest);
            });
        }

        /// <summary>
        /// 物件詳細情報を更新
        /// </summary>
        /// <param name="updateRequest">更新内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("delete-insert-construction-detail-gird")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteInsertConstructionDetailGridAsync(
            [FromBody] List<ConstructionDetail> updateRequest)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFUpdateService.DeleteInsertConstructionDetailGridAsync(updateRequest);
            });
        }

        /// <summary>
        /// 新規物件情報を追加
        /// </summary>
        /// <param name="createRequest">追加内容</param>
        /// <returns>更新結果</returns>
        [HttpPut("add-construction-detail")]
        public async Task<ActionResult<ApiResponse<string>>> AddConstructionDetailAsync(
            [FromBody] Construction createRequest)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFUpdateService.AddViewConstructionDetailAsync(createRequest);
            });
        }

        /// <summary>売上確定前利率異常チェック結果を記録(UPSERT)</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>チェック結果</returns>
        [HttpPut("interest-rate-check-before-fix")]
        public async Task<ActionResult<ApiResponse<List<InterestRateCheckBeforeFixResult>>>> PutInterestRateCheckBeforeFixAsync([FromBody] IEnumerable<InterestRateCheckBeforeFixParameter> parameters)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.PutInterestRateCheckBeforeFixAsync(parameters);
            });
        }

        /// <summary>売上確定後利率異常チェック結果を記録(UPSERT)</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>チェック結果</returns>
        [HttpPut("interest-rate-check-fixed")]
        public async Task<ActionResult<ApiResponse<List<InterestRateCheckFixedResult>>>> PutInterestRateCheckFixedAsync([FromBody] IEnumerable<InterestRateCheckFixedParameter> parameters)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.PutInterestRateCheckFixedAsync(parameters);
            });
        }

        /// <summary>納品一覧表（社内用）チェック結果を記録(UPSERT)</summary>
        /// <param name="parameters">チェック結果を記録する対象</param>
        /// <returns>チェック結果</returns>
        [HttpPut("internal-delivery-check")]
        public async Task<ActionResult<ApiResponse<List<InternalDeliveryCheckResult>>>> PutInternalDeliveryCheckAsync([FromBody] IEnumerable<InternalDeliveryCheckParameter> parameters)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.PutInternalDeliveryCheckAsync(parameters);
            });
        }

        /// <summary>物件コードの重複チェック</summary>
        /// <param name="constructionCode">constructionCode</param>
        /// <returns>重複結果</returns>
        [HttpPost("check-duplicate-construction-code")]
        public Task<ActionResult<ApiResponse<bool>>> CheckDuplicateConstructionCodeAsync(
            string constructionCode)
        {
            return ApiLogicRunner.RunAsync(() =>
            {
                return _hatFSearchService.CheckDuplicateConstructionCodeAsync(constructionCode);
            });
        }

        /// <summary>
        /// 物件詳細ロック
        /// </summary>
        /// <param name="constructionCode">constructionCode</param>
        /// <param name="empid">empid</param>
        /// <returns></returns>
        [HttpPost("construction-lock")]
        public async Task<ActionResult<ApiResponse<Dictionary<string, string>>>> LockAsync(string constructionCode, int empid)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _constructionLockService.LockAsync(constructionCode, empid);
            });
        }

        /// <summary>
        /// 物件詳細アンロック
        /// </summary>
        /// <param name="constructionCode">constructionCode</param>
        /// <returns></returns>
        [HttpPost("construction-unlock")]
        public async Task<ActionResult<ApiResponse<bool>>> UnLockAsync(string constructionCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _constructionLockService.UnLockAsync(constructionCode);
            });
        }

        /// <summary>
        /// 物件詳細の詳細データ取得
        /// </summary>
        /// <param name="constructionCode">物件番号</param>
        /// <returns>検索結果</returns>
        [HttpGet("construction-detail-detail")]
        public async Task<ActionResult<ApiResponse<List<ConstructionDetail>>>> GetConstructionDetailDetailAsync(string constructionCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetConstructionDetailDetailAsync(constructionCode);
            });
        }

        /// <summary>
        /// 仕入金額照合ロック
        /// </summary>
        /// <param name="hatOrderNo">hatOrderNo</param>
        /// <param name="empid">empid</param>
        /// <returns></returns>
        [HttpPost("amount-check-lock")]
        public async Task<ActionResult<ApiResponse<Dictionary<string, string>>>> AmountCheckLockAsync(string hatOrderNo, int empid)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _amountCheckLockService.LockAsync(hatOrderNo, empid);
            });
        }

        /// <summary>
        /// 仕入金額照合アンロック
        /// </summary>
        /// <param name="hatOrderNo">constructionCode</param>
        /// <returns></returns>
        [HttpPost("amount-check-unlock")]
        public async Task<ActionResult<ApiResponse<bool>>> AmountCheckUnLockAsync(string hatOrderNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _amountCheckLockService.UnLockAsync(hatOrderNo);
            });
        }
        /// <summary>
        /// 売上額訂正ロック
        /// </summary>
        /// <param name="hatOrderNo">hatOrderNo</param>
        /// <param name="empid">empid</param>
        /// <returns></returns>
        [HttpPost("sales-edit-lock")]
        public async Task<ActionResult<ApiResponse<Dictionary<string, string>>>> SalesEditLockAsync(string hatOrderNo, int empid)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _salesEditLockService.LockAsync(hatOrderNo, empid);
            });
        }

        /// <summary>
        /// 売上額訂正アンロック
        /// </summary>
        /// <param name="hatOrderNo">constructionCode</param>
        /// <returns></returns>
        [HttpPost("sales-edit-unlock")]
        public async Task<ActionResult<ApiResponse<bool>>> SalesEditUnLockAsync(string hatOrderNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _salesEditLockService.UnLockAsync(hatOrderNo);
            });
        }

        /// <summary>受発注一覧の件数を取得する</summary>
        /// <param name="searchItems">検索条件</param>
        /// <returns>件数</returns>
        [HttpPost("view-orders-count")]
        public async Task<ActionResult<ApiResponse<int>>> GetViewOrdersCountAsync(List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await GenSearchUtil.DoGenSearch(_context.ViewOrders, searchItems).CountAsync();
            });
        }

        /// <summary>受発注一覧を検索</summary>
        /// <param name="searchItems">検索条件</param>
        /// <param name="rows">１ページあたりの件数</param>
        /// <param name="page">ページ位置</param>
        /// <returns>検索結果</returns>
        [HttpPost("view-orders")]
        public async Task<ActionResult<ApiResponse<List<ViewOrder>>>>
            GetOrdersSalesAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_context.ViewOrders, searchItems)
                    .OrderBy(x => x.物件コード);

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>請求詳細を取得</summary>
        /// <param name="companyCode">得意先コード</param>
        /// <returns>請求詳細情報リスト</returns>
        [HttpGet("view-invoice-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewInvoiceDetail>>>>
            GetInvoiceDetailsAsync([FromQuery] string companyCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetInvoiceDetailAsync(companyCode);
            });
        }

        /// <summary>得意先情報を取得</summary>
        /// <param name="companyCode">得意先コード</param>
        /// <returns>請求詳細情報リスト</returns>
        [HttpGet("company-info")]
        public async Task<ActionResult<ApiResponse<CompanysMst>>>
            GetCompanyInfoAsync([FromQuery] string companyCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetCompanyInfoQuery(companyCode)
                    .FirstOrDefaultAsync();
            });
        }

        /// <summary>顧客の入金口座情報の取得</summary>
        /// <param name="companyCode">顧客の得意先コード</param>
        /// <returns>請求詳細情報リスト</returns>
        [HttpGet("invoice-bank")]
        public async Task<ActionResult<ApiResponse<BankAcutMst>>>
            GetInvoiceBankAsync([FromQuery] string companyCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetInvoiceBankQuery(companyCode)
                    .FirstOrDefaultAsync();
            });
        }

        /// <summary>
        /// 社員マスタ検索
        /// </summary>
        [HttpGet("employee")]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetEmployeeAsync([FromQuery] int? employeeId = null, [FromQuery] string empCode = null, [FromQuery] string empName = null, [FromQuery] string empKana = null, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetEmployee(employeeId, empCode, empName, empKana, includeDeleted)
                    .Take(rows);
                return await query.ToListAsync();
            });

        }

        /// <summary>
        /// ユーザー割当権限検索
        /// </summary>
        [HttpGet("user-assigned-role")]
        public async Task<ActionResult<ApiResponse<List<UserAssignedRole>>>> GetUserAssignedRole([FromQuery] int? employeeId = null, [FromQuery] int rows = 200)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetUserAssignedRole(employeeId)
                    .Take(rows);
                return await query.ToListAsync();
            });

        }

        /// <summary>
        /// 権限付き社員検索
        /// </summary>
        [HttpGet("employee-user-assigned-role")]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> GetEmployeeUserAssignedRole([FromQuery] string userRoleCd = null, [FromQuery] int rows = 200)
        {

            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetEmployeeUserAssignedRole(userRoleCd)
                    .Take(rows);
                return await query.ToListAsync();
            });

        }

        /// <summary>
        /// 仕入売上訂正VIEW取得
        /// </summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <returns>検索結果</returns>
        [HttpGet("purchase-sales-correction")]
        public async Task<ActionResult<ApiResponse<List<ViewPurchaseSalesCorrection>>>> GetViewPurchaseSalesCorrectionAsync([FromQuery] string hatOrderNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetPurchaseSalesCorrectionAsync(hatOrderNo);
            });
        }

        /// <summary>与信額チェック</summary>
        /// <param name="tokuiCd">取引先コード</param>
        /// <param name="amount">注文額</param>
        /// <returns>チェック結果</returns>
        [HttpGet("check-credit/{tokuiCd}")]
        public async Task<ActionResult<ApiResponse<CheckCreditResult>>> CheckCreditAsync(string tokuiCd, [FromQuery] decimal amount)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _creditService.CheckCreditAsync(tokuiCd, amount);
            });
        }

        /// <summary>
        /// 返品入力一覧VIEW取得
        /// </summary>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturn>>>> GetViewSalesReturnAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetSalesReturnAsync();
            });
        }

        /// <summary>
        /// 返品入力明細VIEW取得
        /// </summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturnDetail>>>> GetViewSalesReturnDetailAsync([FromQuery] string hatOrderNo, [FromQuery] string denNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetSalesReturnDetailAsync(hatOrderNo, denNo);
            });
        }

        /// <summary>
        /// 返品入庫一覧VIEW取得
        /// </summary>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return-receipt")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturnReceipt>>>> GetViewSalesReturnReceiptAsync()
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetSalesReturnReceiptAsync();
            });
        }

        /// <summary>
        /// 返品入庫明細VIEW取得
        /// </summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-return-receipt-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesReturnReceiptDetail>>>> GetViewSalesReturnReceiptDetailAsync([FromQuery] string hatOrderNo, [FromQuery] string denNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetSalesReturnReceiptDetailAsync(hatOrderNo, denNo);
            });
        }

        /// <summary>
        /// 返品入庫入力完了VIEW取得
        /// </summary>
        /// <param name="hatOrderNo">HAT注文番号</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>検索結果</returns>
        [HttpGet("view-sales-refund-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesRefundDetail>>>> GetViewSalesRefundDetailAsync([FromQuery] string hatOrderNo, [FromQuery] string denNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetSalesRefundDetailAsync(hatOrderNo, denNo);
            });
        }

        /// <summary>
        /// 納品一覧表(訂正・返品)VIEW取得
        /// </summary>
        /// <returns>検索結果</returns>
        [HttpGet("view-correction-delivery")]
        public async Task<ActionResult<ApiResponse<List<ViewCorrectionDelivery>>>> GetViewCorrectionDeliveryAsync([FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetCorrectionDeliveryAsync(fromDate, toDate);
            });
        }

        /// <summary>
        /// 納品明細(訂正・返品)VIEW取得
        /// </summary>
        /// <returns>検索結果</returns>
        [HttpGet("view-correction-delivery-detail")]
        public async Task<ActionResult<ApiResponse<List<ViewCorrectionDeliveryDetail>>>> GetViewCorrectionDeliveryDetailAsync([FromQuery] string compCode, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _processService.GetCorrectionDeliveryDetailAsync(compCode, fromDate, toDate);
            });
        }

        /// <summary>売上調整情報を取得する</summary>
        /// <param name="tokuiCd">得意先コード（必須）</param>
        /// <param name="invoicedDateFrom">請求日（省略可）</param>
        /// <param name="invoicedDateTo">請求日（省略可）</param>
        /// <returns>売上調整情報</returns>
        [HttpGet("sales-adjustment")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesAdjustment>>>> GetSalesAdjustmentsAsync(
            [FromQuery] string tokuiCd, [FromQuery] DateTime? invoicedDateFrom, [FromQuery] DateTime? invoicedDateTo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetSalesAdjustmentsAsync(tokuiCd, invoicedDateFrom, invoicedDateTo);
            });
        }

        /// <summary>売上調整情報を追加/更新する</summary>
        /// <param name="salesAdjustments">売上調整情報</param>
        /// <returns>更新内容を反映したビュー</returns>
        [HttpPut("sales-adjustment")]
        public async Task<ActionResult<ApiResponse<List<ViewSalesAdjustment>>>> PutSalesAdjustmentsAsync(
            [FromBody]List<ViewSalesAdjustment> salesAdjustments)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var updatedList = await _hatFUpdateService.PutSalesAdjustmentsAsync(salesAdjustments);
                return await _hatFSearchService.GetSalesAdjustmentsAsync(
                    updatedList.First().TokuiCd, updatedList.First().InvoicedDate, updatedList.First().InvoicedDate);
            });
        }
        /// <summary>
        /// 得意先（取引先）検索
        /// </summary>
        [HttpGet("companys-mst/")]
        public async Task<ActionResult<ApiResponse<List<CompanysMst>>>> GetCompanysMstAsync([FromQuery] string compCode = null, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetCompanysMst(compCode);
                query = query
                    .Skip((page - 1) * rows)
                    .Take(rows);

                var result = await query.ToListAsync();
                return result;
            });
        }

        /// <summary>
        /// 出荷先（現場）検索
        /// </summary>
        [HttpGet("destinations-mst/")]
        public async Task<ActionResult<ApiResponse<List<DestinationsMst>>>> GetDestinationsMstAsync([FromQuery] string custCode = null, [FromQuery] short? custSubNo = null, [FromQuery] short? distNo = null, [FromQuery] string genbaCode = null, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetDestinationsMst(custCode, custSubNo, distNo, genbaCode);
                query = query
                    .OrderBy(x => x.CustCode)
                    .OrderBy(x => x.CustSubNo)
                    .Skip((page - 1) * rows)
                    .Take(rows);

                var result = await query.ToListAsync();
                return result;
            });
        }


        /// <summary>
        /// 契約単価・販売価格の取得
        /// </summary>
        [HttpGet("k-tanka-sales")]
        public async Task<ActionResult<ApiResponse<List<KTankaSale>>>> GetKTankaSalesAsync([FromQuery] DateTime baseDate, [FromQuery] string prodCode, [FromQuery] string custCode, [FromQuery] string sign, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetKTankaSales(baseDate, prodCode, custCode, sign);
                query = query
                    .OrderBy(x => x.ProdCode)
                    .ThenBy(x => x.CustCode)
                    .ThenBy(x => x.Sign)
                    .ThenBy(x => x.StartDate)
                    .Skip((page - 1) * rows)
                    .Take(rows);

                var result = await query.ToListAsync();
                return result;
            });
        }


        /// <summary>
        /// 契約単価・仕入価格の取得
        /// </summary>
        [HttpGet("k-tanka-purchases")]
        public async Task<ActionResult<ApiResponse<List<KTankaPurchase>>>> GetKTankaPurchases([FromQuery] DateTime baseDate, [FromQuery] string prodCode, [FromQuery] string supCode, [FromQuery] string sign, [FromQuery] int rows = MAX_ROWS, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _hatFSearchService.GetKTankaPurchases(baseDate, prodCode, supCode, sign);
                query = query
                    .OrderBy(x => x.ProdCode)
                    .ThenBy(x => x.SupCode)
                    .ThenBy(x => x.Sign)
                    .ThenBy(x => x.StartDate)
                    .Skip((page - 1) * rows)
                    .Take(rows);

                var result = await query.ToListAsync();
                return result;
            });
        }
    }
}