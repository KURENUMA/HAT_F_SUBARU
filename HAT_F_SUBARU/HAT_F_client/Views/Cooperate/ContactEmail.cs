using System;
using System.Windows.Forms;
using HatFClient.Common;
using HatFClient.Repository;

using NetOffice;
using Outlook = NetOffice.OutlookApi;
using NetOffice.OutlookApi.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HatFClient.Views.Cooperate
{
    public partial class ContactEmail : Form
    {
        private EmployeeRepo employeeRepo;

        public ContactEmail()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                // 共通スタイル設定
                FormStyleHelper.SetFixedSizeDialogStyle(this);

                employeeRepo = EmployeeRepo.GetInstance();

                blobStrageForm1.Init("contact_email");
            }

        }

        private async Task SetSendableEmployees()
        {
            var employees = await employeeRepo.GetSendableEmplyeesAsync();
            cmbTO.Items.Clear();
            cmbTO.SetItems(employees.ConvertAll<string>(x => string.Format("{0}<{1}>", x.EmpName, x.Email)));
        }

        private async Task ContactEmail_Load(object sender, EventArgs e)
        {
            await SetSendableEmployees();
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSEND_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbTO.Text))
            {
                DialogHelper.WarningMessage(this, "連絡先を指定してください。");
                return;
            }

            Outlook.Application outlookApplication = new Outlook.Application();

            Outlook.MailItem mailItem = outlookApplication.CreateItem(OlItemType.olMailItem) as Outlook.MailItem;
            if (mailItem != null )
            {
                mailItem.Recipients.Add(cmbTO.Text);

                mailItem.Subject = txtSUBJECT.Text;

                mailItem.Body = txtBODY.Text;

                List<string> fileList = await blobStrageForm1.DownloadAllAsync();
                foreach (var file in fileList)
                {
                    mailItem.Attachments.Add(file);
                }

                mailItem.Recipients.ResolveAll();

                mailItem.Display(true);

            }
        }
    }
}
