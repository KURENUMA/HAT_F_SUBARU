using HatFClient.Common;
using HatFClient.Repository;
using HatFClient.Views;
using HatFClient.Views.ConstructionProject;
using HatFClient.Views.Estimate;
using HatFClient.Views.Invoice;
using HatFClient.Views.MasterEdit;
using HatFClient.Views.Order;
using HatFClient.Views.PersonalSettings;
using HatFClient.Views.ProductStock;
using HatFClient.Views.Purchase;
using HatFClient.Views.Sales;
using HatFClient.Views.SalesCorrection;
using HatFClient.Views.template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HAT_F_api.CustomModels;
using HatFClient.Views.Delivery;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using HatFClient.Views.Warehousing;
using HatFClient.Views.CorrectionDelivery;

namespace HatFClient
{
    public partial class Menu : Form
    {
        private bool _isLogouting = false;
        private RoleController _roleController;

        public Menu()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this, true);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.MinimizeBox = true;

                // 権限設定
                _roleController = new RoleController(LoginRepo.GetInstance().CurrentUser.Roles, new Dictionary<Control, HatFUserRole[]>() {
                    { btnMasterEdit , new [] { HatFUserRole.MasterEdit }} ,
                });
            }
        }
   
        private async void Menu_Load(object sender, EventArgs e)
        {
            await InitializeFormAsync();
        }

        private async Task InitializeFormAsync()
        {
            this.lblVersion.Text = "バージョン：" + ApplicationHelper.GetAppVersionString();
            this.lblLoginName.Text = $"ようこそ！{LoginRepo.GetInstance().CurrentUser.EmployeeName}さん";

            txtInfo.Text = "取得中...";

            var clientRepo = ClientRepo.GetInstance();
            var announcements = await clientRepo.getAnnouncements();
            var szAnnouncements = announcements
                .Select(a => "■" + a.StartDate?.ToString("yyyy年MM月dd日 (ddd)") + "\r\n" + a.Content)
                .ToList();

            txtInfo.Text = string.Join("\r\n--------------------------------------------\r\n\r\n", szAnnouncements);
        }

        private void BtnMenu01_Click(object sender, System.EventArgs e)
        {
            JH_Main jH_Main = new();
            jH_Main.Show();
        }
        private void BtnMenu02_Click(object sender, System.EventArgs e)
        {
            MT_Main mT_Main = new();
            mT_Main.Show();
        }
        private void BtnMenu03_Click(object sender, System.EventArgs e)
        {
            ProductStock_Main pS_Main = new();
            pS_Main.Show();
        }

        /// <summary>
        /// 仕入請求一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenu04_Click(object sender, System.EventArgs e)
        {
            //BillingList billingList = new();
            //billingList.Show();
            var form = FormFactory.GetModelessForm<Views.Purchase.PU_BillingList>();
            form.Show();
            form.Activate();
        }

        private void BtnLogout_Click(object sender, System.EventArgs e)
        {
            if (false == DialogHelper.OkCancelQuestion(this, "ログアウトしますか?", true))
            {
                return;
            }

            // この画面以外を閉じる
            // (同一フォームの多インスタンス対応)
            List<Form> closingTargets = new List<Form>();
            foreach (Form item in Application.OpenForms)
            {
                //// 非表示にしてあるログイン画面は閉じる対象外
                //if (item is Login)
                //{
                //    continue;
                //}

                if (item != this)
                {
                    if (!item.IsDisposed)
                    {
                        closingTargets.Add(item);
                    }
                }
            }
            closingTargets.ForEach((item) => item.Close());

            FosJyuchuRepo.GetInstance().ClearTmpCache();
            Program.HatFApiClient.Logout();
            Program.IkkankaApiClient.Logout();

            // ログイン画面を開く
            this.Hide();
            var loginForm = new Login();
            loginForm.Show();
            Program.AppContext.MainForm = loginForm;

            _isLogouting = true;
            this.Close();
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            // 閉じる確認はFormClosingイベントで行う
            this.Close();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLogouting)
            {
                // ログアウトボタン経由で来た時
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (false == DialogHelper.OkCancelQuestion(this, "終了しますか?", true))
                {
                    e.Cancel = true;
                    return;
                }
            }

            Application.Exit();
        }

        //private string GetAssembly()
        //{
        //    var assm = System.Reflection.Assembly.GetExecutingAssembly();
        //    var name = assm.GetName();
        //    return name.Version.ToString();
        //}

        private void btnMasterEdit_Click(object sender, EventArgs e)
        {
            using (var me_Menu = new ME_Menu())
            {
                me_Menu.ShowDialog();
            }
        }
        private void btnPersonalSettings_Click(object sender, EventArgs e)
        {
            using (var ps_Main = new PS_Main())
            {
                ps_Main.ShowDialog();
            }
        }

        /// <summary>売上予定一覧</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnReadySales_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<ReadySales_Main>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }

        /// <summary>売上確定一覧</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnFixedSales_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<FixedSales_Main>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }

        private void btnPUAmountCheck_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<PU_BillingList>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }


        /// <summary>物件一覧</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnConstruction_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.ConstructionProject.ConstructionList>();
            form.Show();
            form.Activate();
        }

        private void btnWH_Receivings_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.Warehousing.WH_Receivings>();
            form.Show();
            form.Activate();

            //var form = FormFactory.GetModelessForm<Views.Warehousing.WH_ReceivingsDetail2>();
            //await form.LoadDataAndShowAsync("998419");

        }

        /// <summary>売上訂正一覧</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnSalesCorrectionList_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.SalesCorrection.SalesCorrectionList>();
            form.Show();
            form.Activate();
        }

        private void btnReverseStock_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<ReverseStockList>();
            form.Show();
            form.Activate();
        }

        /// <summary>売上確定前 利率異常チェック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnInterestRate_BeforeFix_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<InterestRate_BeforeFix>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }

        /// <summary>売上確定後 利率異常チェック</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnInterestRateFixed_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<InterestRate_Fixed>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }

        private void btnWH_Shippings_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.Warehousing.WH_Shippings>();
            form.Show();
            form.Activate();            
        }

        /// <summary>受発注一覧ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnOrders_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Orders_Main>();
            form.Show();
            // フォームが表示済みの場合のためにアクティブ化する
            form.Activate();
        }

        /// <summary>受発注物件登録ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnNewConstruction_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.ConstructionProject.NewConstructionDetail>();
            form.LoadDataAndShowAsync(NewConstructionDetail.ScreenMode.NewEntry);
            form.Show();
            form.Activate();
        }

        /// <summary>請求一覧ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnInvoice_Click(object sender, System.EventArgs e)
        {
            var form = FormFactory.GetModelessForm<InvoiceList>();
            form.Show();
            form.Activate();
        }

        /// <summary>請求済一覧ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnInvoicedAmount_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<InvoicedAmountList>();
            form.Show();
            form.Activate();
        }

        private void btnPurchaseDelivery_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Purchase_Receiving>();
            form.Show();
            form.Activate();
        }

        private void btnRedBlackProcessing_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<CreditNoteList>();
            form.Show();
            form.Activate();
        }

        private void btnInventoryCount_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<WH_Inventory>();
            form.Show();
            form.Activate();
        }

        /// <summary>納品一覧表（社内用）</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnInternalDelivery_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<InternalDelivery>();
            form.Show();
            form.Activate();
        }

        private void btnPurchaseAmountCheck_Click(object sender, EventArgs e)
        {
            PU_AmountCheck detail = FormFactory.GetModelessForm<PU_AmountCheck>();
            detail.Show();
            detail.Activate();
        }

        private void txtPasswordSetting_Click(object sender, EventArgs e)
        {
            var user = LoginRepo.GetInstance().CurrentUser;

            using (var form =new ME_PasswordSetting(user.EmployeeId, user.EmployeeCode, false))
            {
                form.ShowDialog();
            }
        }

        private void btnReturnStock_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<Views.ReturnReceiving.ReturnReceivings>();
            form.Show();
            form.Activate();
        }

        private void btnStockReplenishment_Click(object sender, EventArgs e)
        {
            var form = new WH_StockRefill();
            form.Show();
        }

        private void btnReverseStockDelivery_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<CorrectionDeliveryList>();
            form.Show();
            form.Activate();
        }

        private void btnDeliverySchedules_Click(object sender, EventArgs e)
        {
            var form = FormFactory.GetModelessForm<DeliverySchedules_Main>();
            form.Show();
            form.Activate();
        }
    }
}
