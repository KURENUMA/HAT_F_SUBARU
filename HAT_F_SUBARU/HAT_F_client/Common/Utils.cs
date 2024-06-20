using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    public class Utils {
        public static string ConvertSnakeCaseToCamelCase(string text) {
            // 文字列をアンダースコアで分割し、各単語の最初の文字を大文字にする
            var words = text.ToLowerInvariant().Split('_')
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(word => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word));

            // 最初の単語を除き、残りの単語を連結
            return string.Concat(words.First().ToLowerInvariant(), string.Concat(words.Skip(1)));
        }
        public static string ConvertSnakeCaseToPacalCase(string text) {
            var words = text.ToLowerInvariant().Split('_')
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(word => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word));

            // 最初の単語を除き、残りの単語を連結
            return string.Concat(words);
        }
        public static bool PropertyExists<T>(string propertyName) {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo != null;
        }
        public static T GetFieldValue<T>(object obj, string fieldName) {
            // オブジェクトのTypeを取得
            Type type = obj.GetType();

            // 指定されたフィールドのPropertyInfoを取得
            PropertyInfo propertyInfo = type.GetProperty(fieldName);

            // プロパティが存在するかチェック
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(T))
            {
                // プロパティの値を取得し、キャストして返す
                return (T)propertyInfo.GetValue(obj);
            }

            throw new InvalidOperationException("Property not found or type mismatch.");
        }
    }
}
