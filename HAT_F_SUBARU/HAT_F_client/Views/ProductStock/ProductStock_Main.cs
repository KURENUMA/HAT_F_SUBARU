using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using HatFClient.Repository;
using HAT_F_api.CustomModels;
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
using HatFClient.Common;
using HatFClient.Constants;
using HAT_F_api.Models;

namespace HatFClient.Views.ProductStock
{
    public partial class ProductStock_Main : Form
    {
        private readonly List<string> ListLayOut = new();   //リストレイアウト
        private DataTable DtLayOut = new();                 //リスト設定データテーブルレイアウト
        private DataTable DtList = new();                   //FlexGrid用データテーブル
        private DataTable DtPsList = new();                 //明細データテーブル

        private const int NUM_RECS_PER_PAGE = 4; // 1ページの表示件数

        List<ViewProductStock> productStocks;
        ViewProductStock summary;

        public ProductStock_Main()
        {
            InitializeComponent();
        }
        private async void ProductStock_Main_Load(object sender, EventArgs e)
        {

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            // レポジトリ取得
            ProductStockRepo repo = ProductStockRepo.GetInstance();

            ApiResult<ViewProductStock> summaryResult = await ApiHelper.FetchAsync<ViewProductStock>(this, async () => { 
                return await repo.GetSummaryApiResponseAsync();
            });

            if (summaryResult.Failed)
            {
                // ページロード中の失敗は画面を閉じる
                this.Close();
                return;
            }

            summary = summaryResult.Value;

            SetLayOutData();            // FelxGrid 構成情報セット
            InitDrLayOutForGridCols();  // FlexGrid 表示項目情報セット
            SetGridDataName();          // FlexGrid DataTable 紐づけ
            SetGridCols();              // FlexGrid グリッド項目セット

            SetListDataInit();


            // 総商品数
            //txtroCount.Text = DtPsList.Rows.Count.ToString("#,#");
            //txtroCount.Text = summary.ProductCountTotal.ToString("#,#");

            ApiResult<int> totalStockCountResult = await ApiHelper.FetchAsync<int>(this, async () => {
                return await Program.HatFApiClient.GetAsync<int>(ApiResources.HatF.Client.ProductStockCount);
            });

            if (totalStockCountResult.Failed)
            {
                // ページロード中の失敗は画面を閉じる
                this.Close();
                return;
            }

            // 総商品数
            txtroCount.Text = totalStockCountResult.Value.ToString("#,#");


            // PAGER
            // 在庫一覧リスト取得
            pager.onPageChange += async (object sender, int page) =>
            {
                ApiResult<List<ViewProductStock>> stockListResult = await ApiHelper.FetchAsync<List<ViewProductStock>>(this, async () => {
                    return await repo.getPageApiResponseAsync(page, NUM_RECS_PER_PAGE);
                });
                if (stockListResult.Failed)
                {
                    // ページロード中ではないので画面はそのまま
                    return;
                }

                productStocks = stockListResult.Value;
                SetProductStockData();
            };


            ApiResult<int> allCountResult = await ApiHelper.FetchAsync<int>(this, async () => {
                return await repo.GetAllCountApiResponseAsync();
            });

            if (allCountResult.Failed)
            {
                // ページロード中の失敗は画面を閉じる
                this.Close();
                return;
            }

            //pager.TotalPages = (summary.NumProducts - 1) / NUM_RECS_PER_PAGE + 1;
            //pager.TotalPages = (summary.ProductCountTotal - 1) / NUM_RECS_PER_PAGE + 1;
            int listCount = allCountResult.Value;
            pager.TotalPages = (listCount - 1) / NUM_RECS_PER_PAGE + 1;


            // 初期データ表示
            //productStocks = await repo.getPage(0, NUM_RECS_PER_PAGE);
            ApiResult<List<ViewProductStock>> stockListResult = await ApiHelper.FetchAsync<List<ViewProductStock>>(this, async () => {
                return await repo.getPageApiResponseAsync(1, NUM_RECS_PER_PAGE);
            });

            if (stockListResult.Failed)
            {
                // ページロード中の失敗は画面を閉じる
                this.Close();
                return;
            }

            productStocks = stockListResult.Value;

            SetProductStockData();

            txtroBeginningBalance.Text = summary.期首残高.ToString("#,#");
            txtroStockReceiptTotal.Text = (summary.入庫計 ?? 0).ToString("#,#");
            txtroStockOutputTotal.Text = (summary.出庫計 ?? 0).ToString("#,#");
            txtroBalance.Text = (summary.期首残高 + summary.入庫計 - summary.出庫計 ?? 0).ToString("#,#");
            txtroPrice.Text = summary.期末評価価格.ToString("#,#");
        }

