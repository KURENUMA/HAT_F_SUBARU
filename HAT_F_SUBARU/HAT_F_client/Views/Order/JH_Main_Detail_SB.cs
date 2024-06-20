using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Order
{
    public partial class JH_Main_Detail_SB : Form
    {
        private string _StrInputText = "";
        public string JH_Main_Detail_SB_StrInputText
        {
            get
            {
                return _StrInputText;
            }
            set
            {
                _StrInputText = value;
            }
        }

        public JH_Main_Detail_SB()
        {
            InitializeComponent();
        }
        private void JH_Main_Detail_SB_Load(object sender, EventArgs e)
        {
            this.txtSiireBikou.Text = _StrInputText;
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            _StrInputText = this.txtSiireBikou.Text;
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
        }
        private void JH_Main_Detail_SB_KeyDown(object sender, KeyEventArgs e)
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
    }
}
