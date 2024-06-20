using HatFClient.Common;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HAT_F_api.CustomModels;
using HatFClient.Constants;
using Newtonsoft.Json;
using System.Web;
using DocumentFormat.OpenXml.Office2010.Excel;
using C1.Win.C1FlexGrid;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Dynamic;

namespace HatFClient.Views.MasterEdit
{
    public partial class ME_Editor : Form
    {
        private bool _formClosing = false;

        private class DictionayEqualityComparer : IEqualityComparer<KeyValuePair<string, object>>
        {
            public bool Equals(KeyValuePair<string, object> x, KeyValuePair<string, object> y)
            {
                bool eq = $"{x.Key}:{x.Value}" == $"{y.Key}:{y.Value}";
                return eq;
            }

            public int GetHashCode(KeyValuePair<string, object> obj)
            {
                int hc = $"{obj.Key}:{obj.Value}".GetHashCode();
                throw new NotImplementedException();
            }
        }

        public MasterTable MasterEditEntity { get; set; }
        public DataTable OriginalData { get; set; }

        private ConditionFilter DeletedFilter { get; set; }

        private const string ERROR_MIN_LENGTH = "「{0}」は{1}文字以上入力してください。";
        private const string ERROR_NUM_INPUT = "数値を入力してください。";
        private const string ERROR_UNIQUE = "「{0}」は同じ値を設定できません。";
        private const string ERROR_PK = "「{0}」は同じ組み合わせを設定できません。";

        public ME_Editor()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this);

