//using AutoMapper;
//using Dma.DatasourceLoader;
//using Dma.DatasourceLoader.Creator;
//using Dma.DatasourceLoader.Models;
//using HAT_F_api.CustomModels;
//using HAT_F_api.Models;
//using HatFClient.Common;
//using HatFClient.Constants;
//using HatFClient.Helpers;
//using HatFClient.Views.Search;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace HatFClient.ViewModels
//{
//    //class TestClass<T> : SourceConvertingGridManagerBase<T, T> where T : class { }


//    public class SourceConvertingGridManagerBase<T, TSource>: IGridManager where T : class
//    {
//        public Form TargetForm { get; set; }

//        public List<T> dataSource { get; set; }
//        public int TotalPages { get; private set; } = 1;
//        public int Total { get; private set; } = 0;
//        public string FilterOptionStr { get; private set; } = "";

//        private readonly DataTable dt = new DataTable();
//        private List<FilterCriteria> filters = new List<FilterCriteria>();
//        public event System.EventHandler OnDataSourceChange;

//        private int currentPage = 1;
//        public int CurrentPage { get { return currentPage; } }

//        private int pageSize = 200;
//        public int PageSize { get { return pageSize; } }

//        public DataTable Dt => dt;
//        public void SetCurrentPage(int currentPage) { this.currentPage = currentPage; }
//        public void SetPageSize(int pageSize) { this.pageSize = pageSize; }
//        public void SetFilters(List<FilterCriteria> filters) { this.filters = filters; }
//        public List<FilterCriteria> Filters => filters;

//        public SourceConvertingGridManagerBase()
//        {
//            foreach (var config in CriteriaHelper.CreateCriteriaDefinitions<T>())
//            {
//                dt.Columns.Add(config.Caption, config.DataType);
//            }
//        }

//        public async Task<bool> Reload()
//        {
//            return await Reload(this.filters);
//        }

//        internal virtual string ApiUrl { get; }

//        internal virtual Task<ApiResponse<List<T>>> FetchAsync(string apiUrl, List<FilterCriteria> filters) { return null; }

//        /// <summary>
//        /// 一覧用データ取得APIアクセス処理
//        /// </summary>
//        /// <remarks>
//        /// ここに設定した処理はApiHelper.FetchAsyncを通して実行されます（プログレスバーやエラー時のメッセージ表示等）
//        /// </remarks>
//        internal Func<List<FilterCriteria>, Task<ApiResponse<List<TSource>>>> FetchFuncAsync { get; set; }

//        /// <summary>
//        /// 件数データ取得APIアクセス処理
//        /// </summary>
//        /// <remarks>
//        /// ここに設定した処理はApiHelper.FetchAsyncを通して実行されます（プログレスバーやエラー時のメッセージ表示等）
//        /// </remarks>
//        internal Func<List<FilterCriteria>, Task<ApiResponse<int>>> FetchCountFuncAsync { get; set; }

//        /// <summary>
//        /// データソースから取得したデータをGridManagerが扱う型に変換
//        /// </summary>
//        internal Func<List<TSource>, List<T>> SourceConvertFunc { get; set; }


//        public async Task<bool> Reload(List<FilterCriteria> filters)
//        {
//            FilterOptionStr = "";
//            Total = -1;

//            dt.Clear();
//            var conditions = filters.Select(f => f.AsFilterOption()).ToList();

//            //var resp = await Program.HatFApiClient.PostAsync<List<HAT_F_api.Models.ViewReadySale>>(ApiResources.HatF.SearchViewReadySale, JsonConvert.SerializeObject(conditions));
//            //ApiResult<List<T>> apiResult = await ApiHelper.FetchAsync<List<T>>(this.TargetForm, async () => {
//            //    return await FetchAsync(this.ApiUrl, filters);
//            //});

//            ApiResult<List<T>> apiResult;
//            if (this.FetchFuncAsync != null)
//            {
//                apiResult = await ApiHelper.FetchAsync<List<T>>(this.TargetForm, async () => {
//                    //return await this.FetchFuncAsync(filters);

