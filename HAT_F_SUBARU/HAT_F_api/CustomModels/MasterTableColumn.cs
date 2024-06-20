namespace HAT_F_api.CustomModels
{
    public class MasterTableColumn
    {
        /// <summary>
        /// 列の物理名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列の論理名
        /// </summary>
        public string LogicalName { get; set; }

        /// <summary>
        /// 主キー
        /// </summary>
        public bool PrimaryKey { get; set; }

        /// <summary>
        /// 列単体でユニーク
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// 最大桁数
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 最小桁数 (1以上=必須項目。チェックボックス列対象外)
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// 数字のみ入力可
        /// </summary>
        public bool NumberOnly { get; set; }

        /// <summary>
        /// 編集画面に列を表示するか
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 入力欄がチェックボックス
        /// </summary>
        public bool CheckBox { get; set; }

        /// <summary>
        /// 入力テキストを自動的に英大文字化
        /// </summary>
        public bool AutoUpperCase { get; set; }
    }
}
