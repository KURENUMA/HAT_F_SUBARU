using C1.Win.C1FlexGrid;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Models;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Cooperate;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.Purchase
{
    public partial class Purchase_Receiving : Form
    {
        private class PurchaseReceivingGridManager : GridManagerBase<ViewPurchaseReceiving> { }

        private PurchaseReceivingGridManager gridManager = new PurchaseReceivingGridManager();
        //データテーブルレイアウト
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
        private DefaultGridPage projectGrid1;
        private static readonly Type TARGET_MODEL = typeof(HAT_F_api.Models.ViewPurchaseReceiving);
        private const string OUTFILE_NAME = "仕入納品確定一覧_yyyyMMdd_HHmmss";

        public Purchase_Receiving()
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
                //TODOAPIの定義を追加
                // API(ページング条件付与)
                var url = ApiHelper.AddPagingQuery(ApiResources.HatF.Search.ViewPurchaseReceiving, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewPurchaseReceiving>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.Search.ViewPurchaseReceivingCount;
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void Purchase_Receiving_Load(object sender, EventArgs e)
        {

            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<ViewPurchaseReceiving>());
            //デフォルトの検索条件をセット
            //DateTime currentDate = DateTime.Today;
            //DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            //DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //gridManager.SetFilters(new List<FilterCriteria> { new FilterCriteria(CriteriaHelper.CreateCriteriaDefinitions<ViewPurchaseReceiving>(), 8, firstDayOfMonth, lastDayOfMonth) });
            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += GrdList_MouseDoubleClick;

            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();
            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
        }

        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
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
            var criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<ViewPurchaseReceiving>();
            using (var searchFrm = new FrmAdvancedSearch(criteriaDefinition, gridManager.Filters))
            {
                // 検索条件のうちドロップダウン項目にするもの
                //searchFrm.DropDownItems.Add(new SearchDropDownInfo
                //{
                //    FieldName = nameof(ViewPurchaseReceiving.仕入先締日),
                //    DropDownItems = JsonResources.CloseDates.ToDictionary(x => x.Name, x => x.Code.ToString())
                //});
                searchFrm.DropDownItems.Add(new SearchDropDownInfo
                {
                    FieldName = nameof(ViewPurchaseReceiving.仕入先支払月),
                    DropDownItems = JsonResources.PayMonths.ToDictionary(x => x.Name, x => x.Code.ToString())
                });
                searchFrm.DropDownItems.Add(new SearchDropDownInfo
                {
                    FieldName = nameof(ViewPurchaseReceiving.仕入先支払日),
                    DropDownItems = JsonResources.PayDates.ToDictionary(x => x.Name, x => x.Code.ToString())
                });
                searchFrm.DropDownItems.Add(new SearchDropDownInfo
                {
                    FieldName = nameof(ViewPurchaseReceiving.仕入先支払方法区分),
                    DropDownItems = JsonResources.PayMethodTypes.ToDictionary(x => x.Name, x => x.Code.ToString())
                });

                gridManager.DropDownItems = searchFrm.DropDownItems;

                searchFrm.StartPosition = FormStartPosition.CenterParent;

                searchFrm.OnSearch += async (sender, e) =>
                {
                    gridManager.SetCurrentPage(1);
                    await gridManager.Reload(searchFrm.FilterCriterias);
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    gridManager.SetCurrentPage(1);
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

            //gridManager.OnDataSourceChange += GdProjectList_RowColChange;


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

            // 仕入締日
            ListDictionary closeDateItems = new ListDictionary();
            JsonResources.CloseDates.ForEach(item => closeDateItems.Add(item.Code, item.Name));
            //JsonResources.CloseDates.ForEach(item => closeDateItems.Add(item.Code != null ? item.Code : "", item.Name));
            //grid.Cols[nameof(ViewPurchaseReceiving.仕入先締日)].DataMap = closeDateItems;

            // 仕入支払月
            ListDictionary payMonthsItems = new ListDictionary();
            JsonResources.PayMonths.ForEach(item => payMonthsItems.Add(item.Code, item.Name));
            grid.Cols[nameof(ViewPurchaseReceiving.仕入先支払月)].DataMap = payMonthsItems;

            // 仕入先支払日
            ListDictionary payDatesItems = new ListDictionary();
            JsonResources.PayDates.ForEach(item => payDatesItems.Add(item.Code, item.Name));
            grid.Cols[nameof(ViewPurchaseReceiving.仕入先支払日)].DataMap = payDatesItems;

            // 仕入先支払方法区分
            ListDictionary payMethodTypeItems = new ListDictionary();
            JsonResources.PayMethodTypes.ForEach(item => payMethodTypeItems.Add(item.Code, item.Name));
            grid.Cols[nameof(ViewPurchaseReceiving.仕入先支払方法区分)].DataMap = payMethodTypeItems;

            projectGrid1.c1FlexGrid1.Cols.Count = configs.Count + 1;
            projectGrid1.c1FlexGrid1.Cols[0].Caption = "";
            projectGrid1.c1FlexGrid1.Cols[0].Width = 30;

            int columnIndexOffset = 1;
            for (int i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = projectGrid1.c1FlexGrid1.Cols[i + columnIndexOffset];

                col.Caption = config.Label;
                col.Width = config.Width;
                col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                col.Name = config.VarName;
                col.AllowFiltering = AllowFiltering.None;
            }
            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
                grid.Cols[nameof(ViewPurchaseReceiving.発注金額合計)]
            );
        }

        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled == true)
            {
                btnDetail.PerformClick();
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            Purchase_ReceivingCheck detail = FormFactory.GetModelessForm<Purchase_ReceivingCheck>();
            detail.Condition.Hat注文番号 = grid.GetData(grid.RowSel, "Hat注文番号").ToString();
            detail.Show();
            //detail.Init(xxxxxx);
            detail.Activate();
        }

        private void btnDataInput_Click(object sender, EventArgs e)
        {
            //TODO取り込み画面遷移
        }

        private void btnContactEmail_Click(object sender, EventArgs e)
        {
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Purchase_ReceivingCheck detail = FormFactory.GetModelessForm<Purchase_ReceivingCheck>();
            detail.Show();
            //detail.Init(xxxxxx);
            detail.Activate();
        }

        private void Purchase_Receiving_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormFactory.GetModelessFormCache<Purchase_ReceivingCheck>()?.Close();

        }
    }
}
