namespace HAT_F_api.CustomModels
{
    /// <summary>検索画面に表示されるドロップダウン選択肢の挙動を制御する</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CriteriaDefinitionAttribute : Attribute
    {
        /// <summary>表示文字列</summary>
        public string Label { get; private set; }

        /// <summary>プロパティが対応する列名</summary>
        public string Column { get; private set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="label">表示文字列</param>
        /// <param name="column">プロパティが対応する列名</param>
        public CriteriaDefinitionAttribute(string label = null, string column = null)
        {
            Label = label;
            Column = column;
        }
    }
}
