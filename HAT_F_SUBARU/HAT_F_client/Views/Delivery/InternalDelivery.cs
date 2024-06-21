using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Repository;
using HatFClient.ViewModels;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.Delivery
{
    public partial class InternalDelivery : Form
    {
        /// <summary><see cref="ViewInternalDelivery"/>の内容をチェックボックスつきでグリッドに表示させるためのクラス</summary>
        private class CheckableViewInternalDelivery : ViewInternalDelivery
        {
            /// <summary>選択列</summary>
            public bool Checked { get; set; }
        }

        // 納品一覧表（社内用）の一覧を表示するクラス
        private class InternalDeliveryGridManager : GridManagerBase<CheckableViewInternalDelivery> { }

        // TODO: TemplateGridManager から画面用の固有型へ変更 (GridManagerBase<T>の継承で作れます)
        private InternalDeliveryGridManager gridManager = new InternalDeliveryGridManager();

        public event EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        // 検索用の変数
        private DefaultGridPage projectGrid1;
        private static readonly Type TARGET_MODEL = typeof(ViewInternalDelivery);
        private const string OUTFILE_NAME = "納品一覧表（社内用）_yyyyMMdd_HHmmss";

        public InternalDelivery()
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
            gridManager.FetchFuncAsync = async (filter) => {
                // API(ページング条件付与)
                string url = ApiHelper.AddPagingQuery(ApiResources.HatF.Search.InternalDelivery, gridManager.CurrentPage, gridManager.PageSize);
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var apiResponse = await Program.HatFApiClient.PostAsync<List<CheckableViewInternalDelivery>>(
                    url,   // 一覧取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件
                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.Search.InternalDeliveryCount;
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void InternalDelivery_Load(object sender, EventArgs e)
        {
            projectGrid1 = new DefaultGridPage(gridManager);  

            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.Cols.Fixed = 2;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            projectGrid1.c1FlexGrid1.CellChecked += C1FlexGrid1_CellChecked;

            // TODO DefaultGridPageは読み取り専用という設計なのでAllowEditingを直接変更するのは邪道
            // DefaultGridUpdateクラスではページング機能がないのでとりあえずこう対応する
            projectGrid1.c1FlexGrid1.AllowEditing = true;

            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();
            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;

            InitializeEvents();
        }

        /// <summary>セルのチェックボックス変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void C1FlexGrid1_CellChecked(object sender, RowColEventArgs e)
        {
            EnableSaveButton();
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
            gridManager.OnDataSourceChange += GdProjectList_DataSourceChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
        }


        private void GdProjectList_DataSourceChange(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GdProjectList_DataSourceChange");

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
            using (var searchFrm = new FrmAdvancedSearch(CriteriaHelper.CreateCriteriaDefinitions<ViewInternalDelivery>(), gridManager.Filters))
            {
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
                    GdProjectList_DataSourceChange(this, EventArgs.Empty);
                }
            }
        }

        private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GdProjectList_DataSourceChange(this, EventArgs.Empty);
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
            GdProjectList_DataSourceChange(this, EventArgs.Empty);
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

            // 固定列＋チェックボックス列で＋２する
            projectGrid1.c1FlexGrid1.Cols.Count = configs.Count + 2;
            projectGrid1.c1FlexGrid1.Cols[0].Caption = "";
            projectGrid1.c1FlexGrid1.Cols[0].Width = 30;

            // チェックボックス列を追加
            projectGrid1.c1FlexGrid1.Cols[1].Name = nameof(CheckableViewInternalDelivery.Checked);
            projectGrid1.c1FlexGrid1.Cols[1].Caption = string.Empty;
            projectGrid1.c1FlexGrid1.Cols[1].Width = 30;
            projectGrid1.c1FlexGrid1.Cols[1].DataType = typeof(bool);
            projectGrid1.c1FlexGrid1.Cols[1].AllowFiltering = AllowFiltering.None;
            projectGrid1.c1FlexGrid1.Cols[1].AllowEditing = true;

            int columnIndexOffset = 2;
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

            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Percent,
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewInternalDelivery.利率)]
            );
            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewInternalDelivery.販売単価)]
            );
            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewInternalDelivery.販売額)]
            );
            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewInternalDelivery.仕入単価)]
            );
            GridStyleHelper.SetColumnStyle(GridStyleHelper.GridColumnStyleEnum.Currency,
                projectGrid1.c1FlexGrid1.Cols[nameof(ViewInternalDelivery.仕入額)]
            );

#if !DEBUG
            // いくつかの列は強制非表示
            GridHelper.ColumnAction(projectGrid1.c1FlexGrid1, nameof(ViewInternalDelivery.売上番号), c => c.Visible = false);
            GridHelper.ColumnAction(projectGrid1.c1FlexGrid1, nameof(ViewInternalDelivery.売上行番号), c => c.Visible = false);
#endif

            // ★削除予定 全列の幅を自動調整する
            projectGrid1.c1FlexGrid1.AutoSizeCols();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            /* 納品一覧表では詳細画面はなし
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            var detail = FormFactory.GetModelessForm<WH_ReceivingsDetailUpdate_Sample>();
            string denNo = grid.GetData(grid.RowSel, "伝票番号").ToString();
            await detail.LoadDataAndShowAsync(denNo);
            */
        }

        /// <summary>保存ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSaveComment_Click(object sender, EventArgs e)
        {
            var checkedRow = projectGrid1.c1FlexGrid1.Rows.OfType<Row>()
                .Where(r => projectGrid1.c1FlexGrid1.GetCellCheck(r.Index, 1) == CheckEnum.Checked)
                .Select(r => r.DataSource as DataRowView)
                .ToList();
            var parameters = checkedRow
                .Select(r => new InternalDeliveryCheckParameter()
                {
                    SalesNo = r[nameof(CheckableViewInternalDelivery.売上番号)].ToString(),
                    RowNo = (short)r[nameof(CheckableViewInternalDelivery.売上行番号)],
                    Comment = txtComment.Text,
                }).ToList();
            var results = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<List<InternalDeliveryCheckResult>>(ApiResources.HatF.Client.InternalDeliveryCheck, parameters);
            });
            if (results.Failed)
            {
                return;
            }
            // チェック結果を画面に反映
            foreach(var row in checkedRow)
            {
                var salesNo = row[nameof(CheckableViewInternalDelivery.売上番号)].ToString();
                var rowNo = (short)row[nameof(CheckableViewInternalDelivery.売上行番号)];
                var result = results.Value.FirstOrDefault(x => x.SalesNo == salesNo && x.RowNo == rowNo);
                row[nameof(CheckableViewInternalDelivery.確認者)] = result?.Checker;
                row[nameof(CheckableViewInternalDelivery.確認者役職)] = result?.CheckerPost;
                row[nameof(CheckableViewInternalDelivery.コメント)] = result?.Comment;
            }
            projectGrid1.c1FlexGrid1.Invalidate();
        }

        /// <summary>保存ボタンの押下可否を更新</summary>
        private void EnableSaveButton()
        {
            var checkedRowCount = projectGrid1.c1FlexGrid1.Rows.OfType<Row>()
                .Count(r => projectGrid1.c1FlexGrid1.GetCellCheck(r.Index, 1) == CheckEnum.Checked);
            btnSaveComment.Enabled = checkedRowCount > 0 && !string.IsNullOrEmpty(txtComment.Text);
        }

        /// <summary>コメント変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtComment_TextChanged(object sender, EventArgs e)
        {
            EnableSaveButton();
        }
    }
}
