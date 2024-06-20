using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    public partial class MS_Syohin : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"商品情報が存在しません";    // データ無しメッセージ    
        private const string StrConNote = @"スペース区切りで検索キーワードを複数設定出来ます。濁点半濁点小文字は気にせず検索出来ます。";
        private string _strMsSyohinCode = @"";
        private string _strMsSyohinName = @"";
        private string _txtSHIRESAKI_CD;
        private string _txtSYOHIN_CD;
        public string StrMsSyohinCode
        {
            get { return _strMsSyohinCode; }
        }
        public string StrMsSyohinName
        {
            get { return _strMsSyohinName; }
        }
        public string TxtSHIRESAKI_CD
        {
            get { return _txtSHIRESAKI_CD; }
            set { _txtSHIRESAKI_CD = value; }
        }
        public string TxtSYOHIN_CD
        {
            get { return _txtSYOHIN_CD; }
            set { _txtSYOHIN_CD = value; }
        }
        public MS_Syohin()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.txtSHIRESAKI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            this.lblSideNote.Text = StrConNote;
            InitForm();
            txtSHIRESAKI_CD.Text = TxtSHIRESAKI_CD;
            txtSYOHIN_CD.Text = TxtSYOHIN_CD;
            if (txtSHIRESAKI_CD.Text.Length == 6 && txtSYOHIN_CD.Text.Length > 0)
            {
                ShowListHat();
            }
        }
        private void InitForm()
        {
            txtSHIRESAKI_CD.Clear();
            txtSYOHIN_CD.Clear();
            InitDgvList();
            btnFnc04.Enabled = false;
            btnFnc05.Enabled = false;
            btnFnc11.Enabled = false;
            HatFComParts.InitMessageArea(lblNote);
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"MakerCode", @"Bunrui", @"Name", @"NameHankaku"};
            var columnTexts = new string[] { @"ＨＡＴ商品コード", @"メーカー商品コード", @"分類", @"商品名（全角）", @"商品名（半角）" };
            var columnWidths = new int[] { 180, 180, 100, 300, 300 };

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
        private async void ShowListHat()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var shiresakiCode = txtSHIRESAKI_CD.Text;
            var syohinCode = txtSYOHIN_CD.Text;
            var productHat = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchProductHat(shiresakiCode, syohinCode, IntShowMaxConunt);
            });
            if (productHat.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (productHat.Value.Count > 0)
            {
                for (int i = 0; i < productHat.Value.Count; i++)
                {
                    var values = new string[] { 
                        productHat.Value[i].Cd24, 
                        productHat.Value[i].MKey, 
                        productHat.Value[i].Code5, 
                        productHat.Value[i].Nnm, 
                        productHat.Value[i].Anm,
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
        private async void ShowListMaker()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var shiresakiCode = txtSHIRESAKI_CD.Text;
            var syohinCode = txtSYOHIN_CD.Text;
            var productMaker = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchProductMaker(shiresakiCode, syohinCode, IntShowMaxConunt);
            });
            if (productMaker.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (productMaker.Value.Count > 0)
            {
                for (int i = 0; i < productMaker.Value.Count; i++)
                {
                    var values = new string[] { 
                        productMaker.Value[i].Cd24, 
                        productMaker.Value[i].MKey, 
                        productMaker.Value[i].Code5, 
                        productMaker.Value[i].Nnm, 
                        productMaker.Value[i].Anm,
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

        private void BtnFnc04_Click(object sender, System.EventArgs e)
        {
            if (!IsChkTxtShiresakiCd()) { return; }
            this.btnFnc04.Focus();
            if (MessageBox.Show(@"検索しますか？", @"検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            ShowListHat();
        }
        private void BtnFnc05_Click(object sender, System.EventArgs e)
        {
            if (!IsChkTxtShiresakiCd()) { return; }
            this.btnFnc05.Focus();
            if (MessageBox.Show(@"検索しますか？", @"検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            ShowListMaker();
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (dgvList.CurrentRow != null)
            {
                _strMsSyohinCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[2].Value.ToString();
                _strMsSyohinName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[4].Value.ToString();
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
                case Keys.F4:
                    if (btnFnc04.Enabled == true)
                        btnFnc04.PerformClick();
                    break;
                case Keys.F5:
                    if (btnFnc05.Enabled == true)
                        btnFnc05.PerformClick();
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
            if (txtSHIRESAKI_CD.Text.Length > 0) { bFlg = true; }
            if (txtSYOHIN_CD.Text.Length > 0) { bFlg = true; }
            if (bFlg) { btnFnc04.Enabled = true; } else { btnFnc04.Enabled = false; }
            if (bFlg) { btnFnc05.Enabled = true; } else { btnFnc05.Enabled = false; }
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
            IsChkTxtShiresakiCd();
        }
        private bool IsChkTxtShiresakiCd()
        {
            bool boolFlg = true;
            HatFComParts.InitMessageArea(lblNote);
            if (txtSHIRESAKI_CD.Text.Length > 0)
            {
                bool boolChk = HatFComParts.BoolIsAlphabetNumByRegex(txtSHIRESAKI_CD.Text);
                if (!boolChk)
                {
                    HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, @"FO002"));
                    txtSHIRESAKI_CD.Focus();
                    HatFComParts.SetColorOnErrorControl(txtSHIRESAKI_CD);
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
    }
}
