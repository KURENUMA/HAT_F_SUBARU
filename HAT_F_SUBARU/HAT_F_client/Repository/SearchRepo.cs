using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Constants;
using HatFClient.ViewModels;
using Newtonsoft.Json;

namespace HatFClient.Repository
{
    public class SearchRepo
    {
        private static SearchRepo instance = null;

        private const int DEFAULT_ROWS = 200;


        private SearchRepo() { }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SearchRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new SearchRepo();
            }
            return instance;
        }

        /// <summary>
        /// 得意先検索
        /// </summary>
        /// <param name="torihikiCd"></param>
        /// <param name="torihikiName"></param>
        /// <param name="torihikiNameKana"></param>
        /// <param name="teamCd"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Torihiki>>> searchTorihiki(string torihikiCd, string torihikiName, string torihikiNameKana, string teamCd, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(torihikiCd), torihikiCd},
                {nameof(torihikiName), torihikiName},
                {nameof(torihikiNameKana), torihikiNameKana},
                {nameof(teamCd), teamCd},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Torihiki>>(ApiResources.HatF.Client.SearchTorihiki, parameters);
        }

        /// <summary>
        /// キーマン検索
        /// </summary>
        /// <param name="teamCd"></param>
        /// <param name="torihikiCd"></param>
        /// <param name="keymanCd"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Keyman>>> searchKeyman(string teamCd, string torihikiCd, string keymanCd, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(teamCd), teamCd},
                {nameof(torihikiCd), torihikiCd},
                {nameof(keymanCd), keymanCd},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Keyman>>(ApiResources.HatF.Client.SearchKeyman, parameters);
        }

        /// <summary>
        /// 仕入先検索
        /// </summary>
        /// <param name="supplierCd"></param>
        /// <param name="supplierName"></param>
        /// <param name="supplierNameKana"></param>
        /// <param name="teamCd"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Supplier>>> searchSupplier(string supplierCd, string supplierName, string supplierNameKana, string teamCd, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(supplierCd), supplierCd},
                {nameof(supplierName), supplierName},
                {nameof(supplierNameKana), supplierNameKana},
                {nameof(teamCd), teamCd},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Supplier>>(ApiResources.HatF.Client.SearchSupplier, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierCd"></param>
        /// <param name="supplierName"></param>
        /// <param name="supplierNameKana"></param>
        /// <param name="teamCd"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<SupplierCategory>>> searchSupplierCategory(string supplierCd, string supplierName, string supplierNameKana, string teamCd, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(supplierCd), supplierCd},
                {nameof(supplierName), supplierName},
                {nameof(supplierNameKana), supplierNameKana},
                {nameof(teamCd), teamCd},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<SupplierCategory>>(ApiResources.HatF.Client.SearchSupplierCategory, parameters);
        }

        /// <summary>
        /// 郵便番号検索
        /// </summary>
        /// <param name="postCode"></param>
        /// <param name="address"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        async public Task<List<PostAddress>> searchPostCode(string postCode, string address, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(postCode), postCode},
                {nameof(address), address},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return (await Program.HatFApiClient.GetAsync<List<PostAddress>>(ApiResources.HatF.Client.SearchPostAddress, parameters)).Data;
        }

        /// <summary>
        /// 現場検索
        /// </summary>
        /// <param name="torihikiCd">得意先コード</param>
        /// <param name="keymanCode">キーマンコード</param>
        /// <param name="genbaCd">現場コード</param>
        /// <param name="genbaName">現場名</param>
        /// <param name="address">住所</param>
        /// <param name="rows">取得件数</param>
        /// <returns>検索結果</returns>
        public async Task<ApiResponse<List<SearchGenbaResult>>> SearchGenba(
            string torihikiCd, string keymanCode,
            string genbaCd, string genbaName, string address, 
            int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(genbaCd), genbaCd},
                {nameof(genbaName), genbaName},
                {nameof(address), address},
                {nameof(torihikiCd), torihikiCd},
                {nameof(keymanCode), keymanCode},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return await Program.HatFApiClient.GetAsync<List<SearchGenbaResult>>(ApiResources.HatF.Client.SearchGenba, parameters);
        }

        /// <summary>
        /// 工事店検索
        /// </summary>
        /// <param name="koujitenCd"></param>
        /// <param name="koujitenName"></param>
        /// <param name="torihikiCd"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Koujiten>>> searchKoujiten(string koujitenCd, string koujitenName, string torihikiCd, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(koujitenCd), koujitenCd},
                {nameof(koujitenName), koujitenName},
                {nameof(torihikiCd), torihikiCd},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Koujiten>>(ApiResources.HatF.Client.SearchKoujiten, parameters);
        }

        /// <summary>
        /// HAT商品検索
        /// </summary>
        /// <param name="supplierCd"></param>
        /// <param name="productNoOrProductName"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Product>>> searchProductHat(string supplierCd, string productNoOrProductName, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(supplierCd), supplierCd},
                {nameof(productNoOrProductName), productNoOrProductName},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Product>>(ApiResources.HatF.Client.SearchProductHat, parameters);
        }

        /// <summary>
        /// メーカー商品検索
        /// </summary>
        /// <param name="supplierCd"></param>
        /// <param name="productNoOrProductName"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<Product>>> searchProductMaker(string supplierCd, string productNoOrProductName, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(supplierCd), supplierCd},
                {nameof(productNoOrProductName), productNoOrProductName},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return Program.HatFApiClient.GetAsync<List<Product>>(ApiResources.HatF.Client.SearchProductMaker, parameters);
        }
        /// <summary>
        ///  取引先請求書一覧
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        async public Task<List<ViewInvoice>> searchTorihikiInvoices(DateTime startDate, DateTime endDate, int rows = DEFAULT_ROWS)
        {
            var parameters = new Dictionary<string, object>()
            {
                {nameof(startDate), startDate},
                {nameof(endDate), endDate},
                {nameof(rows), rows},
            }.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return (await Program.HatFApiClient.GetAsync<List<ViewInvoice>>(ApiResources.HatF.Client.TorihikiInvoices, parameters)).Data;
        }

        async public Task<List<ViewInvoice>> searchTorihikiInvoices(Dictionary<string, object> parameters, int rows = DEFAULT_ROWS)
        {
            parameters[nameof(rows)] = rows;
            return (await Program.HatFApiClient.GetAsync<List<ViewInvoice>>(ApiResources.HatF.Client.TorihikiInvoices, parameters)).Data;
        }
        /// <summary>
        ///  売上予定一覧
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Task<ApiResponse<List<ViewReadySale>>> SearchReadySalesAsync(Dictionary<string, object> parameters, int page, int rows = DEFAULT_ROWS)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            parameters[nameof(page)] = page;
            return Program.HatFApiClient.GetAsync<List<ViewReadySale>>(ApiResources.HatF.Client.ReadySales, parameters);
        }

        /// <summary>売上予定一覧の件数を取得</summary>
        /// <param name="parameters">検索条件</param>
        /// <returns>一覧の件数</returns>
        public Task<ApiResponse<int>> SearchReadySalesCountAsync(Dictionary<string, object> parameters)
        {
            return Program.HatFApiClient.GetAsync<int>(ApiResources.HatF.Client.ReadySalesCount, parameters);
        }

        /// <summary>売上確定一覧</summary>
        /// <param name="rows"></param>
        /// <returns>売上確定一覧</returns>
        public Task<ApiResponse<List<ViewFixedSale>>> SearchFixedSalesAsync(Dictionary<string, object> parameters, int page, int rows = DEFAULT_ROWS)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            parameters[nameof(page)] = page;
            return Program.HatFApiClient.GetAsync<List<ViewFixedSale>>(ApiResources.HatF.Client.FixedSales, parameters);
        }

        /// <summary>売上確定一覧の件数を取得</summary>
        /// <param name="parameters">検索条件</param>
        /// <returns>一覧の件数</returns>
        public Task<ApiResponse<int>> SearchFixedSalesCountAsync(Dictionary<string, object> parameters)
        {
            return Program.HatFApiClient.GetAsync<int>(ApiResources.HatF.Client.FixedSalesCount, parameters);
        }

        /// <summary>売上確定前利率異常一覧</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="searchItems">詳細検索条件</param>
        /// <returns>利率異常一覧</returns>
        public Task<ApiResponse<List<ViewInterestRateBeforeFix>>> SearchInterestRateBeforeFixesAsync(
            decimal? profitOver, decimal? profitUnder,
            int? suryoOver, int? suryoUnder,
            decimal? uriKinOver, decimal? uriKinUnder,
            List<FilterCriteria> searchItems)
        {
            return Program.HatFApiClient.PostAsync<List<ViewInterestRateBeforeFix>>(ApiResources.HatF.Client.InterestRateBeforeFix,
                new Dictionary<string, object>()
                {
                    {nameof(profitOver), profitOver},
                    {nameof(profitUnder), profitUnder},
                    {nameof(suryoOver), suryoOver},
                    {nameof(suryoUnder), suryoUnder},
                    {nameof(uriKinOver), uriKinUnder},
                },
                JsonConvert.SerializeObject(searchItems.Select(f => f.AsFilterOption()).ToList()));
        }

        /// <summary>売上確定後利率異常一覧</summary>
        /// <param name="profitOver">基本検索条件[利率がX%以上]</param>
        /// <param name="profitUnder">基本検索条件[利率がX%以下]</param>
        /// <param name="suryoOver">基本検索条件[数量がX個以上]</param>
        /// <param name="suryoUnder">基本検索条件[数量がX個以下]</param>
        /// <param name="uriKinOver">基本検索条件[売上金額がX円以上]</param>
        /// <param name="uriKinUnder">基本検索条件[売上金額がX円以下]</param>
        /// <param name="searchItems">詳細検索条件</param>
        /// <returns>利率異常一覧</returns>
        public Task<ApiResponse<List<ViewInterestRateFixed>>> SearchInterestRateFixedAsync(
            decimal? profitOver, decimal? profitUnder,
            int? suryoOver, int? suryoUnder,
            decimal? uriKinOver, decimal? uriKinUnder,
            List<FilterCriteria> searchItems)
        {
            return Program.HatFApiClient.PostAsync<List<ViewInterestRateFixed>>(ApiResources.HatF.Client.InterestRateFixed,
                new Dictionary<string, object>()
                {
                    {nameof(profitOver), profitOver},
                    {nameof(profitUnder), profitUnder},
                    {nameof(suryoOver), suryoOver},
                    {nameof(suryoUnder), suryoUnder},
                    {nameof(uriKinOver), uriKinUnder},
                },
                JsonConvert.SerializeObject(searchItems.Select(f => f.AsFilterOption()).ToList()));
        }
    }
}
