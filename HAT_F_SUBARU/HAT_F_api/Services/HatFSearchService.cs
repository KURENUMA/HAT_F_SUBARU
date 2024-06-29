using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.StateCodes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Identity;
using NuGet.Configuration;
using HAT_F_api.Utils;

namespace HAT_F_api.Services
{
    public class HatFSearchService
    {
        private HatFContext _hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext;
        private UpdateInfoSetter _updateInfoSetter;

        public HatFSearchService(HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, UpdateInfoSetter updateInfoSetter)
        {
            _hatFContext = hatFContext;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _updateInfoSetter = updateInfoSetter;
        }

#if DEBUG
        /// <summary>
        /// 検索サンプル
        /// </summary>
        public async Task<List<ComSyohinMst>> SampleSearch(string searchWord)
        {
            // 中間一致検索
            var query = _hatFContext.ComSyohinMsts.Where(item => item.SelName.Contains(searchWord));
            List<ComSyohinMst> items = await query.ToListAsync();  //「出力」タブに実際に実行されたSQLが出るので確認してください
            return items;
        }
#endif

        public async Task<ClientInit> initClient()
        {
            var init = new ClientInit();
            init.DivBins = await _hatFContext.DivBins.Where(item => !item.Deleted).ToListAsync();

            init.DivDenpyo = await _hatFContext.DivSlips.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.SlipCd, Name = e.SlipName }).ToListAsync();

