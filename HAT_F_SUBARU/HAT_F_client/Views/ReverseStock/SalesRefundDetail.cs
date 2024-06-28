using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Helpers;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Cooperate;
using HatFClient.Views.Estimate;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.template
{
    public partial class SalesRefundDetail : Form
    {
        private PurchaseRepo _purchaseRepo;
        private RoleController _roleController;
        private readonly string approvalType = "R9"; //U1:仕入売上訂正、R1:返品入力、R5:返品入庫 
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
        public ViewSalesRefundDetail Condition { get; set; } = new ViewSalesRefundDetail();

        public SalesRefundDetail()
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
            var response = await Program.HatFApiClient.GetAsync<List<Employee>>(ApiResources.HatF.MasterEditor.EmloyeeUserAssignedRole, new { userRoleCd = "14" }); //14:返金承認権限

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


        private async Task UpdateListAsync()
        {
            if (Condition.Hat注文番号 != null)
            {
                var result = await _purchaseRepo.GetDetail(Condition);
                c1FlexGrid_ReturnReceivingDetail.DataSource = result;
                if (c1FlexGrid_ReturnReceivingDetail.Rows.Count > 1)
                {
                    await SetApprovalDataAsync();
                    blobStrageForm1.Init("ReturnApproval_" + Condition.Hat注文番号 + "_" + Condition.伝票番号);
                }
            }
        }

        //承認データにおける画面制御
        private async Task SetApprovalDataAsync()
        {
            string approvalReqNo = null;
            for (var i = 1; i < c1FlexGrid_ReturnReceivingDetail.Rows.Count; i++)
            {
                //行単位でどれかのデータが入力されていることを保存対象とする
                var r = c1FlexGrid_ReturnReceivingDetail.Rows[i].DataSource as ViewSalesRefundDetail;
                if (!String.IsNullOrEmpty(r.返金承認要求番号?.ToString()))
                {
                    approvalReqNo = r.返金承認要求番号;
                }
            }
            if (approvalReqNo != null)
            {
                var url = String.Format(ApiResources.HatF.Approval.Get, approvalReqNo);
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
                if (response.Data.Approval.ApprovalStatus == 0)
                {
                    btnApplication.Enabled = false;
                    btnAapproval.Enabled = true; ;
                    btnRemand.Enabled = true;
                    CmbAuthorizer.Enabled = false;
                    CmbAuthorizer2.Enabled = false;
                    blobStrageForm1.CanDelete = false;
                    blobStrageForm1.CanDownload = true;
                    blobStrageForm1.CanUpload = false;
                    c1FlexGrid_ReturnReceivingDetail.AllowEditing = false;
                    txtApprovalStatus.Text = "申請中";
                }
                else if (response.Data.Approval.ApprovalStatus == 1)
                {
                    btnApplication.Enabled = false;
                    btnAapproval.Enabled = true; ;
                    btnRemand.Enabled = true;
                    CmbAuthorizer.Enabled = false;
                    CmbAuthorizer2.Enabled = false;
                    c1FlexGrid_ReturnReceivingDetail.AllowEditing = false;
                    blobStrageForm1.CanDelete = false;
                    blobStrageForm1.CanDownload = true;
                    blobStrageForm1.CanUpload = false;
                    txtApprovalStatus.Text = "承認中";
                }
                else
                {
                    if (response.Data.Approval.ApprovalStatus == 9)
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
                    blobStrageForm1.CanDelete = true;
                    blobStrageForm1.CanDownload = true;
                    blobStrageForm1.CanUpload = true;
                    c1FlexGrid_ReturnReceivingDetail.AllowEditing = true;
                }

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
                blobStrageForm1.CanDelete = true;
                blobStrageForm1.CanDownload = true;
                blobStrageForm1.CanUpload = true;
                c1FlexGrid_ReturnReceivingDetail.AllowEditing = true;
            }
        }

        #region <画面アクション>
        private async void ReturnReceivingDetail_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            await InitializeComboAsync();
            if (Condition.Hat注文番号 != null)
            {
                textBoxHATNUMBER.Text = Condition.Hat注文番号.ToString();
                textBoxDenNo.Text = Condition.伝票番号.ToString();
                await UpdateListAsync();
            }
            else
            {
                c1FlexGrid_ReturnReceivingDetail.DataSource = new List<ViewSalesRefundDetail>();
            }
            Cursor.Current = Cursors.Default;
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
            approval.FinalApproverEmpId = int.Parse(CmbAuthorizer2.SelectedValue.ToString());
            List<SalesReturn> target = new List<SalesReturn>();

            for (var i = 1; i < c1FlexGrid_ReturnReceivingDetail.Rows.Count; i++)
            {
                //行単位でどれかのデータが入力されていることを保存対象とする
                var r = c1FlexGrid_ReturnReceivingDetail.Rows[i].DataSource as ViewSalesRefundDetail;
                if (!String.IsNullOrEmpty(r.返金承認要求番号?.ToString()))
                {
                    approvalEx.approvalReqNo = r.返金承認要求番号;
                }
                if (requestType == ApprovalResult.Request)
                {
                    if (!String.IsNullOrEmpty(r.返金単価?.ToString()))
                    {
                        target.Add(new SalesReturn()
                        {
                            ReturningProductsId = r.返品id, // 返品ID
                            RowNo = r.返品行番号, // 行番号
                            RefundUnitPrice = r.返金単価 //返金単価
                        });
                    }
                }
            }
            if (target.Count != 0)
            {
                approval.SalesReturns = target;
            }
            else
            {
                approval.SalesReturns = null;
            }
            approvalEx.request = approval;

            return approvalEx;
        }

        #endregion


        private void validationNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 数字以外の入力を無視する
            }
        }


        private void c1FlexGrid_ReturnReceivingDetail_AfterEdit(object sender, RowColEventArgs e)
        {
            // 編集されたセルの情報を取得
            int editedRow = c1FlexGrid_ReturnReceivingDetail.Row;
            int editedCol = c1FlexGrid_ReturnReceivingDetail.Col;

            // 編集されたセルが数量列または単価列であるかを確認
            if (c1FlexGrid_ReturnReceivingDetail.Cols[editedCol].Name == "返金単価")
            {
                // 数量と単価が入力されているセルの値を取得
                object quantityValue = c1FlexGrid_ReturnReceivingDetail.GetDataDisplay(editedRow, c1FlexGrid_ReturnReceivingDetail.Cols["返品数量"].Index);
                object unitPriceValue = c1FlexGrid_ReturnReceivingDetail.GetDataDisplay(editedRow, c1FlexGrid_ReturnReceivingDetail.Cols["返金単価"].Index);

                if (string.IsNullOrWhiteSpace(quantityValue.ToString()) || string.IsNullOrWhiteSpace(unitPriceValue.ToString()))
                {
                    c1FlexGrid_ReturnReceivingDetail[editedRow, c1FlexGrid_ReturnReceivingDetail.Cols["返金額"].Index] = String.Empty;
                    return;
                }
                // 数量と単価が数値に変換可能かチェック
                if (decimal.TryParse(quantityValue?.ToString(), out decimal quantity) &&
                    decimal.TryParse(unitPriceValue?.ToString().Substring(1), out decimal unitPrice))
                {
                    // 合計金額を計算
                    decimal totalAmount = quantity * unitPrice;

                    // 合計金額を別のセルに出力
                    c1FlexGrid_ReturnReceivingDetail[editedRow, c1FlexGrid_ReturnReceivingDetail.Cols["返金額"].Index] = totalAmount;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //差分があるセルの色を変更する
            // データ行の数
            int rowCount = c1FlexGrid_ReturnReceivingDetail.Rows.Count;

            // 全てのセルを走査して差分を比較
            for (int row = c1FlexGrid_ReturnReceivingDetail.Rows.Fixed; row < rowCount; row++)
            {
                if (String.IsNullOrEmpty(c1FlexGrid_ReturnReceivingDetail[row, "返金単価"]?.ToString()))
                {
                    //色を戻す
                    c1FlexGrid_ReturnReceivingDetail.GetCellRange(row, c1FlexGrid_ReturnReceivingDetail.Cols["返金単価"].Index).StyleNew.BackColor = SystemColors.Window;
                }
                else
                {
                    c1FlexGrid_ReturnReceivingDetail.GetCellRange(row, c1FlexGrid_ReturnReceivingDetail.Cols["返金単価"].Index).StyleNew.BackColor = Color.Yellow; // 背景色を黄色に変更
                }
            }
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
                if (approvalRequestEx.request.SalesReturns != null)
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
            if (int.Parse(CmbAuthorizer.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId && int.Parse(CmbAuthorizer2.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
            {
                approvalResult = ApprovalResult.FinalApprove;
            }
            else if (int.Parse(CmbAuthorizer.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
            {
                approvalResult = ApprovalResult.Approve;
            }
            else if (int.Parse(CmbAuthorizer2.SelectedValue.ToString()) == LoginRepo.GetInstance().CurrentUser.EmployeeId)
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

        //差し戻し
        private async void btnRemand_Click(object sender, EventArgs e)
        {
            await RemandAsync();
            await UpdateListAsync();
        }
        //申請
        private async void btnApplication_Click(object sender, EventArgs e)
        {
            await ApplicationAsync();
            await UpdateListAsync();
        }
        //承認
        private async void btnAapproval_Click(object sender, EventArgs e)
        {
            await ApprovalAsync();
            await UpdateListAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
