using AutoMapper;
using C1.Win.C1FlexGrid;
using ClosedXML.Excel;
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
    public partial class WH_StockRefillSettings : Form
    {
        private class StockRefillViewModel : StockRefill
        {
            /// <summary>
            /// 商品コードのチェックエラーでtrue
            /// </summary>
            public bool IsInvalidProdCode { get; set; }

            /// <summary>
            /// 行のチェックエラー内容保持用
            /// </summary>
            public string Description { get; set; }
        }


        private string _lastSearchedWhCode = null;
        //private List<StockRefill> _gridDataSource = null;

        private class WHStockRefillGridManager : GridManagerBase<StockRefillViewModel> { }
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
        private static readonly Type TARGET_MODEL = typeof(WH_ShippingsViewModel);
        private const string OUTFILE_NAME = "在庫補充条件一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public WH_StockRefillSettings()
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
                string url = ApiResources.HatF.Stock.StockRefill;
                //url = ApiHelper.CreateResourceUrl(url, new { whCode = whCodeValue, prodCode = prodCodeValue });
                url = ApiHelper.AddPagingQuery(url, gridManager.CurrentPage, gridManager.PageSize);

                var param = new { whCode = whCodeValue, prodCode = prodCodeValue };
                var response = await Program.HatFApiClient.GetAsync<List<StockRefillViewModel>>(url, param);
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
                string url = ApiResources.HatF.Stock.StockRefillCount;
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

            var grid = projectGrid1.c1FlexGrid1;
            //grid.AllowFiltering = true;
            grid.AllowAddNew = true;
            grid.AllowDelete = true;

            this.panel1.Controls.Add(projectGrid1);

            //表の列定義
            InitializeColumns();

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            var grid = projectGrid1.c1FlexGrid1;
            grid.AfterAddRow += Grid_AfterAddRow;
            grid.BeforeEdit += Grid_BeforeEdit;
            grid.BeforeDeleteRow += Grid_BeforeDeleteRow;
            grid.AfterDeleteRow += Grid_AfterDeleteRow;
            grid.ValidateEdit += Grid_ValidateEdit;
            grid.AfterEdit += Grid_AfterEdit;
        }

        private void Grid_AfterDeleteRow(object sender, RowColEventArgs e)
        {
            // 行番号再設定
            RenumberGridRows();
        }

        private void Grid_AfterEdit(object sender, RowColEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (false == IsNewDataRow(e.Row))
            {
                grid[e.Row, nameof(StockRefillViewModel.Description)] = "変更";
            }
        }

        private void Grid_BeforeDeleteRow(object sender, RowColEventArgs e)
        {
            if (false == IsNewDataRow(e.Row))
            {
                // 既存データ行は削除させない
                e.Cancel = true;
            }
        }

        private bool IsNewDataRow(int row)
        {
            var grid = projectGrid1.c1FlexGrid1;
            object val = grid[row, nameof(StockRefillViewModel.CreateDate)];

            if (IsNully(val))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Grid_BeforeEdit(object sender, RowColEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (grid.Cols[e.Col].Name == nameof(StockRefillViewModel.ProdCode))
            {
                if (false == IsNewDataRow(e.Row))
                {
                    // 既存データの商品コードは編集させない
                    e.Cancel = true;
                }
            }
        }

        private bool IsNully(object obj)
        {
            if (obj == null) { return true; };
            if (obj is DBNull) { return true; };

            return false;
        }

        private async void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (grid.Cols[e.Col].Name == nameof(StockRefillViewModel.StockThreshold)
                || grid.Cols[e.Col].Name == nameof(StockRefillViewModel.OrderQuantity))
            {
                string prodCode = grid.Editor.Text;
                if (string.IsNullOrEmpty(prodCode))
                {
                    e.Cancel = true;
                    grid[e.Row, e.Col] = 0;
                }
            }

            if (grid.Cols[e.Col].Name == nameof(StockRefillViewModel.ProdCode))
            {
                grid[e.Row, nameof(StockRefillViewModel.IsInvalidProdCode)] = true;

                string prodCode = (grid.Editor?.Text ?? "").Trim();
                prodCode = prodCode.ToUpper();
                prodCode = Strings.StrConv(prodCode, VbStrConv.Narrow);
                prodCode = prodCode.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

                if (string.IsNullOrWhiteSpace(prodCode))
                {
                    return;
                }

                string url = ApiResources.HatF.Stock.ComSyohinMst;
                url = ApiHelper.CreateResourceUrl(url, new { prodCode = prodCode });

                var apiResult = await ApiHelper.FetchAsync(this, async () => {
                    var result = await Program.HatFApiClient.GetAsync<ComSyohinMst>(url);
                    return result;
                });

                if (apiResult.Failed)
                {
                    e.Cancel = true;
                    return;
                }

                if (null == apiResult.Value)
                {
                    grid[e.Row, nameof(StockRefillViewModel.Description)] = "存在しない商品コード";
                    string msg = $"商品コード「{prodCode}」は見つかりませんでした。";
                    DialogHelper.WarningMessage(this, msg);
                    e.Cancel = true;
                    return;
                }

                // 重複チェック
                for (int i = 1; i < grid.Rows.Count; i++)
                {
                    if (i == e.Row) { continue; }

                    string rowProdCode = (string)grid[i, nameof(StockRefill.ProdCode)];
                    if (string.Equals(prodCode, rowProdCode, StringComparison.OrdinalIgnoreCase))
                    {
                        grid[e.Row, nameof(StockRefillViewModel.Description)] = "商品コード重複";
                        string msg = $"商品コード「{prodCode}」は{i}行に存在します。";
                        DialogHelper.WarningMessage(this, msg);
                        e.Cancel = true;
                        return;
                    }
                }

                grid[e.Row, e.Col] = prodCode;
                grid[e.Row, nameof(StockRefillViewModel.IsInvalidProdCode)] = false;
                grid[e.Row, nameof(StockRefillViewModel.Description)] = "新規";
            }
        }

        private void RenumberGridRows()
        {
            var grid = projectGrid1.c1FlexGrid1;
            for (int i = 1; i < grid.Rows.Count; i++)
            {
                grid[i, 0] = i;
            }
        }

        private void Grid_AfterAddRow(object sender, RowColEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (string.IsNullOrEmpty(_lastSearchedWhCode))
            {
                string msg = "倉庫を検索してください。";
                DialogHelper.InformationMessage(this, msg);
                e.Cancel = true;
                grid.Rows.Remove(e.Row);
                return;
            }

            // 行番号再設定
            RenumberGridRows();

            string whCode = _lastSearchedWhCode;
            grid[e.Row, nameof(StockRefillViewModel.WhCode)] = whCode;
            grid[e.Row, nameof(StockRefillViewModel.Description)] = "新規";
            grid[e.Row, nameof(StockRefillViewModel.StockThreshold)] = 0;
            grid[e.Row, nameof(StockRefillViewModel.OrderQuantity)] = 0;
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

        private void C1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            //var grid = projectGrid1.c1FlexGrid1;
            //HitTestInfo hitTestInfo = grid.HitTest(e.X, e.Y);
        }

        private bool ToBool(object val)
        {
            if (val == null) return false;
            if (val == DBNull.Value) return false;

            if (bool.TryParse(val.ToString(), out bool result))
            {
                return result;
            }

            return !string.IsNullOrWhiteSpace(val.ToString());
        }

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
            //await gridManager.Reload();
            await UpdateDataTableAsync();
        }

        //private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    GdProjectList_RowColChange(this, EventArgs.Empty);
        //}

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            //await UpdateDataTableAsync();
        }

        private async Task UpdateDataTableAsync()
        {
            //gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            // 非同期でデータ取得    
            await gridManager.Reload( new List<FilterCriteria>());
            //GdProjectList_RowColChange(this, EventArgs.Empty);
        }

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

            //gridOrderManager.InitializeGridColumns(grid, configs, true);
            //grid.AllowMerging = AllowMergingEnum.Free;

            grid.Cols["IsInvalidProdCode"].Visible = false;   //商品コードのチェック結果保存用

            grid.Cols["Description"].Caption = "(状態)";    // ユーザー向けに入力エラーの可視化用の列
            grid.Cols["Description"].Width = 150;

            grid.Cols["WhCode"].Caption = "倉庫";
            grid.Cols["WhCode"].Width = 100;
            grid.Cols["WhCode"].AllowEditing = false;
            grid.Cols["WhCode"].DataMap = ToDataMap(ClientRepo.GetInstance().Options.DivSokos.Select(x => new OptionData() { Code = x.WhName, Name = x.WhName }));

            grid.Cols["ProdCode"].Caption = "商品コード";
            grid.Cols["ProdCode"].Width = 250;
            //grid.Cols["ProdCode"].AllowEditing = true;

            grid.Cols["StockThreshold"].Caption = "発注閾値";
            grid.Cols["StockThreshold"].Width = 75;
            //grid.Cols["StockThreshold"].AllowEditing = true;

            grid.Cols["OrderQuantity"].Caption = "発注数";
            grid.Cols["OrderQuantity"].Width = 75;
            //grid.Cols["OrderQuantity"].AllowEditing = true;


            HideColumns(grid, new[] { "CreateDate", "Creator", "UpdateDate", "Updater" });
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "在庫補充条件一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }


        //private object DbNullToClrNull(object val)
        //{
        //    if (val == DBNull.Value) { return null; }
        //    return val;
        //}




        //private void WriteShippingExcelReport(string templateFileName, string outputFileName, List<List<ViewWarehousingShippingDetail>> data)
        //{

        //    using (Stream stream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read))
        //    using (XLWorkbook wb = new(stream))
        //    {

        //        IXLWorksheet wsTemplate = wb.Worksheet("出荷指示書テンプレート");

        //        IXLWorksheet wsDestination = wsTemplate.CopyTo("出荷指示書");

        //        int pageNo = 1;
        //        foreach (var denpyo in data)
        //        {
        //            int excelRowNo = wsDestination.LastRowUsed().RowNumber() + 1;

        //            int denpyoLineNo = 1;
        //            foreach (var jyuchuDetail in denpyo)
        //            {
        //                int col = 1;
        //                wsDestination.Cell(excelRowNo, col++).Value = denpyoLineNo;
        //                wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.伝票番号;
        //                wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.Hat商品コード;
        //                wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.商品名;
        //                wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.数量;
        //                //wsDestination.Cell(lineNo, col++).Value = jyuchuDetail.バラ;
        //                excelRowNo++;
        //            }

        //            //改ページ TOOD:効いてない？
        //            wsDestination.PageSetup.AddHorizontalPageBreak(excelRowNo);
        //            pageNo++;
        //        }

        //        wsTemplate.Visibility = XLWorksheetVisibility.VeryHidden;
        //        wb.SaveAs(outputFileName);
        //    }
        //}

        //private void chkContainsPrinted_CheckedChanged(object sender, EventArgs e)
        //{
        //    pnlShipping.Visible = chkContainsPrinted.Checked;
        //}

        //private async void btnSearchPrinted_Click(object sender, EventArgs e)
        //{
        //    await UpdateDataTableAsync();
        //}

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (false == ValidateInputs())
            {
                return;
            }

            var data = GetSaveData();
            if (data == null || data.Count == 0) 
            {
                string message = "データが変更されていません。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            var apiResult = await ApiHelper.UpdateAsync(this, async () => {
                string url = ApiResources.HatF.Stock.PutStockRefill;
                var result = await Program.HatFApiClient.PutAsync<int>(url, data);
                return result;
            });

            if (apiResult.Failed)
            {
                return;
            }

            // 成功：再読み込み
            await UpdateDataTableAsync();
        }

        private List<StockRefill> GetSaveData()
        {
            var grid = projectGrid1.c1FlexGrid1;
            var dataSource = grid.DataSource as DataTable;

            DataTable dt = dataSource.GetChanges();
            List<StockRefill> data = (dt != null) ? DataHelper.DataTableToList<StockRefill>(dt) : new List<StockRefill>();

            return data;
        }

        private bool ValidateInputs()
        {
            var grid = projectGrid1.c1FlexGrid1;

            for (int i = 1; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].DataSource == null)
                {
                    continue;
                }

                bool val = (bool?)grid[i, nameof(StockRefillViewModel.IsInvalidProdCode)] ?? false;
                if (val)
                {
                    string msg = $"商品コードのエラーを確認してください: 行{i}";
                    DialogHelper.WarningMessage(this, msg);
                    return false;
                }

                short threshold = (short)grid[i, nameof(StockRefillViewModel.StockThreshold)];
                if (threshold <= 0)
                {
                    string msg = $"発注閾値は1以上を入力してください: 行{i}";
                    DialogHelper.WarningMessage(this, msg);
                    return false;
                }

                short quantity = (short)grid[i, nameof(StockRefillViewModel.OrderQuantity)];
                if (quantity <= 0)
                {
                    string msg = $"発注数は1以上を入力してください: 行{i}";
                    DialogHelper.WarningMessage(this, msg);
                    return false;
                }
            }

            return true;
        }
    }
}
