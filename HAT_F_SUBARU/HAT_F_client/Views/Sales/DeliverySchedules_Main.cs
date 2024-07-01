using C1.Win.C1FlexGrid;
using Dma.DatasourceLoader.Models;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.MasterSearch;
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
    /// <summary>得意先別納品予定一覧</summary>
    public partial class DeliverySchedules_Main : Form
    {
        /// <summary>グリッド管理</summary>
        private class ReadySalesGridManager : GridManagerBase<ViewReadySale> { }

        /// <summary>グリッド管理</summary>
        private ReadySalesGridManager gridManager = new ReadySalesGridManager();

        /// <summary>詳細検索画面の選択肢リスト</summary>
        private List<ColumnMappingConfig> _criteriaDefinitions = CriteriaHelper.CreateCriteriaDefinitions<ViewReadySalesSearchCondition>();

        /// <summary>検索結果表示用グリッド</summary>
        private DefaultGrid _grid;

        /// <summary>パターン設定画面で使うオブジェクトの型</summary>
        private static readonly Type TARGET_MODEL = typeof(ViewReadySale);

        /// <summary>出力Excel名テンプレート</summary>
        private const string OUTFILE_NAME = "得意先別納品予定一覧_yyyyMMdd_HHmmss";

        /// <summary>コンストラクタ</summary>
        public DeliverySchedules_Main()
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
            gridManager.SetPageSize(int.MaxValue);

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) =>
            {
                // API(ページング条件付与)
                var url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.ReadySales);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewReadySale>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void DeliverySchedules_Main_Load(object sender, EventArgs e)
        {
            _grid = new DefaultGrid(gridManager, false)
            {
                Dock = DockStyle.Fill,
                Visible = false,
            };
            _grid.c1FlexGrid1.AllowFiltering = false;

            this.panel1.Controls.Add(_grid);

            InitializeColumns();
            _grid.Visible = true;

            gridManager.OnDataSourceChange += GridManager_DataSourceChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
        }

        /// <summary>データソース変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GridManager_DataSourceChange(object sender, EventArgs e)
        {
            // 件数出力
            var rows = gridManager.Dt.Rows.Count;
            if (_grid.GetProjectCount() != -1)
            {
                this.lblProjectAllCount.Text = $"検索結果：{_grid.GetProjectCount()}件";
            }
            else
            {
                this.lblProjectAllCount.Text = "検索結果：";
            }

            this.textFilterStr.Text = _grid.GetFilterOptionStr();
            InitializeColumns();
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await gridManager.Reload(BaseConditionToCriteria().ToList());
        }

        /// <summary>詳細検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            using (var searchFrm = new FrmAdvancedSearch(_criteriaDefinitions, gridManager.Filters))
            {
                searchFrm.AddDropDownItems(InitializeDropDownInfo().ToList());
                searchFrm.StartPosition = FormStartPosition.CenterParent;
                gridManager.DropDownItems = searchFrm.DropDownItems;

                searchFrm.OnSearch += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    gridManager.SetCurrentPage(1);
                    await gridManager.Reload(MixFilterCriteria(searchFrm.FilterCriterias));
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    gridManager.SetCurrentPage(1);
                    gridManager.SetFilters(MixFilterCriteria(searchFrm.FilterCriterias));
                    await gridManager.Reload(gridManager.Filters);
                };

                searchFrm.OnReset += (sender, e) =>
                {
                    gridManager.SetFilters(new List<FilterCriteria>());
                };

                if (DialogHelper.IsPositiveResult(searchFrm.ShowDialog()))
                {
                    GridManager_DataSourceChange(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>基本検索条件と詳細検索条件を合成する</summary>
        /// <param name="manager">グリッド管理オブジェクト</param>
        /// <returns>合成結果</returns>
        private List<FilterCriteria> MixFilterCriteria(List<FilterCriteria> filters)
        {
            return filters
                .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewReadySale.得意先コード)))
                .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewReadySale.納期)))
                .Concat(BaseConditionToCriteria()).ToList();
        }

        /// <summary>基本検索条件を<see cref="FilterCriteria"/>オブジェクトに変換する</summary>
        /// <returns><see cref="FilterCriteria"/></returns>
        private IEnumerable<FilterCriteria> BaseConditionToCriteria()
        {
            var index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewReadySale.得意先コード));
            yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Contains, txtCompCode.Text.Trim(), false);

            if (dtNoubiFrom.HasValue && dtNoubiTo.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewReadySale.納期));
                yield return new FilterCriteria(_criteriaDefinitions, index, (DateTime)dtNoubiFrom.Value, (DateTime)dtNoubiTo.Value, false);
            }
            else if (dtNoubiFrom.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewReadySale.納期));
                yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Gte, (DateTime)dtNoubiFrom.Value, false);
            }
            else if (dtNoubiTo.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewReadySale.納期));
                yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Lte, (DateTime)dtNoubiTo.Value, false);
            }
        }

        /// <summary>必須検索条件のチェックを行う</summary>
        /// <param name="parentForm">メッセージを表示する親画面</param>
        /// <returns>成否</returns>
        private bool ValidateRequireCondition(Form parentForm)
        {
            if (string.IsNullOrEmpty(txtCompCode.Text.Trim()))
            {
                DialogHelper.WarningMessage(parentForm, "得意先コードは必須項目です。");
                return false;
            }
            return true;
        }

        /// <summary>表示パターン変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void OnPatternSelected(object sender, PatternInfo e)
        {
            InitializeColumns();
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
            _grid.c1FlexGrid1.Clear();
            var configs = pattern.Columns;

            _grid.c1FlexGrid1.Cols.Count = configs.Count + 1;
            _grid.c1FlexGrid1.Cols[0].Caption = string.Empty;
            _grid.c1FlexGrid1.Cols[0].Width = 30;

            var columnIndexOffset = 1;
            for (var i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = _grid.c1FlexGrid1.Cols[i + columnIndexOffset];

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
            _grid.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

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
            if (_grid.c1FlexGrid1.Cols.Contains(column))
            {
                action(_grid.c1FlexGrid1.Cols[column]);
            }
        }

        /// <summary>得意先コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtCompCode_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = !string.IsNullOrEmpty(txtCompCode.Text);
        }

        /// <summary>得意先検索</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSearchCustomer_Click(object sender, EventArgs e)
        {
            using (var form = new MS_Tokui2())
            {
                form.CustCode = txtCompCode.Text;
                if (DialogHelper.IsPositiveResult(form.ShowDialog(this)))
                {
                    txtCompCode.Text = form.CustCode;
                }
            }
        }
    }
}
