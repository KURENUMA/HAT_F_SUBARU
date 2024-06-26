using C1.Win.C1FlexGrid;
using Dma.DatasourceLoader.Models;
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
using HatFClient.Views.MasterSearch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace HatFClient.Views.Purchase
{
    /// <summary>仕入請求一覧画面</summary>
    public partial class PU_BillingList : Form
    {
        private class PurchaseBillingGridManager : GridManagerBase<ViewPurchaseBilling>
        { }

        private readonly PurchaseBillingGridManager gridManager = new PurchaseBillingGridManager();

        /// <summary>データテーブルレイアウト</summary>
        private GridOrderManager gridOrderManager;

        /// <summary>詳細検索画面に表示される検索条件項目</summary>
        private readonly List<ColumnMappingConfig> _criteriaDefinitions = CriteriaHelper.CreateCriteriaDefinitions<ViewPurchaseBilling>();

        // 検索用の変数
        private DefaultGrid projectGrid1;

        /// <summary>パターン一覧画面に表示するモデルの型</summary>
        private static readonly Type TARGET_MODEL = typeof(ViewPurchaseBilling);

        /// <summary>出力Excelファイル名テンプレート</summary>
        private const string OUTFILE_NAME = "仕入請求一覧_yyyyMMdd_HHmmss";

        /// <summary>アクセラレータキー</summary>
        private Dictionary<Keys, (Button Button, Func<KeyEventArgs, bool> CanExecute)> _accelarators;

        /// <summary>コンストラクタ</summary>
        public PU_BillingList()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                InitializeFetching();

                FormStyleHelper.SetResizableDialogStyle(this, true);

                // INIT PATTERN
                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                this.gridPatternUI.Init(patternRepo);

                cmbPayMonth.InitializeItems();
            }
        }

        #region メイン画面制御

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void PU_BillingList_Load(object sender, EventArgs e)
        {
            // 一部の項目は詳細検索画面に表示しない
            //CriteriaHelper.ChangeVisible(_criteriaDefinitions, nameof(ViewPurchaseBilling.仕入先コード), false);
            //CriteriaHelper.ChangeVisible(_criteriaDefinitions, nameof(ViewPurchaseBilling.仕入支払年月日), false);
            //CriteriaHelper.ChangeVisible(_criteriaDefinitions, nameof(ViewPurchaseBilling.Hat注文番号), false);

            // グリッドの設定
            gridOrderManager = new GridOrderManager(_criteriaDefinitions);
            projectGrid1 = new DefaultGrid(gridManager, gridOrderManager, false);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += GrdList_MouseDoubleClick;
            projectGrid1.c1FlexGrid1.AfterFilter += grdTotalPric_AfterFilter;
            this.panel1.Controls.Add(projectGrid1);

            // グリッドの列設定
            InitializeColumns();
            projectGrid1.Visible = true;

            gridManager.OnDataSourceChange += GridManager_DataSourceChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;

            // ショートカットキーの設定
            _accelarators = new Dictionary<Keys, (Button Button, Func<KeyEventArgs, bool> CanExecute)>()
            {
                {Keys.F9, (btnSearchSupplier, (_) => ActiveControl == txtSupCode && btnSearchSupplier.Enabled)}
            };

            cmbPayMonth.Value = DateTime.Today;
        }

        /// <summary>キーダウン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void PU_BillingList_KeyDown(object sender, KeyEventArgs e)
        {
            if (_accelarators.ContainsKey(e.KeyCode) && _accelarators[e.KeyCode].CanExecute(e))
            {
                _accelarators[e.KeyCode].Button.PerformClick();
            }
        }

        /// <summary>画面終了中</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void PU_BillingList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormFactory.GetModelessFormCache<PU_AmountCheck>()?.Close();
        }

        #endregion メイン画面制御

        #region グリッド制御

        /// <summary>
        /// データ取得処理の初期化
        /// </summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) =>
            {
                // API(ページング条件付与)
                var url = ApiHelper.AddPagingQuery(ApiResources.HatF.Search.ViewPurchaseBilling, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewPurchaseBilling>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };
            gridManager.FetchCountFuncAsync = null;
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
            GridManager_DataSourceChange(this, EventArgs.Empty);
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

            //// 仕入締日
            //ListDictionary closeDateItems = new ListDictionary();
            //JsonResources.CloseDates.ForEach(item => closeDateItems.Add(item.Code, item.Name));
            ////JsonResources.CloseDates.ForEach(item => closeDateItems.Add(item.Code != null ? item.Code : "", item.Name));
            //grid.Cols[nameof(ViewPurchaseBilling.仕入先締日)].DataMap = closeDateItems;

            //// 仕入支払月
            //ListDictionary payMonthsItems = new ListDictionary();
            //JsonResources.PayMonths.ForEach(item => payMonthsItems.Add(item.Code, item.Name));
            //grid.Cols[nameof(ViewPurchaseBilling.仕入先支払月)].DataMap = payMonthsItems;

            //// 仕入先支払日
            //ListDictionary payDatesItems = new ListDictionary();
            //JsonResources.PayDates.ForEach(item => payDatesItems.Add(item.Code, item.Name));
            //grid.Cols[nameof(ViewPurchaseBilling.仕入先支払日)].DataMap = payDatesItems;

            //// 仕入先支払方法区分
            //ListDictionary payMethodTypeItems = new ListDictionary();
            //JsonResources.PayMethodTypes.ForEach(item => payMethodTypeItems.Add(item.Code, item.Name));
            //grid.Cols[nameof(ViewPurchaseBilling.仕入先支払方法区分)].DataMap = payMethodTypeItems;

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
            //GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
            //    grid.Cols[nameof(ViewPurchaseBilling.発注金額合計)]
            //);
        }

        /// <summary>RowやColのプロパティが変更された（選択セルが変更された）</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GridManager_DataSourceChange(object sender, EventArgs e)
        {
            // 件数出力
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

        #endregion グリッド制御

        #region 検索

        /// <summary>仕入先コードのテキスト変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void txtSupCode_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = !string.IsNullOrEmpty(txtSupCode.Text.Trim());
        }

        /// <summary>基本検索条件を<see cref="FilterCriteria"/>オブジェクトに変換する</summary>
        /// <returns><see cref="FilterCriteria"/></returns>
        private IEnumerable<FilterCriteria> BaseConditionToCriteria()
        {
            // 仕入先コード
            var indexF = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.F仕入先コード));
            var criteriaF = new FilterCriteria(_criteriaDefinitions, indexF, FilterOperators.Eq, txtSupCode.Text.Trim(), false);
            criteriaF.GroupKey = "仕入先コード";
            yield return criteriaF;
            var indexM = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.M仕入先コード));
            var criteriaM = new FilterCriteria(_criteriaDefinitions, indexM, FilterOperators.Eq, txtSupCode.Text.Trim(), false);
            criteriaM.CombinationType = Dma.DatasourceLoader.Creator.FilterCombinationTypes.OR;
            criteriaM.GroupKey = "仕入先コード";
            yield return criteriaM;

            // 納日From
            if (!string.IsNullOrEmpty(dtNoukiFrom.Text.Trim()))
            {
                indexF = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.F納日));
                criteriaF = new FilterCriteria(_criteriaDefinitions, indexF, FilterOperators.Gte, (DateTime)dtNoukiFrom.Value, false);
                criteriaF.GroupKey = "納日From";
                yield return criteriaF;
                indexM = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.M納入日));
                criteriaM = new FilterCriteria(_criteriaDefinitions, indexM, FilterOperators.Gte, (DateTime)dtNoukiFrom.Value, false);
                criteriaM.CombinationType = Dma.DatasourceLoader.Creator.FilterCombinationTypes.OR;
                criteriaM.GroupKey = "納日From";
                yield return criteriaM;
            }

            // 納日To
            if (!string.IsNullOrEmpty(dtNoukiTo.Text.Trim()))
            {
                var condition = ((DateTime)dtNoukiTo.Value).AddDays(1);
                indexF = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.F納日));
                criteriaF = new FilterCriteria(_criteriaDefinitions, indexF, FilterOperators.Lt, condition, false);
                criteriaF.GroupKey = "納日To";
                yield return criteriaF;
                indexM = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.M納入日));
                criteriaM = new FilterCriteria(_criteriaDefinitions, indexM, FilterOperators.Lt, condition, false);
                criteriaM.CombinationType = Dma.DatasourceLoader.Creator.FilterCombinationTypes.OR;
                criteriaM.GroupKey = "納日To";
                yield return criteriaM;
            }

            // 注文番号
            if (!string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
            {
                indexF = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.F注文番号));
                criteriaF = new FilterCriteria(_criteriaDefinitions, indexF, FilterOperators.Eq, txtOrderNo.Text.Trim(), false);
                criteriaF.GroupKey = "注文番号";
                yield return criteriaF;
                indexM = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.M注文番号));
                criteriaM = new FilterCriteria(_criteriaDefinitions, indexM, FilterOperators.Eq, txtOrderNo.Text.Trim(), false);
                criteriaM.CombinationType = Dma.DatasourceLoader.Creator.FilterCombinationTypes.OR;
                criteriaM.GroupKey = "注文番号";
                yield return criteriaM;
            }

            // 注番
            if (!string.IsNullOrEmpty(txtChuban.Text.Trim()))
            { 
                indexF = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.F注番));
                criteriaF = new FilterCriteria(_criteriaDefinitions, indexF, FilterOperators.Eq, txtChuban.Text.Trim(), false);
                criteriaF.GroupKey = "注番";
                yield return criteriaF;
                indexM = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.M注番));
                criteriaM = new FilterCriteria(_criteriaDefinitions, indexM, FilterOperators.Eq, txtChuban.Text.Trim(), false);
                criteriaM.CombinationType = Dma.DatasourceLoader.Creator.FilterCombinationTypes.OR;
                criteriaM.GroupKey = "注番";
                yield return criteriaM;
            }


            //    if (cmbPayMonth.FirstOfMonthValue.HasValue)
            //    {
            //        index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.仕入支払年月日));
            //        yield return new FilterCriteria(_criteriaDefinitions, index, cmbPayMonth.FirstOfMonthValue.Value, cmbPayMonth.EndOfMonthValue.Value, false);
            //    }
            //    if (!string.IsNullOrEmpty(txtChuban.Text.Trim()))
            //    {
            //        index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewPurchaseBilling.Hat注文番号));
            //        yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Contains, txtChuban.Text.Trim(), false);
            //    }
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            // 必須項目が入力されないとボタンが押せないためここではバリデーションしない

            gridManager.SetCurrentPage(1);
            await gridManager.Reload(BaseConditionToCriteria().ToList());
            CreateTotalPriceGrid();
        }

        #endregion 検索

        #region 詳細検索

        /// <summary>詳細検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnDetailSearch_Click(object sender, EventArgs e)
        {
            using (var searchFrm = new FrmAdvancedSearch(_criteriaDefinitions, gridManager.Filters))
            {
                // 検索条件のうちドロップダウン項目にするもの
                // TODO searchFrm.DropDownItems.AddRange(MakeDropDownInfo().ToList());
                gridManager.DropDownItems = searchFrm.DropDownItems;

                searchFrm.StartPosition = FormStartPosition.CenterParent;
                searchFrm.OnSearch += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    gridManager.SetCurrentPage(1);
                    // TODO await gridManager.Reload(MixFilterCriteria(searchFrm.FilterCriterias));
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    gridManager.SetCurrentPage(1);
                    // TODO gridManager.SetFilters(MixFilterCriteria(searchFrm.FilterCriterias));
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

        /// <summary>詳細検索画面での選択肢としてドロップダウン項目にするものを用意する</summary>
        /// <returns>ドロップダウン選択肢情報</returns>
        //private IEnumerable<SearchDropDownInfo> MakeDropDownInfo()
        //{
        //    yield return new SearchDropDownInfo
        //    {
        //        FieldName = nameof(ViewPurchaseBilling.仕入先締日),
        //        DropDownItems = JsonResources.CloseDates.ToDictionary(x => x.Name, x => x.Code.ToString())
        //    };
        //    yield return new SearchDropDownInfo
        //    {
        //        FieldName = nameof(ViewPurchaseBilling.仕入先支払月),
        //        DropDownItems = JsonResources.PayMonths.ToDictionary(x => x.Name, x => x.Code.ToString())
        //    };
        //    yield return new SearchDropDownInfo
        //    {
        //        FieldName = nameof(ViewPurchaseBilling.仕入先支払日),
        //        DropDownItems = JsonResources.PayDates.ToDictionary(x => x.Name, x => x.Code.ToString())
        //    };
        //    yield return new SearchDropDownInfo
        //    {
        //        FieldName = nameof(ViewPurchaseBilling.仕入先支払方法区分),
        //        DropDownItems = JsonResources.PayMethodTypes.ToDictionary(x => x.Name, x => x.Code.ToString())
        //    };
        //}

        /// <summary>基本検索条件と詳細検索条件を合成する</summary>
        /// <param name="manager">グリッド管理オブジェクト</param>
        /// <returns>合成結果</returns>
        //private List<FilterCriteria> MixFilterCriteria(List<FilterCriteria> filters)
        //{
        //    return filters
        //        .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewPurchaseBilling.仕入先コード)))
        //        .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewPurchaseBilling.仕入支払年月日)))
        //        .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewPurchaseBilling.Hat注文番号)))
        //        .Concat(BaseConditionToCriteria()).ToList();
        //}

        /// <summary>必須検索条件のチェックを行う</summary>
        /// <param name="parentForm">メッセージを表示する親画面</param>
        /// <returns>成否</returns>
        private bool ValidateRequireCondition(Form parentForm)
        {
            if (string.IsNullOrEmpty(txtSupCode.Text.Trim()))
            {
                DialogHelper.WarningMessage(parentForm, "仕入先コードは必須項目です。");
                return false;
            }
            return true;
        }

        #endregion 詳細検索

        #region 詳細表示

        private void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            PU_AmountCheck detail = FormFactory.GetModelessForm<PU_AmountCheck>();
            //detail.Condition.仕入先コード = grid.GetData(grid.RowSel, nameof(ViewPurchaseBilling.仕入先コード)).ToString();
            detail.Condition.Hat注文番号 = grid.GetData(grid.RowSel, "Hat注文番号").ToString();
            detail.Show();
            //detail.Init(xxxxxx);
            detail.Activate();
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled == true)
            {
                btnDetail.PerformClick();
            }
        }

        #endregion 詳細表示

        #region Excel出力

        /// <summary>Excel出力ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        #endregion Excel出力

        #region 請求データ取込

        /// <summary>請求データ取込ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnDataInput_Click(object sender, EventArgs e)
        {
            //TODO取り込み画面遷移
        }

        #endregion 請求データ取込

        #region 担当者へ連絡

        /// <summary>担当者へ連絡ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnContactEmail_Click(object sender, EventArgs e)
        {
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog();
            }
        }

        #endregion 担当者へ連絡

        #region 仕入金額照合

        /// <summary>仕入金額照合ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnPuAmountCollation_Click(object sender, EventArgs e)
        {
            PU_AmountCheck detail = FormFactory.GetModelessForm<PU_AmountCheck>();
            detail.Show();
            //detail.Init(xxxxxx);
            detail.Activate();
        }

        #endregion 仕入金額照合

        #region 仕入先検索

        /// <summary>仕入先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            using (var searchForm = new MS_Shiresaki())
            {
                searchForm.TxtTEAM_CD = LoginRepo.GetInstance().CurrentUser.TeamCode;
                searchForm.TxtSHIRESAKI_CD = txtSupCode.Text;
                if (DialogHelper.IsPositiveResult(searchForm.ShowDialog()))
                {
                    txtSupCode.Text = searchForm.StrMsShiresakiCode;
                }
            }
        }

        #endregion 仕入先検索

        #region 合計表

        private void CreateTotalPriceGrid()
        {
            //var dt = (gridManager.Dt)?.AsEnumerable().Where(row => gridManager.Rows.Contains(row)).CopyToDataTable();
            //var visibleIndex = projectGrid1.c1FlexGrid1.Rows.OfType<Row>()
            //    .Where(x => x.Visible)
            //    .Select(d => d.Index)
            //    //.Select(x => x.DataSource as ViewPurchaseBilling)
            //    //.Where(x => x != null)
            //    .ToList();
            //var dt = visibleIndex.Select(i => gridManager.dataSource[i - 1]).ToList();
            var dt = gridManager.dataSource;
            if (dt == null) return;

            var taxRates = GetDistinctTaxRates(dt);

            var fTotal = dt.Sum(d => d.F金額 ?? 0);
            var mTotal = dt.Sum(d => d.M金額 ?? 0);

            var taxCalculations = new List<Tuple<int, decimal, decimal>>();

            foreach (var rate in taxRates)
            {
                var fTaxTotal = CalculateTaxTotal(dt, d => d.F金額, d => d.F消費税率, rate);
                var mTaxTotal = CalculateTaxTotal(dt, d => d.M金額, d => d.M消費税率, rate);
                taxCalculations.Add(Tuple.Create(rate, fTaxTotal, mTaxTotal));
            }

            var fPayment = fTotal + taxCalculations.Sum(tc => tc.Item2);
            var mPayment = mTotal + taxCalculations.Sum(tc => tc.Item3);
            var paymentDifference = fPayment - mPayment;

            // Create DataTable for result grid
            var resultTable = new DataTable();
            resultTable.Columns.Add("行ラベル");
            resultTable.Columns.Add("F", typeof(double));
            resultTable.Columns.Add("M", typeof(double));
            resultTable.Columns.Add("差分", typeof(double));

            resultTable.Rows.Add("金額合計", fTotal, mTotal, fTotal - mTotal);

            foreach (var calculation in taxCalculations)
            {
                resultTable.Rows.Add(
                    $"{calculation.Item1}%消費税額合計",
                    calculation.Item2,
                    calculation.Item3,
                    calculation.Item2 - calculation.Item3);
            }

            resultTable.Rows.Add("支払", fPayment, mPayment, paymentDifference);

            // Set the result grid's DataSource to the DataTable
            grdTotalPrice.DataSource = resultTable;
        }

        private HashSet<int> GetDistinctTaxRates(List<ViewPurchaseBilling> dataList)
        {
            var taxRates = new HashSet<int>();
            foreach (var data in dataList)
            {
                if (data.F消費税率.HasValue) taxRates.Add(data.F消費税率.Value);
                if (data.M消費税率.HasValue) taxRates.Add(data.M消費税率.Value);
            }
            return taxRates;
        }

        private decimal CalculateTaxTotal(List<ViewPurchaseBilling> dataList, Func<ViewPurchaseBilling, decimal?> amountSelector, Func<ViewPurchaseBilling, int?> taxRateSelector, int taxRate)
        {
            return dataList
                .Where(d => taxRateSelector(d) == taxRate && amountSelector(d).HasValue)
                .Sum(d => amountSelector(d).Value * taxRate / 100.0m);
        }

        private void grdTotalPric_AfterFilter(object sender, EventArgs e)
        {
            CreateTotalPriceGrid();
        }

        //private void LayoutGrids()
        //{
        //    sourceGrid.Dock = DockStyle.Top;
        //    resultGrid.Dock = DockStyle.Bottom;
        //    resultGrid.Height = 150;

        //    this.Controls.Add(sourceGrid);
        //    this.Controls.Add(resultGrid);
        //}

        #endregion 合計表

    }
}