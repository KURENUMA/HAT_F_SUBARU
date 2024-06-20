using HatFClient.Common;
using System.Text;

namespace HatFClient.Extensions
{
    /// <summary><see cref="string"/>の拡張メソッド</summary>
    public static class StringExtension
    {
        /// <summary>ShiftJis</summary>
        private static Encoding _shiftJis = Encoding.GetEncoding("Shift_JIS");

        /// <summary>文字列の指定範囲を安全に取得する。文字列長が不足している場合は空白を返却する。</summary>
        /// <param name="source">対象文字列</param>
        /// <param name="startIndex">開始位置</param>
        /// <param name="length">長さ</param>
        /// <returns>指定範囲の文字列</returns>
        public static string SafeSubstring(this string source, int startIndex, int length)
        {
            if (!string.IsNullOrWhiteSpace(source) && (startIndex >= 0 && length > 0))
            {
                if (startIndex >= source.Length)
                {
                    return string.Empty;
                }
                else if (startIndex + length > source.Length)
                {
                    return source.Substring(startIndex, source.Length - startIndex);
                }
                else
                {
                    return source.Substring(startIndex, length);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// インスタンスから部分文字列を取得します。
        /// この部分文字列は、指定した文字位置から開始し、指定した文字数の文字列です。
        /// 全角・半角混在の場合、指定したバイト数より、文字数を取得する
        /// </summary>
        /// <param name="source">源文字列</param>
        /// <param name="startIndex">部分文字列の開始位置のインデックス</param>
        /// <param name="byteLength">要求バイト数</param>
        /// <param name="nextStartIndex">取得した文字最後位置</param>
        /// <returns>System.String 形式</returns>
        public static string SubStringByte(this string source, int startIndex, int byteLength, ref int nextStartIndex)
        {
            var resultLength = 0;
            // とりあえず指定位置から後ろをすべて取り出す
            var result = source.Substring(startIndex);
            var byteCount = result.GetShiftJisByteCount();

            if (byteCount <= byteLength)
            {
                // 文字列のバイト長が指定バイト長以下なら指定位置以降の文字列をそのまま返す
                resultLength = result.Length;
            }
            else
            {
                // 取り出した文字列について1文字ずつバイト数を確認して
                // 実際に取り出す文字数を決定する
                int i = 0;
                for (i = 0; i < result.Length; i++)
                {
                    // 1文字区切れ
                    var charStr = result.Substring(i, 1);
                    // 半角なら1バイト、全角なら2バイト減
                    byteLength -= (charStr.GetShiftJisByteCount() == 1 ? 1 : 2);

                    // 指定バイト長を超えたら終了
                    if (byteLength <= 0)
                    {
                        break;
                    }
                }

                if (byteLength == 0)
                {
                    resultLength = i + 1;
                }
                else if (byteLength < 0)
                {
                    resultLength = i;
                }
            }

            result = source.SafeSubstring(startIndex, resultLength);
            nextStartIndex = startIndex + resultLength;
            return result;
        }

        /// <summary>半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。</summary>
        /// <param name="source">バイト数取得の対象となる文字列。</param>
        /// <returns>半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
        private static int GetShiftJisByteCount(this string source)
        {
            var result = 0;
            if (!string.IsNullOrEmpty(source))
            {
                for (int i = 0; i < source.Length; i++)
                {
                    var subStr = source.Substring(i, 1);

                    if (char.IsSurrogate(subStr[0]))
                    {
                        // Unicodeのサロゲート文字に該当する場合
                        // 「𠮷野家」の「𠮷」等。
                        result += 2;
                        continue;
                    }

                    // 文字のSHIFT-JISバイト配列を取得
                    var bytes = _shiftJis.GetBytes(subStr);

                    // 文字列の総バイト数
                    result += bytes.Length;

                    // 【メモ】↓Shift_JISで表現できない文字だった場合の判定をしている模様
                    // 文字コードは63（3F）で、且つ文字は「?」ではない場合、文字のバイト数＝Length+1
                    if (bytes.Length == 1 && bytes[0] == 63 && subStr != "?")
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
