using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HatFClient.Common
{
    /// <summary>フォーム関連の共通メソッド</summary>
    internal class FormHelper
    {
        /// <summary>再帰してすべてのコントロールを取得する</summary>
        /// <param name="parent">起点</param>
        /// <returns>すべてのコントロール</returns>
        public static IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (var c in parent.Controls.OfType<Control>())
            {
                yield return c;
                foreach (var c2 in GetAllControls(c).OfType<Control>())
                {
                    yield return c2;
                }
            }
        }

        /// <summary>
        /// KeyPressイベントで英文字に入力制限します
        /// </summary>
        public static void KeyPressLimitAlphabet(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            if (e.KeyChar >= 'a' && e.KeyChar <= 'z')
            {
                return;
            }

            if (e.KeyChar >= 'A' && e.KeyChar <= 'Z')
            {
                return;
            }

            e.KeyChar = (char)0;
        }

        /// <summary>
        /// KeyPressイベントで数字に入力制限します
        /// </summary>
        public static void KeyPressLimitNumberChar(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }

            e.KeyChar = (char)0;
        }

        /// <summary>
        /// KeyPressイベントで数字と関連文字に入力制限します
        /// </summary>
        public static void KeyPressLimitNumberCharPlus(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }

            switch (e.KeyChar) 
            {
                case '.':
                case ',':
                case '-':
                    return;
            }

            e.KeyChar = (char)0;
        }

        /// <summary>
        /// KeyPressイベントで数字とハイフンに入力制限します
        /// </summary>
        public static void KeyPressLimitNumberHyphenChar(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            if (e.KeyChar == '-') 
            {
                return;
            }

            // 半角長音記号
            if (e.KeyChar == 'ｰ')
            {
                e.KeyChar = '-';
                return;
            }

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return;
            }

            e.KeyChar = (char)0;
        }

        /// <summary>
        /// KeyPressイベントで半角文字に入力制限します
        /// </summary>
        public static void KeyPressLimitNallowChar(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            if (e.KeyChar >= ' ' && e.KeyChar <= 255)
            {
                return;
            }

            e.KeyChar = (char)0;
        }

        /// <summary>
        /// KeyPressイベントで英大文字化します
        /// </summary>
        public static void KeyPressToUpperCase(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            char c = Strings.StrConv(e.KeyChar.ToString(), VbStrConv.Uppercase).First();
            e.KeyChar = c;
        }

        /// <summary>
        /// KeyPressイベントで英小文字化します
        /// </summary>
        public static void KeyPressToLowercase(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            char c = Strings.StrConv(e.KeyChar.ToString(), VbStrConv.Lowercase).First();
            e.KeyChar = c;
        }

        /// <summary>
        /// KeyPressイベントで半角文字に変換します
        /// </summary>
        public static void KeyPressToNallowChar(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            char c = Strings.StrConv(e.KeyChar.ToString(), VbStrConv.Narrow).First();
            e.KeyChar = (char)c;
        }

        /// <summary>
        /// KeyPressイベントで全角文字に変換します
        /// </summary>
        public static void KeyPressToWideChar(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < ' ') { return; }

            char c = Strings.StrConv(e.KeyChar.ToString(), VbStrConv.Wide).First();
            e.KeyChar = (char)c;
        }

        /// <summary>
        /// 郵便番号用制限をかけます
        /// </summary>
        public static void KeyPressForZipCode(object sender, KeyPressEventArgs e)
        {
            KeyPressToNallowChar(sender, e);
            KeyPressLimitNumberHyphenChar(sender, e);
        }

        /// <summary>
        /// 電話/FAX番号用制限をかけます
        /// </summary>
        public static void KeyPressForTelFax(object sender, KeyPressEventArgs e)
        {
            KeyPressToNallowChar(sender, e);
            KeyPressLimitNumberHyphenChar(sender, e);
        }

        /// <summary>
        /// 数値項目用制限をかけます
        /// </summary>
        public static void KeyPressForNumber(object sender, KeyPressEventArgs e)
        {
            KeyPressToNallowChar(sender, e);
            KeyPressLimitNumberChar(sender, e);
        }

        /// <summary>
        /// 数値と関連記号制限をかけます
        /// </summary>
        public static void KeyPressForNumberPlus(object sender, KeyPressEventArgs e)
        {
            KeyPressToNallowChar(sender, e);
            KeyPressLimitNumberCharPlus(sender, e);
        }

        /// <summary>
        /// 英大文字(半角)用制限をかけます
        /// </summary>
        public static void KeyPressForUpperAlpha(object sender, KeyPressEventArgs e)
        {
            KeyPressToNallowChar(sender, e);
            KeyPressToUpperCase(sender, e);
        }

        /// <summary>
        /// カナ名称用制限をかけます
        /// </summary>
        public static void KeyPressForKanaName(object sender, KeyPressEventArgs e)
        {
            KeyPressToWideChar(sender, e);
        }

        /// <summary>
        /// メールアドレス用制限()をかけます
        /// </summary>
        public static void KeyPressForEmail(object sender, KeyPressEventArgs e)
        {
            // RFC準拠は難しいので一旦半角入力のみ対応
            KeyPressToNallowChar(sender, e);
            KeyPressLimitNallowChar(sender, e);
        }
    }
}
