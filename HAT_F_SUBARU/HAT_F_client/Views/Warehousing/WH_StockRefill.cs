using AutoMapper;
using C1.Win.C1FlexGrid;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Helpers;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Estimate;
using HatFClient.Views.Order;
using HatFClient.Views.Search;
using Irony.Parsing;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Warehousing
{
    public partial class WH_StockRefill : Form
    {
        private class ViewStockRefillViewModel : ViewStockRefill
        { 
            public bool 選択 { get; set; }
        }

        //TODO:APIに定義されたら消す

        //public partial class ViewStockRefill
        //{
        //    public string 倉庫コード { get; set; }

        //    public string 倉庫名 { get; set; }

        //    public string 商品コード { get; set; }
        //    public string 商品分類コード { get; set; }
        //    public string ランク { get; set; }
        //    public string 商品分類名 { get; set; }
        //    public string 仕入先コード { get; set; }
        //    public string 仕入先名 { get; set; }


        //    public short 実在庫数 { get; set; }

        //    public short 有効在庫数 { get; set; }

        //    public short? 在庫閾値 { get; set; }

        //    public short? 発注数基準 { get; set; }

        //    public short? 発注済数 { get; set; }

        //    public DateTime? 発注日時 { get; set; }


        //}

        private string _lastSearchedWhCode = null;
        //private List<StockRefill> _gridDataSource = null;

        private class WHStockRefillGridManager : GridManagerBase<ViewStockRefillViewModel> { }
        private WHStockRefillGridManager gridManager = new WHStockRefillGridManager();

        //データテーブルレイアウト
        private GridOrderManager gridOrderManager;

        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        // 検索用の変数
        private DefaultGridPage projectGrid1;   // TODO: TemplateGridPage⇒DefaultGridPage
        private static readonly Type TARGET_MODEL = typeof(ViewStockRefillViewModel);
        private const string OUTFILE_NAME = "在庫補充一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public WH_StockRefill()
        {
            InitializeComponent();           

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);

                InitializeFetching();
            }
        }

        /// <summary>
        /// データ取得処理の初期化
        /// </summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) => {

                string whCodeValue = (string)cboWarehouse.SelectedValue;
                string prodCodeValue = txtProdCode.Text;

                //public static readonly string StockRefill = "api/stock/stock-refill/{whCodeValue}/{prodCodeValue}";
                string url = ApiResources.HatF.Stock.ViewStockRefill;
                //url = ApiHelper.CreateResourceUrl(url, new { whCode = whCodeValue, prodCode = prodCodeValue });
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

                var param = new { whCode = whCodeValue, prodCode = prodCodeValue };
                var response = await Program.HatFApiClient.GetAsync<List<ViewStockRefillViewModel>>(url, param);
                return response;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string whCodeValue = (string)cboWarehouse.SelectedValue;
                string prodCodeValue = txtProdCode.Text;

                //public static readonly string StockRefill = "api/stock/stock-refill/{whCodeValue}/{prodCodeValue}";
                string url = ApiResources.HatF.Stock.ViewStockRefillCount;
                //url = ApiHelper.CreateResourceUrl(url, new { whCode = whCodeValue, prodCode = prodCodeValue });

                var param = new { whCode = whCodeValue, prodCode = prodCodeValue };
                var count = await Program.HatFApiClient.GetAsync<int>(url, param);
                return count;
            };
        }

        private void WH_Shippings_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<StockRefill>());
            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);

            projectGrid1.Dock = DockStyle.Fill;
            //var grid = projectGrid1.c1FlexGrid1;
            this.panel1.Controls.Add(projectGrid1);

            //表の列定義
            InitializeColumns();

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            var grid = projectGrid1.c1FlexGrid1;
            grid.DoubleClick += Grid_DoubleClick;
            grid.CellChecked += Grid_CellChecked;
        }

        private void Grid_CellChecked(object sender, RowColEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (grid.Cols[e.Col].Name == nameof(ViewStockRefillViewModel.選択)) 
            {
                bool newChecked = (bool)grid[e.Row, nameof(ViewStockRefillViewModel.選択)];
                if (!newChecked)
                {
                    return;
                }

                // 今チェックされた行の仕入先コード
                string newSupCode = DataHelper.ToString(grid[e.Row, nameof(ViewStockRefillViewModel.仕入先コード)], "");

                // 異なる仕入先コードの存在チェック
                for (int i = 1; i < grid.Rows.Count; i++)
                {
                    bool selected = (bool)grid[i, nameof(ViewStockRefillViewModel.選択)];
                    if (selected)
                    {
                        string supCode = DataHelper.ToString(grid[i, nameof(ViewStockRefillViewModel.仕入先コード)], "");
                        if(false == string.Equals(supCode, newSupCode, StringComparison.OrdinalIgnoreCase))
                        {
                            string msg = "異なる仕入先は同時に選択できません。";
                            DialogHelper.InformationMessage(this, msg);
                            grid[e.Row, nameof(ViewStockRefillViewModel.選択)] = false;
                            return;
                        }
                    }
                }
            }
        }

        private void Grid_DoubleClick(object sender, EventArgs e)
        {
            if (btnOrder.Enabled)
            {
                btnOrder.PerformClick();
            }
        }

        private void InitializeComboBoxes()
        {
            var divSokos = ClientRepo.GetInstance().Options.DivSokos;
            var sokoComboBoxItems = new List<OptionData>(divSokos.Count());
            divSokos.ToList().ForEach(item =>
            {
                sokoComboBoxItems.Add(new OptionData() { Code = item.WhCode, Name = $"{item.WhCode}:{item.WhName}" });
            });

            cboWarehouse.DisplayMember = nameof(OptionData.Name);
            cboWarehouse.ValueMember = nameof(OptionData.Code);
            cboWarehouse.DataSource = sokoComboBoxItems;
            cboWarehouse.SelectedIndex = 0;
        }

        //private bool ToBool(object val)
        //{
        //    if (val == null) return false;
        //    if (val == DBNull.Value) return false;

        //    if (bool.TryParse(val.ToString(), out bool result))
        //    {
        //        return result;
        //    }

        //    return !string.IsNullOrWhiteSpace(val.ToString());
        //}

        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GdProjectList_RowColChange");

            //件数出力
            var rows = gridManager.Dt.Rows.Count;
            this.rowsCount.Text = rows + "件表示中";
            if (projectGrid1.GetProjectCount() != -1)
            {
                this.lblProjectAllCount.Text = "検索結果：" + projectGrid1.GetProjectCount().ToString() + "件";
            }
            else
            {
                this.lblProjectAllCount.Text = "検索結果：";
            }

            //this.textFilterStr.Text = projectGrid1.GetFilterOptionStr();
            InitializeColumns();

            var grid = projectGrid1.c1FlexGrid1;

            DataTable dataSource = grid.DataSource as DataTable;           
            dataSource.AcceptChanges(); //これ以降の変更を認識できるようにする

            string whCodeValue = (string)cboWarehouse.SelectedValue;
            _lastSearchedWhCode = whCodeValue;
        }

        private void BtnTabClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await gridManager.Reload();
            //await UpdateDataTableAsync();
        }

        //private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    GdProjectList_RowColChange(this, EventArgs.Empty);
        //}

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            //await UpdateDataTableAsync();
        }

        //private async Task UpdateDataTableAsync()
        //{
        //    //gridManager.OnDataSourceChange += GdProjectList_RowColChange;

        //    // 非同期でデータ取得    
        //    await gridManager.Reload( new List<FilterCriteria>());
        //    //GdProjectList_RowColChange(this, EventArgs.Empty);
        //}

        private void InitializeColumns()
        {
            //var pattern = gridPatternUI.SelectedPattern;
            //if (pattern == null)
            //{
            //    return;
            //}

            //// ヘッダの設定
            //projectGrid1.c1FlexGrid1.Clear();
            //BindingList<ColumnInfo> configs = pattern.Columns;

            var grid = projectGrid1.c1FlexGrid1;
            grid.AllowEditing = true;
            foreach (Column col in grid.Cols)
            {
                col.AllowEditing = false;
            }

            // 先頭に移動
            grid.Cols.Move(grid.Cols["選択"].Index, 1);
            grid.Cols["選択"].AllowEditing = true;

            grid.Cols["選択"].Width = 50;
            grid.Cols["商品コード"].Width = 150;
            grid.Cols["商品分類名"].Width = 150;
            grid.Cols["ランク"].Width = 50;
            grid.Cols["仕入先名"].Width = 150;

            grid.Cols["倉庫コード"].Visible = false;
            grid.Cols["商品分類コード"].Visible = false;
            grid.Cols["仕入先コード"].Visible = false;


            //HideColumns(grid, new[] { "CreateDate", "Creator", "UpdateDate", "Updater" });
        }

        //private ListDictionary ToDataMap<T>(IEnumerable<CodeName<T>> source)
        //{
        //    var dictionary = new ListDictionary();
        //    foreach (var item in source.Where(x => x.Code != null))
        //    {
        //        dictionary.Add(item.Code, item.Name);
        //    }
        //    return dictionary;
        //}

        private ListDictionary ToDataMap(IEnumerable<OptionData> source)
        {
            var dictionary = new ListDictionary();
            foreach (var item in source.Where(x => x.Code != null))
            {
                dictionary.Add(item.Code, item.Name);
            }
            return dictionary;
        }

        private void HideColumns(C1FlexGrid grid, IEnumerable<string> columnNames)
        {
            foreach(string colName in columnNames)
            {
                Column col = grid.Cols[colName];
                if (col != null)
                {
                    col.Visible = false;
                }
            }
        }


        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = string.Format(OUTFILE_NAME, DateTime.Now);   //OUTFILE_NAME定数の定義は "出荷指示一覧_{0:yyyyMMdd_HHmmss}.xlsx" のような書式に修正
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "在庫補充一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }
       

        private /*async*/ void btnOrder_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            DataTable dataTable = grid.DataSource as DataTable;
            var source = DataHelper.DataTableToList<ViewStockRefillViewModel>(dataTable);
 
            // 注文情報作成
            var orders = new List<ViewStockRefill>();

            // 注文商品
            for (int i = 1; i < grid.Rows.Count; i++)
            {
                if (source[i].選択)
                {
                    orders.Add(source[i]);
                }
            }


            //// 得意先データ
            //string compCode = "999239";   // HAT-Fのコード

            //var apiResultCompany = await ApiHelper.FetchAsync(this, async () => {
            //    string url = ApiResources.HatF.Client.CompanysMst;
            //    url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

            //    var param = new { compCode = compCode };
            //    var response = await Program.HatFApiClient.GetAsync<List<CompanysMst>>(url, param);
            //    return response;
            //});

            //if (apiResultCompany.Failed)
            //{
            //    return;
            //}

            //if (1 != apiResultCompany.Value.Count)
            //{
            //    string msg = "得意先情報を取得できませんでした。";
            //    DialogHelper.WarningMessage(this, msg);
            //    return;
            //}
            //order.Company = apiResultCompany.Value.Single();


            //// 出荷先データ
            //string custCode = "000201000";   // TODO: HAT-Fに変更する
            //short custSubNo = 0;               // TODO: HAT-F、該当倉庫に変更する 案:SubNoを倉庫CDと合わせる

            //var apiResultDestination = await ApiHelper.FetchAsync(this, async () => {
            //    string url = ApiResources.HatF.Client.DestinationMst;
            //    url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

            //    var param = new { custCode = custCode, custSubNo = custSubNo };
            //    var response = await Program.HatFApiClient.GetAsync<List<DestinationsMst>>(url, param);
            //    return response;
            //});

            //if (apiResultDestination.Failed)
            //{
            //    return;
            //}

            //if (1 != apiResultDestination.Value.Count)
            //{
            //    string msg = "出荷先情報を取得できませんでした。";
            //    DialogHelper.WarningMessage(this, msg);
            //    return;
            //}
            //order.Destination = apiResultDestination.Value.Single();

            ////DialogHelper.InformationMessage(this, "(仮)受発注画面が開きます");

            //using (var form = new JH_Main(JH_Main.JhMainOpenMode.StockOrder))
            //{
            //    form.StockRefillOrder = order;
            //    form.ShowDialog();
            //}

        }


        private void btnRefillSettings_Click(object sender, EventArgs e)
        {
            using(var form = new WH_StockRefillSettings())
            {
                form.ShowDialog();
            }
        }
    }

    ///// <summary>
    ///// 在庫補充発注情報
    ///// </summary>
    //public class StockRefillOrder
    //{
    //    /// <summary>
    //    /// 在庫補充商品
    //    /// </summary>
    //    public List<ProductOrder> Products { get; set; }

    //    /// <summary>
    //    /// 得意先(＝HAT-F)
    //    /// </summary>
    //    public CompanysMst Company { get; set; }

    //    /// <summary>
    //    /// 出荷先 (＝HAT倉庫)
    //    /// </summary>
    //    public DestinationsMst Destination { get; set; }
    //}

    ///// <summary>
    ///// 在庫補充発注情報：対象商品
    ///// </summary>
    //public class ProductOrder
    //{ 
    //    /// <summary>
    //    /// 発注する商品コード
    //    /// </summary>
    //    public string ProdCode { get; set; }

    //    /// <summary>
    //    /// 発注数（デフォルト）
    //    /// </summary>
    //    public short OrderQuantity { get; set; }
    //}
}
