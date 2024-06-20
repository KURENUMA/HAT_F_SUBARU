using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Services
{
    /// <summary>与信額サービス</summary>
    public class CreditService
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _hatFContext;

        /// <summary>コンストラクタ</summary>
        /// <param name="hatFContext">DBコンテキスト</param>
        public CreditService(HatFContext hatFContext)
        {
            _hatFContext = hatFContext;
        }

        /// <summary>与信額チェック</summary>
        /// <param name="tokuiCd">取引先コード</param>
        /// <param name="amount">注文額</param>
        /// <returns>チェック結果</returns>
        public async Task<CheckCreditResult> CheckCreditAsync(string tokuiCd, decimal amount)
        {
            var company = await _hatFContext.CompanysMsts.SingleOrDefaultAsync(x => x.CompCode == tokuiCd);
            return
                // 取引先が見つからない
                (company is null) ? CheckCreditResult.NoCompany :
                // 与信限度額オーバー
                (company.MaxCredit < amount) ? CheckCreditResult.CreditOver :
                // 問題なし
                CheckCreditResult.None;
        }
    }
}
