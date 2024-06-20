using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace HatFClient.Extensions
{
    /// <summary><see cref="DataRow"/>の拡張メソッド</summary>
    public static class DataRowExtension
    {
        /// <summary>指定列を文字列化</summary>
        /// <param name="source">行</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static string GetString(this DataRow source, string columnName)
        {
            if ((source[columnName] == null) || source.IsNull(columnName))
            {
                return string.Empty;
            }
            // Converts the value of the specified System.Object to its System.String format.
            var s = source[columnName] is string ? source[columnName] as string : Convert.ToString(RuntimeHelpers.GetObjectValue(source[columnName]));
            return s.Replace("〜", "～");
        }
    }
}
