using AutoMapper;
using Azure.Storage.Blobs.Models;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services.Authentication;
using HAT_F_api.StateCodes;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;
namespace HAT_F_api.Services
{
    public class HatFUpdateService
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _hatFContext;

        /// <summary>起動時情報コンテキスト</summary>
        private readonly HatFApiExecutionContext _executionContext;

        /// <summary>連番サービス</summary>
        private readonly SequenceNumberService _sequenceNumberService;

        private readonly NLog.ILogger _logger;
        private UpdateInfoSetter _updateInfoSetter;

        /// <summary>ログイン情報</summary>
        private readonly HatFLoginResultAccesser _hatFLoginResultAccesser;

        /// <summary>コンストラクタ</summary>
        /// <param name="hatFContext">DBコンテキスト</param>
        /// <param name="executionContext">起動時情報コンテキスト</param>
        /// <param name="sequenceNumberService">連番サービス</param>
        /// <param name="logger">Logger</param>
        /// <param name="updateInfoSetter">作成者/更新者などを設定するクラス</param>
        /// <param name="hatFLoginResultAccesser">ログイン情報</param>
        public HatFUpdateService(
            HatFContext hatFContext, 
            HatFApiExecutionContext executionContext, 
            SequenceNumberService sequenceNumberService,
            NLog.ILogger logger,
            UpdateInfoSetter updateInfoSetter,
            HatFLoginResultAccesser hatFLoginResultAccesser
            )
        {
            _hatFContext = hatFContext;
            _executionContext = executionContext;
            _sequenceNumberService = sequenceNumberService;
            _logger = logger;
            _updateInfoSetter = updateInfoSetter;
            _hatFLoginResultAccesser = hatFLoginResultAccesser;
        }

        /// <summary>
        /// 受注情報論理削除
        /// </summary>
        /// <param name="saveKey">SAVE_KEY</param>
        /// <param name="denSort">ページ番号</param>
        /// <returns></returns>
        public async Task<ApiResponse<int>> FosJyuchuDeleteAsync(string saveKey, string denSort)
        {
            // 削除対象のページが含まれる、同一伝票のすべてのページを取得する
            var headers = await _hatFContext.FosJyuchuHs
                .Where(h => h.SaveKey == saveKey)
                .ToListAsync();
            // 削除対象のページ
            var header = headers.First(h => h.DenSort == denSort);

            // コンフリクト対策
            if (header.DelFlg == DelFlg.Deleted)
            {
                throw new DbUpdateConcurrencyException($"既に削除済みのため削除できません。(SAVEKEY = {saveKey})");
            }
            if (header.OrderState == HeaderOrderState.Collation)
            {
                throw new DbUpdateConcurrencyException($"既にACOS済みのため削除できません。(SAVEKEY = {saveKey})");
            }

            // 対象ページを論理削除
            header.DelFlg = DelFlg.Deleted;
            // 仕入先回答確認フラグを設定（※一貫化仕様）
            header.AnswerConfirmFlg = "2";
            header.UpdateDate = _executionContext.ExecuteDateTimeJst;
            header.UpdDate = _executionContext.ExecuteDateTimeJst;
            // TODO DB更新者
            // header.Updater = ;

            // 受注明細の更新
            var details = await _hatFContext.FosJyuchuDs
                .Where(d => d.SaveKey == saveKey && d.DenSort == denSort)
                .ToListAsync();
            foreach (var d in details)
            {
                d.DelFlg = DelFlg.Deleted;
                d.UpdateDate = _executionContext.ExecuteDateTimeJst;
                // TODO DB更新者
                // d.Updater = ;
            }

            // ページが削除されることで、他ページのORDER_STATEの条件が変わるので再算出
            var alivePages = headers.Where(h => h.DelFlg != DelFlg.Deleted).ToList();
            var orderState =
                !alivePages.Any(p => (p.DenState != DenState.Collation && p.DenState != DenState.Arranged)) ? HeaderOrderState.Collation :
                alivePages.Any(p => p.DenState == DenState.Pending) ? HeaderOrderState.Pending :
                alivePages.Any(p => p.DenState == DenState.Ordered || p.DenState == DenState.Mix || p.DenState == DenState.Answered) ? HeaderOrderState.Ordered :
                HeaderOrderState.Pending;

            foreach (var p in alivePages)
            {
                p.OrderState = orderState;
                p.UpdateDate = _executionContext.ExecuteDateTimeJst;
                p.UpdDate = _executionContext.ExecuteDateTimeJst;
                // TODO DB更新者
                // p.Updater = ;
            }

            return new ApiOkResponse<int>(await _hatFContext.SaveChangesAsync());
        }

