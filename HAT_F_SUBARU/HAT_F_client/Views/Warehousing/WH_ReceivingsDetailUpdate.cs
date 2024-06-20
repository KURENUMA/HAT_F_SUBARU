using C1.Win.C1FlexGrid;
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
using System;
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
    public partial class WH_ReceivingsDetailUpdate : Form
    {
        private class WHReceivingsGridManager : GridManagerBase<ViewWarehousingReceivingDetail> { }

        private WHReceivingsGridManager gridManager = new WHReceivingsGridManager();

        //データテーブルレイアウト
        private GridOrderManager gridOrderManager;


        public string ConditionDenNo { get; set; }


        public event System.EventHandler Search;

        /// <remarks>
        /// 「OnSearch(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnSearch(System.EventArgs e)
        {            
            Search?.Invoke(this, e);
        }


        // 検索用の変数
        private DefaultGridUpdate projectGrid1;
        private static readonly Type TARGET_MODEL = typeof(ViewWarehousingReceivingDetail);
        private const string OUTFILE_NAME = "入庫確認_yyyyMMdd_HHmmss";

        public WH_ReceivingsDetailUpdate()
        {
            InitializeComponent();
            

            if (!this.DesignMode)
            {
                // INIT PATTERN
                var employeeCode = LoginRepo.GetInstance().CurrentUser.EmployeeCode;
                var patternRepo = new GridPatternRepo(employeeCode, TARGET_MODEL.FullName);
                this.gridPatternUI.Init(patternRepo);

                // TODO:追加
                InitializeFetching();


                FormStyleHelper.SetWorkWindowStyle(this);
            }
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
                // 全件取得条件付与
                string url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.WarehousingReceivingsDetail);

                var conditions = new { denNo = this.ConditionDenNo };
                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingReceivingDetail>>(
                    url,   // 詳細取得API
                    conditions);   // 検索条件
                return apiResponse;
            };
        }

        private void WH_ReceivingsDetailUpdate_Load(object sender, EventArgs e)
        {

            gridOrderManager = new GridOrderManager(TemplateHelpers.TemplateColumnConfigs);
            projectGrid1 = new DefaultGridUpdate(gridManager, gridOrderManager);
            projectGrid1.Dock = DockStyle.Fill;
            projectGrid1.Visible = false;
            projectGrid1.c1FlexGrid1.AllowFiltering = true;

            this.panel1.Controls.Add(projectGrid1);

            InitializeColumns();
            gridManager.OnDataSourceChange += GdProjectList_RowColChange;
            this.gridPatternUI.OnPatternSelected += OnPatternSelected;
            projectGrid1.Visible = true;
            rowsCount.Visible = true;
            btnSearch.Enabled = true;
        }

        public async Task LoadDataAndShowAsync(string denNo)
        {
            this.ConditionDenNo = denNo;
            await gridManager.Reload(new List<FilterCriteria>());
            this.Show();
            this.Activate();
        }

        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
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
            using (var searchFrm = new FrmAdvancedSearch(CriteriaHelper.CreateCriteriaDefinitions<ViewWarehousingReceivingDetail>()))
            {
                searchFrm.StartPosition = FormStartPosition.CenterParent;

                searchFrm.OnSearch += async (sender, e) =>
                {
                    await gridManager.Reload(searchFrm.FilterCriterias);
                };

                searchFrm.OnSearchAndSave += async (sender, e) =>
                {
                    gridManager.SetFilters(searchFrm.FilterCriterias);
                    await gridManager.Reload(searchFrm.FilterCriterias);
                };

                searchFrm.OnReset += (sender, e) =>
                {
                    gridManager.SetFilters(new List<FilterCriteria>());
                };

                if (DialogHelper.IsPositiveResult(searchFrm.ShowDialog()))
                {
                    GdProjectList_RowColChange(this, EventArgs.Empty);
                }
            }
        }

        private void OnPatternSelected(object sender, PatternInfo e)
        {
            updateDataTable();
        }

        private async void updateDataTable()
        {

            gridManager.OnDataSourceChange += GdProjectList_RowColChange;


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

            projectGrid1.c1FlexGrid1.Cols.Count = configs.Count + 1;
            projectGrid1.c1FlexGrid1.Cols[0].Caption = "";
            projectGrid1.c1FlexGrid1.Cols[0].Width = 30;

            int columnIndexOffset = 1;
            for (int i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = projectGrid1.c1FlexGrid1.Cols[i + columnIndexOffset];

                col.Caption = config.Label;
                col.Width = config.Width;
                col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                col.Name = config.VarName;
            }
        }

        private void btnExcel出力_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString(OUTFILE_NAME) + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            AppLauncher.OpenExcel(fName);
        }
    }
    //public class TemplateModelList
    //{
    //    public List<ViewWarehousingReceivingDetail> requests { get; set; }
    //}
}
