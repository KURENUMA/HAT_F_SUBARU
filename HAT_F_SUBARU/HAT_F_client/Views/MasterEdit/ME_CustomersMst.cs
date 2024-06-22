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
using HatFClient.Models;
using HatFClient.Properties;
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
    public partial class ME_CustomersMst : Form
    {
        private class CurrentFormGridManager : GridManagerBase<CustomersMstEx> { }
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
        private static readonly Type TARGET_MODEL = typeof(CustomersMstEx);
        private const string OUTFILE_NAME = "顧客一覧_{0:yyyyMMdd_HHmmss}.xlsx";


        public ME_CustomersMst()
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
            gridManager.FetchFuncAsync = async (filter) => {

                string url = ApiResources.HatF.MasterEditor.CustomersMstGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<CustomersMstEx>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.CustomersMstCountGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void ME_CustomersMst_Load(object sender, EventArgs e)
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

        private void C1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        {
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<CustomersMstViewModel>(true);

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

            // 列幅調整可
            grid.AllowResizing = AllowResizingEnum.Columns;

            // 請求区分
            var arFlags = ToDataMap(JsonConvert.DeserializeObject<CodeName<short?>[]>(Resources.HatF_CustArFlag));
            grid.Cols[nameof(CustomersMstViewModel.CustArFlag)].DataMap = arFlags;

            // 締日1, 2
            var closeDayes = ToDataMap(JsonResources.CloseDates);
            grid.Cols[nameof(CustomersMstViewModel.CustCloseDate1)].DataMap = closeDayes;
            grid.Cols[nameof(CustomersMstViewModel.CustCloseDate2)].DataMap = closeDayes;


            // 支払月1, 2
            var payMonths = ToDataMap<KeyName>(HatF_PaymentMonthRepo.GetInstance().Entities);
            grid.Cols[nameof(CustomersMstViewModel.CustPayMonths1)].DataMap = payMonths;
            grid.Cols[nameof(CustomersMstViewModel.CustPayMonths2)].DataMap = payMonths;

            // 支払日1, 2
            var peyDays = ToDataMap<KeyName>(HatF_PaymentDayRepo.GetInstance().Entities);
            grid.Cols[nameof(CustomersMstViewModel.CustPayDates1)].DataMap = peyDays;
            grid.Cols[nameof(CustomersMstViewModel.CustPayDates2)].DataMap = peyDays;

            // 支払方法1, 2
            var payClasses = ToDataMap<KeyName>(HatF_PaymentClassificationRepo.GetInstance().Entities);
            grid.Cols[nameof(CustomersMstViewModel.CustPayMethod1)].DataMap = payClasses;
            grid.Cols[nameof(CustomersMstViewModel.CustPayMethod2)].DataMap = payClasses;


            // 表示列定義にはあるが初期状態非表示にする
            grid.Cols[nameof(CustomersMstViewModel.Deleted)].DataType = typeof(bool);
            grid.Cols[nameof(CustomersMstViewModel.Deleted)].Visible = chkIncludeDeleted.Checked;

            // ヘッダーのタイトル設定
            SetColumnCaptions();

        }

        private ListDictionary ToDataMap<T>(IEnumerable<CodeName<T>> source) 
        {
            var dictionary = new ListDictionary();
            foreach (var item in source.Where(x => x.Code != null))
            {
                dictionary.Add(item.Code, item.Name);
            }
            return dictionary;
        }

        private ListDictionary ToDataMap<T>(IEnumerable<KeyName> source)
        {
            var dictionary = new ListDictionary();
            foreach (var item in source.Where(x => x.Key != null))
            {
                dictionary.Add(item.Key, item.Name);
            }
            return dictionary;
        }

        private void SetColumnCaptions()
        {
            var grid = projectGrid1.c1FlexGrid1;

            int colIndex = 1;
            foreach (var prop in typeof(CustomersMstViewModel).GetProperties())
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "顧客一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            string custCode = (string)grid[grid.Row, nameof(CustomersMstViewModel.CustCode)];
            using (var form = new ME_CustomersMstDetail(custCode))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }

        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CustomersMstDetail())
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


        private class CustomersMstViewModel
        {
            [GenSearchVisiblity(false)]
            [CriteriaDefinition("削除済")]
            public bool Deleted { get; set; }

            [CriteriaDefinition("顧客コード")]
            public string CustCode { get; set; }

            [CriteriaDefinition("顧客名")]
            public string CustName { get; set; }

            [CriteriaDefinition("顧客名カナ")]
            public string CustKana { get; set; }

            [CriteriaDefinition("請求先コード")]
            public string ArCode { get; set; }

            [GenSearchVisiblity(false)]
            [CriteriaDefinition("請求先名")]
            public string ArName { get; set; }

            [CriteriaDefinition("自社担当者コード")]
            public string EmpCode { get; set; }

            [GenSearchVisiblity(false)]
            [CriteriaDefinition("自社担当者名")]
            public string EmpName { get; set; }

            [CriteriaDefinition("顧客部門名")]
            public string CustUserDepName { get; set; }

            [CriteriaDefinition("顧客郵便番号")]
            public string CustZipCode { get; set; }

            [CriteriaDefinition("顧客都道府県")]
            public string CustState { get; set; }

            [CriteriaDefinition("顧客住所１")]
            public string CustAddress1 { get; set; }

            [CriteriaDefinition("顧客住所２")]
            public string CustAddress2 { get; set; }

            [CriteriaDefinition("顧客住所３")]
            public string CustAddress3 { get; set; }

            [CriteriaDefinition("顧客電話番号")]
            public string CustTel { get; set; }

            [CriteriaDefinition("顧客FAX番号")]
            public string CustFax { get; set; }

            [CriteriaDefinition("顧客メールアドレス")]
            public string CustEmail { get; set; }

            [CriteriaDefinition("顧客請求区分")]
            public short? CustArFlag { get; set; }

            [CriteriaDefinition("顧客締日１")]
            public short? CustCloseDate1 { get; set; }

            [CriteriaDefinition("顧客支払月１")]
            public short? CustPayMonths1 { get; set; }

            [CriteriaDefinition("顧客支払日１")]
            public short? CustPayDates1 { get; set; }

            [CriteriaDefinition("顧客支払方法１")]
            public short? CustPayMethod1 { get; set; }

            [CriteriaDefinition("顧客締日２")]
            public short? CustCloseDate2 { get; set; }

            [CriteriaDefinition("顧客支払月２")]
            public short? CustPayMonths2 { get; set; }

            [CriteriaDefinition("顧客支払日２")]
            public short? CustPayDates2 { get; set; }

            [CriteriaDefinition("顧客支払方法２")]
            public short? CustPayMethod2 { get; set; }
        }
    }
}
