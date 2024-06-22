using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using JWT.Algorithms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace HAT_F_api.Services
{
    public class MasterEditorService
    {
        private readonly HatFContext _hatFContext;
        private readonly IConfiguration _configuration;
        private readonly NLog.ILogger _logger;
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;
        private readonly UpdateInfoSetter _updateInfoSetter;


        public MasterEditorService(HatFContext hatFContext, IConfiguration configuration, NLog.ILogger logger, HatFApiExecutionContext hatFApiExecutionContext, UpdateInfoSetter updateInfoSetter)
        {
            _hatFContext = hatFContext;
            _configuration = configuration;
            _logger = logger;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>
        /// 汎用マスター編集の対象となるテーブルの情報を取得します
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 実体はmasterEditTables.jsonで定義した情報の取得です
        /// </remarks>
        public async Task<List<MasterTable>> GetMasterTablesAsync()
        {
            string json = await File.ReadAllTextAsync("masterEditTables.json");

            // snake_case の JSON を CamelCase の .NET プロパティにマップしたい
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
            var masterEditTables = JsonSerializer.Deserialize<List<MasterTable>>(json, options);

            return masterEditTables;
        }

        public async Task<DataTable> GetByDataTableAsync(string tableName)
        {
            if (!await IsValidTableAsync(tableName))
            {
                throw new HatFApiServiceException($"汎用マスター編集対象外テーブルが指定されました: {tableName}");
            }

            // ORDER BY 1 は 1列目でソート
            string sql = $"SELECT * FROM {tableName} ORDER BY 1";

            using var con = _hatFContext.Database.GetDbConnection() as SqlConnection;
            using var cmd = new SqlCommand(sql, con);
            using var adapter = new SqlDataAdapter(cmd);

            var table = new DataTable();

            await con.OpenAsync();
            adapter.Fill(table);
            await con.CloseAsync();

            table.TableName = tableName;
            table.AcceptChanges();

            return table;
        }

        public async Task<int> UpdateByDataTableAsync(string tableName, DataTable dataTable)
        {
            if (!await IsValidTableAsync(tableName))
            {
                throw new HatFApiServiceException($"汎用マスター編集対象外テーブルが指定されました: {tableName}");
            }

            var tables = await GetMasterTablesAsync();
            var tableInfo = tables.Where(item => item.Name == tableName).Single();

            //int pkCount = 1;
            var existsConditions = new StringBuilder();
            foreach (MasterTableColumn col in tableInfo.Columns.Where(item => item.PrimaryKey))
            {
                if (existsConditions.Length > 0) { existsConditions.Append(" AND "); }

                //existsConditions.Append($"[{col.Name}] = @pk{pkCount++}");
                string paramName = "@" + col.Name;
                existsConditions.Append($"[{col.Name}] = {paramName}");
                System.Diagnostics.Debug.WriteLine($"PK:{paramName}");
            }

            if (0 == existsConditions.Length) 
            {
                string msg = $"該当テーブルはmasterEditTables.jsonでPKが定義されていません: {tableName}";
                throw new HatFApiServiceException(msg);
            }

            // SQL の動的パラメータ
            List<SqlParameter> sqlParameters = new List<SqlParameter>();


            // INSERT文の部分を作成
            var insertCols = new StringBuilder();
            var insertValues = new StringBuilder();
            foreach (DataColumn col in dataTable.Columns)
            {
                if (insertCols.Length > 0) { insertCols.Append(", "); }
                insertCols.Append($"[{col.ColumnName}]");

                string paramName = "@" + col.ColumnName;
                if (insertValues.Length > 0) { insertValues.Append(", "); }
                insertValues.Append(paramName);

                if (!sqlParameters.Any(item => item.ParameterName == paramName))
                {
                    sqlParameters.Add(new SqlParameter(paramName, col.DataType));
                    System.Diagnostics.Debug.WriteLine($"ADD INS COL:{paramName}");
                }
            }


            // UPDATE文の部分を作成(作成日、作成者は更新しない)
            List<string> exclusionColumns = new List<string> { "CREATE_DATE", "CREATOR" };
            var updateCols = new StringBuilder();
            foreach (DataColumn col in dataTable.Columns)
            {
                if (exclusionColumns.Contains(col.ColumnName.ToUpper())) { continue; }

                string paramName = "@" + col.ColumnName;
                if (updateCols.Length > 0) { updateCols.Append(", "); }
                updateCols.Append($"[{col.ColumnName}] = {paramName}");

                if (!sqlParameters.Any(item => item.ParameterName == paramName))
                {
                    sqlParameters.Add(new SqlParameter(paramName, col.DataType));
                    System.Diagnostics.Debug.WriteLine($"ADD UPD COL:{paramName}");
                }
            }

            // 更新条件(PK一致更新)
            var updateConditions = new StringBuilder();
            updateConditions.Append(existsConditions);
            updateConditions.Append(" AND UPDATE_DATE = @__optimistic_exclusion_lock_datetime__");  // 更新の楽観的排他ロック用


            var sql = new StringBuilder();
            sql.Append(@$"
                DECLARE @recCount as INT
                SELECT @recCount = COUNT(*) FROM {tableName} WHERE {existsConditions}
                IF @recCount = 0
                    INSERT INTO {tableName} ({insertCols})
                    VALUES ({insertValues})
                ELSE
                    UPDATE {tableName}
                    SET {updateCols}
                    WHERE {updateConditions}
            ");


            using var sqlConnection = _hatFContext.Database.GetDbConnection() as SqlConnection;
            await sqlConnection.OpenAsync();

            using var sqlTransaction = sqlConnection.BeginTransaction();
            using var sqlCommand = new SqlCommand(sql.ToString(), sqlConnection, sqlTransaction);
            sqlCommand.Parameters.AddRange(sqlParameters.ToArray());
            sqlCommand.Parameters.Add("@__optimistic_exclusion_lock_datetime__", SqlDbType.DateTime);

            // 更新者、更新日時
            int empCode = _updateInfoSetter.EntityUpdaterId;
            DateTime recordUpdateDateTime = _updateInfoSetter.EntityUpdateDateTimeJst;

            int totalUpdatedRowsCount = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                // 更新の楽観的排他ロック用
                sqlCommand.Parameters["@__optimistic_exclusion_lock_datetime__"].Value = row["UPDATE_DATE"];

                row["CREATE_DATE"] = recordUpdateDateTime;
                row["CREATOR"] = empCode;
                row["UPDATE_DATE"] = recordUpdateDateTime;
                row["UPDATER"] = empCode;

                foreach (DataColumn col in dataTable.Columns)
                {
                    object val = GetNoNullValue(row, col);
                    sqlCommand.Parameters["@" + col.ColumnName].Value = val;
                }

                int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();
                if (0 == rowsAffected)
                {
                    string message = $"マスター編集で楽観的排他ロック エラーが発生しました: {tableName}";
                    _logger.Info(message);

                    // 楽観的排他ロック エラー
                    throw new DbUpdateConcurrencyException();
                }
                else if (2 <= rowsAffected)
                {
                    throw new HatFApiServiceException("更新SQLに不具合(WHERE句にバグ)");
                }

                totalUpdatedRowsCount += rowsAffected;
            }

            await sqlTransaction.CommitAsync();
            await sqlConnection.CloseAsync();

            return totalUpdatedRowsCount;
        }

        private object GetNoNullValue(DataRow row, DataColumn dataColumn)
        {
            object val = row[dataColumn.ColumnName];

            if (val == DBNull.Value)
            {
                if (dataColumn.DataType == typeof(string))
                {
                    val = "";
                }
                else if (dataColumn.DataType == typeof(DateTime) || dataColumn.DataType == typeof(DateOnly))
                {
                    val = DateTime.MinValue;
                }
                else
                {
                    // 数値系?
                    val = 0;
                }
            }

            return val;
        }

        private async Task<bool> IsValidTableAsync(string tableName)
        {
            var tables = await GetMasterTablesAsync();

            bool isValid = tables.Where(item => item.Name == tableName).Any();
            return isValid;
        }

        /// <summary>
        /// 得意先取得用
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompanysMst> GetCompanysMst(string compCode, int rows, int page)
        {
            return _hatFContext.CompanysMsts
                .Where(x=> string.IsNullOrEmpty(compCode) || x.CompCode == compCode)
                .Skip(rows * (page - 1)).Take(rows);
        }

        public async Task<int> PutCompanysMstAsnc(CompanysMst companysMst)
        {
            _updateInfoSetter.SetUpdateInfo(companysMst);

            var query = _hatFContext.CompanysMsts.Where(x => x.CompCode == companysMst.CompCode);
            var existedCampanysMst = await query.SingleOrDefaultAsync();
            if (existedCampanysMst == null)
            {
                _hatFContext.CompanysMsts.Add(companysMst);
            }
            else
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CompanysMst, CompanysMst>();
                });
                var mapper = config.CreateMapper();
                mapper.Map(companysMst, existedCampanysMst);
            }

            int count = await _hatFContext.SaveChangesAsync();
            return count;
        }

        /// <summary>
        /// 社員マスタ＋役割（ロール）一括更新
        /// </summary>
        public async Task<int> PutEmployeeUserAssignedRoleAsync(EmployeeUserAssignedRole employeeUserAssignedRole)
        {
            _updateInfoSetter.SetUpdateInfo(employeeUserAssignedRole.Employee);

            var sourceEmployee = employeeUserAssignedRole.Employee;
            var sourceRoles = employeeUserAssignedRole.UserAssignedRoles;

            // トランザクション開始
            using var tran = await _hatFContext.Database.BeginTransactionAsync();

            var employee = await _hatFContext.Employees.Where(x => x.EmpId == sourceEmployee.EmpId).SingleOrDefaultAsync();
            if (employee != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Employee, Employee>();
                });
                var mapper = config.CreateMapper();
                mapper.Map(sourceEmployee, employee);
            }
            else 
            {
                _hatFContext.Employees.Add(sourceEmployee);
            }

            // ★割り当てロールの付け直し
            _ = await _hatFContext.UserAssignedRoles.Where(x => x.EmpId == sourceEmployee.EmpId).ExecuteDeleteAsync();
            foreach (var role in sourceRoles)
            {
                await _hatFContext.UserAssignedRoles.AddAsync(role);
            }

            // DB更新実行
            int affectedCount = await _hatFContext.SaveChangesAsync();

            // 社員番号重複チェック
            int sameEmpCodeCount = await _hatFContext.Employees
                .Where(x => x.EmpCode == sourceEmployee.EmpCode && x.Deleted == false)
                .CountAsync();
            if (sameEmpCodeCount > 1)
            {
                // 削除以外の同一社員番号が複数になっていたらエラー
                await tran.RollbackAsync();
                throw new DbUpdateConcurrencyException();
            }

            await tran.CommitAsync();

            return affectedCount;
        }

        /// <summary>
        /// 役職別デフォルト役割（ロール）取得用
        /// </summary>
        public IQueryable<TitleDefaultRole> GetTitleDefaultRoleAsync(string titleCode)
        {
            var query = _hatFContext.TitleDefaultRoles
                .Where(x => string.IsNullOrEmpty(titleCode) || x.TitleCode == titleCode);
            return query;
        }

        /// <summary>
        /// DIV_USER_ROLE取得用
        /// </summary>
        public IQueryable<DivUserRole> GetDivUserRole(bool includeDeleted)
        {
            var query = _hatFContext.DivUserRoles
                .Where(x => includeDeleted || x.Deleted == false)   //削除済を含めるか
                .OrderBy(x => x.UserRoleCd);
            return query;
        }

        /// <summary>
        /// DEPT_MST取得用
        /// </summary>
        public IQueryable<DeptMst> GetDeptMst(DateTime? startDate, DateTime? endDate)
        {
            var query = _hatFContext.DeptMsts
                .Where(x => startDate == null || x.StartDate <= startDate.Value.Date)
                .Where(x => endDate == null || x.EndDate >= endDate.Value.Date)
                .OrderBy(x => x.DeptCode);
            return query;
        }

        /// <summary>
        /// 顧客検索
        /// </summary>
        public IQueryable<CustomersMst> GetCustomersMst(string custCode, string custName, string custKana, string custUserName, string custUserDepName, bool includeDeleted, int rows)
        {
            var query = _hatFContext.CustomersMsts
                .Where(x => string.IsNullOrEmpty(custCode) || x.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(custName) || x.CustName.Contains(custName))
                .Where(x => string.IsNullOrEmpty(custKana) || x.CustKana.Contains(custKana))
                .Where(x => string.IsNullOrEmpty(custUserName) || x.CustUserName.Contains(custUserName))
                .Where(x => string.IsNullOrEmpty(custUserDepName) || x.CustUserDepName.Contains(custUserDepName))
                .Where(x => includeDeleted || x.Deleted == false)   //削除済を含めるか
                .OrderBy(x => x.CustCode)
                .Take(rows);
            return query;
        }

        /// <summary>
        /// 顧客保存
        /// </summary>
        /// <param name="customersMsts"></param>
        /// <returns></returns>
        public async Task<int> PutCustomersMst(IEnumerable<CustomersMst> customersMsts)
        {
            _updateInfoSetter.SetUpdateInfo(customersMsts);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomersMst, CustomersMst>();
            });
            var mapper = config.CreateMapper();

            foreach (CustomersMst item in customersMsts)
            {
                var exits =  await _hatFContext.CustomersMsts
                    .Where(x => x.CustCode == item.CustCode)
                    .SingleAsync();

                if (exits == null)
                {
                    _hatFContext.Add(item);
                }
                else
                {
                    mapper.Map(item, exits);
                    _hatFContext.Update(exits);
                }
            }

            int rowsAffected = await _hatFContext.SaveChangesAsync();
            return rowsAffected;
        }

        /// <summary>
        /// 出荷先(現場)検索
        /// </summary>
        public IQueryable<DestinationsMst> GetDestinationsMst(string custCode, string genbaCode, int rows, int page)
        {
            var query = _hatFContext.DestinationsMsts
                .Where(x => string.IsNullOrEmpty(custCode) || x.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(genbaCode) || x.GenbaCode == genbaCode)
                .Skip(rows * (page - 1)).Take(rows);

            return query;
        }

        /// <summary>
        /// 出荷先(現場)検索
        /// </summary>
        public async Task<List<DestinationsMstEx>> GetDestinationsMst2(string custCode, string genbaCode, int rows, int page)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DestinationsMst, DestinationsMstEx>();
            });
            var mapper = config.CreateMapper();

            var query = _hatFContext.DestinationsMsts
                .Where(x => string.IsNullOrEmpty(custCode) || x.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(genbaCode) || x.GenbaCode == genbaCode)
                .Join(
                    _hatFContext.CustomersMsts,
                    dest => dest.CustCode,
                    cust => cust.CustCode,
                    (dest, cust) => new { DestinationsMsts = dest, CustomersMsts = cust }
                )
                .Select(x => new DestinationsMstEx
                {
                    Address1 = x.DestinationsMsts.Address1,
                    Address2 = x.DestinationsMsts.Address2,
                    Address3 = x.DestinationsMsts.Address3,
                    AreaCode = x.DestinationsMsts.AreaCode,
                    CustCode = x.DestinationsMsts.CustCode,
                    CustName = x.CustomersMsts.CustName,
                    DestFax = x.DestinationsMsts.DestFax,
                    DestTel = x.DestinationsMsts.DestTel,
                    //DistNo = x.DestinationsMsts.DistNo,
                    DistName1 = x.DestinationsMsts.DistName1,
                    DistName2 = x.DestinationsMsts.DistName2,
                    GenbaCode = x.DestinationsMsts.GenbaCode,
                    Remarks = x.DestinationsMsts.Remarks,
                    ZipCode = x.DestinationsMsts.ZipCode,
                    Deleted = x.DestinationsMsts.Deleted,
                    CreateDate = x.DestinationsMsts.CreateDate,
                    Creator = x.DestinationsMsts.Creator,
                    UpdateDate = x.DestinationsMsts.UpdateDate,
                    Updater = x.DestinationsMsts.Updater,
                })
                .Skip(rows * (page - 1)).Take(rows);

            return await query.ToListAsync();
        }

        /// <summary>
        /// 出荷先(現場)保存
        /// </summary>
        /// <param name="destinationsMst"></param>
        /// <returns></returns>
        public async Task<int> PutDestinationsMst(DestinationsMst destinationsMst)
        {
            _updateInfoSetter.SetUpdateInfo(destinationsMst);

            var query = _hatFContext.DestinationsMsts
                            .Where(x => x.CustCode == destinationsMst.CustCode)
                            .Where(x => x.GenbaCode == destinationsMst.GenbaCode);

            var existedDestinationsMst = await query.SingleOrDefaultAsync();
            if (existedDestinationsMst == null)
            {
                _hatFContext.DestinationsMsts.Add(destinationsMst);
            }
            else
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DestinationsMst, DestinationsMst>();
                });
                var mapper = config.CreateMapper();
                mapper.Map(destinationsMst, existedDestinationsMst);
            }

            int count = await _hatFContext.SaveChangesAsync();
            return count;
        }


        public IQueryable<CustomersUserMst> GetCustomersUserMst(string custCode, string custUserCode)
        {
            var query = _hatFContext.CustomersUserMsts
                .Where(x => x.CustCode == custCode)
                .Where(x => x.CustUserCode == custUserCode);

            return query;
        }

        public async Task<int> PutCustomersUserMstAsync(List<CustomersUserMst> customersUserMsts)
        {
            _updateInfoSetter.SetUpdateInfo(customersUserMsts);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomersUserMst, CustomersUserMst>();
            });
            var mapper = config.CreateMapper();

            foreach (CustomersUserMst item in customersUserMsts)
            {
                var exits = await _hatFContext.CustomersUserMsts
                    .Where(x => x.CustCode == item.CustCode)
                    .Where(x => x.CustUserCode == item.CustUserCode)
                    .SingleAsync();

                if (exits == null)
                {
                    _hatFContext.Add(item);
                }
                else
                {
                    mapper.Map(item, exits);
                    _hatFContext.Update(exits);
                }
            }

            int rowsAffected = await _hatFContext.SaveChangesAsync();
            return rowsAffected;
        }
    }
}
