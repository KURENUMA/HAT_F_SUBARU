namespace HatFClient.Models
{
    /// <summary>
    /// <para>コードと名前をセットで管理するクラス</para>
    /// <para>1:AAA, 2:BBB...のようなコードを表す</para>
    /// </summary>
    /// <typeparam name="T">コードの型</typeparam>
    internal class CodeName<T>
    {
        /// <summary>コード</summary>
        public T Code { get; set; }

        /// <summary>名前</summary>
        public string Name { get; set; }
    }
}