using HatFClient.Common;
using System;
using System.Windows.Forms;

namespace HatFClient.Views.PersonalSettings
{
    /// <summary>個人設定画面</summary>
    public partial class PS_Main : Form
    {
        const string StrConNote = @"納品書No_[伝票番号] ご注文No_[客先注番]";

        /// <summary>コンストラクタ</summary>
        public PS_Main()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
            }
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void PS_Main_Load(object sender, EventArgs e)
        {
            this.lblLoginName.Text = $"（{Properties.Settings.Default.login_user}さんの設定）";
            if (Properties.Settings.Default.personal_on == 1)
            {
                rdoON.Checked = true;
            }
            else
            {
                rdoOFF.Checked = true;
            }
            SetOnOff();
            string strNote = Properties.Settings.Default.personal_note;
            if (strNote.Length == 0)
            {
                txtNOTE.Text = StrConNote;
            }
            else
            {
                txtNOTE.Text = strNote;
            }
            txtRateOver.Text = Properties.Settings.Default.interestrate_rate_over;
            txtRateUnder.Text = Properties.Settings.Default.interestrate_rate_under;
            txtSuryo.Text = HatFComParts.DoFormatN0(Properties.Settings.Default.interestrate_suryo_over);
            txtUriKin.Text = HatFComParts.DoFormatN0(Properties.Settings.Default.interestrate_uri_kin_over);
            chkUriTanZero.Checked = Properties.Settings.Default.interestrate_uri_tan_zero;
        }

        /// <summary>受発注タブのラジオボタンに応じて画面を更新する</summary>
        private void SetOnOff()
        {
            if (rdoON.Checked)
            {
                lblOnOff.Text = @"この機能を有効にする";
                txtNOTE.Enabled = true;
            }
            else
            {
                lblOnOff.Text = @"この機能を無効にする";
                txtNOTE.Enabled = false;
            }

        }

        /// <summary>コピーを試すボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnTryCopy_Click(object sender, System.EventArgs e)
        {
            string strDesc = @"伝票番号と客注番号をコピーしました。" + "\r\n";
            if (txtNOTE.Text.Length > 0)
            {
                string strNote = txtNOTE.Text;
                Clipboard.SetText(strNote);
                MessageBox.Show(strDesc + strNote, @"個人設定");
            }
        }

        /// <summary>初期状態へ戻すボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnDefault_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"初期状態へ戻しますか？", @"個人設定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            txtNOTE.Text = StrConNote;
        }

        /// <summary>決定ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnConfirm_Click(object sender, System.EventArgs e)
        {
            // テキストボックスにShift+Insertで文字列を張り付けることも可能なためバリデーションを行う
            if (!ValidateInterestRate())
            {
                tabSettings.SelectedTab = tabInterestRate;
                return;
            }
            if (rdoON.Checked)
            {
                Properties.Settings.Default.personal_on = 1;
            }
            else
            {
                Properties.Settings.Default.personal_on = 0;
            }
            Properties.Settings.Default.personal_note = txtNOTE.Text;
            Properties.Settings.Default.interestrate_rate_over = txtRateOver.Text.Replace(",", string.Empty);
            Properties.Settings.Default.interestrate_rate_under = txtRateUnder.Text.Replace(",", string.Empty);
            Properties.Settings.Default.interestrate_suryo_over = txtSuryo.Text.Replace(",", string.Empty);
            Properties.Settings.Default.interestrate_uri_kin_over = txtUriKin.Text.Replace(",", string.Empty);
            Properties.Settings.Default.interestrate_uri_tan_zero = chkUriTanZero.Checked;
            Properties.Settings.Default.Save();
            DialogHelper.InformationMessage(this, "個人設定を保存しました。");
            DialogResult = DialogResult.OK;
        }

        /// <summary>ON/OFFのラジオボタン変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void rdoON_CheckedChanged(object sender, EventArgs e)
        {
            SetOnOff();
        }

        /// <summary>利率異常チェックタブの内容を検証する</summary>
        /// <returns>成否</returns>
        private bool ValidateInterestRate()
        {
            // 利率
            if (!ValidateInt(txtRateOver.Text))
            {
                DialogHelper.WarningMessage(this, "利率は数値で入力してください。");
                return false;
            }
            if (!ValidateInt(txtRateUnder.Text))
            {
                DialogHelper.WarningMessage(this, "利率は数値で入力してください。");
                return false;
            }
            // 数量
            if (!ValidateInt(txtSuryo.Text))
            {
                DialogHelper.WarningMessage(this, "数量は数値で入力してください。");
                return false;
            }
            // 金額
            if (!ValidateInt(txtUriKin.Text))
            {
                DialogHelper.WarningMessage(this, "金額は数値で入力してください。");
                return false;
            }
            return true;
        }

        /// <summary>数値項目の検証</summary>
        /// <param name="value">検証対象文字列</param>
        /// <returns></returns>
        private bool ValidateInt(string value)
        {
            value = value.Replace(",", string.Empty);
            return string.IsNullOrEmpty(value.Trim()) || int.TryParse(value.Trim(), out var _);
        }
    }
}
