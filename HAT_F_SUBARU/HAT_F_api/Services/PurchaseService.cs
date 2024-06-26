using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.StateCodes;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;
using NLog.Targets.Wrappers;

namespace HAT_F_api.Services
{
    public class PurchaseService(HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, SequenceNumberService sequenceNumberService, UpdateInfoSetter updateInfoSetter)
    {
        private const string TaxRateCd = "B";

        private HatFContext _hatFContext = hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext = hatFApiExecutionContext;
        private SequenceNumberService _sequenceNumberService = sequenceNumberService;
        private UpdateInfoSetter _updateInfoSetter = updateInfoSetter;

        public class PurchaseData
        {
            public Pu Pu { get; set; }
            public List<PuDetail> PuDetails { get; set; } = new List<PuDetail>();
        }

        /// <summary>
        /// 仕入金額照合VIEWから仕入データ一覧を作成
        /// </summary>
        /// <param name="purchaseBillingDetails"></param>
        /// <returns></returns>
        public async Task<List<PurchaseData>> ToPurchaseDatasAsync(List<PurchaseBillingDetail> purchaseBillingDetails)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;
            Dictionary<string, PurchaseData> dataMap = [];
            foreach (var d in purchaseBillingDetails)
            {

                bool isHeaderNew = true;
                string hKey = d.M伝票番号;
                Pu pu = new();
                List<PuDetail> puDetails = [];

                if (dataMap.TryGetValue(hKey, out PurchaseData value))
                {
                    isHeaderNew = false;
                    pu = value.Pu;
                    puDetails = value.PuDetails;
                }

                if (isHeaderNew)
                {
                    if (string.IsNullOrEmpty(d.仕入番号))
                    {
                        var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                        pu.PuNo = $"{seqNo:D10}";
                        pu.StartDate = now;
                        pu.CreateDate = now;
                        pu.Creator = d.社員Id;
                        pu.EmpId = d.社員Id;
                    }
                    else
                    {
                        pu = await _hatFContext.Pus.Where(x => x.PuNo == d.仕入番号).FirstOrDefaultAsync();
                    }
                }

                pu.DenNo = d.M伝票番号;
                pu.PuDate = d.M納日;
                pu.SupCode = d.仕入先コード;
                pu.SupSubNo = d.仕入先コード枝番;
                pu.PoNo = d.Hat注文番号;
                pu.DeptCode = d.部門コード;
                pu.PuAmmount = 0; // 支払データの補正で設定
                pu.CmpTax = 0; // 支払データの補正で設定
                pu.HatOrderNo = d.Hat注文番号;
                pu.DenFlg = d.伝区;
                pu.SlipComment = d.備考;
                pu.UpdateDate = now;
                pu.Updater = d.社員Id;

                // 明細
                var puDetail = new PuDetail();
                if (!string.IsNullOrEmpty(d.仕入番号) && d.仕入行番号.HasValue)
                {
                    puDetail = await _hatFContext.PuDetails.Where(x => x.PuNo == d.仕入番号 && x.PuRowNo == d.仕入行番号).FirstOrDefaultAsync();
                    puDetail.PuRowDspNo = d.納品書番号;
                    puDetail.PoRowNo = d.子番;
                    puDetail.ProdCode = d.商品コード ?? string.Empty;
                    puDetail.ProdName = d.商品名;
                    puDetail.WhCode = d.倉庫コード;
                    puDetail.PoPrice = d.M単価.HasValue ? d.M単価 : 0;
                    puDetail.PuQuantity = (short)d.M数量;
                    puDetail.CheckStatus = d.照合ステータス;
                    puDetail.Chuban = d.H注番;
                    puDetail.TaxRateCd = string.IsNullOrEmpty(d.消費税) ? TaxRateCd : d.消費税;
                    puDetail.PuPayDate = d.支払日;
                    puDetail.UpdateDate = now;
                    puDetail.Updater = d.社員Id;

                    puDetails.Add(puDetail);
                }
                else
                {
                    puDetail.PuNo = pu.PuNo;
                    puDetail.PuRowNo = Int16.Parse($"{d.Hページ番号}{d.H行番号}"); ;
                    puDetail.PuRowDspNo = d.納品書番号;
                    puDetail.PoRowNo = d.子番;
                    puDetail.ProdCode = d.商品コード ?? string.Empty;
                    puDetail.ProdName = d.商品名;
                    puDetail.WhCode = d.倉庫コード;
                    puDetail.PoPrice = d.M単価.HasValue ? d.M単価 : 0;
                    puDetail.PuQuantity = (short)d.M数量;
                    puDetail.CheckStatus = d.照合ステータス;
                    puDetail.Chuban = d.H注番;
                    puDetail.TaxRateCd = string.IsNullOrEmpty(d.消費税) ? TaxRateCd : d.消費税;
                    puDetail.PuPayDate = d.支払日;
                    puDetail.CreateDate = now;
                    puDetail.Creator = d.社員Id;
                    puDetail.UpdateDate = now;
                    puDetail.Updater = d.社員Id;

                    puDetails.Add(puDetail);
                }

                if (isHeaderNew)
                {
                    PurchaseData data = new()
                    {
                        Pu = pu,
                        PuDetails = puDetails
                    };
                    dataMap.Add(hKey, data);
                }
            }

