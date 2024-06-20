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
using HatFClient.Models;
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
    public partial class ME_CompanysMst : Form
    {
        private class MeCompanyMstGridManager : GridManagerBase<CompanysMst> { }
        private MeCompanyMstGridManager gridManager = new MeCompanyMstGridManager();
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
        private static readonly Type TARGET_MODEL = typeof(CompanysMst);
        private const string OUTFILE_NAME = "得意先一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public ME_CompanysMst()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
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
                string url = ApiResources.HatF.MasterEditor.CompanysMstGensearch;
                url = ApiHelper.AddQuery(url, "includeDeleted", chkIncludeDeleted.Checked); //削除済を含めるか
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<CompanysMst>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.MasterEditor.CompanysMstCountGensearch;
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



        private bool ToBool(object value)
        {
            if (value == null) { return false; }
            if (value == DBNull.Value) { return false; }

            if (bool.TryParse(value.ToString(), out bool result))
            {
                return result;
            }

            return false;
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<CompanysMstViewModel>(true);

            using (var searchFrm = new FrmAdvancedSearch(criteriaDefinition, gridManager.Filters))
            {
                // 検索条件のうちドロップダウン項目にするもの
                searchFrm.DropDownItems.Add(new SearchDropDownInfo
                {
                    FieldName = nameof(CompanysMstViewModel.NoSalesFlg),
                    DropDownItems = JsonResources.NoSalesFlags.ToDictionary(x => x.Name, x => x.Code.ToString())
                });

                gridManager.DropDownItems = searchFrm.DropDownItems;

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

            //// 区分値項目
            //grid.Cols[nameof(SupplierViewItem.PayMethodType)].DataMap = new ListDictionary() { { (short)short.MinValue, "" }, { (short)1, "1:振込" }, { (short)2, "2:手形" } };

            // 取引禁止フラグ
            ListDictionary noSalesFlgItems = new ListDictionary();
            JsonResources.NoSalesFlags.ForEach(item => noSalesFlgItems.Add(item.Code, item.Name));
            grid.Cols[nameof(CompanysMstViewModel.NoSalesFlg)].DataMap = noSalesFlgItems;

            // 削除日
            grid.Cols[nameof(CompanysMstViewModel.DeleteDate)].Format = "yyyy/MM/dd";

            // 表示列定義にはあるが初期状態非表示にする
            grid.Cols[nameof(CompanysMstViewModel.Deleted)].DataType = typeof(bool);
            grid.Cols[nameof(CompanysMstViewModel.Deleted)].Visible = chkIncludeDeleted.Checked;

            // ヘッダーのタイトル設定
            SetColumnCaptions();
        }

        private void SetColumnCaptions()
        {
            var grid = projectGrid1.c1FlexGrid1;

            int colIndex = 1;
            foreach (var prop in typeof(CompanysMstViewModel).GetProperties())
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "得意先一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            string compCode = (string)grid[grid.Row, nameof(CompanysMstViewModel.CompCode)];

            using (var form = new ME_CompanysMstDetail(compCode))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CompanysMstDetail())
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



        private class CompanysMstViewModel
        {
            [GenSearchVisiblity(false)]
            [CriteriaDefinition("削除済")]
            public bool Deleted { get; set; }

            [CriteriaDefinition("取引先コード")]
            public string CompCode { get; set; }

            [CriteriaDefinition("取引先名")]
            public string CompName { get; set; }

            [CriteriaDefinition("取引先名カナ")]
            public string CompKana { get; set; }

            [CriteriaDefinition("取引先名略称")]
            public string CompKanaShort { get; set; }

            [CriteriaDefinition("支店名(センター名)")]
            public string CompBranchName { get; set; }

            [CriteriaDefinition("仕入先区分")]
            public short? SupType { get; set; }

            [CriteriaDefinition("郵便番号")]
            public string ZipCode { get; set; }

            [CriteriaDefinition("都道府県")]
            public string State { get; set; }

            [CriteriaDefinition("住所１")]
            public string Address1 { get; set; }

            [CriteriaDefinition("住所２")]
            public string Address2 { get; set; }

            [CriteriaDefinition("住所３")]
            public string Address3 { get; set; }

            [CriteriaDefinition("Tel")]
            public string Tel { get; set; }

            [CriteriaDefinition("Fax")]
            public string Fax { get; set; }

            [CriteriaDefinition("Fax2")]
            public string Fax2 { get; set; }

            [CriteriaDefinition("取引禁止フラグ")]
            public short? NoSalesFlg { get; set; }

            [CriteriaDefinition("雑区分")]
            public short? WideUseType { get; set; }

            [CriteriaDefinition("取引先グループコード")]
            public string CompGroupCode { get; set; }

            [CriteriaDefinition("与信限度額")]
            public long? MaxCredit { get; set; }

            [CriteriaDefinition("与信一時増加枠")]
            public long? TempCreditUp { get; set; }

            [CriteriaDefinition("インボイス登録番号")]
            public string InvoiceRegistNumber { get; set; }

            [CriteriaDefinition("削除日")]
            public DateTime? DeleteDate { get; set; }
           
        }

        private async void chkIncludeDeleted_CheckedChanged(object sender, EventArgs e)
        {
            await gridManager.Reload();
        }
    }
}
