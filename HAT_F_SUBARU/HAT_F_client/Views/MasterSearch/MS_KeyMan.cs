using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    public partial class MS_KeyMan : Form
    {
        private const int IntShowMaxConunt = 200;   // 表示最大件数
        private const string StrNoDataMessage = @"キーマン情報が存在しません";   // データ無しメッセージ
        private string _txtTEAM_CD;
        private string _txtTOKUI_CD;
        private string _txtKEYMAN_CD;
        private string _strMsKeyManCode = @"";
        private string _strMsKeyManName = @"";
        public string StrMsKeyManCode
        {
            get { return _strMsKeyManCode; }
        }
        public string StrMsKeyManName
        {
            get { return _strMsKeyManName; }
        }
        public string TxtTEAM_CD
        {
            get { return _txtTEAM_CD; }
            set { _txtTEAM_CD = value; }
        }
        public string TxtTOKUI_CD
        {
            get { return _txtTOKUI_CD; }
            set { _txtTOKUI_CD = value; }
        }
        public string TxtKEYMAN_CD
        {
            get { return _txtKEYMAN_CD; }
            set { _txtKEYMAN_CD = value; }
        }
        public MS_KeyMan()
        {
            InitializeComponent();
        }
        private void Fm_Load(object sender, EventArgs e)
        {
            InitForm();
            ShowList();
        }
        private void InitForm()
        {
            HatFComParts.InitMessageArea(lblNote);
            btnFnc11.Enabled = false;
            InitDgvList();
        }
        private void InitDgvList()
        {
            dgvList.Rows.Clear();
            var columnNames = new string[]{@"Code", @"Name"};
            var columnTexts = new string[]{@"キーマンコード" , @"キーマン名" };
            var columnWidths = new int[]{150, 300};

            for (int i = 0; i < columnNames.Length; i++)
            {
                var viewColumn = new DataGridViewColumn();
                viewColumn.Name = columnNames[i];
                viewColumn.HeaderText = columnTexts[i];
                viewColumn.Width = columnWidths[i];
                viewColumn.CellTemplate = new DataGridViewTextBoxCell();
                dgvList.Columns.Add(viewColumn);
                dgvList.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgvList.EnableHeadersVisualStyles = false;
            dgvList.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvList.ColumnHeadersHeight = 60;
            dgvList.RowTemplate.Height = 30;
        }
        private async void ShowList()
        {
            HatFComParts.InitMessageArea(lblNote);
            SearchRepo repo = SearchRepo.GetInstance();
            var teamCode = TxtTEAM_CD;
            var tokuiCode = TxtTOKUI_CD;
            var keyman = await ApiHelper.FetchAsync(this, () =>
            {
                return repo.searchKeyman(teamCode, tokuiCode, "", IntShowMaxConunt);
            });
            if (keyman.Failed)
            {
                return;
            }

            dgvList.Rows.Clear();
            if (keyman.Value.Count > 0)
            {
                for (int i = 0; i < keyman.Value.Count; i++)
                {
                    var values = new string[] { keyman.Value[i].KmanCd, keyman.Value[i].KmanNm1 };
                    dgvList.Rows.Add(values);
                }
                btnFnc11.Enabled = true;
            }
            else
            {
                HatFComParts.ShowMessageAreaError(lblNote, StrNoDataMessage);
                btnFnc11.Enabled = false;
            }
        }

        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            if (dgvList.CurrentRow != null)
            {
                _strMsKeyManCode = dgvList.Rows[dgvList.CurrentRow.Index].Cells[0].Value.ToString();
                _strMsKeyManName = dgvList.Rows[dgvList.CurrentRow.Index].Cells[1].Value.ToString();
            }
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            this.btnFnc12.Focus();
        }
        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    if (btnFnc11.Enabled == true)
                        btnFnc11.PerformClick();
                    break;
                case Keys.F12:
                    if (btnFnc12.Enabled == true)
                        btnFnc12.PerformClick();
                    break;
            }
        }
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnFnc11.Enabled == true)
            {
                btnFnc11.PerformClick();
            }
        }
    }
}
