using C1.Win.C1FlexGrid;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using HatFClient.Repository;
using HatFClient.CustomModels;
using System.Collections.Generic;
using HatFClient.Common;
using System.Linq;

namespace HatFClient.CustomControls
{
    public partial class GridPatternForm : Form
    {       // 親フォーム格納
        private PatternInfo _pattern { get; set; }
        private GridPatternRepo _patternRepo { get; set; }

        public event EventHandler<PatternInfo> OnSavePattern;


        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <remarks>
        /// このコンストラクタはGUIデザイナー用です。コードからは引数付きコンストラクタを呼び出してください。
        /// </remarks>
        public GridPatternForm()
        {
            InitializeComponent();

            if (!this.DesignMode) 
            {
                System.Diagnostics.Debugger.Break();
                throw new InvalidOperationException("このコンストラクタはGUIデザイナー用です。コードからは引数付きコンストラクタを呼び出してください。");
            }
        }

        public GridPatternForm(GridPatternRepo patternRepo, PatternInfo pattern)
        {
            InitializeComponent();

            FormStyleHelper.SetResizableDialogStyle(this);

            _patternRepo = patternRepo;
            _pattern = pattern;

            initColumnInfoView();

            listBox1.Items.AddRange(_pattern.getUnusedVarNames().ToArray());
            dataGridView1.DataSource = _pattern.Columns;

            tbPattern.Text = _pattern.Pattern;
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine($"{sender.GetType()}:{e.RowIndex}:{e.ColumnIndex}");
            if (e.ColumnIndex == dataGridView1.Columns["▲"].Index)
            {
                _pattern.swapColumnPos(e.RowIndex, e.RowIndex - 1);

            }
            else if (e.ColumnIndex == dataGridView1.Columns["▼"].Index)
            {
                _pattern.swapColumnPos(e.RowIndex, e.RowIndex + 1);
            }

        }

        private void initColumnInfoView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn
            {
                Name = "VarName",
                DataPropertyName = "VarName",
                HeaderText = "項目名",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 150,
            };
            dataGridViewColumn.ReadOnly = true;

            dataGridView1.Columns.Add(dataGridViewColumn);
            dataGridViewColumn = new DataGridViewColumn
            {
                Name = "Label",
                DataPropertyName = "Label",
                HeaderText = "表示ラベル",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 150,
            };
            dataGridView1.Columns.Add(dataGridViewColumn);
            dataGridViewColumn = new DataGridViewColumn
            {
                Name = "Width",
                DataPropertyName = "Width",
                HeaderText = "列幅",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 50,
            };
            dataGridView1.Columns.Add(dataGridViewColumn);

            var cc = new DataGridViewComboBoxColumn
            {
                Name = "TextAlignment",
                DataPropertyName = "TextAlignment",
                HeaderText = "文字揃え",
                Width = 75,
            };
            cc.Items.Add(new { Name = "左", Value = (int)TextAlignEnum.LeftCenter });
            cc.Items.Add(new { Name = "中央", Value = (int)TextAlignEnum.CenterCenter });
            cc.Items.Add(new { Name = "右", Value = (int)TextAlignEnum.RightCenter });
            cc.DisplayMember = "Name";
            cc.ValueMember = "Value";
            dataGridView1.Columns.Add(cc);

            var bb = new DataGridViewButtonColumn
            {
                Name = "▲",
                HeaderText = "",
                Text = "▲",
                UseColumnTextForButtonValue = true,
                Width = 30,
            };
            dataGridView1.Columns.Add(bb);
            bb = new DataGridViewButtonColumn
            {
                Name = "▼",
                HeaderText = "",
                Text = "▼",
                UseColumnTextForButtonValue = true,
                Width = 30,
            };
            dataGridView1.Columns.Add(bb);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView_DataError);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellContentClick);

            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSelectVar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                int selectedIndex = listBox1.SelectedIndex;

                List<string> targets = new List<string>();
                foreach (string item in listBox1.SelectedItems)
                {
                    targets.Add(item);
                    _pattern.addColumnByName(item);
                }

                foreach (string item in targets)
                {
                    listBox1.Items.Remove(item);
                }

                listBox1.SelectedIndex = Math.Min(selectedIndex, listBox1.Items.Count - 1);
            }
        }

        private void btnUnselectVar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) 
            {
                return;
            }

            var targets = new List<DataGridViewRow>();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    targets.Add(row);
                }
            }
            else
            {
                targets.Add(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex]);
            }

            foreach (DataGridViewRow row in targets)
            {
                string varName = (string)row.Cells[0].Value;
                _pattern.removeColumnByName(varName);
            }

                listBox1.Items.Clear();
            listBox1.Items.AddRange(_pattern.getUnusedVarNames().ToArray());

            foreach (var item in _pattern.Columns)
            {
                Debug.WriteLine($"{item.Label}:{item.Width}:{item.TextAlignment}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // CHECK FOR DUPNAME
            var patternTemp = _patternRepo.FindByName(tbPattern.Text);
            if (patternTemp != null && _pattern.UUID != patternTemp.UUID)
            {
                MessageBox.Show($"「{tbPattern.Text}」はすでに存在します。\nパターン名を変更してください。", "保存エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string varName = (string)row.Cells[0].Value;
                var item = _pattern.Columns.Where(x => x.VarName == varName).Single();
                item.Index = index++;   // Grid上の順序を記録
            }

            // Grid上の順で並べ替え
            var cols = _pattern.Columns.OrderBy(x => x.Index).ToList();
            _pattern.Columns.Clear();
            foreach (var item in  cols)
            {
                _pattern.Columns.Add(item);
            }

            _pattern.Pattern = tbPattern.Text; 
            OnSavePattern(this, _pattern);
            this.Close();
        }
        

        // DRAG AND DROP Behavior
        private Rectangle dragBoxFromMouseDown = Rectangle.Empty;

        private int rowIndexFromMouseDown = -1;

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(dataGridView1.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            int rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexOfItemUnderMouseToDrop != -1 && e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                //dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                //dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                Debug.WriteLine($"{rowIndexFromMouseDown} => {rowIndexOfItemUnderMouseToDrop}");
                _pattern.moveColumnPos(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
            }
            rowIndexFromMouseDown = -1;
        }
    }
}
