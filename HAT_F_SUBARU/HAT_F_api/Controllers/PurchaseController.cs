using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace HAT_F_api.Controllers
{
    /// <summary>仕入関連</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _context;

        /// <summary>連番サービス</summary>
        private readonly SequenceNumberService _sequenceNumberService;

        /// <summary>実行時情報コンテキスト</summary>
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;

        /// <summary>仕入関連サービス</summary>
        private readonly PurchaseService _purchaseService;

        // デフォルト最大返却件数
        private const int MAX_ROWS = 200;

        /// <summary>コンストラクタ</summary>
        /// <param name="context">DBコンテキスト</param>
        /// <param name="hatFApiExecutionContext">実行時情報コンテキスト</param>
        /// <param name="sequenceNumberService">連番サービス</param>
        /// <param name="purchaseService">仕入関連サービス</param>
        public PurchaseController(HatFContext context,
            HatFApiExecutionContext hatFApiExecutionContext,
            SequenceNumberService sequenceNumberService,
            PurchaseService purchaseService)
        {
            _context = context;
            _sequenceNumberService = sequenceNumberService;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _purchaseService = purchaseService;
        }

        /// <summary>仕入取込データを取得</summary>
        /// <param name="supCode">仕入先コード（必須）</param>
        /// <param name="hatOrderNo">Hat注文番号（省略可）</param>
        /// <param name="noubiFrom">納日（省略可）</param>
        /// <param name="noubiTo">納日（省略可）</param>
        /// <returns>仕入取込データ</returns>
        [HttpGet("pu-import")]
        public async Task<ActionResult<ApiResponse<List<PuImport>>>> GetPuImportAsync(
            [FromQuery] string supCode, [FromQuery] string hatOrderNo, [FromQuery] DateTime? noubiFrom, [FromQuery] DateTime? noubiTo)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _purchaseService.GetPuImportAsync(supCode, hatOrderNo, noubiFrom, noubiTo);
            });
        }

        /// <summary>仕入取込データを更新する</summary>
        /// <param name="puImports">仕入取込データ</param>
        /// <returns>追加されたレコード数</returns>
        [HttpPut("pu-import")]
        public async Task<ActionResult<ApiResponse<int>>> PutPuImportAsync([FromBody] List<PuImport> puImports)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _purchaseService.PutPuImportAsync(puImports);
            });
        }
    }
}