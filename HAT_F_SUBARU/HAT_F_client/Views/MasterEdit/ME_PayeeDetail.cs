using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Models;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    /// <summary>仕入先詳細画面</summary>
    public partial class ME_PayeeDetail : Form
    {
        /// <summary>
        /// 編集対象の取得キー
        /// </summary>
        private string _supCode;

        /// <summary>
        /// 編集モード用にサーバーから取得したデータ
        /// </summary>
        private SupplierMst _initialSupplier;


        private bool IsUpdateMode
        {
            get
            {
                return _supCode != null;
            }
        }

        /// <summary>コンストラクタ</summary>
        public ME_PayeeDetail() : this(null)
        {
            //InitializeComponent();

            //if (!this.DesignMode)
            //{
            //    FormStyleHelper.SetFixedSizeDialogStyle(this);
            //}
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="employeeId">社員ID</param>
        public ME_PayeeDetail(string supCode)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
                _supCode = supCode;
                this.Text += !string.IsNullOrEmpty(supCode) ? " (修正)" : " (新規)";
            }
        }

        /// <summary>OKボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            var result = await ValidateInputSereverCheckingAsync();
            if (result.Failed || result.Value == false)
            {
                return;
            }

            if (!DialogHelper.SaveItemConfirm(this))
            {
                return;
            }

            SupplierMst supplierMst = IsUpdateMode ? _initialSupplier : new SupplierMst();
            ScreenToSupplier(supplierMst);

            var url = string.Format(ApiResources.HatF.MasterEditor.Supplier, supplierMst.SupCode);
            var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<SupplierMst>(url, supplierMst);
            });

            if (apiResult.Failed)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void ME_SupplierDetail_Load(object sender, EventArgs e)
        {
            SetInputLimit();
            InitializeComboBoxes();
            ClearInputs();

            if (IsUpdateMode)
            {
                var url = string.Format(ApiResources.HatF.MasterEditor.Supplier, _supCode);

                var supplier = await ApiHelper.FetchAsync(this, async () =>
                {
                    return await Program.HatFApiClient.GetAsync<SupplierMst>(url);
                });

                if (supplier.Failed)
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                _initialSupplier = supplier.Value;
                SupplierToScreen(_initialSupplier);

                txtSupCode.Enabled = false;
                btnCheckDuplicate.Enabled = false;
            }
        }

        /// <summary>
        /// テキストの入力制限
        /// </summary>
        private void SetInputLimit()
        {
            txtSupCode.KeyPress += FormHelper.KeyPressForNumber;
            txtSupKana.KeyPress += FormHelper.KeyPressForKanaName;
            txtSupZipCode.KeyPress += FormHelper.KeyPressForZipCode;
            txtSupTel.KeyPress += FormHelper.KeyPressForTelFax;
            txtSupFax.KeyPress += FormHelper.KeyPressForTelFax;
            txtSupEmail.KeyPress += FormHelper.KeyPressForEmail;
        }

        private void ClearInputs()
        {
            foreach (var textBox in FormHelper.GetAllControls(this).OfType<TextBox>())
            {
                textBox.Text = string.Empty;
            }
        }

        /// <summary>各コンボボックスの初期化</summary>
        private void InitializeComboBoxes()
        {
            // 仕入先都道府県
            cmbSupState.DisplayMember = nameof(Prefecture.Name);
            cmbSupState.ValueMember = nameof(Prefecture.Name);
            cmbSupState.DataSource = JsonResources.Prefectures;

            // 締日
            cmbSupCloseDate.ValueMember = nameof(CodeName<short?>.Code);
            cmbSupCloseDate.DisplayMember = nameof(CodeName<short?>.Name);
            cmbSupCloseDate.DataSource = JsonResources.CloseDates;

            // 支払月
            cmbSupPayMonths.ValueMember = nameof(HatF_PaymentMonth.Key);
            cmbSupPayMonths.DisplayMember = nameof(HatF_PaymentMonth.Name);
            cmbSupPayMonths.DataSource = HatF_PaymentMonthRepo.GetInstance().Entities;

            // 支払日
            cmbSupPayDates.ValueMember = nameof(HatF_PaymentDay.Key);
            cmbSupPayDates.DisplayMember = nameof(HatF_PaymentDay.Name);
            cmbSupPayDates.DataSource = HatF_PaymentDayRepo.GetInstance().Entities;

            // 支払い方法区分
            cmbPayMethodType.ValueMember = nameof(HatF_PaymentClassification.Key);
            cmbPayMethodType.DisplayMember = nameof(HatF_PaymentClassification.Name);
            cmbPayMethodType.DataSource = HatF_PaymentClassificationRepo.GetInstance().Entities;

            // 発注先種別
            cmbSupplierType.ValueMember = nameof(CodeName<short>.Code);
            cmbSupplierType.DisplayMember = nameof(CodeName<short>.Name);
            cmbSupplierType.DataSource = JsonResources.SupplierTypes;
        }

        /// <summary>仕入先情報を画面に表示</summary>
        /// <param name="supplier">仕入先情報</param>
        private void SupplierToScreen(SupplierMst supplier)
        {
            chkDeleted.Checked = supplier.Deleted;
            txtSupCode.Text = supplier.SupCode;
            txtSupName.Text = supplier.SupName;
            txtSupKana.Text = supplier.SupKana;
            txtSupEmpName.Text = supplier.SupEmpName;
            txtSupDepName.Text = supplier.SupDepName;
            txtSupZipCode.Text = supplier.SupZipCode;
            SelectComboBoxValue(cmbSupState, supplier.SupState);
            txtSupAddress1.Text = supplier.SupAddress1;
            txtSupAddress2.Text = supplier.SupAddress2;
            txtSupTel.Text = supplier.SupTel;
            txtSupFax.Text = supplier.SupFax;
            txtSupEmail.Text = supplier.SupEmail;

            // TODO: DB変更対応
            //SelectComboBoxValue(cmbSupCloseDate, supplier.SupCloseDate);
            //SelectComboBoxValue(cmbSupPayMonths, supplier.SupPayMonths);
            //SelectComboBoxValue(cmbSupPayDates, supplier.SupPayDates);
            //SelectComboBoxValue(cmbPayMethodType, supplier.PayMethodType);
            //SelectComboBoxValue(cmbSupplierType, supplier.SupplierType);
        }

        /// <summary>値がnullでない場合のみコンボボックスに値を設定する</summary>
        /// <param name="comboBox">コンボボックス</param>
        /// <param name="value">値</param>
        private void SelectComboBoxValue(ComboBox comboBox, object value)
        {
            if (value is not null)
            {
                comboBox.SelectedValue = value;
            }
        }

        /// <summary>入力内容から仕入先情報を作成</summary>
        /// <returns>仕入先情報</returns>
        private SupplierMst ScreenToSupplier(SupplierMst destination)
        {
            //var result = new SupplierMst();

            destination.Deleted = chkDeleted.Checked;
            destination.SupCode = txtSupCode.Text.Trim();
            destination.SupName = txtSupName.Text.Trim();
            destination.SupKana = txtSupKana.Text.Trim();
            destination.SupEmpName = txtSupEmpName.Text.Trim();
            destination.SupDepName = txtSupDepName.Text.Trim();
            destination.SupZipCode = txtSupZipCode.Text.Trim();
            destination.SupState = cmbSupState.SelectedValue as string;
            destination.SupAddress1 = txtSupAddress1.Text.Trim();
            destination.SupAddress2 = txtSupAddress2.Text.Trim();
            destination.SupTel = txtSupTel.Text.Trim();
            destination.SupFax = txtSupFax.Text.Trim();
            destination.SupEmail = txtSupEmail.Text.Trim();

            // TODO: DB変更対応
            //destination.SupCloseDate = (short?)cmbSupCloseDate.SelectedValue;
            //destination.SupPayMonths = (short?)cmbSupPayMonths.SelectedValue;
            //destination.SupPayDates = (short?)cmbSupPayDates.SelectedValue;
            //destination.PayMethodType = (short?)cmbPayMethodType.SelectedValue;
            //destination.SupplierType = (short?)cmbSupplierType.SelectedValue;

            return destination;
        }

        /// <summary>重複確認ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnCheckDuplicate_Click(object sender, EventArgs e)
        {
            if (!ValidateSupCode())
            {
                return;
            }

            var result = await ValidateSupCodeDuplicateAsync();
            if (result.Success && result.Value == true)
            {
                DialogHelper.InformationMessage(this, $"{txtSupCode.Text.Trim()}は使用可能です。");
            }
        }

        /// <summary>仕入先情報を取得する</summary>
        /// <param name="supCode">仕入先コード</param>
        /// <returns>仕入先情報</returns>
        private async Task<SupplierMst> GetSupplierAsync(string supCode)
        {
            var url = string.Format(ApiResources.HatF.MasterEditor.Supplier, supCode);
            var supplier = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<SupplierMst>(url);
            });
            if (supplier.Failed)
            {
                Close();
            }
            return supplier.Value;
        }

        private void TrimInputs()
        {
            Control[] controls = new[] { txtSupCode, txtSupName, };

            foreach (Control control in controls)
            {
                control.Text = control.Text.Trim();
            }
        }

        #region バリデーション

        /// <summary>入力項目全体のバリデーション</summary>
        /// <returns>成否</returns>
        private bool ValidateInput()
        {
            // 仕入先コードチェック
            if (false == ValidateSupCode()) //重複確認ボタンと共通
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtSupName.Text.Trim()))
            {
                DialogHelper.InputRequireMessage(this, "仕入先名");
                return false;
            }

            return true;
        }

        /// <summary>サーバーチェックを含むバリデーション</summary>
        /// <returns>成否</returns>
        private async Task<MethodResult<bool>> ValidateInputSereverCheckingAsync()
        {
            if (false == this.IsUpdateMode)
            {
                var result = await ValidateSupCodeDuplicateAsync();
                if (result.Failed || result.Value == false)
                {
                    return result;
                }
            }

            return new MethodResult<bool>(true);
        }

        /// <summary>仕入先コードのバリデーション</summary>
        /// <returns>成否</returns>
        private bool ValidateSupCode()
        {
            if (string.IsNullOrEmpty(txtSupCode.Text.Trim()))
            {
                DialogHelper.InputRequireMessage(this, "仕入先コード");
                return false;
            }

            return true;
        }

        /// <summary>仕入先コードの重複バリデーション</summary>
        /// <returns>成否</returns>
        private async Task<MethodResult<bool>> ValidateSupCodeDuplicateAsync()
        {
            var supCode = txtSupCode.Text.Trim();

            var duplicateSupplier = await ApiHelper.FetchAsync(this, async () =>
            {
                var url = string.Format(ApiResources.HatF.MasterEditor.Supplier, supCode);
                return await Program.HatFApiClient.GetAsync<SupplierMst>(url);
            });

            if (duplicateSupplier.Failed)
            {
                return MethodResult<bool>.FailedResult;
            }

            if (duplicateSupplier.Value is not null)
            {
                DialogHelper.WarningMessage(this, $"{txtSupCode.Text.Trim()}は既に使用されています。");
                return new MethodResult<bool>(false);
            }

            return new MethodResult<bool>(true);
        }

        #endregion バリデーション

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}