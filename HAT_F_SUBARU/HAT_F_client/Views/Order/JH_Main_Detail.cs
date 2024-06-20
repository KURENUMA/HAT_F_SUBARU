using HatFClient.Common;
using HatFClient.CustomControls;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Order
{
    /// <summary>受発注画面の商品１つ分を表すユーザーコントロール</summary>
    [Browsable(true)]
    public partial class JH_Main_Detail : UserControl
    {
        private const int MinimumSearchKeywordLength = 4;
        private static readonly TimeSpan _suggestWait = new TimeSpan(0, 0, 0, 0, 400);  // 連続打鍵時のサジェストデータ取得保留時間
        private DateTime _lastTextChangedDateTime = DateTime.MinValue;

        #region 各コントロールの値を表すプロパティ
        /// <summary>行番号</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? RowNo
        {
            get => HatFComParts.DoParseInt(txtroRowNo.Text);
            set => txtroRowNo.Text = value.ToString();
        }

        /// <summary>子番</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public short? Koban
        {
            get => HatFComParts.DoParseShort(txtKoban.Text);
            set => txtKoban.Text = value?.ToString();
        }

        /// <summary>商品分類CD</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SyobunCd
        {
            get => txtSYOBUN_CD.Text;
            set => txtSYOBUN_CD.Text = value;
        }

        /// <summary>商品コード</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SyohinCode
        {
            get => txtSYOHIN_CD.Text;
            set
            {
                var temporary = UseSuggest;
                UseSuggest = false;
                txtSYOHIN_CD.Text = value;
                UseSuggest = temporary;
            }
        }

        /// <summary>売区</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UriKubun
        {
            get => cmbURIKUBN.GetSelectedCode();
            set => cmbURIKUBN.SetSelectedCode(value);
        }

        /// <summary>在庫数</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? Zaikosuu
        {
            get => HatFComParts.DoParseInt(txtroZaikoSuu.Text);
            set => txtroZaikoSuu.Text = value?.ToString();
        }

        /// <summary>バラ数</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? Suuryo
        {
            get => HatFComParts.DoParseInt(numSURYO.Text);
            set => numSURYO.Text = value?.ToString();
        }

        /// <summary>単位</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Tani
        {
            get => txtTANI.Text;
            set => txtTANI.Text = value;
        }

        /// <summary>バラ数</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? Bara
        {
            get => HatFComParts.DoParseInt(numBARA.Text);
            set => numBARA.Text = value?.ToString();
        }

        /// <summary>定価単価</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? TeikaTanka
        {
            get => HatFComParts.DoParseDecimal(decTEI_TAN.Text);
            set => decTEI_TAN.Text = HatFComParts.DoFormatN2(value);
        }

        /// <summary>回答納期</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? KaitouNouki
        {
            get => !string.IsNullOrEmpty(dateNOUKI.Text) ? (DateTime)dateNOUKI.Value : null;
            set { if (value.HasValue) {dateNOUKI.Value = value;}}
        }

        /// <summary>仕入回答単価</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? ShiireKaitouTanka
        {
            get => HatFComParts.DoParseDecimal(decSII_ANSW_TAN.Text);
            set => decSII_ANSW_TAN.Text = HatFComParts.DoFormatN2(value);
        }

        /// <summary>売上記号</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UriKigou
        {
            get => txtURI_KIGOU.Text;
            set => txtURI_KIGOU.Text = value;
        }

        /// <summary>利率</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? Profit
        {
            get => HatFComParts.DoParseDecimal(txtroRiritsu.Text);
            set => txtroRiritsu.Text = value.HasValue ? string.Format("{0:#,0.0}", value) : string.Empty;
        }

    /// <summary>利率エラー</summary>
    [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ProfitError
        {
            get => txtroRiritsu.Text == "ERR";
            set => txtroRiritsu.Text = value ? "ERR" : string.Empty;
        }

        /// <summary>売上掛率</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? Urikake
        {
            get => HatFComParts.DoParseDecimal(decURI_KAKE.Text);
            set => decURI_KAKE.Text = HatFComParts.DoFormatN1(value);
        }

        /// <summary>売上単価</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? UriTan
        {
            get => HatFComParts.DoParseDecimal(decURI_TAN.Text);
            set => decURI_TAN.Text = HatFComParts.DoFormatN2(value);
        }

        /// <summary>倉庫コード</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SokoCd
        {
            get => cmbSOKO_CD.GetSelectedCode();
            set => cmbSOKO_CD.SetSelectedCode(value);
        }

        /// <summary>仕入先コード</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ShiresakiCode
        {
            get => txtSHIRESAKI_CD.Text;
            set => txtSHIRESAKI_CD.Text = value;
        }

        /// <summary>仕入記号</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SiiKigou
        {
            get => txtSII_KIGOU.Text;
            set => txtSII_KIGOU.Text = value;
        }

        /// <summary>仕入掛率</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? SiiKake
        {
            get => HatFComParts.DoParseDecimal(decSII_KAKE.Text);
            set => decSII_KAKE.Text = HatFComParts.DoFormatN1(value);
        }

        /// <summary>仕入単価</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? SiiTan
        {
            get => HatFComParts.DoParseDecimal(decSII_TAN.Text);
            set => decSII_TAN.Text = HatFComParts.DoFormatN2(value);
        }

        /// <summary>仕／定</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? NoTankaWariai
        {
            get => HatFComParts.DoParseDecimal(txtroNoTankaWariai.Text);
            set => txtroNoTankaWariai.Text = string.Format("{0:#,0.0}", value);
        }

        /// <summary>仕／定のエラー</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NoTankaWariaiError
        {
            get => txtroNoTankaWariai.Text == "ERR";
            set => txtroNoTankaWariai.Text = value ? "ERR" : string.Empty;
        }

        /// <summary>行備考</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LBiko
        {
            get => txtLBIKO.Text;
            set => txtLBIKO.Text = value;
        }

        /// <summary>仕入先備考</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SiireBiko
        {
            get => txtSiireBikou.Text;
            set => txtSiireBikou.Text = value;
        }

        /// <summary>消費税</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TaxFlg
        {
            get => txtTAX_FLG.Text;
            set => txtTAX_FLG.Text = value;
        }
        #endregion

        /// <summary>商品コードのサジェスト有効</summary>
        [Browsable(false), DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseSuggest { get; set; } = true;

        public JH_Main_Detail()
        {
            InitializeComponent();

            // アプリ実行時か、GUIデザイナーからの呼び出しかを判定
            if (!this.DesignMode)
            {
                txtSYOHIN_CD.TextChanged += ForShohinSuggest_TextChanged;
                txtSYOHIN_CD.KeyDown += TxtSYOHIN_CD_KeyDown;
                cmbSyohinSuggest.TabIndex = txtSYOHIN_CD.TabIndex + 1;
                cmbSyohinSuggest.MinimumSearchKeywordLength = MinimumSearchKeywordLength;
                cmbSyohinSuggest.SuggestSelected += CmbSyohinSuggest_SuggestSelected;
                cmbSyohinSuggest.SuggetItemRecived += CmbSyohinSuggest_SuggetItemRecived;
                cmbSyohinSuggest.SelectCanceled += CmbSyohinSuggest_SelectCanceled;
            }
        }

        private void TxtSYOHIN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.TopLevelControl.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        #region << 商品サジェスト >>
        /// <summary>
        /// 商品サジェストコントロールで、サジェストデータをサーバーから取得完了
        /// </summary>
        private void CmbSyohinSuggest_SuggetItemRecived(object sender, SuggetItemRecivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(nameof(CmbSyohinSuggest_SuggetItemRecived));

            if (0 < e.ItemCount)
            {
                // サジェストデータの取得が出来たらフォーカスを移す
                cmbSyohinSuggest.ImeMode = txtSYOHIN_CD.ImeMode;
                cmbSyohinSuggest.FocusEx();
            }
        }

        /// <summary>
        /// 商品サジェストコントロールで選択せずに閉じた
        /// </summary>
        private void CmbSyohinSuggest_SelectCanceled(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(nameof(CmbSyohinSuggest_SelectCanceled));
            txtSYOHIN_CD.Focus();
            cmbSyohinSuggest.Visible = false;
        }

        /// <summary>
        /// 商品サジェストコントロールで特定商品を選択確定した
        /// </summary>
        private void CmbSyohinSuggest_SuggestSelected(object sender, SuggestSelectedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(nameof(CmbSyohinSuggest_SuggestSelected));

            if (!UseSuggest) { return; }

            // 商品サジェストDropDownでアイテムが選択されたら、
            // 商品コード用TextBoxにコピー

            // TextChaned が発生しない状態にして値を変更
            txtSYOHIN_CD.TextChanged -= ForShohinSuggest_TextChanged;
            txtSYOHIN_CD.Text = e.SelectedItem;
            txtSYOHIN_CD.TextChanged += ForShohinSuggest_TextChanged;

            txtSYOHIN_CD.Focus();
            cmbSyohinSuggest.Visible = false;

            this.TopLevelControl.SelectNextControl(this.ActiveControl, true, true, true, true);
        }

        private int GetSjisLength(string text)
        {
            int length = Encoding.GetEncoding("Shift_JIS").GetBytes(text ?? "").Length;
            return length;
        }

        private async void ForShohinSuggest_TextChanged(object sender, EventArgs e)
        {
            if(!UseSuggest)
            {
                return;
            }

            Control ctrl = (Control)sender;
            if (ctrl.Text.Length >= MinimumSearchKeywordLength)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(ForShohinSuggest_TextChanged)}:if (Gctrl.Text.Length > 3)");

                DateTime currentDateTime = DateTime.Now;
                _lastTextChangedDateTime = currentDateTime;
                
                // 連続打鍵の待機
                await Task.Delay(_suggestWait);
                
                // 次の打鍵があったらやらない
                if (_lastTextChangedDateTime <= currentDateTime) 
                {
                    // 商品コード欄にフォーカスが残っているときだけサジェスト展開
                    if (txtSYOHIN_CD.Focused)
                    {
                        // APIサーバーからサジェストデータの取得開始
                        cmbSyohinSuggest.Visible = true;
                        cmbSyohinSuggest.PairControl = txtSYOHIN_CD;
                        await cmbSyohinSuggest.FetchSuggestItemsAsync(txtSYOHIN_CD.Text);
                    }
                }
                else
                {
                    cmbSyohinSuggest.Visible = false;
                }
            }
            else if(ctrl.Text.Length.Equals(0))
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(ForShohinSuggest_TextChanged)}:else if(ctrl.Text.Length.Equals(0))");
                cmbSyohinSuggest.Visible = false;
                cmbSyohinSuggest.ResetSearch();
            }
        }
        #endregion

        #region <<ボタンイベント>>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Search");
        }
        private void BtnSiireBikou_Click(object sender, EventArgs e)
        {
            using (JH_Main_Detail_SB dlg = new())
            {
                dlg.JH_Main_Detail_SB_StrInputText = this.txtSiireBikou.Text;
                switch (dlg.ShowDialog())
                {
                    case DialogResult.OK:
                        this.txtSiireBikou.Text = dlg.JH_Main_Detail_SB_StrInputText;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region << 計算 >>
        private void CalcPer_Validated(object sender, EventArgs e)
        {
            CalcSiireDivTeika();
            CalcProfit();
        }

        /// <summary>利率を算出して表示する</summary>
        public void CalcProfit()
        {
            // ([行.売上単価]-[行.仕入単価])÷[行.売上単価]×100 ※小数点第2位を四捨五入
            if (UriTan == 0m)
            {
                ProfitError = true;
            }
            else
            {
                Profit = (UriTan - SiiTan) / UriTan * 100;
            }
        }

        /// <summary>仕／定を算出して表示する</summary>
        public void CalcSiireDivTeika()
        {
            // [行.仕入単価]÷[行.定価単価]
            if (decSII_TAN.Text.Length > 0 && decTEI_TAN.Text.Length > 0)
            {
                if (TeikaTanka.HasValue && SiiTan.HasValue)
                {
                    if (TeikaTanka == 0m)
                    {
                        NoTankaWariaiError = true;
                    }
                    else
                    {
                        NoTankaWariai = SiiTan / TeikaTanka * 100;
                    }
                }
            }
            else
            {
                NoTankaWariai = null;
            }
        }
        #endregion

        #region << 分類・バラ数 >>
        private void TxtShobunCd_Validated(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (tb.Text.Length > 0)
            {
                this.numBARA.Enabled = true;
            }
            else
            {
                this.numBARA.Enabled = false;
            }
        }
        #endregion

        #region << 仕備ボタン色 >>
        private void TxtSiireBikou_TextChanged(object sender, EventArgs e)
        {
            if (this.txtSiireBikou.Text.Length == 0)
            {
                //this.btnSiireBikou.ForeColor = SystemColors.ControlText;
                this.btnSiireBikou.BackgroundImage = HatFClient.Properties.Resources.transparency_notes_icon;
            }
            else
            {
                //this.btnSiireBikou.ForeColor = Color.Red;
                this.btnSiireBikou.BackgroundImage = HatFClient.Properties.Resources.transparency_notes_icon_red;
            }
        }
        #endregion

        #region << フル桁時タブ遷移 >>
        private void ForTabOrder_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (tb.MaxLength == tb.Text.Length)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        #endregion

        #region << 入力文字制限 >>
        private void TextBoxCharType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumAlphabet(e.KeyChar);
        }
        private void TextBoxKigou_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strLine = "ABCDENXZ";
            bool bolFlg = false;
            if (e.KeyChar == '\b')
            {
                bolFlg = true;
            }
            else
            {
                for (int i = 0; i < strLine.Length; i++)
                {
                    if (e.KeyChar.ToString().ToUpper().Equals(strLine.Substring(i, 1)))
                    {
                        bolFlg = true;
                        break;
                    }
                }
            }
            if (bolFlg == false) { e.Handled = true; }
        }
        private void TextBoxKoban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '1' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        #region << 商品コード >>
        /// <summary>商品コード変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtSYOHIN_CD_TextChanged(object sender, EventArgs e)
        {
            btnSiireBikou.Enabled = !string.IsNullOrEmpty(SyohinCode.Trim());
        }
        #endregion << 商品コード >>
    }
}

