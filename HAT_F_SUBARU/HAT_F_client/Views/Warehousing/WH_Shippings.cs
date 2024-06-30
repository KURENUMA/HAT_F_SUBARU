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
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Warehousing
{
    public partial class WH_Shippings : Form
    {
        private List<ViewWarehousingShipping> _gridDataSource = null;

        // TODO: 画面固有のvGridManagervを定義
        //private class WHStockRefillGridManager : GridManagerBase<ViewWarehousingShipping> { }
        private class WHShippingsGridManager : SourceConvertingGridManagerBase<WH_ShippingsViewModel, ViewWarehousingShipping> { }

        // TODO: TemplateGridManager から画面用の固有型へ変更 (GridManagerBase<T>の継承で作れます)
        private WHShippingsGridManager gridManager = new WHShippingsGridManager();

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
        //private const string OUTFILE_NAME = "出荷指示一覧_yyyyMMdd_HHmmss";
        private const string OUTFILE_NAME = "出荷指示一覧_{0:yyyyMMdd_HHmmss}.xlsx";

        public WH_Shippings()
        {
            InitializeComponent();           

            if (!this.DesignMode)
            {
                // TODO:追加
                InitializeFetching();

                // INIT PATTERN
                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                this.gridPatternUI.Init(patternRepo);

            }
        }

        /// <summary>
        /// データ取得処理の初期化
        /// </summary>
        private void InitializeFetching()
        {
            gridManager.TargetForm = this;

            //gridManager.SourceConvertFunc = (source) =>
            //{
            //    var config = new MapperConfiguration(cfg => cfg.CreateMap<ViewWarehousingShipping, ViewWarehousingShippingViewModel>());
            //    var mapper = new Mapper(config);
            //    var data = mapper.Map<List<ViewWarehousingShippingViewModel>>(source);
            //    return data;
            //};

            // 一覧取得処理
            gridManager.FetchFuncAsync = async (filter) => {

                //// API(ページング条件付与)
                //string url = ApiHelper.AddPagingQuery(ApiResources.HatF.WarehousingShippingsGenSearch, gridManager.CurrentPage, gridManager.PageSize);
                //var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                //var apiResponse = await Program.HatFApiClient.PostAsync<List<ViewWarehousingShipping>>(
                //    url,   // 一覧取得API
                //    JsonConvert.SerializeObject(conditions));   // 検索条件

                // API(ページング条件付与)
                string url = ApiHelper.AddPagingQuery(ApiResources.HatF.Client.WarehousingShippings, gridManager.CurrentPage, gridManager.PageSize);

                ApiResponse<List<ViewWarehousingShipping>> apiResponse;
                if (!chkContainsPrinted.Checked)
                {

                    apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingShipping>>(
                        url   // 一覧取得API
                        );   // 検索条件

                }
                else
                {
                    var conditions = new { includeOrderPrinted = true, shippedDateFrom= dtpShippedFrom.Value, shippedDateTo=dtpShippedTo.Value };

                    apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingShipping>>(
                        url,   // 一覧取得API
                        conditions);   // 検索条件
                }

                _gridDataSource = apiResponse.Data;

                return apiResponse;
            };

            // 件数取得処理
            // ページング有画面は必要
            // ページングがない画面は null(初期値) をセットしておく
            gridManager.FetchCountFuncAsync = async (filter) =>
            {
                string url = ApiResources.HatF.Client.WarehousingShippingsCountGenSearch;
                var conditions = filter.Select(f => f.AsFilterOption()).ToList();

                var count = await Program.HatFApiClient.PostAsync<int>(
                    url,   // 件数取得API
                    JsonConvert.SerializeObject(conditions));   // 検索条件は一覧取得と同じ
                return count;
            };
        }

        private void WH_Shippings_Load(object sender, EventArgs e)
        {
            // TODO:引数を変更
            gridOrderManager = new GridOrderManager(CriteriaHelper.CreateCriteriaDefinitions<WH_ShippingsViewModel>());

            //TODO: TemplateGridPage⇒DefaultGridPage差し替え（用件に合わなければTemplateGridPageをカスタム）
            projectGrid1 = new DefaultGridPage(gridManager, gridOrderManager);  

            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;
            projectGrid1.c1FlexGrid1.MouseDoubleClick += C1FlexGrid1_MouseDoubleClick;
            projectGrid1.c1FlexGrid1.MouseClick += C1FlexGrid1_MouseClick;
            projectGrid1.c1FlexGrid1.SelectionMode = SelectionModeEnum.ListBox;


            this.panel1.Controls.Add(projectGrid1);

            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;

            //表の列定義
            InitializeColumns();    

            InitializeEvents();

            dtpShippedFrom.Value = DateTime.Today;
            dtpShippedTo.Value = DateTime.Today;
        }

        private void C1FlexGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            HitTestInfo hitTestInfo = grid.HitTest(e.X, e.Y);

            //bool val = !ToBool(grid[hitTestInfo.Row, "選択"]);
            //grid[hitTestInfo.Row, "選択"] = val;
            //CheckEnum checkEnum = val ? CheckEnum.Checked : CheckEnum.Unchecked;
            //grid.SetCellCheck(hitTestInfo.Row, grid.Cols["選択"].Index, checkEnum);
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

        private void C1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnDetail.Enabled == true)
            {
                var selected = GetSelectedData();
                if (0 == selected.Count)
                {
                    var grid = projectGrid1.c1FlexGrid1;
                    HitTestInfo hitTestInfo = grid.HitTest(e.X, e.Y);
                    grid[hitTestInfo.Row, "選択"] = true;
                }

                btnDetail.PerformClick();
            }
        }

        private void InitializeEvents()
        {
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
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

            this.textFilterStr.Text = projectGrid1.GetFilterOptionStr();
            InitializeColumns();
        }

        private void BtnTabClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
        }

        private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GdProjectList_RowColChange(this, EventArgs.Empty);
        }

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            updateDataTable();
        }

        private async void updateDataTable()
        {
            //gridManager.OnDataSourceChange += GdProjectList_RowColChange;

            // 非同期でデータ取得    
            await gridManager.Reload(new List<FilterCriteria>());
            GdProjectList_RowColChange(this, EventArgs.Empty);
        }

        private void InitializeColumns()
        {
            var pattern = gridPatternUI.SelectedPattern;
            if (pattern == null)
            {
                return;
            }


            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;

            var grid = projectGrid1.c1FlexGrid1;
            gridOrderManager.InitializeGridColumns(grid, configs, true);

            //Column selectCol = grid.Cols.Add();
            //selectCol.Name = "選択";
            //selectCol.Caption = "選択";
            //selectCol.DataType = typeof(bool);
            //selectCol.Move(1);

            grid.AllowMerging = AllowMergingEnum.Free;
            grid.Cols["伝票番号"].AllowMerging = true;

            grid.AllowEditing = true;

            foreach (Column col in grid.Cols)
            {
                switch (col.Name)
                {
                    case "選択":
                        col.AllowEditing = true;
                        col.DataType = typeof(bool);
                        break;
                    default:
                        col.AllowEditing = false;
                        break;
                }
            }

            HideColumns(grid, new[] { "SaveKey", "DenSort", "伝区", "倉庫ステータス" });
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
            fName = ExcelReportUtil.ToExcelReportFileName(fName, "出荷指示一覧");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;
            if (grid.Rows.Count < 2) { return; }

            var selected = GetSelectedData();
            if (0 == selected.Count) 
            {
                //DialogHelper.InformationMessage("表示したいデータにチェックを付けてください。");
                //return;
                grid[grid.Row, "選択"] = true;
                selected = GetSelectedData();
            }
            else if (2 <= selected.Count)
            {
                if (!DialogHelper.OkCancelQuestion(this, "複数選択されているため先頭を表示します。"))
                {
                    return;
                }
            }

            var detail = FormFactory.GetModelessForm<WH_ShippingsDetail>();
            await detail.LoadDataAndShowAsync(selected.First());

            //using (var detail = new WH_ShippingsDetail())
            //{
            //    if (await detail.LoadDataAsync(selected.First()))                
            //    {
            //        detail.Hide();
            //        detail.ShowDialog();
            //    }
            //}
        }

        private object DbNullToClrNull(object val)
        {
            if (val == DBNull.Value) { return null; }
            return val;
        }

        private List<ViewWarehousingShipping> GetSelectedData()
        {

            var grid = projectGrid1.c1FlexGrid1;
            var results = new List<ViewWarehousingShipping>();


            //foreach (Row row in grid.Rows)
            for(int i = 1; i < grid.Rows.Count; i++)
            {
                var row = grid.Rows[i];
                if (row == null) continue;
                if (row.DataSource == null) continue;

                var dataRowView = (DataRowView)row.DataSource;

                //if ((bool)dataRowView["選択"])
                if ((bool)grid[i, "選択"])
                {
                    // 選択データを取得
                    var query = _gridDataSource
                        .Where(x => x.SaveKey == (string)dataRowView["SaveKey"])
                        .Where(x => x.DenSort == (string)dataRowView["DenSort"]);

                    var data = query.Single();
                    results.Add(data);
                }
            }

            return results;
        }

        private async void btnShipping_Click(object sender, EventArgs e)
        {
            var grid = projectGrid1.c1FlexGrid1;

            // 選択されているデータ
            List<ViewWarehousingShipping> shippingsForPrinted = GetSelectedData();

            if (0 == shippingsForPrinted.Count)
            {
                DialogHelper.InformationMessage(this, "出荷指示を選択してください。");
                return;
            }

            // 出荷指示書印刷用データ取得
            var details = new List<List<ViewWarehousingShippingDetail>>();

            List<string> denNos = shippingsForPrinted.Select(x => x.伝票番号).ToList();
            foreach (string denNo in denNos)
            {
                var apiResult = await ApiHelper.FetchAsync(this, async () => {

                    // API(ページング条件付与)
                    string url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.WarehousingShippingDetails);
                    var conditions = new { DenNo = denNo };

                    var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingShippingDetail>>(
                        url,   // 一覧取得API
                        conditions);   // 検索条件

                    return apiResponse;
                });

                if (apiResult.Failed)
                {
                    // 失敗したら終了
                    return;
                }

                details.Add(apiResult.Value);
            }

            // 印刷対象を「印刷済」に更新
            var shippingForUpdates = shippingsForPrinted.Where(x => x.倉庫ステータス == "2").ToList();   // 出荷指示書が印刷されていないものに限定
            if (shippingForUpdates.Count > 0)
            {
                foreach (var data in shippingForUpdates)
                {
                    data.倉庫ステータス = "3";     // 出荷指示書印刷済
                    data.出荷日 = DateTime.Today;
                    data.到着予定日 = data.出荷日.Value.AddDays(2); // +2日
                }

                var apiResultUupdate = await ApiHelper.UpdateAsync(this, async () => {

                    // API
                    string url = ApiResources.HatF.Client.WarehousingShippings;

                    var apiResponse = await Program.HatFApiClient.PutAsync<int>(
                        url,   // 一覧取得API
                        shippingForUpdates);   // 更新データ

                    return apiResponse;
                }, true);

                if (apiResultUupdate.Failed)
                {
                    // 失敗したら終了
                    return;
                }
            }

            // 出荷指示書データの所得、データ更新両方成功したら Excel を開く

            // 出荷指示書作成
            string templateFileName = ExcelReportUtil.AddTemplatePathToFileName("出荷指示書.xlsx");
            string outputFileName = string.Format("出荷指示書_{0:yyyyMMdd_HHmmss}.xlsx", DateTime.Now);
            outputFileName = ExcelReportUtil.ToExcelReportFileName(outputFileName);
            WriteShippingExcelReport(templateFileName, outputFileName, details);

            AppLauncher.OpenExcel(outputFileName);
        }


        private void WriteShippingExcelReport(string templateFileName, string outputFileName, List<List<ViewWarehousingShippingDetail>> data)
        {

            using (Stream stream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read))
            using (XLWorkbook wb = new(stream))
            {

                IXLWorksheet wsTemplate = wb.Worksheet("出荷指示書テンプレート");

                IXLWorksheet wsDestination = wsTemplate.CopyTo("出荷指示書");

                int pageNo = 1;
                foreach (var denpyo in data)
                {
                    int excelRowNo = wsDestination.LastRowUsed().RowNumber() + 1;

                    int denpyoLineNo = 1;
                    foreach (var jyuchuDetail in denpyo)
                    {
                        int col = 1;
                        wsDestination.Cell(excelRowNo, col++).Value = denpyoLineNo;
                        wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.伝票番号;
                        wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.Hat商品コード;
                        wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.商品名;
                        wsDestination.Cell(excelRowNo, col++).Value = jyuchuDetail.数量;
                        //wsDestination.Cell(lineNo, col++).Value = jyuchuDetail.バラ;
                        excelRowNo++;
                    }

                    //改ページ TOOD:効いてない？
                    wsDestination.PageSetup.AddHorizontalPageBreak(excelRowNo);
                    pageNo++;
                }

                wsTemplate.Visibility = XLWorksheetVisibility.VeryHidden;
                wb.SaveAs(outputFileName);
            }
        }

        private void chkContainsPrinted_CheckedChanged(object sender, EventArgs e)
        {
            pnlShipping.Visible = chkContainsPrinted.Checked;
        }

        private void btnSearchPrinted_Click(object sender, EventArgs e)
        {
            updateDataTable();
        }
    }

    //public class TemplateModelListPage
    //{
    //    public List<HAT_F_api.Models.ViewReadySale> requests { get; set; }
    //}
}
