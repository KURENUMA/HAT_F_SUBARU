using C1.Win.C1Input;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    /// <summary>得意先詳細画面</summary>
    /// <remarks>
    /// 得意先＝取引先。この画面のコメントではDBの論理名に合わせた記述をする
    /// </remarks>
    public partial class ME_CompanysMstDetail : Form
    {
        /// <summary>取引先コード</summary>
        private readonly string _compCode;

        /// <summary>編集モード用にサーバーから取得したデータ</summary>
        private CompanysMst _companysMst;


        private bool IsUpdateMode
        {
            get { return !string.IsNullOrEmpty(_compCode); }
        }

        /// <summary>コンストラクタ</summary>
        public ME_CompanysMstDetail() : this(null)
        {
            //InitializeComponent();

            //if (!this.DesignMode)
            //{
            //    FormStyleHelper.SetFixedSizeDialogStyle(this);
            //}
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="compCode">取引先コード</param>
        public ME_CompanysMstDetail(string compCode)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                _compCode = compCode;
                this.Text += !string.IsNullOrEmpty(compCode) ? " (修正)" : " (新規)";
            }
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void ME_EmployeeDetail_Load(object sender, EventArgs e)
        {
            SetInputLimit();
            InitializeComboBoxes();
            ClearInputs();

            if (this.IsUpdateMode)
            {
                // 取引先情報をして画面に反映する
                var apiResult = await GetCompanysMstAsync(_compCode);
                if (apiResult.Failed)
                {
                    this.DialogResult= DialogResult.Cancel;
                    this.Close();
                    return;
                }

                _companysMst = apiResult.Value.Single();
                CompanysToScreen(_companysMst);

                txtCompCode.Enabled = false;
                btnCheckDuplicate.Enabled = false;
            }
        }


        /// <summary>
        /// テキストの入力制限
        /// </summary>
        private void SetInputLimit()
        {
            txtCompCode.KeyPress += FormHelper.KeyPressForNumber;
            txtZipCode.KeyPress += FormHelper.KeyPressForZipCode;
            txtTel.KeyPress += FormHelper.KeyPressForTelFax;
            txtFax.KeyPress += FormHelper.KeyPressForTelFax;
            txtFax2.KeyPress += FormHelper.KeyPressForTelFax;

            txtMaxCredit.KeyPress += FormHelper.KeyPressForNumberPlus;  //数字とカンマ等のみ
            txtTempCreditUp.KeyPress += FormHelper.KeyPressForNumberPlus;  //数字とカンマ等のみ
        }

        private void ClearInputs()
        {
            foreach (var textBox in FormHelper.GetAllControls(this).OfType<TextBox>())
            {
                textBox.Text = string.Empty;
            }
        }


        /// <summary>OKボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnOK_Click(object sender, EventArgs e)
        {
            // 入力チェック（クライアント）
            if (!ValidateInput())
            {
                return;
            }

            // 入力チェック（サーバー）
            var validationResult = await ValidateInputWithServerAsync();
            if (validationResult.Failed || validationResult.Value == false)
            {
                return;
            }

            if (!DialogHelper.SaveItemConfirm(this))
            {
                return;
            }

            var companysMst = ScreenToCompanys();
            var url = string.Format(ApiResources.HatF.MasterEditor.CompanysMst, companysMst.CompCode);

            var result = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<int>(url, companysMst);
            });

            if (result.Failed)
            {
                //Close();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>重複確認ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnCheckDuplicate_Click(object sender, EventArgs e)
        {
            // クライアント入力チェック
            if (!ValidateCompCode())
            {
                return;
            }

            // 得意先コードの重複チェック
            var result = await IsCompCodeExistsAsync();
            if (result.Failed)
            {
                return;
            }

            string message;
            if (result.Value) 
            {
                message = "得意先コードは使用されています。";
            }
            else
            {
                message = "得意先コードは使用可能です。";
            }
            DialogHelper.InformationMessage(this, message);
        }

        /// <summary>各コンボボックスの初期化</summary>
        private void InitializeComboBoxes()
        {
            // TODO 仕入先区分コンボボックス。詳細不明

            // 都道府県
            cmbState.DisplayMember = nameof(Prefecture.Name);
            cmbState.ValueMember = nameof(Prefecture.Name);
            cmbState.DataSource = JsonResources.Prefectures;

            // 取引禁止フラグ
            cmbNoSalesFlg.DisplayMember = nameof(CodeName<short?>.Name);
            cmbNoSalesFlg.ValueMember = nameof(CodeName<short?>.Code);
            cmbNoSalesFlg.DataSource = JsonResources.NoSalesFlags;

            // TODO 雑区分、得意先グループコード。詳細不明
        }

        /// <summary>入力内容から取引先情報を作成</summary>
        /// <returns>取引先情報</returns>
        private CompanysMst ScreenToCompanys()
        {
            var result = _companysMst ?? new CompanysMst();

            result.Deleted = chkDeleted.Checked;
            result.CompCode = txtCompCode.Text.Trim();
            result.CompName = txtCompName.Text.Trim();
            result.CompKana = txtCompKana.Text.Trim();
            result.CompKanaShort = txtCompKanaShort.Text.Trim();
            result.CompBranchName = txtCompBranchName.Text.Trim();
            //result.SupType = (short?)cmbSupType.SelectedValue;
            result.ZipCode = txtZipCode.Text.Trim();
            result.State = cmbState.SelectedValue as string;
            result.Address1 = txtAddress1.Text.Trim();
            result.Address2 = txtAddress2.Text.Trim();
            result.Address3 = txtAddress3.Text.Trim();
            result.Tel = txtTel.Text.Trim();
            result.Fax = txtFax.Text.Trim();
            result.Fax2 = txtFax2.Text.Trim();
            result.NoSalesFlg = (short?)cmbNoSalesFlg.SelectedValue;
            //result.WideUseType = (short?)cmbWideUseType.SelectedValue;
            //result.CompGroupCode = cmbCompGroupCode.SelectedValue as string;

            result.MaxCredit = txtMaxCredit.ValueIsDbNull ? null : (long?)txtMaxCredit.Value;
            result.TempCreditUp = txtTempCreditUp.ValueIsDbNull ? null : (long?)txtTempCreditUp.Value;

            return result;
        }

        /// <summary>取引先情報を画面に表示</summary>
        /// <param name="companys">取引先情報</param>
        private void CompanysToScreen(CompanysMst companys)
        {
            chkDeleted.Checked = companys.Deleted;
            txtCompCode.Text = companys.CompCode;
            txtCompName.Text = companys.CompName;
            txtCompKana.Text = companys.CompKana;
            txtCompKanaShort.Text = companys.CompKanaShort;
            txtCompBranchName.Text = companys.CompBranchName;
            txtZipCode.Text = companys.ZipCode;
            SelectComboBoxValue(cmbState, companys.State);
            txtAddress1.Text = companys.Address1;
            txtAddress2.Text = companys.Address2;
            txtAddress3.Text = companys.Address3;
            txtTel.Text = companys.Tel;
            txtFax.Text = companys.Fax;
            txtFax2.Text = companys.Fax2;
            SelectComboBoxValue(cmbNoSalesFlg, companys.NoSalesFlg);
            //SelectComboBoxValue(cmbWideUseType, companys.WideUseType);
            //SelectComboBoxValue(cmbCompGroupCode, companys.CompGroupCode);
            txtMaxCredit.Value = companys.MaxCredit;
            txtTempCreditUp.Value = companys.TempCreditUp;
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

        /// <summary>取引先情報を取得</summary>
        /// <param name="compCode">取引先コード</param>
        /// <returns>取引先情報</returns>
        private async Task<ApiResult<List<CompanysMst>>> GetCompanysMstAsync(string compCode)
        {
            var url = string.Format(ApiResources.HatF.MasterEditor.CompanysMst, compCode);
            var parameter = new Dictionary<string, object>()
            {
                {"page", -1 },
                {"rows", -1 },
            };
            var companysMst = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CompanysMst>>(url, parameter);
            });

            return companysMst;
        }

        #region バリデーション


        /// <summary>入力チェック</summary>
        /// <returns>成否</returns>
        private bool ValidateInput()
        {
            // 得意先コードチェック
            if (!ValidateCompCode()) //重複確認ボタンと共通
            {
                return false;
            }

            var compName = txtCompName.Text.Trim();
            if (string.IsNullOrEmpty(compName))
            {
                DialogHelper.InputRequireMessage(this, "得意先名");
                return false;
            }

            var zipCode = txtZipCode.Text.Trim();
            //if (string.IsNullOrEmpty(zipCode))
            //{
            //    DialogHelper.InputRequireMessage(this, "郵便番号");
            //    return false;
            //}
            if (!string.IsNullOrEmpty(zipCode) && !HatFComParts.IsZipCode(zipCode))
            {
                DialogHelper.WarningMessage(this, "郵便番号が不正です。");
                return false;
            }

            return true;
        }

        /// <summary>サーバーチェックを含むバリデーション</summary>
        /// <returns>成否</returns>
        private async Task<MethodResult<bool>> ValidateInputWithServerAsync()
        {
            if (false == this.IsUpdateMode)
            {
                // 得意先コードの重複チェック
                var result = await IsCompCodeExistsAsync();
                //if (result.Failed || result.Value == false)
                if (result.Failed)
                {
                    return result;
                }

                if (result.Value)
                {
                    DialogHelper.WarningMessage(this, "得意先コードは既に使用されています。");
                    return new MethodResult<bool>(false);
                }
            }

            return new MethodResult<bool>(true);
        }


        /// <summary>得意先コードのバリデーション</summary>
        /// <returns>成否</returns>
        private bool ValidateCompCode()
        {
            if (string.IsNullOrEmpty(txtCompCode.Text.Trim()))
            {
                DialogHelper.InputRequireMessage(this, "得意先コード");
                return false;
            }
            return true;
        }

        /// <summary>得意先コードの重複バリデーション</summary>
        /// <returns>成否</returns>
        private async Task<MethodResult<bool>> IsCompCodeExistsAsync()
        {
            var compCode = txtCompCode.Text.Trim();
            var apiResult = await GetCompanysMstAsync(compCode);
            if (apiResult.Failed)
            {
                return MethodResult<bool>.FailedResult;
            }

            bool exists = apiResult.Value.Any();
            return new MethodResult<bool>(exists); 
        }
        #endregion バリデーション
    }
}