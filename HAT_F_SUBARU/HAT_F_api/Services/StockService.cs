using AutoMapper;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HAT_F_api.Services
{
    public class StockService
    {
        private const string StockTypeHatFOwn = "1";
        private const string QuarityTypeGood = "G";

        private static IConfiguration _configuration;
        private static NLog.ILogger _logger;
        private HatFContext _hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext;
        private UpdateInfoSetter _updateInfoSetter;

        public StockService(IConfiguration configuration, NLog.ILogger logger, HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, UpdateInfoSetter updateInfoSetter)
        {
            _configuration = configuration;
            _logger = logger;
            _hatFContext = hatFContext;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _updateInfoSetter = updateInfoSetter;
        }

        public async Task<short?> GetProductStockValidCountAsync(string whCode, string prodCode)
        {
            var query = _hatFContext.Stocks
                .Where(x => x.WhCode == whCode)
                .Where(x => x.ProdCode == prodCode)
                //.Where(x => string.IsNullOrEmpty(x.RotNo))    //主キーから除外予定
                .Where(x => x.StockType == "1")     // 自社在庫
                .Where(x => x.QualityType == "G")   // 良品
                ;
            if (await query.AnyAsync())
            {
                var record = await query.FirstOrDefaultAsync();
                return record.Valid;   // 有効在庫数
            }
            return null;
        }

        /// <summary>在庫テーブルの有効在庫数を更新し、在庫引き当てテーブルに登録する</summary>
        /// <param name="whCode">倉庫コード</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="quantity">数量。対象商品が既に引き当て済みの場合は差分値を指定すること。</param>
        /// <param name="denNo">伝票番号</param>
        /// <returns>STOCKテーブルの更新件数</returns>
        /// <remarks>
        /// <para>このメソッドは複数のテーブルを操作するためトランザクションが必要。</para>
        /// <para>メソッド外でトランザクションを作成すると二重作成でエラーとなるため引数としてトランザクションを強制する</para>
        /// </remarks>
        /// <exception cref="ArgumentException">トランザクションがnullの場合に発生する</exception>
        public async Task<int> PutProductStockReserveAsync(string whCode, string prodCode, short quantity, string denNo)
        {
            if (_hatFContext.Database.CurrentTransaction == null)
            {
                throw new InvalidOperationException("本メソッドはトランザクションを開始してから実行してください。");
            }

            // 有効在庫数が引当数と同数以上を条件に有効在庫数を減らす (UPDATE実行)
            var query = _hatFContext.Stocks
                .Where(x => x.WhCode == whCode)
                .Where(x => x.ProdCode == prodCode)
                .Where(x => x.StockType == StockTypeHatFOwn)    // 自社在庫
                .Where(x => x.QualityType == QuarityTypeGood)   // 良品
                .Where(x => x.Valid >= quantity);                // 引当数が足りることを条件にする

            int stockUpdateCount = await query.ExecuteUpdateAsync(s => s  //↓更新内容
                    .SetProperty(stock => stock.Valid, stock => stock.Valid - quantity) // 有効在庫数を減らす
                    .SetProperty(stock => stock.UpdateDate, stock => _updateInfoSetter.EntityUpdateDateTimeJst)
                    .SetProperty(stock => stock.Updater, stock => _updateInfoSetter.EntityUpdaterId)
                   );
            // ↑SELECTとUPDATEを分けると他のセッションで割り込みが起きる可能性があるので一発で更新する
            // ※SaveChangesではなく、このタイミングでSQL実行されるはずなので明示的トランザクション必要)

            if (0 == stockUpdateCount)
            {
                return 0;
            }

            await PutStockReserveAsync(new StockReserve()
            {
                StockReserveId = 0,
                StockReserveDatetime = _hatFApiExecutionContext.ExecuteDateTimeJst,
                DenNo = denNo,
                WhCode = whCode,
                ProdCode = prodCode,
                StockType = StockTypeHatFOwn,//自社在庫
                Quantity = quantity,
                WarehousingCause = 0,//通常引当
            });

            await _hatFContext.SaveChangesAsync();

            return stockUpdateCount;
        }

        /// <summary>引き当て済みの商品情報を取得する</summary>
        /// <param name="denNo">伝票番号</param>
        /// <param name="whCode">倉庫コード</param>
        /// <param name="prodCode">商品コード(省略可)</param>
        /// <returns>予約情報</returns>
        public IQueryable<StockReserve> GetStockReserveQuery(string denNo, string whCode, string prodCode)
        {
            return _hatFContext.StockReserves
                .Where(x => x.DenNo == denNo)
                .Where(x => x.WhCode == whCode)
                .Where(x => string.IsNullOrEmpty(prodCode) || x.ProdCode == prodCode);
        }

        /// <summary>商品引き当て</summary>
        /// <param name="stockReserve">予約情報</param>
        /// <returns>非同期タスク</returns>
        public async Task PutStockReserveAsync(StockReserve stockReserve)
        {
            _updateInfoSetter.SetUpdateInfo(stockReserve);

            var exists = await GetStockReserveQuery(stockReserve.DenNo, stockReserve.WhCode, stockReserve.ProdCode)
                .SingleOrDefaultAsync();

            if (exists is null)
            {
                // 新規の場合はそのまま登録する
                _hatFContext.StockReserves.Add(stockReserve);
            }
            else
            {
                // 既存の場合は引数の数量を差分として更新する
                exists.Quantity += stockReserve.Quantity;
                exists.UpdateDate = stockReserve.UpdateDate;
                exists.Updater += stockReserve.Updater;
            }
            await _hatFContext.SaveChangesAsync();
        }

        /// <summary>商品を予約する</summary>
        /// <param name="pages">受発注画面情報</param>
        /// <returns>非同期タスク</returns>
        /// <exception cref="HatFApiServiceException">予約失敗</exception>
        public async Task ReserveProductAsync(FosJyuchuPages pages)
        {
            // 伝票番号、倉庫コード、商品コードでグルーピングして予約する
            var group = pages.AlivePages
                .SelectMany(page => page.FosJyuchuDs.Select(detail => new { Header = page.FosJyuchuH, Detail = detail }))
                .Where(x => !string.IsNullOrEmpty(x.Detail.SyohinCd))
                .GroupBy(x => new
                {
                    x.Header.DenNo,
                    SokoCd = string.IsNullOrEmpty(x.Detail.SokoCd) ? x.Header.SokoCd : x.Detail.SokoCd,
                    x.Detail.SyohinCd
                });

            foreach (var syohin in group)
            {
                // 商品マスタに存在しない商品は無視
                if(!await _hatFContext.ComSyohinMsts.AnyAsync(x => x.HatSyohin == syohin.Key.SyohinCd))
                {
                    continue;
                }

                // 予約済みを確認して予約すべき数を算出
                var reservedProduct = await GetStockReserveQuery(syohin.Key.DenNo, syohin.Key.SokoCd, syohin.Key.SyohinCd)
                    .FirstOrDefaultAsync();
                var count = syohin.Sum(s => s.Detail.Bara) - (reservedProduct?.Quantity ?? 0);
                if (count != 0)
                {
                    // TODO バラ数をintかshortか統一する
                    var reservedCount = await PutProductStockReserveAsync(
                        syohin.Key.SokoCd, syohin.Key.SyohinCd, (short)count, syohin.Key.DenNo);
                    if (reservedCount <= 0)
                    {
                        var message = $"商品在庫の予約に失敗しました。{Environment.NewLine}商品コード：{syohin.Key.SyohinCd}";
                        throw new HatFApiServiceException(message);
                    }
                }
            }
        }

        /// <summary>無効な引き当て情報を削除する</summary>
        /// <param name="pages">受発注画面情報</param>
        /// <returns>非同期タスク</returns>
        public async Task RefleshReserveAsync(FosJyuchuPages pages)
        {
            // 伝票番号と倉庫コードでグルーピングして確認していく
            var group = pages.AlivePages
                .SelectMany(page => page.FosJyuchuDs.Select(detail => new { Header = page.FosJyuchuH, Detail = detail }))
                .GroupBy(x => new
                {
                    x.Header.DenNo,
                    SokoCd = string.IsNullOrEmpty(x.Detail.SokoCd) ? x.Header.SokoCd : x.Detail.SokoCd,
                });
            foreach(var details in group)
            {
                // 予約済みを確認して予約すべき数を算出
                var reserves = await GetStockReserveQuery(details.Key.DenNo, details.Key.SokoCd, null).ToListAsync();

                // 予約数が0のレコードを削除する
                foreach (var reserve in reserves.Where(r => r.Quantity == 0))
                {
                    await DeleteStockReserveAsync(reserve);
                }

                // 現在の注文にない予約を削除する
                var targets = reserves.Where(r => !details.Select(d => d.Detail.SyohinCd).Contains(r.ProdCode));
                foreach(var target in targets)
                {
                    var reservedCount = await PutProductStockReserveAsync(
                        target.WhCode, target.ProdCode, (short)-target.Quantity, target.DenNo);
                    await DeleteStockReserveAsync(target);
                }
            }
        }

        /// <summary>商品引き当ての削除</summary>
        /// <param name="stockReserve">削除対象の</param>
        /// <returns>非同期タスク</returns>
        private async Task DeleteStockReserveAsync(StockReserve stockReserve)
        {
            _hatFContext.StockReserves.Remove(stockReserve);
            await _hatFContext.SaveChangesAsync();
        }



        public async Task<int> PutStockInventoriesAsync(List<StockInventory> data)
        {
            _updateInfoSetter.SetUpdateInfo(data);

            foreach (var item in data)
            {
                var query = _hatFContext.StockInventories
                    .Where(x => x.WhCode == item.WhCode)
                    .Where(x => x.InventoryYearmonth == item.InventoryYearmonth)
                    .Where(x => x.ProdCode == item.ProdCode)
                    .Where(x => x.StockType == item.StockType)
                    .Where(x => x.QualityType == item.QualityType)
                    ;

                var exists = await query.SingleOrDefaultAsync();
                if (exists != null)
                {
                    //mapper.Map(item, exists);
                    exists.Actual = item.Actual;
                    _updateInfoSetter.SetUpdateInfo(item);
                }
                else
                {
                    await _hatFContext.StockInventories.AddAsync(item);
                    var newItem = new StockInventory();
                }
            }

            int rowsAffected = await _hatFContext.SaveChangesAsync();
            return rowsAffected;
        }

        /// <summary>
        /// 棚卸情報取得
        /// </summary>
        /// <param name="inventoryYearMonth">棚卸年月</param>
        /// <param name="filterCode">0:全件, 1:差異あり, 2:差異なし, 3:異常値(分類), 4:異常値(商品)</param>
        /// <param name="whCode"></param>
        /// <param name="prodCode"></param>
        /// <param name="stockType"></param>
        /// <param name="qualityType"></param>
        /// <returns></returns>
        public IQueryable<ViewStockInventory> GetViewStockInventories(DateTime? inventoryYearMonth, string filterCode, string whCode, string prodCode, string stockType, string qualityType)
        {
            var query = _hatFContext.ViewStockInventories.Join(
                    _hatFContext.ComSyohinMsts,
                    inv => inv.商品cd,
                    syohin => syohin.HatSyohin,
                    (inv, syohin) => new { ViewStockInventory = inv, ComSyohinMsts = syohin }
                )
                .Where(x => !inventoryYearMonth.HasValue || x.ViewStockInventory.棚卸年月 == inventoryYearMonth)
                .Where(x => string.IsNullOrEmpty(whCode) || x.ViewStockInventory.倉庫cd == whCode)
                .Where(x => string.IsNullOrEmpty(prodCode) || x.ViewStockInventory.商品cd.Contains(prodCode))
                .Where(x => string.IsNullOrEmpty(stockType) || x.ViewStockInventory.在庫区分 == stockType)
                .Where(x => string.IsNullOrEmpty(qualityType) || x.ViewStockInventory.良品区分 == qualityType)
                ;

            switch (filterCode)
            {
                case "0": // 全件
                    break;

                case "1": // 差異あり
                    query = query.Where(x => x.ViewStockInventory.管理在庫数 != x.ViewStockInventory.棚卸在庫数);
                    break;

                case "2": // 差異なし
                    query = query.Where(x => x.ViewStockInventory.管理在庫数 == x.ViewStockInventory.棚卸在庫数);
                    break;

                case "3": // 異常値(分類)：管材の場合50個以上の差異があるもの、それ以外の商品では2個以上の差異があるもの
                    string kanzaiCd5 = "[A-E]%";  //管財類の分類コード：先頭１桁がA～E
                    query = query.Where(x =>
                        (
                            // ↓ 分類が管財類なら、差異絶対値>=50
                            x.ViewStockInventory.棚卸在庫数 != null     // 棚卸未入力は除外
                            && x.ViewStockInventory.商品cd == x.ComSyohinMsts.HatSyohin  // 商品マスタのインデックス検索のため消さないこと
                            && EF.Functions.Like(x.ComSyohinMsts.SyohinBunrui, kanzaiCd5)
                            && Math.Abs(x.ViewStockInventory.管理在庫数 ?? 0 - x.ViewStockInventory.棚卸在庫数 ?? 0) >= 50
                        )
                        ||
                        (
                            // ↓ 分類が管財類【以外】なら、差異絶対値>=2
                            x.ViewStockInventory.棚卸在庫数 != null     // 棚卸未入力は除外
                            && x.ViewStockInventory.商品cd == x.ComSyohinMsts.HatSyohin  // 商品マスタのインデックス検索のため消さないこと
                            && !(EF.Functions.Like(x.ComSyohinMsts.SyohinBunrui, kanzaiCd5))
                            && Math.Abs(x.ViewStockInventory.管理在庫数 ?? 0 - x.ViewStockInventory.棚卸在庫数 ?? 0) >= 2
                        )
                    );
                    break;

                case "4": // 異常値(商品)：1ケース以上の差異があるもの
                    query = query.Where(x =>
                        (
                            // ↓ 入数大=0 AND 入数小>0なら、差異絶対値>=入数小
                            x.ViewStockInventory.棚卸在庫数 != null     // 棚卸未入力は除外
                            && x.ViewStockInventory.商品cd == x.ComSyohinMsts.HatSyohin  // 商品マスタのインデックス検索のため消さないこと
                            && x.ComSyohinMsts.IrisuDai == 0 && x.ComSyohinMsts.IrisuSho > 0
                            && Math.Abs(x.ViewStockInventory.管理在庫数 ?? 0 - x.ViewStockInventory.棚卸在庫数 ?? 0) >= x.ComSyohinMsts.IrisuSho
                        )
                        ||
                        (
                            // ↓ 入数大>0なら、差異絶対値>=入数大
                            x.ViewStockInventory.棚卸在庫数 != null     // 棚卸未入力は除外
                            && x.ViewStockInventory.商品cd == x.ComSyohinMsts.HatSyohin  // 商品マスタのインデックス検索のため消さないこと
                            && x.ComSyohinMsts.IrisuDai > 0
                            && Math.Abs(x.ViewStockInventory.管理在庫数 ?? 0 - x.ViewStockInventory.棚卸在庫数 ?? 0) >= x.ComSyohinMsts.IrisuDai
                        )
                    );

                    break;

                default:
                    //指定なし
                    break;
            }

            System.Diagnostics.Debug.WriteLine(query.ToQueryString());
            int count = query.Count();
            System.Diagnostics.Debug.WriteLine($"count:{count}");

            query = query
                .OrderBy(x=>x.ViewStockInventory.在庫区分)
                .ThenBy(x => x.ViewStockInventory.棚卸no);

            return query.Select(x => x.ViewStockInventory);
        }

        /// <summary>
        /// 棚卸用データを作成する（棚卸用在庫数の確定）
        /// </summary>
        /// <param name="whCode"></param>
        /// <param name="inventoryYearMonth"></param>
        /// <returns></returns>
        public async Task<int> PutStockInventoriesNewAsync(string whCode, DateTime inventoryYearMonth)
        {
            // コピー先を削除（2回目以降の実行を想定）
            _ = await _hatFContext.StockInventories
                .Where(x => x.WhCode == whCode)
                .Where(x => x.InventoryYearmonth == inventoryYearMonth)
                .ExecuteDeleteAsync();

            // 在庫区分 1:自社在庫、2:預かり在庫（マルマ）
            string[] stockTypes = new [] { "1", "2" };

            foreach (string stockType in stockTypes)
            {
                var stockQuery = _hatFContext.Stocks
                    .Where(x => x.WhCode == whCode)
                    .Where(x => x.StockType == stockType)
                    .Where(x => x.QualityType == "G")   //良品区分 対象外でよい？＞F:不良品、U:未検品
                    .OrderBy(x => x.ProdCode)
                    ;

                int i = 1;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Stock, StockInventory>()
                        .ForMember(m => m.Managed, opt => opt.MapFrom(src => src.Actual))   // 在庫.実在庫数を在庫棚卸.管理在庫数に
                        .AfterMap((src, dest) => { dest.Actual = null; })
                        .AfterMap((src, dest) => { dest.InventoryYearmonth = inventoryYearMonth; })
                        .AfterMap((src, dest) => { dest.StockNo = i++; });
                });
                var mapper = config.CreateMapper();

                await foreach (var stock in stockQuery.AsAsyncEnumerable())
                {
                    var inventory = mapper.Map<StockInventory>(stock);
                    _hatFContext.StockInventories.Add(inventory);
                }
            }

            int count = await _hatFContext.SaveChangesAsync();
            return count;
        }

        private string CombineString(string string1, string string2)
        {
            string newString = string.IsNullOrEmpty(string1) ? string2 : $"{string1}, {string2}";
            return newString;
        }

        public async Task<List<StockInventoryAmazon>> PutStockInventoryAmazonsAsync(string whCode, DateTime inventoryYearMonth, List<StockInventoryAmazon> inventories, bool prodCodeExistsCheckOnly)
        {
            foreach (var inv in inventories)
            {
                var query = _hatFContext.StockInventories
                    .Where(x => x.WhCode == whCode)
                    .Where(x => x.InventoryYearmonth == inventoryYearMonth)
                    .Where(x => x.StockType == StockTypeHatFOwn)    //自社(Amazon分は自社扱いでOK?)
                    .Where(x => x.QualityType == QuarityTypeGood)   //良品区分 対象外でよい？＞F:不良品、U:未検品
                    .Where(x => x.ProdCode == inv.ProdCode) 
                    ;

                var record = await query.SingleOrDefaultAsync();
                if (record == null)
                {
                    inv.ProdCodeExists = false;
                    inv.Description = CombineString(inv.Description, "商品コード未登録");

                    if (false == prodCodeExistsCheckOnly) 
                    {
                        string message = $"Amazon棚卸情報の更新先が見つかりませんでした。棚卸年月:{inventoryYearMonth:yyyy/MM}, 倉庫:{whCode}, 商品コード:{inv.ProdCode}";
                        //throw new HatFApiServiceException(message);
                    }
                }
                else
                {
                    inv.ProdCodeExists = true;

                    if (false == prodCodeExistsCheckOnly)
                    {
                        record.Actual = (short?)inv.Inventory;
                        _updateInfoSetter.SetUpdateInfo(record);   //更新日/更新者
                    }
                }

            }

            if (false == prodCodeExistsCheckOnly)
            {
                // エラー(空以外)を含む場合は確定しない
                var errorExistsQuery = inventories.Where(x => !string.IsNullOrEmpty(x.Description));
                if (false == errorExistsQuery.Any())
                {
                    _ = await _hatFContext.SaveChangesAsync();
                }
            }

            return inventories;
        }


        public IQueryable<StockRefill> GetStockRefill(string whCode, string prodCode)
        {
            var query = _hatFContext.StockRefills
                .Where(x => string.IsNullOrEmpty(whCode) || x.WhCode == whCode)
                .Where(x => string.IsNullOrEmpty(prodCode) || x.ProdCode.Contains(prodCode))
                .OrderBy(x => x.ProdCode)
                ;

            return query;
        }

        /// <summary>
        /// 要発注商品
        /// </summary>
        /// <param name="whCode">倉庫コード</param>
        /// <param name="prodCode">商品コード</param>
        /// <param name="excludeeOrdered">発注済を除外</param>
        /// <returns></returns>
        public IQueryable<ViewStockRefill> GetViewStockRefill(string whCode, string prodCode, bool excludeeOrdered)
        {
            var query = _hatFContext.ViewStockRefills
                .Where(x => string.IsNullOrEmpty(whCode) || x.倉庫コード == whCode)
                .Where(x => string.IsNullOrEmpty(prodCode) || x.商品コード.Contains(prodCode))
                .Where(x => (excludeeOrdered == false) || (x.発注済数 == 0 || x.発注済数 == null))  // 発注済を除外の場合、発注がないデータに限定
                .OrderBy(x => x.商品コード);

            return query;
        }

        public async Task<int> PutStockRefillAsync(IEnumerable<StockRefill> stockRefills)
        {
            _updateInfoSetter.SetUpdateInfo(stockRefills);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockRefill, StockRefill>();
            });
            var mapper = config.CreateMapper();

            foreach (var item in stockRefills)
            {
                var query = _hatFContext.StockRefills
                    .Where(x => x.WhCode == item.WhCode)
                    .Where(x => x.ProdCode == item.ProdCode);

                var record = await query.SingleOrDefaultAsync();
                if (record != null)
                {
                    mapper.Map(item, record);
                }
                else
                {
                    await _hatFContext.StockRefills.AddAsync(item);
                }
            }

            int count = await _hatFContext.SaveChangesAsync();
            return count;
        }

        public IQueryable<ComSyohinMst> GetComSyohinMstByProdCode(string hatSyohin)
        {
            var query = _hatFContext.ComSyohinMsts
                .Where(x => string.IsNullOrEmpty(hatSyohin) || x.HatSyohin == hatSyohin);
            return query;
        }
    }
}