            //TODO: 多いので全件返すのは避けるよう検討必要
            init.DivEmployee = await _hatFContext.Employees.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.EmpCode, Name = e.EmpName }).ToListAsync();

            //init.DivGenba = await _hatFContext.DestinationsMsts.Select(e => new OptionData { Code = e.GenbaCode, Name = e.DistName1 }).Take(100).ToListAsync();

            init.DivHachus = await _hatFContext.DivOrders.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.OrderCd, Name = e.OrderName }).ToListAsync();

            //init.DivKmans = await _hatFContext.CustomersMsts.Select(e => new OptionData { Code = e.KeymanCode, Name = e.CustUserName }).Take(100).ToListAsync();
            //init.DivKoujitens = await _hatFContext.CustomersMsts.Select(e => new OptionData { Code = e.CustCode, Name = e.CustName }).Take(100).ToListAsync();

            init.DivNohins = await _hatFContext.DivDeliveries.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.DeliveryCd, Name = e.DeliveryName }).ToListAsync();

            // TODO: OPS商品マスタあるか確認
            //init.DivOpsSyohins = new[] { new OptionData { Code = "OPS01", Name = "OPS商品01" }, new OptionData { Code = "OPS02", Name = "OPS商品02" } };

            //TODO: 多いので全件返すのは避けるよう検討必要
            // 仕入先は数万件レコード想定なので、ClientInitに保持するのは間違い
            init.DivShiresakis = await _hatFContext.SupplierMsts.Select(e => new OptionData { Code = e.SupCode, Name = e.SupName }).Take(100).ToListAsync();

            //init.DivSokos = await _context.DivSokos.Select(e => new OptionData { Code = e.SokoCd, Name = e.SokoName }).ToListAsync();
            init.DivSokos = await _hatFContext.WhMsts.ToListAsync();

            //init.DivTokuis = await _hatFContext.CompanysMsts.Select(e => new OptionData { Code = e.CompCode, Name = e.CompName }).Take(100).ToListAsync();

            init.DivUnchins = await _hatFContext.DivFares.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.FareCd, Name = e.FareName }).ToListAsync();

            // 「在庫移送」「仕入先返品」「社内破損」等々
            init.DivUriages = await _hatFContext.DivUriages.Where(item => !item.Deleted).Select(e => new OptionData { Code = e.UriageCd, Name = e.UriageName }).ToListAsync();

            // 消費税率
            init.DivTaxRates = await _hatFContext.DivTaxRates.ToListAsync();

            return init;
        }


        public async Task<IList<FosJyuchuSearchResult>> SearchFosJyuchuHAsync(int rows)
        {
            return await _hatFContext.FosJyuchuHs
                .Where(e => e.DelFlg == "0")

                .Select(e => new FosJyuchuSearchResult
                {
                    SaveKey = e.SaveKey,
                    DenSort = e.DenSort,
                    Hkbn = e.Hkbn,
                    DenNo = e.DenNo,
                    OrderNo = e.OrderNo,
                    HatOrderNo = e.HatOrderNo,
                    TokuiCd = e.TokuiCd,
                    ShiresakiCd = e.ShiresakiCd,
                    Bukken = e.Bukken,
                    RecYmd = e.RecYmd,
                    Nouki = e.Nouki,
                    Dseq = e.Dseq,
                    OrderFlag = e.OrderFlag,
                    OpsOrderNo = e.OpsOrderNo,
                    OpsRecYmd = e.OpsRecYmd,
                    CustOrderno = e.CustOrderno,
                    GenbaCd = e.GenbaCd,
                    Jyu2Cd = e.Jyu2Cd,
                    Jyu2 = e.Jyu2,
                    DenFlg = e.DenFlg,
                })

                .OrderBy(e => e.SaveKey)
                .ThenBy(e => e.DenSort)
                .Take(rows)
                .ToListAsync();
        }

        public async Task<IEnumerable<FosJyuchuPage>> GetPages(string saveKey)
        {
            var headers = await _hatFContext.FosJyuchuHs
              .Where(e => e.SaveKey == saveKey)
              .OrderBy(e => e.DenSort)
              .ToListAsync();

            var datas = await _hatFContext.FosJyuchuDs
              .Where(e => e.SaveKey == saveKey)
              .OrderBy(e => e.DenSort)
              .ThenBy(e => e.DenNo)
              .ToListAsync();

            var pages = new List<FosJyuchuPage>();

            headers.ForEach(header =>
            {
                var page = new FosJyuchuPage();
                page.FosJyuchuH = header;

                page.FosJyuchuDs = datas
                  .Where(data => data.SaveKey == header.SaveKey && data.DenSort == header.DenSort)
                  .OrderBy(data => data.DenNoLine).ToList();

                pages.Add(page);
            });

            return pages;
        }

        /// <summary>
        /// 受注情報検索
        /// </summary>
        /// <param name="condition">受注情報検索条件</param>
        /// <returns></returns>
        public async Task<List<FosJyuchuSearch>> FosJyuchuSearchAsync(FosJyuchuSearchCondition condition)
        {

            var query = _hatFContext.FosJyuchuHs
                // LEFT JOIN 社員マスタ
                .GroupJoin(
                    _hatFContext.Employees,
                    header => header.Jyu2Id,
                    employee => employee.EmpId,
                    (header, employee) => new { header, employee }
                )
                .SelectMany(
                    x => x.employee.DefaultIfEmpty(),
                    (x, e) => new
                    {
                        header = x.header,
                        employee = e
                    }
                )
                // LEFT JOIN 受注明細
                .GroupJoin(
                    _hatFContext.FosJyuchuDs,
                    x => new { x.header.SaveKey, x.header.DenSort },
                    detail => new { detail.SaveKey, detail.DenSort },
                    (x, details) => new
                    {
                        header = x.header,
                        employee = x.employee,
                        details = details
                    }
                )
                .SelectMany(
                    j => j.details.DefaultIfEmpty(),
                    (j, d) => new
                    {
                        SaveKey = j.header.SaveKey,
                        DenSort = j.header.DenSort,
                        Hkbn = j.header.Hkbn,
                        DenNo = j.header.DenNo,
                        OrderNo = j.header.OrderNo,
                        HatOrderNo = j.header.HatOrderNo,
                        TokuiCd = j.header.TokuiCd,
                        ShiresakiCd = j.header.ShiresakiCd,
                        Bukken = j.header.Bukken,
                        RecYmd = j.header.RecYmd,
                        Nouki = j.header.Nouki,
                        Dseq = j.header.Dseq,
                        OrderFlag = j.header.OrderFlag ?? "1",
                        OpsOrderNo = j.header.OpsOrderNo,
                        OpsRecYmd = j.header.OpsRecYmd,
                        CustOrderno = j.header.CustOrderno,
                        GenbaCd = j.header.GenbaCd,
                        Jyu2Id = j.header.Jyu2Id,
                        Jyu2Cd = j.header.Jyu2Cd,
                        Jyu2 = j.header.Jyu2,
                        Nyu2 = j.header.Nyu2,
                        DenFlg = j.header.DenFlg,
                        TeamCd = j.header.TeamCd,
                        KmanCd = j.header.KmanCd,
                        DelFlg = j.header.DelFlg ?? "0",
                        DenState = j.header.DenState ?? "1",
                        State = j.header.DelFlg == "1" ? "4" // 削除
                                : j.header.UkeshoFlg == "1" ? "6" // 請書処理済
                                : j.header.DenState == DenState.Arranged ? "7" // 手配済
                                : j.header.DenState == DenState.Collation ? "3" // ACOS済
                                : j.header.DenState == DenState.Answered ? "2" // 手配中・回答待
                                : j.header.DenState == DenState.Mix ? "2" // 手配中・回答待
                                : j.header.DenState == DenState.Ordered ? "2" // 手配中・回答待
                                : "1", // 発注前
                        EpukoKanriNo = j.header.EpukoKanriNo,
                        SyohinCd = d.SyohinCd ?? "",
                        SyohinName = d.SyohinName ?? "",

                        Jyu2Name = j.employee.EmpName
                    }
                )
                // チームCD
                .Where(j => string.IsNullOrEmpty(condition.TeamCd) || j.TeamCd.StartsWith(condition.TeamCd))
                // 得意先CD
                .Where(j => string.IsNullOrEmpty(condition.TokuCd) || j.TokuiCd.StartsWith(condition.TokuCd))
                // キーマンCD
                .Where(j => string.IsNullOrEmpty(condition.KmanCd) || j.KmanCd.Equals(condition.KmanCd))
                // 仕入先CD
                .Where(j => string.IsNullOrEmpty(condition.ShiresakiCd) || j.ShiresakiCd.Equals(condition.ShiresakiCd))
                // 受注者
                .Where(j => string.IsNullOrEmpty(condition.Jyu2) || j.Jyu2.Equals(condition.Jyu2))
                // 入力者
                .Where(j => string.IsNullOrEmpty(condition.Nyu2) || j.Nyu2.Equals(condition.Nyu2))
                // 現場CD
                .Where(j => string.IsNullOrEmpty(condition.GenbaCd) || j.GenbaCd.StartsWith(condition.GenbaCd))
                // 現場名
                .Where(j => string.IsNullOrEmpty(condition.GenbaName) || j.Bukken.Contains(condition.GenbaName))
                // 客先注番
                .Where(j => string.IsNullOrEmpty(condition.CustOrderNo) || j.CustOrderno.Contains(condition.CustOrderNo))
                // HAT注番
                .Where(j => string.IsNullOrEmpty(condition.HatOrderNo) || j.HatOrderNo.StartsWith(condition.HatOrderNo))
                // 発注方法
                .Where(j => string.IsNullOrEmpty(condition.Hkbn) || j.Hkbn.Equals(condition.Hkbn))
                // 伝票番号
                .Where(j => string.IsNullOrEmpty(condition.DenNo) || j.DenNo.Equals(condition.DenNo))
                // 受注番号
                .Where(j => string.IsNullOrEmpty(condition.OrderNo) || j.OrderNo.StartsWith(condition.OrderNo))
                // 状態
                .Where(j => string.IsNullOrEmpty(condition.State) || j.State.Equals(condition.State))
                // 商品CD
                .Where(j => string.IsNullOrEmpty(condition.SyohinCd) || j.SyohinCd.Contains(condition.SyohinCd))
                // 商品名
                .Where(j => string.IsNullOrEmpty(condition.SyohinName) || j.SyohinName.Contains(condition.SyohinName))
                // 受注日（FROM）
                .Where(j => !condition.RecYmdFrom.HasValue || j.RecYmd >= condition.RecYmdFrom.Value.Date)
                // 受注日（TO）
                .Where(j => !condition.RecYmdTo.HasValue || j.RecYmd < (condition.RecYmdTo.Value.Date.AddDays(1)))
                // 納日（FROM）
                .Where(j => !condition.NoukiFrom.HasValue || j.Nouki >= condition.NoukiFrom.Value.Date)
                // 納日（TO）
                .Where(j => !condition.NoukiTo.HasValue || j.Nouki < (condition.NoukiTo.Value.Date.AddDays(1)))
                // 受注区分
                .Where(j => string.IsNullOrEmpty(condition.OrderFlag) || j.OrderFlag.Equals(condition.OrderFlag))
                // OPSNo.
                .Where(j => string.IsNullOrEmpty(condition.OpsOrderNo) || j.OpsOrderNo.Equals(condition.OpsOrderNo))
                // ＯＰＳ入力日(From)
                .Where(j => !condition.OpsRecYMDFrom.HasValue || j.OpsRecYmd >= condition.OpsRecYMDFrom.Value.Date)
                // ＯＰＳ入力日(To)
                .Where(j => !condition.OpsRecYMDTo.HasValue || j.OpsRecYmd < (condition.OpsRecYMDTo.Value.Date.AddDays(1)))
                // エプコ管理番号 TODO
                .Where(j => string.IsNullOrEmpty(condition.EpukoKanriNo) || j.EpukoKanriNo.Equals(condition.EpukoKanriNo))

                // 結果SELECT
                .Select(j => new FosJyuchuSearch
                {
                    DelFlg = j.DelFlg,
                    Hkbn = j.Hkbn,
                    DenNo = j.DenNo,
                    Dseq = j.Dseq,
                    TokuiCd = j.TokuiCd,
                    CustOrderno = j.CustOrderno,
                    GenbaCd = j.GenbaCd,
                    ShiresakiCd = j.ShiresakiCd,
                    RecYmd = j.RecYmd,
                    HatOrderNo = j.HatOrderNo,
                    Jyu2Name = j.Jyu2Name,
                    State = j.State,
                    OrderFlag = j.OrderFlag,
                    OpsOrderNo = j.OpsOrderNo,
                    OpsRecYmd = j.OpsRecYmd,
                    Nouki = j.Nouki,
                    DenSort = j.DenSort,

                    SaveKey = j.SaveKey,
                    DenState = j.DenState,
                    TeamCd = j.TeamCd,
                    KmanCd = j.KmanCd,
                    Bukken = j.Bukken,
                    OrderNo = j.OrderNo,
                    Jyu2 = j.Jyu2,
                    Jyu2Cd = j.Jyu2Cd,
                    Jyu2Id = j.Jyu2Id,

                    //SyohinCd = j.SyohinCd,
                    //SyohinName = j.SyohinName,

                })
                .Distinct()

                // ORDER BY
                .OrderBy(j => j.DelFlg)
                .ThenBy(j => j.State)
                .ThenByDescending(
                    j => j.OrderFlag == "3" ? "9" // 3:OPS
                        : j.OrderFlag == "4" ? "9" // 4:HOPE
                        : j.OrderFlag == "7" ? "9" // 7:新OPS
                        : string.IsNullOrEmpty(j.OrderFlag) ? "1"
                        : j.OrderFlag)
                .ThenByDescending(j => j.RecYmd)
                .ThenBy(j => j.DenNo)
                .ThenBy(j => j.DenSort)

                // 取得件数
                .Take(condition.rows);

            return await query.ToListAsync();

        }

        /// <summary>
        /// 得意先検索
        /// </summary>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="torihikiName">得意先名</param>
        /// <param name="torihikiNameKana">得意先名カナ</param>
        /// <param name="rows">取得件数</param>
        /// <returns>得意先リスト</returns>
        public async Task<List<Torihiki>> GetTorihikiAsync(
            string torihikiCd,
            string torihikiName,
            string torihikiNameKana,
            int rows)
        {
            var query = _hatFContext.CompanysMsts
                .Where(item =>
                    (string.IsNullOrEmpty(torihikiCd) || item.CompCode == torihikiCd)
                    && (string.IsNullOrEmpty(torihikiName) || item.CompName.Contains(torihikiName))
                    && (string.IsNullOrEmpty(torihikiNameKana) || item.CompKana.Contains(torihikiNameKana))
                )
                .Select(item => new Torihiki
                {
                    TorihikiCd = item.CompCode,
                    TokuZ = item.CompName,
                    TokuH = item.CompKana,
                    ATel = item.Tel,
                    AFax = item.Fax,
                })
                .Take(rows);

            List<Torihiki> results = await query.ToListAsync();
            return results;
        }

        /// <summary>
        /// キーマンリスト取得
        /// </summary>
        /// <param name="teamCd"></param>
        /// <param name="custCode">リストを作成する顧客コード</param>
        /// <param name="keymanCd">キーマンコード</param>
        /// <param name="rows">最大取得件数</param>
        /// <returns>キーマンリスト</returns>
        public async Task<List<Keyman>> GetKeymenAsync(string teamCd, string custCode, string keymanCd, int rows)
        {
            //var queryCustKeyman = _hatFContext.CustomersMsts
            //    .GroupJoin(
            //        // LEFT JOIN 顧客担当者（キーマン）
            //        _hatFContext.CustomersUserMsts,
            //        custEmp => custEmp.CustCode,
            //        keyman => keyman.CustUserCode,
            //        (custEmp, keyman) => new { CustomersMsts = custEmp, CustomersUserMsts = keyman }
            //    )
            //    .SelectMany(
            //        (items) => items.CustomersUserMsts.DefaultIfEmpty(),
            //        (item, keyman) => new
            //        {
            //            CustomersMsts = item.CustomersMsts,
            //            CustomersUserMsts = keyman,
            //        }
            //    );

            var query = _hatFContext.CustomersMsts
                .GroupJoin(
                    // LEFT JOIN 顧客＋顧客担当者（キーマン）
                    _hatFContext.Employees,
                    cm => cm.EmpCode,
                    emp => emp.EmpCode,
                    (cm, emp) => new { CustomersMsts = cm, Employees = emp }
                )
                .SelectMany(
                    (items) => items.Employees.DefaultIfEmpty(),
                    (item, employee) => new
                    {
                        CustomersMsts = item.CustomersMsts,
                        Employees = employee,
                    }
                )
                .GroupJoin(
                    // LEFT JOIN 顧客担当者（キーマン）
                    _hatFContext.CustomersUserMsts,
                    custEmp => custEmp.CustomersMsts.CustCode,
                    keyman => keyman.CustUserCode,
                    (cust, keyman) => new { CustomersMsts = cust, CustomersUserMsts = keyman }
                )
                .SelectMany(
                    (items) => items.CustomersUserMsts.DefaultIfEmpty(),
                    (item, keyman) => new
                    {
                        CustomersMstsEmployees = item.CustomersMsts,
                        CustomersUserMsts = keyman,
                    }
                )
                .Where(item =>
                    (string.IsNullOrEmpty(custCode) || item.CustomersMstsEmployees.CustomersMsts.CustCode == custCode)
                    && (string.IsNullOrEmpty(keymanCd) || item.CustomersUserMsts.CustUserCode == keymanCd)
                    && (string.IsNullOrEmpty(teamCd) || item.CustomersMstsEmployees.Employees.DeptCode == teamCd)
                )
                .Select(item => new Keyman { KmanCd = item.CustomersUserMsts.CustUserCode, KmanNm1 = item.CustomersUserMsts.CustUserName })
                .Distinct()
                .Take(rows);

            List<Keyman> results = await query.ToListAsync();
            return results;
        }

        /// <summary>仕入先情報取得</summary>
        /// <param name="supCode">仕入先コード</param>
        /// <returns>仕入先情報</returns>
        public async Task<SupplierMst> GetSupplierAsync(string supCode)
        {
            return await _hatFContext.SupplierMsts
                .SingleOrDefaultAsync(s => s.SupCode == supCode);
        }

        /// <summary>取引先情報取得</summary>
        /// <param name="compCode">取引先コード</param>
        /// <returns>取引先情報</returns>
        public async Task<CompanysMst> GetCompanysAsync(string compCode)
        {
            return await _hatFContext.CompanysMsts
                .SingleOrDefaultAsync(s => s.CompCode == compCode);
        }

        /// <summary>
        /// 仕入先検索
        /// </summary>
        /// <param name="supplierCd">仕入先コード</param>
        /// <param name="supplierName">仕入先名</param>
        /// <param name="supplierNameKana">仕入先名カナ</param>
        /// <param name="teamCd">チームコード</param>
        /// <param name="rows">最大取得件数</param>
        /// <returns>仕入先リスト</returns>
        public async Task<List<Supplier>> GetSuppliersAsync(
            string supplierCd,
            string supplierName,
            string supplierNameKana,
            string teamCd,
            int rows)
        {

            // GroupJoin = LEFT JOIN
            // 右辺が複数形に見えるようになる(0個以上)
            var query = _hatFContext.SupplierMsts
                .GroupJoin(
                        _hatFContext.ProductSuppliers
                            .Join(
                                _hatFContext.ProductCategories,
                                ps => ps.CategoryCode,
                                pc => pc.CategoryCode,
                                (ps, pc) => new
                                {
                                    SupCode = ps.SupCode,
                                    CategoryCode = ps.CategoryCode,
                                    ProdCateName = pc.ProdCateName
                                }),

                    sup => sup.SupCode,
                    supCat => supCat.SupCode,
                    (s, sc) => new
                    {
                        Supplier = s,
                        SupplierCategories = sc,
                    }
                )
                .Where(item =>
                    (string.IsNullOrEmpty(supplierCd) || item.Supplier.SupCode.StartsWith(supplierCd))
                    && (string.IsNullOrEmpty(supplierName) || item.Supplier.SupName.Contains(supplierName))
                    && (string.IsNullOrEmpty(supplierNameKana) || item.Supplier.SupKana.Contains(supplierNameKana))
                )
                .SelectMany(
                    (item) => item.SupplierCategories.DefaultIfEmpty(),
                    (item, supCat) => new Supplier
                    {
                        SupplierCd = item.Supplier.SupCode,
                        SupplierName = item.Supplier.SupName,
                        SupplierNameKana = item.Supplier.SupKana,
                        SupplierFax = item.Supplier.SupFax,
                        SupplierTel = item.Supplier.SupTel,
                        CategoryCode = supCat.CategoryCode ?? "",
                        CategoryName = supCat.ProdCateName ?? "",
                    }
                )
                .Take(rows);

            List<Supplier> results = await query.ToListAsync();
            return results;
        }

        /// <summary>
        /// 仕入先分類検索
        /// </summary>
        /// <param name="supplierCd">仕入先コード</param>
        /// <param name="supplierName">仕入先名</param>
        /// <param name="supplierNameKana">仕入先名カナ</param>
        /// <param name="teamCd">チームコード</param>
        /// <param name="rows">最大取得件数</param>
        /// <returns>仕入先リスト</returns>
        public async Task<List<SupplierCategory>> GetSupplierCategoriesAsync(
            string supplierCd,
            string supplierName,
            string supplierNameKana,
            string teamCd,
            int rows)
        {
            var results = await GetSuppliersAsync(supplierCd, supplierName, supplierNameKana, teamCd, rows);

            // 仮実装
            // クラスを共通化するか、取得処理を共通化するか、要検討
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Supplier, SupplierCategory>();
            });
            var mapper = config.CreateMapper();

            // 別の型に詰め替え
            List<SupplierCategory> cats = mapper.Map<List<SupplierCategory>>(results);
            return cats;

        }


        /// <summary>
        /// 現場検索
        /// </summary>
        /// <param name="custCode">顧客コード</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="genbaName">現場名</param>
        /// <param name="address">住所</param>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="keymanCode">キーマンコード</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致した現場情報のリスト</returns>
        public async Task<List<SearchGenbaResult>> GetDestinationsAsync(
            string custCode,
            string genbaCd,
            string genbaName,
            string address,
            string torihikiCd,
            string keymanCode,
            int rows)
        {

            var query = _hatFContext.DestinationsMsts
                .Join(_hatFContext.CustomersMsts,
                    item => new { item.CustCode },
                    item => new { item.CustCode },
                    (dest, cust) => new { DestinationsMsts = dest, CustomersMsts = cust }
                )
                .GroupJoin(_hatFContext.CustomersUserMsts,
                    item => new { item.CustomersMsts.CustCode },
                    item => new { item.CustCode },
                    (destCust, custUser) => new
                    {
                        DestinationsMsts = destCust.DestinationsMsts,
                        CustomersMsts = destCust.CustomersMsts,
                        CustomersUserMsts = custUser
                    }
                )
                .SelectMany(
                    x => x.CustomersUserMsts.DefaultIfEmpty(),
                    (x, e) => new
                    {
                        DestinationsMsts = x.DestinationsMsts,
                        CustomersMsts = x.CustomersMsts,
                        CustomersUserMsts = e
                    }
                )
                .Where(x => string.IsNullOrEmpty(custCode) || x.DestinationsMsts.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(genbaCd) || x.DestinationsMsts.GenbaCode == genbaCd)
                .Where(x => string.IsNullOrEmpty(genbaName) || (x.DestinationsMsts.DistName1 + x.DestinationsMsts.DistName2).Contains(genbaName))
                .Where(x => string.IsNullOrEmpty(address) || (x.DestinationsMsts.Address1 + x.DestinationsMsts.Address2 + x.DestinationsMsts.Address3).Contains(address))
                .Where(x => string.IsNullOrEmpty(torihikiCd) || x.CustomersMsts.ArCode == torihikiCd)
                .Where(x => string.IsNullOrEmpty(keymanCode) || x.CustomersUserMsts.CustUserCode == keymanCode)
                .Select(item => new SearchGenbaResult() { Customer = item.CustomersMsts, Destination = item.DestinationsMsts })
                .Take(rows);

            return await query.ToListAsync();
        }



        /// <summary>
        /// 工事店検索
        /// </summary>
        /// <param name="koujitenCd">工事店CD</param>
        /// <param name="koujitenName">工事店名</param>
        /// <param name="torihikiCd">得意先CD</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致した工事店情報のリスト</returns>
        public async Task<List<Koujiten>> GetKoujitenAsync(
            string koujitenCd,
            string koujitenName,
            string torihikiCd,
            int rows)

        {
            var query = _hatFContext.ConstructionShopMsts
               .Where(item =>
                   (string.IsNullOrEmpty(koujitenCd) || item.ConstCode == koujitenCd)
                   && (string.IsNullOrEmpty(koujitenName) || item.ConstName.Contains(koujitenName))
                   && (string.IsNullOrEmpty(torihikiCd) || item.CustCode == torihikiCd)
                )
                .Select(item => new Koujiten
                {
                    KojiCd = item.ConstCode,
                    KojiNnm = item.ConstName,
                    KojiAnm = item.ConstKana,
                    PostCd = item.ConstZipCode,
                    Adrs1 = item.ConstAddress1,
                    Adrs2 = item.ConstAddress2,
                    Adrs3 = item.ConstAddress3,
                    Fil1 = "",
                })
                .Take(rows);

            List<Koujiten> results = await query.ToListAsync();
            return results;
        }

        /// <summary>
        /// 商品検索（HAT商品）
        /// </summary>
        /// <param name="supplierCd">仕入先CD</param>
        /// <param name="productNoOrProductName">品番/名（規格）</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索に合致したHAT商品のリスト</returns>
        public async Task<List<Product>> GetProductsOfHatAsync(
            string supplierCd,
            string productNoOrProductName,
            int rows)
        {
            var query = _hatFContext.ComSyohinMsts
                .Where(item =>
                   (string.IsNullOrEmpty(supplierCd) || item.Mekarcd == supplierCd)
                   && (string.IsNullOrEmpty(productNoOrProductName) || item.HatSyohin.Contains(productNoOrProductName) || item.SyohinName.Contains(productNoOrProductName) || item.SyohinNameK.Contains(productNoOrProductName))
                )
                .Select(item => new Product
                {
                    Cd24 = item.HatSyohin,
                    MKey = item.MekarHinban,
                    Code5 = item.SyohinBunrui,
                    Nnm = item.SyohinName,
                    Anm = item.SyohinNameK,
                })
                .Take(rows);

            List<Product> results = await query.ToListAsync();
            return results;
        }

        /// <summary>郵便番号検索</summary>
        /// <param name="postCode">郵便番号</param>
        /// <param name="address">住所</param>
        /// <param name="rows">取得件数</param>
        /// <returns>郵便番号、住所に基づいて取得した住所のリスト</returns>
        public Task<List<PostAddress>> GetPostAddressesAsync(string postCode = null, string address = null, int rows = 200)
        {
            string queryPostCode = (postCode ?? "").Replace("-", string.Empty);

            return _hatFContext.PostAddresses
                .Where(x => string.IsNullOrEmpty(postCode) || queryPostCode == x.PostCode7)
                .Where(x => string.IsNullOrEmpty(address) || (x.Prefectures.Contains(address) || x.Municipalities.Contains(address) || x.TownArea.Contains(address)))
                .OrderBy(x => x.PostCode7)
                .Take(rows)
                .ToListAsync();
        }

        /// <summary>
        /// 部門付き社員検索
        /// </summary>
        /// <param name="empCode">社員コード</param>
        /// <returns></returns>
        public async Task<EmployeeDept> GetEmployeeDeptAsync(string empCode)
        {
            // 社員検索
            var employee = await _hatFContext.Employees
                .Where(e => e.EmpCode == empCode)
                .Where(e => e.Deleted == false)
                .FirstOrDefaultAsync();

            var deptMasts = new List<DeptMst>();
            if (employee != null)
            {
                // 部門検索
                DateTime now = _hatFApiExecutionContext.ExecuteDateTimeJst;

                var deptMst = await GetDeptMstAsync(employee.DeptCode, now);
                if (deptMst != null)
                {
                    deptMasts.Add(deptMst);
                    for (int i = deptMst.DeptLayer; 0 < i; i--)
                    {
                        deptMst = await GetDeptMstAsync(deptMst.DeptPath, now);
                        if (deptMst != null)
                        {
                            deptMasts.Add(deptMst);
                        }
                    }
                }

                deptMasts.Sort((a, b) => a.DeptLayer - b.DeptLayer);

            }

            var emplyeeDept = new EmployeeDept();
            emplyeeDept.Employee = employee;
            emplyeeDept.DeptMsts = deptMasts;
            return emplyeeDept;
        }

        public async Task<DeptMst> GetDeptMstAsync(string deptCode, DateTime now)
        {
            var query = _hatFContext.DeptMsts
                .Where(d => d.DeptCode == deptCode)
                .Where(d => d.StartDate <= now)
                .Where(d => d.EndDate >= now);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 返品入庫一覧
        /// </summary>
        /// <param name="denNo">伝No</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        public async Task<List<ViewReturnReceipt>> GetReturnReceiptsAsync(string denNo, int rows)
        {
            var query = _hatFContext.ViewReturnReceipts
                    .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                    .Take(rows);

            return await query.ToListAsync();
        }

        /// <summary>
        /// 入庫確認一覧取得
        /// </summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        public IQueryable<ViewWarehousingReceiving> GetWarehousingReceivings(string denNo, int rows)
        {
            var query = _hatFContext.ViewWarehousingReceivings
                .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo);

            return query;
        }

        /// <summary>
        /// 入庫確認詳細取得
        /// </summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        public IQueryable<ViewWarehousingReceivingDetail> GetWarehousingReceivingDetails(string denNo, int rows)
        {
            var query = _hatFContext.ViewWarehousingReceivingDetails
                .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo);

            return query;
        }

        /// <summary>
        /// 入庫確認詳細更新
        /// </summary>
        /// <returns></returns>
        public int PutWarehousingReceivingDetails(List<ViewWarehousingReceivingDetail> data)
        {
            // 更新日時等のセット
            _updateInfoSetter.SetUpdateInfo(data);

            // TODO:定義方法修正
            const string warehouseCode = "31";

            int count = 0;

            // TODO:自動採番化
            long warehousingId = _hatFContext.Warehousings.Max(x => x.WarehousingId) + 1;

            foreach (var record in data)
            {
                var wh = _hatFContext.Warehousings
                    .Where(x => x.Chuban == record.H注番)
                    .SingleOrDefault();

                int increasingAmount;

                if (wh == null)
                {
                    // 数量増加量
                    increasingAmount = (record.入庫数量 ?? 0);

                    wh = new Warehousing()
                    {
                        // WarehousingId = warehousingId++, // DB側で自動採番（identity column） のため設定しない
                        WhCode = warehouseCode,
                        WarehousingDiv = "I", // 入庫
                        WarehousingDatetime = record.入出庫日時,
                        WarehousingCause = 0,   // 入出庫理由:通常 TODO:定数化
                        DenNo = record.伝票番号,
                        Chuban = record.H注番,
                        ProdCode = record.Hat商品コード,
                        StockType = "2", // 預かり在庫 (マルマ) TODO:定数化
                        QualityType = "G", // 良品 TODO:定数化
                        Quantity = record.入庫数量,
                        QuantityBara = record.入庫バラ数量,
                    };


                    // 更新日時セット
                    _updateInfoSetter.SetUpdateInfo(wh);

                    // INSERT予約
                    _hatFContext.Warehousings.Add(wh);
                }
                else
                {
                    // 数量増加量
                    increasingAmount = (record.入庫数量 ?? 0) - (wh.Quantity ?? 0);

                    wh.Quantity = record.入庫数量;
                    wh.QuantityBara = record.入庫バラ数量;
                    wh.WarehousingDatetime = record.入出庫日時;

                    // 更新日時セット
                    _updateInfoSetter.SetUpdateInfo(wh);

                    // UPDATE予約
                    _hatFContext.Update(wh);
                }

                // TODO:在庫テーブルの反映
                //// TODO:楽観排他必要（在庫テーブルにバージョン列追加）
                //var stockQuery = _hatFContext.Stocks
                //    .Where(x => x.WhCode == warehouseCode)
                //    .Where(x => x.ProdCode == record.Hat商品コード)
                //    .Where(x => x.StockType == "2")   //TODO:定数化
                //    .Where(x => x.QualityType == "G");　//TODO:定数化

                //var stock = stockQuery.Single();
                //DateTime lastUpdateDateTime = stock.UpdateDate;

                //stock.Actual += (short)increasingAmount;
                //stock.Valid += (short)increasingAmount;
                ////stock.ActualBara += (short)record.入庫バラ数量;
                ////stock.ValidBara += (short)record.入庫バラ数量;

                //// 更新日時セット
                //_updateInfoSetter.SetUpdateInfo(stock);

                //// UPDATE予約
                //_hatFContext.Update(stock);

                count++;
            }

            return count;
        }



        /// <summary>
        /// 出荷指示一覧取得
        /// </summary>
        /// <param name="includeOrderPrinted">出荷指示書印刷済の分を含めるか</param>
        /// <param name="shippedDateFrom">出荷日From</param>
        /// <param name="shippedDateTo">出荷日To</param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        public IQueryable<ViewWarehousingShipping> GetWarehousingShippings(bool includeOrderPrinted, DateTime? shippedDateFrom, DateTime? shippedDateTo, int rows)
        {
            var query = _hatFContext.ViewWarehousingShippings
                //.Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                .Where(x =>
                    (x.倉庫ステータス == "2")  // 出荷指示書未印刷は常に含める
                    ||
                    (
                        includeOrderPrinted     // 出荷指示書印刷済の分を含めるか                       
                        && x.倉庫ステータス == "3"     // 出荷指示書印刷済
                        && (!shippedDateFrom.HasValue || x.出荷日 >= shippedDateFrom.Value.Date)        // 出荷指示書印刷済みは指定の日付範囲
                        && (!shippedDateTo.HasValue || x.出荷日 < shippedDateTo.Value.Date.AddDays(1))  // 出荷指示書印刷済みは指定の日付範囲
                    )
                )
                .Take(rows);

            return query;
        }

        public async Task<int> PutWarehousingShippingsAsync(List<ViewWarehousingShipping> data)
        {
            int count = 0;

            foreach (var source in data)
            {
                var query = _hatFContext.FosJyuchuHs
                    .Where(x => x.SaveKey == source.SaveKey && x.DenSort == source.DenSort);

                var dest = query.Single();
                {
                    dest.WhStatus = source.倉庫ステータス;
                    dest.ShippedDate = source.出荷日;
                    dest.DueDate = source.到着予定日;

                    //TODO:更新者 更新日
                }

                count++;
            }

            await _hatFContext.SaveChangesAsync();

            return count;
        }


        /// <summary>
        /// 出荷指示詳細取得
        /// </summary>
        /// <param name="saveKey"></param>
        /// <param name="denSort"></param>
        /// <param name="denNoLine"></param>
        /// <param name="denNo"></param>
        /// <param name="rows">取得件数</param>
        /// <returns></returns>
        public async Task<List<ViewWarehousingShippingDetail>> GetWarehousingShippingDetailsAsync(string saveKey, string denSort, string denNoLine, string denNo, int rows)
        {
            var query = _hatFContext.ViewWarehousingShippingDetails
                .Where(x => string.IsNullOrEmpty(saveKey) || x.SaveKey == saveKey)
                .Where(x => string.IsNullOrEmpty(denSort) || x.DenSort == denSort)
                .Where(x => string.IsNullOrEmpty(denNoLine) || x.DenNoLine == denNoLine)
                .Where(x => string.IsNullOrEmpty(denNo) || x.伝票番号 == denNo)
                .Take(rows);

            return await query.ToListAsync();
        }


        /// <summary>
        /// 商品在庫取得
        /// </summary>
        /// <param name="baseDate"></param>
        /// <returns></returns>
        public IQueryable<ViewProductStock> GetProductStock(DateTime baseDate)
        {
            var query = _hatFContext.ViewProductStocks
                .Where(x => x.入出庫日時 >= baseDate.Date)
                .GroupBy(x => new { /*x.データ種別,*/ x.商品コード, x.商品名, x.商品分類コード, x.商品分類名, x.商品名フル })
                .Select(x => new ViewProductStock
                {
                    データ種別 = null, //x.Key.データ種別, 集計時は区別しないので空
                    商品コード = x.Key.商品コード,
                    商品名 = x.Key.商品名,
                    商品名フル = x.Key.商品名フル,
                    商品分類コード = x.Key.商品分類コード,
                    商品分類名 = x.Key.商品分類名,
                    期首残高 = x.Sum(s => s.期首残高),
                    期首残高バラ = x.Sum(s => s.期首残高バラ),
                    入庫計 = x.Sum(s => s.入庫計),
                    入庫計バラ = x.Sum(s => s.入庫計バラ),
                    出庫計 = x.Sum(s => s.出庫計),
                    出庫計バラ = x.Sum(s => s.出庫計バラ),
                    期末評価価格 = x.Sum(s => s.期末評価価格),
                });

            return query;
        }

        /// <summary>
        /// 商品在庫全体
        /// </summary>
        public IQueryable<ViewProductStock> GetProductStockSummary(DateTime baseDate)
        {
            var query = _hatFContext.ViewProductStocks
                .Where(x => x.入出庫日時 >= baseDate.Date)
                .GroupBy(x => new { 商品コード = "" })
                .Select(x => new ViewProductStock
                {
                    期首残高 = x.Sum(s => s.期首残高),
                    期首残高バラ = x.Sum(s => s.期首残高バラ),
                    入庫計 = x.Sum(s => s.入庫計),
                    入庫計バラ = x.Sum(s => s.入庫計バラ),
                    出庫計 = x.Sum(s => s.出庫計),
                    出庫計バラ = x.Sum(s => s.出庫計バラ),
                    期末評価価格 = x.Sum(s => s.期末評価価格),
                });

            return query;
        }

        /// <summary>
        /// メール送信可能な社員検索
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetSendableEmployeesAsync()
        {
            var query = _hatFContext.Employees
                .Where(e => e.Deleted == false)
                .Where(e => string.IsNullOrWhiteSpace(e.Email) == false)
                ;
            return await query.ToListAsync();
        }

        public async Task<List<ViewPurchaseBillingDetail>> GetViewPurchaseBillingDetailAsync(ViewPurchaseBillingDetailCondition condition, int row = 200)
        {
            var query = _hatFContext.ViewPurchaseBillingDetails
                .Where(v => string.IsNullOrEmpty(condition.仕入先コード) || v.仕入先コード == condition.仕入先コード)
                .Where(v => string.IsNullOrEmpty(condition.Hat注文番号) || v.Hat注文番号 == condition.Hat注文番号)
                .Where(v => !condition.仕入支払年月日From.HasValue || v.仕入支払年月日 >= (condition.仕入支払年月日From.Value.Date))
                .Where(v => !condition.仕入支払年月日To.HasValue || v.仕入支払年月日 < (condition.仕入支払年月日To.Value.Date.AddDays(1)))
                .OrderBy(v => v.Hat注文番号)
                .ThenBy(v => v.伝票番号)
                .ThenBy(v => v.Hページ番号)
                .ThenBy(v => v.H行番号)
                .Take(row)
                ;
            return await query.ToListAsync();
        }
        public async Task<List<ViewPurchaseReceivingDetail>> GetViewPurchaseReceivingDetailAsync(string hatfOrderNo, int row = 200)
        {
            var query = _hatFContext.ViewPurchaseReceivingDetails
                .Where(v => string.IsNullOrEmpty(hatfOrderNo) || v.Hat注文番号 == hatfOrderNo)
                .Take(row)
                ;
            return await query.ToListAsync();
        }

        /// <summary>物件コードの重複チェック</summary>
        /// <param name="constructionCode">物件コード</param>
        /// <returns>重複結果</returns>
        public async Task<bool> CheckDuplicateConstructionCodeAsync(string constructionCode)
        {
            var code = await _hatFContext.Constructions
                .Where(h => h.ConstructionCode == constructionCode)
                .Select(h => h.ConstructionCode)
                .FirstOrDefaultAsync();

            return code == null;
        }

        /// <summary>
        /// 社員マスタ検索
        /// </summary>
        public IQueryable<Employee> GetEmployee(int? employeeId, string empCode, string empName, string empKana, bool includeDeleted)
        {
            var query = _hatFContext.Employees
                .Where(x => !employeeId.HasValue || x.EmpId == employeeId.Value)
                .Where(x => string.IsNullOrEmpty(empCode) || x.EmpCode == empCode)
                .Where(x => string.IsNullOrEmpty(empName) || x.EmpName.Contains(empName))
                .Where(x => string.IsNullOrEmpty(empKana) || x.EmpKana.Contains(empKana))
                .Where(x => includeDeleted || x.Deleted == false) //削除済を含めるか
                .OrderBy(x => x.EmpCode);

            return query;
        }

        /// <summary>
        /// ユーザー割当権限検索
        /// </summary>
        public IQueryable<UserAssignedRole> GetUserAssignedRole(int? employeeId)
        {
            var query = _hatFContext.UserAssignedRoles
                .Where(x => !employeeId.HasValue || x.EmpId == employeeId.Value)
                .OrderBy(x => x.UserRoleCd);

            return query;
        }
        /// <summary>
        /// ユーザー割当権限検索
        /// </summary>
        public IQueryable<Employee> GetEmployeeUserAssignedRole(string UserRoleCd)
        {
            var query = _hatFContext.Employees.Join(
                   _hatFContext.UserAssignedRoles,
                   e => e.EmpId,
                   ur => ur.EmpId,
                   (e, ur) => new { Employee = e, UserRole = ur })
                   .Where(eur => eur.UserRole.UserRoleCd == UserRoleCd)
                   .Select(eur => eur.Employee);


            return query;
        }


        /// <summary>
        /// 得意先(取引先)検索
        /// </summary>
        /// <param name="compCode"></param>

        /// <returns></returns>
        public IQueryable<CompanysMst> GetCompanysMst(string compCode)
        {
            var query = _hatFContext.CompanysMsts
                .Where(x => string.IsNullOrEmpty(compCode) || x.CompCode == compCode)
                ;

            return query;
        }

        /// <summary>
        /// 出荷先(現場)検索
        /// </summary>
        /// <param name="custCode"></param>
        /// <param name="genbaCode"></param>
        /// <returns></returns>
        public IQueryable<DestinationsMst> GetDestinationsMst(string custCode, string genbaCode)
        {
            var query = _hatFContext.DestinationsMsts
                .Where(x => string.IsNullOrEmpty(custCode) || x.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(genbaCode) || x.GenbaCode == genbaCode)
                ;

            return query;
        }

        /// <summary>売上調整情報を取得する</summary>
        /// <param name="tokuiCd">得意先コード（必須）</param>
        /// <param name="invoicedDateFrom">請求日（省略可）</param>
        /// <param name="invoicedDateTo">請求日（省略可）</param>
        /// <returns>売上調整情報</returns>
        public async Task<List<ViewSalesAdjustment>> GetSalesAdjustmentsAsync
            (string tokuiCd, DateTime? invoicedDateFrom, DateTime? invoicedDateTo)
        {
            return await _hatFContext.ViewSalesAdjustments
                .Where(x => x.得意先コード == tokuiCd)
                .Where(x => !invoicedDateFrom.HasValue || invoicedDateFrom <= x.請求日)
                .Where(x => !invoicedDateTo.HasValue || x.請求日 <= invoicedDateTo)
                .ToListAsync();
        }

        /// <summary>
        /// 契約単価・掛率（販売）テーブルの取得
        /// </summary>
        public IQueryable<KTankaSale> GetKTankaSales(DateTime? baseDate, string prodCode, string custCode, string sign)
        {
            var query = _hatFContext.KTankaSales
                .Where(x => false == baseDate.HasValue || (x.StartDate >= baseDate && x.EndDate > baseDate))
                .Where(x => string.IsNullOrEmpty(prodCode) || x.ProdCode == prodCode)
                .Where(x => string.IsNullOrEmpty(custCode) || x.CustCode == custCode)
                .Where(x => string.IsNullOrEmpty(sign) || x.Sign == sign)
                ;

            return query;
        }

        /// <summary>
        /// 契約単価・掛率（仕入）テーブルの取得
        /// </summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="supCode">仕入先コード</param>
        /// <param name="sign">売上記号</param>
        public IQueryable<KTankaPurchase> GetKTankaPurchases(DateTime? baseDate, string prodCode, string supCode, string sign)
        {
            var query = _hatFContext.KTankaPurchases
                .Where(x => false == baseDate.HasValue || (x.StartDate >= baseDate && x.EndDate > baseDate))
                .Where(x => string.IsNullOrEmpty(prodCode) || x.ProdCode == prodCode)
                .Where(x => string.IsNullOrEmpty(supCode) || x.SupCode == supCode)
                .Where(x => string.IsNullOrEmpty(sign) || x.Sign == sign)
                ;

            return query;
        }

        /// <summary>
        /// 契約単価・掛率（販売）の取得（単商品）
        /// </summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="custCode">顧客コード</param>
        /// <param name="sign">売上記号</param>
        public async Task<decimal?> GetKTankaRateSalesAsync(DateTime baseDate, string prodCode, string custCode, string sign)
        {
            var item = await GetKTankaSales(baseDate, prodCode, custCode, sign).SingleOrDefaultAsync();
            return item?.Rate;
        }

        /// <summary>
        /// 契約単価・掛率（仕入）の取得（単商品）
        /// </summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="supCode">仕入先コード</param>
        /// <param name="sign">売上記号</param>
        public async Task<decimal?> GetKTankaRatePurchasesAsync(DateTime baseDate, string prodCode, string supCode, string sign)
        {
            if (string.IsNullOrEmpty(prodCode)) { throw new ArgumentNullException(nameof(prodCode)); }
            if (string.IsNullOrEmpty(supCode)) { throw new ArgumentNullException(nameof(supCode)); }
            if (string.IsNullOrEmpty(sign)) { throw new ArgumentNullException(nameof(sign)); }

            var item = await GetKTankaPurchases(baseDate, prodCode, supCode, sign).SingleOrDefaultAsync();
            return item?.Rate;
        }

        /// <summary>
        /// 契約単価情報（販売）の取得（単商品）
        /// </summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="compCode">得意先コード</param>
        /// <param name="sign">売上記号</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<KTanka> GetKTankaSalesAsync(DateTime baseDate, string prodCode, string compCode, string sign)
        {
            if (string.IsNullOrEmpty(prodCode)) { throw new ArgumentNullException(nameof(prodCode)); }
            if (string.IsNullOrEmpty(compCode)) { throw new ArgumentNullException(nameof(compCode)); }
            if (string.IsNullOrEmpty(sign)) { throw new ArgumentNullException(nameof(sign)); }

            var ktanka = new KTanka();

            var item = await _hatFContext.ComSyohinMsts
                .Where(x => x.HatSyohin == prodCode)
                .SingleOrDefaultAsync();

            if (item != null)
            {
                // 価格の取得
                ktanka.SpecifiedPrice = GetComSyohinMstTanka(baseDate, item, sign);
            }

            // 掛率の取得
            decimal? rate = await GetKTankaRateSalesAsync(baseDate, prodCode, compCode, sign);
            ktanka.RatePercent = rate.HasValue ? rate.Value : 100m;

            if (ktanka.SpecifiedPrice.HasValue && ktanka.RatePercent.HasValue)
            {
                decimal calcRate = ktanka.RatePercent.Value * 0.01m;   // 例：25%⇒0.25
                decimal val = ktanka.SpecifiedPrice.Value * calcRate;

                // 掛率を反映した価格の計算
                // TODO: 四捨五入仕様の確認
                // 桁、銀行丸めにするか（仮で単純四捨五入としておく）
                ktanka.RatedPrice = Math.Round(val, 1, MidpointRounding.AwayFromZero);
            }

            return ktanka;
        }

        /// <summary>
        /// 商品マスタのレコードから契約単価(K単価)を取り出します
        /// </summary>
        /// <param name="baseDate">基準日</param>
        /// <param name="comSyohinMst"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public decimal? GetComSyohinMstTanka(DateTime baseDate, ComSyohinMst comSyohinMst, string sign)
        {
            // COM_SYOHIN_MST.xxxx_DATE が旧価格から新価格への切り替え日
            // COM_SYOHIN_MST.xxxx_OLD ⇒ 旧価格
            // COM_SYOHIN_MST.xxxx_NEW ⇒ 新価格

            decimal? kTanka = (sign ?? "").ToUpper() switch
            {
                // 定価
                "A" => comSyohinMst.TeikatanDate <= baseDate ? comSyohinMst.TeikatanNew : comSyohinMst.TeikatanOld,

                // 販売単価
                "B" => comSyohinMst.HanbaitanDate <= baseDate ? comSyohinMst.HanbaitanNew : comSyohinMst.HanbaitanOld,

                // 卸単価
                "C" => comSyohinMst.OroshitanDate <= baseDate ? comSyohinMst.OroshitanNew : comSyohinMst.OroshitanOld,

                // 非在庫品単価
                "D" => comSyohinMst.HizaikotanDate <= baseDate ? comSyohinMst.HizaikotanNew : comSyohinMst.HizaikotanOld,

                // 在庫品単価
                "E" => comSyohinMst.ZaikotanDate <= baseDate ? comSyohinMst.ZaikotanNew : comSyohinMst.ZaikotanOld,

                _ => throw new ArgumentOutOfRangeException(nameof(sign)),
            };

            return kTanka;
        }
        /// <summary>物件詳細の詳細データ取得</summary>
        /// <param name="constructionCode">物件番号</param>
        /// <returns>クエリ</returns>
        public async Task<List<ConstructionDetailEx>> GetConstructionDetailDetailAsync(string constructionCode)
        {
            var query = _hatFContext.ConstructionDetails
                                       .Where(x => x.ConstructionCode == constructionCode);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConstructionDetail, ConstructionDetailEx>();
            });
            var mapper = config.CreateMapper();
            var joinedQuery = AddJoinToConstructionDetailForDisplayName((IQueryable<ConstructionDetail>)query);

            // 名称列付オブジェクトにコピーして返す
            var src = await joinedQuery.ToListAsync();
            var dest = new List<ConstructionDetailEx>(src.Count);
            foreach (var srcItem in src)
            {
                var destItem = mapper.Map<ConstructionDetailEx>(srcItem.ConstructionDetail);
                destItem.ShiresakiName = srcItem.SupplierName;
                dest.Add(destItem);
            }
            return dest;

        }

        private IQueryable<ConstructionDetailSupplier> AddJoinToConstructionDetailForDisplayName(IQueryable<ConstructionDetail> query)
        {
            var newQuery = query
                    .GroupJoin(_hatFContext.SupplierMsts,
                        dest => dest.ShiresakiCd,
                        cust => cust.SupCode,
                        (dest, cust) => new { ConstructionDetail = dest, Supplier = cust }
                    )
                    .SelectMany(
                        x => x.Supplier.DefaultIfEmpty(),
                        (x, cust) => new
                        {
                            ConstructionDetail = x.ConstructionDetail,
                            Supplier = (cust != null) ? cust.SupName : "",
                        }
                    )
                    .OrderBy(x => x.ConstructionDetail.ConstructionCode)
                    .Select(x => new ConstructionDetailSupplier 
                    { ConstructionDetail = x.ConstructionDetail, SupplierName = x.Supplier });

            return newQuery;
        }

        // 内部処理用
        private class ConstructionDetailSupplier
        {
            public ConstructionDetail ConstructionDetail { get; set; }
            public string SupplierName { get; set; }
        }
    }
}