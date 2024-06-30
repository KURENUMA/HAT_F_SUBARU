using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatFClient.Views.Search
{
    internal class FilterHelper
    {
        /// <summary>検索条件を文字列化</summary>
        /// <param name="filterOptions">検索条件</param>
        /// <returns>検索条件文字列</returns>
        public static string CreateFilterOptionStr(List<(FilterCombinationTypes, FilterOption, string)> filterOptions)
        {
            return CreateFilterOptionStr(filterOptions, null);
        }

        /// <summary>検索条件を文字列化</summary>
        /// <param name="filterOptions">検索条件</param>
        /// <param name="dropDownItems">検索条件の選択肢</param>
        /// <returns>検索条件文字列</returns>
        public static string CreateFilterOptionStr(
            List<(FilterCombinationTypes, FilterOption, string)> filterOptions,
            IEnumerable<SearchDropDownInfo> dropDownItems)
        {
            var sb = new StringBuilder();
            var cnt = 0;
            foreach (var f in filterOptions)
            {
                if (cnt != 0)
                {
                    sb.Append(f.Item1.ToString());
                }

                var itemName = f.Item3;
                var itemOperator = f.Item2.Operator;
                object itemValue = string.Empty;
                if(f.Item2.Operator == "間")
                {
                    var (from, to) = ((DateTime, DateTime))f.Item2.Value;
                    itemValue = $"{from:yyyy/MM/dd}～{to:yyyy/MM/dd}";
                }
                else
                {
                    itemValue = (f.Item2.Value is DateTime dt) ? dt.ToString("yyyy/MM/dd") : f.Item2.Value;
                }

                if (dropDownItems != null)
                {
                    var dropDownDefine = dropDownItems.Where(x => x.FieldName == f.Item2.PropertyName).SingleOrDefault();
                    if (dropDownDefine != null)
                    {
                        var dropDownItem = dropDownDefine.DropDownItems.Where(x => x.Value == itemValue.ToString()).Single();
                        itemValue = dropDownItem.Key;
                    }
                }

                sb.AppendLine($" 項目名：「{itemName}」 条件：「{itemOperator}」 値：「{itemValue}」");

                cnt++;
            }
            return sb.ToString();
        }
    }
}
