using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    /// <summary>日付入力用コントロール</summary>
    public partial class C1DateInputEx : C1.Win.Calendar.C1DateEdit
    {
        #region 書式制御プロパティ

        /// <summary><see cref="FocusedBackColor"/>を使用するかどうか</summary>
        [Category("フォーカス取得時")]
        [Description($"コントロールがフォーカスされているときの背景色に{nameof(FocusedBackColor)}を使用します。")]
        [DefaultValue(true)]
        public bool UseFocusedBackColor { get; set; } = true;

        /// <summary>フォーカスされているときの色</summary>
        [Category("フォーカス取得時")]
        [DefaultValue(typeof(Color), nameof(Color.Yellow))]
        [Description($"コントロールがフォーカスされているときの背景色を選択します。")]
        public Color FocusedBackColor { get; set; } = Color.Yellow;

        /// <summary>フォーカスされいているときに太字にするかどうか</summary>
        [Category("フォーカス取得時")]
        [Description("コントロールがフォーカスされているときに文字列を太字にします。")]
        [DefaultValue(true)]
        public bool BoldOnFocused { get; set; } = true;

        /// <summary><see cref="FocusedForeColor"/>を使用するかどうか</summary>
        [Category("フォーカス取得時")]
        [Description($"コントロールがフォーカスされているときの前景色に{nameof(FocusedForeColor)}を使用します。")]
        [DefaultValue(true)]
        public bool UseFocusedForeColor { get; set; } = true;

        /// <summary>フォーカスされているときの前景色</summary>
        [Category("フォーカス取得時")]
        [DefaultValue(typeof(Color), nameof(SystemColors.HotTrack))]
        [Description("コントロールがフォーカスされているときの前景色を選択します。")]
        public Color FocusedForeColor { get; set; } = SystemColors.HotTrack;

        #endregion 書式制御プロパティ

        #region private

        /// <summary>背景色のバックアップ</summary>
        private Color _backColorBackup;

        /// <summary>前景色のバックアップ</summary>
        private Color _foreColorBackup;

        /// <summary>フォントスタイルのバックアップ</summary>
        private FontStyle _fontStyleBackup;

        #endregion private

        /// <summary>値の有無</summary>
        public bool HasValue => Value != DBNull.Value;


        /// <summary>コンストラクタ</summary>
        public C1DateInputEx()
        {
            SetC1DateInputEx();
        }

        /// <summary>コントロールの設定を行う</summary>
        private void SetC1DateInputEx()
        {
            this.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.EditFormat.CustomFormat = @"yy/MM/dd";
            this.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DisplayFormat.CustomFormat = @"yy/MM/dd";
            this.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.LoopPosition = false;
            this.EmptyAsNull = true;
            this.ImeMode = ImeMode.Disable;
            this.DateTimeInput = false;
            this.EditMask = "90/90/90";
            this.MaxLength = 8;
            this.ExitOnLastChar = true;
            this.Enter += new EventHandler(C1DateInputEx_Enter);
            this.Leave += new EventHandler(C1DateInputEx_Leave);
        }

        /// <summary>コントロールがフォーカスを取得したとき</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void C1DateInputEx_Enter(object sender, EventArgs e)
        {
            // 現在のスタイルを記憶
            this._backColorBackup = this.BackColor;
            this._foreColorBackup = this.ForeColor;
            this._fontStyleBackup = this.Font.Style;
            // プロパティに応じてスタイルを変更
            if (this.UseFocusedBackColor)
            {
                this.BackColor = this.FocusedBackColor;
            }
            if (this.BoldOnFocused)
            {
                this.Font = new Font(this.Font, FontStyle.Bold);
            }
            if (this.UseFocusedForeColor)
            {
                this.ForeColor = FocusedForeColor;
            }
        }

        /// <summary>コントロールがフォーカスを喪失したとき</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void C1DateInputEx_Leave(object sender, EventArgs e)
        {
            // スタイルの復元
            this.BackColor = this._backColorBackup;
            this.Font = new Font(this.Font, this._fontStyleBackup);
            this.ForeColor = this._foreColorBackup;
        }
    }
}