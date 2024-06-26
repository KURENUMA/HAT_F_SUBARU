using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using HatFClient.Views.MasterSearch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Purchase
{
    /// <summary>仕入金額照合画面</summary>
    public partial class PU_AmountCheck : Form
    {
        private PurchaseRepo _purchaseRepo;

        private readonly Dictionary<short, string> checkStatus = new Dictionary<short, string>() {
            { 0,"未確認"},
            { 1,"編集中"},
            { 2,"確認済"},
            { 3,"違算"},
            { 4,"未決"},
            { 5,"未請求"},
        };

        private readonly Dictionary<short, string> completeStatus = new Dictionary<short, string>()
        {
            { 0, "未確定" },
            { 1, "確定済み" }
        };

        public ViewPurchaseBillingDetailCondition Condition { get; set; } = new ViewPurchaseBillingDetailCondition();
        private string lockUserName;
        private string lockEditStartDateTime;
        // TODO 暫定的に排他制御機能を封印
        //private ScreenMode currentMode = ScreenMode.NewEntry;

        /// <summary>画面編集状態</summary>
        public enum ScreenMode
        {
            /// <summary>新規登録</summary>
            NewEntry,

            /// <summary>編集</summary>
            Edit,

            /// <summary>閲覧</summary>
            ReadOnly
        }

        #region グリッド列アクセス用プロパティ

        /// <summary>Hat注文番号</summary>
        private Column clmHat注文番号 => grdPuAmountCheck.Cols["Hat注文番号"];

        /// <summary>H子番</summary>
        private Column clmH子番 => grdPuAmountCheck.Cols["H子番"];

        /// <summary>伝票番号</summary>
        private Column clm伝票番号 => grdPuAmountCheck.Cols["伝票番号"];

        /// <summary>売上確定</summary>
        private Column clm売上確定 => grdPuAmountCheck.Cols["売上確定"];

        /// <summary>H納日</summary>
        private Column clmH納日 => grdPuAmountCheck.Cols["H納日"];

        /// <summary>商品コード</summary>
        private Column clm商品コード => grdPuAmountCheck.Cols["商品コード"];

        /// <summary>商品名</summary>
        private Column clm商品名 => grdPuAmountCheck.Cols["商品名"];

        /// <summary>H注番</summary>
        private Column clmH注番 => grdPuAmountCheck.Cols["H注番"];

        /// <summary>H数量</summary>
        private Column clmH数量 => grdPuAmountCheck.Cols["H数量"];

        /// <summary>H単価</summary>
        private Column clmH単価 => grdPuAmountCheck.Cols["H単価"];

        /// <summary>H金額</summary>
        private Column clmH金額 => grdPuAmountCheck.Cols["H金額"];

        /// <summary>M伝票番号</summary>
        private Column clmM伝票番号 => grdPuAmountCheck.Cols["M伝票番号"];

        /// <summary>M納品書番号</summary>
        private Column clmM納品書番号 => grdPuAmountCheck.Cols["M納品書番号"];

        /// <summary>M納日</summary>
        private Column clmM納日 => grdPuAmountCheck.Cols["M納日"];

        /// <summary>M注番</summary>
        private Column clmM注番 => grdPuAmountCheck.Cols["M注番"];

        /// <summary>M数量</summary>
        private Column clmM数量 => grdPuAmountCheck.Cols["M数量"];

        /// <summary>M単価</summary>
        private Column clmM単価 => grdPuAmountCheck.Cols["M単価"];

        /// <summary>M金額</summary>
        private Column clmM金額 => grdPuAmountCheck.Cols["M金額"];

        /// <summary>照合ステータス</summary>
        private Column clm照合ステータス => grdPuAmountCheck.Cols["照合ステータス"];

        /// <summary>伝区</summary>
        private Column clm伝区 => grdPuAmountCheck.Cols["伝区"];

        /// <summary>仕入番号</summary>
        private Column clm仕入番号 => grdPuAmountCheck.Cols["仕入番号"];

        /// <summary>仕入行番号</summary>
        private Column clm仕入行番号 => grdPuAmountCheck.Cols["仕入行番号"];

        /// <summary>仕入先</summary>
        private Column clm仕入先 => grdPuAmountCheck.Cols["仕入先"];

        /// <summary>仕入先コード</summary>
        private Column clm仕入先コード => grdPuAmountCheck.Cols["仕入先コード"];

        #endregion グリッド列アクセス用プロパティ

        /// <summary>コンストラクタ</summary>
        public PU_AmountCheck()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                _purchaseRepo = PurchaseRepo.GetInstance();
            }
        }

        #region メイン画面制御

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void PU_AmountCheck_Load(object sender, EventArgs e)
        {
            clm照合ステータス.DataType = typeof(short);
            clm照合ステータス.DataMap = checkStatus;
            clm売上確定.DataType = typeof(short);
            clm売上確定.DataMap = completeStatus;
            if (Condition.Hat注文番号 != null)
            {
                txtPuCode.Text = Condition.仕入先コード.ToString();
                txtHatOrderNo.Text = Condition.Hat注文番号.ToString();
                if (!await SearchAsync())
                {
                    Close();
                }
            }
            else
            {
                grdPuAmountCheck.DataSource = new List<ViewPurchaseBillingDetail>();
            }

            // TODO 暫定的に排他制御機能を封印
            btnUnlock.Visible = false;
        }

        private void PU_AmountCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormFactory.GetModelessFormCache<PU_Edit>()?.Close();
            // TODO 暫定的に排他制御機能を封印
            //if (currentMode == ScreenMode.Edit)
            //{
            //    if (grdPuAmountCheck.Rows.Count > 1)
            //    {
            //        await AmountCheckUnlockAsync();
            //    }
            //}
        }

        /// <summary>検索実行</summary>
        /// <returns>成否</returns>
        private async Task<bool> SearchAsync()
        {
            if (Condition.Hat注文番号 != null)
            {
                var details = await ApiHelper.FetchAsync(this, async () =>
                {
                    return await _purchaseRepo.GetDetailAsync(Condition);
                });
                if (details.Failed)
                {
                    return false;
                }
                grdPuAmountCheck.DataSource = details.Value;
                if (grdPuAmountCheck.Rows.Count > 1)
                {
                    //txtPuName.Text = details.Value.First().仕入先;
                    // TODO 暫定的に排他制御機能を封印
                    // await AmountCheckLockAndSettingAsync();
                }
                grdPuAmountCheck.AutoSizeCols();
            }
            return true;
        }

        /// <summary>編集設定</summary>
        private void EditSetting()
        {
            lblScreenMode.ForeColor = SystemColors.WindowText;

            lblLockInfo.Visible = false;
            btnUnlock.Visible = false;

            lblScreenMode.Text = "仕入金額照合";

            // TODO 暫定的に排他制御機能を封印
            //currentMode = ScreenMode.Edit;

            this.btnContactEmail.Enabled = true;
            this.btnSave.Enabled = true;
        }

        /// <summary>読み取り設定</summary>
        private void ReadSetting()
        {
            lblScreenMode.ForeColor = Color.Red;

            lblLockInfo.Visible = true;
            btnUnlock.Visible = true;

            lblScreenMode.Text = "仕入金額照合（読み取り専用）";
            lblLockInfo.Text = $"編集者：{lockUserName}{Environment.NewLine}編集開始日時：{lockEditStartDateTime}";

            // TODO 暫定的に排他制御機能を封印
            //this.currentMode = ScreenMode.ReadOnly;

            this.btnContactEmail.Enabled = false;
            this.btnSave.Enabled = false;
        }

        #endregion メイン画面制御

        #region グリッド制御

        private void grdPuAmountCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (grdPuAmountCheck.Cols[grdPuAmountCheck.Col] == clmM納日)
                {
                    grdPuAmountCheck[grdPuAmountCheck.Row, grdPuAmountCheck.Col] = null;
                }
            }
        }

        private void grdPuAmountCheck_AfterEdit(object sender, RowColEventArgs e)
        {
            // 編集されたセルの情報を取得
            int editedRow = grdPuAmountCheck.Row;
            int editedCol = grdPuAmountCheck.Col;

            // 編集されたセルが数量列または単価列であるかを確認
            if (grdPuAmountCheck.Cols[editedCol] == clmM数量 || grdPuAmountCheck.Cols[editedCol] == clmM単価)
            {
                // 数量と単価が入力されているセルの値を取得
                object quantityValue = grdPuAmountCheck.GetDataDisplay(editedRow, clmM数量.Index);
                object unitPriceValue = grdPuAmountCheck.GetDataDisplay(editedRow, clmM単価.Index);

                if (string.IsNullOrWhiteSpace(quantityValue.ToString()) || string.IsNullOrWhiteSpace(unitPriceValue.ToString()))
                {
                    grdPuAmountCheck[editedRow, clmM金額.Index] = string.Empty;
                    return;
                }
                // 数量と単価が数値に変換可能かチェック
                if (decimal.TryParse(quantityValue?.ToString(), out decimal quantity) &&
                    decimal.TryParse(unitPriceValue?.ToString().Substring(1), out decimal unitPrice))
                {
                    // 合計金額を計算
                    decimal totalAmount = quantity * unitPrice;

                    // 合計金額を別のセルに出力
                    grdPuAmountCheck[editedRow, clmM金額.Index] = totalAmount;
                }
            }
        }

        #endregion グリッド制御

        #region 排他制御（暫定的に不使用）

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (DialogHelper.YesNoQuestion(this, "読み取り専用を解除しますか？"))
            {
                // TODO 暫定的に排他制御機能を封印
                //await AmountCheckUnlockAsync();
                //await AmountCheckLockAndSettingAsync();
            }
        }

        /// <summary>仕入金額照合ロック機能</summary>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> AmountCheckLockAsync()
        {
            var result = await Program.HatFApiClient.PostAsync<Dictionary<string, string>>
                (ApiResources.HatF.Client.AmountCheckLock, new Dictionary<string, object>()
                {
                    { "hatOrderNo", Condition.Hat注文番号},
                    { "empid",LoginRepo.GetInstance().CurrentUser.EmployeeCode }
                });
            return result.Data;
        }

        /// <summary>仕入金額照合アンロック機能</summary>
        private async Task<bool> AmountCheckUnlockAsync()
        {
            var result = await Program.HatFApiClient.PostAsync<bool>(ApiResources.HatF.Client.AmountCheckUnLock, new Dictionary<string, object>()
            {
                {"hatOrderNo", Condition.Hat注文番号}
            });
            return result.Data;
        }

        /// <summary>排他制御をかけて画面表示を更新する</summary>
        /// <returns>非同期タスク</returns>
        private async Task AmountCheckLockAndSettingAsync()
        {
            var result = await AmountCheckLockAsync();
            if (result.Count == 0)
            {
                EditSetting();
            }
            else
            {
                lockUserName = result["emp_name"];
                lockEditStartDateTime = result["edit_start_datetime"];
                ReadSetting();
            }
        }

        #endregion 排他制御（暫定的に不使用）

        #region 絞り込み

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SetCondition();
        }

        private void btnFilterResetClick(object sender, EventArgs e)
        {
            grdPuAmountCheck.ClearFilter();
        }

        private void SetCondition()

        {
            // フィルタ文字列が空の場合はフィルタをクリアする
            grdPuAmountCheck.ClearFilter();

            // 入力内容に応じてフィルタを適用する

            // HAT注文番号
            if (!string.IsNullOrEmpty(txtHatOrderNo.Text))
            {
                var col = clmHat注文番号;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = txtHatOrderNo.Text;
            }

            // H注番
            if (!string.IsNullOrWhiteSpace(txtHChuban.Text))
            {
                var col = clmH注番;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = txtHChuban.Text;
            }

            // H納日(From～To)
            if (dtHNoukiFrom.HasValue && dtHNoukiTo.HasValue)
            {
                var col = clmH納日;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = dtHNoukiFrom.Value;
                cf.Condition2.Operator = ConditionOperator.LessThanOrEqualTo;
                cf.Condition2.Parameter = dtHNoukiTo.Value;
            }
            // H納日(From～)
            else if (dtHNoukiFrom.HasValue)
            {
                var col = clmH納日;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = dtHNoukiFrom.Value;
            }
            // H納日(～To)
            else if (dtHNoukiTo.HasValue)
            {
                var col = clmH納日;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.LessThanOrEqualTo;
                cf.Condition1.Parameter = dtHNoukiTo.Value;
            }
            // フィルタを適用する
            grdPuAmountCheck.ApplyFilters();
        }

        #endregion 絞り込み

        #region 保存

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {
                // 画面からリクエストオブジェクト生成
                var request = ToSaveObjects();
                // 変更処理
                if (request.Count > 0)
                {
                    await ApiHelper.UpdateAsync(this, async () =>
                    {
                        return await Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.PutPurchaseBillingDetail, request);
                    });
                }
                else
                {
                    DialogHelper.WarningMessage(this, "保存対象が０件です。\n入力可能データが全て入力されているデータが対処になります。");
                }
            }
        }

        /// <summary>必須項目の入力チェック</summary>
        private bool CheckInputs()
        {
            return true;
        }

        private List<PurchaseBillingDetail> ToSaveObjects()
        {
            List<PurchaseBillingDetail> result = new();

            for (var i = 1; i < grdPuAmountCheck.Rows.Count; i++)
            {
                //行単位ですべてのデータが入力されていることを保存対象とする
                var r = grdPuAmountCheck.Rows[i].DataSource as ViewPurchaseBillingDetail;
                //if (!string.IsNullOrEmpty(r.M納日?.ToString()) && !string.IsNullOrEmpty(r.M注番?.ToString())
                //    && !string.IsNullOrEmpty(r.M数量?.ToString()) && !string.IsNullOrEmpty(r.M単価?.ToString()))
                if (true)
                {
                    result.Add(new PurchaseBillingDetail()
                    {
                        //Hat注文番号 = r.Hat注文番号,
                        //仕入先コード = r.仕入先コード,
                        //仕入先コード枝番 = r.仕入先コード枝番,
                        //仕入先 = r.仕入先,
                        //H注番 = r.H注番,
                        //商品コード = r.商品コード,
                        //商品名 = r.商品名,
                        //H数量 = r.H数量 ?? 0,
                        //H単価 = r.H単価 ?? 0,
                        //M納日 = r.M納日,
                        M伝票番号 = r.伝票番号,
                        M注番 = r.M注番,
                        M数量 = r.M数量 ?? 0,
                        M単価 = r.M単価 ?? 0,
                        //照合ステータス = r.照合ステータス ?? 0,
                        //倉庫コード = r.倉庫コード,
                        //H行番号 = r.H行番号,
                        //Hページ番号 = r.Hページ番号,
                        //社員Id = r.社員Id,
                        //部門コード = r.部門コード,
                        社員Id = 0,
                        // TODO 部門コードを正しく設定
                        部門コード = "dummy",
                        備考 = null,
                        伝区 = r.伝区,
                        仕入番号 = r.仕入番号,
                        仕入行番号 = r.仕入行番号,
                    });
                }
            }

            return result;
        }

        #endregion 保存

        #region 担当者へ連絡

        private void btnContactEmail_Click(object sender, EventArgs e)
        {
            // Form.ShowDialog する場合は Dispose が必要です。
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog();
            }
        }

        #endregion 担当者へ連絡

        #region 再計算

        /// <summary>再計算ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            // フィルタリングされた行のみ取得
            var filteredRows = grdPuAmountCheck.Rows.OfType<Row>()
                .Where(x => x.Visible)
                .Select(x => x.DataSource as ViewPurchaseBillingDetail)
                .Where(x => x != null)
                .ToList();
            //txtHTotal.Text = $"{filteredRows.Sum(x => x.H金額):C0}";
            txtMTotal.Text = $"{filteredRows.Sum(x => x.M金額):C0}";
        }

        #endregion 再計算

        private void btnDifferenceCheck_Click(object sender, EventArgs e)
        {
            //差分があるセルの色を変更する
            // データ行の数
            int rowCount = grdPuAmountCheck.Rows.Count;

            // 全てのセルを走査して差分を比較
            for (int row = grdPuAmountCheck.Rows.Fixed; row < rowCount; row++)
            {
                if (string.IsNullOrEmpty(grdPuAmountCheck[row, "M納日"]?.ToString()) &&
                    string.IsNullOrEmpty(grdPuAmountCheck[row, "M注番"]?.ToString()) &&
                    string.IsNullOrEmpty(grdPuAmountCheck[row, "M数量"]?.ToString()) &&
                    string.IsNullOrEmpty(grdPuAmountCheck[row, "M単価"]?.ToString()))
                {
                    //色を戻す
                    grdPuAmountCheck.GetCellRange(row, clmM納日.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(row, clmM注番.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(row, clmM数量.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(row, clmM単価.Index).StyleNew.BackColor = SystemColors.Window;
                    continue;
                }

                // 値が異なる場合に背景色を変更

                if (!grdPuAmountCheck[row, "M納日"].Equals(grdPuAmountCheck[row, "H納日"]))
                {
                    grdPuAmountCheck.GetCellRange(row, clmM納日.Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    grdPuAmountCheck.GetCellRange(row, clmM納日.Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!grdPuAmountCheck[row, "M注番"].Equals(grdPuAmountCheck[row, "H注番"]))
                {
                    grdPuAmountCheck.GetCellRange(row, clmM注番.Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    grdPuAmountCheck.GetCellRange(row, clmM注番.Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (decimal.TryParse(grdPuAmountCheck[row, "M数量"]?.ToString(), out decimal mQuantity) &&
                     int.TryParse(grdPuAmountCheck[row, "H数量"]?.ToString(), out int hQuantity))
                {
                    // M数量とH数量の値が異なる場合、背景色を変更
                    if (mQuantity != hQuantity)
                    {
                        // 背景色を黄色に変更
                        grdPuAmountCheck.GetCellRange(row, clmM数量.Index).StyleNew.BackColor = Color.Yellow;
                    }
                    else
                    {
                        grdPuAmountCheck.GetCellRange(row, clmM数量.Index).StyleNew.BackColor = SystemColors.Window;
                    }
                }
                else
                {
                    grdPuAmountCheck.GetCellRange(row, clmM数量.Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!grdPuAmountCheck[row, "M単価"].Equals(grdPuAmountCheck[row, "H単価"]))
                {
                    grdPuAmountCheck.GetCellRange(row, clmM単価.Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    grdPuAmountCheck.GetCellRange(row, clmM単価.Index).StyleNew.BackColor = SystemColors.Window;
                }
            }
        }

        #region 一括確認

        private void btnSetStatus_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < grdPuAmountCheck.Rows.Count; i++) // 1行目はヘッダーなのでスキップする
            {
                if (grdPuAmountCheck.Rows[i].Visible) // フィルタリングされた行だけを考慮する
                {
                    if (grdPuAmountCheck[i, "照合ステータス"] == null || (int)grdPuAmountCheck[i, "照合ステータス"] == 0)
                    {
                        grdPuAmountCheck[i, "照合ステータス"] = 2; // 2 は "確認済" のキー
                    }
                }
            }
        }

        #endregion 一括確認

        #region 検索

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // TODO 暫定的に排他制御機能を封印
            //if (currentMode == ScreenMode.ReadOnly)
            //{
            //    EditSetting();
            //}
            //else
            //{
            //    if (grdPuAmountCheck.Rows.Count > 1)
            //    {
            //        await AmountCheckUnlockAsync();
            //    }
            //}
            Condition.仕入先コード = txtPuCode.Text;
            if (!string.IsNullOrEmpty(txtHatOrderNo.Text.Trim()))
            {
                Condition.Hat注文番号 = txtHatOrderNo.Text.Trim();
            }
            if (dtPayDateFrom.HasValue)
            {
                Condition.仕入支払年月日From = HatFComParts.DoParseDateTime(dtPayDateFrom.Value);
            }
            if (dtPayDateTo.HasValue)
            {
                Condition.仕入支払年月日To = HatFComParts.DoParseDateTime(dtPayDateTo.Value);
            }
            await SearchAsync();
        }

        /// <summary>仕入先コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void txtPuCode_TextChanged(object sender, EventArgs e)
        {
            EnableSearchButton();
        }

        /// <summary>検索ボタンの押下可否を設定</summary>
        private void EnableSearchButton()
        {
            btnSearch.Enabled = !string.IsNullOrEmpty(txtPuCode.Text.Trim());
        }

        #endregion 検索

        #region 仕入登録

        /// <summary>仕入登録ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnPuEdit_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessFormCache<PU_Edit>();
            if(form == null)
            {
                // 初回起動時のみ検索条件を設定する
                form = FormFactory.GetModelessForm<PU_Edit>();
                form.PuCode = txtPuCode.Text;
                form.PuName = txtPuName.Text;
                form.HatOrderNo = txtHatOrderNo.Text;
                form.PayDateFrom = HatFComParts.DoParseDateTime(dtPayDateFrom.Value);
                form.PayDateTo = HatFComParts.DoParseDateTime(dtPayDateTo.Value);
                form.InitialSearch = true;
            }
            form.Show();
            form.Activate();
        }

        #endregion 仕入登録

        #region 仕入先検索

        /// <summary>仕入先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            using (var searchForm = new MS_Shiresaki())
            {
                searchForm.TxtTEAM_CD = LoginRepo.GetInstance().CurrentUser.TeamCode;
                searchForm.TxtSHIRESAKI_CD = txtPuCode.Text;
                if (DialogHelper.IsPositiveResult(searchForm.ShowDialog()))
                {
                    txtPuCode.Text = searchForm.StrMsShiresakiCode;
                    txtPuName.Text = searchForm.StrMsShiresakiName;
                }
            }
        }

        #endregion 仕入先検索

    }
}