        /// <summary>受注情報保存</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>受注情報</returns>
        public async Task<List<FosJyuchuPage>> SavePagesAsync(FosJyuchuPages pages)
        {
            if (pages == null)
            {
                throw new ArgumentNullException(nameof(pages));
            }

            // 補正
            await CorrectPagesForSaveAsync(pages);

            foreach (var page in pages.Pages.Where(p => p.FosJyuchuH is not null))
            {
                // ヘッダのUpsert
                if (_hatFContext.FosJyuchuHs.Any(e => e.SaveKey == page.FosJyuchuH.SaveKey && e.DenSort == page.FosJyuchuH.DenSort))
                {
                    _hatFContext.FosJyuchuHs.Update(page.FosJyuchuH);
                }
                else
                {
                    _hatFContext.FosJyuchuHs.Add(page.FosJyuchuH);
                }

                // 明細のUpsert
                foreach (var detail in page.FosJyuchuDs)
                {
                    if (_hatFContext.FosJyuchuDs.Any(e => e.SaveKey == detail.SaveKey && e.DenSort == detail.DenSort && e.DenNoLine == detail.DenNoLine))
                    {
                        _hatFContext.FosJyuchuDs.Update(detail);
                    }
                    else
                    {
                        _hatFContext.FosJyuchuDs.Add(detail);
                    }
                }
            }

            await _hatFContext.SaveChangesAsync();

            return pages.Pages;
        }

        /// <summary>受注情報確定</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>受注情報</returns>
        public async Task<List<FosJyuchuPage>> CommitPagesAsync(FosJyuchuPages pages)
        {
            // 補正
            await CorrectPagesForSaveAsync(pages);
            await CorrectPagesForCommitAsync(pages);

            foreach (var page in pages.Pages.Where(p => p.FosJyuchuH is not null))
            {
                // ヘッダのUpsert
                if (_hatFContext.FosJyuchuHs.Any(e => e.SaveKey == page.FosJyuchuH.SaveKey && e.DenSort == page.FosJyuchuH.DenSort))
                {
                    _hatFContext.FosJyuchuHs.Update(page.FosJyuchuH);
                }
                else
                {
                    _hatFContext.FosJyuchuHs.Add(page.FosJyuchuH);
                }

                // 明細のUpsert
                foreach (var detail in page.FosJyuchuDs)
                {
                    if (_hatFContext.FosJyuchuDs.Any(e => e.SaveKey == detail.SaveKey && e.DenSort == detail.DenSort && e.DenNo == detail.DenNo))
                    {
                        _hatFContext.FosJyuchuDs.Update(detail);
                    }
                    else
                    {
                        _hatFContext.FosJyuchuDs.Add(detail);
                    }
                }
            }

            await _hatFContext.SaveChangesAsync();

            return pages.Pages;
        }

        /// <summary>発注照合</summary>
        /// <param name="pages">受注情報</param>
        /// <returns>受注情報</returns>
        public async Task<List<FosJyuchuPage>> CollationPagesAsync(FosJyuchuPages pages)
        {
            // 補正
            await CorrectPagesForSaveAsync(pages);
            await CorrectPagesForCommitAsync(pages);
            await CorrectPagesForCollationAsync(pages);

            foreach (var page in pages.Pages.Where(p => p.FosJyuchuH is not null))
            {
                // ヘッダのUpsert
                if (_hatFContext.FosJyuchuHs.Any(e => e.SaveKey == page.FosJyuchuH.SaveKey && e.DenSort == page.FosJyuchuH.DenSort))
                {
                    _hatFContext.FosJyuchuHs.Update(page.FosJyuchuH);
                }
                else
                {
                    _hatFContext.FosJyuchuHs.Add(page.FosJyuchuH);
                }

                // 明細のUpsert
                foreach (var detail in page.FosJyuchuDs)
                {
                    if (_hatFContext.FosJyuchuDs.Any(e => e.SaveKey == detail.SaveKey && e.DenSort == detail.DenSort && e.DenNoLine == detail.DenNoLine))
                    {
                        _hatFContext.FosJyuchuDs.Update(detail);
                    }
                    else
                    {
                        _hatFContext.FosJyuchuDs.Add(detail);
                    }
                }
            }

            await _hatFContext.SaveChangesAsync();

            return pages.Pages;
        }

