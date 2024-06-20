using C1.Win.C1FlexGrid;
using DocumentFormat.OpenXml.ExtendedProperties;
using HatFClient.Helpers;
using HatFClient.Models;
using HatFClient.Shared;
using HatFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using templateApp.Controls;

namespace HatFClient.CustomControls.Grids
{
    public partial class DefaultGridUpdate : UserControl
    {
        private IGridManager _manager;
        //private GridOrderManager _orderManager;

        public DefaultGridUpdate()
        {
            InitializeComponent();
        }

        public DefaultGridUpdate(IGridManager manager, GridOrderManager orderManager)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                _manager = manager;
                //_orderManager = orderManager;

                c1FlexGrid1.DataSource = _manager.Dt;

                _manager.OnDataSourceChange += (object sender, EventArgs args) =>
                {
                    IndexRowNumbers();
                };

                c1FlexGrid1.AfterDragColumn += GdProjectList_AfterDragColumn;
                c1FlexGrid1.MouseDoubleClick += projectGrid_click;
                //if(_orderManager != null )
                //InitializeColumns();
            }
        }


        private void projectGrid_click(object sender, MouseEventArgs e)
        {
            
        }
        private async void ProjectGrid_Load(object sender, EventArgs e)
        {
            await DataLoadAsync();
        }

        private void InitializeColumns()
        {
            //c1FlexGrid1.SuspendLayout();
            //// ヘッダの設定
            //c1FlexGrid1.Cols[0].Caption = "";
            ////c1FlexGrid1.Cols[0].Width = 30;
            //c1FlexGrid1.AllowFiltering = true;
            //c1FlexGrid1.AllowEditing = false;
            ////c1FlexGrid1.AutoResize = true;
            ////List<ColumnSetting> configs = _orderManager.GetColumnSettingsForSelectedPattern();

            //c1FlexGrid1.Cols.Count = configs.Count + 1;
            //c1FlexGrid1.Rows.DefaultSize = 30;

            ////    c1SuperTooltip1.AutomaticDelay = 100; // 1秒後にツールチップを表示

            //int columnIndexOffset = 1;

            //for (int i = 0; i < configs.Count; i++)
            //{
            //    if (TemplateHelpers.TemplateColumnConfigs.Exists(config => config.Caption == configs[i].ColumnName))
            //    {
            //        var config = TemplateHelpers.TemplateColumnConfigs.First(x => x.Caption == configs[i].ColumnName);
            //        c1FlexGrid1.Cols[i + columnIndexOffset].Caption = config.Caption;
            //        c1FlexGrid1.Cols[i + columnIndexOffset].Width = config.Width;
            //        c1FlexGrid1.Cols[i + columnIndexOffset].StyleNew.TextAlign = config.TextAlign;
            //        c1FlexGrid1.Cols[i + columnIndexOffset].StyleNew.Font = config.Font;
            //        if (config.DataType == typeof(DateTime))
            //        {
            //            c1FlexGrid1.Cols[i + columnIndexOffset].Format = "yyyy/MM/dd HH:mm:ss";
            //        }
            //    }
            //}
            //c1FlexGrid1.ResumeLayout();
        }

        private void IndexRowNumbers()
        {
            //すべての行についてループを実行
            for (int rowIndex = 1; rowIndex < c1FlexGrid1.Rows.Count; rowIndex++)
            {
                c1FlexGrid1[rowIndex, 0] = rowIndex; // 行のインデックスをCols[0]に設定
            }
        }

        /// <summary>
        /// 列をドラッグで入れ替えた際に表示順をJSONファイルに保存する
        /// </summary>
        private void GdProjectList_AfterDragColumn(object sender, DragRowColEventArgs e)
        {

            //// 現在の列順序を取得
            //List<string> currentColumnOrder = new List<string>();
            //for (int i = 1; i < c1FlexGrid1.Cols.Count; i++)
            //{
            //    // 1列目はヘッダ列のため、1からスタート
            //    currentColumnOrder.Add(c1FlexGrid1.Cols[i].Name);
            //}

            //// JSONを更新
            //_orderManager.UpdateColumnOrderForPattern(_orderManager.SelectedPattern, currentColumnOrder.ToArray());

            //// 更新されたJSONデータをファイルに保存
            //_orderManager.SaveToFile();
        }

        public void ApplyColumnOrder(string patternName)
        {


            //// 列の順序と設定を取得
            //var columnOrder = _orderManager.GetColumnOrderForPattern(patternName);

            //// グリッドの列順序を更新
            //for (int i = 0; i < columnOrder.Length; i++)
            //{
            //    var col = c1FlexGrid1.Cols[columnOrder[i]];

            //    // 最初の列（行ヘッダ用の列）を移動しないようにする
            //    if (col.Index == 0) continue;

            //    // 移動先のインデックスも0を避けるようにする
            //    int targetIndex = i + 1; // iが0から始まるため、最初の移動先インデックスは1

            //    c1FlexGrid1.Cols.Move(col.Index, targetIndex);

            //}
        }

        public void ApplyColumnSettings(string patternName)
        {
            //var columnSettings = _orderManager.GetColumnSettingsForPattern(patternName);
            //foreach (var setting in columnSettings)
            //{
            //    var col = c1FlexGrid1.Cols[setting.ColumnName];

            //    col.Width = setting.Width;
            //    col.Visible = setting.Visible;

            //}
        }
        public void ShowDetailForm(string frm, int id)
        {

        }

        public int GetProjectCount()
        {
            return _manager.Total;
        }

        public string GetFilterOptionStr()
        {
            return _manager.FilterOptionStr;
        }

        public async Task<bool> DataLoadAsync()
        {
            return await _manager.Reload();
        }

        private void c1FlexGrid1_DataSourceChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < c1FlexGrid1.Rows.Count; i++)
            //{
            //    for (int j = 0; j < c1FlexGrid1.Rows.Count; j++)
            //    {
            //        c1FlexGrid1[i, j] = i.ToString() + " " + j.ToString();
            //    }
            //}
        }

    }
}
