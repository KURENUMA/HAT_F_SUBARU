using DocumentFormat.OpenXml.VariantTypes;
using HAT_F_api.CustomModels;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Common
{
    /// <summary>
    /// 権限制御
    /// </summary>
    internal class RoleController
    {
        /// <summary>
        /// ユーザーが保持しているロール
        /// </summary>
        private HatFUserRole[] _userRoles;

        /// <summary>
        /// コントロールとコントロールの使用に必要なロールの定義
        /// </summary>
        private Dictionary<Control, HatFUserRole[]> _controlRoles = new Dictionary<Control, HatFUserRole[]>();

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="userRoles">ユーザーに割り当てられたロール(カンマ区切り)</param>
        /// <param name="controlRoles">コントロールの使用に必要なロール</param>
        public RoleController(string userRoles, Dictionary<Control, HatFUserRole[]> controlRoles)
        {
            var query = (userRoles ?? "")
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //.Select(item => (HatFUserRole)Enum.Parse(typeof(HatFUserRole), item.Trim()));

            var roles = new List<HatFUserRole>();
            foreach(var item in query)
            {
                if (Enum.TryParse<HatFUserRole>(item, out HatFUserRole parsed))
                {
                    roles.Add(parsed);
                }
                else 
                {
                    string message = $"ユーザーに割り当てられたロール '{item}' を認識できませんでした。";
                    ApplicationInsightsHelper.TelemetryClient.TrackTrace(message, SeverityLevel.Error);
                }
            }

            _userRoles = roles.ToArray();
            _controlRoles = controlRoles;

            Initialze();
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="userRoles">ユーザーに割り当てられたロール(カンマ区切り)</param>
        /// <param name="controlRoles">コントロールの使用に必要なロール</param>
        public RoleController(HatFUserRole[] userRoles, Dictionary<Control, HatFUserRole[]> controlRoles) 
        {
            _userRoles = userRoles;
            _controlRoles = controlRoles;

            Initialze();
        }

        private void Initialze()
        {
#if DEBUG
            DisplayNecessaryRole();
#endif

            Apply();
        }

        /// <summary>
        /// 任意のタイミングでコントロールのEnabledを再適用します
        /// </summary>
        public void Apply()
        {
            foreach (var role in _controlRoles)
            {
                Control control = role.Key;
                control.EnabledChanged += Control_EnabledChanged;

                if (!HasRole(control))
                {
                    control.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 指定コントロールを使用可能なロールを保持しているかを返します
        /// </summary>
        private bool HasRole(Control control)
        {
            var r = _controlRoles[control];
            bool inRole = false;

            foreach(var userRole in _userRoles)
            {
                if (r.Contains(userRole))
                {
                    inRole = true;
                    break;
                }
            }

            return inRole;
        }

        /// <summary>
        /// コントロールのEnabledが変化したとき、権限が不足していたら押せなくします
        /// </summary>
        private void Control_EnabledChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (!HasRole(control))
            {
                control.Enabled = false;
            }
        }

        /// <summary>
        /// 対象コントロール上にに必要なロールを表示します
        /// </summary>
        private void DisplayNecessaryRole()
        {
            Form form = (Form)_controlRoles.Keys.First().TopLevelControl;
            Font font = new Font(form?.Font.FontFamily ?? new FontFamily("Meiryo UI"), 9f);

            foreach (var role in _controlRoles)
            {
                var label = new System.Windows.Forms.Label();
                label.Location = new System.Drawing.Point(2, 2);
                label.Text = string.Join(", ", role.Value);
                label.ForeColor = System.Drawing.Color.Red;
                label.BackColor = System.Drawing.Color.Transparent;
                label.Font = font;
                label.AutoSize = true;
                label.Click += (s, e) => (role.Key as Button)?.PerformClick();

                var target = role.Key;
                target.Controls.Add(label);
            }
        }
    }
}
