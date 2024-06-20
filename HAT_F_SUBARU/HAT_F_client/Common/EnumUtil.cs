using System;
using System.ComponentModel;
using System.Reflection;

namespace HatFClient.Common
{
    /// <summary>Enum全般の共通メソッド群</summary>
    internal static class EnumUtil
    {
        /// <summary><see cref="DescriptionAttribute"/>の値を取得する</summary>
        /// <param name="value">Enum値</param>
        /// <returns><see cref="DescriptionAttribute"/>の値</returns>
        public static string GetDescription(Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }
    }
}
