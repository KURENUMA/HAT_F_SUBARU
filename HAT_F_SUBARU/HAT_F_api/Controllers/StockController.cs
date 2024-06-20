using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using HAT_F_api.Services.Authentication;
using HAT_F_api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly UpdateInfoSetter _updateInfoSetter;
        private readonly NLog.ILogger _logger;
        private readonly HatFContext _context;
        //private readonly SequenceNumberService _sequenceNumberService;
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;
        private readonly StockService _stockService;

        public StockController(
            HatFContext context,
            NLog.ILogger logger,
            HatFApiExecutionContext hatFApiExecutionContext,
            StockService stockService,
            //SequenceNumberService sequenceNumberService,
            UpdateInfoSetter updateInfoSetter
            )
        {
            _context = context;
            _logger = logger;
            //            _sequenceNumberService = sequenceNumberService;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _stockService = stockService;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>
        /// 商品有効在庫数取得
        /// </summary>
        /// <param name="whCode">倉庫CD</param>
        /// <param name="prodCode">商品コード</param>
        /// <returns>在庫数</returns>
        [HttpGet("product-stock-valid-count/{whCode}/{prodCode}")]
        public async Task<ActionResult<ApiResponse<short?>>> GetProductStockValidCount(string whCode, string prodCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var validCount = await  _stockService.GetProductStockValidCountAsync(whCode, prodCode);
                return validCount;
            });
        }

        /// <summary>
        /// 商品の引当
        /// </summary>
        /// <param name="whCode">倉庫CD</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="quantity">引当数</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>STOCKテーブルの更新件数</returns>
        [HttpPost("product-stock-reserve/{whCode}/{prodCode}")]
        public async Task<ActionResult<ApiResponse<int>>> PostProductStockReserveAsync(string whCode, string prodCode, [FromQuery] short quantity, [FromQuery] string denNo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                using var tran = await _context.Database.BeginTransactionAsync();
                var reservedCount = await _stockService.PutProductStockReserveAsync(whCode, prodCode, quantity, denNo);
                if(reservedCount <= 0)
                {
                    await tran.CommitAsync();
                }
                return reservedCount;
            });
        }

        /// <summary>
        /// 在庫データ棚卸テーブル保存
        /// </summary>

        [HttpPut("stock-inventories")]
        public async Task<ActionResult<ApiResponse<int>>> PutStockInventoriesAsync([FromBody] List<StockInventory> data)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _stockService.PutStockInventoriesAsync(data);
            });
        }

        /// <summary>
        /// 在庫データ棚卸テーブル取得
        /// </summary>
        /// <param name="filterCode">0:全件, 1:差異あり, 2:差異なし, 3:異常値(分類), 4:異常値(商品)</param>
        /// <param name="inventoryYearMonth">棚卸年月</param>
        /// <param name="whCode"></param>
        /// <param name="prodCode"></param>
        /// <param name="stockType"></param>
        /// <param name="qualityType"></param>
        /// <param name="rows"></param>
        /// <param name="pageFrom1"></param>
        /// <returns></returns>
        [HttpGet("view-stock-inventories")]

        public async Task<ActionResult<ApiResponse<List<ViewStockInventory>>>> GetViewStockInventoriesAsync([FromQuery] DateTime? inventoryYearMonth = null, [FromQuery] string filterCode = null, [FromQuery] string whCode = null, [FromQuery] string prodCode = null, [FromQuery] string stockType = null, [FromQuery] string qualityType = null, [FromQuery] int rows = 200, [FromQuery] int pageFrom1 = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 商品在庫取得
                var result = _stockService.GetViewStockInventories(inventoryYearMonth, filterCode, whCode, prodCode, stockType, qualityType);
                result = result.Skip(rows * (pageFrom1 - 1)).Take(rows);
                return await result.ToListAsync();
            });
        }

        /// <summary>
        /// 在庫データ棚卸テーブル取得・件数
        /// </summary>
        /// <param name="filterCode">0:全件, 1:差異あり, 2:差異なし, 3:異常値(分類), 4:異常値(商品)</param>
        /// <param name="inventoryYearMonth">棚卸年月</param>
        /// <param name="whCode"></param>
        /// <param name="prodCode"></param>
        /// <param name="stockType"></param>
        /// <param name="qualityType"></param>
        /// <returns></returns>
        [HttpGet("view-stock-inventories-count")]

        public async Task<ActionResult<ApiResponse<int>>> GetViewStockInventoriesCountAsync([FromQuery] DateTime? inventoryYearMonth = null, [FromQuery] string filterCode = null, [FromQuery] string whCode = null, [FromQuery] string prodCode = null, [FromQuery] string stockType = null, [FromQuery] string qualityType = null)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 商品在庫取得
                var result = _stockService.GetViewStockInventories(inventoryYearMonth, filterCode, whCode, prodCode, stockType, qualityType);
                return await result.CountAsync();
            });
        }

        /// <summary>
        /// 棚卸用データを作成する（棚卸用在庫数の確定）
        /// </summary>
        [HttpPut("stock-inventories/new")]
        public async Task<ActionResult<ApiResponse<int>>> PutStockInventoriesNewAsync([FromQuery] string whCode, [FromQuery] DateTime inventoryYearMonth)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 商品在庫取得
                var result = await _stockService.PutStockInventoriesNewAsync(whCode, inventoryYearMonth);
                return result;
            });
        }

        /// <summary>
        /// Amazon棚卸情報を在庫データ棚卸テーブルに登録します
        /// </summary>
        [HttpPut("stock-inventory-amazons")]
        public async Task<ActionResult<ApiResponse<List<StockInventoryAmazon>>>> PutStockInventoryAmazonsAsync([FromQuery]string whCode, [FromQuery] DateTime inventoryYearMonth, [FromBody]List<StockInventoryAmazon> inventories, [FromQuery] bool prodCodeExistsCheckOnly)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // Amazon棚卸の反映
                var result = await _stockService.PutStockInventoryAmazonsAsync(whCode, inventoryYearMonth, inventories, prodCodeExistsCheckOnly);
                return result;
            });
        }


        /// <summary>
        /// 在庫補充情報の取得
        /// </summary>
        [HttpGet("stock-refill/")]
        public async Task<ActionResult<ApiResponse<List<StockRefill>>>> GetStockRefillAsync([FromQuery] string whCode, [FromQuery] string prodCode, [FromQuery] int rows = 200, [FromQuery] int pageFrom1 = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _stockService.GetStockRefill(whCode, prodCode);
                query = query.Skip(rows * (pageFrom1 - 1)).Take(rows);
                return await query.ToListAsync();
            });           
        }

        /// <summary>
        /// 在庫補充情報件数の取得
        /// </summary>
        [HttpGet("stock-refill-count/")]
        public async Task<ActionResult<ApiResponse<int>>> GetStockRefillCountAsync([FromQuery] string whCode, [FromQuery] string prodCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _stockService.GetStockRefill(whCode, prodCode);
                return await query.CountAsync();
            });
        }

        //------------


        /// <summary>
        /// 要発注商品
        /// </summary>
        /// <param name="whCode">倉庫コード</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="excludeeOrdered">発注済を除外</param>
        /// <param name="rows"></param>
        /// <param name="pageFrom1"></param>
        /// <returns></returns>
        [HttpGet("view-stock-refill/")]
        public async Task<ActionResult<ApiResponse<List<ViewStockRefill>>>> GetViewStockRefillAsync([FromQuery] string whCode, [FromQuery] string prodCode, [FromQuery] bool excludeeOrdered = false , [FromQuery] int rows = 200, [FromQuery] int pageFrom1 = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _stockService.GetViewStockRefill(whCode, prodCode, excludeeOrdered);
                query = query.Skip(rows * (pageFrom1 - 1)).Take(rows);
                return await query.ToListAsync();
            });
        }

        /// <summary>
        /// 要発注商品
        /// </summary>
        /// <param name="whCode">倉庫コード</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="excludeeOrdered">発注済を除外</param>
        [HttpGet("view-stock-refill-count/")]
        public async Task<ActionResult<ApiResponse<int>>> GetViewStockRefillCountAsync([FromQuery] string whCode, [FromQuery] string prodCode, [FromQuery] bool excludeeOrdered = false)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = _stockService.GetViewStockRefill(whCode, prodCode, excludeeOrdered);
                return await query.CountAsync();
            });
        }


        //-----------------

        /// <summary>
        /// 在庫補充情報テーブルの保存
        /// </summary>
        [HttpPut("stock-refill")]
        public async Task<ActionResult<ApiResponse<int>>> PutStockRefillAsync([FromBody] IEnumerable<StockRefill> stockRefills)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var result = await _stockService.PutStockRefillAsync(stockRefills);
                return result;
            });
        }


        [HttpGet("com-syohin-mst/{prodCode}")]
        public async Task<ActionResult<ApiResponse<ComSyohinMst>>> GetComSyohinMstByProdCodeAsync(string prodCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var result = await _stockService.GetComSyohinMstByProdCode(prodCode).SingleOrDefaultAsync();
                return result;
            });
        }
    }
}
