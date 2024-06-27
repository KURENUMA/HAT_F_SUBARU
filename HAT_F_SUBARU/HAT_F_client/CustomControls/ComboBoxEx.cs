using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    /// <summary>"Code:Name"形式の表示に特化したコンボボックス</summary>
    public partial class ComboBoxEx : ComboBox
    {
        private List<string> sourceItems = new List<string>();

        private const int INPUT_DELAY = 1;

        /// <summary>コードと値を区切る文字</summary>
        private const string SEPARATOR = ":";

        private bool enableFiltering = false;
        private CancellationTokenSource _ctsFiltering = new CancellationTokenSource();
        private string filterWord = "";

        private CancellationTokenSource _ctsAutoJump = new CancellationTokenSource();

        private bool inited = false;

        /// <summary>選択が確定したとき、コードを通知する</summary>
        public event EventHandler<string> CodeChanged;

        public ComboBoxEx() {
            SetComboBoxEx();
        }
        private void SetComboBoxEx() {
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.FlatStyle = FlatStyle.Flat;
            this.ImeMode = ImeMode.Off;
            this.KeyDown += new KeyEventHandler(ComboBoxEx_KeyDown);
            this.Enter += new EventHandler(ComboBoxEx_Enter);
            this.Leave += new EventHandler(ComboBoxEx_Leave);
            this.TextChanged += new EventHandler(ComboBoxEx_TextChanged);
            this.SelectedValueChanged += new EventHandler(ComboBoxEx_SelectedValueChanged);
            this.SelectionChangeCommitted += new EventHandler(ComboBoxEx_SelectionChangeCommitted);
            this.inited = false;
            log("NEW");
        }

        /// <summary>コンボボックスの選択が確定したとき</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント嬢王</param>
        private void ComboBoxEx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CodeChanged?.Invoke(this, GetSelectedCode());
        }

        private void log(string msg) {
            //Debug.WriteLine($"{this.Name}:{msg}: {this.inited}");
        }

        private void resetItems() {
            log("resetItems");
            this.Items.Clear();
            this.Items.AddRange(sourceItems.ToArray());

            this.filterWord = "";
            this._ctsFiltering.Cancel();
            this.enableFiltering = false;

            this._ctsAutoJump.Cancel();
        }
        private async Task doFilteredItemSelect(string text)
        {
            int cursorPosition = this.SelectionStart;
            if (this.filterWord == text) {
                return;
            }
            this.filterWord = text;

            this.Items.Clear();
            this.Items.AddRange(sourceItems.Where(item => item.Contains(this.filterWord)).ToArray());
            log($"doFiltered: {this.inited} {filterWord}");
            if (this.Items.Count > 1) {
                if (!this.DroppedDown) {
                    this.DroppedDown = true;
                }
            }
            else if (this.Items.Count == 1 && this.filterWord != this.Items[0].ToString()) {
                this.DroppedDown = false;
                this.Text = this.Items[0].ToString();

                // AUTO JUMP
                await doAutoSelectJump();
            }

            this.SelectionStart = cursorPosition;
        }
        private async Task doAutoSelectJump() {
            _ctsAutoJump.Cancel();
            _ctsAutoJump = new CancellationTokenSource();
            try {
                await Task.Delay(INPUT_DELAY, _ctsAutoJump.Token);
                this.Parent.SelectNextControl(this, true, true, true, true);
            } catch (TaskCanceledException) { }
        }

        private void ComboBoxEx_Enter(object sender, EventArgs e)
        {
            log("Enter");
            this.BackColor = Color.Yellow;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.ForeColor = SystemColors.HotTrack;

            resetItems();
        }
        private void ComboBoxEx_Leave(object sender, EventArgs e)
        {
            log("Leave");
            this.BackColor = SystemColors.Window;
            this.Font = new Font(this.Font, FontStyle.Regular);
            this.ForeColor = SystemColors.WindowText;
            this.SelectionLength = 0;

            resetItems();
        }

        /// <summary>ウィンドウメッセージがコントロールにより処理される前のフック処理</summary>
        /// <param name="msg">メッセージ構造体</param>
        /// <returns>メッセージがコントロールによって処理された場合は true。それ以外の場合は false。</returns>
        public override bool PreProcessMessage(ref Message msg)
        {
            const int WM_KEYDOWN = 0x100;
            if (msg.Msg == WM_KEYDOWN)
            {
                var keyCode = (Keys)msg.WParam & Keys.KeyCode;
                if(keyCode == Keys.Escape && DroppedDown) 
                {
                    // ドロップダウンを開いている場合にEscを押すとドロップダウンが終了するが
                    // SelectionChangeCommittedイベントが発生しない
                    // 選択は現在の項目にあわせて変更してしまうのでEscキーをEnterキーに差し替える
                    msg.WParam = (IntPtr)Keys.Enter;
                }
            }
            return base.PreProcessMessage(ref msg);
        }

        /// <summary>キーダウン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void ComboBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            log($"KeyDown:Code={e.KeyCode} Data={e.KeyData}");
            this.inited = true;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) {
                if (!this.DroppedDown) { this.DroppedDown = true; }
                enableFiltering = false;
            } else if (e.KeyCode == Keys.Return) {
                await doAutoSelectJump();
            } else {
                enableFiltering = true;
            }
        }

        private async void ComboBoxEx_TextChanged(object sender, EventArgs e) {
            log("TextChanged");
            _ctsAutoJump.Cancel();
            if (!enableFiltering) {
                return;
            }
            log($"TextChanged: {enableFiltering} {this.Text.Length}");
            _ctsFiltering.Cancel();
            _ctsFiltering = new CancellationTokenSource();
            try {
                await Task.Delay(INPUT_DELAY, _ctsFiltering.Token);
                await doFilteredItemSelect(this.Text);
            }
            catch (TaskCanceledException) { }
        }


        private void ComboBoxEx_SelectedValueChanged(object sender, EventArgs e) {
            log($"SelectedValueChanged: {this.inited}");
            if (this.Text.Length == 0) {
                resetItems();

                // 選択値が変更された場合にコンボボックスの値を再設定してドロップダウンを開く理由は不明だが
                // 何かしらの問題があり再設定が必要だったと考えられるためドロップダウンを開く処理のみ無効とする
                //if (!this.DroppedDown && this.inited) {
                //    this.DroppedDown = true;
                //}
            }
        }

        public void SetItems(IEnumerable<string> items) {
            log("SetItems");
            sourceItems = items.ToList();
            this.Items.Clear();
            this.Items.AddRange(sourceItems.ToArray());
        }

        /// <summary>現在のコードを取得する</summary>
        /// <returns>現在のコード</returns>
        public string GetSelectedCode() 
        {
            var value = SelectedIndex >= 0 ? Items[SelectedIndex].ToString() : Text;
            if (value == null || value.IndexOf(SEPARATOR) < 1) {
                return null;
            }
            return value.Substring(0, value.IndexOf(SEPARATOR));
        }

        /// <summary>コードを指定して選択項目を設定する</summary>
        /// <param name="code">コード</param>
        public void SetSelectedCode(string code)
        {
            var index = sourceItems.FindIndex(x => x.StartsWith($"{code}{SEPARATOR}"));
            if(index >=  0)
            {
                this.SelectedIndex = index;
            }
        }
    }
}
