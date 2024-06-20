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
    public partial class ME_Employee : Form
    {
        private class WhEmployeeGridManager : GridManagerBase<Employee> { }
        private WhEmployeeGridManager gridManager = new WhEmployeeGridManager();
        private GridOrderManager gridOrderManager;

        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        // 検索用の変数
        private DefaultGridPage projectGrid1;   // TODO: TemplateGridPage⇒DefaultGridPage
        private static readonly Type TARGET_MODEL = typeof(EmployeeViewColumn);
        private const string OUTFILE_NAME = "社員一覧_{0:yyyyMMdd_HHmmss}.xlsx";


        public ME_Employee()
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
                string url = ApiResources.HatF.MasterEditor.EmployeesGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<Employee>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件

                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.EmployeesCountGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ

                return count;
            };
        }

        private void WH_Receivings_Load(object sender, EventArgs e)
        {
            InitializeGrid();

            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;
        }

        //private void C1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        //{
        //}

        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled != true)
            {
                return;
            }

            btnDetail.PerformClick();
        }

        private void InitializeGrid()
        {
            //gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<Employee>());
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<EmployeeViewColumn>());

            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();

            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            //projectGrid1.c1FlexGrid1.MouseClick += C1FlexGrid1_MouseClick;
            projectGrid1.c1FlexGrid1.SelectionMode = SelectionModeEnum.ListBox;
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            gridPatternUI.OnPatternSelected += OnPatternSelected;
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
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<EmployeeViewColumn>(true);

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

            var grid = projectGrid1.c1FlexGrid1;

            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;
            gridOrderManager.InitializeGridColumns(grid, configs, true);

            // 表示列定義にはあるが初期状態非表示にする
            grid.Cols["Deleted"].DataType = typeof(bool);
            grid.Cols["Deleted"].Visible = chkIncludeDeleted.Checked;

            // ヘッダーのタイトル設定
            SetColumnCaptions();
        }

        private void SetColumnCaptions()
        {
            var grid = projectGrid1.c1FlexGrid1;

            int colIndex = 1;
            foreach(var prop in typeof(EmployeeViewColumn).GetProperties())
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "社員一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            // 新規登録
            using (var form = new ME_EmployeeDetail(null))
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
            if (grid.Rows.Count < 2) { return; }

            // 修正対象の社員ID(≠社員CD)
            int empId = (int)grid[grid.Row, "EmpId"];

            // 修正モードで詳細表示
            using (var form = new ME_EmployeeDetail(empId))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            int empId = (int)grid[grid.Row, "EmpId"];
            string empCode = (string)grid[grid.Row, "EmpCode"];

            using (var form = new ME_PasswordSetting(empId, empCode, true))
            {
                form.ShowDialog();
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

        private class EmployeeViewColumn 
        {
            [GenSearchVisiblity(false)]
            [CriteriaDefinition("削除済")]
            public bool Deleted { get; set; }

            [CriteriaDefinition("社員コード")]
            public string EmpCode { get; set; }

            [CriteriaDefinition("社員名")]
            public string EmpName { get; set; }

            [CriteriaDefinition("社員名カナ")]
            public string EmpKana { get; set; }

            [CriteriaDefinition("社員指定タグ")]
            public string EmpTag { get; set; }

            [CriteriaDefinition("電話番号")]
            public string Tel { get; set; }

            [CriteriaDefinition("FAX番号")]
            public string Fax { get; set; }

            [CriteriaDefinition("メールアドレス")]
            public string Email { get; set; }

            [CriteriaDefinition("部門コード")]
            public string DeptCode { get; set; }

            [CriteriaDefinition("職種コード")]
            public string OccuCode { get; set; }

            [CriteriaDefinition("役職コード")]
            public string TitleCode { get; set; }
        }
    }

}
