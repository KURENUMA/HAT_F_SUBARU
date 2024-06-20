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
    public partial class MS_Shiresaki : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"仕入先情報が存在しません";    // データ無しメッセージ    
        private string _txtTEAM_CD;
        private string _txtSHIRESAKI_CD;
        private string _strMsShiresakiCode = @"";
        private string _strMsShiresakiName = @"";
        public string StrMsShiresakiCode
        {
            get { return _strMsShiresakiCode; }
        }
        public string StrMsShiresakiName
        {
            get { return _strMsShiresakiName; }
        }
        public string TxtTEAM_CD
        {
            get { return _txtTEAM_CD; }
            set { _txtTEAM_CD = value; }
        }
        public string TxtSHIRESAKI_CD
        {
            get { return _txtSHIRESAKI_CD; }
            set { _txtSHIRESAKI_CD = value; }
        }
        public MS_Shiresaki()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.txtSHIRESAKI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            InitForm();
            txtSHIRESAKI_CD.Text = TxtSHIRESAKI_CD;
            if (txtSHIRESAKI_CD.Text.Length == 6)
            {
                ShowList();
            }
        }
        private void InitForm()
        {
            txtSHIRESAKI_CD.Clear();
            txtSHIRESAKI_NAME.Clear();
            txtSHIRESAKI_KANA.Clear();
            InitDgvList();
            btnFnc09.Enabled = false;
            btnFnc11.Enabled = false;
            HatFComParts.InitMessageArea(lblNote);
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"Name", @"5Code", @"Bunrui", @"Fax", @"Tel" };
            var columnTexts = new string[] { @"仕入先ＣＤ", @"仕入先名＿漢字", @"５桁コード", @"商品分類名", @"ＦＡＸ番号", @"電話番号" };
            var columnWidths = new int[] { 100, 300, 100, 250, 150, 150 };

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
            var shiresakiCode = txtSHIRESAKI_CD.Text;
            var shiresakiName = txtSHIRESAKI_NAME.Text;
            var shiresakiKana = txtSHIRESAKI_KANA.Text;
            var teamCode = TxtTEAM_CD;
            var suppliers = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchSupplier(shiresakiCode, shiresakiName, shiresakiKana, teamCode, IntShowMaxConunt);
            });
            if (suppliers.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (suppliers.Value.Count > 0)
            {
                for (int i = 0; i < suppliers.Value.Count; i++)
                {
                    var values = new string[] { 
                        suppliers.Value[i].SupplierCd, 
                        suppliers.Value[i].SupplierName, 
                        suppliers.Value[i].CategoryCode, 
                        suppliers.Value[i].CategoryName, 
                        suppliers.Value[i].SupplierFax, 
                        suppliers.Value[i].SupplierTel,
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
            if (!IsChkTxtShiresakiCd()) { return; }
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
                _strMsShiresakiCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[0].Value.ToString();
                _strMsShiresakiName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[1].Value.ToString();
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
            if (txtSHIRESAKI_CD.Text.Length > 0) { bFlg = true; }
            if (txtSHIRESAKI_NAME.Text.Length > 0) { bFlg = true; }
            if (txtSHIRESAKI_KANA.Text.Length > 0) { bFlg = true; }
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
