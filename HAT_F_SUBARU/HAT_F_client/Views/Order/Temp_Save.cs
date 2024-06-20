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
    public partial class Temp_Save : Form
    {
        public JH_Main jh_Main;         // 引継用

        public Temp_Save()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            var pages = jh_Main.getFosJyuchPages();
            FosJyuchuRepo.GetInstance().SetTmpCache(pages);
            MessageBox.Show("一時保存が完了しました");
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Temp_Save_Load(object sender, EventArgs e) {
            var fosRepo = FosJyuchuRepo.GetInstance();

            String saveKey = fosRepo.GetCurrentSaveKey();
            this.btnRestore.Enabled = fosRepo.HasTmpCache(saveKey);
            this.btnSave.Enabled = saveKey != null;

        }

        private async void btnRestore_Click(object sender, EventArgs e) {
            if (MessageBox.Show(@"入力内容を破棄しますか？", @"保存呼出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                this.Close();
                return;
            }
            var fosRepo = FosJyuchuRepo.GetInstance();

            String saveKey = fosRepo.GetCurrentSaveKey();

            this.jh_Main.SetData(fosRepo.GetTmpCache(saveKey));
            await this.jh_Main.ShowJH_MainAsync(false);

            this.Close();
        }
    }
}
