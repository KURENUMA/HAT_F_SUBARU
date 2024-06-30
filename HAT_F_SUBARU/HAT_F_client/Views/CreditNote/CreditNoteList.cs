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
using HatFClient.Views.MasterSearch;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.SalesCorrection
{
    /// <summary>赤黒登録一覧画面</summary>
    public partial class CreditNoteList : Form
    {
        private class CreditNoteListManager : GridManagerBase<ViewSalesAdjustment> { }

        private CreditNoteListManager _gridManager = new CreditNoteListManager();

        /// <summary>検索結果表示用グリッド</summary>
        private DefaultGrid _grid;

        /// <summary>詳細検索画面の選択肢リスト</summary>
        private List<ColumnMappingConfig> _criteriaDefinitions = CriteriaHelper.CreateCriteriaDefinitions<ViewSalesAdjustment>(
            x => x.売上調整番号,
            x => x.承認要求番号,
            x => x.得意先コード,
            x => x.得意先名,
            x => x.請求年月);

        /// <summary>パターン編集画面に表示される列の型情報</summary>
        private static readonly Type TARGET_MODEL = typeof(ViewSalesAdjustment);

        /// <summary>Excelファイル名テンプレート</summary>
        private const string OUTFILE_NAME = "赤黒登録一覧_yyyyMMdd_HHmmss";

        /// <summary>コンストラクタ</summary>
        public CreditNoteList()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);

                InitializeFetching();

                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                patternRepo.ExceptColumns.AddRange(new[]
                {
                    nameof(ViewSalesAdjustment.売上調整番号),
                    nameof(ViewSalesAdjustment.承認要求番号),
                });
                this.gridPatternUI.Init(patternRepo);
            }
        }

        /// <summary>データ取得処理の初期化</summary>
        private void InitializeFetching()
        {
            _gridManager.TargetForm = this;
            _gridManager.SetPageSize(int.MaxValue);

            // 一覧取得処理
            _gridManager.FetchFuncAsync = async (filter) =>
            {
                // API(ページング条件付与)
                string url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.SalesAdjustment);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewSalesAdjustment>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void CreditNoteList_Load(object sender, EventArgs e)
        {
            _grid = new DefaultGrid(_gridManager, false)
            {
                Dock = DockStyle.Fill,
                Visible = false,
            };
            _grid.c1FlexGrid1.AllowFiltering = true;
            _grid.c1FlexGrid1.SelectionMode = SelectionModeEnum.Default;
            _grid.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            this.panel1.Controls.Add(_grid);

            _gridManager.OnDataSourceChange += Grid_DataSourceChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;

            InitializeColumns();
            _grid.Visible = true;
            btnAdvancedSearch.Enabled = true;

        }

        /// <summary>画面終了</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void CreditNoteList_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormFactory.GetModelessFormCache<CreditNote.CreditNote>()?.Close();
        }

        /// <summary>グリッド上でのダブルクリック/summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDetail.PerformClick();
        }

        /// <summary>データソースの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void Grid_DataSourceChange(object sender, EventArgs e)
        {
            // 件数出力
            this.lblProjectAllCount.Text = _grid.GetProjectCount() != 1 ? $"検索結果：{_grid.GetProjectCount()}件" : "検索結果：";
            this.textFilterStr.Text = _grid.GetFilterOptionStr();
            this.btnDetail.Enabled = _grid.GetProjectCount() > 0;
            InitializeColumns();
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await _gridManager.Reload(BaseConditionToCriteria().ToList());
        }

        /// <summary>詳細検索ボタン</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            using (var searchFrm = new FrmAdvancedSearch(_criteriaDefinitions))
            {
                searchFrm.StartPosition = FormStartPosition.CenterParent;
                searchFrm.AddDropDownItems(MakeDropDownInfo().ToList());

                searchFrm.OnSearch += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    await _gridManager.Reload(MixFilterCriteria(searchFrm.FilterCriterias));
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    if (!ValidateRequireCondition(searchFrm))
                    {
                        e.Cancel = true;
                        return;
                    }
                    _gridManager.SetFilters(searchFrm.FilterCriterias);
                    await _gridManager.Reload(MixFilterCriteria(searchFrm.FilterCriterias));
                };

                searchFrm.OnReset += (sender, e) =>
                {
                    _gridManager.SetFilters(new List<FilterCriteria>());
                };

                if (DialogHelper.IsPositiveResult(searchFrm.ShowDialog()))
                {
                    Grid_DataSourceChange(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>基本検索条件を<see cref="FilterCriteria"/>オブジェクトに変換する</summary>
        /// <returns><see cref="FilterCriteria"/></returns>
        private IEnumerable<FilterCriteria> BaseConditionToCriteria()
        {
            var index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewSalesAdjustment.得意先コード));
            yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Contains, txtCustCode.Text.Trim(), false);

            if (ymInvoicedFrom.Value.HasValue && ymInvoicedTo.Value.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewSalesAdjustment.請求年月));
                yield return new FilterCriteria(_criteriaDefinitions, index, ymInvoicedFrom.Value.Value, ymInvoicedTo.Value.Value, false);
            }
            else if (ymInvoicedFrom.Value.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewSalesAdjustment.請求年月));
                yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Gte, ymInvoicedFrom.Value.Value, false);
            }
            else if (ymInvoicedTo.Value.HasValue)
            {
                index = _criteriaDefinitions.FindIndex(x => x.FieldName == nameof(ViewSalesAdjustment.請求年月));
                yield return new FilterCriteria(_criteriaDefinitions, index, FilterOperators.Lte, ymInvoicedTo.Value.Value, false);
            }
        }

        /// <summary>基本検索条件と詳細検索条件を合成する</summary>
        /// <param name="manager">グリッド管理オブジェクト</param>
        /// <returns>合成結果</returns>
        private List<FilterCriteria> MixFilterCriteria(List<FilterCriteria> filters)
        {
            return filters
                .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewSalesAdjustment.得意先コード)))
                .Except(filters.Where(x => x.SelectedColumn.FieldName == nameof(ViewSalesAdjustment.請求年月)))
                .Concat(BaseConditionToCriteria()).ToList();
        }

        /// <summary>必須検索条件のチェックを行う</summary>
        /// <param name="parentForm">メッセージを表示する親画面</param>
        /// <returns>成否</returns>
        private bool ValidateRequireCondition(Form parentForm)
        {
            if (string.IsNullOrEmpty(txtCustCode.Text.Trim()))
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

        private async void updateDataTable()
        {
            //gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            // 非同期でデータ取得    
            await _gridManager.Reload(new List<FilterCriteria>());
            Grid_DataSourceChange(this, EventArgs.Empty);
        }

        /// <summary>グリッド列初期化</summary>
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

            int columnIndexOffset = 1;
            for (int i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = _grid.c1FlexGrid1.Cols[i + columnIndexOffset];

                col.Caption = config.Label;
                col.Width = config.Width;
                col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                col.Name = config.VarName;
                col.AllowFiltering = AllowFiltering.None;
                col.DataMap = null;
                col.Format = string.Empty;
            }

            GridHelper.ColumnAction(_grid.c1FlexGrid1, nameof(ViewSalesAdjustment.請求年月), c =>
            {
                c.Format = "yy/MM";
            });
            GridHelper.ColumnAction(_grid.c1FlexGrid1, nameof(ViewSalesAdjustment.区分), c =>
            {
                c.DataMap = JsonResources.SalesAdjustmentCategories.ToDictionary(x => x.Key, x => $"{x.Key}:{x.Value}");
            });
            GridHelper.ColumnAction(_grid.c1FlexGrid1, nameof(ViewSalesAdjustment.金額), c =>
            {
                GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency, c);
            });
            GridHelper.ColumnAction(_grid.c1FlexGrid1, nameof(ViewSalesAdjustment.消費税), c =>
            {
                c.TextAlign = TextAlignEnum.LeftCenter;
            });
            GridHelper.ColumnAction(_grid.c1FlexGrid1, nameof(ViewSalesAdjustment.承認状態), c =>
            {
                var map = Enum.GetValues(typeof(ApprovalStatus)).OfType<ApprovalStatus>()
                    .ToDictionary(x => (int)x, x => EnumUtil.GetDescription(x));
                c.DataMap = map;
            });
        }

        /// <summary>Excel出力ボタン</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExcelPrint_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            _grid.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        /// <summary>新規赤黒登録ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnNew_Click(object sender, EventArgs e)
        {
            // 新規登録なので、既に開いている画面が編集モードだったら一度閉じる
            var existForm = FormFactory.GetModelessFormCache<CreditNote.CreditNote>();
            if (existForm?.IsEditMode == true)
            {
                existForm.Close();
            }

            var form = FormFactory.GetModelessForm<CreditNote.CreditNote>(x =>
            {
                x.IsEditMode = false;
            });
            form.Show();
            form.Activate();
        }

        /// <summary>赤黒詳細ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnDetail_Click(object sender, EventArgs e)
        {
            // 編集なので、既に開いている画面が新規モードだったら一度閉じる
            var existForm = FormFactory.GetModelessFormCache<CreditNote.CreditNote>();
            if (existForm?.IsEditMode == false)
            {
                existForm.Close();
            }

            var dataSource = _grid.c1FlexGrid1.Rows[_grid.c1FlexGrid1.RowSel].DataSource as DataRowView;
            var form = FormFactory.GetModelessForm<CreditNote.CreditNote>(x =>
            {
                x.IsEditMode = true;
                x.TokuiCd = dataSource[nameof(ViewSalesAdjustment.得意先コード)]?.ToString();
                x.TokuiName = dataSource[nameof(ViewSalesAdjustment.得意先名)]?.ToString();
                x.InvoicedYearMonth = (DateTime)dataSource[nameof(ViewSalesAdjustment.請求年月)];
                x.ApprovalId = dataSource[nameof(ViewSalesAdjustment.承認要求番号)]?.ToString();
            });
            form.Show();
            form.Activate();
        }

        /// <summary>詳細検索画面での選択肢としてドロップダウン項目にするものを用意する</summary>
        /// <returns>ドロップダウン選択肢情報</returns>
        private IEnumerable<SearchDropDownInfo> MakeDropDownInfo()
        {
            yield return new SearchDropDownInfo
            {
                FieldName = nameof(ViewSalesAdjustment.区分),
                DropDownItems = JsonResources.SalesAdjustmentCategories.ToDictionary(x => $"{x.Key}:{x.Value}", x => x.Key.ToString()),
            };
        }

        /// <summary>得意先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSearchCustomers_Click(object sender, EventArgs e)
        {
            using (var form = new MS_Tokui2())
            {
                form.CustCode = txtCustCode.Text.Trim();
                if (DialogHelper.IsPositiveResult(form.ShowDialog(this)))
                {
                    txtCustCode.Text = form.CustCode;
                }
            }
        }

        /// <summary>得意先コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtCustCode_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = !string.IsNullOrEmpty(txtCustCode.Text.Trim());
        }
    }
}
