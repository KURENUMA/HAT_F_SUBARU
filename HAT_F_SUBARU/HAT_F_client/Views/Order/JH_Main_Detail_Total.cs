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

namespace HatFClient.Views.Order
{
    public partial class JH_Main_Detail_Total : Form
    { 
        private string _strTei = @"";
        private string _strUri = @"";
        private string _strSii = @"";
        private string _strArari = @"";
        private string _strRiritu = @"";
        public string StrTei
        {
            get { return _strTei; }
            set { _strTei = value; }
        }
        public string StrUri
        {
            get { return _strUri; }
            set { _strUri = value; }
        }
        public string StrSii
        {
            get { return _strSii; }
            set { _strSii = value; }
        }
        public string StrArari
        {
            get { return _strArari; }
            set { _strArari = value; }
        }
        public string StrRiritu
        {
            get { return _strRiritu; }
            set { _strRiritu = value; }
        }

        public JH_Main_Detail_Total()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            InitForm();
            ShowData();
        }
        private void InitForm()
        {
            this.txtroTeika.Clear();
            this.txtroBaigaku.Clear();
            this.txtroShigaku.Clear();
            this.txtroArari.Clear();
            this.txtroRiritsu.Clear();
        }
        private void ShowData()
        {
            this.txtroTeika.Text = StrTei;
            this.txtroBaigaku.Text = StrUri;
            this.txtroShigaku.Text = StrSii;
            this.txtroArari.Text = StrArari;
            this.txtroRiritsu.Text = StrRiritu;
        }
        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (btnFnc12.Enabled == true)
                        btnFnc12.PerformClick();
                    break;
            }
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
        }
    }
}