        #region << グリッド >>
        private void SetLayOutData()
        {
            // ID,Caption,DataName,Width,Format,TextAlign,Flg,Sort,Filter,Edit,IsSort
            ListLayOut.Add("1,商品コード,ProdCode,100,,L,1,1,1,0,1");
            ListLayOut.Add("2,商品名,ProdName,300,,L,1,1,1,0,1");
            ListLayOut.Add("3,商品分類名,ProdCategoryName,200,,L,1,1,1,0,1");
            ListLayOut.Add("4,期首残高,BeginningBalance,90,N0,R,1,1,1,0,1");
            ListLayOut.Add("5,,LblPlus,20,,M,1,1,0,0,0");
            ListLayOut.Add("6,入庫数量計,StockReceiptTotal,90,N0,R,1,1,1,0,1");
            ListLayOut.Add("7,,LblMinus,20,,M,1,1,0,0,0");
            ListLayOut.Add("8,出庫数量計,StockOutputTotal,90,N0,R,1,1,1,0,1");
            ListLayOut.Add("9,,LblEqual,20,,M,1,1,0,0,0");
            ListLayOut.Add("10,期末残高,Balance,90,N0,R,1,1,1,0,1");
            ListLayOut.Add("11,期末評価価格,PeriodEndEvaluationPrice,110,N0,R,1,1,1,0,1");
            ListLayOut.Add("12,金額,Price,110,N0,R,1,1,1,0,1");
        }
        private void SetListDataInit()
        {
            DtPsList.Columns.Add("ProdCode", typeof(string));               // 商品コード
            DtPsList.Columns.Add("ProdName", typeof(string));               // 商品名
            DtPsList.Columns.Add("ProdCategoryName", typeof(string));       // 商品分類名
            DtPsList.Columns.Add("BeginningBalance", typeof(int));          // 期首残高
            DtPsList.Columns.Add("LblPlus", typeof(string));                // ＋
            DtPsList.Columns.Add("StockReceiptTotal", typeof(int));         // 入庫数量計
            DtPsList.Columns.Add("LblMinus", typeof(string));               // －
            DtPsList.Columns.Add("StockOutputTotal", typeof(int));          // 出庫数量計
            DtPsList.Columns.Add("LblEqual", typeof(string));               // ＝
            DtPsList.Columns.Add("Balance", typeof(int));                   // 期末残高
            DtPsList.Columns.Add("PeriodEndEvaluationPrice", typeof(int));  // 期末評価価格
            DtPsList.Columns.Add("Price", typeof(int));                     // 金額
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
        //private async Task FetchAndShowPage(int pageNo) {
        //    ProductStockRepo repo = ProductStockRepo.GetInstance();
        //    productStocks = await repo.getPage(pageNo, NUM_RECS_PER_PAGE);
        //    SetProductStockData();
        //}
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

            grdList.Cols.Fixed = 0;
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
        private void ShowGridData()
        {
            DtPsList.Clear();
            DataView dv = new(DtPsList);
            DataTable result = dv.ToTable();
            grdList.ClearFilter();
            grdList.SetDataBinding(result, "", true);
        }
        private void SetProductStockData()
        {

            DataRow dr;

            if (this.productStocks == null)
            {
                return;
            }
            DtPsList.Clear();

            this.productStocks.ForEach(x => {
                dr = DtPsList.NewRow();
                //dr["ProdCode"] = x.ProdCode;
                //dr["ProdName"] = x.ProdName;
                //dr["ProdCategoryName"] = x.ProdCategoryName;
                //dr["BeginningBalance"] = x.BeginningBalance;
                //dr["LblPlus"] = @"＋";
                //dr["StockReceiptTotal"] = x.StockReceiptTotal;
                //dr["LblMinus"] = @"－";
                //dr["StockOutputTotal"] = x.StockOutputTotal;
                //dr["Balance"] = x.BeginningBalance + x.StockReceiptTotal - x.StockOutputTotal;
                //dr["LblEqual"] = @"＝";
                //dr["PeriodEndEvaluationPrice"] = x.PeriodEndEvaluationPrice;
                //dr["Price"] = (x.BeginningBalance + x.StockReceiptTotal - x.StockOutputTotal) * x.PeriodEndEvaluationPrice;
                dr["ProdCode"] = x.商品コード;
                dr["ProdName"] = x.商品名;
                dr["ProdCategoryName"] = x.商品分類名;
                dr["BeginningBalance"] = x.期首残高;
                dr["LblPlus"] = @"＋";
                dr["StockReceiptTotal"] = x.入庫計;
                dr["LblMinus"] = @"－";
                dr["StockOutputTotal"] = x.出庫計;
                dr["Balance"] = x.期首残高 + x.入庫計 - x.出庫計;
                dr["LblEqual"] = @"＝";
                dr["PeriodEndEvaluationPrice"] = x.期末評価価格;
                dr["Price"] = (x.期首残高 + x.入庫計 - x.出庫計) * x.期末評価価格;

                DtPsList.Rows.Add(dr);
            });


            DataView dv = new(DtPsList);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);
        }
        #endregion

