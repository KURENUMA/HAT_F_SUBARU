namespace HAT_F_api.Services
{
    /// <summary>
    /// 対応するSQL Serverシーケンスオブジェクトを表します
    /// </summary>
    public class TargetSequenceObjectAttribute : Attribute
    {
        /// <summary>
        /// シーケンス名
        /// </summary>
        public string TargetSequenceObject { get; }

        /// <summary>
        /// インスタンスを生成します
        /// </summary>
        /// <param name="targetSequenceName">シーケンス名</param>
        public TargetSequenceObjectAttribute(string targetSequenceName) 
        {
            TargetSequenceObject = targetSequenceName;
        }
    }
}
