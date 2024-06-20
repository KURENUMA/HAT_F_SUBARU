using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;

namespace HatFClient.Repository
{
    class FosJyuchuRepo
    {
        private FosJyuchuRepo() { }

        private static FosJyuchuRepo instance = null;

        public List<FosJyuchuSearch> SearchResults { get; set; }

        public List<FosJyuchuPage> Pages { get; set; }

        //private String SaveKey { get; set; }
        //private String DenSort { get; set; }

        public String GetCurrentSaveKey()
        {
            if (Pages == null || Pages.Count == 0)
            {
                return null;
            }
            return Pages.Select(p => p.FosJyuchuH.SaveKey).FirstOrDefault();
        }


        public static FosJyuchuRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new FosJyuchuRepo();
            }
            return instance;
        }

        public async Task<ApiResponse<List<FosJyuchuSearch>>> SearchAsync(FosJyuchuSearchCondition param)
        {
            ApiResponse<List<FosJyuchuSearch>> response = await Program.HatFApiClient.PostAsync<List<FosJyuchuSearch>>(ApiResources.HatF.Client.Search, param);
            SearchResults = ApiHelper.IsPositiveResponse(response) ? response.Data : new List<FosJyuchuSearch>();
            return response;
        }

        public async Task<ApiResponse<List<FosJyuchuPage>>> GetPages(string saveKey)
        {
            var apiResult = await Program.HatFApiClient.GetAsync<List<FosJyuchuPage>>(string.Format(ApiResources.HatF.Client.GetOrder, saveKey));
            if(ApiHelper.IsPositiveResponse(apiResult))
            {
                Pages = apiResult.Data;
            }
            return apiResult;
        }

        private void log(string msg) {
            Debug.WriteLine($"FosJyuchuRepo:{msg}");
        }

        public void ClearPages()
        {
            if (this.Pages != null)
            {
                this.Pages.Clear();
            }
            this.Pages = null;
        }

        public async Task<ApiResponse<int>> Delete(List<(string SaveKey, string DenSort)> deleteList)
        {
            ApiResponse<int> apiResponse = null;

            for (int i = 0; i < deleteList.Count; i++)
            {
                var (saveKey, denSort) = deleteList[i];

                apiResponse = await Program.HatFApiClient.DeleteAsync<int>(string.Format(ApiResources.HatF.Client.DeleteHeader, saveKey, denSort));
                if (ApiHelper.IsNegativeResponse(apiResponse)) 
                {
                    return apiResponse;
                }
            }

            return apiResponse ?? new ApiOkResponse<int>(0);
        }

        #region tmp cache
        private Dictionary<String, List<FosJyuchuPage>> _tmpCache = new Dictionary<string, List<FosJyuchuPage>>();

        public void ClearTmpCache() { _tmpCache.Clear(); }

        public Boolean HasTmpCache(String key) { return key != null && _tmpCache.ContainsKey(key); }

        public List<FosJyuchuPage> GetTmpCache(String key) { return _tmpCache[key]; }

        public void SetTmpCache(List<FosJyuchuPage> pages)
        {
            var saveKey = pages.Select(p => p.FosJyuchuH.SaveKey).First();
            if (HasTmpCache(saveKey))
            {
                _tmpCache.Remove(saveKey);
            }
            _tmpCache.Add(saveKey, pages);
        }
        #endregion

        public String CreateNewSaveKey(String jyu2Cd, String jyu2)
        {
            String fmtJyu2Cd = $"0000{jyu2Cd}";
            if (string.IsNullOrEmpty(jyu2)) {
                jyu2 = "A";
            }
            return jyu2 + fmtJyu2Cd.Substring(fmtJyu2Cd.Length - 4, 4) + DateTime.Now.ToString("yyyyMMddHHmmddssfff");
        }
        private T parseResult<T>(ApiResponse<T> response) {
            if (!ApiHelper.IsPositiveResponse(response)) {
                MessageBox.Show(@"更新エラー", "Error " + MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
            return response.Data;
        }

        public async Task<ApiResponse<CompleteHeaderResult>> GetCompleteHeader(CompleteHeaderRequest request)
        {
            return await Program.HatFApiClient.PostAsync<CompleteHeaderResult>(ApiResources.HatF.Client.CompleteHeader, request);
        }

        public async Task<ApiResponse<CompleteDetailsResult>> GetCompleteDetails(CompleteDetailsRequest request)
        {
            return await Program.HatFApiClient.PostAsync<CompleteDetailsResult>(ApiResources.HatF.Client.CompleteDetails, request);
        }
        private async Task<ApiResponse<List<FosJyuchuPage>>> callDataCollectionApi(string putApiUrl, FosJyuchuPages pages) {

            try {
                log("IN:" + Newtonsoft.Json.JsonConvert.SerializeObject(pages));

                var result = await Program.HatFApiClient.PutAsync<List<FosJyuchuPage>>(putApiUrl, pages);
                var data = parseResult(result);
                if (data != null) {
                    ClearPages();
                    this.Pages = data;
                }
                log("OUT:" + (data != null ? Newtonsoft.Json.JsonConvert.SerializeObject(data): "null")) ;
                return result;
            } catch (Exception ex) {
                MessageBox.Show(@"更新エラー:" + ex.Message, "Error " + MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public async Task<ApiResponse<List<FosJyuchuPage>>> putOrderCommit(FosJyuchuPages pages) {
            return await callDataCollectionApi(ApiResources.HatF.Client.OrderCommit, pages);
        }
        public async Task<ApiResponse<List<FosJyuchuPage>>> putOrderlCollation(FosJyuchuPages pages) {
            return await callDataCollectionApi(ApiResources.HatF.Client.OrderCollation, pages);
        }

    }
}
