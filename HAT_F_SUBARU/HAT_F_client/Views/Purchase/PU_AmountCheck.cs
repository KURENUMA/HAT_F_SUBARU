using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        #region 公開プロパティ
        /// <summary>仕入先コード</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SupplierCode
        {
            get => txtPuCode.Text;
            set => txtPuCode.Text = value;
        }

        /// <summary>仕入先名</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SupplierName
        {
            get => txtPuName.Text;
            set => txtPuName.Text = value;
        }

        /// <summary>Hat注文番号</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HatOrderNo
        {
            get => txtHatOrderNo.Text;
            set => txtHatOrderNo.Text = value;
        }
        #endregion

        #region グリッド列アクセス用プロパティ

        /// <summary>伝票番号</summary>
        public Column clm伝票番号 => grdPuAmountCheck.Cols["伝票番号"];

        /// <summary>納日</summary>
        public Column clmF納日 => grdPuAmountCheck.Cols["F納日"];

        /// <summary>売上確定</summary>
        public Column clm売上確定 => grdPuAmountCheck.Cols["売上確定"];

        /// <summary>照合ステータス</summary>
        public Column clm照合ステータス => grdPuAmountCheck.Cols["照合ステータス"];

        /// <summary>商品コード</summary>
        public Column clmF商品コード => grdPuAmountCheck.Cols["F商品コード"];

        /// <summary>商品名</summary>
        public Column clmF商品名 => grdPuAmountCheck.Cols["F商品名"];

        /// <summary>F注番</summary>
        public Column clmF注番 => grdPuAmountCheck.Cols["F注番"];

        /// <summary>F数量</summary>
        public Column clmF数量 => grdPuAmountCheck.Cols["F数量"];

        /// <summary>F単価</summary>
        public Column clmF単価 => grdPuAmountCheck.Cols["F単価"];

        /// <summary>F金額</summary>
        public Column clmF金額 => grdPuAmountCheck.Cols["F金額"];

        /// <summary>F税区分</summary>
        public Column clmF消費税区分 => grdPuAmountCheck.Cols["F消費税区分"];

        /// <summary>F税率</summary>
        public Column clmF税率 => grdPuAmountCheck.Cols["F税率"];

        /// <summary>M納品書番号</summary>
        public Column clmM納品書番号 => grdPuAmountCheck.Cols["M納品書番号"];

        /// <summary>M納日</summary>
        public Column clmM納入日 => grdPuAmountCheck.Cols["M納入日"];

        /// <summary>M注番</summary>
        public Column clmM注番 => grdPuAmountCheck.Cols["M注番"];

        /// <summary>M数量</summary>
        public Column clmM数量 => grdPuAmountCheck.Cols["M数量"];

        /// <summary>M単価</summary>
        public Column clmM単価 => grdPuAmountCheck.Cols["M単価"];

        /// <summary>M金額</summary>
        public Column clmM金額 => grdPuAmountCheck.Cols["M金額"];

        /// <summary>M税区分</summary>
        public Column clmM消費税区分 => grdPuAmountCheck.Cols["M消費税区分"];

        /// <summary>M税率</summary>
        public Column clmM消費税率 => grdPuAmountCheck.Cols["M消費税率"];

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
#if DEBUG
            txtPuCode.Text = "665001";
            txtHatOrderNo.Text = "33AH02C";
