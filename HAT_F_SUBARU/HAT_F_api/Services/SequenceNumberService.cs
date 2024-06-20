using HAT_F_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace HAT_F_api.Services
{
    public class SequenceNumberService
    {
        private HatFContext _hatFContext;

        public SequenceNumberService(HatFContext hatFContext)
        {
            _hatFContext = hatFContext;
        }

        /// <summary>
        /// 次のシーケンス番号を取得します
        /// </summary>
        /// <param name="sequentialNumber">番号を取得するシーケンス種別</param>
        /// <returns></returns>
        public async Task<int> GetNextNumberAsync(SequenceNumber sequentialNumber) 
        {
            // 対応するSQL Serverシーケンシャルオブジェクト名を取得
            // これはSequentialNumber列挙型で定義してあります
            var attr = sequentialNumber.GetAttributeOfType<TargetSequenceObjectAttribute>();
            string targetSequentialNumberObject = attr.TargetSequenceObject;

            // targetSequentialNumberObject は定義済み文字列（非外部パラメータ）しか入らない
            string sql = @$"
                DECLARE @seqNo AS int
                SET @seqNo = NEXT VALUE FOR {targetSequentialNumberObject}
                SELECT @seqNo as [Value]
            ";

            // ここでは構文上シーケンスオブジェクト名をパラメータ変数に出来ないので名前に "Raw" があるメソッドを使用しています
            // 通常は名前に "Raw" を含まないメソッドを使用してください
            var query = _hatFContext.Database.SqlQueryRaw<int>(sql);

            int newSeqNo = int.MinValue;
            await foreach (var item in query.AsAsyncEnumerable())   // SingleやToList等は実行時エラーになる ⇒ AsEnumerable/AsAsyncEnumerable が必要
            {
                newSeqNo = item;
                break;
            }

            if (newSeqNo == int.MinValue)
            { 
                // 必ず1件取得できるはずなのでループを通らなかったらエラー
                throw new HatFApiServiceException($"シーケンス番号取得失敗: {targetSequentialNumberObject}"); 
            }

            if (newSeqNo < 1) 
            {
                // 1 未満が取得された場合、シーケンス番号の初期値設定が出来ていないと判断
                throw new HatFApiServiceException($"シーケンス番号の初期設定が完了していません: {targetSequentialNumberObject}");
            }

            return newSeqNo;
        }

        /// <summary>H注番を新規採番する</summary>
        /// <param name="key">H注番のキー</param>
        /// <param name="denFlg">伝区</param>
        /// <returns>新規H注番</returns>
        public async Task<string> GetNextHatOrderNoAsync(string key, string denFlg)
        {
            var sequence = await _hatFContext.HatOrderNoSequences.SingleOrDefaultAsync(x => x.Key == key);
            if (sequence == null)
            {
                sequence = new HatOrderNoSequence()
                {
                    Key = key,
                    Number = 1,
                };
                await _hatFContext.HatOrderNoSequences.AddAsync(sequence);
            }
            else
            {
                sequence.Number = (sequence.Number == 999 ? 1 : sequence.Number + 1);
            }
            await _hatFContext.SaveChangesAsync();

            // 取次：B 直送：C
            var mark =
                denFlg == "15" ? "B" :
                denFlg == "21" ? "C" : string.Empty;
            return $"{key}{sequence.Number:00}{mark}";
        }
    }
}
