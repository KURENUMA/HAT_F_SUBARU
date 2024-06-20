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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HatFClient.Views.PriceBatch
{
    public partial class PB_Main : Form
    {
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;

        private string _strPriceBatchKigouUri = @"";
        public string StrPriceBatchKigouUri
        {
            get { return _strPriceBatchKigouUri; }
        }
        private string _strPriceBatchPerUri = @"";
        public string StrPriceBatchPerUri
        {
            get { return _strPriceBatchPerUri; }
        }
        private string _strPriceBatchKigouSii = @"";
        public string StrPriceBatchKigouSii
        {
            get { return _strPriceBatchKigouSii; }
        }
        private string _strPriceBatchPerSii = @"";
        public string StrPriceBatchPerSii
        {
            get { return _strPriceBatchPerSii; }
        }
        private bool _boolPriceBatchUri = false;
        public bool BoolPriceBatchUri
        {
            get { return _boolPriceBatchUri; }
        }
        private bool _boolPriceBatchSii = false;
        public bool BoolPriceBatchSii
        {
            get { return _boolPriceBatchSii; }
        }
        public PB_Main()
        {
            InitializeComponent();
        }
        private void PB_Main_Load(object sender, EventArgs e)
        {
            this.decUriPer.Validated += new EventHandler(this.IsFO010CheckFocusOut_Validated);
            this.decSiiPer.Validated += new EventHandler(this.IsFO010CheckFocusOut_Validated);
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            InitForm();
        }
        private void InitForm()
        {
            chkUriTan.Checked = false;
            chkSiiTan.Checked = false;
            gbUriTan.Enabled = false;
            gbSiiTan.Enabled = false;
            rdoUriTanA.Checked = true;
            rdoSiiTanA.Checked = true;
            decUriPer.Clear();
            decSiiPer.Clear();
        }
        private void ChkUriTan_CheckedChanged(object sender, EventArgs e)
        {
            gbUriTan.Enabled = chkUriTan.Checked;
            if (!gbUriTan.Enabled) { decUriPer.Clear(); }
        }
        private void ChkSiiTan_CheckedChanged(object sender, EventArgs e)
        {
            gbSiiTan.Enabled = chkSiiTan.Checked;
            if (!gbSiiTan.Enabled) { decSiiPer.Clear(); }
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (MessageBox.Show(@"決定しますか？", @"決定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (gbUriTan.Enabled) 
            {
                if (!IsChkPer(decUriPer)) { return; }
                _boolPriceBatchUri = true;
                var objUri = gbUriTanSlc.Controls.OfType<System.Windows.Forms.RadioButton>().SingleOrDefault(rb => rb.Checked == true);
                _strPriceBatchKigouUri = (objUri.Tag == null) ? "" : objUri.Tag.ToString();
                if (decUriPer.Text != @"0") { _strPriceBatchPerUri = decUriPer.Text; }
            }
            if (gbSiiTan.Enabled)
            {
                if (!IsChkPer(decSiiPer)) { return; }
                _boolPriceBatchSii = true;
                var objSii = gbSiiTanSlc.Controls.OfType<System.Windows.Forms.RadioButton>().SingleOrDefault(rb => rb.Checked == true);
                _strPriceBatchKigouSii = (objSii.Tag == null) ? "" : objSii.Tag.ToString();
                if (decSiiPer.Text != @"0") { _strPriceBatchPerSii = decSiiPer.Text; }
            }
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
        }
        private void PB_Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
        private void IsFO010CheckFocusOut_Validated(object sender, EventArgs e)
        {
            IsChkPer((System.Windows.Forms.TextBox)sender);
        }
        private bool IsChkPer(Control conName)
        {
            conName.Font = new Font(conName.Font, FontStyle.Regular);
            if (conName.Text.Length == 0) { return true; }

            bool boolChk = true;
            decimal? decVal;
            switch (conName.Name)
            {
                case nameof(decUriPer):
                    if (!rdoUriTanR.Checked)
                    {
                        decVal = HatFComParts.DoParseDecimal(conName.Text);
                        if (decVal != null && decVal > (decimal)999.9)
                        {
                            boolChk = false;
                        }
                    }
                    break;
                case nameof(decSiiPer):
                    if (!rdoSiiTanR.Checked)
                    {
                        decVal = HatFComParts.DoParseDecimal(conName.Text);
                        if (decVal != null && decVal > (decimal)999.9)
                        {
                            boolChk = false;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (!boolChk)
            {
                string strId = @"FO010";
                string strMsg = HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId);
                conName.Focus();
                HatFComParts.SetColorOnErrorControl(conName);
                MessageBox.Show(strMsg, @"価格一括", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
