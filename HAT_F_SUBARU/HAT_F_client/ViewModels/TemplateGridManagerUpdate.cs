using Dma.DatasourceLoader;
using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.ViewModels
{
    public class TemplateGridManagerUpdate : IGridManager
    {
        public List<HAT_F_api.Models.ViewReadySale> dataSource { get; set; }
        public int TotalPages { get; private set; } = 1;
        public int Total { get; private set; } = 0;
        public string FilterOptionStr { get; private set; } = "";

        private readonly DataTable dt = new DataTable();
        private List<FilterCriteria> filters = new List<FilterCriteria>();
        public event System.EventHandler OnDataSourceChange;

        private int currentPage = 1;
        public int CurrentPage { get { return currentPage; } }

        private int pageSize = 200;
        public DataTable Dt => dt;
        public void SetCurrentPage(int currentPage) { this.currentPage = currentPage; }
        public void SetPageSize(int pageSize) { this.pageSize = pageSize; }
        public void SetFilters(List<FilterCriteria> filters) { this.filters = filters; }
        public List<FilterCriteria> Filters => filters;

        public TemplateGridManagerUpdate()
        {

            this.dt = new DataTable();
            foreach (var config in TemplateHelpers.TemplateColumnConfigs)
            {
                dt.Columns.Add(config.Caption, config.DataType);
            }
        }

        public async Task<bool> Reload()
        {
            await Reload(this.filters);
            return true;
        }
        public async Task<bool> Reload(List<FilterCriteria> filters)
        {
            FilterOptionStr = "";
            Total = -1;

            dt.Clear();
            var conditions = filters.Select(f => f.AsFilterOption()).ToList();
            var resp = await Program.HatFApiClient.PostAsync<List<HAT_F_api.Models.ViewReadySale>>(ApiResources.HatF.Search.ViewReadySale, JsonConvert.SerializeObject(conditions));
            this.dataSource = resp.Data;

            //検索条件を表示用文字列に変換
            FilterOptionStr = CreateFilterOptionStr(filters.Select(f => f.AsFilterOptionAndCaption()).ToList());

            var query = dataSource.AsQueryable();

            if (filters != null)
                query = DataSourceLoader.ApplyCombinedFilters(query, filters.Select(f => f.AsFilterOption()).ToList());
            var paginatedQuery = query
                .Take(pageSize);
            Total = query.Count();

            PopulateDataToDataTable(paginatedQuery.ToList(), true);
            OnDataSourceChange?.Invoke(this, EventArgs.Empty);

            return true;
        }

        private void PopulateDataToDataTable(List<HAT_F_api.Models.ViewReadySale> list, bool shouldReset = false)
        {
            dt.BeginLoadData();

            if (shouldReset)
            {
                // データテーブルを初期化する
                dt.Clear();
            }
            // 一時的にdtをDtProjectに変更する
            foreach (var data in list)
            {
                DataRow newRow = dt.NewRow();

                foreach (var config in TemplateHelpers.TemplateColumnConfigs)
                {
                    // ColumnConfig のマッピングストラテジーを使用してデータを変換する
                    object mappedValue = config.MapData(data);
                    if (mappedValue == null)
                    {
                        mappedValue = DBNull.Value; // すべてのデータ型に対して DBNull.Value をセット
                    }
                    newRow[config.Caption] = mappedValue;
                }

                dt.Rows.Add(newRow);
            }

            dt.EndLoadData();
            dt.AcceptChanges();
        }

        private string CreateFilterOptionStr(List<(FilterCombinationTypes, FilterOption, string)> filterOptions)
        {
            StringBuilder sb = new StringBuilder();
            int cnt = 0;
            foreach (var f in filterOptions)
            {
                if (cnt == 0)
                {
                    sb.AppendLine(" 項目名：「" + f.Item3 + "」 条件：「" + f.Item2.Operator + "」 値：「" + f.Item2.Value + "」");
                }
                else
                {
                    sb.AppendLine(f.Item1.ToString() + " 項目名：「" + f.Item3 + "」 条件：「" + f.Item2.Operator + "」 値：「" + f.Item2.Value + "」");
                }
                cnt++;
            }
            return sb.ToString();
        }
    }
}