                DeletedFilter = new ConditionFilter();
                DeletedFilter.Condition1.Operator = ConditionOperator.NotEquals;
                DeletedFilter.Condition1.Parameter = true;
            }
        }

        private async void ME_Editor_Load(object sender, EventArgs e)
        {
            this.label1.Text = $"編集中：{MasterEditEntity.LogicalName}";

            this.cbShowDeleted.Checked = false;

            setupGridView();

            await setupRowData();

            c1FlexGrid1.BeforeEdit += C1FlexGrid1_BeforeEdit;
            c1FlexGrid1.LeaveEdit += C1FlexGrid1_LeaveEdit;
            c1FlexGrid1.AfterAddRow += C1FlexGrid1_AfterAddRow;
            c1FlexGrid1.KeyPressEdit += C1FlexGrid1_KeyPressEdit;
            c1FlexGrid1.ValidateEdit += C1FlexGrid1_ValidateEdit;
        }


        private MasterTableColumn GetMasterTableColumn(int columnIdx)
        {
            var name = c1FlexGrid1.Cols[columnIdx].Name;
            return this.MasterEditEntity.Columns.Find(x => x.Name == name);
        }

        private void C1FlexGrid1_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            var colInfo = GetMasterTableColumn(e.Col);
            if (colInfo == null) return;

            if (colInfo.NumberOnly)
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }
        private void C1FlexGrid1_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            if (_formClosing)
            {
                e.Cancel = true;
                return;
            }

            var colInfo = GetMasterTableColumn(e.Col);
            if (colInfo == null || colInfo.CheckBox) return;

            var editedText = c1FlexGrid1.Editor.Text;
            if (editedText.Length == 0) return;

            if (colInfo.NumberOnly)
            {
                if (false == decimal.TryParse(editedText, out _))
                {
                    DialogHelper.WarningMessage(this, ERROR_NUM_INPUT);
                    e.Cancel = true;
                    return;
                }
            }

            if (!colInfo.CheckBox && colInfo.MinLength > editedText.Length)
            {
                string message = string.Format(ERROR_MIN_LENGTH, colInfo.LogicalName, colInfo.MinLength);
                DialogHelper.WarningMessage(this, message);

                e.Cancel = true;
                return;
            }

            // ユニーク列は列単体重複チェック
            if (colInfo.Unique)
            {
                var dt = c1FlexGrid1.DataSource as DataTable;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];

                    // 入力行はチェック対象外
                    if (i == (e.Row - 1))
                    {
                        continue;
                    }

                    if (dr[colInfo.Name].ToString() == editedText)
                    {
                        string message = string.Format(ERROR_UNIQUE, colInfo.LogicalName);
                        DialogHelper.WarningMessage(this, message);

                        e.Cancel = true;
                        return;
                    }
                }
            }

            // PK列は組み合わせ重複チェック
            if (colInfo.PrimaryKey)
            {
                // 編集行のPK値を確保
                Dictionary<string, object> editedRowPkValues = GetPkColumnValues(e.Row);
                editedRowPkValues[colInfo.Name] = colInfo.AutoUpperCase ? editedText.ToUpper() : editedText;

                //for (var i = 0; i < dt.Rows.Count; i++)
                var grid = c1FlexGrid1;
                for (var i = 1; i < grid.Rows.Count; i++)
                {
                    // 入力行はチェック対象外
                    if (i == e.Row)
                    {
                        continue;
                    }

                    // 行のPK値
                    Dictionary<string, object> rowPkValues = GetPkColumnValues(i);

                    // PK値が同じか?
                    bool isSameContents = rowPkValues.SequenceEqual(editedRowPkValues, new DictionayEqualityComparer());
                    if (isSameContents)
                    {
                        string pkColName = GetPkColumnName();
                        string message = (pkColName.Length > 1) ? string.Format(ERROR_PK, pkColName) : string.Format(ERROR_UNIQUE, pkColName);
                        DialogHelper.WarningMessage(this, message);

                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private string GetPkColumnName()
        {
            var grid = c1FlexGrid1;
            var sb = new StringBuilder();

            for (int i = 0; i < grid.Cols.Count; i++)
            {
                var colInfo = GetMasterTableColumn(i);
                if (colInfo?.PrimaryKey ?? false)
                {
                    if (sb.Length > 0) {  sb.Append(", "); }
                    sb.Append(colInfo.LogicalName);
                }
            }

            return sb.ToString();
        }

        private Dictionary<string, object> GetPkColumnValues(int gridRowIndex)
        {
            var grid = c1FlexGrid1;
            var list = new Dictionary<string, object>();

            for (int i = 0; i < grid.Cols.Count; i++)
            {
                var colInfo = GetMasterTableColumn(i);
                if (colInfo?.PrimaryKey ?? false)
                {
                    //list.Add(col.ColumnName, row[col.ColumnName]);
                    list.Add(colInfo.Name, grid[gridRowIndex, colInfo.Name]);
                }
            }

            return list;
        }


        /// <summary>
        /// 行挿入時に削除フラグを初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C1FlexGrid1_AfterAddRow(object sender, RowColEventArgs e)
        {
            var col = c1FlexGrid1.Cols["DELETED"];
            if (col != null)
            {
                c1FlexGrid1.SetData(e.Row, col.Index, false);
            }
        }


        private void C1FlexGrid1_LeaveEdit(object sender, RowColEventArgs e)
        {
            var colInfo = GetMasterTableColumn(e.Col);
            if (colInfo == null) return;

            if (colInfo.AutoUpperCase)
            {
                c1FlexGrid1.Editor.Text = c1FlexGrid1.Editor.Text.ToUpper();
            }
        }

        /// <summary>
        /// 既存データのPKEY編集を不可に
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C1FlexGrid1_BeforeEdit(object sender, RowColEventArgs e)
        {
            var colInfo = GetMasterTableColumn(e.Col);
            if (colInfo == null) return;

            if (colInfo.PrimaryKey && e.Row <= this.OriginalData.Rows.Count)
            {
                e.Cancel = true;
            }
        }

        private async Task setupRowData()
        {
            string url = string.Format(ApiResources.HatF.MasterEditor.MasterTable, this.MasterEditEntity.Name);
            var apiResponse = await Program.HatFApiClient.GetAsync<string>(url, null);
            string dataTableJson = apiResponse.Data;
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(dataTableJson);
            dataTable.AcceptChanges();

            this.OriginalData = dataTable.Copy();

            c1FlexGrid1.DataSource = dataTable;
            c1FlexGrid1.ApplyFilters();
        }

        private void setupGridView()
        {
            c1FlexGrid1.Clear();
            c1FlexGrid1.AutoGenerateColumns = false;
            c1FlexGrid1.AllowAddNew = true;
            c1FlexGrid1.AllowFiltering = true;
            c1FlexGrid1.Cols.Count = MasterEditEntity.Columns.Count + 1;

            using (Graphics graphics = this.CreateGraphics())
            {
                c1FlexGrid1.Cols[0].Caption = "";
                c1FlexGrid1.Cols[0].Name = "Header";

                for (int i = 1; i <= MasterEditEntity.Columns.Count; i++)
                {
                    var columnInfo = MasterEditEntity.Columns[i - 1];
                    var c1Col = c1FlexGrid1.Cols[i];

                    c1Col.Name = columnInfo.Name;

                    c1Col.Caption = columnInfo.LogicalName;
                    if (columnInfo.PrimaryKey)
                    {
                        c1Col.Caption = $"{columnInfo.LogicalName} (CD)";
                    }
                    else if (columnInfo.Unique)
                    {
                        c1Col.Caption = $"{columnInfo.LogicalName} (重複不可)";
                    }


                    if (columnInfo.CheckBox)
                    {
                        c1Col.DataType = typeof(bool);
                        c1Col.ImageAlign = ImageAlignEnum.CenterCenter;
                        c1Col.Width = 50;
                    }
                    else
                    {
                        c1Col.MaxLength = columnInfo.MaxLength;
                        // 全角想定で幅計算
                        string text = new string('漢', columnInfo.MaxLength);
                        int width = (int)(TextRenderer.MeasureText(graphics, text, c1FlexGrid1.Font).Width);
                        width = Math.Max(width, 100);
                        width = Math.Min(width, 200);
                        c1Col.Width = width;
                        c1Col.TextAlign = TextAlignEnum.LeftCenter;
                    }

                    if (columnInfo.Name == "DELETED")
                    {
                        c1Col.Filter = this.DeletedFilter;
                    }
                }
            }
        }

        private (bool, string, string) validate1Row(DataRow row)
        {
            foreach (var columnInfo in this.MasterEditEntity.Columns)
            {
                if (columnInfo.CheckBox)
                {
                    continue;
                }
                var col = row[columnInfo.Name].ToString();
                if (col.Length < columnInfo.MinLength)
                {
                    return (false, columnInfo.LogicalName, string.Format(ERROR_MIN_LENGTH, columnInfo.LogicalName, columnInfo.MinLength));
                }
            }
            return (true, "", "");
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        private bool ValidationInput()
        {
            DataTable dt = (DataTable)c1FlexGrid1.DataSource;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var (result, colName, msg) = validate1Row(dt.Rows[i]);
                if (!result)
                {
                    DialogHelper.WarningMessage(this, $"{i + 1}行目:{msg}");
                    return false;
                }
            }

            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationInput())
            {
                // 入力エラー
                return;
            }

            DataTable dataGridSoruce = (DataTable)c1FlexGrid1.DataSource;
            DataTable updateSource = dataGridSoruce.GetChanges();

            if (updateSource == null)
            {
                DialogHelper.InformationMessage(this, "変更はありません。");
            }
            else
            {
                string message = "更新してよろいですか?";
                if (DialogHelper.OkCancelQuestion(this, message, true))
                {
                    // 保存処理
                    string dataTableJson = JsonConvert.SerializeObject(updateSource);
                    string url = string.Format(ApiResources.HatF.MasterEditor.MasterTable, this.MasterEditEntity.Name);
                    var apiResponse = await Program.HatFApiClient.PutAsync<int>(url, new ApiArgumentWrapper(dataTableJson));

                    // 処理状況結果メッセージを出すなど
                    Func<Task> updateConflictReloadLogic = async () => await setupRowData();    // 楽観的排他ロックでエラー時のリロード処理
                    if (await ApiHelper.AfterDataUpdateBehaviorAsync(this, apiResponse, updateConflictReloadLogic))
                    {
                        dataGridSoruce.AcceptChanges();
                        // 更新成功時
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 閉じる確認はFormClosingイベントで行う
            this.Close();
        }

        private void ME_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (c1FlexGrid1.Focused)
                {
                    // グリッドの編集データをDataTableへ反映しておく
                    this.SelectNextControl(this, true, true, true, true);
                }

                DataTable dataGridSoruce = (DataTable)c1FlexGrid1.DataSource;
                DataTable updateSource = dataGridSoruce.GetChanges();

                if (updateSource != null)
                {
                    string message = "保存されていない変更があります。保存せずに終了してよろいですか?";
                    if (false == DialogHelper.YesNoQuestion(this, message, true))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            _formClosing = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid1.Focused)
            {
                // グリッドの編集データをDataTableへ反映しておく
                this.SelectNextControl(this, true, true, true, true);
            }

            DataTable dataGridSoruce = (DataTable)c1FlexGrid1.DataSource;
            DataTable updateSource = dataGridSoruce.GetChanges();

            if (updateSource == null)
            {
                DialogHelper.InformationMessage(this, "変更はありません。");
            }
            else
            {
                string message = "保存されていない変更があります。変更を破棄してよろいですか?";
                if (false == DialogHelper.YesNoQuestion(this, message, true))
                {
                    return;
                }

                dataGridSoruce.RejectChanges();
                dataGridSoruce.AcceptChanges();
                c1FlexGrid1.Update();
            }
        }

        private void ME_Editor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10:
                    if (btnReset.Enabled == true)
                        btnReset.PerformClick();
                    break;
                case Keys.F11:
                    if (btnSave.Enabled == true)
                        btnSave.PerformClick();
                    break;
                case Keys.F12:
                    if (btnCancel.Enabled == true)
                        btnCancel.PerformClick();
                    break;
            }
        }

        private void cbShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowDeleted.Checked == true)
            {
                c1FlexGrid1.Cols["DELETED"].Filter = null;
            }
            else
            {
                c1FlexGrid1.Cols["DELETED"].Filter = this.DeletedFilter;
            }
            c1FlexGrid1.ApplyFilters();
        }
    }
}
