using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Services
{
    /// <summary>受注情報補完サービス</summary>
    public class CompleteOrderService
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _hatFContext;

        /// <summary>起動時情報へのコンテキスト</summary>
        private readonly HatFApiExecutionContext _executionContext;

        /// <summary>連番サービス</summary>
        private readonly SequenceNumberService _sequenceNumberService;

        /// <summary>検索サービス</summary>
        private readonly HatFSearchService _searchService;

        /// <summary>在庫サービス</summary>
        private readonly StockService _stockService;


        private UpdateInfoSetter _updateInfoSetter;

        /// <summary>コンストラクタ</summary>
        /// <param name="hatFContext">DBコンテキスト</param>
        /// <param name="executionContext">起動時情報へのコンテキスト</param>
        /// <param name="sequenceNumberService">連番サービス</param>
        /// <param name="searchService">検索サービス</param>
        /// <param name="stockService">在庫サービス</param>
        /// <param name="updateInfoSetter">エンティティ更新情報設定</param>
        public CompleteOrderService(
            HatFContext hatFContext,
            HatFApiExecutionContext executionContext,
            HatFSearchService searchService,
            SequenceNumberService sequenceNumberService,
            StockService stockService,
            UpdateInfoSetter updateInfoSetter
            )
        {
            _hatFContext = hatFContext;
            _executionContext = executionContext;
            _sequenceNumberService = sequenceNumberService;
            _searchService = searchService;
            _stockService = stockService;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>受注情報のヘッダー部分を補完する</summary>
        /// <param name="request">入力パラメータ</param>
        /// <returns>補完結果</returns>
        public async Task<CompleteHeaderResult> CompleteHeaderAsync(CompleteHeaderRequest request)
        {
            // 社員マスタ
            var employee = await _hatFContext.Employees.Where(e => e.EmpCode == request.TantoCd).FirstOrDefaultAsync();
            // 扱便マスタ
            var bin = await _hatFContext.DivBins.Where(b => b.BinCd == request.BinCd).FirstOrDefaultAsync();
            // 倉庫マスタ
            var repository = await _hatFContext.WhMsts.Where(w => w.WhCode == request.SokoCd).FirstOrDefaultAsync();
            // 仕入先マスタ
            var supplier = await _hatFContext.SupplierMsts
                .Where(s => s.SupCode == request.ShiresakiCd)
                .OrderByDescending(s => s.SupSubNo)
                .FirstOrDefaultAsync();
            // 取引先マスタ
            var torihikisaki = await _hatFContext.CompanysMsts.Where(c => c.CompCode == request.TokuiCd).FirstOrDefaultAsync();
            // 伝票番号
            var denNo = request.DenNo;
            if (string.IsNullOrEmpty(denNo))
            {
                int nextDenNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHDenNo);
                denNo = $"{nextDenNo:D6}";
            }

            var isConditionAny = new[] { request.GenbaCd, request.TokuiCd, request.KeymanCd }.Any(x => !string.IsNullOrEmpty(x));
            var destinations = isConditionAny ? await _searchService.GetDestinationsAsync(
                null, null, request.GenbaCd, null, null, request.TokuiCd, request.KeymanCd, 200) : null;
            // 顧客マスタ
            var customer = destinations?.FirstOrDefault()?.Customer;
            // 出荷先マスタ
            var destination = destinations?.FirstOrDefault()?.Destination;
            // 工事店名補完用の顧客マスタ。工事店コードから検索する
            var koujiten = await _hatFContext.CustomersMsts
                .Where(c => !string.IsNullOrEmpty(request.KoujitenCd) && c.KojitenCode == request.KoujitenCd)
                .Where(c => string.IsNullOrEmpty(request.KeymanCd) || c.KeymanCode == request.KeymanCd)
                .FirstOrDefaultAsync();

            return new CompleteHeaderResult()
            {
                // 担当者Cd
                TantoCd = request.TantoCd,
                // 担当者名
                TantoName = employee?.EmpName,
                // 受注者（起票者）Cd
                JyuchuCd = employee?.EmpCode,
                // 得意先名
                TokuiName = torihikisaki?.CompName,
                // キーマン名
                KeymanName = customer?.CustUserName,
                // 工事店名
                KoujitenName = koujiten?.CustName,
                // 倉庫名
                SokoName = repository?.WhName,
                // 仕入れ先名
                ShiresakiName = supplier?.SupName,
                // TODO 補完：注文書（発注方法）
                Hkbn = "5",
                // 送信時用FAX番号
                Fax = customer?.CustFax,
                // 送信時用メールアドレス
                MailAddress = customer?.CustEmail,
                // 扱便名
                BinName = bin?.BinName,
                // 届先（宛先）１
                RecvName1 = destination?.DistName1,
                // 届先（宛先）２
                RecvName2 = destination?.DistName2,
                // 届先（TEL）
                RecvTel = destination?.DestTel,
                // 届先（郵便番号）
                RecvPostcode = destination?.ZipCode,
                // 届先（住所）１
                RecvAddress1 = destination?.Address1,
                // 届先（住所）２
                RecvAddress2 = destination?.Address2,
                // 届先（住所）３
                RecvAddress3 = destination?.Address3,
                // 仕入課
                ShireKa = supplier?.SupDepName,
                // 伝No
                DenNo = denNo,
            };
        }

        /// <summary>受注情報の明細部分の金額について補完する</summary>
        /// <param name="request">入力パラメータ</param>
        /// <returns>補完結果</returns>
        public async Task<CompleteDetailsResult> CompleteDetailsAsync(CompleteDetailsRequest request)
        {
            var result = new CompleteDetailsResult
            {
                Details = new List<CompleteDetailsResultDetail>(),
            };

            short i = 1;
            foreach (var requestDetail in request.Details)
            {
                var resultDetail = new CompleteDetailsResultDetail();
                resultDetail.Koban = requestDetail.Koban ?? i;

                // 商品マスタ
                var syohin = await _hatFContext.ComSyohinMsts
                    .Where(x => x.HatSyohin == requestDetail.SyohinCd)
                    .SingleOrDefaultAsync();

                resultDetail.Bara = DecideBara(request, requestDetail, syohin);

                // 在庫
                resultDetail.Stock = await _stockService.GetProductStockValidCountAsync(
                    string.IsNullOrEmpty(requestDetail.SokoCd) ? request.SokoCd : requestDetail.SokoCd,
                    requestDetail.SyohinCd);

                // TODO 記号がXY以外は0とするが、空白時は何もしない（暫定）
                if (!string.IsNullOrEmpty(requestDetail.UriageKigou) && IsKigouXZ(requestDetail.UriageKigou))
                {
                    requestDetail.UriageKakeritsu = 0m;
                    requestDetail.UriageTanka = 0;
                }
                // TODO 記号がXY以外は0とするが、空白時は何もしない（暫定）
                if (!string.IsNullOrEmpty(requestDetail.ShiireKigou) && IsKigouXZ(requestDetail.ShiireKigou))
                {
                    requestDetail.ShiireKakeritsu = 0m;
                    requestDetail.ShiireTanka = 0m;
                }
                // 後の計算でも使用するためリクエスト情報の定価単価も更新する
                resultDetail.TeikaTanka = requestDetail.TeikaTanka = DecideTeikaTanka(request.SyobunCdChk, requestDetail);
                resultDetail.TeikaKingaku = resultDetail.TeikaTanka * resultDetail.Bara;

                resultDetail.UriageTanka = DecideUriageTanka(request.SyobunCdChk, requestDetail);
                resultDetail.UriageKingaku = resultDetail.UriageTanka * resultDetail.Bara;

                resultDetail.SiireTanka = DecideShiireTanka(request.SyobunCdChk, requestDetail);
                resultDetail.SiireKingaku = resultDetail.SiireTanka * resultDetail.Bara;

                // TODO とりあえず同じものを返す
                resultDetail.SyohinCd = requestDetail.SyohinCd;
                resultDetail.Urikake = requestDetail.UriageKakeritsu;
                resultDetail.SiireKake = requestDetail.ShiireKakeritsu;
                resultDetail.ShiireKaitouTanka = requestDetail.ShiireKaitouTanka;
                // TODO いらない？
                // resultDetail.UriageTankaType = requestDetail.;

                i++;
                result.Details.Add(resultDetail);
            }

            result.TeikaKingakuSum = result.Details.Sum(d => d.TeikaKingaku);
            result.UriageKingakuSum = result.Details.Sum(d => d.UriageKingaku);
            result.ShiireKingakuSum = result.Details.Sum(d => d.SiireKingaku);

            result.Profit = result.UriageKingakuSum - result.ShiireKingakuSum;
            if (result.Profit != 0)
            {
                result.ProfitRate = result.Profit / result.UriageKingakuSum * 100m;
            }

            return result;
        }

        private bool IsKigouXZ(string kigou)
            => "XZ".Contains(kigou);

        private bool IsKigouNS(string kigou)
            => "NS".Contains(kigou);

        private bool IsKigouA(string kigou)
            => kigou == "A";

        private decimal? DecideTeikaTanka(bool syobunCdCheck, CompleteDetailsRequestDetail detail)
        {
            if (!syobunCdCheck && !string.IsNullOrEmpty(detail.SyohinCd))
            {
                return detail.TeikaTanka;
            }

            if (IsKigouNS(detail.UriageKigou))
            {
                // TODO 売上記号がNかSの場合、かつFOS_TEIKA_SETMにレコードが存在する場合にTANKAXMから取得した単価を採用する
            }
            return detail.TeikaTanka;
        }

        private decimal? DecideUriageTanka(bool syobunCdCheck, CompleteDetailsRequestDetail detail)
        {
            if (detail.UriageTanka.HasValue)
            {
                return detail.UriageTanka;
            }

            // 記号が'A'の場合、商品マスタの定価ではなく画面上の定価をもとに金額の計算を行う。
            if (IsKigouA(detail.UriageKigou))
            {
                // TODO (定価単価が0 or 空白) && (売上掛率が0 or 空白)時にLINK単価　＊　LINK率　で単価を求め、その単価に対してまるめ区分ごとにまるめ処理を行う。
                // 売上掛率変更時、売上種別はNULLに変更する。
            }
            // TODO 暫定的に記号に関係なく画面上の定価単価と売上掛率から計算する
            var result = detail.TeikaTanka * (detail.UriageKakeritsu / 100);
            return result.HasValue ? Math.Truncate(result.Value + 0.5m) : null;
        }

        private decimal? DecideShiireTanka(bool syobunCdCheck, CompleteDetailsRequestDetail detail)
        {
            if (detail.ShiireTanka.HasValue)
            {
                return detail.ShiireTanka;
            }

            // 記号が'A'の場合、商品マスタの定価ではなく画面上の定価をもとに金額の計算を行う。
            if (IsKigouA(detail.ShiireKigou))
            {
                // TODO (定価単価が0 or 空白) && (仕入掛率が0 or 空白)時にLINK単価　＊　LINK率　で単価を求め、その単価に対してまるめ区分ごとにまるめ処理を行う。
            }
            // TODO 暫定的に記号に関係なく画面上の定価単価と仕入掛率から計算する
            var result = detail.TeikaTanka * (detail.ShiireKakeritsu / 100);
            return result.HasValue ? Math.Truncate(result.Value + 0.5m) : null;
        }

        /// <summary>バラ数を決定する</summary>
        /// <param name="request">リクエスト(全体)</param>
        /// <param name="requestDetail">リクエスト(明細部分)</param>
        /// <param name="syohinMst">商品マスタ</param>
        /// <returns>バラ数</returns>
        private int? DecideBara(CompleteDetailsRequest request, CompleteDetailsRequestDetail requestDetail, ComSyohinMst syohinMst)
        {
            var isNeedOrder = new[] { "15", "21" }.Contains(request.DenFlag);

            // バラ数が入力できる状況でバラ数が入力済みの場合はそのバラ数を採用する
            if (!string.IsNullOrEmpty(requestDetail.SyohinCd) && isNeedOrder && requestDetail.BaraCount.HasValue)
            {
                return requestDetail.BaraCount;
            }
            // 数量がない場合→null
            if (!requestDetail.Suuryo.HasValue)
            {
                return null;
            }
            // 数量があり単位がない場合→数量
            // 数量があり単位がLMS以外の場合→数量
            if (!new[] { "L", "M", "S" }.Contains(requestDetail.Tani))
            {
                return requestDetail.Suuryo;
            }
            // 単位に応じてIRISU_XXXを取得する
            var irisu =
                requestDetail.Tani == "L" ? syohinMst?.IrisuDai ?? 1m :
                requestDetail.Tani == "M" ? syohinMst?.IrisuChu ?? 1m :
                requestDetail.Tani == "S" ? syohinMst?.IrisuSho ?? 1m : 1m;
            // ただし、IRISUが0の場合は1とする
            irisu = irisu == 0 ? 1 : irisu;

            return requestDetail.Suuryo * (int)irisu;
        }
    }
}