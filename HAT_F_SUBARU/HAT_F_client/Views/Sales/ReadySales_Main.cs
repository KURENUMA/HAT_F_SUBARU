using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Sales
{
    /// <summary>売上予定一覧</summary>
    public partial class ReadySales_Main : Form
    {
        /// <summary>グリッド管理</summary>
        private class ReadySalesGridManager : GridManagerBase<ViewReadySale> { }

        /// <summary>グリッド管理</summary>
        private ReadySalesGridManager gridManager = new ReadySalesGridManager();

        /// <summary></summary>
        private List<ColumnMappingConfig> _criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<ViewReadySalesSearchCondition>();

        // 検索用の変数
        private DefaultGridPage projectGrid1;

        /// <summary>パターン設定画面で使うオブジェクトの型</summary>
        private static readonly Type TARGET_MODEL = typeof(ViewReadySale);

        /// <summary>出力Excel名テンプレート</summary>
        private const string OUTFILE_NAME = "売上予定一覧_yyyyMMdd_HHmmss";

        /// <summary>コンストラクタ</summary>
        public ReadySales_Main()
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

        /// <summary>データ取得処理の初期化</summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) =>
            {
                // API(ページング条件付与)
                var url = ApiHelper.AddPagingQuery(ApiResources.HatF.Client.ReadySales, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewReadySale>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.Client.ReadySalesCount;
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void ReadySales_Main_Load(object sender, EventArgs e)
        {
            projectGrid1 = new DefaultGridPage(gridManager)
            {
                Dock = DockStyle.Fill,
                Visible = false,
            };
            projectGrid1.c1FlexGrid1.AllowFiltering = false;

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

        /// <summary>現在のセルが変更された後に発生します ( RowプロパティとColプロパティ)。</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
            // 件数出力
            var rows = gridManager.Dt.Rows.Count;
            this.rowsCount.Text = $"{rows}件表示中";
            if (projectGrid1.GetProjectCount() != -1)
            {
                this.lblProjectAllCount.Text = $"検索結果：{projectGrid1.GetProjectCount()}件";
            }
            else
            {
                this.lblProjectAllCount.Text = "検索結果：";
            }

            this.textFilterStr.Text = projectGrid1.GetFilterOptionStr();
            InitializeColumns();
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            using (var searchFrm = new FrmAdvancedSearch(_criteriaDefinition, gridManager.Filters))
            {
                searchFrm.AddDropDownItems(InitializeDropDownInfo().ToList());
                searchFrm.StartPosition = FormStartPosition.CenterParent;
                gridManager.DropDownItems = searchFrm.DropDownItems;

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

        /// <summary>表示パターン変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void OnPatternSelected(object sender, PatternInfo e)
        {
            await UpdateDataTableAsync();
        }

        /// <summary>グリッドの表示内容を更新する</summary>
        /// <returns>非同期タスク</returns>

        private async Task UpdateDataTableAsync()
        {
            // 非同期でデータ取得    
            await gridManager.Reload(new List<FilterCriteria>());
            GdProjectList_RowColChange(this, EventArgs.Empty);
        }

        /// <summary>表示パターンにしたがってグリッドの列を初期化する</summary>
        private void InitializeColumns()
        {
            var pattern = gridPatternUI.SelectedPattern;
            if (pattern == null)
            {
                return;
            }

            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            var configs = pattern.Columns;

            projectGrid1.c1FlexGrid1.Cols.Count = configs.Count + 1;
            projectGrid1.c1FlexGrid1.Cols[0].Caption = string.Empty;
            projectGrid1.c1FlexGrid1.Cols[0].Width = 30;

            var columnIndexOffset = 1;
            for (var i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = projectGrid1.c1FlexGrid1.Cols[i + columnIndexOffset];

                col.Caption = config.Label;
                col.Width = config.Width;
                col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                col.Name = config.VarName;
                col.AllowFiltering = AllowFiltering.None;
                col.AllowSorting = false;
            }

            // 個別設定
            ColumnAction(nameof(ViewReadySale.利率), c => GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Percent2, c));
            ColumnAction(nameof(ViewReadySale.売上掛率), c => GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.SimplePercent, c));
            ColumnAction(nameof(ViewReadySale.仕入掛率), c => GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.SimplePercent, c));
            ColumnAction(nameof(ViewReadySale.納期), c => GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Date, c));
            ColumnAction(nameof(ViewReadySale.入荷日), c => GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Date, c));

            // いくつかの列を強制非表示
            ColumnAction(nameof(ViewReadySale.発注先), c => c.Visible = false);
        }

        /// <summary>Excel印刷ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnExcel出力_Click(object sender, EventArgs e)
        {
            var fName = Path.Combine(HatFAppDataPath.GetLocalDataSavePath("ViewReadySale"), $"{DateTime.Now.ToString(OUTFILE_NAME)}.xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        /// <summary>検索画面にドロップダウンとして表示すべき項目の設定を取得</summary>
        /// <returns>ドロップダウン情報</returns>
        private IEnumerable<SearchDropDownInfo> InitializeDropDownInfo()
        {
            yield return new SearchDropDownInfo()
            {
                FieldName = nameof(ViewReadySalesSearchCondition.発注先),
                DropDownItems = CriteriaHelper.SupplierTypes,
            };
            yield return new SearchDropDownInfo()
            {
                FieldName = nameof(ViewReadySalesSearchCondition.伝票区分),
                DropDownItems = CriteriaHelper.Slips,
            };
        }

        /// <summary>グリッドの列を名前で指定して処理を実行する。列が存在しない場合は何もしない。</summary>
        /// <param name="column">列名</param>
        /// <param name="action">処理</param>
        private void ColumnAction(string column, Action<Column> action)
        {
            if (projectGrid1.c1FlexGrid1.Cols.Contains(column))
            {
                action(projectGrid1.c1FlexGrid1.Cols[column]);
            }
        }
    }
}
