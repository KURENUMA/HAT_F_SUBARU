using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class ColorUtil
    {
        private const string RGB_PATTERN = "^#(?<r>[0-9A-Fa-f]{2})(?<g>[0-9A-Fa-f]{2})(?<b>[0-9A-Fa-f]{2})$";
        private const string ARGB_PATTERN = "^#(?<a>[0-9A-Fa-f]{2})(?<r>[0-9A-Fa-f]{2})(?<g>[0-9A-Fa-f]{2})(?<b>[0-9A-Fa-f]{2})$";

        /// <summary>
        /// 16進数表記からColorオブジェクトを取得する
        /// </summary>
        /// <param name="hex">(A)RGB を表す16進数表記</param>
        /// <returns></returns>
        public static Color FromHex(string hex)
        {
            int[] argb = { 255, 255, 255, 255 };

            if (Regex.IsMatch(hex, RGB_PATTERN))
            {
                Match m = Regex.Match(hex, RGB_PATTERN);
                argb[1] = Convert.ToInt32(m.Groups["r"].Value, 16);
                argb[2] = Convert.ToInt32(m.Groups["g"].Value, 16);
                argb[3] = Convert.ToInt32(m.Groups["b"].Value, 16);
            }
            else if (Regex.IsMatch(hex, ARGB_PATTERN))
            {
                Match m = Regex.Match(hex, ARGB_PATTERN);
                argb[0] = Convert.ToInt32(m.Groups["a"].Value, 16);
                argb[1] = Convert.ToInt32(m.Groups["r"].Value, 16);
                argb[2] = Convert.ToInt32(m.Groups["g"].Value, 16);
                argb[3] = Convert.ToInt32(m.Groups["b"].Value, 16);
            }

            return Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);
        }

    }
}
