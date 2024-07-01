using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
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
                    var newNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuImportPuImportNo);
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
    }
}
