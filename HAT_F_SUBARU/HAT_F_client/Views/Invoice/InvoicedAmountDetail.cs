using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Helpers;
using HatFClient.Models;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Estimate;
using HatFClient.Views.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Invoice
{
    public partial class InvoicedAmountDetail : Form
    {
        private List<ViewInvoiceDetail> ViewInvoiceDetailData { get; set; }
        private string companyCode { get; set; }

        public event System.EventHandler Search;

        List<string> billingStatus = new List<string> { "0:未請求", "1:請求書発行済", "2:請求書送付済", "3:入金済", "4:完了" };

        // 橋本総業ファシリティーズのカンパニーコード
        // 実運用でマスタが変更された際は更新する必要あり
        private const string HATF_COMPANY_CODE = "999239";

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {            
            Search?.Invoke(this, e);
        }


        // 検索用の変数
        //private DefaultGridUpdate projectGrid1;
        private static readonly Type TARGET_MODEL = typeof(ViewInvoiceDetail);

        public InvoicedAmountDetail()
        {
            InitializeComponent();
            
            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);

                this.gridInvoiceDetail.SelChange += new System.EventHandler(gridInvoiceDetail_SelChange);
            }
        }

        private void gridInvoiceDetail_SelChange(object sender, EventArgs e)
        {
            var grid = sender as C1FlexGrid;
            int selectedRow = grid.Row;

            if (selectedRow >= grid.Rows.Fixed)
            {
                int statusColIndex = grid.Cols.IndexOf("請求状態");
                int statusValue = Convert.ToInt32(grid[selectedRow, statusColIndex]);

                // 請求状態に基づいて処理を分岐
                switch (statusValue)
                {
                    case 0: // 未請求
                        radioUnbilled.Checked = true;
                        break;
                    case 1: // 請求書発行済
                        radioInvoiceIssued.Checked = true;
                        break;
                    case 2: // 請求書送付済
                        radioInvoiceSent.Checked = true;
                        break;
                    case 3: // 入金済
                        radioPaymentReceived.Checked = true;
                        break;
                    case 4: // 完了
                        radioCompleted.Checked = true;
                        break;
                    default:
                        radioUnbilled.Checked = true;
                        break;
                }
            }
        }

        public void LoadDataAndShow(List<ViewInvoiceDetail> InvoiceDetailData = null)
        {
            this.companyCode = InvoiceDetailData[0].得意先コード;
            this.ViewInvoiceDetailData = InvoiceDetailData;

            lblCompCode.Text = "得意先コード：" + companyCode;
            lblCompName.Text = "得意先名：" + InvoiceDetailData[0].得意先名;

            // 請求明細バインド
            gridInvoiceDetail.DataSource = ViewInvoiceDetailData;

            this.Show();
            this.Activate();
        }

        private void InvoiceDetail_Load(object sender, EventArgs e)
        {
            // Gridの初期設定
            InitializeColumns();

            // 自社情報取得処理
            InitializeCompanyGrid();

            // 口座情報
            InitializePaymentGrid();

            // 請求総計
            InitializeInvoiceTotalGrid();
        }

        private void InitializeColumns()
        {
            // コンボボックスの選択肢を設定
            Dictionary<string, string> comboBoxItems = new Dictionary<string, string>();
            foreach (string item in billingStatus)
            {
                comboBoxItems.Add(item, item);
            }

            gridInvoiceDetail.Cols[nameof(ViewInvoiceDetail.請求状態)].DataMap = comboBoxItems;

            // 請求状態列の値置き換え
            var billingStatusMap = new ListDictionary()
            {
                { (short)short.MinValue, "" },
                { (short)0, "未請求" },
                { (short)1, "請求書発行済" },
                { (short)2, "請求書送付済" },
                { (short)3, "入金済" },
                { (short)4, "完了" }
            };

            gridInvoiceDetail.Cols["請求状態"].DataMap = billingStatusMap;
            gridInvoiceDetail.Cols["請求状態"].AllowEditing = true;
        }

        /// <summary>
        /// 自社情報の反映
        /// </summary>
        private void InitializeCompanyGrid()
        {
            AdjustGridSize(gridCompany);
            InitializeCompanyGridHeaders();
            LoadCompanyDetailsAsync();
        }

        /// <summary>
        /// 自社情報のGridヘッダ情報設定
        /// </summary>
        private void InitializeCompanyGridHeaders()
        {
            gridCompany.Rows.Count = 5;
            gridCompany.Cols[1].AllowEditing = true;
            gridCompany[0, 0] = "業者名";
            gridCompany[1, 0] = "カナ";
            gridCompany[2, 0] = "TEL";
            gridCompany[3, 0] = "FAX";
            gridCompany[4, 0] = "部門/担当者";
        }

        /// <summary>
        /// 自社情報の取得
        /// </summary>
        private async void LoadCompanyDetailsAsync()
        {
            /*
            var CompanyInfo = await Program.HatFApiClient.GetAsync<CompanysMst>(ApiResources.HatF.CompanyInfo, new { HATF_COMPANY_CODE });

            if (CompanyInfo.Data != null)
            {
                UpdateCompanyGridWithDetails(CompanyInfo.Data);
            }
            */
            SearchRepo repo = SearchRepo.GetInstance();
            var hatfInfo = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchTorihiki(HATF_COMPANY_CODE, string.Empty, string.Empty, string.Empty, 1);
            });

            if (!hatfInfo.Failed && hatfInfo.Value != null && hatfInfo.Value.Count > 0)
            {
                UpdateCompanyGridWithDetails(hatfInfo.Value);
            }
        }

        /// <summary>
        /// 自社情報の設定
        /// <param name="info">自社情報</param>
        /// </summary>
        private void UpdateCompanyGridWithDetails(List<Torihiki> hatfInfo)
        {
            gridCompany[0, 1] = hatfInfo[0].TokuZ;
            gridCompany[1, 1] = hatfInfo[0].TokuH;
            gridCompany[2, 1] = hatfInfo[0].ATel;
            gridCompany[3, 1] = hatfInfo[0].AFax;
            gridCompany[4, 1] = LoginRepo.GetInstance().CurrentUser.EmployeeName;
        }


        /// <summary>
        /// 振込先口座の反映
        /// </summary>
        private void InitializePaymentGrid()
        {
            AdjustGridSize(gridPayment);
            InitializeGridHeaders();
            LoadBankAccountDetailsAsync();
        }

        /// <summary>
        /// 振込先口座のGridヘッダ情報設定
        /// </summary>
        private void InitializeGridHeaders()
        {
            gridPayment.Rows.Count = 5;
            gridPayment.Cols[1].AllowEditing = true;
            gridPayment.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            gridPayment[0, 0] = "振込先";
            gridPayment[1, 0] = "支店";
            gridPayment[2, 0] = "口座種別";
            gridPayment[3, 0] = "口座番号";
            gridPayment[4, 0] = "口座人名義";
        }

        /// <summary>
        /// 振込先口座の取得
        /// </summary>
        private async void LoadBankAccountDetailsAsync()
        {
            var bankAcut = await Program.HatFApiClient.GetAsync<BankAcutMst>(ApiResources.HatF.Client.InvoiceBank, new { companyCode });

            if (bankAcut.Data != null)
            {
                UpdateGridWithBankDetails(bankAcut.Data);
            }
        }

        /// <summary>
        /// 振込先口座の設定
        /// <param name="bankAcut">入金口座情報</param>
        /// </summary>
        private void UpdateGridWithBankDetails(BankAcutMst bankAcut)
        {
            gridPayment[0, 1] = bankAcut.StartActName;
            gridPayment[1, 1] = bankAcut.ABankCode;

            // BankActType を設定する前にBankActType の値に基づいて文字列を置き換え
            string actTypeDescription;
            switch (bankAcut.BankActType)
            {
                case "O":
                    actTypeDescription = "普通";
                    break;
                case "C":
                    actTypeDescription = "当座";
                    break;
                default:
                    actTypeDescription = "";
                    break;
            }
            gridPayment[2, 1] = actTypeDescription;

            gridPayment[3, 1] = bankAcut.ReciveActNo;
            gridPayment[4, 1] = bankAcut.ActName;
        }

        /// <summary>
        /// 合計金額の反映
        /// </summary>
        private void InitializeInvoiceTotalGrid()
        {
            AdjustGridSize(gridInvoiceTotal);
            SetupInvoiceTotalGridHeaders();
            CalculateAndDisplayTotals();
        }

        /// <summary>
        /// 合計金額ののGridヘッダ情報設定
        /// </summary>
        private void SetupInvoiceTotalGridHeaders()
        {
            gridInvoiceTotal.Rows.Count = 5;
            gridInvoiceTotal[0, 0] = "税抜小計";
            gridInvoiceTotal[1, 0] = "消費税等";
            gridInvoiceTotal[2, 0] = "請求合計";
            gridInvoiceTotal[3, 0] = "入金額の入力";
            gridInvoiceTotal[4, 0] = "差額";
        }

        private void CalculateAndDisplayTotals()
        {
            var totals = CalculateTotals();
            DisplayTotals(totals);
        }

        /// <summary>
        /// 明細から合計金額を取得
        /// </summary>
        private (double totalBilling, double totalTax) CalculateTotals()
        {
            string billingColumnName = "当月請求額";
            string taxColumnName = "消費税金額";
            int billingColumnIndex = gridInvoiceDetail.Cols.IndexOf(billingColumnName);
            int taxColumnIndex = gridInvoiceDetail.Cols.IndexOf(taxColumnName);
            double totalBilling = 0;
            double totalTax = 0;

            // 当月請求額の合計を計算
            if (billingColumnIndex != -1)
            {
                for (int row = 1; row < gridInvoiceDetail.Rows.Count; row++)
                {
                    if (double.TryParse(gridInvoiceDetail[row, billingColumnIndex].ToString(), out double value))
                    {
                        totalBilling += value;
                    }
                }
            }

            // 消費税金額の合計を計算
            if (taxColumnIndex != -1)
            {
                for (int row = 1; row < gridInvoiceDetail.Rows.Count; row++)
                {
                    if (double.TryParse(gridInvoiceDetail[row, taxColumnIndex].ToString(), out double value))
                    {
                        totalTax += value;
                    }
                }
            }

            return (totalBilling, totalTax);
        }

        /// <summary>
        /// 合計金額の設定
        /// <param name="totalBilling">合計金額</param>
        /// <param name="totalTax">消費税合計金額</param>
        /// </summary>
        private void DisplayTotals((double totalBilling, double totalTax) totals)
        {
            gridInvoiceTotal[0, 1] = totals.totalBilling;
            gridInvoiceTotal[1, 1] = totals.totalTax;
            gridInvoiceTotal[2, 1] = totals.totalBilling - totals.totalTax;
        }

        /// <summary>
        /// 入力金額のチェック
        /// <param name="totalBilling">合計金額</param>
        /// <param name="totalTax">消費税合計金額</param>
        /// </summary>
        private void SubtractAndSetValue()
        {
            var value1 = Convert.ToDouble(gridInvoiceTotal[2, 1]);
            var value2 = Convert.ToDouble(gridInvoiceTotal[3, 1]);
            gridInvoiceTotal[4, 1] = value2 - value1;
        }


        private void AdjustGridSize(C1FlexGrid grid)
        {
            int colCount = grid.Cols.Count;
            int rowCount = grid.Rows.Count;

            if (colCount > 0 && rowCount > 0)
            {
                int availableWidth = grid.Width - grid.Cols[0].Width;
                int availableHeight = grid.Height - grid.Rows[0].Height;

                for (int i = 1; i < colCount; i++)
                {
                    grid.Cols[i].Width = availableWidth / (colCount - 1);
                }

                for (int i = 1; i < rowCount; i++)
                {
                    grid.Rows[i].Height = availableHeight / (rowCount - 1);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            // 2列目の4行目に値を設定
            gridInvoiceTotal[3, 1] = gridInvoiceTotal[2, 1];
            SubtractAndSetValue();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            SubtractAndSetValue();
        }
    }
}
