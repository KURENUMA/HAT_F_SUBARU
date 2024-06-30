using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    /// <summary>得意先検索画面の体裁で顧客マスタを検索する画面</summary>
    public partial class MS_Tokui2 : Form
    {
        /// <summary>最大表示件数</summary>
        private const int IntShowMaxConunt = 200;

        /// <summary>得意先コード（顧客コード）</summary>
        public string CustCode { get; set; }

        /// <summary>得意先名（顧客名）</summary>
        public string CustName { get; set; }

        /// <summary>得意先名カナ（顧客名カナ）</summary>
        public string CustNameKana { get; set; }

        /// <summary>チームコード</summary>
        public string TeamCode { get; set; }

        /// <summary>コンストラクタ</summary>
        public MS_Tokui2()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this);
            }
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void Fm_Load(object sender, EventArgs e)
        {
            lblMaxCount.Text = $"最大{IntShowMaxConunt}件表示";
            txtCustCode.Text = CustCode;
            txtCustName.Text = CustName;
            txtCustKana.Text = CustNameKana;
            lblNote.Visible = false;
            dgvList.AutoGenerateColumns = false;
            btnFnc09.Enabled = false;

            if (txtCustCode.Text.Length == 6)
            {
                await SearchAsync();
            }
        }

        /// <summary>検索実行</summary>
        /// <returns>非同期タスク</returns>
        private async Task SearchAsync()
        {
            SearchRepo repo = SearchRepo.GetInstance();

            var tokuiCode = txtCustCode.Text;
            var tokuiName = txtCustName.Text;
            var tokuiKana = txtCustKana.Text;
            var teamCode = TeamCode;

            var customers = await ApiHelper.FetchAsync(this, async () =>
            {
                var url = string.Format(ApiResources.HatF.MasterEditor.CustomersMst, txtCustCode.Text.Trim());
                return await Program.HatFApiClient.GetAsync<List<CustomersMstEx>>(url, new
                {
                    CustName = txtCustName.Text,
                    CustKana = txtCustKana.Text,
                });
            });
            if (customers.Failed)
            {
                return;
            }

            dgvList.DataSource = customers.Value;
            btnFnc11.Enabled = customers.Value.Any();
            lblNote.Visible = !customers.Value.Any();
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            this.btnFnc09.Focus();
            if (MessageBox.Show(@"検索しますか？", @"検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            await SearchAsync();
        }

        /// <summary>決定ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (dgvList.CurrentRow != null)
            {
                var dataSource = dgvList.CurrentRow.DataBoundItem as CustomersMstEx;
                CustCode = dataSource.CustCode;
                CustName = dataSource.CustName;
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>閉じるボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnFnc12_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>キーダウン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    if (btnFnc09.Enabled == true)
                        btnFnc09.PerformClick();
                    break;

                case Keys.F11:
                    if (btnFnc11.Enabled == true)
                        btnFnc11.PerformClick();
                    break;

                case Keys.F12:
                    if (btnFnc12.Enabled == true)
                        btnFnc12.PerformClick();
                    break;
            }
        }

        /// <summary>グリッドでのダブルクリック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnFnc11.PerformClick();
        }

        /// <summary>検索条件3種のテキスト変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void Condition_TextChanged(object sender, EventArgs e)
        {
            btnFnc09.Enabled = !string.IsNullOrEmpty(txtCustCode.Text.Trim()) ||
                !string.IsNullOrEmpty(txtCustName.Text.Trim()) ||
                !string.IsNullOrEmpty(txtCustKana.Text.Trim());
        }
    }
}