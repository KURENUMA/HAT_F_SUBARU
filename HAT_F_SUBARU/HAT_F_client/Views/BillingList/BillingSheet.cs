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

namespace HatFClient.Views.BillingList
{
    public partial class BillingSheet : Form
    {
        private HatF_InvoiceConfirmationRepo hatFInvoiceConfirmationRepo;
        private string _orderNo;
        private string _invoiceConfirmation;
        public string StrOrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }
        public string StrInvoiceConfirmation
        {
            get { return _invoiceConfirmation; }
            set { _invoiceConfirmation = value; }
        }
        public BillingSheet()
        {
            InitializeComponent();
        }
        private void BillingSheet_Load(object sender, EventArgs e)
        {
            this.hatFInvoiceConfirmationRepo = HatF_InvoiceConfirmationRepo.GetInstance();
            SetComboBoxForInvoiceConfirmation();

            InitForm();
            txtroORDER_NO.Text = _orderNo;
            cmbInvoiceConfirmation.Text = StrInvoiceConfirmation;
        }
        private void InitForm()
        {
            txtroORDER_NO.Clear();
            txtroORDER_TOTAL_AMOUNT.Clear();
            txtroCONSUMPTION_TAX_AMOUNT.Clear();
            txtroSUPPLIER_PAYMENT_MONTH.Clear();
            txtroSUPPLIER_PAYMENT_DATE.Clear();
            txtroSUPPLIER_PAYMENT_CATEGORY.Clear();
            cmbInvoiceConfirmation.SelectedIndex = -1;
            InitDgvList();
        }
        private void SetComboBoxForInvoiceConfirmation()
        {
            this.cmbInvoiceConfirmation.Items.Clear();
            for (int i = 0; i < hatFInvoiceConfirmationRepo.Entities.Count; i++)
            {
                this.cmbInvoiceConfirmation.Items.Add(hatFInvoiceConfirmationRepo.Entities[i].Name);
            }
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[] { @"Code", @"Name", @"UnitPrice", @"Quantity" };
            var columnTexts = new string[] { @"商品コード", @"商品名", @"仕入単価", @"発注数量" };
            var columnWidths = new int[] { 100, 200, 120, 120};

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
            dgvList.ColumnHeadersHeight = 30;
            dgvList.RowTemplate.Height = 20;
        }
        private void BtnClose_Click(object sender, System.EventArgs e)
        {

        }
    }
}
