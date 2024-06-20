using HAT_F_api.CustomModels;
using HatFClient.Common;
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

namespace HatFClient.Views.MasterSearch
{
    public partial class MS_Tokui : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"得意先情報が存在しません";    // データ無しメッセージ    
        private string _txtTEAM_CD;
        private string _txtTOKUI_CD;
        private string _strMsTokuiCode = @"";
        private string _strMsTokuiName = @"";
        public string StrMsTokuiCode
        {
            get { return _strMsTokuiCode; }
        }
        public string StrMsTokuiName
        {
            get { return _strMsTokuiName; }
        }
        public string TxtTEAM_CD
        {
            get { return _txtTEAM_CD; }
            set { _txtTEAM_CD = value; }
        }
        public string TxtTOKUI_CD
        {
            get { return _txtTOKUI_CD; }
            set { _txtTOKUI_CD = value; }
        }

        public MS_Tokui()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this);
            }
        }

        private void Fm_Load(object sender, EventArgs e)
        {
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.txtTOKUI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            InitForm();
            this.txtTOKUI_CD.Text = TxtTOKUI_CD;
            if (this.txtTOKUI_CD.Text.Length == 6)
            {
                ShowList();
            }
        }

        private void InitForm()
        {
            txtTOKUI_CD.Clear();
            txtTOKUI_NAME.Clear();
            txtTOKUI_KANA.Clear();
            InitDgvList();
            btnFnc09.Enabled = false;
            btnFnc11.Enabled = false;
            HatFComParts.InitMessageArea(lblNote);
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"Name", @"Kana", @"Fax", @"Tel" };
            var columnTexts = new string[] { @"得意先コード", @"得意先名＿漢字", @"得意先名＿カナ", @"ＦＡＸ番号", @"電話番号" };
            var columnWidths = new int[] { 150, 300, 300, 150, 150};

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
        }
        private async void ShowList()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var tokuiCode = txtTOKUI_CD.Text;
            var tokuiName = txtTOKUI_NAME.Text;
            var tokuiKana = txtTOKUI_KANA.Text;
            var teamCode = TxtTEAM_CD;
            var torihikis = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchTorihiki(tokuiCode, tokuiName, tokuiKana, teamCode, IntShowMaxConunt);
            });
            if (torihikis.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (torihikis.Value.Count > 0)
            {
                for (int i = 0; i < torihikis.Value.Count; i++)
                {
                    var values = new string[] { 
                        torihikis.Value[i].TorihikiCd, 
                        torihikis.Value[i].TokuZ, 
                        torihikis.Value[i].TokuH, 
                        torihikis.Value[i].AFax,
                        torihikis.Value[i].ATel,
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
            if (!IsChkTxtTokuiCd()) { return; }
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
                _strMsTokuiCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[0].Value.ToString();
                _strMsTokuiName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[1].Value.ToString();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
            this.DialogResult = DialogResult.Cancel;
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
            if (txtTOKUI_CD.Text.Length > 0) { bFlg = true; }
            if (txtTOKUI_NAME.Text.Length > 0) { bFlg = true; }
            if (txtTOKUI_KANA.Text.Length > 0) { bFlg = true; }
            if (bFlg) { btnFnc09.Enabled = true; } else { btnFnc09.Enabled = false; }
        }
        private void TextBoxCharType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumAlphabet(e.KeyChar);
        }
        private void IsInputCheckFocusOut_Validated(object sender, EventArgs e)
        {
            IsChkTxtTokuiCd();
        }
        private bool IsChkTxtTokuiCd()
        {
            bool boolFlg = true;
            HatFComParts.InitMessageArea(lblNote);
            if (this.txtTOKUI_CD.Text.Length > 0)
            {
                bool boolChk = HatFComParts.BoolIsAlphabetNumByRegex(txtTOKUI_CD.Text);
                if (!boolChk)
                {
                    HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, @"FO002"));
                    txtTOKUI_CD.Focus();
                    HatFComParts.SetColorOnErrorControl(txtTOKUI_CD);
                    boolFlg = false;
                }
            }
            return boolFlg;
        }

    }
}
