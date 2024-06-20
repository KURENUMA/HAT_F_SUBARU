using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    // TODO comboBox3とcheckedListBox1がマッピング不明

    /// <summary>社員詳細画面</summary>
    public partial class ME_EmployeeDetail : Form
    {
        /// <summary>社員ID</summary>
        private int? _employeeId;

        private Employee _employee;

        private bool IsUpdateMode
        {
            get 
            {
                return _employeeId.HasValue;
            }
        }


        /// <summary>コンストラクタ</summary>
        public ME_EmployeeDetail() : this(null)
        {
            //InitializeComponent();

            //if (!this.DesignMode)
            //{
            //    FormStyleHelper.SetFixedSizeDialogStyle(this);
            //}
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="employeeId">社員ID</param>
        public ME_EmployeeDetail(int? employeeId)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
                this.ClearInputs();

                ////test
                //employeeId = 90;

                _employeeId = employeeId;
                this.Text += employeeId.HasValue ? " (修正)" : " (新規)";
            }
        }

        /// <summary>OKボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsValidInput())
            {
                return;
            }

            if (!await SaveDataAsync())
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private async Task<bool> SaveDataAsync()
        {
            List<UserAssignedRole> roles = new List<UserAssignedRole>();
            foreach (string item in clbSelectedRole.CheckedItems)
            {
                string[] parts = item.Split(':');
                roles.Add(new UserAssignedRole() { EmpId = _employeeId ?? 0, UserRoleCd = parts.First() });
            }

            // 更新モードではデータ表示時に取得したレコードを再利用（画面にない項目も返却したい）
            Employee saveEmployee = this.IsUpdateMode ? _employee : new Employee();

            saveEmployee.Deleted = chkDeleted.Checked;
            saveEmployee.EmpCode = txtEmpCode.Text;
            saveEmployee.EmpName = txtEmpName.Text;
            saveEmployee.EmpKana = txtEmpKana.Text;
            saveEmployee.EmpTag = txtEmpTag.Text;
            saveEmployee.Tel = txtTel.Text;
            saveEmployee.Fax = txtFax.Text;
            saveEmployee.Email = txtEmail.Text;
            saveEmployee.DeptCode = (string)cmbDeptCode.SelectedValue;
            saveEmployee.OccuCode = cmbOccuCode.Text;
            saveEmployee.TitleCode = cmbTitle.Text;

            string url = ApiResources.HatF.MasterEditor.EmployeeUserAssignedRole;
            var data = new { Employee = saveEmployee, UserAssignedRoles = roles };

            var result = await ApiHelper.UpdateAsync(this, async () => {
                var apiResponse = await Program.HatFApiClient.PutAsync<int>(url, data);
                return apiResponse;
            });

            return result.Successed;
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
            if (false == await InitListItems())
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            SetInputLimit();
            ClearInputs();

            if (this.IsUpdateMode)
            {
                //txtEmpCode.ReadOnly = true;
                //btnChkDuplicate.Enabled = false;

                // 社員情報を画面にセット
                if (false == await SetFormData(_employeeId.Value))
                {
                    DialogResult = DialogResult.Cancel;
                    return;
                }
            }
        }

        private void SetInputLimit()
        {
            txtEmpTag.KeyPress += FormHelper.KeyPressForUpperAlpha;
            txtEmpCode.KeyPress += FormHelper.KeyPressForNumber;
            txtTel.KeyPress += FormHelper.KeyPressForTelFax;
            txtFax.KeyPress += FormHelper.KeyPressForTelFax;
            txtEmail.KeyPress += FormHelper.KeyPressForEmail;
        }

        private void ClearInputs()
        {
            chkDeleted.Checked = false;
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtEmpKana.Text = "";
            txtEmpTag.Text = "";
            txtTel.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";

            cmbDeptCode.Text = "";
            cmbOccuCode.Text = "正社員";
            cmbTitle.Text = "担当";

            //clbSelectedRole.CheckedItems
        }

        private async Task<bool> InitListItems() 
        {
            {
                string url = ApiResources.HatF.MasterEditor.DivUserRole;
                var result = await ApiHelper.FetchAsync(this, async () => {
                    var apiResponse = await Program.HatFApiClient.GetAsync<List<DivUserRole>>(url);
                    return apiResponse;
                });
                if (result.Failed)
                {
                    return false;
                }

                clbSelectedRole.BeginUpdate();
                clbSelectedRole.Items.Clear();
                foreach (var item in result.Value)
                {
                    string listItem = $"{item.UserRoleCd}:{item.UserRoleName}";
                    clbSelectedRole.Items.Add(listItem);
                }
                clbSelectedRole.EndUpdate();
            }


            {
                string url = ApiResources.HatF.MasterEditor.DeptMst;
                var result = await ApiHelper.FetchAsync(this, async () => {
                    var apiResponse = await Program.HatFApiClient.GetAsync<List<DeptMst>>(url);
                    return apiResponse;
                });
                if (result.Failed)
                {
                    return false;
                }

                cmbDeptCode.BeginUpdate();
                cmbDeptCode.Items.Clear();
                cmbDeptCode.DisplayMember = nameof(DeptMst.DepName);
                cmbDeptCode.ValueMember = nameof(DeptMst.DeptCode);
                cmbDeptCode.DataSource = result.Value;
                //foreach (var item in result.Value)
                //{
                //    string listItem = $"{item.UserRoleCd}:{item.UserRoleName}";
                //    clbSelectedRole.Items.Add(listItem);
                //}
                cmbDeptCode.EndUpdate();
            }

            return true;

        }

        private void cboTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async void cboTitle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!this.IsUpdateMode)
            {
                string titleCode = cmbTitle.Text;
                await SetTitleDefaultRole(titleCode);
            }

        }

        private async Task<bool> SetFormData(int employeeId)
        {
            // 社員
            var apiResult = await ApiHelper.FetchAsync<List<Employee>>(this, async () => {
                string url = ApiResources.HatF.MasterEditor.Emloyee;
                var conditions = new { EmployeeId = employeeId, includeDeleted = true };

                var apiResponse = await Program.HatFApiClient.GetAsync<List<Employee>>(
                    url,   // 一覧取得API
                    conditions);   // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return false;
            }

            // 社員のロール
            var apiResultRole = await ApiHelper.FetchAsync<List<UserAssignedRole>>(this, async () => {
                string url = ApiResources.HatF.MasterEditor.UserAssignedRole;
                var conditions = new { EmployeeId = employeeId };

                var apiResponse = await Program.HatFApiClient.GetAsync<List<UserAssignedRole>>(
                    url,   // 一覧取得API
                    conditions);   // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return false;
            }

            _employee = apiResult.Value.Single();
            chkDeleted.Checked = _employee.Deleted;
            txtEmpCode.Text = _employee.EmpCode;
            txtEmpName.Text = _employee.EmpName;
            txtEmpKana.Text = _employee.EmpKana;
            txtEmpTag.Text = _employee.EmpTag;
            txtTel.Text = _employee.Tel;
            txtFax.Text = _employee.Fax;
            txtEmail.Text = _employee.Email;
            cmbDeptCode.SelectedValue = _employee.DeptCode;
            cmbOccuCode.Text = _employee.OccuCode;
            cmbTitle.Text = _employee.TitleCode;


            // 割当権限のチェック付与
            var roles = apiResultRole.Value.Select(x => x.UserRoleCd).ToList();
            CheckRoles(roles);

            return true;
        }

        private async Task SetTitleDefaultRole(string titleCode)
        {

            var apiResult = await ApiHelper.FetchAsync<List<TitleDefaultRole>>(this, async () => {
                string url = ApiResources.HatF.MasterEditor.TitleDefaultRole;
                var conditions = new { TitleCode = titleCode };

                var apiResponse = await Program.HatFApiClient.GetAsync<List<TitleDefaultRole>>(
                    url,   // 一覧取得API
                    conditions);   // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return;
            }

            // 割当権限のチェック付与
            var roles = apiResult.Value.Select(x => x.UserRoleCd).ToList();
            CheckRoles(roles);
        }

        private void CheckRoles(IEnumerable<string> roles)
        {
            for (int i = 0; i < clbSelectedRole.Items.Count; i++)
            {
                string item = clbSelectedRole.Items[i].ToString();
                string cd = item.Split(':').First();

                bool exists = roles.Where(x => x == cd).Any();
                clbSelectedRole.SetItemChecked(i, exists);
            }
        }


        private async void btnResetRoles_Click(object sender, EventArgs e)
        {
            string titleCode = cmbTitle.Text;
            await SetTitleDefaultRole(titleCode);
        }


        private bool IsValidInput()
        {
            if (false == Require(txtEmpCode, "社員番号")) { return false; }
            if (false == Require(txtEmpName, "社員名")) { return false; }
            if (false == Require(cmbDeptCode, "部門")) { return false; }
            if (false == Require(cmbDeptCode, "職種")) { return false; }
            if (false == Require(cmbTitle, "役職")) { return false; }

            return true;

        }

        private bool Require(Control control, string displayName)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                DialogHelper.InputRequireMessage(this, displayName);
                control.Focus();
                return false;
            }
            return true;
        }

        private async void btnChkDuplicate_Click(object sender, EventArgs e)
        {
            string empCode = txtEmpCode.Text.Trim();
            var isDuplication = await IsEmpCodeDuplicationAync(empCode, _employeeId);

            if (isDuplication.Failed)
            {
                return;
            }

            string message = isDuplication.Value ? "社員番号は使用されています。" : "社員番号は使用可能です。";
            DialogHelper.InformationMessage(this, message);
        }


        private async Task<MethodResult<bool>> IsEmpCodeDuplicationAync(string empCode, int? employeeId)
        {
            var apiResult = await ApiHelper.FetchAsync<List<Employee>>(this, async () => {
                string url = ApiResources.HatF.MasterEditor.Emloyee;

                // 同一社員番号のデータを検索
                // 削除済は除外
                var conditions = new { EmpCode = empCode, includeDeleted = false };

                var apiResponse = await Program.HatFApiClient.GetAsync<List<Employee>>(
                    url,   // 一覧取得API
                    conditions);   // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return MethodResult<bool>.FailedResult;
            }

            // 無効化されていないレコードがあるか？
            bool isDuplicated = apiResult.Value.Where(x => !employeeId.HasValue || x.EmpId != employeeId).Any();
            return new MethodResult<bool>(isDuplicated);
        }



        //private void txtEmpTag_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //}

        //private void txtEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //}

        //private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //}

        //private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //}

        //private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //}
    }
}