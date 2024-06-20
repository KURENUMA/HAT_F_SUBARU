using System.Collections.Generic;
using System.Linq;

namespace HatFClient.Soap
{
    /// <summary>XML文字列に関するユーティリティ</summary>
    internal static class XmlHelper
    {
        /// <summary>XMLタグを作成する</summary>
        /// <param name="tagName">タグ名</param>
        /// <param name="value">タグの値</param>
        /// <returns>XMLタグ</returns>
        public static string Tag(string tagName, object value, IEnumerable<KeyValuePair<string, string>> attributes = null)
        {
            if (attributes is not null)
            {
                var attributesString = string.Join(" ", attributes.Select(a => $"{a.Key} = \"{Escape(a.Value)}\""));
                return $"<{tagName} {attributesString}>{Serialize(value)}</{tagName}>";
            }
            else
            {
                return $"<{tagName}>{Serialize(value)}</{tagName}>";
            }
        }

        /// <summary>stringタグを作成する</summary>
        /// <param name="value">stringタグの値</param>
        /// <returns>stringタグ</returns>
        public static string String(object value)
            => Tag("string", value);

        /// <summary>XML文字列のエスケープ</summary>
        /// <param name="xml">XML文字列</param>
        /// <returns>エスケープ結果</returns>
        public static string Escape(string xml)
            => xml.Replace("/&/g", "&amp")
                .Replace("/</g", "&lt")
                .Replace("/>/g", "&gt");

        /// <summary>シリアライズ</summary>
        /// <param name="value">値</param>
        /// <returns>シリアライズ結果</returns>
        private static string Serialize(object value)
        {
            switch (value)
            {
                case string s:
                    return Escape(s);
                case bool b:
                    return b.ToString();
            }
            return null;
        }
    }
}
