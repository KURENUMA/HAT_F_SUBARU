using C1.Win.C1FlexGrid;
using DocumentFormat.OpenXml.Drawing;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using HatFClient.Views.Cooperate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.CorrectionDelivery
{
    public partial class CorrectionDeliveryDetail : Form
    {
        private PurchaseRepo _purchaseRepo;

        private Dictionary<short, string> checkStatus = new Dictionary<short, string>() {
            { 0,"未確認"},
            { 1,"編集中"},
            { 2,"確認済"},
            { 3,"違算"},
            { 4,"未決"},
            { 5,"未請求"},
        };
        Dictionary<short, string> completeStatus = new Dictionary<short, string>()
        {
            { 0, "未確定" },
            { 1, "確定済み" }
        };


        public ViewCorrectionDeliveryDetail Condition { get; set; } = new ViewCorrectionDeliveryDetail();

        /// <summary><see cref="ViewCorrectionDeliveryDetail"/>の内容をチェックボックスつきでグリッドに表示させるためのクラス</summary>
        public class CheckableViewCorrectionDeliveryDetail : ViewCorrectionDeliveryDetail
        {
            /// <summary>選択列</summary>
            public bool Checked { get; set; }
        }

        public enum ScreenMode
        {
            /// <summary>新規登録</summary>
            NewEntry,
            /// <summary>編集</summary>
            Edit,
            /// <summary>閲覧</summary>
            ReadOnly
        }

        public CorrectionDeliveryDetail()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
                _purchaseRepo = PurchaseRepo.GetInstance();

            }
        }

        private async Task UpdateListAsync()
        {
            var result = await _purchaseRepo.GetDetail(Condition.得意先コード);
            c1FlexGridCorrectionDeliveryDetail.DataSource = result;
        }

        #region <画面アクション>
        private async void CorrectionDeliveryDetail_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            await UpdateListAsync();
            labelCode.Text = "得意先コード：" + Condition.得意先コード;
            labelName.Text = "得意先名：" + Condition.得意先名;
            Cursor.Current = Cursors.Default;
        }


        private void buttonCONTACT_EMAIL_Click(object sender, EventArgs e)
        {
            // Form.ShowDialog する場合は Dispose が必要です。
            using (ContactEmail view = new ContactEmail())
            {
                view.ShowDialog();
            }
        }

        /// <summary>必須項目の入力チェック</summary>
        private bool CheckInputs()
        {
            return true;
        }
        #endregion


        private void validationNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 数字以外の入力を無視する
            }
        }


        private void c1FlexGridCorrectionDeliveryDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (c1FlexGridCorrectionDeliveryDetail.Cols[c1FlexGridCorrectionDeliveryDetail.Col].Name == "M納日")
                {
                    c1FlexGridCorrectionDeliveryDetail[c1FlexGridCorrectionDeliveryDetail.Row, c1FlexGridCorrectionDeliveryDetail.Col] = null;
                }
            }
        }

        private async void btnSaveComment_Click(object sender, EventArgs e)
        {
            var checkedRows = c1FlexGridCorrectionDeliveryDetail.Rows.OfType<Row>()
            .Where(r => c1FlexGridCorrectionDeliveryDetail.GetCellCheck(r.Index, c1FlexGridCorrectionDeliveryDetail.Cols["Checked"].Index) == CheckEnum.Checked)
            .ToList();

            var parameters = checkedRows
                .Select(r => new InternalDeliveryCheckParameter()
                {
                    SalesNo = r[nameof(CheckableViewCorrectionDeliveryDetail.訂正番号)].ToString(),
                    RowNo = (short)r[nameof(CheckableViewCorrectionDeliveryDetail.訂正行番号)],
                    Comment = txtComment.Text,
                }).ToList();
            var results = await ApiHelper.UpdateAsync(this, async () =>
            {
                return await Program.HatFApiClient.PutAsync<List<InternalDeliveryCheckResult>>(ApiResources.HatF.Client.CorrectionDeliveryCheck, parameters);
            });
            if (results.Failed)
            {
                return;
            }
            // チェック結果を画面に反映
            foreach (var row in checkedRows)
            {
                var salesNo = row[nameof(CheckableViewCorrectionDeliveryDetail.訂正番号)].ToString();
                var rowNo = (short)row[nameof(CheckableViewCorrectionDeliveryDetail.訂正行番号)];
                var result = results.Value.FirstOrDefault(x => x.SalesNo == salesNo && x.RowNo == rowNo);
                row[nameof(CheckableViewCorrectionDeliveryDetail.確認者)] = result?.Checker;
                row[nameof(CheckableViewCorrectionDeliveryDetail.確認者役職)] = result?.CheckerPost;
                row[nameof(CheckableViewCorrectionDeliveryDetail.コメント)] = result?.Comment;
            }
            c1FlexGridCorrectionDeliveryDetail.Invalidate();
        }
        /// <summary>保存ボタンの押下可否を更新</summary>
        private void EnableSaveButton()
        {
            var checkedRowCount = c1FlexGridCorrectionDeliveryDetail.Rows.OfType<Row>()
                .Count(r => c1FlexGridCorrectionDeliveryDetail.GetCellCheck(r.Index, 1) == CheckEnum.Checked);
            btnSaveComment.Enabled = checkedRowCount > 0 && !string.IsNullOrEmpty(txtComment.Text);
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
              EnableSaveButton();
        }
        /// <summary>セルのチェックボックス変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void C1FlexGrid1_CellChecked(object sender, RowColEventArgs e)
        {
            EnableSaveButton();
        }
    }
}
