using C1.Win.C1FlexGrid;
using System;

namespace HatFClient.Common
{
    /// <summary>グリッド関連の共通メソッド群</summary>
    internal class GridHelper
    {
        /// <summary>列を名前で検索し、存在する場合のみ指定処理を実行する</summary>
        /// <param name="grid">グリッド</param>
        /// <param name="columnName">列名</param>
        /// <param name="action">処理内容</param>
        public static void ColumnAction(C1FlexGrid grid, string columnName, Action<Column> action)
        {
            if (grid.Cols.Contains(columnName))
            {
                action(grid.Cols[columnName]);
            }
        }
    }
}
