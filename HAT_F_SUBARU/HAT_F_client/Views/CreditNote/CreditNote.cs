using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Models;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.CreditNote
{
    public partial class CreditNote : Form
    {
        /// <summary>権限管理</summary>
        private RoleController _roleController;

        /// <summary>U1:仕入売上訂正、R1:返品入力、R5:返品入庫 S1:売上調整</summary>
        private readonly string _approvalType = "S1";

        /// <summary>現在の承認状況</summary>
        private ApprovalStatus? CurrentStatus
            => _approvalSuite?.Approval.ApprovalStatus.HasValue == true ? (ApprovalStatus)_approvalSuite.Approval.ApprovalStatus.Value : null;

        /// <summary>現在の承認状況詳細</summary>
        private ApprovalSuite _approvalSuite;

        #region 公開プロパティ

        /// <summary>物件コード</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConstructionCode { get; set; }

        /// <summary>物件名</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConstructionName { get; set; }

        /// <summary>得意先コード</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TokuiCd
        {
            get => txtTokuiCd.Text;
            set => txtTokuiCd.Text = value;
        }

        /// <summary>得意先名</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TokuiName
        {
            get => txtTokuiName.Text;
            set => txtTokuiName.Text = value;
        }

        /// <summary>請求日</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? InvoicedDate
        {
            get => string.IsNullOrEmpty(txtInvoicedDate.Text) ? null : DateTime.ParseExact(txtInvoicedDate.Text, "yy/MM/dd", null);
            set => txtInvoicedDate.Text = value?.ToString("yy/MM/dd") ?? string.Empty;
        }

        #endregion 公開プロパティ

        #region グリッド列アクセス用プロパティ
        private Column clm物件コード => grdSalesAdjustments.Cols["物件コード"];
        private Column clm物件名 => grdSalesAdjustments.Cols["物件名"];
        private Column clm得意先コード => grdSalesAdjustments.Cols["得意先コード"];
        private Column clm得意先名 => grdSalesAdjustments.Cols["得意先名"];
        private Column clm調整区分 => grdSalesAdjustments.Cols["調整区分"];
        private Column clm摘要 => grdSalesAdjustments.Cols["摘要"];
        private Column clm勘定科目 => grdSalesAdjustments.Cols["勘定科目"];
        private Column clm調整金額 => grdSalesAdjustments.Cols["調整金額"];
        private Column clm消費税 => grdSalesAdjustments.Cols["消費税"];
        private Column clm消費税率 => grdSalesAdjustments.Cols["消費税率"];
        private Column clm請求日 => grdSalesAdjustments.Cols["請求日"];
        private Column clm社員ID => grdSalesAdjustments.Cols["社員ID"];
        private Column clm社員名 => grdSalesAdjustments.Cols["社員名"];

        private Column clmHistoryEmpName => grdApprovalHistory.Cols["EmpName"];
        private Column clmHistoryApprovalResult => grdApprovalHistory.Cols["ApprovalResult"];
        private Column clmHistoryApprovalComment => grdApprovalHistory.Cols["ApprovalComment"];

        #endregion グリッド列アクセス用プロパティ

        // TODO 排他制御関連は暫定的に封印
        public ViewPurchaseSalesCorrection Condition { get; set; } = new ViewPurchaseSalesCorrection();
        //private string lockUserName = null;
        //private string lockEditStartDateTime = null;
        //private ScreenMode currentMode = ScreenMode.NewEntry;

        //public enum ScreenMode
        //{
        //    /// <summary>新規登録</summary>
        //    NewEntry,

        //    /// <summary>編集</summary>
        //    Edit,

        //    /// <summary>閲覧</summary>
        //    ReadOnly
        //}

        /// <summary>コンストラクタ</summary>
        public CreditNote()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                // TODO: 暫定でFormStyleHelperでやっていることと同じことをする
                if (bool.TryParse(HatFConfigReader.GetAppSetting("Theme:Enabled"), out bool themaEnabled))
                {
                    if (themaEnabled)
                    {
                        blobStrageForm1.BackColor = HatFTheme.AquaColor;
                    }
                }

                _roleController = new RoleController(LoginRepo.GetInstance().CurrentUser.Roles, new Dictionary<Control, HatFUserRole[]>() {
                    { btnApproval , new [] { HatFUserRole.ApplicationSaPurchaseApproval }} ,
                    { btnRemand , new [] { HatFUserRole.ApplicationSaPurchaseApproval }} ,
                });
            }
        }

        #region メイン画面制御

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void CreditNote_Load(object sender, EventArgs e)
        {
            // デバッグ用の保存ボタンを、リリースモード時は隠す
#if !DEBUG
            btnSave.Visible = false;
            btnSave.Enabled = false;
#endif

            var approvableUsers = await ApiHelper.FetchAsync(this, async () =>
            {
                // ??:売上調整
                return await Program.HatFApiClient.GetAsync<List<Employee>>(
                    ApiResources.HatF.MasterEditor.EmloyeeUserAssignedRole,
                    new { userRoleCd = "11" });
            });
            if (approvableUsers.Failed)
            {
                Close();
            }

            // グリッドの調整区分をコンボボックスにする
            clm調整区分.DataMap = JsonResources.SalesAdjustmentCategories
                .ToDictionary(x => x.Key, x => $"{x.Key}:{x.Value}");

            // 承認履歴グリッドの承認ステータス
            clmHistoryApprovalResult.DataMap = Enum.GetValues(typeof(ApprovalResult)).OfType<ApprovalResult>()
                .ToDictionary(x => (int)x, x => EnumUtil.GetDescription(x));

            // 承認者のコンボボックス設定
            InitializeComboBoxAsync(approvableUsers.Value);

            if (!await SearchAsync())
            {
                Close();
            }
            blobStrageForm1.Init($"SalesAdjustment_{TokuiCd}_{InvoicedDate:yyyyMMdd}");
        }

        /// <summary>画面終了時</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void CreditNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO 排他制御関連は暫定的に封印
            //if (currentMode == ScreenMode.Edit)
            //{
            //    if (grdSalesAdjustments.Rows.Count > 1)
            //    {
            //        await AmountCheckUnlockAsync();
            //    }
            //}
        }

        /// <summary>承認者のコンボボックスを設定する</summary>
        /// <param name="roleUsers">承認可能社員</param>
        private void InitializeComboBoxAsync(List<Employee> roleUsers)
        {
            // コンボボックスにバインド
            cmbAuthorizer.DataSource = roleUsers;
            cmbAuthorizer.DisplayMember = nameof(Employee.EmpName);
            cmbAuthorizer.ValueMember = nameof(Employee.EmpId);
            // 最初の項目を選択
            cmbAuthorizer.SelectedIndex = -1;

            cmbAuthorizer2.DataSource = roleUsers;
            cmbAuthorizer2.DisplayMember = nameof(Employee.EmpName);
            cmbAuthorizer2.ValueMember = nameof(Employee.EmpId);
            // 最初の項目を選択
            cmbAuthorizer2.SelectedIndex = -1;
        }

        /// <summary>検索実行</summary>
        /// <returns>成否</returns>
        private async Task<bool> SearchAsync()
        {
            // 売上調整一覧を取得
            var salesAdjustments = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<ViewSalesAdjustment>>(ApiResources.HatF.Client.SalesAdjustment, new
                {
                    TokuiCd,
                    InvoicedDateFrom = InvoicedDate,
                    InvoicedDateTo = InvoicedDate,
                });
            });
            if (salesAdjustments.Failed)
            {
                return false;
            }

            // 日単位で申請、承認するため、売上調整一覧のすべての行に同じ承認要求番号が設定されている
            ApprovalSuite approvalSuite = null;
            if (!string.IsNullOrEmpty(salesAdjustments.Value.FirstOrDefault()?.承認要求番号))
            {
                var url = string.Format(ApiResources.HatF.Approval.Get, salesAdjustments.Value.First().承認要求番号);
                var approvalSuiteResult = await ApiHelper.FetchAsync(this, async () =>
                {
                    return await Program.HatFApiClient.GetAsync<ApprovalSuite>(url);
                });
                if (approvalSuiteResult.Failed)
                {
                    return false;
                }
                approvalSuite = approvalSuiteResult.Value;
            }

            UpdateScreen(salesAdjustments.Value, approvalSuite);
            return true;
        }

        /// <summary>画面表示内容を更新する</summary>
        /// <param name="salesAdjustments">売上調整データ</param>
        /// <param name="approvalSuite">承認情報</param>
        private void UpdateScreen(IList<ViewSalesAdjustment> salesAdjustments, ApprovalSuite approvalSuite)
        {
            // 売上調整一覧をグリッドにバインドする
            grdSalesAdjustments.DataSource = new BindingList<ViewSalesAdjustment>(salesAdjustments);
            grdSalesAdjustments.AutoSizeCols();

            // 合計金額の算出
            CalcTotalAmount();

            _approvalSuite = approvalSuite;

            // 承認系コントロールに値をセット
            txtApprovalStatus.Text = CurrentStatus.HasValue ? EnumUtil.GetDescription(CurrentStatus.Value) : string.Empty;
            txtComment.Text = string.Empty;
            grdApprovalHistory.DataSource = approvalSuite?.ApprovalProcedures;
            SelectAuthorizer(cmbAuthorizer, approvalSuite?.Approval.Approver1EmpId);
            SelectAuthorizer(cmbAuthorizer2, approvalSuite?.Approval.Approver2EmpId);

            // 申請中や承認済みは編集不可。ステータス無か差し戻された場合のみ編集可
            var canEdit = !CurrentStatus.HasValue;
            grdSalesAdjustments.AllowEditing = canEdit;
            grdSalesAdjustments.AllowAddNew = canEdit;
            grdSalesAdjustments.AllowDelete = canEdit;

            EnableControls();
        }

        /// <summary>承認者のコンボボックスを選択する</summary>
        /// <param name="comboBox">対象コンボボックス</param>
        /// <param name="employeeId">社員ID</param>
        private void SelectAuthorizer(ComboBox comboBox, int? employeeId)
        {
            var employees = comboBox.DataSource as List<Employee>;
            var employee = employees.FirstOrDefault(x => x.EmpId == employeeId);
            comboBox.SelectedItem = employee;
        }

        /// <summary>各コントロールのEnable状態を更新する</summary>
        private void EnableControls()
        {
            var dataSource = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            var currentEmployeeId = LoginRepo.GetInstance().CurrentUser.EmployeeId;

            // 申請ボタンは、グリッドに行があって承認者が2人選択されていなければ押せない
            // 承認後は再申請も可能
            btnApplication.Enabled = dataSource?.Any() == true && 
                cmbAuthorizer.SelectedItem != null && cmbAuthorizer2.SelectedItem != null &&
                (!CurrentStatus.HasValue || CurrentStatus == ApprovalStatus.FinalApprove);

            // 差戻ボタンは申請中または承認中で、承認者が自分でなければ押せない
            btnRemand.Enabled =
                CurrentStatus == ApprovalStatus.Request || CurrentStatus == ApprovalStatus.Approve &&
                _approvalSuite?.Approval.Approver1EmpId == currentEmployeeId;

            btnApproval.Enabled =
                // 申請中の場合、承認ボタンは最終承認者が選択されていて、承認者が自分でなければ押せない
                CurrentStatus == ApprovalStatus.Request ? cmbAuthorizer2.SelectedItem != null && _approvalSuite?.Approval.Approver1EmpId == currentEmployeeId :
                // 承認中の場合、最終承認者が自分でなければ押せない
                CurrentStatus == ApprovalStatus.Approve ? _approvalSuite?.Approval.Approver2EmpId == currentEmployeeId :
                false;

            // 承認者2人は、未申請の状態でなければ選択できない
            cmbAuthorizer.Enabled = !CurrentStatus.HasValue || CurrentStatus == ApprovalStatus.FinalApprove;
            cmbAuthorizer2.Enabled = !CurrentStatus.HasValue || CurrentStatus == ApprovalStatus.FinalApprove;

            // ファイルアップロードと削除は申請前と承認済みの場合のみ可能
            // ダウンロードはいつでもできる
            blobStrageForm1.CanUpload = !CurrentStatus.HasValue || CurrentStatus == ApprovalStatus.FinalApprove;
            blobStrageForm1.CanDelete = !CurrentStatus.HasValue || CurrentStatus == ApprovalStatus.FinalApprove;
        }

        /// <summary>合計金額を算出する</summary>
        private void CalcTotalAmount()
        {
            var dataSource = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            txtTotalAmount.Text = string.Format($"{dataSource.Sum(x => x.調整金額):C0}");
        }

        /// <summary>キャンセルボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>承認者/最終承認者のコンボボックス変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void CmbAuthorizer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnableControls();
        }

        #endregion メイン画面制御

        #region グリッド制御

        /// <summary>行追加</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdSalesAdjustments_AfterAddRow(object sender, RowColEventArgs e)
        {
            // 新規行の初期値をセットする
            var dataSource = grdSalesAdjustments.Rows[e.Row].DataSource as ViewSalesAdjustment;
            dataSource.物件コード = ConstructionCode;
            dataSource.物件名 = ConstructionName;
            dataSource.得意先コード = TokuiCd;
            dataSource.得意先名 = TokuiName;
            dataSource.請求日 = InvoicedDate;

            EnableControls();
        }

        /// <summary>行削除前</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdSalesAdjustments_BeforeDeleteRow(object sender, RowColEventArgs e)
        {
            var dataSource = grdSalesAdjustments.Rows[e.Row].DataSource as ViewSalesAdjustment;
            if (dataSource == null)
            {
                return;
            }
            // 既存レコードは削除させない
            if (!string.IsNullOrEmpty(dataSource.売上調整番号))
            {
                DialogHelper.WarningMessage(this, "1度申請された情報は削除できません。");
                e.Cancel = true;
            }
        }

        /// <summary>行削除後</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdSalesAdjustments_AfterDeleteRow(object sender, RowColEventArgs e)
        {
            CalcTotalAmount();
            EnableControls();
        }

        /// <summary>セル編集</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdSalesAdjustments_AfterEdit(object sender, RowColEventArgs e)
        {
            var dataSource = grdSalesAdjustments.Rows[e.Row].DataSource as ViewSalesAdjustment;
            if (e.Col == clm消費税.Index)
            {
                var tax = ClientRepo.GetInstance().Options.DivTaxRates
                    .FirstOrDefault(x => x.TaxRateCd == dataSource.消費税);
                dataSource.消費税率 = tax?.TaxRate;
                grdSalesAdjustments.Invalidate();
            }
            else if (e.Col == clm調整金額.Index)
            {
                CalcTotalAmount();
            }
        }

        #endregion グリッド制御

        #region 承認

        /// <summary>承認ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnAapproval_Click(object sender, EventArgs e)
        {
            if (!DialogHelper.YesNoQuestion(this, "承認してよろしいですか？", true))
            {
                return;
            }
            if (await ApprovalAsync())
            {
                DialogHelper.InformationMessage(this, "承認が完了しました。");
            }
        }

        /// <summary>差し戻しボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnRemand_Click(object sender, EventArgs e)
        {
            if (!DialogHelper.YesNoQuestion(this, "差し戻してよろしいですか？", true))
            {
                return;
            }
            if (await RemandAsync())
            {
                DialogHelper.InformationMessage(this, "差し戻しが完了しました。");
            }
        }

        /// <summary>申請ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnApplication_Click(object sender, EventArgs e)
        {
            if (!DialogHelper.YesNoQuestion(this, "申請してよろしいですか？", true))
            {
                return;
            }
            if (await ApplicationAsync())
            {
                DialogHelper.InformationMessage(this, "申請しました。");
            }
        }

        /// <summary>申請処理</summary>
        /// <returns>成否</returns>
        private async Task<bool> ApplicationAsync()
        {
            // 申請
            var dataSource = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            var approvalId = dataSource.First().承認要求番号;
            var comment = txtComment.Text;
            var approverId = (cmbAuthorizer.SelectedItem as Employee).EmpId;
            var approverId2 = (cmbAuthorizer2.SelectedItem as Employee).EmpId;
            var approvalSuite = await ApiHelper.UpdateAsync(this, async () =>
            {
                var url = string.IsNullOrEmpty(approvalId) ?
                    string.Format(ApiResources.HatF.Approval.Put, _approvalType) :
                    string.Format(ApiResources.HatF.Approval.Update, approvalId, _approvalType);
                return await Program.HatFApiClient.PutAsync<ApprovalSuite>(url, new ApprovalRequest()
                {
                    EmpId = LoginRepo.GetInstance().CurrentUser.EmployeeId,
                    RequestType = (int)ApprovalResult.Request,
                    Approver1EmpId = approverId,
                    Approver2EmpId = approverId2,
                    ApprovalComment = comment,
                    ViewSalesAdjustments = dataSource.ToList(),
                });
            }, true);
            if (approvalSuite.Failed)
            {
                return false;
            }

            // 最新情報を表示するために再検索
            if (!await SearchAsync())
            {
                return false;
            }
            return true;
        }

        /// <summary>差し戻し処理</summary>
        /// <returns>成否</returns>
        private async Task<bool> RemandAsync()
        {
            // 日単位で申請、承認するため、売上調整一覧のすべての行に同じ承認要求番号が設定されている
            var dataSource = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            var approvalId = dataSource.First().承認要求番号;

            var comment = txtComment.Text;
            var url = string.Format(ApiResources.HatF.Approval.Update, approvalId, _approvalType);
            var approvalSuite = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<ApprovalSuite>(url, new ApprovalRequest()
                {
                    EmpId = LoginRepo.GetInstance().CurrentUser.EmployeeId,
                    RequestType = (int)ApprovalResult.Reject,
                    ApprovalComment = comment,
                });
            }, true);
            if (approvalSuite.Failed)
            {
                return false;
            }

            // 最新情報を表示するために再検索
            if (!await SearchAsync())
            {
                return false;
            }
            return true;
        }

        /// <summary>承認処理</summary>
        /// <returns>成否</returns>
        private async Task<bool> ApprovalAsync()
        {
            // 日単位で申請、承認するため、売上調整一覧のすべての行に同じ承認要求番号が設定されている
            var dataSource = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            var approvalId = dataSource.First().承認要求番号;

            var comment = txtComment.Text;
            var url = string.Format(ApiResources.HatF.Approval.Update, approvalId, _approvalType);
            var requestType = CurrentStatus == ApprovalStatus.Approve ? ApprovalResult.FinalApprove : ApprovalResult.Approve;

            var approvalSuite = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<ApprovalSuite>(url, new ApprovalRequest()
                {
                    EmpId = LoginRepo.GetInstance().CurrentUser.EmployeeId,
                    RequestType = (int)requestType,
                    ApprovalComment = comment,
                });
            }, true);
            if (approvalSuite.Failed)
            {
                return false;
            }

            // 最新情報を表示するために再検索
            if (!await SearchAsync())
            {
                return false;
            }
            return true;
        }
        #endregion 承認

        #region 排他制御(暫定的に封印)

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            //if (DialogHelper.YesNoQuestion(this, "読み取り専用を解除しますか？"))
            //{
            //    await AmountCheckUnlockAsync();
            //    await AmountCheckLockAndSettingAsync();
            //}
        }

        /// <summary>
        /// 読み取り設定
        /// </summary>
        private void ReadSetting()
        {
            //lblScreenMode.ForeColor = System.Drawing.Color.Red;
            //lblScreenMode.Text = "赤黒登録（読み取り専用）";
            //lblLockInfo.Text = $"編集者：{lockUserName}{Environment.NewLine}編集開始日時：{lockEditStartDateTime}";
            //this.currentMode = ScreenMode.ReadOnly;
            //this.btnContact.Enabled = false;
            //lblLockInfo.Visible = true;
            //btnUnlock.Visible = true;
        }

        /// <summary>
        /// 編集設定
        /// </summary>
        private void EditSetting()
        {
            //lblScreenMode.ForeColor = System.Drawing.SystemColors.WindowText;
            //lblLockInfo.Visible = false;
            //btnUnlock.Visible = false;
            //lblScreenMode.Text = "売上調整";
            //currentMode = ScreenMode.Edit;
            //this.btnContact.Enabled = true;
        }

        /// <summary>
        /// 仕入金額照合アンロック機能
        /// </summary>
        private async Task<bool> AmountCheckUnlockAsync()
        {
            //var result = await Program.HatFApiClient.PostAsync<bool>(ApiResources.HatF.Client.SalesEditUnLock, new Dictionary<string, object>()
            //    {
            //                { "hatOrderNo", Condition.Hat注文番号}
            //    });
            //return result.Data;

            //return true;
            return await Task.FromResult(true);// warning除去のため一時的な行なので削除してください
        }

        private async Task AmountCheckLockAndSettingAsync()
        {
            await Task.CompletedTask; 　// warning除去のため一時的な行なので削除してください

            //var result = await AmountCheckLock();
            //if (result.Count == 0)
            //{
            //    EditSetting();
            //}
            //else
            //{
            //    lockUserName = result["emp_name"];
            //    lockEditStartDateTime = result["edit_start_datetime"];
            //    ReadSetting();
            //}
        }

        /// <summary>
        /// 仕入金額照合ロック機能
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> AmountCheckLockAsync()
        {
            var result = await Program.HatFApiClient.PostAsync<Dictionary<string, string>>
                (ApiResources.HatF.Client.SalesEditLock, new Dictionary<string, object>()
                {
                    { "hatOrderNo", Condition.Hat注文番号},
                    { "empid",LoginRepo.GetInstance().CurrentUser.EmployeeCode }
                });
            return result.Data;
        }

        #endregion 排他制御(暫定的に封印)

        #region 担当者へ連絡

        /// <summary>担当者へ連絡ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnContact_Click(object sender, EventArgs e)
        {
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog(this);
            }
        }

        #endregion 担当者へ連絡

        #region デバッグ

        /// <summary>[デバッグ用]保存ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!DialogHelper.YesNoQuestion(this, "保存してよろしいですか？", true))
            {
                return;
            }

            var salesAdjustments = grdSalesAdjustments.DataSource as BindingList<ViewSalesAdjustment>;
            // 売上調整データをDB登録する
            var result = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<List<ViewSalesAdjustment>>(
                    ApiResources.HatF.Client.SalesAdjustment,
                    salesAdjustments.ToList());
            });
            if (result.Failed)
            {
                return;
            }
            // 最新情報をバインドする
            await SearchAsync();
        }

        #endregion デバッグ
    }
}