using AutoMapper;
using C1.Win.C1FlexGrid;
using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Properties;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Sales
{
    /// <summary>売上確定後　利率異常チェック</summary>
    public partial class InterestRate_Fixed : Form
    {
        /// <summary>検索条件</summary>
        private List<FilterCriteria> _filters = new();

        /// <summary>検索画面のドロップダウンを定義</summary>
        private readonly List<ColumnMappingConfig> _criteriaDefinition = CriteriaHelper.CreateCriteriaDefinitions<ViewInterestRateFixedSearchCondition>();

        /// <summary>直前で選択されていた売上グリッドの行</summary>
        private int _prevSaleRow = -1;

        /// <summary>売上一覧グリッドにバインドされるデータソース</summary>
        private SortableBindingList<ViewInterestRateFixed> _salesSource;

        /// <summary>明細グリッドにバインドされるデータソース</summary>
        private SortableBindingList<CheckableInterestRateFixed> _detailSource;

        /// <summary>
        /// <para><see cref="ViewInterestRateFixed"/>から<see cref="CheckableInterestRateFixed"/>へ変換するためのオブジェクト</para>
        /// <para>明細グリッドに表示する情報を生成するのに使用する</para>
        /// </summary>
        private readonly Mapper _detailMapper;

        /// <summary>コンストラクタ</summary>
        public InterestRate_Fixed()
        {
            InitializeComponent();
            this.grdList.AllowFiltering = true;

            if (!this.DesignMode)
            {
                // 共通スタイル設定
                FormStyleHelper.SetWorkWindowStyle(this);

                _detailMapper = new(new MapperConfiguration(cfg => cfg.CreateMap<ViewInterestRateFixed, CheckableInterestRateFixed>()));
            }
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void InterestRate_Fixed_Load(object sender, EventArgs e)
        {
            UpdateEnabled();

            // 個人設定による検索条件を表示
            txtRateOver.Text = Settings.Default.interestrate_rate_over;
            txtRateUnder.Text = Settings.Default.interestrate_rate_under;
            txtSuryoOver.Text = HatFComParts.DoFormatN0(Settings.Default.interestrate_suryo_over);
            txtUriKinOver.Text = HatFComParts.DoFormatN0(Settings.Default.interestrate_uri_kin_over);
            chkUriTanZero.Checked = Settings.Default.interestrate_uri_tan_zero;

            // 検索実行
            await SyncDataAsync(_filters);
        }

        /// <summary>検索して結果を一覧に同期</summary>
        /// <param name="criterias">検索条件</param>
        /// <returns>非同期タスク</returns>
        private async Task SyncDataAsync(List<FilterCriteria> criterias)
        {
            grdList.Visible = false;
            grdDetail.Visible = false;
            _prevSaleRow = -1;

            // 利率
            var profitOver = HatFComParts.DoParseDecimal(txtRateOver.Text);
            profitOver = profitOver.HasValue ? profitOver.Value / 100 : null;
            var profitUnder = HatFComParts.DoParseDecimal(txtRateUnder.Text);
            profitUnder = profitUnder.HasValue ? profitUnder.Value / 100 : null;
            // 数量
            var suryoOver = HatFComParts.DoParseInt(txtSuryoOver.Text);
            // 金額
            var uriKinOver = HatFComParts.DoParseDecimal(txtUriKinOver.Text);
            // ゼロ円
            var uriTanZero = chkUriTanZero.Checked;

            // 検索
            var result = await ApiHelper.FetchAsync(this, () =>
            {
                return SearchRepo.GetInstance().SearchInterestRateFixedAsync(
                    profitOver, profitUnder,
                    suryoOver, null,
                    uriKinOver, null,
                    uriTanZero,
                    criterias);
            });
            if (result.Failed)
            {
                Close();
            }
            // 売上一覧表示
            BindSalesList(result.Value);

            grdList.Visible = true;
            grdDetail.Visible = true;
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            using (var searchFrm = new FrmAdvancedSearch(_criteriaDefinition, _filters.ToList()))
            {
                searchFrm.StartPosition = FormStartPosition.CenterParent;
                // 別インスタンスとして保存する
                searchFrm.OnSearchAndSave += (sender, e) => _filters = searchFrm.FilterCriterias.ToList();
                searchFrm.OnReset += (sender, e) => _filters = new List<FilterCriteria>();

                if (DialogHelper.IsPositiveResult(searchFrm.ShowDialog()))
                {
                    txtCondition.Text = FilterHelper.CreateFilterOptionStr(searchFrm.FilterCriterias.Select(f => f.AsFilterOptionAndCaption()).ToList());
                    await SyncDataAsync(searchFrm.FilterCriterias);
                }
            }
        }

        /// <summary>売上一覧にバインドする</summary>
        /// <param name="list">表示データ</param>
        private void BindSalesList(List<ViewInterestRateFixed> list)
        {
            // バインドの際に先頭行が選択されてSelChangeが発生することを回避するためにイベントハンドラを解除しておく
            grdList.SelChange -= GrdList_SelChange;

            // 伝票番号でグルーピングしてリスト表示
            var group = list.GroupBy(x => x.伝票番号);
            _salesSource = new SortableBindingList<ViewInterestRateFixed>(group.Select(x => x.First()).ToList());
            grdList.DataSource = _salesSource;

            // グリッド選択時に明細グリッドで参照するデータを記憶しておく
            for (int i = 0; i < grdList.Rows.Count - 1; i++)
            {
                grdList.Rows[i + 1].UserData = group.ElementAt(i);
            }

            grdList.AutoSizeCols(1, grdList.Cols.Count, 0);
            grdList.Select(-1, -1);
            grdList.SelChange += GrdList_SelChange;

            grdDetail.DataSource = Enumerable.Empty<ViewInterestRateFixed>();
        }

        /// <summary>売上一覧選択</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdList_SelChange(object sender, EventArgs e)
        {
            if (_prevSaleRow == grdList.Selection.BottomRow)
            {
                return;
            }
            _prevSaleRow = grdList.Selection.BottomRow;
            var selectedItem = grdList.Rows[grdList.Selection.BottomRow].UserData as IGrouping<string, ViewInterestRateFixed>;
            BindDetailList(selectedItem);
        }

        /// <summary>明細にバインドする</summary>
        /// <param name="list">表示データ</param>
        private void BindDetailList(IEnumerable<ViewInterestRateFixed> list)
        {
            _detailSource = new SortableBindingList<CheckableInterestRateFixed>(_detailMapper.Map<List<CheckableInterestRateFixed>>(list));
            grdDetail.DataSource = _detailSource;
            grdDetail.AutoSizeCols(1, grdDetail.Cols.Count, 0);
            UpdateEnabled();
        }

        /// <summary>コメント変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtComment_TextChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        /// <summary>明細グリッドのチェックボックス変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void grdDetail_CellChecked(object sender, RowColEventArgs e)
        {
            UpdateEnabled();
        }

        /// <summary>各コントロール使用可否を更新</summary>
        private void UpdateEnabled()
        {
            txtComment.Enabled = _detailSource?.Any() == true;
            btnSave.Enabled = _detailSource?.Any(x => x.選択) == true && !string.IsNullOrEmpty(txtComment.Text.Trim());
        }

        /// <summary>保存ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var checkedInterestRates = _detailSource.Where(x => x.選択)
                .Select(x => new InterestRateCheckFixedParameter()
                {
                    SalesNo = x.売上番号,
                    RowNo = x.売上行番号,
                    Comment = txtComment.Text,
                });
            var result = await ApiHelper.UpdateAsync(this, () =>
            {
                return Program.HatFApiClient.PutAsync<List<InterestRateCheckFixedResult>>(
                    ApiResources.HatF.Client.InterestRateCheckFixed, checkedInterestRates);
            });

            if (result.Failed)
            {
                Close();
            }
            // 画面を更新するためにバインドされているオブジェクトを更新する
            var updateTargets = _detailSource.Where(x => x.選択);
            foreach (var target in updateTargets)
            {
                var checkResult = result.Value.First();
                target.コメント者 = checkResult.Checker;
                target.コメント役職 = checkResult.CheckerPost;
                target.コメント = checkResult.Comment;
                // 別の伝票を選択して、再度当該の伝票を表示したときに値が消えてしまうので、売上一覧にバインドした大本のデータも更新する
                var salesSource = grdList.Rows.OfType<Row>().Skip(1)
                    .Select(x => (x.UserData as IGrouping<string, ViewInterestRateFixed>))
                    .SelectMany(x => x.AsEnumerable())
                    .Where(x => x.売上番号 == target.売上番号)
                    .Where(x => x.売上行番号 == target.売上行番号)
                    .FirstOrDefault();
                salesSource.コメント者 = checkResult.Checker;
                salesSource.コメント役職 = checkResult.CheckerPost;
                salesSource.コメント = checkResult.Comment;
            }
            // 再描画
            grdDetail.Invalidate();
            grdDetail.AutoSizeCols(1, grdDetail.Cols.Count, 0);

            txtComment.Text = string.Empty;
        }
    }
}