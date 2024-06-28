using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Models;
using HatFClient.Properties;
using HatFClient.Views.MasterSearch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    // TODO 都道府県がマッピング不明

    /// <summary>現場（出荷先）詳細画面</summary>
    public partial class ME_CustomersUserMstDetail : Form
    {
        private string _custCode;
        private string _custUserCode;

        private CustomersUserMst _customersUserMst;

        /// <summary>コンストラクタ</summary>
        public ME_CustomersUserMstDetail() : this(null, null)
        {
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="employeeId">社員ID</param>
        public ME_CustomersUserMstDetail(string custCode, string custUserCode)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                this.IsUpdateMode = !string.IsNullOrEmpty(custCode);
                if (!string.IsNullOrEmpty(custCode)) 
                {
                    _custCode = custCode;
                    _custUserCode = custUserCode;
                }
            }
        }

        private bool IsUpdateMode {  get; set; }
        private string CreateItemInfo(string code, string name)
        {
            string val = $"{name} ({code})";
            return val;
        }

        /// <summary>OKボタン</summary>
        /// <param name="sender">イベント発生元イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void btnOK_Click(object sender, EventArgs e)
        {
            TrimControls();

            // 入力チェック（クライアント）
            if (!ValidateInput())
            {
                return;
            }

            // 入力チェック（サーバー）
            var serverValidation = await ValidateInputWithServer();
            if (serverValidation.Failed || serverValidation.Value == false)
            {
                // 処理失敗 or バリデーションエラー
                return;
            }

            // 保存確認
            if (!DialogHelper.SaveItemConfirm(this))
            {
                return;
            }

            var formData = new List<CustomersUserMst>() { GetFormData() };

            var url = ApiResources.HatF.MasterEditor.CustomersUserMstPut;
            var result = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<int>(url, formData);
            });

            if (result.Failed)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private CustomersUserMst GetFormData()
        {
            CustomersUserMst item;
            if (this.IsUpdateMode)
            {
                item = _customersUserMst;
            }
            else
            {
                item = new CustomersUserMst();

                item.CustCode = _custCode ?? "";        // 顧客CD
                item.CustUserCode = txtCustUserCode.Text;     // 現場CD               
            }

            item.Deleted = chkDeleted.Checked;

            item.CustCode = _custCode;
            item.CustUserCode = txtCustUserCode.Text.Trim();
            item.CustUserName = txtCustUserName.Text.Trim();
            item.CustUserEmail = txtCustUserEmail.Text.Trim();

            return item;
        }


        private void TrimControls()
        {
            var controls = FormHelper.GetAllControls(this);

            foreach (TextBox control in controls.OfType<TextBox>())
            {
                control.Text = control.Text.Trim();
            }
        }

        private bool ValidateInput()
        {
            if (!this.IsUpdateMode)
            {
                // 顧客
                if (string.IsNullOrEmpty(_custCode))
                {
                    DialogHelper.InputRequireMessage(this, "顧客");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(_custCode))
            {
                DialogHelper.InputRequireMessage(this, "顧客");
                return false;
            }

            if (string.IsNullOrEmpty(txtCustUserCode.Text))
            {
                DialogHelper.InputRequireMessage(this, "担当者(キーマン)コード");
                return false;
            }

            if (string.IsNullOrEmpty(txtCustUserName.Text))
            {
                DialogHelper.InputRequireMessage(this, "担当者(キーマン)名");
                return false;
            }

            if (string.IsNullOrEmpty(txtCustUserEmail.Text))
            {
                DialogHelper.InputRequireMessage(this, "メールアドレス");
                return false;
            }

            return true;
        }

        private async Task<MethodResult<bool>> ValidateInputWithServer()
        {
            if (!this.IsUpdateMode)
            {
                string custCode = _custCode;
                string genbaCode = txtCustUserCode.Text;

                // TODO: キーマンコードチェックに変える
                var result = await IsKeymanCodeUsedAsync(custCode, genbaCode);
                if (result.Failed) { return MethodResult<bool>.FailedResult; }

                if (result.Value)
                {
                    DialogHelper.InformationMessage(this, "顧客担当者(キーマン)CDは使用されています。");
                    return new MethodResult<bool>(false);
                }
            }

            return new MethodResult<bool>(true);
        }

        private async Task<MethodResult<bool>> IsKeymanCodeUsedAsync(string custCode, string genbaCode)
        {
            string genbaCodeUrlEncoded = ApiHelper.UrlEncodeForWebApi(genbaCode);
            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersUserMst, custCode, genbaCodeUrlEncoded);

            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<DestinationsMst>>(url);
            });

            if (apiResult.Failed)
            {
                return MethodResult<bool>.FailedResult;
            }

            bool isUsed = apiResult.Value.Any();
            return new MethodResult<bool>(isUsed);
        }

        /// <summary>キャンセルボタン</summary>
        /// <param name="sender">イベント発生元イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void ME_EmployeeDetail_Load(object sender, EventArgs e)
        {
            SetInputLimit();
            InitializeComboBoxes();
            ClearInputs();

            if (this.IsUpdateMode)
            {
                if (!await SetEditDataAsync(_custCode, _custUserCode))
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                btnSelectCustomer.Enabled = false;
                txtCustUserCode.ReadOnly = true;
                btnCheckDuplicate.Enabled = false;
            }

        }

        private async Task<bool> SetEditDataAsync(string custCode,  string custUserCode)
        {
            // 顧客担当者(キーマン)
            string genbaCodeUrlEncoded = ApiHelper.UrlEncodeForWebApi(custUserCode);
            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersUserMst, custCode, genbaCodeUrlEncoded);
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CustomersUserMst>>(url);
            });

            if (apiResult.Failed || apiResult.Value.Count == 0)
            {
                return false;
            }

            // 顧客マスタ
            var urlCust = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, custCode);
            var apiResultCust = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CustomersMst>>(urlCust);
            });

            if (apiResultCust.Failed || apiResultCust.Value.Count == 0)
            {
                return false;
            }

            var item = apiResult.Value.Single();
            _customersUserMst = item;

            // 削除済チェックボックス
            chkDeleted.Checked = item.Deleted ?? false;

            // 顧客
            var customersMst = apiResultCust.Value.Single();
            _custCode = item.CustCode;
            txtCustName.Text = CreateItemInfo(customersMst.CustCode, customersMst.CustName);

            // 顧客担当者(キーマン)
            txtCustUserCode.Text = item.CustUserCode;
            txtCustUserName.Text = item.CustUserName;
            txtCustUserEmail.Text = item.CustUserEmail;

            return true;
        }


        /// <summary>
        /// テキストの入力制限
        /// </summary>
        private void SetInputLimit()
        {
            txtCustUserCode.KeyPress += FormHelper.KeyPressToNallowChar;   // 半角
            txtCustUserCode.KeyPress += FormHelper.KeyPressToUpperCase;    // 大文字

            txtCustUserEmail.KeyPress += FormHelper.KeyPressForEmail;
        }

        private void ClearInputs()
        {
            foreach (var control in FormHelper.GetAllControls(this).OfType<TextBox>())
            {
                control.Text = string.Empty;
            }

            foreach (ComboBox control in FormHelper.GetAllControls(this).OfType<ComboBox>())
            {
                control.SelectedIndex = -1;
            }
        }

        /// <summary>各コンボボックスの初期化</summary>
        private void InitializeComboBoxes()
        {
            //// 仕入先都道府県
            //var prefectures = JsonConvert.DeserializeObject<Prefecture[]>(Resources.prefectures);
            //comboBox11.DisplayMember = "Name";
            //comboBox11.ValueMember = "Name";
            //comboBox11.Items.AddRange(prefectures);
        }

        private void Clear()
        {
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            using (var form = new MS_Customer())
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    var result = form.CustomersMst;
                    _custCode = result.CustCode;

                    txtCustName.Text = CreateItemInfo(result.CustCode, result.CustName);
                }
            }
        }

        private async void btnCheckDuplicate_Click(object sender, EventArgs e)
        {
            string custCode = _custCode;
            if (string.IsNullOrEmpty(custCode))
            {
                DialogHelper.InputRequireMessage(this, "顧客");
                return;
            }

            string cuserUserCode = txtCustUserCode.Text;
            if (string.IsNullOrEmpty(cuserUserCode))
            {
                DialogHelper.InputRequireMessage(this, "顧客担当者(キーマン)CD");
                return;
            }

            var result = await IsKeymanCodeUsedAsync(custCode, cuserUserCode);
            if (result.Failed) { return; }

            string message;
            if (result.Value)
            {
                message = "顧客担当者(キーマン)CDは使用されています。";
            }
            else
            {
                message = "顧客担当者(キーマン)CDは使用可能です。";
            }
            DialogHelper.InformationMessage(this, message);
        }
    }
}
