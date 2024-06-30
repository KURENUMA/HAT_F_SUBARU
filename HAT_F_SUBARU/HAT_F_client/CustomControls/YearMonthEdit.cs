using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;

namespace HatFClient.CustomControls
{
    /// <summary>年月の入力用コントロール</summary>
    internal class YearMonthEdit : C1TextBox
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

        #region プロパティ拡張

        [Description("コントロールの値をDateTime型で取得または設定します。")]
        public new DateTime? Value
        {
            // 年月コントロールなのでDayは必ず1
            get
            {
                if (ValueIsDbNull)
                {
                    return null;
                }
                var result = (DateTime)base.Value;
                return new DateTime(result.Year, result.Month, 1);
            }
            set => base.Value = value;
        }

        #endregion プロパティ拡張

        /// <summary>コンストラクタ</summary>
        public YearMonthEdit()
        {
            DataType = typeof(DateTime);
            EmptyAsNull = true;
            EditMask = "99/99";
            CustomFormat = "yy/MM";
            FormatType = FormatTypeEnum.CustomFormat;
            DisplayFormat.CustomFormat = "yy/MM";
            DisplayFormat.FormatType = FormatTypeEnum.CustomFormat;
            EditFormat.CustomFormat = "yy/MM";
            EditFormat.FormatType = FormatTypeEnum.CustomFormat;

            Enter += YearMonthEdit_Enter;
            Leave += YearMonthEdit_Leave;
        }

        /// <summary>コントロールがフォーカスを取得したとき</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void YearMonthEdit_Enter(object sender, EventArgs e)
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
        private void YearMonthEdit_Leave(object sender, EventArgs e)
        {
            // スタイルの復元
            this.BackColor = this._backColorBackup;
            this.Font = new Font(this.Font, this._fontStyleBackup);
            this.ForeColor = this._foreColorBackup;
        }
    }
}