//                    ApiResponse<List<TSource>> sourceApiResponse = await this.FetchFuncAsync(filters);
//                    //return new ApiResponse<List<T>>(this.SourceConvertFunc(source.Data));

//                    var covertedApiResponse = ApiHelper.ConvertApiResponseDataType<TSource, T>(sourceApiResponse, this.SourceConvertFunc);
//                    return covertedApiResponse;
//                });
//            }
//            else
//            {
//                // TODO:こちらの処理は削除予定
//                apiResult = await ApiHelper.FetchAsync<List<T>>(this.TargetForm, async () => {
//                    return await FetchAsync(this.ApiUrl, filters);
//                });
//            }

//            if (apiResult.Failed)
//            {
//                return false;
//            }

//            this.dataSource = apiResult.Value;

//            //検索条件を表示用文字列に変換
//            FilterOptionStr = CreateFilterOptionStr(filters.Select(f => f.AsFilterOptionAndCaption()).ToList());

//            var query = dataSource.AsQueryable();

//            if (filters != null)
//            {
//                query = DataSourceLoader.ApplyCombinedFilters(query, filters.Select(f => f.AsFilterOption()).ToList());
//            }

//            var paginatedQuery = query.Take(pageSize);
//            if (this.FetchCountFuncAsync != null)
//            {
//                ApiResult<int> apiCountResult = await ApiHelper.FetchAsync<int>(this.TargetForm, async () => {
//                    return await this.FetchCountFuncAsync(filters);
//                });

//                if (apiCountResult.Failed)
//                {
//                    return false;
//                }
//                Total = apiCountResult.Value;
//            }
//            else
//            {
//                Total = query.Count();
//            }
//            TotalPages = CalcTotalPages(Total, pageSize);

//            PopulateDataToDataTable(paginatedQuery.ToList(), true);
//            OnDataSourceChange?.Invoke(this, EventArgs.Empty);

//            return true;
//        }

//        private void PopulateDataToDataTable(List<T> list, bool shouldReset = false)
//        {
//            if (shouldReset)
//            {
//                // データテーブルを初期化する
//                dt.Clear();
//            }
//            // 一時的にdtをDtProjectに変更する
//            foreach (var data in list)
//            {
//                DataRow newRow = dt.NewRow();

//                var criteria = CriteriaHelper.CreateCriteriaDefinitions<T>();
//                //foreach (var config in TemplateHelpers.TemplateColumnConfigs)
//                foreach (var config in criteria)
//                {
//                    // ColumnConfig のマッピングストラテジーを使用してデータを変換する
//                    object mappedValue = config.MapData(data);
//                    if (mappedValue == null)
//                    {
//                        mappedValue = DBNull.Value; // すべてのデータ型に対して DBNull.Value をセット
//                    }
//                    newRow[config.Caption] = mappedValue;
//                }

//                dt.Rows.Add(newRow);
//            }
//        }

//        private int CalcTotalPages(int records, int pageSize)
//        {
//            int val = (int)Math.Ceiling((double)records / (double)pageSize);
//            val = Math.Max(val, 1);
//            return val;
//        }

//        private string CreateFilterOptionStr(List<(FilterCombinationTypes, FilterOption, string)> filterOptions)
//        {
//            StringBuilder sb = new StringBuilder();
//            int cnt = 0;
//            foreach (var f in filterOptions)
//            {
//                if (cnt == 0)
//                {
//                    sb.AppendLine(" 項目名：「" + f.Item3 + "」 条件：「" + f.Item2.Operator + "」 値：「" + f.Item2.Value + "」");
//                }
//                else
//                {
//                    sb.AppendLine(f.Item1.ToString() + " 項目名：「" + f.Item3 + "」 条件：「" + f.Item2.Operator + "」 値：「" + f.Item2.Value + "」");
//                }
//                cnt++;
//            }
//            return sb.ToString();
//        }
//    }
//}