#endif

            clm照合ステータス.DataType = typeof(short);
            clm照合ステータス.DataMap = checkStatus;
            clm売上確定.DataType = typeof(short);
            clm売上確定.DataMap = completeStatus;
            Condition.仕入先コード = SupplierCode;
            Condition.Hat注文番号 = HatOrderNo;
            if (!await SearchAsync())
            {
                Close();
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
            var details = await ApiHelper.FetchAsync(this, async () =>
            {
                return await _purchaseRepo.GetDetailAsync(Condition);
            });
            if (details.Failed)
            {
                return false;
            }
            var dataSource = new BindingList<ViewPurchaseBillingDetail>(details.Value);
            grdPuAmountCheck.DataSource = dataSource;
            if (dataSource.Any())
            {
                txtPuName.Text = details.Value.First().F仕入先;
                // TODO 暫定的に排他制御機能を封印
                // await AmountCheckLockAndSettingAsync();
            }
            grdPuAmountCheck.AutoSizeCols();
            CalcTotalAmount();
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

        /// <summary>合計金額を計算する</summary>
        private void CalcTotalAmount()
        {
            // フィルタリングされた行のみ取得
            var filteredRows = grdPuAmountCheck.Rows.OfType<Row>()
                .Where(x => x.Visible)
                .Select(x => x.DataSource as ViewPurchaseBillingDetail)
                .Where(x => x != null)
                .ToList();
            var taxRates = filteredRows
                .Select(x => x.F消費税区分).Concat(filteredRows.Select(x => x.M消費税区分))
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct().ToList();
            var amounts = new[]
            {
                new
                {
                    Name = "金額合計",
                    FAmount = filteredRows.Sum(x => x.F金額),
                    MAmount = filteredRows.Sum(x => x.M金額),
                }
            }.Concat(taxRates.Select(x => new
            {
                Name = ClientRepo.GetInstance().Options.DivTaxRates.FirstOrDefault(z => z.TaxRateCd == x)?.TaxRateName,
                FAmount = filteredRows.Where(z => z.F消費税区分 == x).Sum(z => z.F金額 * (z.F消費税率 / 100m)),
                MAmount = filteredRows.Where(z => z.M消費税区分 == x).Sum(z => z.M金額 * (z.M消費税率 / 100m)),
            })).ToList();

            grdTotalAmount.DataSource = amounts;
            grdTotalAmount.AutoSizeCols();
        }
        #endregion メイン画面制御

        #region グリッド制御

        private void grdPuAmountCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (grdPuAmountCheck.Cols[grdPuAmountCheck.Col] == clmM納入日)
                {
                    grdPuAmountCheck[grdPuAmountCheck.Row, grdPuAmountCheck.Col] = null;
                }
            }
        }

        private void grdPuAmountCheck_AfterEdit(object sender, RowColEventArgs e)
        {
            var amountChanged = false;
            if (e.Col == clmM数量.Index || e.Col == clmM単価.Index)
            {
                var dataSource = grdPuAmountCheck.Rows[e.Row].DataSource as ViewPurchaseBillingDetail;
                dataSource.M金額 = dataSource.M単価 * dataSource.M数量;
                amountChanged = true;
            }
            else if (e.Col == clmM消費税区分.Index)
            {
                var dataSource = grdPuAmountCheck.Rows[e.Row].DataSource as ViewPurchaseBillingDetail;
                dataSource.M消費税率 = ClientRepo.GetInstance().Options.DivTaxRates.FirstOrDefault(x => x.TaxRateCd == dataSource.M消費税区分)?.TaxRate;
                amountChanged = true;
            }
            if (amountChanged)
            {
                CalcTotalAmount();
                grdPuAmountCheck.Invalidate();
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
            CalcTotalAmount();
        }

        private void btnFilterResetClick(object sender, EventArgs e)
        {
            grdPuAmountCheck.ClearFilter();
            CalcTotalAmount();
        }

        private void SetCondition()

        {
            // フィルタ文字列が空の場合はフィルタをクリアする
            grdPuAmountCheck.ClearFilter();

            // 入力内容に応じてフィルタを適用する

            // H注番
            if (!string.IsNullOrWhiteSpace(txtHChuban.Text))
            {
                var col = clmF注番;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = txtHChuban.Text;
            }

            // H納日(From～To)
            if (dtHNoukiFrom.HasValue && dtHNoukiTo.HasValue)
            {
                var col = clmF納日;
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
                var col = clmF納日;
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = dtHNoukiFrom.Value;
            }
            // H納日(～To)
            else if (dtHNoukiTo.HasValue)
            {
                var col = clmF納日;
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
            // 画面からリクエストオブジェクト生成
            var request = ToSaveObjects();
            // 変更処理
            if (request.Any())
            {
                await ApiHelper.UpdateAsync(this, async () =>
                {
                    return await Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.PutPurchaseBillingDetail, request);
                });
                await SearchAsync();
            }
            else
            {
                DialogHelper.WarningMessage(this, $"保存対象が0件です。{Environment.NewLine}入力可能データが全て入力されているデータが対象になります。");
            }
        }

        /// <summary>APIパラメータを作成する</summary>
        /// <returns>Apiパラメータ</returns>
        private List<PurchaseBillingDetail> ToSaveObjects()
        {
            var dataSource = grdPuAmountCheck.DataSource as BindingList<ViewPurchaseBillingDetail>;
            return dataSource
                .Where(x => !string.IsNullOrEmpty(x.M納品書番号))
                .Where(x => x.M納入日.HasValue)
                .Where(x => !string.IsNullOrEmpty(x.M注番))
                .Where(x => x.M単価.HasValue)
                .Where(x => x.M数量.HasValue)
                .Where(x => !string.IsNullOrEmpty(x.M消費税区分))
                .Select(x => new PurchaseBillingDetail()
                {
                    Hat注文番号 = txtHatOrderNo.Text,
                    仕入先コード = x.M仕入先コード ?? x.F仕入先コード,
                    仕入先コード枝番 = x.M仕入先コード枝番,
                    支払先コード = txtPuCode.Text,
                    仕入先 = x.F仕入先,
                    H注番 = x.F注番,
                    Hページ番号 = x.DenSort,
                    H行番号 = x.DenNoLine,
                    商品コード = x.F商品コード,
                    商品名 = x.F商品名,
                    H数量 = x.F数量 ?? 0,
                    H単価 = x.F単価,
                    区分 = x.M区分,
                    H伝票番号 = x.伝票番号,
                    M納日 = x.M納入日,
                    M伝票番号 = x.M伝票番号,
                    M注番 = x.M注番,
                    M数量 = x.M数量 ?? 0,
                    M単価 = x.M単価,
                    倉庫コード = x.F倉庫コード,
                    社員Id = x.F受発注者 ?? 0,
                    部門コード = x.F受発注者部門コード,
                    備考 = string.Empty,
                    仕入番号 = x.仕入番号,
                    仕入行番号 = x.仕入行番号,
                    伝区 = x.M伝区,
                    納品書番号 = x.M納品書番号,
                    子番 = x.DenNoLine,
                    消費税 = x.M消費税区分,
                    支払日 = x.M納入日,
                    照合ステータス = 
                        string.IsNullOrEmpty(x.SaveKey) || !x.仕入行番号.HasValue ? (short)0 :
                        x.F金額 != x.M金額 ? (short)1 :
                        x.F消費税率 != x.M消費税率 ? (short)1 : (short)2,
                }).ToList();
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

        /// <summary>差分チェックボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnDifferenceCheck_Click(object sender, EventArgs e)
        {
            //差分があるセルの色を変更する
            // データ行の数
            int rowCount = grdPuAmountCheck.Rows.Count;

            // 全てのセルを走査して差分を比較
            for (int i = grdPuAmountCheck.Rows.Fixed; i < rowCount; i++)
            {
                var row = grdPuAmountCheck.Rows[i];
                var dataSource = row.DataSource as ViewPurchaseBillingDetail;

                if (!dataSource.M納入日.HasValue &&
                    string.IsNullOrEmpty(dataSource.M注番) &&
                    !dataSource.M数量.HasValue &&
                    !dataSource.M数量.HasValue)
                {
                    //色を戻す
                    grdPuAmountCheck.GetCellRange(i, clmM納入日.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(i, clmM注番.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(i, clmM数量.Index).StyleNew.BackColor = SystemColors.Window;
                    grdPuAmountCheck.GetCellRange(i, clmM単価.Index).StyleNew.BackColor = SystemColors.Window;
                    continue;
                }

                // 値が異なる場合に背景色を変更
                grdPuAmountCheck.GetCellRange(i, clmM納入日.Index).StyleNew.BackColor =
                    dataSource.M納入日 == dataSource.F納日 ? SystemColors.Window :Color.Yellow;

                grdPuAmountCheck.GetCellRange(i, clmM注番.Index).StyleNew.BackColor =
                    dataSource.M注番 == dataSource.F注番 ? SystemColors.Window :Color.Yellow;

                grdPuAmountCheck.GetCellRange(i, clmM数量.Index).StyleNew.BackColor =
                    dataSource.M数量.HasValue && dataSource.F数量.HasValue && dataSource.M数量 == dataSource.F数量 ? SystemColors.Window : Color.Yellow;

                grdPuAmountCheck.GetCellRange(i, clmM単価.Index).StyleNew.BackColor =
                    dataSource.M単価 == dataSource.F単価 ? SystemColors.Window : Color.Yellow;
            }
        }

        #region 仕入登録

        /// <summary>仕入登録ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnPuEdit_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessFormCache<PU_Edit>();
            if (form == null)
            {
                // 初回起動時のみ検索条件を設定する
                form = FormFactory.GetModelessForm<PU_Edit>();
                form.PuCode = txtPuCode.Text;
                form.PuName = txtPuName.Text;
                form.HatOrderNo = txtHatOrderNo.Text;
                //form.PayDateFrom = HatFComParts.DoParseDateTime(dtPayDateFrom.Value);
                //form.PayDateTo = HatFComParts.DoParseDateTime(dtPayDateTo.Value);
                form.InitialSearch = true;
            }
            form.Show();
            form.Activate();
        }

        #endregion 仕入登録

        private void grdPuAmountCheck_Click(object sender, EventArgs e)
        {

        }


        public class TotalPrice
        {
            public string 項目 { get; set; }
            public int F合計 { get; set; }
            public int M合計 { get; set; }
        }

        private void SetPriceGrid()
        {
            // フィルタリングされた行のみ取得
            var filteredRows = grdPuAmountCheck.Rows.OfType<Row>()
                .Where(x => x.Visible)
                .Select(x => x.DataSource as ViewPurchaseBillingDetail)
                .Where(x => x != null)
                .ToList();
            filteredRows
                .GroupBy(d => d.F金額)
                .Select(d => new TotalPrice
                {

                });

        }
    }
}