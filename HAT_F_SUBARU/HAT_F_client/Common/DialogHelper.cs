using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Common
{
    internal class DialogHelper
    {
        private static readonly string DefaultTitle = "SUBARU";

        public static bool IsPositiveResult(DialogResult dialogResult) 
        { 
            switch (dialogResult)
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                case DialogResult.Retry:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsNegativeResult(DialogResult dialogResult)
        {
            bool isPositive = IsPositiveResult(dialogResult);
            return !isPositive;
        }

        /// <summary>
        /// 一般的な通知メッセージ
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        public static void InformationMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 軽微なエラーを表すメッセージ（入力エラー、通信エラー）
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        public static void WarningMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 必須項目の入力を促すメッセージ
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="itemName">必須項目名。メッセージ全体ではなく項目名のみ指定します。</param>
        public static void InputRequireMessage(IWin32Window owner, string itemName)
        {
            string message = $"{itemName}を入力してください。";
            MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ユーザーに「はい」「いいえ」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        public static DialogResult YesNoQuestionForDialogResult(IWin32Window owner, string message)
        {
            return YesNoQuestionForDialogResult(owner, message, false);
        }

        /// <summary>
        /// ユーザーに「はい」「いいえ」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「いいえ」をデフォルトボタンにする場合trueを指定します</param>
        public static DialogResult YesNoQuestionForDialogResult(IWin32Window owner, string message, bool negativeDefault)
        {
            MessageBoxDefaultButton defaultButton = negativeDefault ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1;
            return MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton);
        }

        /// <summary>
        /// ユーザーに「はい」「いいえ」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <returns>ユーザーが肯定的な回答をした場合trueを返します</returns>
        public static bool YesNoQuestion(IWin32Window owner, string message)
        {
            return DialogHelper.IsPositiveResult(YesNoQuestionForDialogResult(owner, message));
        }

        /// <summary>
        /// ユーザーに「はい」「いいえ」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「いいえ」をデフォルトボタンにする場合trueを指定します</param>
        /// <returns></returns>
        public static bool YesNoQuestion(IWin32Window owner, string message, bool negativeDefault)
        {
            return DialogHelper.IsPositiveResult(YesNoQuestionForDialogResult(owner, message, negativeDefault));
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        public static DialogResult OkCancelQuestionForDialogResult(IWin32Window owner, string message)
        {
            return OkCancelQuestionForDialogResult(owner, message, false);
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「キャンセル」をデフォルトボタンにする場合trueを指定します</param>
        public static DialogResult OkCancelQuestionForDialogResult(IWin32Window owner, string message, bool negativeDefault)
        {
            MessageBoxDefaultButton defaultButton = negativeDefault ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1;
            return MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, defaultButton);
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <returns>ユーザーが肯定的な回答をした場合trueを返します</returns>
        public static bool OkCancelQuestion(IWin32Window owner, string message)
        {
            return DialogHelper.IsPositiveResult(OkCancelQuestionForDialogResult(owner, message));
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「キャンセル」をデフォルトボタンにする場合trueを指定します</param>
        /// <returns>ユーザーが肯定的な回答をした場合trueを返します</returns>
        public static bool OkCancelQuestion(IWin32Window owner, string message, bool negativeDefault)
        {
            return DialogHelper.IsPositiveResult(OkCancelQuestionForDialogResult(owner, message, negativeDefault));
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。（警告アイコン）
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        public static DialogResult OkCancelWarningForDialogResult(IWin32Window owner, string message)
        {
            return OkCancelWarningForDialogResult(owner, message, false);
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。（警告アイコン）
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「キャンセル」をデフォルトボタンにする場合trueを指定します</param>
        public static DialogResult OkCancelWarningForDialogResult(IWin32Window owner, string message, bool negativeDefault)
        {
            MessageBoxDefaultButton defaultButton = negativeDefault ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1;
            return MessageBox.Show(owner, message, DefaultTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, defaultButton);
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。（警告アイコン）
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <returns>ユーザーが肯定的な回答をした場合trueを返します</returns>
        public static bool OkCancelWarning(IWin32Window owner, string message)
        {
            return DialogHelper.IsPositiveResult(OkCancelWarningForDialogResult(owner, message));
        }

        /// <summary>
        /// ユーザーに「OK」「キャンセル」で問い合わせます。（警告アイコン）
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="negativeDefault">「キャンセル」をデフォルトボタンにする場合trueを指定します</param>
        /// <returns>ユーザーが肯定的な回答をした場合trueを返します</returns>
        public static bool OkCancelWarning(IWin32Window owner, string message, bool negativeDefault)
        {
            return DialogHelper.IsPositiveResult(OkCancelWarningForDialogResult(owner, message, negativeDefault));
        }

        /// <summary>
        /// フォームを閉じる確認メッセージ 
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        public static bool FormClosingConfirm(IWin32Window owner)
        {
            string message = "画面を閉じてよろしいですか?";
            return YesNoQuestion(owner, message, true);    // Noをデフォルト
        }

        /// <summary>
        /// 検索確認メッセージ (検索してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <remarks>
        /// 通常、検索前の確認はしません。特定状況で必要な場合にだけ使用してください。
        /// </remarks>
        public static bool SearchingConfirm(IWin32Window owner)
        {
            return SearchingConfirm(owner, "");
        }

        /// <summary>
        /// 検索確認メッセージ ([itenName]を検索してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <remarks>
        /// 通常、検索前の確認はしません。特定状況で必要な場合にだけ使用してください。
        /// </remarks>
        public static bool SearchingConfirm(IWin32Window owner, string itemName)
        {
            string message = string.IsNullOrEmpty(itemName)
                ? $"検索してよろしいですか?"
                : $"{itemName}を検索してよろしいですか?";
            return YesNoQuestion(owner, message);
        }

        /// <summary>
        /// 保存確認メッセージ (保存してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        public static bool SaveItemConfirm(IWin32Window owner)
        {
            return SaveItemConfirm(owner, "");
        }

        /// <summary>
        /// 保存確認メッセージ ([itenName]を保存してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="itemName">保存確認する対象の名前</param>
        public static bool SaveItemConfirm(IWin32Window owner, string itemName)
        {
            string message = string.IsNullOrEmpty(itemName)
                ? $"保存してよろしいですか?"
                : $"{itemName}を保存してよろしいですか?";
            return YesNoQuestion(owner, message);
        }

        /// <summary>
        /// 削除確認メッセージ (削除してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        public static bool DeleteItemConfirm(IWin32Window owner)
        {
            return DeleteItemConfirm(owner, "");
        }

        /// <summary>
        /// 削除確認メッセージ ([itenName]を削除してよろしいですか?)
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        public static bool DeleteItemConfirm(IWin32Window owner, string itemName)
        {
            string message = string.IsNullOrEmpty(itemName)
                ? $"削除してよろしいですか?"
                : $"{itemName}を削除してよろしいですか?";
            return YesNoQuestion(owner, message, true);    // Noをデフォルト
        }

        /// <summary>
        /// 検索結果0件を示すメッセージ表示
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        public static void SearchNoResultMessage(IWin32Window owner) 
        {
            InformationMessage(owner, "条件に一致するデータが見つかりませんでした。");
        }
    }
}
