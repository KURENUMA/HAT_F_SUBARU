using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HatFClient.Common
{
    internal class GuiUtil
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        public const int WM_SETREDRAW = 0x000B;

        /// <remarks>
        /// WinFormが把握している内部状態と矛盾しやすいのでなるべく使用しないでください。
        /// </remarks>
        public static void BeginUpdate(Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 0, 0);
        }

        /// <remarks>
        /// WinFormが把握している内部状態と矛盾しやすいのでなるべく使用しないでください。
        /// </remarks>
        public static void EndUpdate(Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 1, 0);
            control.Refresh();
        }
    }
}
