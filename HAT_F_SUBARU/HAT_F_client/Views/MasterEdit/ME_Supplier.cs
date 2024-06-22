using AutoMapper;
using AutoMapper.Configuration.Conventions;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using ClosedXML.Excel;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Helpers;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Estimate;
using HatFClient.Views.Search;
using HatFClient.Views.Warehousing;
using Irony.Parsing;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    public partial class ME_Supplier : Form
    {
        private class MeSupplierGridManager : GridManagerBase<SupplierMst> { }
        private MeSupplierGridManager gridManager = new MeSupplierGridManager();
        private GridOrderManager gridOrderManager;
        private List<SupplierMst> _gridData = new List<SupplierMst>();
        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        private DefaultGridPage projectGrid1;
        private static readonly Type TARGET_MODEL = typeof(SupplierMst);
        private const string OUTFILE_NAME = "仕入先一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public ME_Supplier()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);

                InitializeFetching();

                // INIT PATTERN
                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                this.gridPatternUI.Init(patternRepo);
            }
        }

        /// <summary>
        /// データ取得処理の初期化
        /// </summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.SupplierMstGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<SupplierMst>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件

                _gridData = apiResponse.Data;

                return apiResponse;

            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.SupplierMstCountGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void ME_Supplier_Load(object sender, EventArgs e)
        {
            InitializeGrid(); 

            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;
        }

        private void InitializeGrid()
        {
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<SupplierMst>());

            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();

            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            projectGrid1.c1FlexGrid1.SelectionMode = SelectionModeEnum.ListBox;
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            gridPatternUI.OnPatternSelected += OnPatternSelected;
        }


        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled != true)
            {
                return;
            }

            btnDetail.PerformClick();
        }

        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GdProjectList_RowColChange");

            //件数出力
            var rows = gridManager.Dt.Rows.Count;
            this.rowsCount.Text = rows + "件表示中";
            if (projectGrid1.GetProjectCount() != -1)
            {
                this.lblProjectAllCount.Text = "検索結果：" + projectGrid1.GetProjectCount().ToString() + "件";
            }
            else
            {
                this.lblProjectAllCount.Text = "検索結果：";
            }

            this.textFilterStr.Text = projectGrid1.GetFilterOptionStr();
            InitializeColumns();
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<SupplierViewItem>(true);

            using (var searchFrm = new FrmAdvancedSearch(criteriaDefinition, gridManager.Filters))
            {
                searchFrm.StartPosition = FormStartPosition.CenterParent;

                searchFrm.OnSearch += async (sender, e) =>
                {
                    await gridManager.Reload(searchFrm.FilterCriterias);
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    gridManager.SetFilters(searchFrm.FilterCriterias);
                    await gridManager.Reload(searchFrm.FilterCriterias);
                };

                searchFrm.OnReset += (sender, e) =>
                {
                    gridManager.SetFilters(new List<FilterCriteria>());
                };

                if (DialogHelper.IsPositiveResult(searchFrm.ShowDialog()))
                {
                    GdProjectList_RowColChange(this, EventArgs.Empty);
                }
            }
        }

        private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            updateDataTable();
        }

        private async void updateDataTable()
        {
            // 非同期でデータ取得    
            await gridManager.Reload(new List<FilterCriteria>());
            GdProjectList_RowColChange(this, EventArgs.Empty);
        }

        private void InitializeColumns()
        {
            var pattern = gridPatternUI.SelectedPattern;
            if (pattern == null)
            {
                return;
            }

            var grid = projectGrid1.c1FlexGrid1;

            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;
            gridOrderManager.InitializeGridColumns(grid, configs, true);

            grid.AllowResizing = AllowResizingEnum.Columns;

            // 表示列定義にはあるが初期状態非表示にする
            grid.Cols[nameof(SupplierViewItem.Deleted)].DataType = typeof(bool);
            grid.Cols[nameof(SupplierViewItem.Deleted)].Visible = chkIncludeDeleted.Checked;

            // ヘッダーのタイトル設定
            SetColumnCaptions();
        }


        private void SetColumnCaptions()
        {
            var grid = projectGrid1.c1FlexGrid1;

            int colIndex = 1;
            foreach (var prop in typeof(SupplierViewItem).GetProperties())
            {
                Column col = grid.Cols[prop.Name];
                if (col != null)
                {
                    var item = prop.GetCustomAttribute<CriteriaDefinitionAttribute>();
                    col.Name = prop.Name;
                    col.Caption = item.Label;
                    col.Move(colIndex++);
                }
            }

            for (int i = colIndex; i < grid.Cols.Count; i++)
            {
                grid.Cols[i].Visible = false;
            }
        }


        private void HideColumns(C1FlexGrid grid, IEnumerable<string> columnNames)
        {
            foreach (string colName in columnNames)
            {
                Column col = grid.Cols[colName];
                if (col != null)
                {
                    col.Visible = false;
                }
            }
        }


        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = string.Format(OUTFILE_NAME, DateTime.Now);   //OUTFILE_NAME定数の定義は "出荷指示一覧_{0:yyyyMMdd_HHmmss}.xlsx" のような書式に修正
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "仕入先一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var form = new ME_SupplierDetail())
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            string supCode = (string)grid[grid.Row, "SupCode"];

            using (var form = new ME_SupplierDetail(supCode))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }

        private object DbNullToClrNull(object val)
        {
            if (val == DBNull.Value) { return null; }
            return val;
        }

        private void btnSearchPrinted_Click(object sender, EventArgs e)
        {
            updateDataTable();
        }

        private async void chkIncludeDeleted_CheckedChanged(object sender, EventArgs e)
        {
            await gridManager.Reload();
        }

        private class SupplierViewItem
        {
            [GenSearchVisiblity(false)]
            [CriteriaDefinition("削除済")]
            public bool Deleted { get; set; }

            [CriteriaDefinition("仕入先コード")]
            public string SupCode { get; set; }

            [GenSearchVisiblity(false)]
            [CriteriaDefinition("仕入先枝番")]
            public short SupSubNo { get; set; }

            [CriteriaDefinition("仕入先名")]
            public string SupName { get; set; }

            [CriteriaDefinition("仕入先名カナ")]
            public string SupKana { get; set; }

            [CriteriaDefinition("仕入先担当者名")]
            public string SupEmpName { get; set; }

            [CriteriaDefinition("仕入先部門名")]
            public string SupDepName { get; set; }

            [CriteriaDefinition("仕入先郵便番号")]
            public string SupZipCode { get; set; }

            [CriteriaDefinition("仕入先都道府県")]
            public string SupState { get; set; }

            [CriteriaDefinition("仕入先住所１")]
            public string SupAddress1 { get; set; }

            [CriteriaDefinition("仕入先住所２")]
            public string SupAddress2 { get; set; }

            [CriteriaDefinition("仕入先電話番号")]
            public string SupTel { get; set; }

            [CriteriaDefinition("仕入先FAX番号")]
            public string SupFax { get; set; }

            [CriteriaDefinition("仕入先メールアドレス")]
            public string SupEmail { get; set; }

            [CriteriaDefinition("仕入先締日")]
            public short? SupCloseDate { get; set; }

            [CriteriaDefinition("仕入先支払月")]
            public short? SupPayMonths { get; set; }

            [CriteriaDefinition("仕入先支払日")]
            public short? SupPayDates { get; set; }

            [CriteriaDefinition("支払方法区分")]
            public short? PayMethodType { get; set; }

            [CriteriaDefinition("発注先種別")]
            public short? SupplierType { get; set; }
        }

    }

}
