using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using HAT_F_api.Services.Authentication;
using HAT_F_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

namespace HAT_F_api.Controllers
{
#if !DEBUG
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "MasterEdit")]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class MasterEditorController : ControllerBase
    {
        // 内部処理用
        private class DestinationsCustomers
        {
            public DestinationsMst DestinationsMst { get; set; }
            public CustomersMst CustomersMst { get; set; }
        }

        // 内部処理用
        private class CustomersCompanyesEmployee
        {
            public CustomersMst CustomersMst { get; set; }
            public CompanysMst CompanysMst { get; set; }
            public Employee Employee { get; set; }
        }

        private readonly MasterEditorService _masterEditorService;
        private readonly HatFContext _hatFContext;
        private readonly IConfiguration _configuration;
        private readonly NLog.ILogger _logger;
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;

        /// <summary>検索系サービス</summary>
        private readonly HatFSearchService _hatFSearchService;

        /// <summary>更新系サービス</summary>
        private readonly HatFUpdateService _hatFUpdateService;

        /// <summary>更新用データのCREATE_DATE等セット機能</summary>
        private readonly UpdateInfoSetter _updateInfoSetter;

        public MasterEditorController(
            MasterEditorService masterEditorService,
            HatFContext hatFContext,
            IConfiguration configuration,
            NLog.ILogger logger,
            HatFApiExecutionContext hatFApiExecutionContext,
            HatFSearchService hatFSearchService,
            HatFUpdateService hatFUpdateService,
            UpdateInfoSetter updateInfoSetter
            )
        {
            _masterEditorService = masterEditorService;
            _hatFContext = hatFContext;
            _configuration = configuration;
            _logger = logger;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _hatFSearchService = hatFSearchService;
            _hatFUpdateService = hatFUpdateService;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>
        /// 汎用マスター編集対象テーブルの情報を取得します
        /// </summary>
        /// <returns></returns>
        [HttpGet("master-tables/")]
        public async Task<ActionResult<ApiResponse<List<MasterTable>>>> GetMasterTablesAsync()
        {
            var tables = await _masterEditorService.GetMasterTablesAsync();
            return new ApiOkResponse<List<MasterTable>>(tables);
        }

        /// <summary>
        /// 汎用マスター編集用データをJSON変換したDataTableとして取得します
        /// </summary>
        /// <param name="tableName">対象テーブル名(物理名)</param>
        /// <returns></returns>
        /// <remarks>
        /// 取得結果はNewtonsoft.Json.JsonConvertでDataTableにデシリアイズしてください。
        /// </remarks>
        [HttpGet("master-table/{tableName}")]
        public async Task<ActionResult<ApiResponse<string>>> GetMasterDataTableAsync(string tableName)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var dataTable = await _masterEditorService.GetByDataTableAsync(tableName);
                string jsonString = JsonConvert.SerializeObject(dataTable);
                return jsonString;
            });

            //var dataTable = await _masterEditorService.GetByDataTableAsync(tableName);
            //string jsonString = JsonConvert.SerializeObject(dataTable);
            //return new ApiOkResponse<string>(jsonString);
        }

        /// <summary>
        /// 汎用マスター編集用データ更新
        /// </summary>
        /// <param name="tableName">対象テーブル名(物理名)</param>
        /// <param name="dataTableJson">JSONシリアライズしたDataTable</param>
        /// <returns></returns>
        /// <remarks>
        /// DataTableはNewtonsoft.Json.JsonConvertでシリアイズし、ApiArgumentWrapperを通して渡してください。
        /// </remarks>
        [HttpPut("master-table/{tableName}")]
        public async Task<ActionResult<ApiResponse<int>>> PutMasterDataTableAsync(string tableName, [FromBody] ApiArgumentWrapper dataTableJson)
        {
            //return new ApiErrorResponse<int>(ApiResultType.ApiDbUpdateConcurrencyError, "楽観排他エラー");

            return await ApiLogicRunner.RunAsync(async () =>
            {
                DataTable dataTable = ToDataTable(dataTableJson);
                return await _masterEditorService.UpdateByDataTableAsync(tableName, dataTable);
            });

            //try
            //{
            //    await _masterEditorService.UpdateByDataTableAsync(tableName, dataTable);
            //    return new ApiOkResponse();
            //}
            //catch (HatFApiServiceOptimisticLockingException)
            //{
            //    return new ApiErrorResponse(ApiResultType.ApiDbOptimisticLockingError);
            //}
        }

        private DataTable ToDataTable(ApiArgumentWrapper dataTableJson)
        {
            DataTable dataTable;

            if (dataTableJson.Value is string)
            {
                dataTable = JsonConvert.DeserializeObject<DataTable>((string)dataTableJson.Value);
            }
            else if (dataTableJson.Value is JsonElement)
            {
                string json = dataTableJson.Value.ToString();
                dataTable = JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                throw new InvalidCastException(nameof(dataTableJson));
            }
            return dataTable;
        }

        [HttpPost("companys-mst-gensearch")]
        public async Task<ActionResult<ApiResponse<List<CompanysMst>>>> PostCompanysMstsGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.CompanysMsts, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false))   //削除済を含めるか
                    .OrderBy(x => x.CompCode);  //TODO:ソート修正

                //System.Diagnostics.Debug.WriteLine(query.ToQueryString());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        [HttpPost("companys-mst-count-gensearch")]
        public async Task<ActionResult<ApiResponse<int>>> PostCompanysMstsCountGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.CompanysMsts, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false));   //削除済を含めるか

                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 得意先検索
        /// </summary>
        [HttpGet("companys-mst/{compCode}")]

        public async Task<ActionResult<ApiResponse<List<CompanysMst>>>> GetCompanysMstAsync(string compCode = null, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var result = _masterEditorService.GetCompanysMst(compCode, rows, page);
                return await result.ToListAsync();
            });
        }

        /// <summary>
        /// 得意先保存
        /// </summary>
        [HttpPut("companys-mst/{compCode}")]

        public async Task<ActionResult<ApiResponse<int>>> PutCompanysMstAsync(string compCode, [FromBody] CompanysMst companysMst)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                companysMst.CompCode = compCode;
                int count = await _masterEditorService.PutCompanysMstAsnc(companysMst);
                return count;
            });
        }

        /// <summary>
        /// 仕入先・汎用検索
        /// </summary>
        [HttpPost("supplier-mst-gensearch")]
        public async Task<ActionResult<ApiResponse<List<SupplierMst>>>> PostSupplierMstGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.SupplierMsts, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false))   //削除済を含めるか
                    .OrderBy(x => x.SupCode);  //TODO:ソート修正

                //System.Diagnostics.Debug.WriteLine(query.ToQueryString());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 仕入先・汎用検索（件数）
        /// </summary>
        [HttpPost("supplier-mst-count-gensearch")]
        public async Task<ActionResult<ApiResponse<int>>> PostSupplierMstCountGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.SupplierMsts, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false));   //削除済を含めるか
                return await query.CountAsync();
            });
        }

        /// <summary>仕入先情報取得</summary>
        /// <param name="supCode">仕入先コード</param>
        /// <returns>仕入先情報</returns>
        [HttpGet("supplier/{supCode}/{supSubNo}")]
        public async Task<ActionResult<ApiResponse<SupplierMst>>> GetSupplierAsync(string supCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFSearchService.GetSupplierAsync(supCode);
            });
        }

        /// <summary>仕入先登録</summary>
        /// <param name="supCode">仕入先コード</param>
        /// <param name="supplier">仕入先情報</param>
        /// <returns>仕入先情報</returns>
        [HttpPut("supplier/{supCode}/{supSubNo}")]
        public async Task<ActionResult<ApiResponse<SupplierMst>>> PutSupplierAsync(string supCode, [FromBody] SupplierMst supplier)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                supplier.SupCode = supCode;
                //_updateInfoSetter.SetUpdateInfo(supplier);
                return await _hatFUpdateService.UpsertSupplierAsync(supplier);
            });
        }

        private IQueryable<CustomersCompanyesEmployee> AddJoinToCustomersMstForDisplayName(IQueryable<CustomersMst> query)
        {
            // 得意先（取引先）、社員マスタを連結、

            var newQuery = query.GroupJoin(_hatFContext.Employees,
                        cust => cust.EmpCode,
                        emp => emp.EmpCode,
                        (cust, emp) => new { CustomersMst = cust, Employee = emp }
                    )
                    .SelectMany(
                        x => x.Employee.DefaultIfEmpty(),
                        (x, emp) => new
                        {
                            CustomersMst = x.CustomersMst,
                            Employee = emp
                        }
                    )
                    .GroupJoin(_hatFContext.CompanysMsts,
                        custEmp => custEmp.CustomersMst.ArCode,
                        comp => comp.CompCode,
                        (custEmp, comp) => new { CustomersMst = custEmp.CustomersMst, Employee = custEmp.Employee, CompanysMst = comp }
                    )
                    .SelectMany(
                        x => x.CompanysMst.DefaultIfEmpty(),
                        (x, comp) => new
                        {
                            CustomersMst = x.CustomersMst,
                            Employee = x.Employee,
                            CompanysMst = comp
                        }
                    )
                    .OrderBy(x => x.CustomersMst.CustCode)
                    .Select(x => new CustomersCompanyesEmployee() { CustomersMst = x.CustomersMst, Employee = x.Employee, CompanysMst = x.CompanysMst });

            return newQuery;
        }

        /// <summary>
        /// 顧客汎用検索
        /// </summary>
        [HttpPost("customers-mst-gensearch")]
        public async Task<ActionResult<ApiResponse<List<CustomersMstEx>>>> PostCustomersMstGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CustomersMst, CustomersMstEx>();
                });
                var mapper = config.CreateMapper();

                var query = GenSearchUtil.DoGenSearch(_hatFContext.CustomersMsts, searchItems);
                var joinedQuery = AddJoinToCustomersMstForDisplayName(query);

                joinedQuery = GenSearchUtil.AddPaging(joinedQuery, rows, page);

                System.Diagnostics.Debug.WriteLine(joinedQuery.ToQueryString());

                // 名称列付オブジェクトにコピーして返す
                var src = await joinedQuery.ToListAsync();
                var dest = new List<CustomersMstEx>(src.Count);
                foreach (var srcItem in src)
                {
                    var destItem = mapper.Map<CustomersMstEx>(srcItem.CustomersMst);
                    destItem.ArName = srcItem.CompanysMst?.CompName ?? "";
                    destItem.EmpName = srcItem.Employee?.EmpName ?? "";
                    dest.Add(destItem);
                }

                return dest;
                //return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 顧客汎用検索（件数）
        /// </summary>
        [HttpPost("customers-mst-count-gensearch")]
        public async Task<ActionResult<ApiResponse<int>>> PostCustomersMstCountGenSearchAsync([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.CustomersMsts, searchItems);
                return await query.CountAsync();
            });
        }


        /// <summary>
        /// 顧客検索
        /// </summary>
        [HttpGet("customers-mst/{custCode:?}")]
        public async Task<ActionResult<ApiResponse<List<CustomersMstEx>>>> GetCustomersMstAsync(string custCode = null, [FromQuery] string custName = null, [FromQuery] string custKana = null, [FromQuery] string custUserName = null, [FromQuery] string custUserDepName = null, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CustomersMst, CustomersMstEx>();
                });
                var mapper = config.CreateMapper();

                var query = _masterEditorService
                    .GetCustomersMst(custCode, custName, custKana, custUserName, custUserDepName, includeDeleted, rows);

                // 名称列追加のためテーブル追加
                var joinedQuery = AddJoinToCustomersMstForDisplayName(query);

                // 名称列付オブジェクトにコピーして返す
                var src = await joinedQuery.ToListAsync();
                var dest = new List<CustomersMstEx>(src.Count);
                foreach (var srcItem in src)
                {
                    var destItem = mapper.Map<CustomersMstEx>(srcItem.CustomersMst);
                    destItem.ArName = srcItem.CompanysMst?.CompName ?? "";
                    destItem.EmpName = srcItem.Employee?.EmpName ?? "";
                    dest.Add(destItem);
                }

                return dest;
            });
        }

        /// <summary>
        /// 顧客の保存
        /// </summary>
        [HttpPut("customers-mst")]
        public async Task<ActionResult<ApiResponse<int>>> PutCustomersMstAsync([FromBody] List<CustomersMst> customersMsts)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                _updateInfoSetter.SetUpdateInfo(customersMsts);
                return await _masterEditorService.PutCustomersMst(customersMsts);
            });
        }

        private IQueryable<DestinationsCustomers> AddJoinToDestinationsMstForDisplayName(IQueryable<DestinationsMst> query)
        {
            // 得意先（取引先）を連結
            var newQuery = query
                    .GroupJoin(_hatFContext.CustomersMsts,
                        dest => dest.CustCode,
                        cust => cust.CustCode,
                        (dest, cust) => new { DestinationsMst = dest, CustomersMst = cust }
                    )
                    .SelectMany(
                        x => x.CustomersMst.DefaultIfEmpty(),
                        (x, cust) => new
                        {
                            DestinationsMst = x.DestinationsMst,
                            CustomersMst = cust
                        }
                    )
                    .OrderBy(x => x.DestinationsMst.CustCode)
                    .Select(x => new DestinationsCustomers { DestinationsMst = x.DestinationsMst, CustomersMst = x.CustomersMst });

            return newQuery;
        }



        /// <summary>
        /// 出荷先（現場）汎用検索
        /// </summary>
        [HttpPost("destinations-mst-gensearch")]
        public async Task<ActionResult<ApiResponse<List<DestinationsMstEx>>>> PostDestinationsMstGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DestinationsMst, DestinationsMstEx>();
                });
                var mapper = config.CreateMapper();

                var query = GenSearchUtil.DoGenSearch(_hatFContext.DestinationsMsts, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false))   //削除済を含めるか
                    .OrderBy(x => x.CustCode);
                var pagingQuery = GenSearchUtil.AddPaging(query, rows, page);

                // 名称列追加のための連結
                var joinedQuery = AddJoinToDestinationsMstForDisplayName(pagingQuery);

                // 名称列付オブジェクトにコピーして返す
                var src = await joinedQuery.ToListAsync();
                var dest = new List<DestinationsMstEx>(src.Count);
                foreach (var srcItem in src)
                {
                    var destItem = mapper.Map<DestinationsMstEx>(srcItem.DestinationsMst);
                    destItem.CustName = srcItem.CustomersMst?.CustName ?? "";
                    dest.Add(destItem);
                }

                return dest;
            });
        }

        /// <summary>
        ///出荷先（現場）汎用検索（件数）
        /// </summary>
        [HttpPost("destinations-mst-count-gensearch")]
        public async Task<ActionResult<ApiResponse<int>>> PostDestinationsMstCountGenSearchAsync([FromBody] List<GenSearchItem> searchItems)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.DestinationsMsts, searchItems);
                return await query.CountAsync();
            });
        }

        /// <summary>
        ///出荷先（現場）検索
        /// </summary>
        [HttpGet("destinations-mst/{custCode:?}/{genbaCode:?}")]
        public async Task<ActionResult<ApiResponse<List<DestinationsMstEx>>>> GetDestinationsMstAsync(string custCode = null, string genbaCode = null, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                //var query = _masterEditorService.GetDestinationsMst(custCode, custSubNo, distNo, genbaCode, rows, page);
                //return await query.ToListAsync();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DestinationsMst, DestinationsMstEx>();
                });
                var mapper = config.CreateMapper();

                var query = _masterEditorService.GetDestinationsMst(custCode, genbaCode, rows, page);
                var joinedQuery = AddJoinToDestinationsMstForDisplayName(query);

                // 名称列付オブジェクトにコピーして返す
                var src = await joinedQuery.ToListAsync();
                var dest = new List<DestinationsMstEx>(src.Count);
                foreach (var srcItem in src)
                {
                    var destItem = mapper.Map<DestinationsMstEx>(srcItem.DestinationsMst);
                    destItem.CustName = srcItem.CustomersMst?.CustName ?? "";
                    dest.Add(destItem);
                }

                return dest;
            });
        }

        /// <summary>
        ///出荷先（現場）更新
        /// </summary>
        [HttpPut("destinations-mst")]
        public async Task<ActionResult<ApiResponse<int>>> PutDestinationsMstAsync([FromBody] DestinationsMst destinationsMst)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                //_updateInfoSetter.SetUpdateInfo(destinationsMst);
                var count = await _masterEditorService.PutDestinationsMst(destinationsMst);
                return count;
            });
        }

        /// <summary>
        /// 社員マスタ・汎用検索用
        /// </summary>
        [HttpPost("employees-gensearch")]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> PostEmployeesGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false, [FromQuery] int rows = 200, [FromQuery] int page = 1)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.Employees, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false))   //削除済を含めるか
                    .OrderBy(x => x.EmpCode);

                //System.Diagnostics.Debug.WriteLine(query.ToQueryString());

                return await GenSearchUtil.AddPaging(query, rows, page).ToListAsync();
            });
        }

        /// <summary>
        /// 社員マスタ・汎用検索件数用
        /// </summary>
        [HttpPost("employees-count-gensearch")]
        public async Task<ActionResult<ApiResponse<int>>> PostEmployeesCountGenSearchAsync([FromBody] List<GenSearchItem> searchItems, [FromQuery] bool includeDeleted = false)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                var query = GenSearchUtil.DoGenSearch(_hatFContext.Employees, searchItems)
                    .Select(x => x) // 前の条件全体と下の条件をANDしたい
                    .Where(x => includeDeleted || (includeDeleted == false && x.Deleted == false));   //削除済を含めるか
                return await query.CountAsync();
            });
        }

        /// <summary>
        /// 社員役割（ロール）保存
        /// </summary>
        [HttpPut("employee-user-assigned-role")]
        public async Task<ActionResult<ApiResponse<int>>> PutEmployeeUserAssignedRoleAsync([FromBody] EmployeeUserAssignedRole employeeUserAssignedRole)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                //_updateInfoSetter.SetUpdateInfo(employeeUserAssignedRole.Employee);

                var count = await _masterEditorService.PutEmployeeUserAssignedRoleAsync(employeeUserAssignedRole);
                return count;
            });
        }

        /// <summary>
        /// 役職別デフォルト役割（ロール）取得用
        /// </summary>
        [HttpGet("title-default-role")]
        public async Task<ActionResult<ApiResponse<List<TitleDefaultRole>>>> GetTitleDefaultRoleAsync([FromQuery] string titleCode)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                // 検索条件の付与などなく全件返却なので一旦サービスクラス省略
                var query = _masterEditorService.GetTitleDefaultRoleAsync(titleCode);
                return await query.ToListAsync();
            });
        }

        /// <summary>
        /// DIV_USER_ROLE取得用
        /// </summary>
        [HttpGet("div-user-role/")]
        public async Task<ActionResult<ApiResponse<List<DivUserRole>>>> GetDivUserRoleAsync([FromQuery] bool includeDeleted = false)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _masterEditorService.GetDivUserRole(includeDeleted).ToListAsync();
            });
        }

        /// <summary>
        /// DEPT_MST取得用
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("dept-mst/")]
        public async Task<ActionResult<ApiResponse<List<DeptMst>>>> GetDeptMstAsync([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    DateTime dateTime = _hatFApiExecutionContext.ExecuteDateTimeJst.Date;

                    return await _masterEditorService.GetDeptMst(dateTime, dateTime).ToListAsync();
                }
                else
                {
                    return await _masterEditorService.GetDeptMst(startDate, endDate).ToListAsync();
                }
            });
        }
    }
}