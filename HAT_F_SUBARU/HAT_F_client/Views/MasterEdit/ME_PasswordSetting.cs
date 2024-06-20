using HatFClient.Common;
using HatFClient.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    public partial class ME_PasswordSetting : Form
    {
        private int _empId = default;
        private bool _isAdminMode = default;

        public ME_PasswordSetting() : this(default, default, default)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="empId">社員ID</param>
        /// <param name="empCode">社員番号</param>
        /// <param name="isAdminMode">管理者変更ならtrue、自身で変更ならfalse</param>
        public ME_PasswordSetting(int empId, string empCode, bool isAdminMode)
        {
            InitializeComponent();

            if (!this.DesignMode) 
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                _empId = empId;
                _isAdminMode = isAdminMode;
                
                txtEmpCode.Text = empCode;
                txtEmpCode.ReadOnly = true;

                txtCurrentPassword.ReadOnly = isAdminMode;
                txtCurrentPassword.Text = "********";
            }
        }
        private void ME_PasswordSetting_Load(object sender, EventArgs e)
        {
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            {
                string url = _isAdminMode ? ApiResources.HatF.Login.LoginPasswordAdmin : ApiResources.HatF.Login.LoginPassword;

                string currentPassword = txtCurrentPassword.Text;
                string newPassword = txtNewPassword.Text;

                //var conditions = new { empId = _empId, newPassword = newPassword, currentPassword = currentPassword, isAdminMode = _isAdminMode };
                var conditions = new Dictionary<string, object>() { { "empId", _empId }, { "newPassword", newPassword } };
                if (!_isAdminMode)
                {
                    conditions.Add("currentPassword", currentPassword);
                }

                var apiResponse = await Program.HatFApiClient.PutAsync<int>(resource: url, queryParameters: conditions);
                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool ValidateInputs()
        {
            if (!_isAdminMode)
            {
                if (string.IsNullOrEmpty(txtCurrentPassword.Text))
                {
                    DialogHelper.InputRequireMessage(this, "現在のパスワード");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                DialogHelper.InputRequireMessage(this, "新しいパスワード");
                return false;
            }

            if (string.IsNullOrEmpty(txtNewPasswordConfirrm.Text))
            {
                DialogHelper.InputRequireMessage(this, "新しいパスワード (確認)");
                return false;
            }

            if (!IsSameInputPasswords())
            {
                DialogHelper.WarningMessage(this, "新しいパスワードが一致していません。");
                return false;
            }

            return true;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            PasswordCharChanged();
        }

        private void txtNewPasswordConfirrm_TextChanged(object sender, EventArgs e)
        {
            PasswordCharChanged();
        }

        private void PasswordCharChanged()
        {
            if (string.IsNullOrEmpty(txtNewPasswordConfirrm.Text))
            {
                lblPasswordNotMatchWarning.Visible = false;
                return;
            }

            lblPasswordNotMatchWarning.Visible = !IsSameInputPasswords();
        }

        private bool IsSameInputPasswords()
        {
            string password1 = txtNewPassword.Text;
            string password2 = txtNewPasswordConfirrm.Text;

            bool isSame = password1 == password2;
            return isSame;
        }
    }
}
