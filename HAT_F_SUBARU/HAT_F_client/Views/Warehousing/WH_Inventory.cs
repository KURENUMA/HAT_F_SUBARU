using C1.Win.C1FlexGrid;
using ClosedXML.Excel;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.Grids;
using HatFClient.CustomModels;
using HatFClient.Models;
using HatFClient.Repository;
using HatFClient.Shared;
using HatFClient.ViewModels;
using HatFClient.Views.Search;
using HatFClient.Views.Warehousing;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterEdit
{
    public partial class WH_Inventory : Form
    {
        //private List<ViewStockInventory> _dataSource;
        //private List<ViewStockInventory> _updatedData = new List<ViewStockInventory>();

        private class MeCompanyMstGridManager : GridManagerBase<ViewStockInventory> { }
        private MeCompanyMstGridManager gridManager = new MeCompanyMstGridManager();
        private GridOrderManager gridOrderManager;

        //private bool isDirty = false;

        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {
            Search?.Invoke(this, e);
        }

        private DefaultGridPage projectGrid1;   // TODO: TemplateGridPage⇒DefaultGridPage
        private static readonly Type TARGET_MODEL = typeof(ViewStockInventory);
        private const string OUTFILE_NAME = "得意先一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public WH_Inventory()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);

                //InitializeComboBoxes();
                InitializeFetching();

                // INIT PATTERN
                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                //this.gridPatternUI.Init(patternRepo);
            }
        }

        //private string GetRdoStockTypeValue()
        //{
        //    foreach (var item in pnlStockType.Controls)
        //    {
        //        RadioButton radioButton = item as RadioButton;
        //        if (radioButton != null && radioButton.Checked)
        //        {
        //            return (string)radioButton.Tag;
        //        }
        //    }
        //    throw new InvalidOperationException();
        //}

        private string GetRdoFilterValue()
        {
            foreach (var item in pnlFilter.Controls)
            {
                RadioButton radioButton = item as RadioButton;
                if (radioButton != null && radioButton.Checked)
                {
                    return (string)radioButton.Tag;
                }
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// データ取得処理の初期化
        /// </summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) =>
            {
                var param = GetSearchCondition(withFilterCondition: true);
                string url = ApiResources.HatF.Stock.ViewStockInventories;

                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewStockInventory>>(
                    url,   // 一覧取得API
                    param);   // 検索条件

                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                var param = GetSearchCondition(withFilterCondition: true);
                string url = ApiResources.HatF.Stock.ViewStockInventoriesCount;

                var count = await Program.HatFApiClient.GetAsync<int>(
                    url,   // 件数取得API
                    param);   // 検索条件は一覧取得と同じ
                return count;

            };
        }

        private Dictionary<string, object> GetSearchCondition(bool withFilterCondition)
        {
            // 棚卸対象
            DateTime inventoryYearMonth = DateTime.Parse(cboYearMonth.Text);
            string whCode = (string)cboWarehouse.SelectedValue;

            // 絞り込み条件
            string filterCode = GetRdoFilterValue();    // 「全件」「差異あり」等のラジオボタンに対応する値
            string prodCode = txtProdCode.Text.Trim();  // 商品コード

            Dictionary<string, object> param = new() { { "inventoryYearMonth", inventoryYearMonth }, { "whCode", whCode } };
            if (withFilterCondition) 
            {
                param.Add("filterCode", filterCode);
                param.Add("prodCode", prodCode);
            }

            return param;
        }

        private void ME_Supplier_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            InitializeGrid();
            InitializeFormData();

            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;
        }

        private void InitializeFormData()
        {
            //txtYearMonth.Text = string.Format("{0:yyyy/MM}", DateTime.Today);

            DateTime yearMonth = DateTime.Now.AddMonths(2);
            yearMonth = new DateTime(yearMonth.Year, yearMonth.Month, 1);

            var terms = new List<CodeName<DateTime>>();
            while (terms.Count < 3)
            {
                if (yearMonth.Month == 3 || yearMonth.Month == 9)
                {
                    terms.Add(new Models.CodeName<DateTime>() { Code = yearMonth, Name = $"{yearMonth:yyyy/MM}" });
                }
                yearMonth = yearMonth.AddMonths(-1);
            }

            cboYearMonth.DisplayMember = "Name";
            cboYearMonth.ValueMember = "Code";
            cboYearMonth.DataSource = terms;
        }

        private void InitializeGrid()
        {
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<ViewStockInventory>());

            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();

            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            //this.gridPatternUI.OnPatternSelected += OnPatternSelected;

            projectGrid1.c1FlexGrid1.AfterEdit += C1FlexGrid1_AfterEdit;
        }

        private void C1FlexGrid1_AfterEdit(object sender, RowColEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            if (e.Col == grid.Cols[nameof(ViewStockInventory.棚卸在庫数)].Index)
            {
                short? mananged = (short?)grid[grid.Row, nameof(ViewStockInventory.管理在庫数)];
                short? inventory = (short?)grid[grid.Row, nameof(ViewStockInventory.棚卸在庫数)];
                int? diff = inventory - mananged;
                grid[grid.Row, nameof(ViewStockInventory.差異数)] = diff;

                //// データ変更済み
                //isDirty = true;
            }
        }

        private bool ToBool(object value)
        {
            if (value == null) { return false; }
            if (value == DBNull.Value) { return false; }

            if (bool.TryParse(value.ToString(), out bool result))
            {
                return result;
            }

            return false;
        }

        private void InitializeComboBoxes()
        {
            var divSokos = ClientRepo.GetInstance().Options.DivSokos;
            var sokoComboBoxItems = new List<OptionData>(divSokos.Count());
            divSokos.ToList().ForEach(item => {
                sokoComboBoxItems.Add(new OptionData() { Code = item.Code, Name = $"{item.Code}:{item.Name}" });
            });

            cboWarehouse.DisplayMember = nameof(OptionData.Name);
            cboWarehouse.ValueMember = nameof(OptionData.Code);
            cboWarehouse.DataSource = sokoComboBoxItems;

            //var stockTypes = new List<CodeName<string>>();
            //stockTypes.Add(new CodeName<string> { Code = "1", Name = "自社在庫" });
            //stockTypes.Add(new CodeName<string> { Code = "2", Name = "預託品" });  //預かり在庫
            //cboStockType.DisplayMember = nameof(CodeName<string>.Name);
            //cboStockType.ValueMember = nameof(CodeName<string>.Code);
            //cboStockType.Items.Add(stockTypes);

            rdoFilterAll.Tag = "0";
            rdoFilterDiff.Tag = "1";
            rdoFilterNoDiff.Tag = "2";
            rdoFilterAbnormalClass.Tag = "3";
            rdoFilterAbnormalProduct.Tag = "4";
        }


        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (btnDetail.Enabled != true)
            //{
            //    return;
            //}

            //btnDetail.PerformClick();
        }

        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GdProjectList_RowColChange");

            // データ未修整
            //isDirty = false;
            gridManager.Dt.AcceptChanges();

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

            InitializeColumns();

            bool enabled = (cboYearMonth.SelectedIndex == 0);
            btnSave.Enabled = enabled && (rows > 0);
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            // 棚卸条件のみ指定、絞り込み条件なし
            var param = GetSearchCondition(withFilterCondition: false);
            string url = ApiResources.HatF.Stock.ViewStockInventoriesCount;

            var apiResult = await ApiHelper.FetchAsync(this, async () => {
                var count = await Program.HatFApiClient.GetAsync<int>(url, param);
                return count;
            });

            if (apiResult.Failed)
            {
                return;
            }

            if (apiResult.Value == 0)
            {
                string message = $"該当倉庫の棚卸用データは未作成です。{btnCreateInventoryStockInfo.Text}ボタンをクリックして作成してください。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            //await gridManager.Reload();
            await LoadDataUserActionAsync();
        }

        private async Task LoadDataUserActionAsync()
        {
            //if (isDirty)
            var dataTable = gridManager.Dt;
            if (dataTable.GetChanges() != null && dataTable.Rows.Count > 0)
            {
                string message = "入力データが保存されていません。保存されていない入力データを破棄してよろしいですか?";
                if (false == DialogHelper.OkCancelWarning(this, message, true))
                {
                    return;
                }
            }

            await gridManager.Reload();

            //var grid = projectGrid1.c1FlexGrid1;
            //if (grid.Rows.Count == 1)
            //{
            //    string message = $"該当倉庫の棚卸用データは未作成です。{btnCreateInventoryStockInfo.Text}ボタンをクリックして作成してください。";
            //    DialogHelper.InformationMessage(message);
            //}
        }

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            updateDataTable();
        }

        private async void updateDataTable()
        {
            // 非同期でデータ取得    
            //await gridManager.Reload(new List<FilterCriteria>());
            //GdProjectList_RowColChange(this, EventArgs.Empty);
            await LoadDataUserActionAsync();
        }


        private void InitializeColumns()
        {
            //var pattern = gridPatternUI.SelectedPattern;
            //if (pattern == null)
            //{
            //    return;
            //}


            var grid = projectGrid1.c1FlexGrid1;

            // ヘッダの設定
            //projectGrid1.c1FlexGrid1.Clear();
            //BindingList<ColumnInfo> configs = pattern.Columns;
            //gridOrderManager.InitializeGridColumns(grid, configs, true);

            // grid.Cols[nameof(ViewStockInventory.差異数)].Expression = $"[{nameof(ViewStockInventory.管理在庫数)}] - [{nameof(ViewStockInventory.棚卸在庫数)}]";

            grid.AllowEditing = true;
            foreach(Column col in grid.Cols) 
            {
                if (col.Index == 0) { continue; }
                col.AllowEditing = false;
                if (col.Style == null) 
                {
                    col.Style = grid.Styles.Add(col.Name);
                }
                col.Style.BackColor = Color.LightGray;
            }
            grid.Cols[nameof(ViewStockInventory.棚卸在庫数)].AllowEditing = true;
            grid.Cols[nameof(ViewStockInventory.棚卸在庫数)].Style = null;
            grid.Cols[nameof(ViewStockInventory.棚卸在庫数)].MaxLength = 5;

            MoveGridColumn(grid, nameof(ViewStockInventory.棚卸年月), 1);
            MoveGridColumn(grid, nameof(ViewStockInventory.倉庫cd), 2);
            MoveGridColumn(grid, nameof(ViewStockInventory.倉庫名), 3);
            MoveGridColumn(grid, nameof(ViewStockInventory.在庫区分), 4);
            MoveGridColumn(grid, nameof(ViewStockInventory.棚卸no), 5);

            grid.Cols[nameof(ViewStockInventory.棚卸年月)].Format = "yyyy/MM";

            grid.Cols[nameof(ViewStockInventory.棚卸no)].Width = 75;
            grid.Cols[nameof(ViewStockInventory.ランク)].Width = 75;
            grid.Cols[nameof(ViewStockInventory.商品cd)].Width = 250;
            grid.Cols[nameof(ViewStockInventory.在庫置場名)].Width = 150;

            grid.Cols[nameof(ViewStockInventory.倉庫cd)].Visible = false;
            grid.Cols[nameof(ViewStockInventory.良品区分)].Visible = false;
            grid.Cols[nameof(ViewStockInventory.在庫置場cd)].Visible = false;

            // TODO: コード値定義
            grid.Cols[nameof(ViewStockInventory.在庫区分)].DataMap = new ListDictionary() { { "1", "自社在庫" }, { "2", "マルマ" } };
            grid.Cols[nameof(ViewStockInventory.良品区分)].DataMap = new ListDictionary() { { "G", "良品" }, { "F", "不良品" }, { "U", "未検品" } };

            //var style = grid.Styles.Add(nameof(ViewStockInventory.棚卸在庫数));
            //style.BackColor = Color.Yellow;
            //grid.Cols[nameof(ViewStockInventory.棚卸在庫数)].Style = style;

            // ヘッダーのタイトル設定
            SetColumnCaptions();
        }

        private void MoveGridColumn(C1FlexGrid grid, string colName, int moveToIndex)
        {
            var col = grid.Cols[colName];
            if (col != null)
            {
                grid.Cols.Move(col.Index, moveToIndex);
            }
        }

        private void SetColumnCaptions()
        {
            //var grid = projectGrid1.c1FlexGrid1;

            //int colIndex = 1;
            //foreach (var prop in typeof(CompanysMstViewModel).GetProperties())
            //{
            //    Column col = grid.Cols[prop.Name];
            //    if (col != null)
            //    {
            //        var item = prop.GetCustomAttribute<CriteriaDefinitionAttribute>();
            //        col.Name = prop.Name;
            //        col.Caption = item.Label;
            //        col.Move(colIndex++);
            //    }
            //}

            //for (int i = colIndex; i < grid.Cols.Count; i++)
            //{
            //    grid.Cols[i].Visible = false;
            //}
        }



        private void HideColumns(C1FlexGrid grid, IEnumerable<string> columnNames)
        {
            foreach (string colName in columnNames)
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "得意先一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
        }

        private object DbNullToClrNull(object val)
        {
            if (val == DBNull.Value) { return null; }
            return val;
        }


        private void rdoFilter_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            //var grid = projectGrid1.c1FlexGrid1;

            if (!DialogHelper.SaveItemConfirm(this))
            {
                return;
            }

            var updateSource = GetUpdateSource();
            string url = ApiResources.HatF.Stock.StockInventories;

            var apiResult = await ApiHelper.UpdateAsync(this, async () => {
                var apiResponse = await Program.HatFApiClient.PutAsync<int>(url, updateSource);
                return apiResponse;
            });

            if (apiResult.Failed) 
            { 
                return; 
            }

            await gridManager.Reload();
        }

        private List<StockInventory> GetUpdateSource()
        {
            var grid = projectGrid1.c1FlexGrid1;
            var dataTable = (DataTable)grid.DataSource;

            DateTime inventoryYearMonth = DateTime.Parse(cboYearMonth.Text);

            var updateSoure = new List<StockInventory>();

            var updatedDataTable = dataTable.GetChanges();
            if (updateDataTable != null)
            {
                foreach (DataRow row in updatedDataTable.Rows)
                {
                    var item = new StockInventory();
                    item.WhCode = (string)DbNullToClrNull(row[nameof(ViewStockInventory.倉庫cd)]);
                    item.ProdCode = (string)DbNullToClrNull(row[nameof(ViewStockInventory.商品cd)]);
                    item.StockType = (string)DbNullToClrNull(row[nameof(ViewStockInventory.在庫区分)]);
                    item.QualityType = (string)DbNullToClrNull(row[nameof(ViewStockInventory.良品区分)]);
                    item.StockNo = (int)DbNullToClrNull(row[nameof(ViewStockInventory.棚卸no)]);
                    item.StockRank = (string)DbNullToClrNull(row[nameof(ViewStockInventory.ランク)]);
                    // item.InventoryYearmonth = row.IsNull(nameof(ViewStockInventory.棚卸年月)) ? inventoryYearMonth : (DateTime)row[nameof(ViewStockInventory.棚卸年月)];
                    item.InventoryYearmonth = (DateTime)(DbNullToClrNull(row[nameof(ViewStockInventory.棚卸年月)]) ?? inventoryYearMonth);
                    item.Managed = (short?)DbNullToClrNull(row[nameof(ViewStockInventory.管理在庫数)]);
                    item.Actual = (short?)DbNullToClrNull(row[nameof(ViewStockInventory.棚卸在庫数)]);
                    item.Status = (string)DbNullToClrNull(row[nameof(ViewStockInventory.状況)]);
                    updateSoure.Add(item);
                }
            }

            return updateSoure;
        }

        private void WH_Inventory_Shown(object sender, EventArgs e)
        {
        }

        private async void btnCreateInventoryStockInfo_Click(object sender, EventArgs e)
        {
            string whName = cboWarehouse.Text;
            string whCode = (string)cboWarehouse.SelectedValue;
            DateTime inventoryYearMonth = DateTime.Parse(cboYearMonth.Text);

            // 棚卸情報取得用、絞り込み条件なし
            var checkParam = GetSearchCondition(withFilterCondition: false); 
            string checkUrl = ApiResources.HatF.Stock.ViewStockInventories;

            // すでに実行済みかチェック
            var apiResult = await ApiHelper.FetchAsync(this, async () => {
                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewStockInventory>>(
                    checkUrl,   // 一覧取得API
                    checkParam);   // 検索条件
                return apiResponse;
            });
            if (apiResult.Failed)
            {
                return;
            }

            string target = $"{inventoryYearMonth:yyyy年MM月} {whName}倉庫";

            string message;
            if (apiResult.Value.Any())
            {
                message = $"{target} の棚卸用データはすでに作成されています。作成しなおしてよろしいですか?{Environment.NewLine}{Environment.NewLine}※入力済みの棚卸数は削除されます";
            }
            else 
            {
                message = $"{target} の棚卸用データを作成してよろしいですか?";
            }
            if (false == DialogHelper.OkCancelQuestion(this, message, true))
            {
                return;
            }

            // 在庫テーブルから在庫データ棚卸にデータをコピー
            var url = ApiResources.HatF.Stock.StockInventoriesNew;
            var param = GetSearchCondition(withFilterCondition: false); // 棚卸情報取得用、絞り込み条件なし

            var result = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<int>(url, param, null);
            }, silentWhenSuccess: true);

            if (result.Failed)
            {
                return;
            }

            if (0 < result.Value)
            {
                await gridManager.Reload();
                DialogHelper.InformationMessage(this, $"{target} の棚卸用データを作成しました。");
            }
            else
            {
                DialogHelper.InformationMessage(this, $"{whName}倉庫の在庫データが見つかりませんでした。");
            }
        }

        private async void btnOutputInventoryList_Click(object sender, EventArgs e)
        {
            var param = GetSearchCondition(withFilterCondition: true);

            string url = ApiResources.HatF.Stock.ViewStockInventories;
            url = ApiHelper.AddUnlimitedQuery(url); //全件条件付与

            var apiResult = await ApiHelper.FetchAsync(this, async () => {
                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewStockInventory>>(
                    url,   // 一覧取得API
                    param);   // 検索条件
                return apiResponse;
            });
            if (apiResult.Failed)
            {
                return;
            }

            string whName = cboWarehouse.Text;
            string whCode = (string)cboWarehouse.SelectedValue;
            DateTime inventoryYearMonth = DateTime.Parse(cboYearMonth.Text);
            string target = $"{inventoryYearMonth:yyyy年MM月} {whName}倉庫";

            var data = apiResult.Value;
            if (0 == data.Count)
            {
                string message = $"{target} の棚卸用データが見つかりませんでした。棚卸在庫情報作成ボタンで作成してください。";
                DialogHelper.WarningMessage(this, message);
                return;
            }

            // 自社在庫分
            var hatData = data.Where(x => x.在庫区分 == "1").ToList();
            if (hatData.Count > 0)
            {
                // 棚卸記入用紙作成
                string templateFileName = ExcelReportUtil.AddTemplatePathToFileName("棚卸記入用紙.xlsx");
                string outputFileName = string.Format("棚卸記入用紙_{0}_{1:yyyyMMdd_HHmmss}.xlsx", target, DateTime.Now);
                outputFileName = outputFileName.Replace(":", "-");
                outputFileName = ExcelReportUtil.ToExcelReportFileName(outputFileName);
                WriteExcelReport(templateFileName, outputFileName, hatData, "1");
                AppLauncher.OpenExcel(outputFileName);
            }

            // 預かり在庫（マルマ）
            var othersData = data.Where(x => x.在庫区分 == "2").ToList();
            if (othersData.Count > 0)
            {
                // 預託品記入用紙作成
                string templateFileName = ExcelReportUtil.AddTemplatePathToFileName("棚卸記入用紙.xlsx");
                string outputFileName = string.Format("預託品記入用紙_{0}_{1:yyyyMMdd_HHmmss}.xlsx", target, DateTime.Now);
                outputFileName = outputFileName.Replace(":", "-");
                outputFileName = ExcelReportUtil.ToExcelReportFileName(outputFileName);
                WriteExcelReport(templateFileName, outputFileName, othersData, "2");
                AppLauncher.OpenExcel(outputFileName);
            }
        }

        private void WriteExcelReport(string templateFileName, string outputFileName, List<ViewStockInventory> data, string stockType)
        {
            string whName = cboWarehouse.Text;
            bool isMaruma = stockType == "2"; //預かり在庫（マルマ）

            using (Stream stream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read))
            using (XLWorkbook wb = new(stream))
            {

                IXLWorksheet wsTemplate = wb.Worksheet("棚卸記入用紙テンプレート");

                string sheetName = isMaruma ? "預託品記入用紙" : "棚卸記入用紙";
                IXLWorksheet wsDestination = wsTemplate.CopyTo(sheetName);

                if (isMaruma)
                {
                    var paper = wsDestination.Search("（１）棚卸記入用紙").First();
                    paper.Value = paper.Value.ToString().Replace("（１）棚卸記入用紙", "（３）預託品記入用紙");

                    var paper2 = wsDestination.Search("（２）未登録記入用紙").First();
                    paper2.Value = paper2.Value.ToString().Replace("（２）未登録記入用紙", "（４）預託未登録品記入用紙");
                }


                wsDestination.Search("倉庫コード").First().Value = $"倉庫コード：{whName}";

                // 列位置調査
                var colIndex = new Dictionary<string, int>();
                Action<string> getColumnIndex = (searchText) => {
                    colIndex.Add(searchText, wsDestination.Search(searchText).Last().Address.ColumnNumber);
                };
                getColumnIndex("#");
                getColumnIndex("商品コード");
                getColumnIndex("ﾗﾝｸ");
                getColumnIndex("再");
                getColumnIndex("CPU");
                getColumnIndex("カウント1");
                getColumnIndex("カウント2");
                getColumnIndex("カウント3");
                getColumnIndex("LC");
                getColumnIndex("状態");

                //int pageNo = 1;
                //foreach (var denpyo in data)
                {
                    // 未使用の最初の行
                    int excelRowNo = wsDestination.LastRowUsed().RowNumber() + 1;

                    foreach (var record in data)
                    {
                        MergeLeftCell(wsDestination, excelRowNo, colIndex["#"]);
                        wsDestination.Cell(excelRowNo, colIndex["#"] - 1).Value = record.棚卸no;

                        string prodCode = string.IsNullOrEmpty(record.在庫置場名) ? record.商品cd : $"{record.商品cd} ({record.在庫置場名})";
                        wsDestination.Cell(excelRowNo, colIndex["商品コード"]).Value = prodCode;

                        wsDestination.Cell(excelRowNo, colIndex["ﾗﾝｸ"]).Value = record.ランク;
                        wsDestination.Cell(excelRowNo, colIndex["再"]).Value = "（   ）";    // 再

                        MergeLeftCell(wsDestination, excelRowNo, colIndex["CPU"]);
                        wsDestination.Cell(excelRowNo, colIndex["CPU"] - 1).Value = record.管理在庫数;    // CPU

                        wsDestination.Cell(excelRowNo, colIndex["カウント1"]).Value = "（　　　　）";  // カウント1
                        wsDestination.Cell(excelRowNo, colIndex["カウント2"]).Value = "（　　　　）";  // カウント2
                        wsDestination.Cell(excelRowNo, colIndex["カウント3"]).Value = "（　　　　）";  // カウント3
                        wsDestination.Cell(excelRowNo, colIndex["状態"]).Value = "（   ）";   // 補完状態
                        excelRowNo++;
                    }
                    //pageNo++;
                }

                wsTemplate.Visibility = XLWorksheetVisibility.VeryHidden;
                wb.SaveAs(outputFileName);
            }
        }

        private void MergeLeftCell(IXLWorksheet ws, int row, int col)
        {
            var cell = ws.Cell(row, col);

            // ↓「A1:B1」形式でCPU列と1個左のセルを表現
            string mergeCellAddress = $"{cell.CellLeft().Address.ColumnLetter}{row}:{cell.Address.ColumnLetter}{row}";
            ws.Range(mergeCellAddress).Merge();
        }

        private void cboYearMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = (cboYearMonth.SelectedIndex == 0);
            btnCreateInventoryStockInfo.Enabled = enabled;
            btnOutputInventoryList.Enabled = enabled;
            btnSave.Enabled = enabled;
        }

        private async void btnLoadAmazon_Click(object sender, EventArgs e)
        {
            var param = GetSearchCondition(withFilterCondition: true);
            string url = ApiResources.HatF.Stock.ViewStockInventoriesCount;

            // 件数取得
            var apiResult = await ApiHelper.FetchAsync(this, async () => {
                var count = await Program.HatFApiClient.GetAsync<int>(url, param);   // 検索条件は一覧取得と同じ
                return count;
            });

            if (apiResult.Failed)
            {
                return;
            }

            string whName = cboWarehouse.Text;
            string whCode = (string)cboWarehouse.SelectedValue;
            DateTime inventoryYearMonth = DateTime.Parse(cboYearMonth.Text);
            string target = $"{inventoryYearMonth:yyyy年MM月} {whName}倉庫";

            if (apiResult.Value == 0)
            {
                string message = $"{target} の棚卸用データが見つかりませんでした。棚卸在庫情報作成ボタンで作成してください。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            using (var form = new WH_InventoryAmazonImport(whCode, whName, inventoryYearMonth))
            {
                if (DialogHelper.IsPositiveResult(form.ShowDialog()))
                {
                    await gridManager.Reload();
                }
            }
        }
    }
}
