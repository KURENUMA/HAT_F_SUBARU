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

namespace HatFClient.Views.Estimate
{
    public partial class MT_Main : Form
    {
        public MT_Main()
        {
            InitializeComponent();
        }
        private void MT_Main_Load(object sender, EventArgs e)
        {

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            // オプション情報取得
            //this.clientRepo = await ClientRepo.GetInstance();
        }
        #region <<< 上部ボタン >>>
        private void BtnFnc01_Click(object sender, System.EventArgs e)
        {
            this.btnFnc01.Focus();
            if (MessageBox.Show("新規入力しますか？", "新規", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            MessageBox.Show(this.btnFnc01.Text);
        }
        private void BtnFnc02_Click(object sender, System.EventArgs e)
        {
            this.btnFnc02.Focus();
            MessageBox.Show(this.btnFnc02.Text);
        }
        private void BtnFnc03_Click(object sender, System.EventArgs e)
        {
            this.btnFnc03.Focus();
            MessageBox.Show(this.btnFnc03.Text);
        }
        private void BtnFnc05_Click(object sender, System.EventArgs e)
        {
            this.btnFnc05.Focus();
            MessageBox.Show(this.btnFnc05.Text);
        }
        private void BtnFnc06_Click(object sender, System.EventArgs e)
        {
            this.btnFnc06.Focus();
            MessageBox.Show(this.btnFnc06.Text);
        }
        private void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            this.btnFnc09.Focus();
            MessageBox.Show(this.btnFnc09.Text);
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            MessageBox.Show(this.btnFnc11.Text);
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
            using (MT_Main_Save dlg = new())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.Yes:
                        if (MessageBox.Show("保存しますか？", "保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        break;
                    case DialogResult.No:
                        if (MessageBox.Show("終了しますか？", "終了", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        this.Close();
                        this.Dispose();
                        break;
                    default:
                        break;
                }
            }
        }
        private void BtnOps_Click(object sender, System.EventArgs e)
        {
            this.btnOPS.Focus();
            MessageBox.Show(this.btnOPS.Text);
        }
        private void BtnKakunin_Click(object sender, System.EventArgs e)
        {
            this.btnKakunin.Focus();
            MessageBox.Show(this.btnKakunin.Text);
        }
        private void BtnEstCond_Click(object sender, System.EventArgs e)
        {
            this.btnEstCond.Focus();
            MessageBox.Show(this.btnEstCond.Text);
        }
        private void BtnTableLayout_Click(object sender, System.EventArgs e)
        {
            this.btnTableLayout.Focus();
            MessageBox.Show(this.btnTableLayout.Text);
        }
        private void BtnStockInfo_Click(object sender, System.EventArgs e)
        {
            this.btnStockInfo.Focus();
            MessageBox.Show(this.btnStockInfo.Text);
        }
        private void BtnPrintConfig_Click(object sender, System.EventArgs e)
        {
            this.btnPrintConfig.Focus();
            MessageBox.Show(this.btnPrintConfig.Text);
        }
        private void BtnSetting_Click(object sender, System.EventArgs e)
        {
            this.btnSetting.Focus();
            MessageBox.Show(this.btnSetting.Text);
        }
        #endregion

        #region <<< 下部ボタン >>>
        private void BtnTree_Click(object sender, System.EventArgs e)
        {
            this.btnTree.Focus();
            MessageBox.Show(this.btnTree.Text);
        }
        private void BtnBulkPrice_Click(object sender, System.EventArgs e)
        {
            this.btnBulkPrice.Focus();
            MessageBox.Show(this.btnBulkPrice.Text);
        }
        private void BtnFnc07_Click(object sender, System.EventArgs e)
        {
            this.btnFnc07.Focus();
            MessageBox.Show(this.btnFnc07.Text);
        }
        private void BtnFnc08_Click(object sender, System.EventArgs e)
        {
            this.btnFnc08.Focus();
            MessageBox.Show(this.btnFnc08.Text);
        }
        private void BtnRowIns_Click(object sender, System.EventArgs e)
        {
            this.btnRowIns.Focus();
            MessageBox.Show(this.btnRowIns.Text);
        }
        private void BtnRowCopy_Click(object sender, System.EventArgs e)
        {
            this.btnRowCopy.Focus();
            MessageBox.Show(this.btnRowCopy.Text);
        }
        private void BtnRowDel_Click(object sender, System.EventArgs e)
        {
            this.btnRowDel.Focus();
            MessageBox.Show(this.btnRowDel.Text);
        }
        #endregion

        #region << ショートカット制御 >>
        private void MT_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 & e.Control == true)
            {
                if (btnRowCopy.Enabled == true)
                {
                    btnRowCopy.PerformClick();
                    return;
                }
            }
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnFnc01.Enabled == true)
                        btnFnc01.PerformClick();
                    break;
                case Keys.F2:
                    if (btnFnc02.Enabled == true)
                        btnFnc02.PerformClick();
                    break;
                case Keys.F3:
                    if (btnFnc03.Enabled == true)
                        btnFnc03.PerformClick();
                    break;
                case Keys.F5:
                    if (btnFnc05.Enabled == true)
                        btnFnc05.PerformClick();
                    break;
                case Keys.F6:
                    if (btnFnc06.Enabled == true)
                        btnFnc06.PerformClick();
                    break;
                case Keys.F7:
                    if (btnFnc07.Enabled == true)
                        btnFnc07.PerformClick();
                    break;
                case Keys.F8:
                    if (btnFnc08.Enabled == true)
                        btnFnc08.PerformClick();
                    break;
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
                case Keys.PageDown:
                    if (btnKakunin.Enabled == true)
                        btnKakunin.PerformClick();
                    break;
                case Keys.Insert:
                    if (btnRowIns.Enabled == true)
                        btnRowIns.PerformClick();
                    break;
                case Keys.Delete:
                    if (btnRowDel.Enabled == true)
                        btnRowDel.PerformClick();
                    break;
            }
        }
        #endregion
    }
}
