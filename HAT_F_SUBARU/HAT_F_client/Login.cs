using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using System.Reflection;
using System.Security.Claims;
using HatFClient.Repository;
using HAT_F_api.Models;
using Newtonsoft.Json.Linq;
using HAT_F_api.CustomModels;
using HatFClient.Common;

namespace HatFClient
{
    public partial class Login : Form
    {
        //string strTitle = @"一貫化Ｖ３";
        string strDesc = @"※動作保証OS：Windows10 21H2、Windows10 22H2";

        private const string AES_IV = @"pf69DL6GrWFyZcMK";
        private const string AES_Key = @"9Fix4L4HB4PKeKWY";
        private static string getRegistryValue(string keyname, string valuename)
            => Registry.GetValue(keyname, valuename, "").ToString();
        private static string GetProducutName { get; }
            = getRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
        private static string GetDisplayVersion { get; }
            = getRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion");

        private LoginRepo loginRepo = null;

        public Login()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this, true);
                FormStyleHelper.SetDefaultColor(this);

                // ログイン画面は親画面関係なく画面中央としておく
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //this.lblTitle.Text = strTitle;
            this.lblVersion.Text = "バージョン：" + ApplicationHelper.GetAppVersionString();
            this.lblDesc.Text = strDesc;
            this.lblOS.Text = $"（この端末のOS情報：{GetOSInfo()}）";

            this.txtEmpCode.Clear();
            this.txtPass.Clear();
            this.chkLogin.Checked = false;

            if (Properties.Settings.Default.login_flg)
            {
                this.txtEmpCode.Text = Decrypt(Properties.Settings.Default.login_id, AES_IV, AES_Key);

#if DEBUG
                this.Text += " [DEBUG]";
                this.lblPassword.Text += " (DEBUGビルドのみ前回パスワードが設定されます)";
                this.txtPass.Text = Decrypt(Properties.Settings.Default.login_pass, AES_IV, AES_Key);
#endif

                this.chkLogin.Checked = true;
                this.ActiveControl = this.btnLogin;
            }

            loginRepo = LoginRepo.GetInstance();
        }

        private async void BtnLogin_Click(object sender, System.EventArgs e)
        {
            // IDが空かどうかチェック
            if (string.IsNullOrEmpty(this.txtEmpCode.Text))
            {
                DialogHelper.InputRequireMessage(this, "社員番号");
                txtEmpCode.Focus();
                return;
            }

            // パスワードが空かどうかチェック
            if (string.IsNullOrEmpty(this.txtPass.Text))
            {
                DialogHelper.InputRequireMessage(this, "パスワード");
                txtPass.Focus();
                return;
            }

            string id = this.txtEmpCode.Text;
            string pass = this.txtPass.Text;
            ApiResult<HatFLoginResult> apiResult = await ApiHelper.FetchAsync<HatFLoginResult>(this, async () => {
                return await loginRepo.DoAuth(id, pass);
            });

            if (apiResult.Failed)
            {
                // 戻り値がfalseの場合はAPI呼出の失敗 or ログイン認証エラーなので処理を切り上げます。
                // もう一度ボタンを押せるように画面は閉じない
                return;
            }

            // API の呼出は成功、ログインの成否を判定する
            HatFLoginResult loginResult = apiResult.Value;
            if (!loginResult.LoginSucceeded)
            {
                DialogHelper.WarningMessage(this, apiResult.ApiErrorResponse.Errors.StatusMessage);
                return;
            }

            loginRepo.CurrentUser = loginResult;

            // ログイン情報の保存
            string password = "";
#if DEBUG
            password = this.txtPass.Text;
#endif
            SetSettingData(loginResult, password);

            this.Hide();

            // メイン画面をメニュー画面に変更
            var menuForm = new Menu();
            menuForm.Show();
            Program.AppContext.MainForm = menuForm;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetSettingData(HatFLoginResult loginResult, string savePassword)
        {
            if (this.chkLogin.Checked)
            {
                Properties.Settings.Default.login_id = Encrypt(loginResult.EmployeeCode, AES_IV, AES_Key);

                // 認証APIはパスワードを返さない
                Properties.Settings.Default.login_pass = Encrypt(savePassword, AES_IV, AES_Key);
                Properties.Settings.Default.login_user = loginResult.EmployeeName;
                Properties.Settings.Default.login_flg = true;
            }
            else
            {
                Properties.Settings.Default.login_id = @"";
                Properties.Settings.Default.login_pass = @"";
                Properties.Settings.Default.login_user = @"";
                Properties.Settings.Default.login_flg = false;
            }
            Properties.Settings.Default.Save();
        }
        private string GetOSInfo()
        {
            return GetProducutName + @" " + GetDisplayVersion;
        }

        /// <summary>
        /// 対称鍵暗号を使って文字列を暗号化する
        /// </summary>
        /// <param name="text">暗号化する文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string text, string iv, string key)
        {
            string strRet = "";
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                byte[] encrypted;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(ctStream))
                        {
                            sw.Write(text);
                        }
                        encrypted = mStream.ToArray();
                    }
                }
                strRet = System.Convert.ToBase64String(encrypted);
            }
            return strRet;
        }

        /// <summary>
        /// 対称鍵暗号を使って暗号文を復号する
        /// </summary>
        /// <param name="cipher">暗号化された文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>復号された文字列</returns>
        public static string Decrypt(string cipher, string iv, string key)
        {
            string plain = string.Empty;
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                using (MemoryStream mStream = new MemoryStream(System.Convert.FromBase64String(cipher)))
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(ctStream))
                        {
                            plain = sr.ReadLine();
                        }
                    }
                }
            }
            return plain;
        }
    }
}