        /// <summary>SAVE_KEYの採番</summary>
        /// <param name="empCode">社員Cd</param>
        /// <param name="nyu2">入力者アルファベット</param>
        /// <returns>SAVE_KEY</returns>
        private string MakeSaveKey(string empCode, string nyu2)
        {
            var now = _executionContext.ExecuteDateTimeJst;
            return $"{nyu2}{empCode}{now:yyyyMMddHHmmssfff}";
        }

        /// <summary>受注情報を補正</summary>
        /// <param name="pages">受注情報</param>
        private async Task CorrectPagesForSaveAsync(FosJyuchuPages pages)
        {
            // TODO 入力者と社員ID
            var saveKey = pages.Pages.FirstOrDefault(p => p.FosJyuchuH.SaveKey is not null)?.FosJyuchuH.SaveKey ?? MakeSaveKey("", "");
            // 最初の既存ページ
            var firstExistsPage = pages.AlivePages.FirstOrDefault(p => !p.IsNew);
            var pageNo = 0;
            foreach (var page in pages.AlivePages)
            {
                var header = page.FosJyuchuH;
                var aliveDetails = page.FosJyuchuDs.Where(d => d.DelFlg != DelFlg.Deleted && d.MoveFlg != MoveFlg.Moved);
                pageNo++;

                #region ヘッダー部分に対する編集

                header.SaveKey = saveKey;
                header.DenSort = pageNo.ToString();
                header.UpdateDate = _executionContext.ExecuteDateTimeJst;
                // TODO 社員ID
                // header.Updater = empId;
                if (!page.IsNew)
                {
                    header.OrderNo = null;
                    if (string.Compare(HeaderOrderState.Pending, header.OrderState) > 0)
                    {
                        header.OrderState = HeaderOrderState.Pending;
                    }
                    if(string.Compare(DenState.Pending, header.DenState) > 0)
                    {
                        header.DenState = DenState.Pending;
                    }
                    header.OrderFlag = firstExistsPage?.FosJyuchuH.OrderFlag ?? OrderFlag.Normal;
                    // TODO IPアドレスの設定
                    // header.IpAdd = $"V{IPAddress}";
                    header.InpDate = _executionContext.ExecuteDateTimeJst;
                    header.CreateDate = _executionContext.ExecuteDateTimeJst;
                    // TODO 社員ID
                    // header.Creator = empId;
                }

                #endregion ヘッダー部分に対する編集

                #region 明細部分に対する編集

                foreach (var detail in aliveDetails)
                {
                    var syohin = await _hatFContext.ComSyohinMsts
                        .SingleOrDefaultAsync(x => x.HatSyohin == detail.SyohinCd);

                    // TODO 新規ページや新規明細行の場合、という条件があるが、新規明細行に未対応
                    detail.SaveKey = saveKey;
                    detail.DenNo = header.DenNo;
                    detail.DenSort = header.DenSort;
                    detail.Chuban = string.IsNullOrEmpty(header.HatOrderNo) ? null : $"{header.HatOrderNo}{detail.Koban}";
                    detail.EstimateNo = header.EstimateNo;
                    detail.EstCoNo = header.EstCoNo;
                    // TODO 型が違うが、単純にint.Parseでよいのか確認
                    // detail.Dencd = header.DelFlg;
                    detail.Code5 = syohin?.SyohinBunrui;
                    if (!page.IsNew)
                    {
                        detail.OrderState = DetailOrderState.Pending;
                        // TODO 社員IDを設定
                        // detail.Creator = null;
                        detail.CreateDate = _executionContext.ExecuteDateTimeJst;
                    }
                    // TODO 社員IDを設定
                    // detail.Updater = null;
                    detail.UpdateDate = _executionContext.ExecuteDateTimeJst;
                }

                #endregion 明細部分に対する編集
            }
        }

