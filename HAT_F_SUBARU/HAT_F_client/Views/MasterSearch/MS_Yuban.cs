using HatFClient.Common;
using HatFClient.Models;
using HatFClient.Repository;
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
using HAT_F_api.Models;



namespace HatFClient.Views.MasterSearch
{
    public partial class MS_Yuban : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"郵便番号情報が存在しません";   // データ無しメッセージ    
        private string _txtPOSTCODE;
        private string _strMsPostCode = @"";
        private string _strMsAddName = @"";
        private string _strMsAddName1 = @"";
        private string _strMsAddName2 = @"";
        private string _strMsAddName3 = @"";
        public string StrMsPostCode
        {
            get { return _strMsPostCode; }
        }
        public string StrMsAddName
        {
            get { return _strMsAddName; }
        }
        public string StrMsAddName1
        {
            get { return _strMsAddName1; }
        }
        public string StrMsAddName2
        {
            get { return _strMsAddName2; }
        }
        public string StrMsAddName3
        {
            get { return _strMsAddName3; }
        }
        public string TxtPOSTCODE
        {
            get { return _txtPOSTCODE; }
            set { _txtPOSTCODE = value; }
        }
        public MS_Yuban()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.txtPOSTCODE.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            InitForm();
            txtPOSTCODE.Text = TxtPOSTCODE;
            if (txtPOSTCODE.Text.Length == 8)
            {
                ShowList();
            }
        }
        private void InitForm()
        {
            txtPOSTCODE.Clear();
            txtAddress.Clear();
            InitDgvList();
            btnFnc09.Enabled = false;
            btnFnc11.Enabled = false;
            HatFComParts.InitMessageArea(lblNote);
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"Name", @"Adrs1", @"Adrs2", @"Adrs3" };
            var columnTexts = new string[] { @"郵便番号", @"住所", @"住所1", @"住所2", @"住所3" };
            var columnWidths = new int[] { 150, 600, 0, 0, 0};

            for (int i = 0; i < columnNames.Length; i++)
            {
                var viewColumn = new DataGridViewColumn();
                viewColumn.Name = columnNames[i];
                viewColumn.HeaderText = columnTexts[i];
                viewColumn.Width = columnWidths[i];
                viewColumn.CellTemplate = new DataGridViewTextBoxCell();
                dgvList.Columns.Add(viewColumn);
                dgvList.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgvList.EnableHeadersVisualStyles = false;
            dgvList.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvList.ColumnHeadersHeight = 60;
            dgvList.RowTemplate.Height = 30;
            dgvList.Columns[2].Visible = false;
            dgvList.Columns[3].Visible = false;
            dgvList.Columns[4].Visible = false;
        }
        private async void ShowList()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            List<PostAddress> postCode = await repo.searchPostCode(txtPOSTCODE.Text, txtAddress.Text, IntShowMaxConunt);

            dgvList.Rows.Clear();
            if (postCode.Count > 0)
            {
                for (int i = 0; i < postCode.Count; i++)
                {
                    var values = new string[] { postCode[i].PostCode7, postCode[i].Prefectures + @" " + postCode[i].Municipalities + @" " + postCode[i].TownArea, postCode[i].Prefectures, postCode[i].Municipalities, postCode[i].TownArea };
                    dgvList.Rows.Add(values);
                }
                btnFnc11.Enabled = true;
            }
            else
            {
                HatFComParts.ShowMessageAreaError(lblNote, StrNoDataMessage);
                btnFnc11.Enabled = false;
            }
        }

        private void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            if (!IsChkTxtPostCd()) { return; }
            this.btnFnc09.Focus();
            if (MessageBox.Show(@"検索しますか？", @"検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            ShowList();
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (dgvList.CurrentRow != null)
            {
                _strMsPostCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[0].Value.ToString();
                if (_strMsPostCode.Length == 7) { _strMsPostCode = _strMsPostCode.Substring(0,3) + @"-" + _strMsPostCode.Substring(3, 4); }
                _strMsAddName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[1].Value.ToString();
                _strMsAddName1 = dgvList.Rows[dgvList.CurrentRow.Index].Cells[2].Value.ToString();
                _strMsAddName2 = dgvList.Rows[dgvList.CurrentRow.Index].Cells[3].Value.ToString();
                _strMsAddName3 = dgvList.Rows[dgvList.CurrentRow.Index].Cells[4].Value.ToString();
            }
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
        }
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
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnFnc11.Enabled == true)
            {
                btnFnc11.PerformClick();
            }
        }
        private void Condition_TextChanged(object sender, EventArgs e)
        {
            bool bFlg = false;
            if (txtPOSTCODE.Text.Length > 0) { bFlg = true; }
            if (txtAddress.Text.Length > 0) { bFlg = true; }
            if (bFlg) { btnFnc09.Enabled = true; } else { btnFnc09.Enabled = false; }
        }
        private void TextBoxHyphen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumHyphen(e.KeyChar);
        }
        private void IsInputCheckFocusOut_Validated(object sender, EventArgs e)
        {
            IsChkTxtPostCd();
        }
        private bool IsChkTxtPostCd()
        {
            bool boolFlg = true;
            HatFComParts.InitMessageArea(lblNote);
            if (this.txtPOSTCODE.Text.Length > 0)
            {
                bool boolChk = HatFComParts.IsZipCode(txtPOSTCODE.Text);
                if (!boolChk)
                {
                    HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, @"FO009"));
                    txtPOSTCODE.Focus();
                    HatFComParts.SetColorOnErrorControl(txtPOSTCODE);
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
    }
}
