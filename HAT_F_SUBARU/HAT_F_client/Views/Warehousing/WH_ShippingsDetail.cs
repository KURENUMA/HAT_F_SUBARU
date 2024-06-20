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
    public partial class WH_ShippingsDetail : Form
    {
        // 親レコード（前画面で選択した行データ）
        private ViewWarehousingShipping _parent;


        public WH_ShippingsDetail()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
                //FormStyleHelper.SetDefaultFont(this);
                InitializeC1FlexGrid();
            }
        }



        private async Task<bool> LoadDataAsync(ViewWarehousingShipping parent)
        {
            var apiResult = await ApiHelper.FetchAsync(this, async () =>
            {
                // 全件取得条件付与
                string url = ApiHelper.AddUnlimitedQuery(ApiResources.HatF.Client.WarehousingShippingDetails);

                var conditions = new { SaveKey = parent.SaveKey, DenSort = parent.DenSort };
                var apiResponse = await Program.HatFApiClient.GetAsync<List<ViewWarehousingShippingDetail>>(
                    url,            // 詳細取得API
                    conditions);    // 検索条件

                return apiResponse;
            });

            if (!apiResult.Successed)
            {
                return false;
            }

            SetFormData(parent, apiResult.Value);
            return true;
        }


        public async Task<bool> LoadDataAndShowAsync(ViewWarehousingShipping parent)
        {
            if (!await LoadDataAsync(parent))
            {
                this.Close();
                return false;
            }
            else
            {
                _parent = parent;

                this.Show();
                this.Activate();
                return true;
            }
        }

        private void InitializeC1FlexGrid()
        {
            var grid = grdDataView;
            grid.AutoGenerateColumns = false;
            grid.AllowEditing = false;
        }

        private void SetFormData(ViewWarehousingShipping parent, IEnumerable<ViewWarehousingShippingDetail> data)
        {
            txtDenNo.Text = parent.伝票番号;
            dtShippedDate.Value = parent.出荷日;
            dtDueDate.Value = parent.到着予定日;

            grdDataView.DataSource = data;
        }

        private bool IsValidForSave()
        {
            if (dtShippedDate.Value == null)
            {
                DialogHelper.InformationMessage(this, "出荷日を入力してください。");
                return false;
            }

            if (dtDueDate.Value == null)
            {
                DialogHelper.InformationMessage(this, "到着予定日を入力してください。");
                return false;
            }

            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidForSave())
            {
                return;
            }

            // 更新データ
            var data = _parent;
            data.出荷日 = (DateTime)dtShippedDate.Value;
            data.到着予定日 = (DateTime)dtDueDate.Value;

            var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            {
                string url = ApiResources.HatF.Client.WarehousingShippings;
                List<ViewWarehousingShipping> param = new() { data };

                var apiResponse = await Program.HatFApiClient.PutAsync<int>(
                    url,            // 詳細取得API
                    param);    // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WH_ShippingsDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!DialogHelper.FormClosingConfirm())
            //{
            //    e.Cancel = true;
            //}
        }
    }

}