        /// <summary>確定させるためにページ情報を変更する</summary>
        /// <param name="pages">ページ情報</param>
        /// <returns>ページ情報</returns>
        public async Task<List<FosJyuchuPage>> CorrectPagesForCommitAsync(FosJyuchuPages pages)
        {
            var page = pages.Pages[pages.TargetPage];
            var header = page.FosJyuchuH;
            var details = page.FosJyuchuDs;
            var alivePages = pages.AlivePages.ToList();

            #region 明細部分に対する編集

            foreach (var detail in details)
            {
                // 発注状態
                detail.OrderState =
                    // TODO 倉出しの判定は倉庫マスタにフラグを設ける形にしたい
                    (header.DenFlg.EndsWith(":倉出") || header.Hkbn == "0" || header.OrderFlag == "5") ? DetailOrderState.Collation :
                    (detail.SiiAnswTan.HasValue && detail.Nouki.HasValue) ? DetailOrderState.Answered :
                    DetailOrderState.Ordered;
            }

            #endregion 明細部分に対する編集

            #region ヘッダー部分に対する編集

            // 受注番号
            var random = new Random();
            // TODO 新規採番、部支店Cdはとりあえず"O"として固定
            var orderNo = alivePages.FirstOrDefault(p => !p.IsNew && !string.IsNullOrEmpty(p.FosJyuchuH.OrderNo))?.FosJyuchuH.OrderNo;
            if (string.IsNullOrEmpty(orderNo))
            {
                int next = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHOrderNo);
                orderNo = $"{DateTime.Today:yyMM}O{next:D6}";
            }
            foreach (var p in alivePages)
            {
                p.FosJyuchuH.OrderNo = orderNo;
            }

            // 発注状態
            header.DenState =
                !details.Any(d => d.OrderState != DetailOrderState.Collation) ? DenState.Arranged :
                !details.Any(d => d.OrderState != DetailOrderState.Answered) ? DenState.Answered :
                details.Any(d => d.OrderState == DetailOrderState.Answered) ? DenState.Mix :
                DenState.Ordered;

