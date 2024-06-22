﻿using HAT_F_api.CustomModels;
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
    public partial class ME_DestinaitonsMstDetail : Form
    {
        private string _custCode;
        private string _genbaCode;

        private DestinationsMst _destinationsMst;

        /// <summary>コンストラクタ</summary>
        public ME_DestinaitonsMstDetail() : this(null, null)
        {
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="employeeId">社員ID</param>
        public ME_DestinaitonsMstDetail(string custCode, string genbaCode)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                this.IsUpdateMode = !string.IsNullOrEmpty(custCode);
                if (!string.IsNullOrEmpty(custCode)) 
                {
                    _custCode = custCode;
                    _genbaCode = genbaCode;
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

            var formData = GetFormData();

            var url = ApiResources.HatF.MasterEditor.DestinationsMstPut;
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

        private DestinationsMst GetFormData()
        {
            DestinationsMst item;
            if (this.IsUpdateMode)
            {
                item = _destinationsMst;
            }
            else
            {
                item = new DestinationsMst();
                item.CustCode = _custCode ?? "";        // 顧客CD
                item.GenbaCode = txtGenbaCode.Text;     // 現場CD               
            }

            item.Deleted = chkDeleted.Checked;

            item.DistName1 = txtDistName1.Text;
            item.DistName2 = txtDistName2.Text;

            item.ZipCode = txtZipCode.Text;
            item.Address1 = txtAddress1.Text;
            item.Address2 = txtAddress2.Text;
            item.Address3 = txtAddress3.Text;

            item.DestTel = txtDestTel.Text;
            item.DestFax = txtDestFax.Text;

            item.Remarks = txtRemarks.Text;

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

            if (string.IsNullOrEmpty(txtGenbaCode.Text))
            {
                DialogHelper.InputRequireMessage(this, "現場コード");
                return false;
            }

            if (string.IsNullOrEmpty(txtDistName1.Text))
            {
                DialogHelper.InputRequireMessage(this, "出荷先名１");
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                DialogHelper.InputRequireMessage(this, "出荷先住所１");
                return false;
            }

            return true;
        }

        private async Task<MethodResult<bool>> ValidateInputWithServer()
        {
            if (!this.IsUpdateMode)
            {
                string custCode = _custCode;
                string genbaCode = txtGenbaCode.Text;

                var result = await IsGenbaCodeUsedAsync(custCode, genbaCode);
                if (result.Failed) { return MethodResult<bool>.FailedResult; }

                if (result.Value)
                {
                    DialogHelper.InformationMessage(this, "現場コードは使用されています。");
                    return new MethodResult<bool>(false);
                }
            }

            return new MethodResult<bool>(true);
        }

        private async Task<MethodResult<bool>> IsGenbaCodeUsedAsync(string custCode, string genbaCode)
        {
            string genbaCodeUrlEncoded = ApiHelper.UrlEncodeForWebApi(genbaCode);
            var url = string.Format(ApiResources.HatF.MasterEditor.DestinationsMst, custCode, genbaCodeUrlEncoded);

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
                if (!await SetEditDataAsync(_custCode, _genbaCode))
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                btnSelectCustomer.Enabled = false;
                txtGenbaCode.ReadOnly = true;
                btnCheckDuplicate.Enabled = false;
            }

        }

        private async Task<bool> SetEditDataAsync(string custCode,  string genbaCode)
        {
            // 出荷先マスタ(現場)
            string genbaCodeUrlEncoded = ApiHelper.UrlEncodeForWebApi(genbaCode);
            var url = string.Format(ApiResources.HatF.MasterEditor.DestinationsMst, custCode, genbaCodeUrlEncoded);
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<DestinationsMst>>(url);
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
            _destinationsMst = item;

            // 削除済チェックボックス
            chkDeleted.Checked = item.Deleted;

            // 顧客
            var customersMst = apiResultCust.Value.Single();
            txtCustName.Text = CreateItemInfo(customersMst.CustCode, customersMst.CustName);

            txtGenbaCode.Text = item.GenbaCode;
            //txtDistNo.Text = this.IsUpdateMode ? item.DistNo.ToString() : "(新規)";

            txtDistName1.Text = item.DistName1;
            txtDistName2.Text = item.DistName2;

            txtZipCode.Text = item.ZipCode;
            txtAddress1.Text = item.Address1;
            txtAddress2.Text = item.Address2;
            txtAddress3.Text = item.Address3;

            txtDestTel.Text = item.DestTel;
            txtDestFax.Text = item.DestFax;

            txtRemarks.Text = item.Remarks;

            return true;
        }


        /// <summary>
        /// テキストの入力制限
        /// </summary>
        private void SetInputLimit()
        {
            txtGenbaCode.KeyPress += FormHelper.KeyPressToNallowChar;   // 半角
            txtGenbaCode.KeyPress += FormHelper.KeyPressToUpperCase;    // 大文字

            txtZipCode.KeyPress += FormHelper.KeyPressForZipCode;
            txtDestTel.KeyPress += FormHelper.KeyPressForTelFax;
            txtDestFax.KeyPress += FormHelper.KeyPressForTelFax;
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

            string genbaCode = txtGenbaCode.Text;
            if (string.IsNullOrEmpty(genbaCode))
            {
                DialogHelper.InputRequireMessage(this, "現場コード");
                return;
            }

            var result = await IsGenbaCodeUsedAsync(custCode, genbaCode);
            if (result.Failed) { return; }

            string message;
            if (result.Value)
            {
                message = "現場コードは使用されています。";
            }
            else
            {
                message = "現場コードは使用可能です。";
            }
            DialogHelper.InformationMessage(this, message);
        }
    }
}
