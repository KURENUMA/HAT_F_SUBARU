using AutoMapper;
using C1.Win.C1FlexGrid;
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
    public partial class ME_CustomersUserMst : Form
    {
        private class CurrentFormGridManager : GridManagerBase<CustomersUserMst> { }
        private CurrentFormGridManager gridManager = new CurrentFormGridManager();
        private GridOrderManager gridOrderManager;
        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        private DefaultGridPage projectGrid1;   // TODO: TemplateGridPage⇒DefaultGridPage
        private static readonly Type TARGET_MODEL = typeof(CustomersUserMst);
        private const string OUTFILE_NAME = "顧客担当者一覧_{0:yyyyMMdd_HHmmss}.xlsx";


        public ME_CustomersUserMst()
        {
            InitializeComponent();           

            if (!this.DesignMode)
            {
                // TODO:追加
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
            gridManager.FetchFuncAsync = async (filter) => {

                // API(ページング条件付与)
                string url = ApiHelper.AddPagingQuery(ApiResources.HatF.MasterEditor.CustomersUserMstGensearch, gridManager.CurrentPage, gridManager.PageSize);
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか

                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<CustomersUserMst>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.CustomersUserMstCountGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか

                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void ME_DestinaitonsMst_Load(object sender, EventArgs e)
        {
            InitializeGrid();

            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;
        }

        private void InitializeGrid()
        {
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<CompanysMst>());

            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();

            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
        }

        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled != true)
            {
                return;
            }

            btnDetail.PerformClick();
        }

        private void InitializeEvents()
        {
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
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

        private void BtnTabClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<CustomersUserMstViewModel>();
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
            GdProjectList_RowColChange(this, EventArgs.Empty);
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


            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;

            var grid = projectGrid1.c1FlexGrid1;
            gridOrderManager.InitializeGridColumns(grid, configs, true);

            // 列幅調整調整可
            grid.AllowResizing = AllowResizingEnum.Columns;

            grid.Cols[nameof(CustomersUserMstViewModel.CustCode)].Width = 100;
            grid.Cols[nameof(CustomersUserMstViewModel.CustUserCode)].Width = 150;
            grid.Cols[nameof(CustomersUserMstViewModel.CustUserName)].Width = 150;
            grid.Cols[nameof(CustomersUserMstViewModel.CustUserEmail)].Width = 200;


            // 表示列定義にはあるが初期状態非表示にする
            grid.Cols[nameof(CustomersUserMstViewModel.Deleted)].DataType = typeof(bool);
            grid.Cols[nameof(CustomersUserMstViewModel.Deleted)].Visible = chkIncludeDeleted.Checked;

            // ヘッダーのタイトル設定
            SetColumnCaptions();
        }


        private void SetColumnCaptions()
        {
            var grid = projectGrid1.c1FlexGrid1;

            int colIndex = 1;
            foreach (var prop in typeof(CustomersUserMstViewModel).GetProperties())
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
            foreach(string colName in columnNames)
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "出荷先一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            string custCode = (string)grid[grid.Row, nameof(CustomersUserMst.CustCode)];
            string custUserCode = (string)grid[grid.Row, nameof(CustomersUserMst.CustUserCode)];

            using (var form = new ME_CustomersUserMstDetail(custCode, custUserCode))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CustomersUserMstDetail())
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

        private class CustomersUserMstViewModel
        {
            [GenSearchVisiblity(false)]
            [CriteriaDefinition("削除済")]
            public bool Deleted { get; set; }

            /// <summary>
            /// 顧客コード,取引先CD6桁:KOJICD 13桁 + 予備
            /// </summary>
            [CriteriaDefinition("顧客コード")]
            public string CustCode { get; set; }

            /// <summary>
            /// 担当者コード (キーマンCD)
            /// </summary>
            [CriteriaDefinition("担当者コード (キーマンCD)")]
            public string CustUserCode { get; set; }

            /// <summary>
            /// 担当者名 (キーマン名)
            /// </summary>
            [CriteriaDefinition("担当者名 (キーマン名)")]
            public string CustUserName { get; set; }

            /// <summary>
            /// 担当者メールアドレス
            /// </summary>
            [CriteriaDefinition("担当者メールアドレス")]
            public string CustUserEmail { get; set; }

            ///// <summary>
            ///// 削除済
            ///// </summary>
            //public bool? Deleted { get; set; }
        }
    }
}