            // 内部No
            var dseq = alivePages.FirstOrDefault(p => !p.IsNew && !string.IsNullOrEmpty(p.FosJyuchuH.Dseq))?.FosJyuchuH.Dseq;
            if (string.IsNullOrEmpty(dseq))
            {
                int next = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHDSeq);
                dseq = $"{next:D6}";
            }
            foreach (var p in alivePages)
            {
                p.FosJyuchuH.Dseq = dseq;
            }

            // 注文書番号
            header.OrderDenNo = $"{header.Dseq}{header.DenFlg}{header.DenNo}";

            header.GOrderAmnt = details.Sum(d => (long?)d.UriKin);
            // TODO 消費税額合計はとりあえず10%
            header.GCmpTax = details.Sum(d => (long?)((d.UriKin) * 0.1m));

            #endregion ヘッダー部分に対する編集

            #region 全ページにまたがる編集

            var orderState =
                !alivePages.Any(p => (p.FosJyuchuH.DenState != DenState.Collation && p.FosJyuchuH.DenState != DenState.Arranged)) ? HeaderOrderState.Collation :
                alivePages.Any(p => p.FosJyuchuH.DenState == DenState.Pending) ? HeaderOrderState.Pending :
                alivePages.Any(p => p.FosJyuchuH.DenState == DenState.Ordered || p.FosJyuchuH.DenState == DenState.Mix || p.FosJyuchuH.DenState == DenState.Answered) ? HeaderOrderState.Ordered :
                HeaderOrderState.Pending;
            foreach (var p in alivePages.Where(p => string.Compare(orderState, p.FosJyuchuH.OrderState) > 0))
            {
                p.FosJyuchuH.OrderState = orderState;
                foreach(var d in p.FosJyuchuDs)
                {
                    d.Dseq = p.FosJyuchuH.Dseq;
                    d.Nouki = p.FosJyuchuH.Nouki;
                    d.SokoCd = p.FosJyuchuH.SokoCd;
                    d.ShiresakiCd = p.FosJyuchuH.ShiresakiCd;
                }
            }

            #endregion 全ページにまたがる編集

            return pages.Pages;
        }

        /// <summary>発注照合させるためにページ情報を変更する</summary>
        /// <param name="pages">ページ情報</param>
        /// <returns>ページ情報</returns>
        public async Task<List<FosJyuchuPage>> CorrectPagesForCollationAsync(FosJyuchuPages pages)
        {
            var page = pages.Pages[pages.TargetPage];
            var header = page.FosJyuchuH;
            var details = page.FosJyuchuDs;
            var alivePages = pages.AlivePages.ToList();

            #region 明細部分に対する編集

            foreach (var detail in page.FosJyuchuDs)
            {
                // 発注状態
                detail.OrderState = DetailOrderState.Collation;
            }

            #endregion 明細部分に対する編集

            #region ヘッダー部分に対する編集

            // 発注状態
            header.DenState = DenState.Arranged;
            // TODO 新規採番、部支店Cdはとりあえず"O"として固定
            var orderNo = alivePages.FirstOrDefault(p => !p.IsNew && !string.IsNullOrEmpty(p.FosJyuchuH.OrderNo))?.FosJyuchuH.OrderNo;
            if (string.IsNullOrEmpty(orderNo))
            {
                int next = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHOrderNo);
                orderNo = $"{DateTime.Today:yyMM}O{next:D6}";
            }
            foreach (var p in alivePages)
            {
                p.FosJyuchuH.OrderNo = orderNo;
            }
            // 内部No
            var dseq = alivePages.FirstOrDefault(p => !p.IsNew && !string.IsNullOrEmpty(p.FosJyuchuH.Dseq))?.FosJyuchuH.Dseq;
            if (string.IsNullOrEmpty(dseq))
            {
                int next = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.FosJyuchuHDSeq);
                dseq = $"{next:D6}";
            }
            foreach (var p in alivePages)
            {
                p.FosJyuchuH.Dseq = dseq;
                foreach(var d in p.FosJyuchuDs)
                {
                    d.Dseq = dseq;
                }
            }

            // 注文書番号
            header.OrderDenNo = $"{header.Dseq}{header.DenFlg}{header.DenNo}";

            // ヘッダ部分の値を明細部分に反映
            foreach(var p in alivePages)
            {
                foreach(var d in p.FosJyuchuDs)
                {
                }
            }
            #endregion ヘッダー部分に対する編集

            #region 全ページにまたがる編集

            var orderState =
                !alivePages.Any(p => (p.FosJyuchuH.DenState != DenState.Collation && p.FosJyuchuH.DenState != DenState.Arranged)) ? HeaderOrderState.Collation :
                alivePages.Any(p => p.FosJyuchuH.DenState == DenState.Pending) ? HeaderOrderState.Pending :
                alivePages.Any(p => p.FosJyuchuH.DenState == DenState.Ordered || p.FosJyuchuH.DenState == DenState.Mix || p.FosJyuchuH.DenState == DenState.Answered) ? HeaderOrderState.Ordered :
                HeaderOrderState.Pending;
            foreach (var p in alivePages.Where(p => string.Compare(orderState, p.FosJyuchuH.OrderState) > 0))
            {
                p.FosJyuchuH.OrderState = orderState;
                foreach(var d in p.FosJyuchuDs)
                {
                    d.Dseq = p.FosJyuchuH.Dseq;
                    d.Nouki = p.FosJyuchuH.Nouki;
                    d.SokoCd = p.FosJyuchuH.SokoCd;
                    d.ShiresakiCd = p.FosJyuchuH.ShiresakiCd;
                }
            }

            #endregion 全ページにまたがる編集

            return pages.Pages;
        }

        /// <summary>物件情報を更新する</summary>
        /// <param name="request">物件情報</param>
        /// <returns>ページ情報</returns>
        public async Task<int> UpdateViewConstructionDetailAsync(Construction request)
        {
            var constructionDetail = await _hatFContext.Constructions
                .FirstOrDefaultAsync(x => x.ConstructionCode == request.ConstructionCode);
            var result = 0;

            if (constructionDetail == null)
            {
                // 物件が見つからない場合
                return result;
            }

            constructionDetail.SearchKey = request.SearchKey;
            constructionDetail.OrderState = request.OrderState;
            constructionDetail.TeamCd = request.TeamCd;
            constructionDetail.EmpId = request.EmpId;
            constructionDetail.ConstructionName = request.ConstructionName;
            constructionDetail.ConstructionKana = request.ConstructionKana;;
            constructionDetail.InquiryDate = request.InquiryDate;
            constructionDetail.EstimateSendDate = request.EstimateSendDate;
            constructionDetail.OrderRceiptDate = request.OrderRceiptDate;
            constructionDetail.OrderContractRceiptDate = request.OrderContractRceiptDate;
            constructionDetail.OrderContractSendDate = request.OrderContractSendDate;
            constructionDetail.OrderCompeltedDate = request.OrderCompeltedDate;
            constructionDetail.Recommended = request.Recommended;
            constructionDetail.Uncontracted = request.Uncontracted;
            constructionDetail.SalesEvent = request.SalesEvent;
            constructionDetail.BalanceSheet = request.BalanceSheet;
            constructionDetail.Thailand = request.Thailand;
            //constructionDetail.ConstructtonNotes = request.ConstructtonNotes;
            constructionDetail.TokuiCd = request.TokuiCd;
            constructionDetail.KmanCd = request.KmanCd;
            constructionDetail.RecvPostcode = request.RecvPostcode;
            constructionDetail.RecvAdd1 = request.RecvAdd1;
            constructionDetail.RecvAdd2 = request.RecvAdd2;
            constructionDetail.RecvAdd3 = request.RecvAdd3;
            constructionDetail.BuildingNameEtc = request.BuildingNameEtc;
            constructionDetail.RecvTel = request.RecvTel;
            constructionDetail.RecvFax = request.RecvFax;
            constructionDetail.ConstructorName = request.ConstructorName;
            constructionDetail.ConstructorType = request.ConstructorType;
            constructionDetail.ConstructorIndustry = request.ConstructorIndustry;
            constructionDetail.ConstructorRepName = request.ConstructorRepName;
            constructionDetail.ConstructorTel = request.ConstructorTel;
            constructionDetail.ConstructorFax = request.ConstructorFax;
            constructionDetail.Comment = request.Comment;

            _hatFContext.Constructions.Update(constructionDetail);

            // 変更を保存
            await _hatFContext.SaveChangesAsync();

            result = 1;
            return result;
        }

        /// <summary>物件詳細のGridを削除してから追加する</summary>
        /// <param name="request">物件詳細のGridのList</param>
        /// <returns></returns>
        public async Task<int> DeleteInsertConstructionDetailGridAsync(List<ConstructionDetail> request)
        {
            var constructionDetail = await _hatFContext.ConstructionDetails
                .Where(x => x.ConstructionCode == request.FirstOrDefault().ConstructionCode)
                .ToListAsync();

            //物件コードを条件にデータを削除
            _hatFContext.ConstructionDetails.RemoveRange(constructionDetail);

            if (request.FirstOrDefault().AppropState != null)
            {
                _hatFContext.ConstructionDetails.AddRange(request);
            }

            // 変更を保存
            var result = await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>物件詳細のGridのステータスを更新</summary>
        /// <param name="request">項番のlist</param>
        /// <returns></returns>
        public async Task<int> UpdateConstructionDetailGridKobanAsync(List<ConstructionDetail> request)
        {
            List<int> kobans = request.Select(x => x.Koban).ToList();
            var constructionDetail = await _hatFContext.ConstructionDetails
                .Where(x => x.ConstructionCode == request.FirstOrDefault().ConstructionCode
                && kobans.Contains(x.Koban))
                .ToListAsync();

            for (int i = 0; i < constructionDetail.Count; i++)
            {
                constructionDetail[i].AppropState = 1;
            }

            // 変更を保存
            var result = await _hatFContext.SaveChangesAsync();
            return result;
        }

        /// <summary>物件情報を登録する</summary>
        /// <param name="request">物件情報</param>
        /// <returns>物件コード</returns>
        public async Task<string> AddViewConstructionDetailAsync(Construction request)
        {
            request.CreateDate = DateTime.Now;
            request.UpdateDate = DateTime.Now;

            _hatFContext.Constructions.Add(request);

            // 変更を保存
            await _hatFContext.SaveChangesAsync();

            return request.ConstructionCode;
        }

        /// <summary>仕入先登録</summary>
        /// <param name="supplier">仕入先情報</param>
        /// <returns>仕入先情報</returns>
        public async Task<SupplierMst> UpsertSupplierAsync(SupplierMst supplier)
        {
            _updateInfoSetter.SetUpdateInfo(supplier);

            var exists = await _hatFContext.SupplierMsts
                .SingleOrDefaultAsync(e => e.SupCode == supplier.SupCode && e.SupSubNo == supplier.SupSubNo);

            if (exists is not null)
            {
                new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SupplierMst, SupplierMst>()
                        .ForMember(x => x.SupCode, x => x.Ignore())
                        .ForMember(x => x.SupSubNo, x => x.Ignore())
                        .ForMember(x => x.CreateDate, x => x.Ignore())
                        .ForMember(x => x.Creator, x => x.Ignore());
                })).Map(supplier, exists);
            }
            else
            {
                _hatFContext.SupplierMsts.Add(supplier);
            }
            await _hatFContext.SaveChangesAsync();
            return supplier;
        }

        /// <summary>取引先登録</summary>
        /// <param name="companys">仕入先情報</param>
        /// <returns>仕入先情報</returns>
        public async Task<CompanysMst> UpsertCompanysAsync(CompanysMst companys)
        {
            if (await _hatFContext.CompanysMsts.AnyAsync(c => c.CompCode == companys.CompCode))
            {
                _hatFContext.CompanysMsts.Update(companys);
            }
            else
            {
                _hatFContext.CompanysMsts.Add(companys);
            }
            await _hatFContext.SaveChangesAsync();
            return companys;
        }
        
        /// <summary>売上調整情報を追加/更新する</summary>
        /// <param name="salesAdjustments">売上調整情報</param>
        /// <returns>最新のレコード情報</returns>
        public async Task<List<SalesAdjustment>> PutSalesAdjustmentsAsync(IEnumerable<ViewSalesAdjustment> salesAdjustments)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ViewSalesAdjustment, SalesAdjustment>()
                    .ForMember(x => x.ConstructionCode, x => x.MapFrom(source => source.物件コード))
                    .ForMember(x => x.TokuiCd, x => x.MapFrom(source => source.得意先コード))
                    .ForMember(x => x.AdjustmentCategory, x => x.MapFrom(source => source.調整区分))
                    .ForMember(x => x.Description, x => x.MapFrom(source => source.摘要))
                    .ForMember(x => x.AccountTitle, x => x.MapFrom(source => source.勘定科目))
                    .ForMember(x => x.AdjustmentAmount, x => x.MapFrom(source => source.調整金額))
                    .ForMember(x => x.TaxFlg, x => x.MapFrom(source => source.消費税))
                    .ForMember(x => x.TaxRate, x => x.MapFrom(source => source.消費税率))
                    .ForMember(x => x.InvoicedDate, x => x.MapFrom(source => source.請求日))
                    .ForMember(x => x.ApprovalId, x => x.MapFrom(source => source.承認要求番号))
                    .ForMember(x => x.SalesAdjustmentNo, x => x.Ignore())
                    .ForMember(x => x.CreateDate, x => x.Ignore())
                    .ForMember(x => x.Creator, x => x.Ignore())
                    .ForMember(x => x.UpdateDate, x => x.Ignore())
                    .ForMember(x => x.Updater, x => x.Ignore());
            }));
            var result = new List<SalesAdjustment>();

            // 消費税率はレコード数が少ないので先にすべて保持しておく
            var taxRates = await _hatFContext.DivTaxRates.ToListAsync();
            foreach (var item in salesAdjustments)
            {
                var exists = await _hatFContext.SalesAdjustments.SingleOrDefaultAsync(x => x.SalesAdjustmentNo == item.売上調整番号);

                // 新規/更新共通の処理
                item.消費税率 = taxRates.SingleOrDefault(x => x.TaxRateCd == item.消費税)?.TaxRate;

                // 新規
                if (exists == null)
                {
                    var newItem = new SalesAdjustment();
                    var newNo = await _sequenceNumberService.GetNextNumberAsync(SequenceNumber.PuImportNo);

                    mapper.Map(item, newItem);
                    newItem.SalesAdjustmentNo = $"SA{newNo:D8}";
                    newItem.EmpId = _hatFLoginResultAccesser.HatFLoginResult.EmployeeId;

                    _updateInfoSetter.SetUpdateInfo(item);
                    _hatFContext.SalesAdjustments.Add(newItem);
                    result.Add(newItem);
                }
                // 更新
                else
                {
                    mapper.Map(item, exists);
                    exists.EmpId = _hatFLoginResultAccesser.HatFLoginResult.EmployeeId;

                    _updateInfoSetter.SetUpdateInfo(exists);
                    _hatFContext.SalesAdjustments.Update(exists);
                    result.Add(exists);
                }
            }
            await _hatFContext.SaveChangesAsync();
            return result;
        }
    }
}