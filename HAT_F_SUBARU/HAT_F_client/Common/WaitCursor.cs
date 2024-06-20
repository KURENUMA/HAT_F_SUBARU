using System.Windows.Forms;

namespace HatFClient.Common
{
    /// <summary>usingブロックでマウスカーソルを変更するクラス</summary>
    internal class WaitCursor : Scope
    {
        /// <summary>変更時のカーソルを記憶</summary>
        private Cursor _prevCursor;

        /// <summary>コンストラクタ</summary>
        public WaitCursor()
        {
            SetCursor(Cursors.WaitCursor);
            OnDispose = RestoreCursor;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="cursor">マウスカーソル</param>
        public WaitCursor(Cursor cursor)
        {
            SetCursor(cursor);
            OnDispose = RestoreCursor;
        }

        /// <summary>マウスカーソルを変更する</summary>
        public void SetCursor(Cursor cursor)
        {
            _prevCursor = Cursor.Current;
            Application.UseWaitCursor = true;
            Cursor.Current = cursor;
        }

        /// <summary>マウスカーソルを元に戻す</summary>
        public void RestoreCursor()
        {
            Cursor.Current = _prevCursor;
            Application.UseWaitCursor = false;
        }
    }
}