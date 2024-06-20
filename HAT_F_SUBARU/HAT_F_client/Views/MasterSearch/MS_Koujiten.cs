using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    public partial class MS_Koujiten : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"工事店情報が存在しません";    // データ無しメッセージ    
        private string _txtKOUJITEN_CD;
        private string _txtKOUJITEN_NAME;
        private string _txtTOKUI_CD;
        public string _strMsKoujitenCode = @"";
        public string _strMsKoujitenName = @"";
        public string StrMsKoujitenCode
        {
            get { return _strMsKoujitenCode; }
        }
        public string StrMsKoujitenName
        {
            get { return _strMsKoujitenName; }
        }
        public string TxtKOUJITEN_CD
        {
            get { return _txtKOUJITEN_CD; }
            set { _txtKOUJITEN_CD = value; }
        }
        public string TxtKOUJITEN_NAME
        {
            get { return _txtKOUJITEN_NAME; }
            set { _txtKOUJITEN_NAME = value; }
        }
        public string TxtTOKUI_CD
        {
            get { return _txtTOKUI_CD; }
            set { _txtTOKUI_CD = value; }
        }
        public MS_Koujiten()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.txtKOUJITEN_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            InitForm();
            this.txtKOUJITEN_CD.Text = TxtKOUJITEN_CD;
            this.txtKOUJITEN_NAME.Text = TxtKOUJITEN_NAME;
            if (this.txtKOUJITEN_CD.Text.Length == 6 || this.txtKOUJITEN_NAME.Text.Length > 0)
            {
                ShowList();
            }
        }
        private void InitForm()
        {
            txtKOUJITEN_CD.Clear();
            txtKOUJITEN_NAME.Clear();
            InitDgvList();
            btnFnc09.Enabled = false;
            btnFnc11.Enabled = false;
            HatFComParts.InitMessageArea(lblNote);
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"NameA", @"NameN", @"Adrs", @"Adrs1", @"Adrs3", @"Adrs3", @"Post" };
            var columnTexts = new string[] { @"工事店コード", @"工事店名", @"カナ", @"住所", @"住所１", @"住所２", @"住所３", @"郵便番号" };
            var columnWidths = new int[] { 150, 300, 0, 400, 0,0,0,0 };

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
            dgvList.Columns[4].Visible = false;
            dgvList.Columns[5].Visible = false;
            dgvList.Columns[6].Visible = false;
            dgvList.Columns[7].Visible = false;
        }
        private async void ShowList()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var koujitenCode = txtKOUJITEN_CD.Text;
            var koujitenName = txtKOUJITEN_NAME.Text;
            var tokuiCode = TxtTOKUI_CD;
            var koujitenList = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchKoujiten(koujitenCode, koujitenName, tokuiCode, IntShowMaxConunt);
            });
            if (koujitenList.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (koujitenList.Value.Count > 0)
            {
                for (int i = 0; i < koujitenList.Value.Count; i++)
                {
                    var values = new string[] {
                        koujitenList.Value[i].KojiCd,
                        koujitenList.Value[i].KojiNnm, 
                        koujitenList.Value[i].KojiAnm, 
                        koujitenList.Value[i].Adrs1 + koujitenList.Value[i].Adrs2 + koujitenList.Value[i].Adrs3,
                        koujitenList.Value[i].Adrs1, 
                        koujitenList.Value[i].Adrs2, 
                        koujitenList.Value[i].Adrs3,
                        koujitenList.Value[i].PostCd
                    };
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
            if (!IsChkTxtKoujitenCd()) { return; }
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
                _strMsKoujitenCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[0].Value?.ToString();
                _strMsKoujitenName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[1].Value?.ToString();
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
            if (txtKOUJITEN_CD.Text.Length > 0) { bFlg = true; }
            if (txtKOUJITEN_NAME.Text.Length > 0) { bFlg = true; }
            if (bFlg) { btnFnc09.Enabled = true; } else { btnFnc09.Enabled = false; }
        }
        private void IsInputCheckFocusOut_Validated(object sender, EventArgs e)
        {
            IsChkTxtKoujitenCd();
        }
        private bool IsChkTxtKoujitenCd()
        {
            bool boolFlg = true;
            HatFComParts.InitMessageArea(lblNote);
            if (this.txtKOUJITEN_CD.Text.Length > 0)
            {
                bool boolChk = HatFComParts.BoolIsHalfByRegex(txtKOUJITEN_CD.Text);
                if (!boolChk)
                {
                    HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, @"FO002"));
                    txtKOUJITEN_CD.Focus();
                    HatFComParts.SetColorOnErrorControl(txtKOUJITEN_CD);
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
    }
}
