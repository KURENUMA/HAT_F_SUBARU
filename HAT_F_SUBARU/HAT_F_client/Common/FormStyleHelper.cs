using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HatFClient.Common
{
    internal class FormStyleHelper
    {
        public static void SetDefaultColor(Form form)
        {
            var controls = GetAllControls(form);

            foreach(Control control in controls)
            {
                if (control is Form || control is Label || control is CheckBox)
                {
                    // TODO: 暫定でアリスブルーよりやや濃青で背景を設定
                    // ステークホルダとの調整後に色を正式決定及び関数化などを実施する
                    form.BackColor = HatFTheme.AquaColor;
                    form.ForeColor = SystemColors.ControlText;
                    //form.BackColor = Color.FromArgb(239, 248, 255);
                    //form.BackColor = SystemColors.Control;
                    //form.ForeColor = SystemColors.ControlText;
                }
                else if (control is TextBox)
                {
                    form.BackColor = SystemColors.Window;
                    form.ForeColor = SystemColors.ControlText;
                }
                else if (control is Button)
                {
                    form.BackColor = SystemColors.Control;
                    form.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    form.BackColor = SystemColors.Control;
                    form.ForeColor = SystemColors.ControlText;
                }
            }
        }

        public static void SetFixedSizeDialogStyle(Form form)
        {
            SetFixedSizeDialogStyle(form, false);
        }

        public static void SetFixedSizeDialogStyle(Form form, bool shwoInTaskbar)
        {
            form.ControlBox = true;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowInTaskbar = shwoInTaskbar;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ShowIcon = false;
            form.StartPosition = FormStartPosition.CenterParent;
            // TODO アイコンの設定は一旦とりやめ
            //form.Icon = Properties.Resources.SUBARU;

            if (bool.TryParse(HatFConfigReader.GetAppSetting("Theme:Enabled"), out bool themaEnabled))
            {
                if (themaEnabled)
                {
                    // TODO: 暫定でアリスブルーよりやや濃青で背景を設定
                    // ステークホルダとの調整後に色を正式決定及び関数化などを実施する
                    //form.BackColor = Color.FromArgb(239, 248, 255);
                    form.BackColor = HatFTheme.AquaColor;
                }
            }
        }

        public static void SetResizableDialogStyle(Form form)
        {
            SetResizableDialogStyle(form, false);
        }

        public static void SetResizableDialogStyle(Form form, bool shwoInTaskbar)
        {
            form.ControlBox = true;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowInTaskbar = shwoInTaskbar;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.ShowIcon = false;
            // TODO アイコンの設定は一旦とりやめ
            //form.Icon = Properties.Resources.SUBARU;

            if (bool.TryParse(HatFConfigReader.GetAppSetting("Theme:Enabled"), out bool themaEnabled))
            {
                if (themaEnabled)
                {
                    // TODO: 暫定でアリスブルーよりやや濃青で背景を設定
                    // ステークホルダとの調整後に色を正式決定及び関数化などを実施する
                    //form.BackColor = Color.FromArgb(239, 248, 255);
                    form.BackColor = HatFTheme.AquaColor;
                }
            }
        }

        public static void SetWorkWindowStyle(Form form)
        {
            SetWorkWindowStyle(form, true);
        }

        public static void SetWorkWindowStyle(Form form, bool shwoInTaskbar)
        {
            form.ControlBox = true;
            form.MaximizeBox = true;
            form.MinimizeBox = true;
            form.ShowInTaskbar = shwoInTaskbar;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.ShowIcon = true;
            // TODO アイコンの設定は一旦とりやめ
            //form.Icon = Properties.Resources.SUBARU;
            form.StartPosition = FormStartPosition.WindowsDefaultLocation;
            if (bool.TryParse(HatFConfigReader.GetAppSetting("Theme:Enabled"), out bool themaEnabled))
            {
                if (themaEnabled)
                {
                    // TODO: 暫定でアリスブルーよりやや濃青で背景を設定
                    // ステークホルダとの調整後に色を正式決定及び関数化などを実施する
                    //form.BackColor = Color.FromArgb(239, 248, 255);
                    form.BackColor = HatFTheme.AquaColor;
                }
            }
        }

        public static void SetDefaultFont(Form form)
        {
            form.AutoScaleMode = AutoScaleMode.Dpi;

            List<Control> controls = GetAllControls(form);

            foreach (var ctrl in controls)
            {
                System.Drawing.Font previousFont = ctrl.Font;
                ctrl.Font = new System.Drawing.Font("Meiryo UI", previousFont.Size, previousFont.Style);
                previousFont?.Dispose();
            }
        }

        private static List<Control> GetAllControls(Control parentControl)
        {
            var controls = new List<Control>();
            controls.Add(parentControl);
            foreach (Control childControl in parentControl.Controls)
            {
                controls.AddRange(GetAllControls(childControl));
            }
            return controls;
        }
    }
}
