using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.Repository;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    public class SuggestSelectedEventArgs : EventArgs 
    {
        public string SelectedItem { get; set; }
    }

    public class SuggetItemRecivedEventArgs : EventArgs
    {
        public int ItemCount { get; set; }
    }

    public partial class ComboBoxSuggest : ComboBox
    {
        private static readonly TimeSpan _suggestWait = new TimeSpan(0, 0, 0, 0, 400);  // キー押下からサジェスト取得までの待ち時間
        private static readonly Regex _productSuggestFormat = new Regex(@"\[(.+)\]\s?(.+)");    //商品サジェストの書式 「[商品コード] 商品名[規格]」
        private DateTime _lastProductInputDateTime = DateTime.MinValue;
        private DateTime _lastSelectedIndexChangedDateTime = DateTime.MinValue;
        private volatile string _latestSearchKeyword = "";
        private static readonly TimeSpan _suggestOneItemWait = new TimeSpan(0, 0, 0, 0, 750);  // サジェスト取得結果が1件だった場合に自動確定までの待ち時間

        // サジェストとペアになるコントロール
        public Control PairControl {  get; set; }

        /// <summary>
        /// サジェスト項目取得する際の最低検索文字数
        /// </summary>
        public int MinimumSearchKeywordLength { get; set; } = 1;

        /// <summary>
        /// サジェストデータの取得が有効か
        /// </summary>
        public bool SuggestItemFetchingEnabled { get; set; } = true;

        /// <summary>
        /// サジェスト項目をサーバーから受信したことを示すイベント
        /// </summary>
        public EventHandler<SuggetItemRecivedEventArgs> SuggetItemRecived;

        /// <summary>
        /// サジェスト項目が選択されたことを示すイベント
        /// </summary>
        public EventHandler<SuggestSelectedEventArgs> SuggestSelected;

        /// <summary>
        /// サジェスト項目が選択されずキャンセルされたことを示すイベント
        /// </summary>
        public EventHandler<EventArgs> SelectCanceled;

        /// <summary>
        /// アイテム選択、もしくはキャンセルイベントを起こす可能があるか
        /// </summary>
        /// <remarks>
        /// サジェストデータ取得後に1度だけアイテム選択かキャンセルイベントを実行するため
        /// </remarks>
        private bool _canRaiseItemSelectEvent = false;

        /// <summary>
        /// このコントロールで定義したイベントの有効性
        /// </summary>
        public bool CustomEventsEnabled { get; set; } = true;

        private bool _otherControlGotFocusAttached = false;

        public ComboBoxSuggest()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.DropDownWidth = this.Width;
                this.DropDownStyle = ComboBoxStyle.DropDown;

                this.TextChanged += ComboBoxSuggest_TextChanged;
                this.DropDownClosed += ComboBoxSuggest_DropDownClosed;
                this.DropDown += ComboBoxSuggest_DropDown;
                this.SelectionChangeCommitted += ComboBoxSuggest_SelectionChangeCommitted;
                this.SelectedIndexChanged += ComboBoxSuggest_SelectedIndexChanged;
                this.KeyDown += ComboBoxSuggest_KeyDown;
                this.VisibleChanged += ComboBoxSuggest_VisibleChanged;
            }
        }

        private void ComboBoxSuggest_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ComboBoxSuggest_SelectedIndexChanged)}");
            _lastSelectedIndexChangedDateTime = DateTime.Now;
        }

        private void ComboBoxSuggest_DropDown(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ComboBoxSuggest_DropDown)}");
            //MoveCaretToLast(this);
        }

        private void ComboBoxSuggest_VisibleChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ComboBoxSuggest_VisibleChanged)}: Visible {this.Visible}");

            // このメソッドがGUIデザイナーで実行されてしまい落ちる問題の対策
            if (this.DesignMode) { return; }
            if (this.FindForm() == null) { return; }

            if (!this.Visible)
            {
                // 非表示になるときはドロップダウンリストも非表示化
                this.DroppedDown = false;
            }
            else
            {
                // 1回だけイベント接続
                if (!_otherControlGotFocusAttached)
                {
                    foreach (Control control in FormHelper.GetAllControls(this.FindForm()))
                    {
                        if (!(control == this || control == this.PairControl))
                        {
                            // 他のコントロールがフォーカスを持ったのを検出したら自分を非表示
                            control.GotFocus += (object sender, EventArgs e) => { this.Hide(); };
                        }
                    }
                    _otherControlGotFocusAttached = true;
                }
            }
        }


        private void ComboBoxSuggest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DroppedDown = false;
                OnSelectCanceled(EventArgs.Empty);
            }
        }

        /// <summary>
        /// サジェスト項目の選択確定を表すイベントです
        /// </summary>
        public void OnSuggestSelected(SuggestSelectedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(OnSuggestSelected)}: {e.SelectedItem}");

            if (!CustomEventsEnabled)
            {
                System.Diagnostics.Debug.WriteLine("イベント無効化中");
                return;
            }

            if (!_canRaiseItemSelectEvent)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(OnSuggestSelected)}: 複数回実行防止");
            }
            else
            {
                _canRaiseItemSelectEvent = false;
                SuggestSelected?.Invoke(this, e);
            }
        }

        public void OnSelectCanceled(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(nameof(OnSelectCanceled));

            if (!CustomEventsEnabled) 
            {
                System.Diagnostics.Debug.WriteLine("イベント無効化中");
                return; 
            }

            if (!_canRaiseItemSelectEvent)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(OnSelectCanceled)}: 複数回実行防止");
            }
            else
            {
                _canRaiseItemSelectEvent = false;
                SelectCanceled?.Invoke(this, e);
            }
        }

        public void OnSuggetItemRecived(SuggetItemRecivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(nameof(OnSuggetItemRecived));

            if (!CustomEventsEnabled)
            {
                System.Diagnostics.Debug.WriteLine("イベント無効化中");
                return;
            }

            SuggetItemRecived?.Invoke(this, e);
        }

        public void FocusEx()
        {
            // フォーカスが自身 OR 商品コードのときだけ
            if (this.Focused || (this.PairControl?.Focused ?? true))
            {
                // このコントロール内でなるべく状況変化を起こさないようにしつつFocusする
                this.CustomEventsEnabled = false;
                this.Focus();
                this.CustomEventsEnabled = true;
            }
        }

        public void ResetSearch()
        {
            _latestSearchKeyword = "";
        }

        /// <summary>
        /// サジェストデータの取得開始
        /// </summary>
        public async Task FetchSuggestItemsAsync(string searchKeyword)
        {
            System.Diagnostics.Debug.WriteLine(nameof(FetchSuggestItemsAsync));

            if (!this.SuggestItemFetchingEnabled)
            {
                System.Diagnostics.Debug.WriteLine($"サジェストデータの取得キャンセル(無効化中)");
                return;
            }

            this.Text = searchKeyword;
            this.SelectionStart = this.Text.Length;

            await SuggestProcessAsync(searchKeyword);
        }


        private Tuple<string, string> ParseProduct(string text)
        {
            Tuple<string, string> result;

            var match = _productSuggestFormat.Match(text);
            if (match.Success)
            {
                string productCode = match.Groups[1].Value.Trim();
                string productName = match.Groups[2].Value.Trim();
                result = new Tuple<string, string>(productCode, productName);
            }
            else
            {
                result = new Tuple<string, string>("", "");
            }

            return result;
        }

        private bool ContainsProductCodeInSuggests(string productCode)
        {
            if (!string.IsNullOrEmpty(productCode)) { return false; }

            foreach (string suggest in this.Items)
            {
                var product = ParseProduct(suggest);
                string suggestProductCode = product.Item1;
                if (string.Equals(productCode, suggestProductCode))
                {
                    return true;
                }
            }

            return false;
        }


        private string GetProductCode(string keyword)
        {
            // サーバーから受信した形式に一致すれば1文字以上を取得
            var productCode = ParseProduct(keyword).Item1;    // 商品コード

            if (string.IsNullOrEmpty(productCode))
            {
                // サーバーから受信した形式じゃないが、
                // 商品コードとして正しいかチェックしたい
                productCode = keyword;
            }

            if (ContainsSuggestProductCode(productCode))
            {
                return productCode;
            }

            return "";
        }

        /// <summary>
        /// サジェスト項目中に該当商品コードが存在するか
        /// </summary>
        private bool ContainsSuggestProductCode(string productCode) 
        {
            bool isContains = false;

            foreach (string item in this.Items) 
            { 
                var suggestProductCode = ParseProduct(item).Item1;

                if (string.Equals(productCode, suggestProductCode)) 
                { 
                    isContains= true;
                    break;
                }
            }

            return isContains;
        }

        private void ComboBoxSuggest_DropDownClosed(object sender, EventArgs e)   
        {
            System.Diagnostics.Debug.WriteLine($"ComboBoxSuggest_DropDownClosed: {this.Text}");

            if (!CustomEventsEnabled)
            {
                System.Diagnostics.Debug.WriteLine($"イベント無効化中");

                // カスタムイベント無効化中はドロップダウンリストを閉じたくない
                if (this.Visible) 
                {
                    this.DroppedDown = true;
                }
                return;
            }

            OnSelectCanceled(EventArgs.Empty);
        }

        private async void ComboBoxSuggest_TextChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"ComboBoxSuggest_TextChanged: {this.Text}");

            if (!CustomEventsEnabled)
            {
                System.Diagnostics.Debug.WriteLine($"イベント無効化中");
                return;
            }

            // ドロップダウンリストを選択した際、
            // TextChanged -> SelectedIndexChanged の順でイベントが発生するようなので
            // 先にSelectedIndexChangedを処理してもらうためディレイ入れる
            await Task.Delay(1);

            TimeSpan noActionTime = new TimeSpan(0, 0, 0, 10);
            if (DateTime.Now < (_lastSelectedIndexChangedDateTime + noActionTime))
            {
                System.Diagnostics.Debug.WriteLine($"ドロップダウンリスト選択インデックス変更直後はTextChangedを処理しない");
                return;
            }

            string keyword = this.Text;
            await this.FetchSuggestItemsAsync(keyword);
        }

        private void ComboBoxSuggest_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"ComboBoxSuggest_SelectionChangeCommitted: [Text: {this.Text}]");
            System.Diagnostics.Debug.WriteLine($"ComboBoxSuggest_SelectionChangeCommitted: [SelectedIndex: {this.SelectedIndex}]");

            if (!CustomEventsEnabled) 
            {
                System.Diagnostics.Debug.WriteLine($"イベント無効化中");
                return; 
            }

            string keyword = this.Text;
            if (this.SelectedIndex >= 0)
            {
                // ドロップダウンリストのマウスクリック時
                keyword = (string)this.Items[this.SelectedIndex];
            }

            string productCode = GetProductCode(keyword);
            if (!string.IsNullOrEmpty(productCode))
            {
                var ea = new SuggestSelectedEventArgs() { SelectedItem = productCode };
                OnSuggestSelected(ea);
            }
        }

        private async Task SuggestProcessAsync(string searchKeyword)
        {
            Application.DoEvents(); //キーボード入力のウィンドウメッセージを一通り回す

            ComboBoxSuggest productComboBox = this;

            DateTime prevInputDateTime = _lastProductInputDateTime;
            DateTime currentProductInputDateTime = DateTime.Now;
            _lastProductInputDateTime = currentProductInputDateTime;

            string val = searchKeyword ?? "";

            if (val.Length < MinimumSearchKeywordLength)
            {
                // 最低N字入力したら検索
                System.Diagnostics.Debug.WriteLine("最低検索長チェック検索キャンセル");
                return;
            }

            // 1文字入力ごとに検索せず、キー入力がひと段落してから検索するためちょっと待つ
            await Task.Delay(_suggestWait);

            if (_lastProductInputDateTime > currentProductInputDateTime)
            {
                // 待ち時間中に次のキーが押されたのでサジェスト取得をキャンセル
                System.Diagnostics.Debug.WriteLine("連続打鍵キャンセル");
                return;
            }

            // 検索キーワード
            string keyword = (searchKeyword ?? "").Trim();

            int selectionStart = this.SelectionStart;
            productComboBox.Items.Clear();
            productComboBox.Items.Add("取得中...");
            this.SelectionStart = selectionStart;

            if (productComboBox.Visible)
            {
                System.Diagnostics.Debug.WriteLine("取得中...");
                if (this.Visible)
                {
                    productComboBox.DroppedDown = true;
                }
            }

            _latestSearchKeyword = keyword;

            System.Diagnostics.Debug.WriteLine($"サジェストデータ取得開始: {DateTime.Now:HH:mm:ss} ({keyword})");
            var response = await Program.HatFApiClient.GetAsync<string[]>(string.Format(ApiResources.HatF.Client.ProductSuggestion, keyword));

            if (this.IsDisposed)
            {
                ApplicationInsightsHelper.TelemetryClient.TrackTrace("WebAPI 待機中に画面が閉じられました。", SeverityLevel.Warning);
                return;
            }

            if (keyword != _latestSearchKeyword)
            {
                // 検索に時間がかかり複数回の検索結果が前後した場合の対策
                // 検索キーワードが古くなっていたら破棄
                // ※取得上限に抵触する検索は早く終わることが多いが、満たない検索は全件走査になり時間がかかる
                System.Diagnostics.Debug.WriteLine($"古い検索結果の破棄: {keyword}");
                return;
            }

            if (null == response.Data)
            {
                string message = $"WebAPIが商品サジェストデータとしてnull値を返しました。検索キーワード:{keyword}";
                ApplicationInsightsHelper.TelemetryClient.TrackTrace(message, SeverityLevel.Warning);
            }

            string[] suggestions = response.Data ?? new string[] { };

            selectionStart = this.SelectionStart;
            productComboBox.Items.Clear();
            productComboBox.Items.AddRange(suggestions);

            // 「《xxxx》」はサーバーからの注釈の想定 として、除外した件数カウント
            int effectiveDataCount = suggestions.Where(item => !(item.StartsWith("《") && item.EndsWith("》"))).Count();

            if (1 == effectiveDataCount)
            {
                System.Diagnostics.Debug.WriteLine("有効データ1件(確定)");
                string productCode = ParseProduct(suggestions.First()).Item1;

                if (productComboBox.Visible)
                {
                    System.Diagnostics.Debug.WriteLine("商品サジェスト展開");
                    if (this.Visible)
                    {
                        productComboBox.DroppedDown = true;
                    }
                }

                // 少し待って(単一候補を表示して)から確定
                await Task.Delay(_suggestOneItemWait);

                // サジェスト項目取得後に確定やキャンセルのイベント実行可
                _canRaiseItemSelectEvent = true;

                var ea = new SuggestSelectedEventArgs() { SelectedItem = productCode };
                OnSuggestSelected(ea);
            }
            else
            {
                if (0 < effectiveDataCount) 
                {
                    if (productComboBox.Visible)
                    {
                        System.Diagnostics.Debug.WriteLine("商品サジェスト展開");
                        if (this.Visible)
                        {
                            productComboBox.DroppedDown = true;
                        }
                    }
                }

                await Task.Delay(10);

                // サジェスト項目取得後に確定やキャンセルのイベント実行可
                _canRaiseItemSelectEvent = true;

                var ea = new SuggetItemRecivedEventArgs() { ItemCount = effectiveDataCount };
                OnSuggetItemRecived(ea);

                this.SelectionStart = selectionStart;
                this.SelectionLength = 0;
            }
        }

        private void MoveCaretToLast(ComboBox comboBox) 
        {
            comboBox.SelectionStart = comboBox.Text.Length;
            comboBox.SelectionLength = 0;
        }
    }
}
