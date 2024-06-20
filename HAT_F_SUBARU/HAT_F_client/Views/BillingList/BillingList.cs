using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Models;
using HatFClient.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Util.Win.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace HatFClient.Views.BillingList
{
    public partial class BillingList : Form
    {
        private readonly List<string> ListLayOut = new();   //リストレイアウト
        private DataTable DtLayOut = new();                 //リスト設定データテーブルレイアウト
        private DataTable DtList = new();                   //FlexGrid用データテーブル
        private DataTable DtSiList = new();                 //明細データテーブル

        private HatF_InvoiceConfirmationRepo hatFInvoiceConfirmationRepo;
        private HatF_PaymentClassificationRepo hatFPaymentClassificationRepo;
        private HatF_PaymentMonthRepo hatFPaymentMonthRepo;
        private HatF_PaymentDayRepo hatFPaymentDayRepo;

        List<SupplierInvoices> supplierInvoices;
        public BillingList()
        {
            InitializeComponent();
        }
        private void BillingList_Load(object sender, EventArgs e)
        {

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            // レポジトリ取得
            SupplierInvoicesRepo repo = SupplierInvoicesRepo.GetInstance();
            this.hatFInvoiceConfirmationRepo = HatF_InvoiceConfirmationRepo.GetInstance();
            this.hatFPaymentClassificationRepo = HatF_PaymentClassificationRepo.GetInstance();
            this.hatFPaymentMonthRepo = HatF_PaymentMonthRepo.GetInstance();
            this.hatFPaymentDayRepo = HatF_PaymentDayRepo.GetInstance();

            // 条件コンボボックス
            SetComboBoxForInvoiceConfirmation();
            SetComboBoxForPaymentMonth();

            SetLayOutData();            // FelxGrid 構成情報セット
            InitDrLayOutForGridCols();  // FlexGrid 表示項目情報セット
            SetGridDataName();          // FlexGrid DataTable 紐づけ
            SetGridCols();              // FlexGrid グリッド項目セット１
            SetListDataInit();          // FlexGrid グリッド項目セット２
            SetComboBoxForFlexGrid();   // FlexGrid グリッドコンボ関連

            SetC1FlexGridStyleForRowBackColor(grdList);
            grdList.BeforeSort += C1FlexGrid_BeforeSort;
            grdList.AfterSort += C1FlexGrid_AfterSort;

        }

        #region << 条件コンボボックス >>
        private void SetComboBoxForInvoiceConfirmation()
        {
            this.cmbInvoiceConfirmation.Items.Clear();
            this.cmbInvoiceConfirmation.Items.Add(@"全件表示");
            for (int i = 0; i < hatFInvoiceConfirmationRepo.Entities.Count; i++)
            {
                this.cmbInvoiceConfirmation.Items.Add(hatFInvoiceConfirmationRepo.Entities[i].Name);
            }
            this.cmbInvoiceConfirmation.SelectedIndex = 0;
        }
        private void SetComboBoxForPaymentMonth()
        {
            this.cmbPaymentMonth.Items.Clear();
            this.cmbPaymentMonth.Items.Add(@"指定なし");
            for (int i = 0; i < hatFPaymentMonthRepo.Entities.Count; i++)
            {
                this.cmbPaymentMonth.Items.Add(hatFPaymentMonthRepo.Entities[i].Name);
            }
            this.cmbPaymentMonth.SelectedIndex = 0;
        }
        #endregion

        #region << グリッド >>
        private void SetLayOutData()
        {
            // ID,Caption,DataName,Width,Format,TextAlign,Flg,Sort,Filter,Edit,IsSort
            ListLayOut.Add("0,,No,40,,R,1,1,0,0,0");
            ListLayOut.Add("1,受注番号,OrderNo,130,,M,1,1,1,0,1");
            ListLayOut.Add("2,伝票番号,denNo,80,,M,1,1,1,0,1");
            ListLayOut.Add("3,仕入先\r\nコード,ShiresakiCd,80,,M,1,1,1,0,1");
            ListLayOut.Add("4,仕入先名,ShiresakiName,200,,L,1,1,1,0,1");
            ListLayOut.Add("5,発注合計金額,OrderAmnt,120,N2,R,1,1,1,0,1");
            ListLayOut.Add("6,消費税\r\n記号,CmpTax,60,,M,1,1,1,0,1");
            ListLayOut.Add("7,納期,Nouki,100,d,M,1,1,1,0,1");
            ListLayOut.Add("8,入力日,InpDate,100,d,M,1,1,1,0,1");
            ListLayOut.Add("9,入力担当,InpName,100,,L,1,1,1,0,1");
            ListLayOut.Add("10,営業担当,HatSales,100,,L,1,1,1,0,1");
            ListLayOut.Add("11,得意先,Tokuisaki,200,,L,1,1,1,0,1");
            ListLayOut.Add("12,得意先の\r\n営業担当,TokuisakiSales,100,,L,1,1,1,0,1");
            ListLayOut.Add("13,請求書確認,InvoiceConfirmation,100,,M,1,1,1,1,1");
            ListLayOut.Add("14,変更有無,DataChange,100,,M,1,1,1,0,1");
            ListLayOut.Add("15,仕入先\r\n支払月,ShiresakiPaymentMonth,80,,M,1,1,1,0,1");
            ListLayOut.Add("16,仕入先\r\n支払日,ShiresakiPaymentDay,80,,M,1,1,1,0,1");
            ListLayOut.Add("17,支払方法\r\n区分,ShiresakiPaymentClassification,80,,M,1,1,1,0,1");
            ListLayOut.Add("18,初期値,InitialValue,0,,M,1,1,0,0,0");
            ListLayOut.Add("19,変更フラグ,ChangeFlg,0,,M,1,1,0,0,0");
            ListLayOut.Add("20,PK,Key,0,,M,1,1,0,0,0");
        }
        private void SetListDataInit()
        {
            DtSiList.Columns.Add("No", typeof(string));                             // 表示番号
            DtSiList.Columns.Add("OrderNo", typeof(string));                        // 受注番号
            DtSiList.Columns.Add("denNo", typeof(string));                          // 伝票番号
            DtSiList.Columns.Add("ShiresakiCd", typeof(string));                    // 仕入先コード
            DtSiList.Columns.Add("ShiresakiName", typeof(string));                  // 仕入先名
            DtSiList.Columns.Add("OrderAmnt", typeof(decimal));                     // 発注合計金額
            DtSiList.Columns.Add("CmpTax", typeof(string));                         // 消費税記号
            DtSiList.Columns.Add("Nouki", typeof(DateTime));                        // 納期
            DtSiList.Columns.Add("InpDate", typeof(DateTime));                      // 入力日
            DtSiList.Columns.Add("InpName", typeof(string));                        // 入力者
            DtSiList.Columns.Add("HatSales", typeof(string));                       // 営業担当
            DtSiList.Columns.Add("Tokuisaki", typeof(string));                      // 得意先
            DtSiList.Columns.Add("TokuisakiSales", typeof(string));                 // 得意先の営業担当
            DtSiList.Columns.Add("InvoiceConfirmation", typeof(string));            // 請求書確認：0:未確認,1:確認済,2:編集中,3:違算
            DtSiList.Columns.Add("DataChange", typeof(string));                     // 変更有無
            DtSiList.Columns.Add("ShiresakiPaymentMonth", typeof(string));          // 仕入先支払月：0:当月,1:翌月,2:翌々月
            DtSiList.Columns.Add("ShiresakiPaymentDay", typeof(string));            // 仕入先支払日：10:10日払い,99：末日
            DtSiList.Columns.Add("ShiresakiPaymentClassification", typeof(string)); // 仕入先支払い区分：1:振込,2:手形
            DtSiList.Columns.Add("InitialValue", typeof(string));                   // 請求書確認 初期値
            DtSiList.Columns.Add("ChangeFlg", typeof(int));                         // 変更フラグ 1:変更有
            DtSiList.Columns.Add("Key", typeof(string));                            // PK
        }
        private void InitDrLayOutForGridCols()
        {
            DtLayOut.Columns.Add("Id", typeof(int));
            DtLayOut.Columns.Add("Caption", typeof(string));
            DtLayOut.Columns.Add("DataName", typeof(string));
            DtLayOut.Columns.Add("Width", typeof(int));
            DtLayOut.Columns.Add("Format", typeof(string));
            DtLayOut.Columns.Add("TextAlign", typeof(string));
            DtLayOut.Columns.Add("Flg", typeof(int));
            DtLayOut.Columns.Add("Sort", typeof(int));
            DtLayOut.Columns.Add("SortNull", typeof(int));
            DtLayOut.Columns.Add("Filter", typeof(int));
            DtLayOut.Columns.Add("Edit", typeof(int));
            DtLayOut.Columns.Add("IsSort", typeof(int));

            DataRow DrLayOut;
            for (int i = 0; i < ListLayOut.Count; i++)
            {
                string[] arrList = ListLayOut[i].Split(',');
                DrLayOut = DtLayOut.NewRow();
                DrLayOut["Id"] = int.Parse(arrList[0]);
                DrLayOut["Caption"] = arrList[1];
                DrLayOut["DataName"] = arrList[2];
                DrLayOut["Width"] = int.Parse(arrList[3]);
                DrLayOut["Format"] = arrList[4];
                DrLayOut["TextAlign"] = arrList[5];
                DrLayOut["Flg"] = int.Parse(arrList[6]);
                DrLayOut["Sort"] = int.Parse(arrList[7]);
                DrLayOut["SortNull"] = 0;
                DrLayOut["Filter"] = int.Parse(arrList[8]);
                DrLayOut["Edit"] = int.Parse(arrList[9]);
                DrLayOut["IsSort"] = int.Parse(arrList[10]);
                DtLayOut.Rows.Add(DrLayOut);
            }
        }
        private void SetGridDataName()
        {
            DataView dv = new DataView(DtLayOut);
            dv.Sort = "SortNull,Sort";
            DataTable result = dv.ToTable();
            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
            {
                DtList.Columns.Add(result.Rows[rowIndex]["DataName"].ToString(), typeof(string));
            }
        }
        private void SetGridCols()
        {
            DataView dv = new DataView(DtLayOut);
            dv.Sort = "SortNull,Sort";
            DataTable result = dv.ToTable();

            grdList.Cols.Fixed = 1;
            grdList.Cols.Count = result.Rows.Count;
            grdList.Rows[0].Height = 50;
            grdList.AllowFiltering = true;      //フィルタ設定
            grdList.AllowEditing = true;        //編集可

            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
            {
                grdList.Cols[rowIndex].Caption = result.Rows[rowIndex]["Caption"].ToString();
                grdList.Cols[rowIndex].Name = result.Rows[rowIndex]["DataName"].ToString();
                grdList.Cols[rowIndex].Width = (int)result.Rows[rowIndex]["Width"];
                grdList.Cols[rowIndex].Format = result.Rows[rowIndex]["Format"].ToString();

                if (result.Rows[rowIndex]["DataName"].Equals(@"DataChange"))
                {
                    CellStyle cs = grdList.Styles.Add("DataChangeStyle");
                    cs.ForeColor = System.Drawing.Color.Red;
                    cs.Font = new Font("Meiryo UI", 9, FontStyle.Bold);
                    grdList.Cols[rowIndex].Style = grdList.Styles["DataChangeStyle"]; ;
                }

                switch (result.Rows[rowIndex]["TextAlign"].ToString())
                {
                    case "L":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                        break;
                    case "M":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        break;
                    case "R":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        break;
                    default:
                        break;
                }
                grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                if ((int)result.Rows[rowIndex]["Flg"] == 1)
                {
                    grdList.Cols[rowIndex].Visible = true;
                }
                else
                {
                    grdList.Cols[rowIndex].Visible = false;
                }
                if ((int)result.Rows[rowIndex]["Filter"] == 0)
                {
                    grdList.Cols[rowIndex].AllowFiltering = AllowFiltering.None;
                }
                if ((int)result.Rows[rowIndex]["IsSort"] == 0)
                {
                    grdList.Cols[rowIndex].AllowSorting = false;
                }
                if ((int)result.Rows[rowIndex]["Edit"] == 0)
                {
                    grdList.Cols[rowIndex].AllowEditing = false;
                }
            }
        }
        private void SetSupplierInvoicesData()
        {

            DataRow dr;

            if (this.supplierInvoices == null)
            {
                return;
            }
            DtSiList.Clear();

            this.supplierInvoices.ForEach(x => {
                dr = DtSiList.NewRow();
                dr["OrderNo"] = x.OrderNo;
                dr["denNo"] = x.denNo;
                dr["ShiresakiCd"] = x.ShiresakiCd;
                dr["ShiresakiName"] = x.ShiresakiName;
                dr["OrderAmnt"] = x.OrderAmnt;
                dr["CmpTax"] = x.CmpTax;
                dr["Nouki"] = x.Nouki;
                dr["InpDate"] = x.InpDate;
                dr["InpName"] = x.InpName;
                dr["HatSales"] = x.HatSales;
                dr["Tokuisaki"] = x.Tokuisaki;
                dr["TokuisakiSales"] = x.TokuisakiSales;
                var invoiceConfirmation = this.hatFInvoiceConfirmationRepo.Entities.Find(op => op.Key.ToString() == x.InvoiceConfirmation.ToString());
                if (invoiceConfirmation != null)
                {
                    dr["InvoiceConfirmation"] = invoiceConfirmation.Name;
                }

                var shiresakiPaymentMonth = this.hatFPaymentMonthRepo.Entities.Find(op => op.Key.ToString() == x.ShiresakiPaymentMonth.ToString());
                if (shiresakiPaymentMonth != null)
                {
                    dr["ShiresakiPaymentMonth"] = shiresakiPaymentMonth.Name;
                }
                else
                {
                    dr["ShiresakiPaymentMonth"] = "*" + x.ShiresakiPaymentMonth.ToString();
                }
                var shiresakiPaymentDay = this.hatFPaymentDayRepo.Entities.Find(op => op.Key.ToString() == x.ShiresakiPaymentDay.ToString());
                if (shiresakiPaymentDay != null)
                {
                    dr["ShiresakiPaymentDay"] = shiresakiPaymentDay.Name;
                }
                else
                {
                    dr["ShiresakiPaymentDay"] = "*" + x.ShiresakiPaymentDay.ToString();
                }
                var shiresakiPaymentClassification = this.hatFPaymentClassificationRepo.Entities.Find(op => op.Key.ToString() == x.ShiresakiPaymentClassification.ToString());
                if (shiresakiPaymentClassification != null)
                {
                    dr["ShiresakiPaymentClassification"] = shiresakiPaymentClassification.Name;
                }
                else
                {
                    dr["ShiresakiPaymentClassification"] = "*" + x.ShiresakiPaymentClassification.ToString();
                }

                dr["InitialValue"] = dr["InvoiceConfirmation"];
                dr["ChangeFlg"] = 0;
                dr["Key"] = x.OrderNo;

                DtSiList.Rows.Add(dr);
            });

            DataView dv = new(DtSiList);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);

            for (int i = grdList.Rows.Fixed; i < grdList.Rows.Count; i++)
            {
                grdList[i, 0] = i.ToString();
            }
            txtroCount.Text = DtSiList.Rows.Count.ToString();
            SetBackColorInvoiceConfirmation();
        }
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled == true)
            {
                btnDetail.PerformClick();
            }
        }
        #endregion

        #region << グリッド背景色等 >>
        private void SetCellColor(int intRow, int intCol)
        {
            string strVal = grdList.GetData(intRow, intCol).ToString();
            var invoiceConfirmation = this.hatFInvoiceConfirmationRepo.Entities.Find(op => op.Name.ToString().Equals(strVal));
            if (invoiceConfirmation != null)
            {
                string strColor = invoiceConfirmation.Color;
                Color colColor = Color.FromName(strColor);
                grdList.GetCellRange(intRow, intCol).StyleNew.BackColor = colColor;
            }
        }
        private int IntGetColNo(string strTitle)
        {
            int colNo = 0;
            for (int i = 0; i < grdList.Cols.Count; i++)
            {
                if (grdList.GetData(0, i).ToString().Equals(strTitle))
                {
                    colNo = i; break;
                }
            }
            return colNo;
        }
        private void SetBackColorInvoiceConfirmation()
        {
            int intColNo = IntGetColNo(@"請求書確認");
            for (int i = grdList.Rows.Fixed; i < grdList.Rows.Count; i++)
            {
                SetCellColor(i, intColNo);
            }
        }
        private void SetC1FlexGridStyleForRowBackColor(C1FlexGrid grdName)
        {
            grdName.Styles.Add("StyleChangeRowColor");
            grdName.Styles["StyleChangeRowColor"].BackColor = Color.LightYellow;
            grdName.Styles.Add("StyleDefaultRowColor");
            grdName.Styles["StyleDefaultRowColor"].BackColor = Color.White;
        }
        private void SetC1FlexGridRowBackColor(C1FlexGrid grdName)
        {
            for (int i = grdName.Rows.Fixed; i < grdName.Rows.Count; i++)
            {
                string strFlg = grdName.GetData(i, 19).ToString();
                if (strFlg.Equals(@"1"))
                {
                    grdName.Rows[i].Style = grdName.Styles["StyleChangeRowColor"];
                }
                else
                {
                    grdName.Rows[i].Style = grdName.Styles["StyleDefaultRowColor"];
                }
            }
        }
        private void C1FlexGrid_BeforeSort(object sender, SortColEventArgs e)
        {
            for (int i = ((C1FlexGrid)sender).Rows.Fixed; i < ((C1FlexGrid)sender).Rows.Count; i++)
            {
                ((C1FlexGrid)sender).Rows[i].Style = ((C1FlexGrid)sender).Styles["StyleDefaultRowColor"];
            }
        }
        private void C1FlexGrid_AfterSort(object sender, SortColEventArgs e)
        {
            SetC1FlexGridRowBackColor((C1FlexGrid)sender);
            SetBackColorInvoiceConfirmation();
        }
        #endregion

        #region << グリッド内コンボボックス >>
        private void SetComboBoxForFlexGrid()
        {
            C1ComboBox c1ComboBox = new();
            c1ComboBox.ItemsDataSource = hatFInvoiceConfirmationRepo.Entities;
            c1ComboBox.ItemsDisplayMember = "Name";
            c1ComboBox.ItemsValueMember = "Name";
            c1ComboBox.TextDetached = true;
            grdList.Cols[13].Editor = c1ComboBox;
            c1ComboBox.SelectedIndexChanged += C1ComboBox_SelectedIndexChanged;
        }
        private void C1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdList.FinishEditing();
            SetCellColor(grdList.RowSel, grdList.CursorCell.LeftCol);
            if (grdList.GetData(grdList.RowSel, grdList.CursorCell.LeftCol).Equals(grdList.GetData(grdList.RowSel, 18)))
            {
                grdList[grdList.RowSel, 19] = 0;
                grdList[grdList.RowSel, IntGetColNo(@"変更有無")] = @"";
            }
            else
            {
                grdList[grdList.RowSel, 19] = 1;
                grdList[grdList.RowSel, IntGetColNo(@"変更有無")] = @"変更あり";
            }
            for (int i = grdList.Rows.Fixed; i < grdList.Rows.Count; i++)
            {
                grdList[i, 0] = i.ToString();
            }
            SetC1FlexGridRowBackColor(grdList);
        }
        #endregion

        #region << ボタン >>
        private async void BtnSearch_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"検索しますか？", ((Button)sender).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            // レポジトリ取得
            SupplierInvoicesRepo repo = SupplierInvoicesRepo.GetInstance();
            supplierInvoices = await repo.getAll();
            SetSupplierInvoicesData();
        }
        private void BtnDetail_Click(object sender, System.EventArgs e)
        {
            if (grdList.Rows.Count < 2) { return; }

            using (BillingSheet dlg = new())
            {
                dlg.StrOrderNo = grdList.GetData(grdList.RowSel,20).ToString();
                dlg.StrInvoiceConfirmation = grdList.GetData(grdList.RowSel, IntGetColNo(@"請求書確認")).ToString();
                switch (dlg.ShowDialog())
                {
                    case DialogResult.OK:
                        break;
                    default:
                        break;
                }
            }
        }
        private void BtnPrint_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"印刷しますか？", ((Button)sender).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
        }
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"保存しますか？", ((Button)sender).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
        }
        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"本画面を閉じますか？", ((Button)sender).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.Close();
        }
        #endregion

    }
}
