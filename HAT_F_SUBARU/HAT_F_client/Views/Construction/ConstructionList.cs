using C1.Win.C1FlexGrid;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.ConstructionProject
{
    public partial class ConstructionList : Form
    {
        // TODO: 画面固有のvGridManagervを定義
        private class ConstructionListGridManager : GridManagerBase<ViewConstruction> { }

        // TODO: TemplateGridManager から画面用の固有型へ変更 (GridManagerBase<T>の継承で作れます)
        private ConstructionListGridManager gridManager = new ConstructionListGridManager();

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
        private DefaultGridPage projectGrid1;   // TODO: TemplateGridPage⇒DefaultGridPage
        private static readonly Type TARGET_MODEL = typeof(HAT_F_api.Models.ViewConstruction);
        private const string OUTFILE_NAME = "物件一覧_yyyyMMdd_HHmmss";

        public ConstructionList()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                // TODO:追加
                InitializeFetching();

                FormStyleHelper.SetWorkWindowStyle(this);

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
                string url = ApiHelper.AddPagingQuery(ApiResources.HatF.Search.ConstructionList, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewConstruction>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.Search.ConstructionListCount;
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void ConstructionList_Load(object sender, EventArgs e)
        {
            // TODO:引数を変更
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<ViewConstruction>());

            //TODO: TemplateGridPage⇒DefaultGridPage差し替え（用件に合わなければTemplateGridPageをカスタム）
            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);  

            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;

            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();
            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;

            InitializeEvents();
        }

        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled == true)
            {
                btnDetail.PerformClick();
            }
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
            using (var searchFrm = new FrmAdvancedSearch(CriteriaHelper.CreateCriteriaDefinitions<ViewConstruction>(), gridManager.Filters))
            {
                var dropDwonItems = new List<SearchDropDownInfo>();

                SearchDropDownInfo sddi;
                sddi = new SearchDropDownInfo();
                sddi.FieldName = "受注状態";
                sddi.DropDownItems = new Dictionary<string, string>() { { "引合", "0" }, { "見積作成", "1" }, { "見積提出", "2" }, { "受注済", "3" }, { "完了", "4" } };
                dropDwonItems.Add(sddi);

                searchFrm.AddDropDownItems(dropDwonItems);

                gridManager.DropDownItems = searchFrm.DropDownItems;
;
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

            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;

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
            if (projectGrid1.c1FlexGrid1.Cols[nameof(ViewConstruction.受注状態)] != null)
            {
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewConstruction.受注状態)].DataMap = new ListDictionary()
                { 
                    { (short)short.MinValue, "" },
                    { (short)0, "引合" },
                    { (short)1, "見積作成" },
                    { (short)2, "見積提出" },
                    { (short)3, "受注済" },
                    { (short)4, "完了" } };
            }

            // ★削除予定 全列の幅を自動調整する
            projectGrid1.c1FlexGrid1.AutoSizeCols();
        }

        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            string constructionCode = grid.GetData(grid.RowSel, "物件コード").ToString();

            var detail = await ApiHelper.FetchAsync(this, () =>
                Program.HatFApiClient.GetAsync<ViewConstructionDetail>(ApiResources.HatF.Client.ConstructionDetail, new { constructionCode })
            );
            if (detail.Failed)
            {
                return;
            }

            //var ConstructionDetail = new ConstructionDetail();
            //ConstructionDetail.LoadDataAndShowAsync(ConstructionDetail.ScreenMode.Edit, detail.Value);
            var ConstructionDetail = new NewConstructionDetail();
            ConstructionDetail.LoadDataAndShowAsync(NewConstructionDetail.ScreenMode.Edit, detail.Value);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //var ConstructionDetail = FormFactory.GetModelessForm<ConstructionDetail>();
            //ConstructionDetail.LoadDataAndShowAsync(ConstructionDetail.ScreenMode.NewEntry);
            var ConstructionDetail = FormFactory.GetModelessForm<NewConstructionDetail>();
            ConstructionDetail.LoadDataAndShowAsync(NewConstructionDetail.ScreenMode.NewEntry);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<NewConstructionDetail>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }
    }
}
