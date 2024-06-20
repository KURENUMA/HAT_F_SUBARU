using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.CustomControls;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HatFClient.Views.Sales.SalesEdit;

namespace HatFClient.Views.Sales
{
    public partial class SalesEdit : Form
    {
        private PurchaseRepo _purchaseRepo;
        private RoleController _roleController;
        private readonly string approvalType = "U1"; //U1:仕入売上訂正、R1:返品入力、R5:返品入庫 

        private Dictionary<short, string> checkStatus = new Dictionary<short, string>() {
            { 0,"未確認"},
            { 1,"編集中"},
            { 2,"確認済"},
            { 3,"違算"},
            { 4,"未決"},
            { 5,"未請求"},
        };
        private Dictionary<short, string> approvalResult = new Dictionary<short, string>() {
            { 0,"申請"},
            { 1,"差し戻し"},
            { 2,"承認済"},
            { 3,"最終承認済"},
        };
        public enum ApprovalResult
        {
            // 申請
            Request = 0,
            // 差し戻し
            Reject = 1,
            // 承認
            Approve = 2,
            // 最終承認
            FinalApprove = 3
        }

        public class ApprovalRequestEx
        {
            public ApprovalRequest request;
            public string approvalReqNo = null; //承認要求番号
        }


        public ViewPurchaseSalesCorrection Condition { get; set; } = new ViewPurchaseSalesCorrection();
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

