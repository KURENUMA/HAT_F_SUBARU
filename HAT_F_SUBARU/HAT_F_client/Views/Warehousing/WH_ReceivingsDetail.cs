using C1.Win.C1FlexGrid;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClosedXML.Excel.XLPredefinedFormat;
using DateTime = System.DateTime;

namespace HatFClient.Views.Warehousing
{
    public partial class WH_ReceivingsDetail : Form
    {
        // 画面を表示に使用したデータの検索キー
        private string _denNo;

        // グリッド上で変更したレコード把握用
        private List<ViewWarehousingReceivingDetail> _recordsUpdated = new List<ViewWarehousingReceivingDetail>(); 


        public WH_ReceivingsDetail()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                InitializeC1FlexGrid();
            }
        }

        private async Task<bool> LoadDataAsync(string denNo)
        {
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                // 全件取得条件付与
                string url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.WarehousingReceivingsDetail);

                var conditions = new { denNo = denNo };
                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingReceivingDetail>>(
                    url,            // 詳細取得API
                    conditions);    // 検索条件

                return apiResponse;
            });

            if (!apiResult.Successed)
            {
                return false;
            }


            UpdateList(apiResult.Value);
            return true;
        }


        public async Task<bool> LoadDataAndShowAsync(string denNo)
        {
            if (!await LoadDataAsync(denNo))
            {
                this.Close();
                return false;
            }
            else
            {
                _denNo = denNo;

                this.Show();
                this.Activate();
                return true;
            }
        }

        private void InitializeC1FlexGrid()
        {
            var grid = grdDataView;
            grid.AutoGenerateColumns = false;

            foreach(CellStyle s in grid.Styles)
            {
                System.Diagnostics.Debug.WriteLine(s.Name);
            }

            // 編集不可項目背景色
            Color readOnlyItemColor = SystemColors.Control;

            // デフォルト編集不可
            var defaultReadOnlyStyle = grid.Styles.Add("defaultReadOnlyStyle");
            defaultReadOnlyStyle.BackColor = readOnlyItemColor;

            foreach (C1.Win.C1FlexGrid.Column col in grid.Cols)
            {
                if (col.Style == null)
                {
                    col.Style = defaultReadOnlyStyle;
                }
                else
                {
                    col.Style.BackColor = readOnlyItemColor;
                }
                col.AllowEditing = false;
            }

            var quantityReadOnlyStyle = grid.Styles.Add("quantityReadOnlyStyle");
            quantityReadOnlyStyle.DataType = typeof(int);
            quantityReadOnlyStyle.TextAlign = TextAlignEnum.RightCenter;
            quantityReadOnlyStyle.BackColor = readOnlyItemColor;
            grid.Cols["入庫予定数量"].Style = quantityReadOnlyStyle;
            grid.Cols["入庫予定バラ数量"].Style = quantityReadOnlyStyle;

            var quantityInputStyle = grid.Styles.Add("quantityInputStyle");
            quantityInputStyle.DataType = typeof(int);
            quantityInputStyle.TextAlign = TextAlignEnum.RightCenter;
            quantityInputStyle.Format = "#,##0";
            quantityInputStyle.EditMask = "#,##0";
            grid.Cols["入庫数量"].Style = quantityInputStyle;
            grid.Cols["入庫バラ数量"].Style = quantityInputStyle;
            grid.Cols["入庫数量"].MaxLength = quantityInputStyle.EditMask.Length;
            grid.Cols["入庫数量"].AllowEditing = true;
            grid.Cols["入庫バラ数量"].MaxLength = quantityInputStyle.EditMask.Length;
            grid.Cols["入庫バラ数量"].AllowEditing = true;


            var datetimeInputStyle = grid.Styles.Add("datetimeInputStyle");
            datetimeInputStyle.DataType = typeof(DateTime);
            datetimeInputStyle.Format = "yy/MM/dd HH:mm";
            grid.Cols["入出庫日時"].Style = datetimeInputStyle;
            grid.Cols["入出庫日時"].AllowEditing = true;

        }

        private void UpdateList(IEnumerable<ViewWarehousingReceivingDetail> data)
        {
            grdDataView.DataSource = data;
            _recordsUpdated.Clear();

            // グリッド編集済行の色分け
            SetGridRowModifiedStyle();

            return;
        }

        private void SetGridRowModifiedStyle()
        {
            // グリッド編集済行の色分け
            GridStyleHelper.SetRowModifiedStyle<ViewWarehousingReceivingDetail>(grdDataView, (row) => {
                return _recordsUpdated.Contains(row);
            });


            //// グリッド編集済行の色分け
            //GridStyleHelper.SetRowModifiedStyle(grdDataView, (row) => {
            //    // DataSourceがDataTableの場合の例(ColName列で編集済みを管理している)
            //    DataRow dataRow = (DataRow)row;
            //    bool val = (bool)dataRow["ColName"];
            //    return (val == true);
            //});
        }

        /// <summary>
        /// ユーザーがグリッド上に入力した場合
        /// </summary>
        private void grdDataView_AfterEdit(object sender, RowColEventArgs e)
        {
            var dt = grdDataView[e.Row, "入出庫日時"];
            System.Diagnostics.Debug.WriteLine($"入出庫日時:{dt}");
            if (dt == null)
            {
                // 入出庫日時が空なら現在日時を補完
                grdDataView[e.Row, "入出庫日時"] = DateTime.Now;
            }

            // 更新があった行を保持しておく
            var row = (ViewWarehousingReceivingDetail)grdDataView.Rows[e.Row].DataSource;
            if (!_recordsUpdated.Contains(row))
            {
                _recordsUpdated.Add(row);
            }

            // グリッド編集済行の色分け
            SetGridRowModifiedStyle();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // 更新データ
            var data = _recordsUpdated;
   
            var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            {
                string url = ApiResources.HatF.Client.WarehousingReceivingsDetail;

                var apiResponse = await Program.HatFApiClient.PutAsync<int>(
                    url,            // 詳細取得API
                    data);    // 検索条件

                return apiResponse;
            });


            if (apiResult.Successed)
            {
                // 編集済み行の管理をクリア
                _recordsUpdated.Clear();

                // グリッド編集済行の色分け
                SetGridRowModifiedStyle();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