        #region << グリッド >>
        private void CalcGrdList()
        {
            int intBeginningBalance = 0;
            int intStockReceiptTotal = 0;
            int intStockOutputTotal = 0;
            int intPeriodEndEvaluationPrice = 0;
            if (int.TryParse(grdList.GetData(grdList.Row, 3).ToString(), out int intBB)) { intBeginningBalance = intBB; }
            if (int.TryParse(grdList.GetData(grdList.Row, 5).ToString(), out int intSR)) { intStockReceiptTotal = intSR; }
            if (int.TryParse(grdList.GetData(grdList.Row, 7).ToString(), out int intSO)) { intStockOutputTotal = intSO; }
            if (int.TryParse(grdList.GetData(grdList.Row, 10).ToString(), out int intPP)) { intPeriodEndEvaluationPrice = intPP; }

            var index = DtPsList.Rows.IndexOf(DtPsList.AsEnumerable().Where(a => a.Field<string>(0) == grdList.GetData(grdList.Row, 0).ToString()).FirstOrDefault());

            DtPsList.Rows[index]["BeginningBalance"] = intBeginningBalance;
            DtPsList.Rows[index]["StockReceiptTotal"] = intStockReceiptTotal;
            DtPsList.Rows[index]["StockOutputTotal"] = intStockOutputTotal;
            DtPsList.Rows[index]["Balance"] = intBeginningBalance + intStockReceiptTotal - intStockOutputTotal;
            DtPsList.Rows[index]["PeriodEndEvaluationPrice"] = intPeriodEndEvaluationPrice;
            DtPsList.Rows[index]["Price"] = (intBeginningBalance + intStockReceiptTotal - intStockOutputTotal) * intPeriodEndEvaluationPrice;

            DtPsList.AcceptChanges();
            grdList.SetDataBinding(DtPsList, "", true);
        }
        private void GrdList_AfterEdit(object sender, RowColEventArgs e)
        {
            CalcGrdList();
            SetFooter();
        }
        private void SetFooter()
        {
            int sumBeginningBalance = Convert.ToInt32(DtPsList.Compute("Sum(BeginningBalance)", null));
            txtroBeginningBalance.Text = sumBeginningBalance.ToString("#,#");
            int sumStockReceiptTotal = Convert.ToInt32(DtPsList.Compute("Sum(StockReceiptTotal)", null));
            txtroStockReceiptTotal.Text = sumStockReceiptTotal.ToString("#,#");
            int sumStockOutputTotal = Convert.ToInt32(DtPsList.Compute("Sum(StockOutputTotal)", null));
            txtroStockOutputTotal.Text = sumStockOutputTotal.ToString("#,#");
            int sumBalance = Convert.ToInt32(DtPsList.Compute("Sum(Balance)", null));
            txtroBalance.Text = sumBalance.ToString("#,#");
            int sumPrice = Convert.ToInt32(DtPsList.Compute("Sum(Price)", null));
            txtroPrice.Text = sumPrice.ToString("#,#");
        }
        #endregion

        #region << ボタン >>
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"本画面を閉じますか？", @"閉じる", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.Close();
        }
        #endregion

        #region << ショートカット制御 >>
        private void ProductStock_Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.F11:
                //    if (btnFnc11.Enabled == true)
                //        btnFnc11.PerformClick();
                //    break;
                case Keys.F12:
                    if (btnFnc12.Enabled == true)
                        btnFnc12.PerformClick();
                    break;
            }
        }
        #endregion
    }
}
