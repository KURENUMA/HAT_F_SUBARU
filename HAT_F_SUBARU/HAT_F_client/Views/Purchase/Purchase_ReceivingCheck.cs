using C1.Win.C1FlexGrid;
using DocumentFormat.OpenXml.Drawing;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Purchase
{
    public partial class Purchase_ReceivingCheck : Form
    {
        private PurchaseRepo _purchaseRepo;

        private Dictionary<short, string> checkStatus = new Dictionary<short, string>() {
            { 0,"未確認"},
            { 1,"編集中"},
            { 2,"確認済"},
            { 3,"違算"},
            { 4,"未決"},
            { 5,"未請求"},
        };
        Dictionary<short, string> completeStatus = new Dictionary<short, string>()
        {
            { 0, "未確定" },
            { 1, "確定済み" }
        };


        public ViewPurchaseReceivingDetail Condition { get; set; } = new ViewPurchaseReceivingDetail();
        private string lockUserName;
        private string lockEditStartDateTime;
        private ScreenMode currentMode = ScreenMode.NewEntry;

        public enum ScreenMode
        {
            /// <summary>新規登録</summary>
            NewEntry,
            /// <summary>編集</summary>
            Edit,
            /// <summary>閲覧</summary>
            ReadOnly
        }

        public Purchase_ReceivingCheck()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                _purchaseRepo = PurchaseRepo.GetInstance();

            }
        }

        public async Task UpdateFormAsync(ViewPurchaseReceivingDetail condition)
        {
            Condition = condition;
            await UpdateListAsync();
        }

        private void SetCondition()
        {
            // フィルタ文字列が空の場合はフィルタをクリアする
            c1FlexGridPurchase_ReceivingCheck.ClearFilter();

            // 入力内容に応じてフィルタを適用する
            if (!string.IsNullOrWhiteSpace(textBoxPU_CODE.Text))
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["仕入先コード"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textBoxPU_CODE.Text;
            }

            if (!string.IsNullOrWhiteSpace(textBoxPU_NAME.Text))
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["仕入先"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textBoxPU_NAME.Text;
            }

            if (!string.IsNullOrWhiteSpace(textboxH_NUMBER.Text))
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["H注番"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textboxH_NUMBER.Text;
            }

            if(FromDate.Value!=DBNull.Value && ToDate.Value != DBNull.Value)
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = FromDate.Value;
                cf.Condition2.Operator = ConditionOperator.LessThanOrEqualTo;
                cf.Condition2.Parameter = ToDate.Value;
            }
            else if(FromDate.Value != DBNull.Value)
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = FromDate.Value;
            }
            else if(ToDate.Value != DBNull.Value)
            {
                var col = c1FlexGridPurchase_ReceivingCheck.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.LessThanOrEqualTo;
                cf.Condition1.Parameter = ToDate.Value;
            }
            // フィルタを適用する
            c1FlexGridPurchase_ReceivingCheck.ApplyFilters();

        }

        private async Task UpdateListAsync()
        {
            if (!String.IsNullOrEmpty(Condition.Hat注文番号))
            {
                var result = await _purchaseRepo.GetDetail(Condition);
                c1FlexGridPurchase_ReceivingCheck.DataSource = result;
                if(c1FlexGridPurchase_ReceivingCheck.Rows.Count >1)
                {
                    await AmountCheckLockAndSettingAsync();
                }
            }
        }

        #region <画面アクション>
        private async void Purchase_ReceivingCheck_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            c1FlexGridPurchase_ReceivingCheck.Cols["照合ステータス"].DataType = typeof(short);
            c1FlexGridPurchase_ReceivingCheck.Cols["照合ステータス"].DataMap = checkStatus;
            c1FlexGridPurchase_ReceivingCheck.Cols["売上確定"].DataType = typeof(short);
            c1FlexGridPurchase_ReceivingCheck.Cols["売上確定"].DataMap = completeStatus;

            if (!String.IsNullOrEmpty(Condition.Hat注文番号))
            {
                textBoxHATNUMBER.Text = Condition.Hat注文番号.ToString();
                await UpdateListAsync();
            }
            else
            {
                c1FlexGridPurchase_ReceivingCheck.DataSource = new List<ViewPurchaseReceivingDetail>();
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonFILTER_Click(object sender, EventArgs e)
        {
            SetCondition();
        }

        private void buttonCONTACT_EMAIL_Click(object sender, EventArgs e)
        {
            // Form.ShowDialog する場合は Dispose が必要です。
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog();
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

            for (var i = 1; i < c1FlexGridPurchase_ReceivingCheck.Rows.Count; i++)
            {
                //行単位ですべてのデータが入力されていることを保存対象とする
                var r = c1FlexGridPurchase_ReceivingCheck.Rows[i].DataSource as ViewPurchaseReceivingDetail;
                //if(!String.IsNullOrEmpty(r.M納日?.ToString()) && !String.IsNullOrEmpty(r.M注番?.ToString())
                //    && !String.IsNullOrEmpty(r.M数量?.ToString()) && !String.IsNullOrEmpty(r.M単価?.ToString()))
                if (!String.IsNullOrEmpty(r.M納日?.ToString()) && !String.IsNullOrEmpty(r.M数量?.ToString()))
                    {
                        result.Add(new PurchaseBillingDetail()
                    {
                        Hat注文番号 = r.Hat注文番号,
                        仕入先コード = r.仕入先コード,
                        //仕入先コード枝番 = r.仕入先コード枝番,
                        仕入先 = r.仕入先,
                        H注番 = r.H注番,
                        商品コード = r.商品コード,
                        商品名 = r.商品名,
                        H数量 = r.H数量 ?? 0,
                        H単価 = r.H単価 ?? 0,
                        M納日 = r.M納日,
                        M伝票番号 = r.伝票番号,
                        M注番 = r.M注番,
                        M数量 = r.M数量 ?? 0,
                        M単価 = r.M単価 ?? 0,
                        照合ステータス = r.照合ステータス ?? 0,
                        //倉庫コード = r.倉庫コード,
                        H行番号 = r.H行番号,
                        Hページ番号 = r.Hページ番号,
                        //社員Id = r.社員Id,
                        //部門コード = r.部門コード,
                        社員Id = 0,
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

        private async void buttonSAVE_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (CheckInputs())
            {
                // 画面からリクエストオブジェクト生成
                var request = ToSaveObjects();
                // 変更処理
                if(request.Count > 0)
                {
                    await ApiHelper.UpdateAsync(this, () =>
                        Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.PutPurchaseBillingDetail, request)
                    );
                }
                else
                {
                    DialogHelper.WarningMessage(this, "保存対象が０件です。\n入力可能データが全て入力されているデータが対処になります。");
                }
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void buttonCALCU_Click(object sender, EventArgs e)
        {
            // フィルタリングされた行の合計を計算する
            double totalAmountH = 0;
            double totalAmountM = 0;
            for (int i = 1; i < c1FlexGridPurchase_ReceivingCheck.Rows.Count; i++) // 1行目はヘッダーなのでスキップする
            {
                if (c1FlexGridPurchase_ReceivingCheck.Rows[i].Visible) // フィルタリングされた行だけを考慮する
                {
                    // H金額の処理
                    object valueH = c1FlexGridPurchase_ReceivingCheck[i, "H金額"];
                    if (valueH != null && double.TryParse(valueH.ToString(), out double amountH))
                    {
                        totalAmountH += amountH;
                    }

                    // M金額の処理
                    object valueM = c1FlexGridPurchase_ReceivingCheck[i, "M金額"];
                    if (valueM != null && double.TryParse(valueM.ToString(), out double amountM))
                    {
                        totalAmountM += amountM;
                    }
                }
            }
            textBoxH_TOTAL.Text = totalAmountH.ToString("C0");
            textBoxM_TOTAL.Text = totalAmountM.ToString("C0");
        }

        private void button_FILTER_RESET_Click(object sender, EventArgs e)
        {
            c1FlexGridPurchase_ReceivingCheck.ClearFilter();
        }

        private void validationNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 数字以外の入力を無視する
            }
        }


        private void c1FlexGridPurchase_ReceivingCheck_AfterEdit(object sender, RowColEventArgs e)
        {
            // 編集されたセルの情報を取得
            int editedRow = c1FlexGridPurchase_ReceivingCheck.Row;
            int editedCol = c1FlexGridPurchase_ReceivingCheck.Col;

            // 編集されたセルが数量列または単価列であるかを確認
            if (c1FlexGridPurchase_ReceivingCheck.Cols[editedCol].Name == "M数量" || c1FlexGridPurchase_ReceivingCheck.Cols[editedCol].Name == "M単価")
            {
                // 数量と単価が入力されているセルの値を取得
                object quantityValue = c1FlexGridPurchase_ReceivingCheck.GetDataDisplay(editedRow, c1FlexGridPurchase_ReceivingCheck.Cols["M数量"].Index);
                object unitPriceValue = c1FlexGridPurchase_ReceivingCheck.GetDataDisplay(editedRow, c1FlexGridPurchase_ReceivingCheck.Cols["M単価"].Index);

                if(string.IsNullOrWhiteSpace(quantityValue.ToString()) || string.IsNullOrWhiteSpace(unitPriceValue.ToString())){
                    c1FlexGridPurchase_ReceivingCheck[editedRow, c1FlexGridPurchase_ReceivingCheck.Cols["M金額"].Index] = String.Empty;
                    return;
                }
                // 数量と単価が数値に変換可能かチェック
                if (decimal.TryParse(quantityValue?.ToString(), out decimal quantity) &&
                    decimal.TryParse(unitPriceValue?.ToString().Substring(1), out decimal unitPrice))
                {
                    // 合計金額を計算
                    decimal totalAmount = quantity * unitPrice;

                    // 合計金額を別のセルに出力
                    c1FlexGridPurchase_ReceivingCheck[editedRow, c1FlexGridPurchase_ReceivingCheck.Cols["M金額"].Index] = totalAmount;
                }
            }
        }

        /// <summary>
        /// 仕入納品確認ロック機能
        /// </summary>
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
        /// <summary>
        /// 仕入納品確認アンロック機能
        /// </summary>
        private async Task<bool> AmountCheckUnlockAsync()
        {
            var result = await Program.HatFApiClient.PostAsync<bool>(ApiResources.HatF.Client.AmountCheckUnLock, new Dictionary<string, object>()
                {
                            { "hatOrderNo", Condition.Hat注文番号}
                });
            return result.Data;
        }

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
        /// <summary>
        /// 編集設定
        /// </summary>
        private void EditSetting()
        {
            lblScreenMode.ForeColor = System.Drawing.SystemColors.WindowText;
            lblLockInfo.Visible = false;
            btnUnlock.Visible = false;
            lblScreenMode.Text = "仕入納品確認";
            currentMode = ScreenMode.Edit;
            this.buttonCONTACT_EMAIL.Enabled = true;
            this.buttonSAVE.Enabled = true;
        }
        /// <summary>
        /// 読み取り設定
        /// </summary>
        private void ReadSetting()
        {
            lblScreenMode.ForeColor = System.Drawing.Color.Red;
            lblScreenMode.Text = "仕入納品確認（読み取り専用）";
            lblLockInfo.Text = "編集者：" + lockUserName
                + "\r\n"
                + "編集開始日時：" + lockEditStartDateTime;
            this.currentMode = ScreenMode.ReadOnly;
            this.buttonCONTACT_EMAIL.Enabled = false;
            this.buttonSAVE.Enabled = false;
            lblLockInfo.Visible = true;
            btnUnlock.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //差分があるセルの色を変更する
            // データ行の数
            int rowCount = c1FlexGridPurchase_ReceivingCheck.Rows.Count;

            // 全てのセルを走査して差分を比較
            for (int row = c1FlexGridPurchase_ReceivingCheck.Rows.Fixed; row < rowCount; row++)
            {
                if (String.IsNullOrEmpty(c1FlexGridPurchase_ReceivingCheck[row, "M納日"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridPurchase_ReceivingCheck[row, "M注番"]?.ToString()) && 
                    String.IsNullOrEmpty(c1FlexGridPurchase_ReceivingCheck[row, "M数量"]?.ToString()) && 
                    String.IsNullOrEmpty(c1FlexGridPurchase_ReceivingCheck[row, "M単価"]?.ToString()))
                {
                    //色を戻す
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M納日"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M注番"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M数量"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M単価"].Index).StyleNew.BackColor = SystemColors.Window;
                    continue;
                }

                // 値が異なる場合に背景色を変更

                if (!c1FlexGridPurchase_ReceivingCheck[row, "M納日"].Equals(c1FlexGridPurchase_ReceivingCheck[row, "H納日"]))
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M納日"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M納日"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!c1FlexGridPurchase_ReceivingCheck[row, "M注番"].Equals(c1FlexGridPurchase_ReceivingCheck[row, "H注番"]))
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M注番"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M注番"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (decimal.TryParse(c1FlexGridPurchase_ReceivingCheck[row, "M数量"]?.ToString(), out decimal mQuantity) &&
                     int.TryParse(c1FlexGridPurchase_ReceivingCheck[row, "H数量"]?.ToString(), out int hQuantity))
                {
                    // M数量とH数量の値が異なる場合、背景色を変更
                    if (mQuantity != hQuantity)
                    {
                        // 背景色を黄色に変更
                        c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M数量"].Index).StyleNew.BackColor = Color.Yellow;
                    }
                    else
                    {
                        c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M数量"].Index).StyleNew.BackColor = SystemColors.Window;
                    }
                }
                else
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M数量"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!c1FlexGridPurchase_ReceivingCheck[row, "M単価"].Equals(c1FlexGridPurchase_ReceivingCheck[row, "H単価"]))
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M単価"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridPurchase_ReceivingCheck.GetCellRange(row, c1FlexGridPurchase_ReceivingCheck.Cols["M単価"].Index).StyleNew.BackColor = SystemColors.Window;
                }
            }
        }

        private void c1FlexGridPurchase_ReceivingCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (c1FlexGridPurchase_ReceivingCheck.Cols[c1FlexGridPurchase_ReceivingCheck.Col].Name == "M納日")
                {
                    c1FlexGridPurchase_ReceivingCheck[c1FlexGridPurchase_ReceivingCheck.Row, c1FlexGridPurchase_ReceivingCheck.Col] = null;
                }
            }
        }

        private async void btnUnlock_Click(object sender, EventArgs e)
        {
            if (DialogHelper.YesNoQuestion(this, "読み取り専用を解除しますか？"))
            {
                await AmountCheckUnlockAsync();
                await AmountCheckLockAndSettingAsync();
            }
        }

        private async void c1FlexGridPurchase_ReceivingCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentMode == ScreenMode.Edit)
            {
                if (c1FlexGridPurchase_ReceivingCheck.Rows.Count > 1)
                {
                    await AmountCheckUnlockAsync();
                }
            }
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(textBoxHATNUMBER.Text) && Condition.Hat注文番号 != textBoxHATNUMBER.Text)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (currentMode == ScreenMode.ReadOnly)
                {
                    EditSetting();
                }
                else
                {
                    if (c1FlexGridPurchase_ReceivingCheck.Rows.Count > 1)
                    {
                        await AmountCheckUnlockAsync();
                    }
                }
                Condition.Hat注文番号 = textBoxHATNUMBER.Text;
                await UpdateListAsync();
                Cursor.Current = Cursors.Default;
            }
            
        }
    }
}