            return await CorrectPurchase([.. dataMap.Values]);
        }

        /// <summary>
        /// 仕入金額照合VIEWから仕入データ一覧を作成(Mデータ用)
        /// </summary>
        /// <param name="purchaseBillingDetails"></param>
        /// <returns></returns>
        public async Task<List<PurchaseData>> ToDeliveryPurchaseDatasAsync(List<PurchaseBillingDetail> purchaseBillingDetails)
        {
            var now = _hatFApiExecutionContext.ExecuteDateTimeJst;
            Dictionary<string, PurchaseData> dataMap = [];
            Pu pu = new();
            List<PuDetail> puDetails = [];
            bool isHeaderNew = true;
            string hKey = "mdata";
            short rowNo = 1;

            foreach (var d in purchaseBillingDetails)
            {

                if (isHeaderNew)
                {
                    if (string.IsNullOrEmpty(d.仕入番号))
                    {
                        var seqNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                        pu.PuNo = $"{seqNo:D10}";
                        pu.StartDate = now;
                        pu.CreateDate = now;
                        pu.Creator = d.社員Id;
                        pu.EmpId = d.社員Id;
                    }
                    else
                    {
                        pu = await _hatFContext.Pus.Where(x => x.PuNo == d.仕入番号).FirstOrDefaultAsync();
                        var query = await _hatFContext.PuDetails.Where(x => x.PuNo == d.仕入番号).ToListAsync();

                        rowNo = query.Max(x => x.PuRowNo);
                        rowNo++;
                    }

                    isHeaderNew = false;
                }

                pu.PuDate = d.M納日;
                pu.SupCode = d.仕入先コード;
                pu.SupSubNo = d.仕入先コード枝番;
                pu.DeptCode = d.部門コード;
                pu.PuAmmount = 0; // 支払データの補正で設定
                pu.CmpTax = 0; // 支払データの補正で設定
                pu.HatOrderNo = d.Hat注文番号;
                pu.UpdateDate = now;
                pu.Updater = d.社員Id;


                // 明細
                var puDetail = new PuDetail();
                if (!string.IsNullOrEmpty(d.仕入番号) && d.仕入行番号.HasValue)
                {
                    puDetail = await _hatFContext.PuDetails.Where(x => x.PuNo == d.仕入番号 && x.PuRowNo == d.仕入行番号).FirstOrDefaultAsync();
                    puDetail.PuRowDspNo = d.納品書番号;
                    puDetail.PoRowNo = d.子番;
                    puDetail.ProdCode = d.商品コード ?? string.Empty;
                    puDetail.ProdName = d.商品名;
                    puDetail.PoPrice = d.M単価.HasValue ? d.M単価 : 0;
                    puDetail.PuQuantity = (short)d.M数量;
                    puDetail.CheckStatus = 3;
                    puDetail.Chuban = d.Hat注文番号 + d.子番;
                    puDetail.TaxRateCd = string.IsNullOrEmpty(d.消費税) ? TaxRateCd : d.消費税;
                    puDetail.PuPayDate = d.支払日;
                    puDetail.UpdateDate = now;
                    puDetail.Updater = d.社員Id;

                    puDetails.Add(puDetail);
                }
                else
                {
                    puDetail.PuNo = pu.PuNo;
                    puDetail.PuRowNo = rowNo;
                    puDetail.PuRowDspNo = d.納品書番号;
                    puDetail.PoRowNo = d.子番;
                    puDetail.ProdCode = d.商品コード ?? string.Empty;
                    puDetail.ProdName = d.商品名;
                    puDetail.PoPrice = d.M単価.HasValue ? d.M単価 : 0;
                    puDetail.PuQuantity = (short)d.M数量;
                    puDetail.CheckStatus = 3;
                    puDetail.Chuban = d.Hat注文番号 + d.子番;
                    puDetail.TaxRateCd = string.IsNullOrEmpty(d.消費税) ? TaxRateCd : d.消費税;
                    puDetail.PuPayDate = d.支払日;
                    puDetail.CreateDate = now;
                    puDetail.Creator = d.社員Id;
                    puDetail.UpdateDate = now;
                    puDetail.Updater = d.社員Id;

                    puDetails.Add(puDetail);

                    rowNo++;
                }
            }

            PurchaseData data = new()
            {
                Pu = pu,
                PuDetails = puDetails
            };
            dataMap.Add(hKey, data);

            return await CorrectPurchase([.. dataMap.Values]);
        }