        public SalesEdit()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                _purchaseRepo = PurchaseRepo.GetInstance();
                _roleController = new RoleController(LoginRepo.GetInstance().CurrentUser.Roles, new Dictionary<Control, HatFUserRole[]>() {
                    { btnAapproval , new [] { HatFUserRole.ApplicationSaPurchaseApproval }} ,
                    { btnRemand , new [] { HatFUserRole.ApplicationSaPurchaseApproval }} ,
                });
            }
        }

        private async Task InitializeComboAsync()
        {
            var response = await Program.HatFApiClient.GetAsync<List<Employee>>(ApiResources.HatF.MasterEditor.EmloyeeUserAssignedRole, new { userRoleCd = "11"}); //11:売上額訂正承認権限

            // コンボボックスにバインド
            CmbAuthorizer.DataSource = response.Data;
            CmbAuthorizer.DisplayMember = "EmpName";
            CmbAuthorizer.ValueMember = "EmpId";
            CmbAuthorizer.SelectedIndex = -1; // 最初の項目を選択

            CmbAuthorizer2.DataSource = response.Data;
            CmbAuthorizer2.DisplayMember = "EmpName";
            CmbAuthorizer2.ValueMember = "EmpId";
            CmbAuthorizer2.SelectedIndex = -1; // 最初の項目を選択
        }

        private void SetCondition()
        {
            // フィルタ文字列が空の場合はフィルタをクリアする
            c1FlexGridSalesEdit.ClearFilter();

            // 入力内容に応じてフィルタを適用する
            if (!string.IsNullOrWhiteSpace(textBoxPU_CODE.Text))
            {
                var col = c1FlexGridSalesEdit.Cols["仕入先コード"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textBoxPU_CODE.Text;
            }

            if (!string.IsNullOrWhiteSpace(textBoxPU_NAME.Text))
            {
                var col = c1FlexGridSalesEdit.Cols["仕入先"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textBoxPU_NAME.Text;
            }

            if (!string.IsNullOrWhiteSpace(textboxH_NUMBER.Text))
            {
                var col = c1FlexGridSalesEdit.Cols["H注番"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.Contains;
                cf.Condition1.Parameter = textboxH_NUMBER.Text;
            }

            if (FromDate.Value != DBNull.Value && ToDate.Value != DBNull.Value)
            {
                var col = c1FlexGridSalesEdit.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = FromDate.Value;
                cf.Condition2.Operator = ConditionOperator.LessThanOrEqualTo;   
                cf.Condition2.Parameter = ToDate.Value;
            }
            else if (FromDate.Value != DBNull.Value)
            {
                var col = c1FlexGridSalesEdit.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.GreaterThanOrEqualTo;
                cf.Condition1.Parameter = FromDate.Value;
            }
            else if (ToDate.Value != DBNull.Value)
            {
                var col = c1FlexGridSalesEdit.Cols["H納日"];
                col.AllowFiltering = AllowFiltering.ByCondition;
                ConditionFilter cf = col.Filter as ConditionFilter;
                cf.Condition1.Operator = ConditionOperator.LessThanOrEqualTo;
                cf.Condition1.Parameter = ToDate.Value;
            }
            // フィルタを適用する
            c1FlexGridSalesEdit.ApplyFilters();

        }

        private async Task UpdateListAsync()
        {
            if (Condition.Hat注文番号 != null)
            {
                var result = await _purchaseRepo.GetDetail(Condition);
                c1FlexGridSalesEdit.DataSource = result;
                if (c1FlexGridSalesEdit.Rows.Count > 1)
                {
                    await AmountCheckLockAndSettingAsync();
                    await SetApprovalDataAsync();
                    blobStrageForm1.Init("SalesEdit_" + Condition.Hat注文番号);
                }
            }
        }

        //承認データにおける画面制御
        private async Task SetApprovalDataAsync()
        {
            string approvalReqNo = null;
            for (var i = 1; i < c1FlexGridSalesEdit.Rows.Count; i++)
            {
                //行単位でどれかのデータが入力されていることを保存対象とする
                var r = c1FlexGridSalesEdit.Rows[i].DataSource as ViewPurchaseSalesCorrection;
                if (!String.IsNullOrEmpty(r.承認要求番号?.ToString()))
                {
                    approvalReqNo = r.承認要求番号;
                }
            }
            if(approvalReqNo != null)
            {
                var url = String.Format(ApiResources.HatF.Approval.Put, approvalReqNo);
                var response = await Program.HatFApiClient.GetAsync<ApprovalSuite>(url); //承認状況を取得
                if (response.Data.Approval == null)
                {
                    return;
                }
                if (response.Data.Approval.Approver1EmpId != null)
                {
                    CmbAuthorizer.SelectedValue = (int)response.Data.Approval.Approver1EmpId;
                }
                CmbAuthorizer2.SelectedValue = (int)response.Data.Approval.FinalApproverEmpId;
                //承認状況によるボタン制御
                if(response.Data.Approval.ApprovalStatus == 0)
                {
                    btnApplication.Enabled = false;
                    btnAapproval.Enabled = true; ;
                    btnRemand.Enabled = true;
                    CmbAuthorizer.Enabled = false;
                    CmbAuthorizer2.Enabled = false;
                    c1FlexGridSalesEdit.AllowEditing = false;
                    txtApprovalStatus.Text = "申請中";
                }
                else if (response.Data.Approval.ApprovalStatus == 1)
                {
                    btnApplication.Enabled = false;
                    btnAapproval.Enabled = true; ;
                    btnRemand.Enabled = true;
                    CmbAuthorizer.Enabled = false;
                    CmbAuthorizer2.Enabled = false;
                    c1FlexGridSalesEdit.AllowEditing = false;
                    txtApprovalStatus.Text = "承認中";
                }
                else
                {
                    if(response.Data.Approval.ApprovalStatus == 9)
                    {
                        txtApprovalStatus.Text = "承認済み";
                    }
                    else
                    {
                        txtApprovalStatus.Text = "";
                    }
                    CmbAuthorizer.Enabled = true;
                    CmbAuthorizer2.Enabled = true;
                    btnApplication.Enabled = true;
                    btnAapproval.Enabled = false; ;
                    btnRemand.Enabled = false;
                    c1FlexGridSalesEdit.AllowEditing = true;
                }
                //gridDataを更新ＴＯＤＯ社員ＩＤを社員名にできるようにする
                c1FlexGrid1.Cols["ApprovalResult"].DataType = typeof(int);
                c1FlexGrid1.Cols["ApprovalResult"].DataMap = approvalResult;
                c1FlexGrid1.DataSource = response.Data.ApprovalProcedures;
            }
            else
            {
                txtApprovalStatus.Text = "";
                CmbAuthorizer.Enabled = true;
                CmbAuthorizer2.Enabled = true;
                btnApplication.Enabled = true;
                btnAapproval.Enabled = false; ;
                btnRemand.Enabled = false;
                c1FlexGridSalesEdit.AllowEditing = true;
            }
        }

        #region <画面アクション>
        private async void SalesEdit_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            c1FlexGridSalesEdit.Cols["M照合ステータス"].DataType = typeof(short);
            c1FlexGridSalesEdit.Cols["M照合ステータス"].DataMap = checkStatus;
            await InitializeComboAsync();
            if (Condition.Hat注文番号 != null)
            {
                await UpdateListAsync();
            }
            else
            {
                c1FlexGridSalesEdit.DataSource = new List<ViewPurchaseSalesCorrection>();
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
            if(CmbAuthorizer.SelectedIndex ==  -1 || CmbAuthorizer.SelectedIndex == -1)
            {
                return false;
            }
                return true;
        }

        private ApprovalRequestEx ToApprovalObjects(ApprovalResult requestType)
        {
            //ViewPurchaseSalesCorrectionからApprovalRequestを作成
            ApprovalRequestEx approvalEx = new ApprovalRequestEx();
            ApprovalRequest approval = new ApprovalRequest();
            approval.EmpId = LoginRepo.GetInstance().CurrentUser.EmployeeId;
            approval.RequestType = ((int)requestType);
            approval.ApprovalComment = txtComment.Text;
            approval.Approver1EmpId = int.Parse(CmbAuthorizer.SelectedValue.ToString());
            //approval.Approver2EmpId = CmbAuthorizer.SelectedIndex;
            approval.FinalApproverEmpId = int.Parse(CmbAuthorizer2.SelectedValue.ToString()) ;
            List<PurchaseSalesCorrection> target = new List<PurchaseSalesCorrection>();

            for (var i = 1; i < c1FlexGridSalesEdit.Rows.Count; i++)
            {
                //行単位でどれかのデータが入力されていることを保存対象とする
                var r = c1FlexGridSalesEdit.Rows[i].DataSource as ViewPurchaseSalesCorrection;
                if (!String.IsNullOrEmpty(r.承認要求番号?.ToString()))
                {
                    approvalEx.approvalReqNo = r.承認要求番号;
                }
                if (requestType == ApprovalResult.Request)
                {
                    if (!String.IsNullOrEmpty(r.変更後得意先?.ToString()) || !String.IsNullOrEmpty(r.変更後仕入先?.ToString()) || !String.IsNullOrEmpty(r.変更後仕入先枝番?.ToString())
                        || !String.IsNullOrEmpty(r.変更後h数量?.ToString()) || !String.IsNullOrEmpty(r.変更後h単価?.ToString())
                          || !String.IsNullOrEmpty(r.変更後m数量?.ToString()) || !String.IsNullOrEmpty(r.変更後m単価?.ToString()))
                    {
                        target.Add(new PurchaseSalesCorrection()
                        {
                            ApprovalTargetId = r.承認対象id, // 承認対象ID
                            ApprovalTargetSub = r.承認対象枝番, // 承認対象枝番
                            PuNo = r.仕入番号, // 仕入番号
                            PuRowNo = r.仕入行番号, // 仕入行番号
                            SupCode = r.変更後仕入先, // 仕入先コード
                            SupSubNo = r.変更後仕入先枝番, // 仕入先枝番
                            PoPrice = r.変更後m単価, // 仕入単価（M単価）
                            PuQuantity = r.変更後m数量, // 仕入数量（M数量）
                            SalesNo = r.売上番号, // 売上番号
                            RowNo = r.売上行番号, // 売上行番号
                            Quantity = r.変更後h数量, // 売上数量(H数量？)
                            Unitprice = r.変更後h単価, // 販売単価（H単価？）
                            CompCode = r.変更後得意先 // 取引先コード（得意先コード？）
                        });
                    }
                }
            }
            if (target.Count != 0)
            {
                approval.PuSalesCorrection = target;
            }
            else
            {
                approval.PuSalesCorrection = null;
            }
            approvalEx.request = approval;

            return approvalEx;
        }

        /// <summary>
        /// 申請処理
        /// </summary>
        private async Task ApplicationAsync()
        {
            if (CheckInputs())
            {
                // 画面からリクエストオブジェクト生成(申請時)
                var approvalRequestEx = ToApprovalObjects(ApprovalResult.Request);
                // 変更処理
                if (approvalRequestEx.request.PuSalesCorrection != null)
                {
                    if (approvalRequestEx.approvalReqNo != null)
                    {
                        //更新
                        var url = String.Format(ApiResources.HatF.Approval.Update, approvalRequestEx.approvalReqNo, approvalType); 
                        await ApiHelper.UpdateAsync(this, () =>
                            Program.HatFApiClient.PutAsync<ApprovalSuite>(url, approvalRequestEx.request)
                        );
                    }
                    else
                    {
                        //新規
                        var url = String.Format(ApiResources.HatF.Approval.Put, approvalType); 
                        await ApiHelper.UpdateAsync(this, () =>
                            Program.HatFApiClient.PutAsync<ApprovalSuite>(url, approvalRequestEx.request)
                        );
                    }
                }
                else
                {
                    DialogHelper.WarningMessage(this, "申請対象が０件です。");
                }
            }
            else
            {
                DialogHelper.WarningMessage(this, "承認者を設定してください。");
            }
        }
        /// <summary>
        /// 承認処理
        /// </summary>
        private async Task ApprovalAsync()
        {
            // 画面からリクエストオブジェクト生成(承認時)
            ApprovalResult approvalResult;
            if(int.Parse(CmbAuthorizer.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId && int.Parse(CmbAuthorizer2.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
            {
                approvalResult = ApprovalResult.FinalApprove;
            }
            else if (int.Parse(CmbAuthorizer.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
            {
                approvalResult = ApprovalResult.Approve;
            }
            else if(int.Parse(CmbAuthorizer2.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
            {
                approvalResult = ApprovalResult.FinalApprove;
            }
            else
            {
                approvalResult = ApprovalResult.Approve;
            }
            var approvalRequestEx = ToApprovalObjects(approvalResult);
            // 変更処理
            if (approvalRequestEx.approvalReqNo != null)
            {
                //更新
                var url = String.Format(ApiResources.HatF.Approval.Update, approvalRequestEx.approvalReqNo, approvalType); 
                await ApiHelper.UpdateAsync(this, () =>
                    Program.HatFApiClient.PutAsync<ApprovalSuite>(url, approvalRequestEx.request)
                );
            }
            else
            {
                DialogHelper.WarningMessage(this, "承認対象が存在しません。");
            }
        }

        /// <summary>
        /// 差し戻し処理
        /// </summary>
        private async Task RemandAsync()
        {
            // 画面からリクエストオブジェクト生成(承認時)
            var approvalRequestEx = ToApprovalObjects(ApprovalResult.Reject);
            // 変更処理
            if (approvalRequestEx.approvalReqNo != null)
            {
                //更新
                var url = String.Format(ApiResources.HatF.Approval.Update, approvalRequestEx.approvalReqNo, approvalType); //承認種別 0:
                await ApiHelper.UpdateAsync(this, () =>
                    Program.HatFApiClient.PutAsync<ApprovalSuite>(url, approvalRequestEx.request)
                );
            }
            else
            {
                DialogHelper.WarningMessage(this, "差し戻し対象が存在しません。");
            }
        }
        #endregion

        private void buttonCALCU_Click(object sender, EventArgs e)
        {
            // フィルタリングされた行の合計を計算する
            double totalAmountH = 0;
            double totalAmountM = 0;
            double totalAfterAmountH = 0;
            double totalAfterAmountM = 0;
            for (int i = 1; i < c1FlexGridSalesEdit.Rows.Count; i++) // 1行目はヘッダーなのでスキップする
            {
                if (c1FlexGridSalesEdit.Rows[i].Visible) // フィルタリングされた行だけを考慮する
                {
                    //H金額
                    string valueH = c1FlexGridSalesEdit.GetDataDisplay(i, c1FlexGridSalesEdit.Cols["H金額"].Index);
                    if (!string.IsNullOrEmpty(valueH))
                    {
                        if (valueH.StartsWith("-"))
                        {
                            valueH = valueH.Substring(2);
                        }
                        else
                        {
                            valueH = valueH.Substring(1);
                        }
                    }
                    //変更後H金額
                    string afterValueH = c1FlexGridSalesEdit.GetDataDisplay(i, c1FlexGridSalesEdit.Cols["変更後h金額"].Index);
                    if (!string.IsNullOrEmpty(afterValueH))
                    {
                        if (afterValueH.StartsWith("-"))
                        {
                            afterValueH = afterValueH.Substring(2);
                        }
                        else
                        {
                            afterValueH = afterValueH.Substring(1);
                        }
                    }
                    //M金額
                    string valueM = c1FlexGridSalesEdit.GetDataDisplay(i, c1FlexGridSalesEdit.Cols["M金額"].Index);
                    if (!string.IsNullOrEmpty(valueM))
                    {
                        if (valueM.StartsWith("-"))
                        {
                            valueM = valueM.Substring(2);
                        }
                        else
                        {
                            valueM = valueM.Substring(1);
                        }
                    }
                    //変更後M金額
                    string afterValueM = c1FlexGridSalesEdit.GetDataDisplay(i, c1FlexGridSalesEdit.Cols["変更後m金額"].Index);
                    if (!string.IsNullOrEmpty(afterValueM))
                    {
                        if (afterValueM.StartsWith("-"))
                        {
                            afterValueM = afterValueM.Substring(2);
                        }
                        else
                        {
                            afterValueM = afterValueM.Substring(1);
                        }
                    }

                    // H金額合計の処理
                    if (double.TryParse(valueH, out double amountH))
                    {
                        totalAmountH += amountH;
                    }

                    // M金額合計の処理
                    if (double.TryParse(valueM, out double amountM))
                    {
                        totalAmountM += amountM;
                    }

                    //変更後H金額合計の処理
                    if(string.IsNullOrEmpty(afterValueH))
                    {
                        if (double.TryParse(valueH, out double afterAmountH))
                        {
                            totalAfterAmountH += afterAmountH;
                        }
                    }
                    else
                    {
                        if (double.TryParse(afterValueH, out double afterAmountH))
                        {
                            totalAfterAmountH += afterAmountH;
                        }
                    }

                    //変更後M金額合計の処理
                    if (string.IsNullOrEmpty(afterValueM))
                    {
                        if (double.TryParse(valueM.ToString(), out double afterAmountM))
                        {
                            totalAfterAmountM += afterAmountM;
                        }
                    }
                    else
                    {
                        if (double.TryParse(afterValueM.ToString(), out double afterAmountM))
                        {
                            totalAfterAmountM += afterAmountM;
                        }
                    }
                }
            }
            textBoxH_TOTAL.Text = totalAmountH.ToString("C0");
            textBoxM_TOTAL.Text = totalAmountM.ToString("C0");
            textBoxAfterH_TOTAL.Text = totalAfterAmountH.ToString("C0");
            textBoxAfterM_TOTAL.Text = totalAfterAmountM.ToString("C0");
            textBoxHDIFF.Text = (totalAfterAmountH - totalAmountH).ToString("C0");
            textBoxMDIFF.Text = (totalAfterAmountM - totalAmountM).ToString("C0");

            if ((totalAfterAmountH - totalAmountH) < 0)
            {
                textBoxHDIFF.ForeColor = Color.Red;
            }
            else
            {
                textBoxHDIFF.ForeColor = SystemColors.WindowText;
            }
            if ((totalAfterAmountM - totalAmountM) < 0)
            {
                textBoxMDIFF.ForeColor = Color.Red;
            }
            else
            {
                textBoxMDIFF.ForeColor = SystemColors.WindowText;
            }
        }

        private void button_FILTER_RESET_Click(object sender, EventArgs e)
        {
            c1FlexGridSalesEdit.ClearFilter();
        }


        private void c1FlexGridSalesEdit_AfterEdit(object sender, RowColEventArgs e)
        {
            // 編集されたセルの情報を取得
            int editedRow = c1FlexGridSalesEdit.Row;
            int editedCol = c1FlexGridSalesEdit.Col;

            // 編集されたセルが数量列または単価列であるかを確認
            if (c1FlexGridSalesEdit.Cols[editedCol].Name == "変更後m数量" || c1FlexGridSalesEdit.Cols[editedCol].Name == "変更後m単価" ||
                c1FlexGridSalesEdit.Cols[editedCol].Name == "変更後h数量" || c1FlexGridSalesEdit.Cols[editedCol].Name == "変更後h単価")
            {
                // 数量と単価が入力されているセルの値を取得
                //変更後M数量
                string quantityAfterValueM = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["変更後m数量"].Index);
                if (!string.IsNullOrEmpty(quantityAfterValueM) && quantityAfterValueM.ToString().StartsWith("-"))
                {
                    quantityAfterValueM = quantityAfterValueM.Substring(1);
                }

                //変更後M単価
                string unitPriceAfterValueM = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["変更後m単価"].Index);
                if (!string.IsNullOrEmpty(unitPriceAfterValueM))
                {
                    if (unitPriceAfterValueM.ToString().StartsWith("-"))
                    {
                        unitPriceAfterValueM = unitPriceAfterValueM.Substring(2);
                    }
                    else
                    {
                        unitPriceAfterValueM = unitPriceAfterValueM.Substring(1);
                    }
                }

                //M数量
                string quantityValueM = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["M数量"].Index);
                if (!string.IsNullOrEmpty(quantityValueM) && quantityValueM.ToString().StartsWith("-"))
                {
                    quantityValueM = quantityValueM.Substring(1);
                }

                //M単価
                string unitPriceValueM = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["M単価"].Index);
                if (!string.IsNullOrEmpty(unitPriceValueM))
                {
                    if (unitPriceValueM.ToString().StartsWith("-"))
                    {
                        unitPriceValueM = unitPriceValueM.Substring(2);
                    }
                    else
                    {
                        unitPriceValueM = unitPriceValueM.Substring(1);
                    }
                }
               
                //M金額
                string priceValueM = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["M金額"].Index);
                if (!string.IsNullOrEmpty(priceValueM))
                {
                    if (priceValueM.ToString().StartsWith("-"))
                    {
                        priceValueM = priceValueM.Substring(2);
                    }
                    else
                    {
                        priceValueM = priceValueM.Substring(1);
                    }
               }             

                if (string.IsNullOrEmpty(quantityAfterValueM) && string.IsNullOrEmpty(unitPriceAfterValueM))
                {
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後m金額"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index] = String.Empty;
                }
                else if (string.IsNullOrEmpty(quantityAfterValueM))
                {
                    // 変更後M金額、M金額差分の計算
                    if (decimal.TryParse(quantityValueM, out decimal quantityM) &&
                        decimal.TryParse(unitPriceAfterValueM, out decimal unitPriceM))
                    {
                        // 合計金額を計算
                        decimal totalAmountM = quantityM * unitPriceM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後m金額"].Index] = totalAmountM;
                        if (decimal.TryParse(priceValueM, out decimal praceM))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index] = totalAmountM - praceM;
                            if ((totalAmountM - praceM) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //M数量の差分は無し
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index] = String.Empty;

                    //M単価差分の計算
                    if (decimal.TryParse(unitPriceAfterValueM, out decimal aftrerUnitPriceM) &&
                       decimal.TryParse(unitPriceValueM, out decimal _unitPriceM))
                    {
                        // 合計金額を計算
                        decimal qunitPriceDifffM = aftrerUnitPriceM - _unitPriceM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index] = qunitPriceDifffM;
                        if (qunitPriceDifffM < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }
                }
                else if (string.IsNullOrEmpty(unitPriceAfterValueM))
                {
                    // 変更後M金額、M金額差分の計算
                    if (decimal.TryParse(quantityAfterValueM, out decimal quantityM) &&
                        decimal.TryParse(unitPriceValueM, out decimal unitPriceM))
                    {
                        // 合計金額を計算
                        decimal totalAmountM = quantityM * unitPriceM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後m金額"].Index] = totalAmountM;
                        if (decimal.TryParse(priceValueM, out decimal praceM))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index] = totalAmountM - praceM;
                            if ((totalAmountM - praceM) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //M数量差分の計算
                    if (decimal.TryParse(quantityAfterValueM, out decimal aftrerQuantityM) &&
                        decimal.TryParse(quantityValueM, out decimal _quantityValueM))
                    {
                        // 合計金額を計算
                        decimal quantityDifffM = aftrerQuantityM - _quantityValueM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index] = quantityDifffM;
                        if (quantityDifffM < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }

                    //M単価差分は無し
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index] = String.Empty;
                }
                else
                {
                    // 変更後M金額、M金額差分の計算
                    if (decimal.TryParse(quantityAfterValueM, out decimal quantityM) &&
                        decimal.TryParse(unitPriceAfterValueM, out decimal unitPriceM))
                    {
                        // 合計金額を計算
                        decimal totalAmountM = quantityM * unitPriceM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後m金額"].Index] = totalAmountM;
                        if (decimal.TryParse(priceValueM, out decimal praceM))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index] = totalAmountM - praceM;
                            if ((totalAmountM - praceM) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //M数量差分の計算
                    if (decimal.TryParse(quantityAfterValueM, out decimal aftrerQuantityM) &&
                        decimal.TryParse(quantityValueM, out decimal _quantityValueM))
                    {
                        // 合計金額を計算
                        decimal quantityDifffM = aftrerQuantityM - _quantityValueM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index] = quantityDifffM;
                        if (quantityDifffM < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M数量差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }

                    //M単価差分の計算
                    if (decimal.TryParse(unitPriceAfterValueM, out decimal aftrerUnitPriceM) &&
                       decimal.TryParse(unitPriceValueM, out decimal _unitPriceM))
                    {
                        // 合計金額を計算
                        decimal qunitPriceDifffM = aftrerUnitPriceM - _unitPriceM;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index] = qunitPriceDifffM;
                        if (qunitPriceDifffM < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["M単価差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }
                }

               

                //H系統の金額の計算
                //変更後H数量
                string quantityAfterValueH = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["変更後h数量"].Index);
                if (!string.IsNullOrEmpty(quantityAfterValueH) && quantityAfterValueH.ToString().StartsWith("-"))
                {
                    quantityAfterValueH = quantityAfterValueH.Substring(1);
                }

                //変更後H単価
                string unitPriceAfterValueH = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["変更後h単価"].Index);
                if (!string.IsNullOrEmpty(unitPriceAfterValueH))
                {
                    if (unitPriceAfterValueH.ToString().StartsWith("-"))
                    {
                        unitPriceAfterValueH = unitPriceAfterValueH.Substring(2);
                    }
                    else
                    {
                        unitPriceAfterValueH = unitPriceAfterValueH.Substring(1);
                    }
                }

                //H数量
                string quantityValueH = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["H数量"].Index);
                if (!string.IsNullOrEmpty(quantityValueH) && quantityValueH.ToString().StartsWith("-"))
                {
                    quantityValueH = quantityValueH.Substring(1);
                }

                //H単価
                string unitPriceValueH = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["H単価"].Index);
                if (!string.IsNullOrEmpty(unitPriceValueH))
                {
                    if (unitPriceValueH.ToString().StartsWith("-"))
                    {
                        unitPriceValueH = unitPriceValueH.Substring(2);
                    }
                    else
                    {
                        unitPriceValueH = unitPriceValueH.Substring(1);
                    }
                }

                //H金額
                string priceValueH = c1FlexGridSalesEdit.GetDataDisplay(editedRow, c1FlexGridSalesEdit.Cols["H金額"].Index);
                if (!string.IsNullOrEmpty(priceValueH))
                {
                    if (priceValueH.ToString().StartsWith("-"))
                    {
                        priceValueH = priceValueH.Substring(2);
                    }
                    else
                    {
                        priceValueH = priceValueH.Substring(1);
                    }
                }

                if (string.IsNullOrWhiteSpace(quantityAfterValueH) && string.IsNullOrWhiteSpace(unitPriceAfterValueH))
                {
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後h金額"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index] = String.Empty;
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index] = String.Empty;
                }
                else if (string.IsNullOrWhiteSpace(quantityAfterValueH))
                {
                    //変更後H金額、H金額差分の計算
                    if (decimal.TryParse(quantityValueH, out decimal quantityH) &&
                        decimal.TryParse(unitPriceAfterValueH, out decimal unitPriceH))
                    {
                        // 合計金額を計算
                        decimal totalAmountH = quantityH * unitPriceH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後h金額"].Index] = totalAmountH;
                        if (decimal.TryParse(priceValueH, out decimal praceH))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index] = totalAmountH - praceH;
                            if ((totalAmountH - praceH) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //H数量差分は無し
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index] = String.Empty;

                    //H単価差分の計算
                    if (decimal.TryParse(unitPriceAfterValueH, out decimal aftrerUnitPriceH) &&
                        decimal.TryParse(unitPriceValueH, out decimal _unitPriceH))
                    {
                        // 合計金額を計算
                        decimal qunitPriceDifffH = aftrerUnitPriceH - _unitPriceH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index] = qunitPriceDifffH;
                        if (qunitPriceDifffH < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index).StyleNew.ForeColor = Color.Red; ;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }
                }
                else if (string.IsNullOrWhiteSpace(unitPriceAfterValueH))
                {
                    // 変更後H金額、H金額差分の計算
                    if (decimal.TryParse(quantityAfterValueH, out decimal quantityH) &&
                        decimal.TryParse(unitPriceValueH, out decimal unitPriceH))
                    {
                        // 合計金額を計算
                        decimal totalAmountH = quantityH * unitPriceH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後h金額"].Index] = totalAmountH;
                        if (decimal.TryParse(priceValueH, out decimal praceH))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index] = totalAmountH - praceH;
                            if ((totalAmountH - praceH) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //H数量差分の計算
                    if (decimal.TryParse(quantityAfterValueH, out decimal aftrerQuantityH) &&
                        decimal.TryParse(quantityValueH, out decimal _quantityValueH))
                    {
                        // 合計金額を計算
                        decimal quantityDifffH = aftrerQuantityH - _quantityValueH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index] = quantityDifffH;
                        if (quantityDifffH < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }

                    //H単価差分は無し
                    c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index] = String.Empty;
                }
                else
                {
                    //変更後H金額、H金額差分の計算
                    if (decimal.TryParse(quantityAfterValueH, out decimal quantityH) &&
                        decimal.TryParse(unitPriceAfterValueH, out decimal unitPriceH))
                    {
                        // 合計金額を計算
                        decimal totalAmountH = quantityH * unitPriceH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["変更後h金額"].Index] = totalAmountH;
                        if (decimal.TryParse(priceValueH, out decimal praceH))
                        {
                            c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index] = totalAmountH - praceH;
                            if ((totalAmountH - praceH) < 0)
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = Color.Red;
                            }
                            else
                            {
                                c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H金額差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }

                    //H数量差分の計算
                    if (decimal.TryParse(quantityAfterValueH, out decimal aftrerQuantityH) &&
                        decimal.TryParse(quantityValueH, out decimal _quantityValueH))
                    {
                        // 合計金額を計算
                        decimal quantityDifffH = aftrerQuantityH - _quantityValueH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index] = quantityDifffH;
                        if (quantityDifffH < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index).StyleNew.ForeColor = Color.Red;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H数量差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }

                    //H単価差分の計算
                    if (decimal.TryParse(unitPriceAfterValueH, out decimal aftrerUnitPriceH) &&
                        decimal.TryParse(unitPriceValueH, out decimal _unitPriceH))
                    {
                        // 合計金額を計算
                        decimal qunitPriceDifffH = aftrerUnitPriceH - _unitPriceH;

                        // 合計金額を別のセルに出力
                        c1FlexGridSalesEdit[editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index] = qunitPriceDifffH;
                        if (qunitPriceDifffH < 0)
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index).StyleNew.ForeColor = Color.Red; ;
                        }
                        else
                        {
                            c1FlexGridSalesEdit.GetCellRange(editedRow, c1FlexGridSalesEdit.Cols["H単価差分"].Index).StyleNew.ForeColor = SystemColors.WindowText;
                        }
                    }
                }
            }
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
        /// <summary>
        /// 仕入金額照合アンロック機能
        /// </summary>
        private async Task<bool> AmountCheckUnlockAsync()
        {
            var result = await Program.HatFApiClient.PostAsync<bool>(ApiResources.HatF.Client.SalesEditUnLock, new Dictionary<string, object>()
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
            lblScreenMode.Text = "売上額訂正";
            currentMode = ScreenMode.Edit;
            this.buttonCONTACT_EMAIL.Enabled = true;
            //this.buttonSAVE.Enabled = true;
        }
        /// <summary>
        /// 読み取り設定
        /// </summary>
        private void ReadSetting()
        {
            lblScreenMode.ForeColor = System.Drawing.Color.Red;
            lblScreenMode.Text = "売上額訂正（読み取り専用）";
            lblLockInfo.Text = "編集者：" + lockUserName
                + "\r\n"
                + "編集開始日時：" + lockEditStartDateTime;
            this.currentMode = ScreenMode.ReadOnly;
            this.buttonCONTACT_EMAIL.Enabled = false;
            //this.buttonSAVE.Enabled = false;
            lblLockInfo.Visible = true;
            btnUnlock.Visible = true;
        }

        private void buttonDiff_Click(object sender, EventArgs e)
        {
            //差分があるセルの色を変更する
            // データ行の数
            int rowCount = c1FlexGridSalesEdit.Rows.Count;

            // 全てのセルを走査して差分を比較
            for (int row = c1FlexGridSalesEdit.Rows.Fixed; row < rowCount; row++)
            {
                if (String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後得意先"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後仕入先"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後仕入先枝番"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後h数量"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後h単価"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後m数量"]?.ToString()) &&
                    String.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後m単価"]?.ToString()))
                {
                    //色を戻す
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後得意先"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先枝番"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h数量"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h単価"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m数量"].Index).StyleNew.BackColor = SystemColors.Window;
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m単価"].Index).StyleNew.BackColor = SystemColors.Window;
                    continue;
                }

                // 変更がある場合に背景色を変更
                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後得意先"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後得意先"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後得意先"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後仕入先"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後仕入先枝番"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先枝番"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後仕入先枝番"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後h数量"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h数量"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h数量"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後h単価"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h単価"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後h単価"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後m数量"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m数量"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m数量"].Index).StyleNew.BackColor = SystemColors.Window;
                }

                if (!string.IsNullOrEmpty(c1FlexGridSalesEdit[row, "変更後m単価"]?.ToString()))
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m単価"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
                else
                {
                    c1FlexGridSalesEdit.GetCellRange(row, c1FlexGridSalesEdit.Cols["変更後m単価"].Index).StyleNew.BackColor = SystemColors.Window;
                }

            }   
        }

        private void c1FlexGridSalesEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (c1FlexGridSalesEdit.Cols[c1FlexGridSalesEdit.Col].Name == "M納日")
                {
                    c1FlexGridSalesEdit[c1FlexGridSalesEdit.Row, c1FlexGridSalesEdit.Col] = null;
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

        private async void SalesEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentMode == ScreenMode.Edit)
            {
                if (c1FlexGridSalesEdit.Rows.Count > 1)
                {
                    await AmountCheckUnlockAsync();
                }
            }
        }

        private async void buttonSearch_Click_1(object sender, EventArgs e)
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
                    if (c1FlexGridSalesEdit.Rows.Count > 1)
                    {
                        await AmountCheckUnlockAsync();
                    }
                }
                Condition.Hat注文番号 = textBoxHATNUMBER.Text;
                await UpdateListAsync();
                Cursor.Current = Cursors.Default;
            }
        }
        //差し戻し
        private async void btnRemand_Click(object sender, EventArgs e)
        {
            await RemandAsync();
            await AmountCheckUnlockAsync();
            await UpdateListAsync();
        }
        //申請
        private async void btnApplication_Click(object sender, EventArgs e)
        {
            await ApplicationAsync();
            await AmountCheckUnlockAsync();
            await UpdateListAsync();
        }
        //承認
        private async void btnAapproval_Click(object sender, EventArgs e)
        {
            await ApprovalAsync();
            await AmountCheckUnlockAsync();
            await UpdateListAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
