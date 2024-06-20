using C1.Win.C1FlexGrid;
using HAT_F_api.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HatFClient.Common
{
    internal class GridStyleHelper
    {
        public static void SetReadOnlyColumnColor(C1FlexGrid grid)
        {
            // 編集不可項目背景色
            Color readOnlyItemColor = SystemColors.Control;

            foreach (C1.Win.C1FlexGrid.Column col in grid.Cols)
            {
                if (col.Index == 0) { continue; }

                if (col.AllowEditing == false)
                {
                    if (col.Style == null)
                    {
                        // デフォルト編集不可
                        var readOnlyStyle = grid.Styles.Add($"__ReadOnlyStyle_{col.Name}__");
                        readOnlyStyle.BackColor = readOnlyItemColor;
                        col.Style = readOnlyStyle;
                    }
                    else
                    {
                        col.Style.BackColor = readOnlyItemColor;
                    }
                }
            }

        }

        public static void SetRowModifiedStyle<T>(C1FlexGrid grid, Func<T, bool> editedChecker)
        {
            if (!grid.Styles.Contains("__EditedRowStyle__"))
            {
                var style = grid.Styles.Add("__EditedRowStyle__");
                style.BackColor = Color.LightYellow;
            }

            if (!grid.Styles.Contains("__NoEditedRowStyle__"))
            {
                var style = grid.Styles.Add("__NoEditedRowStyle__");
                style.BackColor = SystemColors.Window;
            }

            var editedStyle = grid.Styles["__EditedRowStyle__"];
            var nonEditedStyle = grid.Styles["__NoEditedRowStyle__"];

            for (int i = 1; i < grid.Rows.Count; i++)
            {
                bool edited = editedChecker((T)grid.Rows[i].DataSource);
                grid.Rows[i].Style = edited ? editedStyle : nonEditedStyle;
            }

        }

        /// <summary>
        /// 列スタイル定義を表します
        /// </summary>
        public enum GridColumnStyleEnum
        {
            /// <summary>
            /// 通貨
            /// </summary>
            Currency,

            /// <summary>
            /// パーセント
            /// </summary>
            Percent,

            /// <summary>
            /// パーセント、小数点以下２桁
            /// </summary>
            Percent2,

            /// <summary>#,##0.00%で表示する。<see cref="Percent"/>と異なり100倍しない</summary>
            SimplePercent,

            /// <summary>日付</summary>
            Date,
        }

        /// <summary>
        /// 列スタイル摘要
        /// </summary>
        /// <param name="style"></param>
        /// <param name="cols"></param>
        public static void SetColumnStyle(GridColumnStyleEnum style, params Column[] cols)
        {
            foreach (var c in cols)
            {
                switch (style)
                {
                    case GridColumnStyleEnum.Currency:
                        c.Format = "C0";
                        c.TextAlign = TextAlignEnum.RightCenter;
                        break;
                    case GridColumnStyleEnum.Percent:
                        c.DataType = typeof(double);
                        c.Format = "0%";
                        c.TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    case GridColumnStyleEnum.Percent2:
                        c.DataType = typeof(double);
                        c.Format = "P2";
                        c.TextAlign = TextAlignEnum.RightCenter;
                        break;
                    case GridColumnStyleEnum.Date:
                        c.DataType = typeof(DateTime);
                        c.Format = "yy/MM/dd";
                        c.TextAlign = TextAlignEnum.LeftCenter;
                        break;
                    case GridColumnStyleEnum.SimplePercent:
                        c.DataType = typeof(double);
                        c.Format = "#,##0.00\\%";
                        c.TextAlign = TextAlignEnum.LeftCenter;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
