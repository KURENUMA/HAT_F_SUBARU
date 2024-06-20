using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.MasterSearch;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Purchase
{
    /// <summary>仕入編集画面</summary>
    public partial class PU_Edit : Form
    {
        #region グリッド制御用の内部クラス

        /// <summary>
        /// <para><see cref="PuImport"/>の項目に選択列を追加するためのクラス</para>
        /// <para>匿名オブジェクトでなくクラスとして定義しなければ、グリッドにバインドしても編集ができない</para>
        /// </summary>
        private class CheckablePuImport : PuImport
        {
            /// <summary>選択列</summary>
            public bool Checked { get; set; }

            /// <summary>金額（単価×数量）</summary>
            public decimal? Price => PuQuantity * PoPrice;
        }

        #endregion グリッド制御用の内部クラス

        #region グリッド列アクセス用プロパティ

        /// <summary>選択列</summary>
        private Column clmChecked => grdPuImports.Cols["Checked"];

        /// <summary>仕入先コード</summary>
        private Column clmSupCode => grdPuImports.Cols["SupCode"];

        /// <summary>支払先コード</summary>
        private Column clmPaySupCode => grdPuImports.Cols["PaySupCode"];

        /// <summary>納品書番号</summary>
        private Column clmDeliveryNo => grdPuImports.Cols["DeliveryNo"];

        /// <summary>納日</summary>
        private Column clmNoubi => grdPuImports.Cols["Noubi"];

        /// <summary>HAT注文番号</summary>
        private Column clmHatOrderNo => grdPuImports.Cols["HatOrderNo"];

        /// <summary>子番</summary>
        private Column clmKoban => grdPuImports.Cols["Koban"];

        /// <summary>F注番</summary>
        private Column clmChuban => grdPuImports.Cols["Chuban"];

        /// <summary>商品・コード</summary>
        private Column clmProdCode => grdPuImports.Cols["ProdCode"];

        /// <summary>数量</summary>
        private Column clmPuQuantity => grdPuImports.Cols["PuQuantity"];

        /// <summary>単価</summary>
        private Column clmPoPrice => grdPuImports.Cols["PoPrice"];

        /// <summary>金額</summary>
        private Column clmPrice => grdPuImports.Cols["Price"];

        /// <summary>消費税</summary>
        private Column clmTaxFlg => grdPuImports.Cols["TaxFlg"];

        /// <summary>区分</summary>
        private Column clmPuKbn => grdPuImports.Cols["PuKbn"];

        #endregion グリッド列アクセス用プロパティ

        #region 公開プロパティ

        /// <summary>仕入先コード</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PuCode
        {
            get => txtPuCode.Text;
            set => txtPuCode.Text = value;
        }

        /// <summary>仕入先名</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PuName
        {
            get => txtPuName.Text;
            set => txtPuName.Text = value;
        }

        /// <summary>HAT注文番号</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HatOrderNo
        {
            get => txtHatOrderNo.Text;
            set => txtHatOrderNo.Text = value;
        }

        /// <summary>仕入支払日From</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? PayDateFrom
        {
            get => HatFComParts.DoParseDateTime(dtPayDateFrom.Value);
            set => dtPayDateFrom.Value = value;
        }

        /// <summary>仕入支払日To</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? PayDateTo
        {
            get => HatFComParts.DoParseDateTime(dtPayDateTo.Value);
            set => dtPayDateTo.Value = value;
        }

        /// <summary>画面の表示と同時に検索を実行する</summary>
        public bool InitialSearch { get; set; } = false;

        #endregion 公開プロパティ

        /// <summary>保存ボタンにより終了された</summary>
        public event EventHandler OnSaveClosed;

        /// <summary>グリッドにバインドするデータソース</summary>
        private BindingList<CheckablePuImport> _dataSource;

        /// <summary>コンストラクタ</summary>
        public PU_Edit()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this, true);
                txtPuName.BackColor = HatFTheme.AquaColor;
                txtTotalPrice.BackColor = HatFTheme.AquaColor;
            }
        }

        #region メイン画面制御

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void PU_Edit_Load(object sender, EventArgs e)
        {
            CanExecuteSaveButton();
            if (InitialSearch)
            {
                var searchResult = await SearchAsync();
                if (searchResult != null)
                {
                    BindData(searchResult);
                }
            }
            // チェックボックス列のヘッダにチェックボックスを設定する
            var style = grdPuImports.Styles.Add("CheckBoxColumn");
            style.DataType = typeof(bool);
            style.ImageAlign = ImageAlignEnum.CenterCenter;
            grdPuImports.SetCellStyle(0, clmChecked.Index, style);

            // 区分列にコンボボックスを設定する
            // TODO リストをDBやJsonなどから取得するようにする
            clmPuKbn.DataMap = new ListDictionary()
            {
                {string.Empty, string.Empty},
                {0, "売上"},
            };

            // 消費税列にコンボボックスを設定する
            var taxDictionary = new ListDictionary();
            var taxRates = new DivTaxRate[] { new DivTaxRate() { TaxRateCd = string.Empty, TaxRate = 0, TaxRateName = string.Empty} }
                .Concat(ClientRepo.GetInstance().Options.DivTaxRates);
            foreach(var tax in taxRates)
            {
                taxDictionary.Add(tax.TaxRateCd, tax.TaxRateName);
            }
            clmTaxFlg.DataMap = taxDictionary;

        }

        /// <summary>合計金額の算出</summary>
        private void CalclulateTotalPrice()
        {
            txtTotalPrice.Text = $"{_dataSource.Sum(x => x.Price):C0}";
        }

        /// <summary>仕入先コードの検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSearchSupplier_Click(object sender, EventArgs e)
        {
            ShowSearchSupplierDialog(txtPuCode.Text.Trim(), form =>
            {
                txtPuCode.Text = form.StrMsShiresakiCode;
                txtPuName.Text = form.StrMsShiresakiName;
            });
        }

        /// <summary>仕入先検索画面を表示する</summary>
        /// <param name="defaultSupCode">初期仕入先コード</param>
        /// <param name="searchedAction">検索時の処理</param>
        /// <returns>true:検索確定 false:検索キャンセル</returns>
        private bool ShowSearchSupplierDialog(string defaultSupCode, Action<MS_Shiresaki> searchedAction)
        {
            using (var searchForm = new MS_Shiresaki())
            {
                searchForm.TxtTEAM_CD = LoginRepo.GetInstance().CurrentUser.TeamCode;
                searchForm.TxtSHIRESAKI_CD = defaultSupCode;
                var result = DialogHelper.IsPositiveResult(searchForm.ShowDialog());
                if (result)
                {
                    searchedAction(searchForm);
                }
                return result;
            }
        }

        /// <summary>グリッドのセル変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_CellChanged(object sender, RowColEventArgs e)
        {
            var sourceColumn = grdPuImports.Cols[e.Col];
            if (sourceColumn == clmPuQuantity || sourceColumn == clmPoPrice)
            {
                CalclulateTotalPrice();
                // 単価か数量が変わった場合は金額列を更新するために再描画させる
                grdPuImports.Invalidate();
            }
        }

        /// <summary>セル内ボタンクリック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_CellButtonClick(object sender, RowColEventArgs e)
        {
            var column = grdPuImports.Cols[e.Col];
            var target = grdPuImports.Rows[e.Row].DataSource as CheckablePuImport;
            // 仕入先コード列の場合
            if (column == clmSupCode)
            {
                e.Cancel = ShowSearchSupplierDialog(target.PaySupCode, form =>
                {
                    target.SupCode = form.StrMsShiresakiCode;
                });
            }
            // 支払先コード列の場合
            else if (column == clmPaySupCode)
            {
                e.Cancel = ShowSearchSupplierDialog(target.PaySupCode, form =>
                {
                    target.PaySupCode = form.StrMsShiresakiCode;
                });
            }
        }

        /// <summary>チェックボックスのチェック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_CellChecked(object sender, RowColEventArgs e)
        {
            if (e.Row == 0 & grdPuImports.Cols[e.Col] == clmChecked)
            {
                foreach (var row in _dataSource)
                {
                    row.Checked = grdPuImports.GetCellCheck(0, e.Col) == CheckEnum.Checked;
                }
                grdPuImports.Invalidate();
            }
        }

        /// <summary>グリッド行追加</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_AfterAddRow(object sender, RowColEventArgs e)
        {
            var addedRow = grdPuImports.Rows[e.Row].DataSource as CheckablePuImport;

            // 新規行追加時の初期値
            addedRow.Noubi = new DateTime(2024, 01, 01);
            addedRow.TaxFlg = "B";

            CanExecuteSaveButton();
        }

        /// <summary>グリッド行削除</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_AfterDeleteRow(object sender, RowColEventArgs e)
        {
            CanExecuteSaveButton();
        }

        /// <summary>DataSourceの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdPuImports_DataSourceChanged(object sender, EventArgs e)
        {
            CanExecuteSaveButton();
        }

        /// <summary>保存ボタンの押下可否を更新する</summary>
        private void CanExecuteSaveButton()
        {
            btnSAVE.Enabled = _dataSource?.Any() == true;
        }

        #endregion メイン画面制御

        #region 検索

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchResult = await SearchAsync();
            if (searchResult != null)
            {
                BindData(searchResult);
            }
        }

        /// <summary>検索実行</summary>
        /// <returns>検索結果</returns>
        private async Task<List<CheckablePuImport>> SearchAsync()
        {
            var supCode = PuCode;
            var hatOrderNo = HatOrderNo;
            var payDateFrom = PayDateFrom;
            var payDateTo = PayDateTo;
            var result = await ApiHelper.FetchAsync(this, async () =>
            {
                // APIの結果はPuImport型だがCheckablePuImportにデシリアライズする
                return await Program.HatFApiClient.GetAsync<List<CheckablePuImport>>(ApiResources.HatF.Purchase.PuImport, new
                {
                    supCode,
                    hatOrderNo,
                    payDateFrom,
                    payDateTo,
                });
            });
            return result.Successed ? result.Value : null;
        }

        /// <summary>仕入取込データをグリッドにバインドする</summary>
        /// <param name="list">仕入取込データ</param>
        private void BindData(List<CheckablePuImport> list)
        {
            _dataSource = new BindingList<CheckablePuImport>(list);
            grdPuImports.DataSource = _dataSource;
            grdPuImports.AutoSizeCols();
            CalclulateTotalPrice();
        }

        #endregion 検索

        #region 選択行に反映

        /// <summary>支払先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSearcyPaySupplier_Click(object sender, EventArgs e)
        {
            ShowSearchSupplierDialog(txtPaySupCode.Text.Trim(), form =>
            {
                txtPaySupCode.Text = form.StrMsShiresakiCode;
                txtPaySupName.Text = form.StrMsShiresakiName;
            });
        }

        /// <summary>選択行に反映ボタン（支払先コード）</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSetPaySupCode_Click(object sender, EventArgs e)
        {
            SetValueToCheckedRow(row => row.PaySupCode = txtPaySupCode.Text.Trim());
        }

        /// <summary>選択行に反映ボタン（納品書番号）</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSetDspNo_Click(object sender, EventArgs e)
        {
            SetValueToCheckedRow(row => row.DeliveryNo = txtDeliveryNo.Text.Trim());
        }

        /// <summary>選択行に反映ボタン（納日）</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnSetNouki_Click(object sender, EventArgs e)
        {
            SetValueToCheckedRow(row => row.Noubi = HatFComParts.DoParseDateTime(dtNouki.Value).Value);
        }

        /// <summary>チェックされた項目に対して処理を実行する</summary>
        /// <param name="action">処理内容</param>
        private void SetValueToCheckedRow(Action<CheckablePuImport> action)
        {
            foreach (var row in _dataSource.Where(x => x.Checked))
            {
                action(row);
            }
            grdPuImports.Invalidate();
        }

        /// <summary>支払先コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtPaySupCode_TextChanged(object sender, EventArgs e)
        {
            btnSetPaySupCode.Enabled = !string.IsNullOrEmpty(txtPaySupCode.Text.Trim());
        }

        /// <summary>納品書番号の変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtDspNo_TextChanged(object sender, EventArgs e)
        {
            btnSetDspNo.Enabled = !string.IsNullOrEmpty(txtDeliveryNo.Text.Trim());
        }

        /// <summary>納日の変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void DtNouki_ValueChanged(object sender, EventArgs e)
        {
            btnSetNouki.Enabled = dtNouki.HasValue;
        }

        #endregion 選択行に反映

        #region 保存

        /// <summary>保存ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSAVE_Click(object sender, EventArgs e)
        {
            if (!ValidateGrid() || !DialogHelper.SaveItemConfirm(this))
            {
                return;
            }
            // 消費税、区分列のコンボボックス選択肢にnullを設定できなかったため、空白ならnullに置き換える
            var list = _dataSource.ToList();
            foreach (var item in list)
            {
                item.PuKbn = string.IsNullOrEmpty(item.PuKbn) ? null : item.PuKbn;
                item.TaxFlg = string.IsNullOrEmpty(item.TaxFlg) ? null : item.TaxFlg;
            }
            var result = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Purchase.PuImport, list);
            });
            if (result.Failed)
            {
                return;
            }
            FormClosed += (s, ev) => OnSaveClosed?.Invoke(this, EventArgs.Empty);
            Close();
        }

        /// <summary>グリッドの入力内容を検証する</summary>
        /// <returns>成否</returns>
        private bool ValidateGrid()
        {
            // 仕入先コード
            {
                var error = _dataSource.FirstOrDefault(x => string.IsNullOrEmpty(x.SupCode));
                if (error != null)
                {
                    var row = grdPuImports.Rows.OfType<Row>().First(x => x.DataSource == error);
                    grdPuImports.Select(row.Index, clmSupCode.Index);
                    DialogHelper.WarningMessage(this, "仕入先コードが空白です。");
                    return false;
                }
            }

            // 納品書番号
            {
                var error = _dataSource.FirstOrDefault(x => string.IsNullOrEmpty(x.DeliveryNo));
                if (error != null)
                {
                    var row = grdPuImports.Rows.OfType<Row>().First(x => x.DataSource == error);
                    grdPuImports.Select(row.Index, clmDeliveryNo.Index);
                    DialogHelper.WarningMessage(this, "納品書番号が空白です。");
                    return false;
                }
            }
            // 仕入数量
            {
                var error = _dataSource.FirstOrDefault(x => x.PuQuantity <= 0);
                if (error != null)
                {
                    var row = grdPuImports.Rows.OfType<Row>().First(x => x.DataSource == error);
                    grdPuImports.Select(row.Index, clmPuQuantity.Index);
                    DialogHelper.WarningMessage(this, "仕入数量には0以上を入力してください。");
                    return false;
                }
            }

            // 納日、Hat注文番号、子番が重複する場合はNG
            {
                var duplicates = _dataSource
                    .GroupBy(x => new { x.Noubi, x.HatOrderNo, x.Koban })
                    .Where(x => x.Skip(1).Any())
                    .SelectMany(x => x);
                if (duplicates.Any())
                {
                    var error = duplicates.First();
                    var message = new StringBuilder()
                        .AppendLine("納日、Hat注文番号、子番の組み合わせが重複しています。")
                        .AppendLine($"納日：{error.Noubi:yy/MM/dd}")
                        .AppendLine($"Hat注文番号：{error.HatOrderNo}")
                        .AppendLine($"子番：{error.Koban}")
                        .ToString();
                    DialogHelper.WarningMessage(this, message);
                    return false;
                }
            }

            return true;
        }
        #endregion 保存
    }
}