        /// <summary>
        /// 支払データの補正
        /// </summary>
        /// <param name="datas"></param>
        public async Task<List<PurchaseData>> CorrectPurchase(List<PurchaseData> datas)
        {
            foreach (PurchaseData data in datas)
            {

                var puDetails = data.PuDetails;
                var rowNo = puDetails.Select(x => x.PuRowNo).ToList();

                decimal puAmmountDb = 0;
                var detail = await _hatFContext.PuDetails.Where(x => x.PuNo == data.Pu.PuNo).ToListAsync();
                if (detail.Any())
                {
                    var antDetail = detail.Where(x => !rowNo.Contains(x.PuRowNo)).ToList();
                    puAmmountDb = antDetail.Any() ? (decimal)antDetail.Select(x => x.PoPrice * x.PuQuantity).Sum() : 0;
                }

                decimal puAmmount = (decimal)puDetails.Select(x => x.PoPrice * x.PuQuantity).Sum();

                data.Pu.PuAmmount = puAmmountDb + puAmmount;    // 仕入金額合計

                // TODO:消費税金額
            }
            return datas;
        }

        /// <summary>
        /// 支払データの変更
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public async Task<int> PutPurchaseDatasAsync(List<PurchaseData> datas)
        {

            foreach (PurchaseData data in datas)
            {
                if (_hatFContext.Pus.Any(e => e.PuNo == data.Pu.PuNo))
                {
                    _hatFContext.Pus.Update(data.Pu);
                }
                else
                {
                    _hatFContext.Pus.Add(data.Pu);
                }

                foreach (var puDetail in data.PuDetails)
                {
                    if (_hatFContext.PuDetails.Any(e => e.PuNo == puDetail.PuNo && e.PuRowNo == puDetail.PuRowNo))
                    {
                        _hatFContext.PuDetails.Update(puDetail);
                    }
                    else
                    {
                        _hatFContext.PuDetails.Add(puDetail);
                    }
                }
            }

            return await _hatFContext.SaveChangesAsync();
        }

