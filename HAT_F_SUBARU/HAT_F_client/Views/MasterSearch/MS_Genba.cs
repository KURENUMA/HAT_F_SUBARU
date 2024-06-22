using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    /// <summary>現場検索画面</summary>
    public partial class MS_Genba : Form
    {
        /// <summary>検索結果のグリッドに表示するための項目クラス</summary>
        private class GenbaGridViewItem
        {
            /// <summary>得意先コード</summary>
            public string CustomerCode { get; set; }

            /// <summary>キーマンコード</summary>
            public string KeymanCode { get; set; }

            /// <summary>キーマン名</summary>
            public string KeymanName { get; set; }

            /// <summary>現場コード</summary>
            public string GenbaCode { get; set; }

            /// <summary>現場名</summary>
            public string GenbaName { get; set; }

            /// <summary>住所</summary>
            public string Address { get; set; }

            /// <summary>顧客マスタ</summary>
            public CustomersMst Customer { get; set; }

            /// <summary>出荷先マスタ</summary>
            public DestinationsMst Destination { get; set; }

            /// <summary>コンストラクタ</summary>
            public GenbaGridViewItem(CustomersMst customer, DestinationsMst destination)
            {
                Customer = customer;
                Destination = destination;
                CustomerCode = customer.ArCode;

                // TODO: DB変更対応
                //KeymanCode = customer.KeymanCode;

                KeymanName = customer.CustUserName;
                GenbaCode = destination.GenbaCode;
                GenbaName = destination.DistName1 + destination.DistName2;
                Address = destination.Address1 + destination.Address2 + destination.Address3;
            }
        }

        /// <summary>エラーメッセージ管理クラス</summary>
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;

        /// <summary>表示最大件数</summary>
        private const int IntShowMaxConunt = 200;

        /// <summary>検索結果0件時のメッセージ</summary>
        private const string NoDataMessage = @"現場情報が存在しません";

        /// <summary>顧客マスタ</summary>
        public CustomersMst Customer { get; set; }

        /// <summary>出荷先</summary>
        public DestinationsMst Destination { get; set; }

        /// <summary>顧客コード</summary>
        public string CustomerCode { get; set; }

        /// <summary>キーマンコード</summary>
        public string KeymanCode { get; set; }

        /// <summary>現場コード</summary>
        public string GenbaCode { get; set; }

        /// <summary>コンストラクタ</summary>
        public MS_Genba()
        {
            InitializeComponent();
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void MS_Genba_Load(object sender, EventArgs e)
        {
            dgvList.AutoGenerateColumns = false;

            hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            txtGENBA_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            InitForm();

            if (txtGENBA_CD.Text.Length > 0 && HatFComParts.BoolIsHalfByRegex(txtGENBA_CD.Text))
            {
                SearchAsync();
            }
        }

        /// <summary>各コントロールのクリア</summary>
        private void InitForm()
        {
            txtCUST_CODE.Text = CustomerCode;
            txtKEYMAN_CODE.Text = KeymanCode;
            txtGENBA_CD.Text = GenbaCode;
            txtGENBA_NAME.Clear();
            txtGENBA_ADDRESS.Clear();
            lblMaxCount.Text = $"最大 {IntShowMaxConunt} 件表示";
            EnableButtons();
            HatFComParts.InitMessageArea(lblNote);
        }

        /// <summary>検索と表示</summary>
        private async void SearchAsync()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var genbaCode = txtGENBA_CD.Text;
            var genbaName = txtGENBA_NAME.Text;
            var genbaAddress = txtGENBA_ADDRESS.Text;
            var tokuiCode = txtCUST_CODE.Text;
            var keymanCode = txtKEYMAN_CODE.Text;
            var genbaList = await ApiHelper.FetchAsync(this, async () =>
            {
                return await repo.SearchGenba(tokuiCode, keymanCode, genbaCode, genbaName, genbaAddress, IntShowMaxConunt);
            });
            if (genbaList.Failed)
            {
                return;
            }

            var list = genbaList.Value.Select(x => new GenbaGridViewItem(x.Customer, x.Destination)).ToList();
            dgvList.DataSource = list;
            EnableButtons();
            if (!list.Any())
            {
                HatFComParts.ShowMessageAreaError(lblNote, NoDataMessage);
            }
        }

        /// <summary>検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            if (!ValidateGenbaCd())
            {
                return;
            }
            this.btnFnc09.Focus();
            if (!DialogHelper.SearchingConfirm(this))
            {
                return;
            }
            SearchAsync();
        }

        /// <summary>決定ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (dgvList.CurrentRow != null)
            {
                var selectedItem = dgvList.CurrentRow.DataBoundItem as GenbaGridViewItem;
                Customer = selectedItem.Customer;
                Destination = selectedItem.Destination;
            }
        }

        /// <summary>キーダウン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void MS_Genba_KeyDown(object sender, KeyEventArgs e)
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

        /// <summary>検索結果のダブルクリック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnFnc11.Enabled == true)
            {
                btnFnc11.PerformClick();
            }
        }

        /// <summary>各種テキストボックスの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void Condition_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        /// <summary>各ボタンの押下可否を決定</summary>
        private void EnableButtons()
        {
            var textBoxes = new[]
            {
                txtCUST_CODE, txtKEYMAN_CODE, txtGENBA_CD, txtGENBA_NAME, txtGENBA_ADDRESS
            };

            btnFnc09.Enabled = textBoxes.Any(t => t.Text.Length > 0);
            btnFnc11.Enabled = dgvList.CurrentRow != null;
        }

        /// <summary>現場CDのバリデート後（フォーカスアウト）</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsInputCheckFocusOut_Validated(object sender, EventArgs e)
        {
            ValidateGenbaCd();
        }

        /// <summary>現場Cdのバリデーション</summary>
        /// <returns>成否</returns>
        private bool ValidateGenbaCd()
        {
            var result = true;
            HatFComParts.InitMessageArea(lblNote);
            if (txtGENBA_CD.Text.Length > 0 && !HatFComParts.BoolIsHalfByRegex(txtGENBA_CD.Text))
            {
                HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, "FO004"));
                txtGENBA_CD.Focus();
                HatFComParts.SetColorOnErrorControl(txtGENBA_CD);
                result = false;
            }
            return result;
        }
    }
}