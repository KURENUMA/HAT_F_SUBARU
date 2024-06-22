using AutoMapper;
using AutoMapper.Internal;
using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Models;
using HatFClient.Properties;
using HatFClient.Repository;
using HatFClient.Views.MasterSearch;
using NetOffice.OutlookApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    // TODO キーマン関係のマッピング不明
    // TODO 顧客支払月のコンボボックスは、選択肢の内容的に顧客支払日だと思われる

    /// <summary>工事店詳細画面</summary>
    public partial class ME_CustomersMstDetail : Form
    {
        private const int UnsavedCustSubNo = -1;

        /// <summary>社員ID</summary>
        private string _custCode;

        /// <summary>
        /// キーマン一覧グリッド用
        /// </summary>
        private class CustomersMstViewModel : CustomersMstEx
        {
            public bool Checked { get; set; }
            public string NewOrExists { get; set; }
        }

        private BindingList<CustomersMstViewModel> _customersMst;

        private bool IsUpdateMode 
        {
            get { return !string.IsNullOrEmpty(_custCode); }
        }

        /// <summary>コンストラクタ</summary>
        public ME_CustomersMstDetail() : this(null)
        {
            //InitializeComponent();

            //if (!this.DesignMode)
            //{
            //    FormStyleHelper.SetFixedSizeDialogStyle(this);
            //}
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="custCode">社員ID</param>
        public ME_CustomersMstDetail(string custCode)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                _custCode = custCode;
            }
        }

        /// <summary>OKボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void btnOK_Click(object sender, EventArgs e)
        {
            // 更新について
            // 1画面で複数枝番に対応している想定
            // 更新時、全枝番を更新する。キーマン以外は同じ値で更新。

            TrimControls();

            // 入力チェック（クライアント）
            if (!ValidateInput())
            {
                return;
            }

            // 入力チェック（サーバー）
            var serverValidation = await ValidateInputWithServerAsync();
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

            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, _custCode, "");
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

        private DataTable ToDataTable<T>(IEnumerable<T> source)
        {
            var dt = new DataTable();
            dt.BeginInit();

            var props = typeof(T).GetProperties();
            foreach (PropertyInfo pi in props)
            {
                //dt.Columns.Add(pi.Name, pi.PropertyType);
                dt.Columns.Add(pi.Name);
            }

            dt.BeginLoadData();
            foreach(T item in source)
            {
                var row = dt.NewRow();
                foreach(PropertyInfo pi in props)
                {
                    object val = pi.GetValue(item);
                    row[pi.Name] = val;
                }
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();
            dt.EndLoadData();
            dt.EndInit();

            return dt;
        }

        private bool ValidateInput()
        {
            if (!this.IsUpdateMode)
            {
                // メモ
                // 顧客(工事店)マスタには、得意先(取引先)マスタのレコード(コード6桁)も格納されていますが
                // 得意先の新規登録は得意先マスタ編集画面から行います。
                // 得意先のレコードは更新のみ可とします

                if (txtCustCode.Text.Length < txtCustCode.MaxLength)
                {
                    DialogHelper.WarningMessage(this, $"工事店コードを{txtCustCode.MaxLength}桁入力してください。");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtCustName.Text))
            {
                DialogHelper.InputRequireMessage(this, "工事店名");
                return false;
            }

            if (txtAr.Tag == null)
            {
                DialogHelper.InputRequireMessage(this, "請求先");
                return false;
            }

            // キーマン・空行の除去
            var empties = new List<CustomersMstViewModel>();
            for (int i = 0; i < c1gKeymen.Rows.Count; i++)
            {
                var item = c1gKeymen.Rows[i].DataSource as CustomersMstViewModel;
                if (item == null) { continue; }

                // TODO: DB変更対応
                //if (item.CustSubNo < 0
                //    && string.IsNullOrWhiteSpace(item.KeymanCode)
                //    && string.IsNullOrWhiteSpace(item.CustUserName))
                //{
                //    // 新規行で キーマンCDとキーマン名が空は削除対象
                //    empties.Add(item);
                //}
            }
            empties.ForEach(x => _customersMst.Remove(x));

            if (!chkDeleted.Checked)
            {
                // 削除済でないキーマンがいるか
                if (!_customersMst.Where(x => x.Deleted == false).Any())
                {
                    DialogHelper.WarningMessage(this, "キーマンを1人以上有効にしてください。");
                    FlexGridFocus(c1gKeymen, 1, nameof(CustomersMstViewModel.Deleted));
                    return false;
                }
            }
            else
            {
                if (false == DialogHelper.OkCancelQuestion(this, "工事店を無効化するため、キーマンもすべて無効化してよろしいですか?"))
                {
                    return false;
                }

                foreach (var item in _customersMst)
                {
                    item.Deleted = true;
                }
            }

            // キーマン入力チェック
            for (int i = 0; i < c1gKeymen.Rows.Count; i++)
            {
                var item = c1gKeymen.Rows[i].DataSource as CustomersMstViewModel;
                if (item == null) { continue; }

                //if (string.IsNullOrWhiteSpace(item.KeymanCode))
                //{
                //    FlexGridFocus(c1gKeymen, i, nameof(CustomersMstViewModel.KeymanCode));
                //    DialogHelper.InputRequireMessage("キーマンCD");
                //    return false;
                //}

                if (string.IsNullOrWhiteSpace(item.CustUserName))
                {
                    FlexGridFocus(c1gKeymen, i, nameof(CustomersMstViewModel.CustUserName));
                    DialogHelper.InputRequireMessage(this, "キーマン名");
                    return false;
                }
            }

            return true;
        }




        private void FlexGridFocus(C1FlexGrid grid, int row, string colName)
        {
            // フォーカス
            grid.Row = row;
            grid.Col = c1gKeymen.Cols[colName].Index;

            // 見える位置調整
            grid.TopRow = grid.Row;
            grid.LeftCol = 0;
        }

        private async Task<MethodResult<bool>> ValidateInputWithServerAsync() 
        {
            if (!this.IsUpdateMode)
            {
                string custCode = txtCustCode.Text;
                var result = await IsCustCodeUsedAsync(custCode);
                if (result.Failed) { return MethodResult<bool>.FailedResult; }

                if (result.Value)
                {
                    DialogHelper.InformationMessage(this, "工事店コードは使用されています。");
                    return new MethodResult<bool>(false);
                }
            }

            return new MethodResult<bool>(true);
        }

        private async Task<MethodResult<bool>> IsCustCodeUsedAsync(string custCode) 
        {
            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, custCode, null, null);
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CustomersMstEx>>(url);
            });

            if (apiResult.Failed)
            {
                return MethodResult<bool>.FailedResult;
            }

            bool isUsed = apiResult.Value.Any();
            return new MethodResult<bool>(isUsed);
        }


        private void TrimControls()
        {
            var controls = FormHelper.GetAllControls(this);

            foreach(TextBox control in controls.OfType<TextBox>())
            {
                control.Text = control.Text.Trim();
            }
        }

        /// <summary>キャンセルボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
                if (!await SetEditDataAsync(_custCode))
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                txtCustCode.Enabled = false;
                btnChkDuplicate.Enabled = false;
            }
        }

        /// <summary>
        /// テキストの入力制限
        /// </summary>
        private void SetInputLimit()
        {
            txtCustCode.KeyPress += FormHelper.KeyPressForNumber;
            txtCustKana.KeyPress += FormHelper.KeyPressForKanaName;
            txtCustTel.KeyPress += FormHelper.KeyPressForTelFax;
            txtCustFax.KeyPress += FormHelper.KeyPressForTelFax;
            txtCustEmail.KeyPress += FormHelper.KeyPressForEmail;
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

        private async Task<bool> SetEditDataAsync(string custCode)
        {
            string custSubNo = "";
            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, custCode, custSubNo);
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CustomersMstEx>>(url);
            });

            if (apiResult.Failed || apiResult.Value.Count == 0) 
            {
                return false;
            }

            // 画面用の項目追加オブジェクトにコピー
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomersMstEx, CustomersMstViewModel>());
            var mapper = new Mapper(config);
            var data = mapper.Map<List<CustomersMstViewModel>>(apiResult.Value);

            _customersMst = new BindingList<CustomersMstViewModel>(data);

            // 取得した工事店レコードが複数の場合、キーマン部分以外は同じ工事店を表す
            var item = _customersMst.First();

            // 削除済チェックボックス
            bool existsEnabledKeymen = _customersMst.Where(x => x.Deleted == false).Any();  //有効キーマンいる？
            chkDeleted.Checked = !existsEnabledKeymen;

            txtCustCode.Text = item.CustCode;
            txtCustName.Text = item.CustName;
            txtCustKana.Text = item.CustKana;

            if (!string.IsNullOrEmpty(item.ArCode))
            {
                txtAr.Tag = item.ArCode;
                txtAr.Text = $"{item.ArName} ({item.ArCode})";
            }

            if (!string.IsNullOrEmpty(item.EmpCode))
            {
                txtHatFEmployee.Tag = item.EmpCode;
                txtHatFEmployee.Text = $"{item.EmpName} ({item.EmpCode})";
            }

            // キーマンはグリッドで表示
            //txtCustUserName.Text = item.CustUserName;

            txtCustUserDepName.Text = item.CustUserDepName;
            txtCustZipCode.SelectedText = item.CustZipCode;
            cmbCustState.SelectedValue = item.CustState;
            txtCustAddress1.Text = item.CustAddress1;
            txtCustAddress2.Text = item.CustAddress2;
            txtCustAddress3.Text = item.CustAddress3;
            txtCustTel.Text = item.CustTel;
            txtCustFax.Text = item.CustFax;
            txtCustEmail.Text = item.CustEmail;

            gbxBilling.Enabled = (item.CustCode == item.ArCode);
            if (gbxBilling.Enabled)
            {
                SetComboBoxValue(cmbCustArFlag, item.CustArFlag);

                SetComboBoxValue(cmbCustCloseDay1, item.CustCloseDate1);
                SetComboBoxValue(cmbCustPayMonth1, item.CustPayDates1);
                SetComboBoxValue(cmbCustPayMethod1, item.CustPayMethod1);

                SetComboBoxValue(cmbCustCloseDay2, item.CustCloseDate2);
                SetComboBoxValue(cmbCustPayMonth2, item.CustPayDates2);
                SetComboBoxValue(cmbCustPayMethod2, item.CustPayMethod2);
            }

            // キーマン表示グリッド
            c1gKeymen.DataSource = _customersMst;

            return true;
        }
        
        private List<CustomersMst> GetFormData()
        {
            var formData = (BindingList<CustomersMst>)c1gKeymen.DataSource;
            foreach(CustomersMst customersMst in formData)
            {
                GetFormData(customersMst);
            }
            return formData.ToList();
        }

        private void GetFormData(CustomersMst item)
        {
            item.Deleted = chkDeleted.Checked;

            item.CustCode = txtCustCode.Text;
            item.CustName = txtCustName.Text;
            item.CustKana = txtCustKana.Text;
            
            item.ArCode = (string)txtAr.Tag;
            item.EmpCode = (string)txtHatFEmployee.Tag;
            
           // item.CustUserName = txtCustUserName.Text;
            item.CustUserDepName = txtCustUserDepName.Text;
            item.CustZipCode = txtCustZipCode.SelectedText;
            item.CustState = (string)cmbCustState.SelectedValue;
            item.CustAddress1 = txtCustAddress1.Text;
            item.CustAddress2 = txtCustAddress2.Text;
            item.CustAddress3 = txtCustAddress3.Text;
            item.CustTel = txtCustTel.Text;
            item.CustFax = txtCustFax.Text;
            item.CustEmail = txtCustEmail.Text;

            if (gbxBilling.Enabled)
            {
                item.CustArFlag = (short?)cmbCustArFlag.SelectedValue;

                item.CustCloseDate1 = (short?)cmbCustCloseDay1.SelectedValue;
                item.CustPayDates1 = (short?)cmbCustPayMonth1.SelectedValue;
                item.CustPayMethod1 = (short?)cmbCustPayMethod1.SelectedValue;

                item.CustCloseDate2 = (short?)cmbCustCloseDay2.SelectedValue;
                item.CustPayDates2 = (short?)cmbCustPayMonth2.SelectedValue;
                item.CustPayMethod2 = (short?)cmbCustPayMethod2.SelectedValue;
            }
        }


        private void SetComboBoxValue(ComboBox comboBox, object value)
        {
            if(comboBox.Items.Count == 0)
            {
                return;
            }

            if (value == null || value == DBNull.Value)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    object val = comboBox.Items[i];
                    if (val == null || val == DBNull.Value || string.IsNullOrEmpty(val.ToString()))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }

                comboBox.SelectedIndex = 0;
            }
            else
            {
                comboBox.SelectedValue = value;
            }
        }


        /// <summary>各コンボボックスの初期化</summary>
        private void InitializeComboBoxes()
        {
            // 仕入先都道府県
            var prefectures = JsonConvert.DeserializeObject<Prefecture[]>(Resources.prefectures);
            cmbCustState.DisplayMember = "Name";
            cmbCustState.ValueMember = "Name";
            cmbCustState.Items.AddRange(prefectures);

            // 請求区分
            var arFlags = JsonConvert.DeserializeObject<CodeName<short?>[]>(Resources.HatF_CustArFlag);
            cmbCustArFlag.DisplayMember = "Name";
            cmbCustArFlag.ValueMember = "Code";
            cmbCustArFlag.DataSource = arFlags;

            // 締日
            var closeDay = JsonResources.CloseDates;
            cmbCustCloseDay1.DisplayMember = "Name";
            cmbCustCloseDay1.ValueMember = "Code";
            cmbCustCloseDay1.DataSource = closeDay;
            cmbCustCloseDay2.DisplayMember = "Name";
            cmbCustCloseDay2.ValueMember = "Code";
            cmbCustCloseDay2.DataSource = closeDay;

            // 支払月
            var payMonths = HatF_PaymentMonthRepo.GetInstance().Entities;
            cmbCustPayMonth1.DisplayMember = "Name";
            cmbCustPayMonth1.ValueMember = "Key";
            cmbCustPayMonth1.DataSource = payMonths;
            cmbCustPayMonth2.DisplayMember = "Name";
            cmbCustPayMonth2.ValueMember = "Key";
            cmbCustPayMonth2.DataSource = payMonths;

            // 支払日
            var payDays = HatF_PaymentDayRepo.GetInstance().Entities;
            cmbCustPayDay1.DisplayMember = "Name";
            cmbCustPayDay1.ValueMember = "Key";
            cmbCustPayDay1.DataSource = payDays;
            cmbCustPayDay2.DisplayMember = "Name";
            cmbCustPayDay2.ValueMember = "Key";
            cmbCustPayDay2.DataSource = payDays;

            // 支払方法
            var payClasses = HatF_PaymentClassificationRepo.GetInstance().Entities;
            cmbCustPayMethod1.DisplayMember = "Name";
            cmbCustPayMethod1.ValueMember = "Key";
            cmbCustPayMethod1.DataSource = payClasses;
            cmbCustPayMethod2.DisplayMember = "Name";
            cmbCustPayMethod2.ValueMember = "Key";
            cmbCustPayMethod2.DataSource = payClasses;
        }

        private void btnSelectBilling_Click(object sender, EventArgs e)
        {
            using (var form = new MS_Tokui())
            {
                form.TxtTEAM_CD = "";
                form.TxtTOKUI_CD = "";

                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    string code = form.StrMsTokuiCode;
                    string name = form.StrMsTokuiName;

                    txtAr.Tag = code;
                    txtAr.Text = $"{name} ({code})";
                }
            }
        }

        private void btnSelectEmployee_Click(object sender, EventArgs e)
        {
            using(var form = new MS_Employee())
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    Employee employee = form.Employee;
                    txtHatFEmployee.Text = $"{employee.EmpName} ({employee.EmpCode})";
                }
            }
        }

        private void btnRemoveKeyman_Click(object sender, EventArgs e)
        {

        }

        private void btnAddKeyman_Click(object sender, EventArgs e)
        {
        }

        private void c1gKeymen_AfterAddRow(object sender, RowColEventArgs e)
        {
            // TODO: DB変更対応
            //c1gKeymen[c1gKeymen.Row, nameof(CustomersMstViewModel.CustSubNo)] = UnsavedCustSubNo;
            c1gKeymen[c1gKeymen.Row, nameof(CustomersMstViewModel.NewOrExists)] = "(追加)";
        }

        private async void btnChkDuplicate_Click(object sender, EventArgs e)
        {
            string custCode = txtCustCode.Text;
            if (string.IsNullOrWhiteSpace(custCode))
            {
                DialogHelper.InputRequireMessage(this, "工事店コード");
                return;
            }

            string custSubNo = "";
            var url = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, custCode, custSubNo);
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                return await Program.HatFApiClient.GetAsync<List<CustomersMstEx>>(url);
            });

            if (apiResult.Failed )
            {
                return;
            }

            string message = (apiResult.Value.Count > 0) ? "工事店コードは使用されています。" : "工事店コードは使用可能です。";
            DialogHelper.InformationMessage(this, message);

        }

        private void c1gKeymen_BeforeDeleteRow(object sender, RowColEventArgs e)
        {
            if (e.Row >= c1gKeymen.Rows.Count)
            {
                return;
            }
            if (c1gKeymen.Rows[e.Row].IsNew)
            {
                return;
            }

            // TODO: DB変更対応
            //short subNo = (short)c1gKeymen[c1gKeymen.Row, nameof(CustomersMstViewModel.CustSubNo)];
            //if (subNo != UnsavedCustSubNo)
            //{
            //    // 新規に追加した行以外は消させない
            //    e.Cancel = true;
            //}
        }
    }


}