        /// <summary>仕入取込データを取得する</summary>
        /// <param name="supCode">仕入先コード（必須）</param>
        /// <param name="hatOrderNo">Hat注文番号（省略可）</param>
        /// <param name="noubiFrom">納日（省略可）</param>
        /// <param name="noubiTo">納日（省略可）</param>
        /// <returns>仕入取込データ</returns>
        public async Task<List<PuImport>> GetPuImportAsync(
             string supCode, string hatOrderNo, DateTime? noubiFrom, DateTime? noubiTo)
        {
            return await _hatFContext.PuImports
                .Where(x => x.SupCode == supCode)
                .Where(x => string.IsNullOrEmpty(hatOrderNo) || x.HatOrderNo == hatOrderNo)
                .Where(x => !noubiFrom.HasValue || noubiFrom <= x.Noubi)
                .Where(x => !noubiTo.HasValue || x.Noubi <= noubiTo)
                .ToListAsync();
        }

        /// <summary>仕入取込データを更新する</summary>
        /// <param name="puImports">仕入取込データ</param>
        /// <returns>追加されたレコード数</returns>
        public async Task<int> PutPuImportAsync(IEnumerable<PuImport> puImports)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PuImport, PuImport>()
                    .ForMember(x => x.PuImportNo, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));
            var result = 0;

            // 消費税率はレコード数が少ないので先にすべて保持しておく
            var taxRates = await _hatFContext.DivTaxRates.ToListAsync();
            foreach (var item in puImports)
            {
                // 新規入力情報が既存レコードを重複することもあるため主キーでの検索はしない
                var exists = await _hatFContext.PuImports
                    .Where(x => x.SupCode == item.SupCode)
                    .Where(x => x.HatOrderNo == item.HatOrderNo)
                    .Where(x => x.Koban == item.Koban)
                    .SingleOrDefaultAsync();

                // 新規/更新共通の処理
                item.TaxRate = taxRates.SingleOrDefault(x => x.TaxRateCd == item.TaxFlg)?.TaxRate;

                // 新規
                if (exists == null)
                {
                    var newNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuImportNo);
                    item.PuImportNo = $"PUI{newNo:D7}";

                    var syohin = !string.IsNullOrEmpty(item.ProdCode) ? await _hatFContext.ComSyohinMsts
                        .Where(x => x.HatSyohin == item.ProdCode)
                        .SingleOrDefaultAsync() : null;
                    item.ProdName = syohin?.SyohinName;

                    _updateInfoSetter.SetUpdateInfo(item);
                    _hatFContext.PuImports.Add(item);
                    result++;
                }
                // 更新
                else
                {
                    // 商品コードが変化している場合のみ商品名を検索する
                    if (!string.IsNullOrEmpty(item.ProdCode) && exists.ProdCode != item.ProdCode)
                    {
                        var syohin = await _hatFContext.ComSyohinMsts.SingleOrDefaultAsync(x => x.HatSyohin == item.ProdCode);
                        item.ProdName = syohin?.SyohinName;
                    }
                    mapper.Map(item, exists);
                    _updateInfoSetter.SetUpdateInfo(exists);
                    _hatFContext.PuImports.Update(exists);
                }
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>仕入明細情報（ヘッダ含む）を取得</summary>
        /// <param name="supCode">仕入先コード</param>
        /// <returns>仕入明細情報</returns>
        public async Task<List<ViewPuDetail>> GetViewPuDetailsAsync(string supCode)
        {
            return await _hatFContext.ViewPuDetails
                .Where(x => x.SupCode == supCode)
                .ToListAsync();
        }

        /// <summary>仕入テーブルを更新する</summary>
        /// <param name="billingDetails">更新内容</param>
        /// <returns>追加した行数</returns>
        public async Task UpdatePuAsync(IEnumerable<PurchaseBillingDetail> billingDetails)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PurchaseBillingDetail, PuKoiso>()
                    .ForMember(x => x.PuDate, x => x.MapFrom(source => source.M納日))
                    .ForMember(x => x.SupCode, x => x.MapFrom(source => source.仕入先コード))
                    .ForMember(x => x.SupSubNo, x => x.MapFrom(source => source.仕入先コード枝番))
                    .ForMember(x => x.PaySupCode, x => x.MapFrom(source => source.支払先コード))
                    .ForMember(x => x.EmpId, x => x.MapFrom(source => source.社員Id))
                    .ForMember(x => x.StartDate, x => x.MapFrom(source => _hatFApiExecutionContext.ExecuteDateTimeJst))
                    // TODO NOT NULL対応
                    .ForMember(x => x.DeptCode, x => x.MapFrom(source => source.部門コード ?? string.Empty))
                    .ForMember(x => x.HatOrderNo, x => x.MapFrom(source => source.Hat注文番号))
                    // 除外メンバー
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));
            var group = billingDetails.GroupBy(x => new { x.仕入番号, supSubNo = x.仕入行番号, x.M納日 }).Select(x => x.First());
            foreach (var item in group)
            {
                // 新規入力情報が既存レコードを重複することもあるため主キーでの検索はしない
                // 仕入先コード、納品日、商品コードで検索したいが仕入先コードがPUにあるためVIEWから既存確認をする
                var exists = await _hatFContext.PuKoisos
                    .Where(x => x.SupCode == item.仕入先コード)
                    .Where(x => x.SupSubNo == (item.仕入先コード枝番 ?? 0))
                    .Where(x => x.PuDate == item.M納日)
                    // PU_DETAILS更新時に新規登録も済ませているのでnullにはならないはず
                    .SingleOrDefaultAsync() ?? throw new KeyNotFoundException("更新対象の仕入ヘッダ情報がありません。");

                mapper.Map(item, exists);
                exists.PuAmmount = await _hatFContext.PuDetailsKoisos
                    .Where(x => x.PuNo == exists.PuNo)
                    .Where(x => x.DelFlg != DelFlg.Deleted)
                    .SumAsync(x => x.PoPrice * x.PuQuantity);

                exists.CmpTax = (await _hatFContext.PuDetailsKoisos
                    .Where(x => x.PuNo == exists.PuNo)
                    .Where(x => x.DelFlg != DelFlg.Deleted)
                    .SumAsync(x => (x.PoPrice * x.PuQuantity) * x.TaxRate / 100)) ?? 0;

                _updateInfoSetter.SetUpdateInfo(exists);
            }
            await _hatFContext.SaveChangesAsync();
        }


        // TODO ★削除予定
        /// <summary>仕入テーブルを更新する</summary>
        /// <param name="viewPuDetails">更新内容</param>
        /// <returns>追加した行数</returns>
        public async Task UpdatePuAsync(IEnumerable<ViewPuDetail> viewPuDetails)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ViewPuDetail, PuKoiso>()
                    .ForMember(x => x.StartDate, x => x.MapFrom(source => _hatFApiExecutionContext.ExecuteDateTimeJst))
                    // 除外メンバー
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.DeptCode, x => x.Ignore())
                    .ForMember(x => x.EmpId, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));
            var group = viewPuDetails.GroupBy(x => new { x.PuNo, supSubNo = 0, x.Noubi }).Select(x => x.First());
            foreach (var item in group)
            {
                // 新規入力情報が既存レコードを重複することもあるため主キーでの検索はしない
                // 仕入先コード、納品日、商品コードで検索したいが仕入先コードがPUにあるためVIEWから既存確認をする
                var exists = await _hatFContext.PuKoisos
                    .Where(x => x.SupCode == item.SupCode)
                    .Where(x => x.SupSubNo == 0)
                    .Where(x => x.PuDate == item.Noubi)
                    // PU_DETAILS更新時に新規登録も済ませているのでnullにはならないはず
                    .SingleOrDefaultAsync() ?? throw new KeyNotFoundException("更新対象の仕入ヘッダ情報がありません。");

                mapper.Map(item, exists);
                exists.PuAmmount = await _hatFContext.PuDetailsKoisos
                    .Where(x => x.PuNo == exists.PuNo)
                    .Where(x => x.DelFlg != DelFlg.Deleted)
                    .SumAsync(x => x.PoPrice * x.PuQuantity);

                exists.CmpTax = (await _hatFContext.PuDetailsKoisos
                    .Where(x => x.PuNo == exists.PuNo)
                    .Where(x => x.DelFlg != DelFlg.Deleted)
                    .SumAsync(x => (x.PoPrice * x.PuQuantity) * x.TaxRate / 100)) ?? 0;

                _updateInfoSetter.SetUpdateInfo(exists);
            }
            await _hatFContext.SaveChangesAsync();
        }

        /// <summary>仕入明細テーブルを追加/更新する</summary>
        /// <param name="billingDetails">更新内容</param>
        /// <returns>追加した行数</returns>
        public async Task<int> UpsertPuDetailsAsync(IEnumerable<PurchaseBillingDetail> billingDetails)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PurchaseBillingDetail, PuKoiso>()
                    .ForMember(x => x.SupCode, x => x.MapFrom(source => source.仕入先コード))
                    .ForMember(x => x.SupSubNo, x => x.MapFrom(source => 0))
                    .ForMember(x => x.PaySupCode, x => x.MapFrom(source => source.支払先コード))
                    .ForMember(x => x.PuDate, x => x.MapFrom(source => source.M納日))
                    .ForMember(x => x.StartDate, x => x.MapFrom(source => _hatFApiExecutionContext.ExecuteDateTimeJst))
                    // TODO NULL不許可なのでとりあえず空白
                    .ForMember(x => x.DeptCode, x => x.MapFrom(source => string.Empty))
                    // 除外メンバー
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.EmpId, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
                cfg.CreateMap<PurchaseBillingDetail, PuDetailsKoiso>()
                    .ForMember(x => x.PuRowDspNo, x => x.MapFrom(source => source.納品書番号))
                    .ForMember(x => x.Noubi, x => x.MapFrom(source => source.M納日))
                    .ForMember(x => x.HatOrderNo, x => x.MapFrom(source => source.Hat注文番号))
                    .ForMember(x => x.PoRowNo, x => x.MapFrom(source => source.H行番号))
                    .ForMember(x => x.ProdCode, x => x.MapFrom(source => source.商品コード))
                    .ForMember(x => x.ProdName, x => x.MapFrom(source => source.商品名))
                    .ForMember(x => x.WhCode, x => x.MapFrom(source => source.倉庫コード))
                    .ForMember(x => x.PoPrice, x => x.MapFrom(source => source.M単価))
                    .ForMember(x => x.PuQuantity, x => x.MapFrom(source => source.M数量))
                    .ForMember(x => x.TaxFlg, x => x.MapFrom(source => source.消費税))
                    .ForMember(x => x.CheckStatus, x => x.MapFrom(source => source.照合ステータス))
                    .ForMember(x => x.PuKbn, x => x.MapFrom(source => source.区分))
                    .ForMember(x => x.PuPayYearMonth, x => x.MapFrom(source => source.支払日))
                    .ForMember(x => x.PuPayDate, x => x.MapFrom(source => source.支払日))
                    .ForMember(x => x.Chuban, x => x.MapFrom(source => source.M注番))
                    .ForMember(x => x.DenNo, x => x.MapFrom(source => source.M伝票番号))
                    .ForMember(x => x.DenFlg, x => x.MapFrom(source => source.伝区))
                    // 以下の除外メンバー以外で、同名のものはそのまま取り込む
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.PuRowNo, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));

            var result = 0;
            // PU_DETAILSを更新する
            foreach (var item in billingDetails)
            {
                // 新規入力情報が既存レコードを重複することもあるため主キーでの検索はしない
                // 仕入先コード、納品日、商品コードで検索したいが仕入先コードがPUにあるためVIEWから既存確認をする
                var viewExists = await _hatFContext.ViewPuDetails
                    .Where(x => x.SupCode == item.仕入先コード)
                    .Where(x => x.Noubi == item.M納日)
                    .Where(x => (x.ProdCode ?? x.ProdName) == (item.商品コード ?? item.商品名))
                    .SingleOrDefaultAsync();

                // 更新
                if (viewExists != null)
                {
                    var exists = _hatFContext.PuDetailsKoisos
                        .Where(x => x.PuNo == viewExists.PuNo)
                        .Where(x => x.PuRowNo == viewExists.PuRowNo)
                        .Where(x => x.DelFlg != DelFlg.Deleted)
                        .First();
                    mapper.Map(item, exists);
                    _updateInfoSetter.SetUpdateInfo(exists);
                }
                // 追加
                else
                {
                    // 追加対象となるPUテーブルを検索
                    var header = await _hatFContext.PuKoisos
                        .Where(x => x.SupCode == item.仕入先コード)
                        .Where(x => x.SupSubNo == 0)
                        .Where(x => x.PuDate == item.M納日)
                        .SingleOrDefaultAsync();
                    // 該当するものがなければ主キーだけ設定して追加しておく
                    if (header == null)
                    {
                        var newNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                        header = mapper.Map<PurchaseBillingDetail, PuKoiso>(item);
                        header.PuNo = $"{newNo:D10}";
                        _updateInfoSetter.SetUpdateInfo(newNo);
                        _hatFContext.PuKoisos.Add(header);
                    }
                    var maxPuRowNo = (await _hatFContext.PuDetailsKoisos.Where(x => x.PuNo == header.PuNo).MaxAsync(x => x.PuRowNo as short?)) ?? 0;
                    var newDetail = mapper.Map<PurchaseBillingDetail, PuDetailsKoiso>(item);
                    newDetail.PuNo = header.PuNo;
                    newDetail.PuRowNo = (short)(maxPuRowNo + 1);

                    _updateInfoSetter.SetUpdateInfo(newDetail);
                    _hatFContext.PuDetailsKoisos.Add(newDetail);
                    result++;
                }
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        // TODO ★削除予定
        /// <summary>仕入明細テーブルを追加/更新する</summary>
        /// <param name="viewPuDetails">更新内容</param>
        /// <returns>追加した行数</returns>
        public async Task<int> UpsertPuDetailsAsync(IEnumerable<ViewPuDetail> viewPuDetails)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ViewPuDetail, PuKoiso>()
                    .ForMember(x => x.SupSubNo, x => x.MapFrom(source => 0))
                    .ForMember(x => x.PuDate, x => x.MapFrom(source => source.Noubi))
                    .ForMember(x => x.StartDate, x => x.MapFrom(source => _hatFApiExecutionContext.ExecuteDateTimeJst))
                    // TODO NULL不許可なのでとりあえず空白
                    .ForMember(x => x.DeptCode, x => x.MapFrom(source => string.Empty))
                    // 除外メンバー
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.EmpId, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
                cfg.CreateMap<ViewPuDetail, PuDetailsKoiso>()
                    .ForMember(x => x.Chuban, x => x.MapFrom(source =>
                        (!string.IsNullOrEmpty(source.HatOrderNo) && !string.IsNullOrEmpty(source.PoRowNo)) ? $"{source.HatOrderNo}{source.PoRowNo}" : null))
                    // 以下の除外メンバー以外で、同名のものはそのまま取り込む
                    .ForMember(x => x.PuNo, x => x.Ignore())
                    .ForMember(x => x.PuRowNo, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));
            // 仕入先マスタの内容を先に取得しておく
            var supCodes = viewPuDetails.GroupBy(x => x.SupCode).Select(x => x.Key).ToList();
            var suppliers = await _hatFContext.SupplierMsts.Where(x => supCodes.Contains(x.SupCode))
                .ToDictionaryAsync(x => x.SupCode, x => x);

            var result = 0;
            // PU_DETAILSを更新する
            foreach (var item in viewPuDetails)
            {
                // 新規入力情報が既存レコードを重複することもあるため主キーでの検索はしない
                // 仕入先コード、納品日、商品コードで検索したいが仕入先コードがPUにあるためVIEWから既存確認をする
                var viewExists = await _hatFContext.ViewPuDetails
                    .Where(x => x.SupCode == item.SupCode)
                    .Where(x => x.Noubi == item.Noubi)
                    .Where(x => (x.ProdCode ?? x.ProdName) == (item.ProdCode ?? item.ProdName))
                    .SingleOrDefaultAsync();

                // 更新
                if (viewExists != null)
                {
                    var exists = _hatFContext.PuDetailsKoisos
                        .Where(x => x.PuNo == viewExists.PuNo)
                        .Where(x => x.PuRowNo == viewExists.PuRowNo)
                        .Where(x => x.DelFlg != DelFlg.Deleted)
                        .First();
                    mapper.Map(item, exists);
                    var supplier = suppliers[item.SupCode];
                    exists.PuPayYearMonth = DateTimeUtil.GetPayDate(item.Noubi, supplier.SupCloseDate, supplier.SupPayMonths, supplier.SupPayDates);
                    exists.PuPayDate = DateTimeUtil.GetPayDate(item.Noubi, supplier.SupCloseDate, supplier.SupPayMonths, supplier.SupPayDates);
                    _updateInfoSetter.SetUpdateInfo(exists);
                }
                // 追加
                else
                {
                    // 追加対象となるPUテーブルを検索
                    var header = await _hatFContext.PuKoisos
                        .Where(x => x.SupCode == item.SupCode)
                        .Where(x => x.SupSubNo == 0)
                        .Where(x => x.PuDate == item.Noubi)
                        .SingleOrDefaultAsync();
                    // 該当するものがなければ主キーだけ設定して追加しておく
                    if (header == null)
                    {
                        var newNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuPuNo);
                        header = mapper.Map<ViewPuDetail, PuKoiso>(item, opt => opt.AfterMap((src, dest) =>
                        {
                            dest.PuNo = $"{newNo:D10}";
                        }));
                        _updateInfoSetter.SetUpdateInfo(newNo);
                        _hatFContext.PuKoisos.Add(header);
                    }
                    var maxPuRowNo = (await _hatFContext.PuDetailsKoisos.Where(x => x.PuNo == header.PuNo).MaxAsync(x => x.PuRowNo as short?)) ?? 0;
                    var newDetail = mapper.Map<ViewPuDetail, PuDetailsKoiso>(item, opt => opt.AfterMap((src, dest) =>
                    {
                        dest.PuNo = header.PuNo;
                        dest.PuRowNo = (short)(maxPuRowNo + 1);

                        var supplier = suppliers.ContainsKey(src.SupCode) ? suppliers[src.SupCode] : null;
                        dest.PuPayYearMonth = DateTimeUtil.GetPayDate(src.Noubi, supplier?.SupCloseDate, supplier?.SupPayMonths, supplier?.SupPayDates);
                        dest.PuPayDate = DateTimeUtil.GetPayDate(src.Noubi, supplier?.SupCloseDate, supplier?.SupPayMonths, supplier?.SupPayDates);
                        _updateInfoSetter.SetUpdateInfo(dest);
                    }));
                    _hatFContext.PuDetailsKoisos.Add(newDetail);
                    result++;
                }
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>仕入明細情報の削除</summary>
        /// <param name="viewPuDetails">仕入情報</param>
        /// <returns>削除件数</returns>
        public async Task<int> DeletePuDetailAsync(IEnumerable<ViewPuDetail> viewPuDetails)
        {
            var keys = viewPuDetails.Select(x => new { x.PuNo, x.PuRowNo });
            var targets = await _hatFContext.PuDetailsKoisos
                .Where(x => keys.Contains(new { x.PuNo, x.PuRowNo }))
                .ToListAsync();
            targets.ForEach(x => x.DelFlg = DelFlg.Deleted);
            await _hatFContext.SaveChangesAsync();
            return targets.Count;
        }
    }
}
