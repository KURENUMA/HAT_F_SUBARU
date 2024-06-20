using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class HatFTheme
    {
        private static readonly string COLOR_MAIN = HatFConfigReader.GetAppSetting("Theme:Color:Main");
        private static readonly string COLOR_AQUA = HatFConfigReader.GetAppSetting("Theme:Color:Aqua");

        /// <summary>
        /// HAT-F メイン色
        /// </summary>
        public static Color MainColor { get; private set; } = ColorUtil.FromHex(COLOR_MAIN);

        /// <summary>
        /// HAT-F 水色
        /// </summary>
        public static Color AquaColor { get; private set; } = ColorUtil.FromHex(COLOR_AQUA);

    }
}
