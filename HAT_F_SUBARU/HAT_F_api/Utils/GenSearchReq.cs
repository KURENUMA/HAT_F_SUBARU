
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace HAT_F_api.Utils
{
    public enum CombinationTypes
    {
        AND, OR
    }
    public record GenSearchProperty
    {
        public string PropertyName { get; set; } = null!;
        public string Operator { get; set; } = null!;
        public object Value { get; set; } = null!;
        public string GroupKey { get; set; } = null!;
    }

    public class GenSearchItem
    {
        [System.Text.Json.Serialization.JsonPropertyName("Item1")]
        public CombinationTypes CombinationType { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("Item2")]
        public GenSearchProperty Property { get; set; } = null!;
    }

    public class GenSearchDateBetweenItem
    {
        public string Item1 { get; set; } = null!;
        public string Item2 { get; set; } = null!;
    }

    public static class GenSearchUtil
    {
        private static Func<string, object, string> _contains = (string propertyName, object value) =>
        {
            return $"{propertyName}.Contains(\"{EscapeString(value)}\")";
        };
        private static Func<string, object, string> _doesNotContain = (string propertyName, object value) =>
        {
            return $"!{propertyName}.Contains(\"{EscapeString(value)}\")";
        };
        private static Func<string, object, string> _startsWith = (string propertyName, object value) =>
        {
            return $"{propertyName}.StartsWith(\"{EscapeString(value)}\")";
        };
        private static Func<string, object, string> _endsWith = (string propertyName, object value) =>
        {
            return $"{propertyName}.EndsWith(\"{EscapeString(value)}\")";
        };
        
        private static Func<string, object, string> _equals = (string propertyName, object value) =>
        {
            if (value is JsonElement)
            {
                JsonElement je = (JsonElement)value;
                if (je.ValueKind == JsonValueKind.String)
                {
                    return propertyName + " = \"" + EscapeString(je.ToString()) + "\"";
                }
            }

            return propertyName + " = " + value;
        };

        private static string EscapeString(object value)
        {
            return EscapeString($"{value}");
        }

        private static string EscapeString(string stringValue) 
        {
            string escaped = stringValue;
            escaped = escaped.Replace("\\", "\\\\");
            escaped = escaped.Replace("\"", "\"\"");
            return escaped;
        }

        private static Func<string, object, string> _notEquals = (string propertyName, object value) =>
        {
            return propertyName + " != " + value;
        };
        private static Func<string, object, string> _greaterThan = (string propertyName, object value) =>
        {
            return propertyName + " > " + value;
        };
        private static Func<string, object, string> _greaterThanOrEqual = (string propertyName, object value) =>
        {
            return propertyName + " >= " + value;
        };
        private static Func<string, object, string> _lessThan = (string propertyName, object value) =>
        {
            return propertyName + " < " + value;
        };
        private static Func<string, object, string> _lessThanOrEqual = (string propertyName, object value) =>
        {
            return propertyName + " <= " + value;
        };
        private static string getDateTimeValue(object value)
        {
            DateTime dateValue;
            if (value is string)
            {
                dateValue = DateTime.Parse((string)value);
            }
            else
            {
                ((JsonElement)value).TryGetDateTime(out dateValue);
            }
            return dateValue.Date.ToString("yyyy-MM-dd");
        }
        private static Func<string, object, string> _dateEquals = (string propertyName, object value) =>
        {
            return propertyName + " = \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateNotEquals = (string propertyName, object value) =>
        {
            return propertyName + " != \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateGreaterThan = (string propertyName, object value) =>
        {
            return propertyName + " > \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateGreaterThanOrEqual = (string propertyName, object value) =>
        {
            return propertyName + " >= \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateLessThan = (string propertyName, object value) =>
        {
            return propertyName + " < \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateLessThanOrEqual = (string propertyName, object value) =>
        {
            return propertyName + " <= \"" + getDateTimeValue(value) + "\"";
        };
        private static Func<string, object, string> _dateBetween = (string propertyName, object value) =>
        {
            var between = ((JsonElement)value).Deserialize<GenSearchDateBetweenItem>();
            if (between == null)
            {
                throw new System.Exception("Invalid date between value: " + value);
            }
            return "(" + _dateGreaterThanOrEqual(propertyName, between.Item1) + " AND " + _dateLessThanOrEqual(propertyName, between.Item2) + ")";
        };
        private static Dictionary<string, Func<string, object, string>> stringConverters = new Dictionary<string, Func<string, object, string>> {
          { "含む", _contains },
          { "含まない", _doesNotContain },
          { "前方一致", _startsWith },
          { "後方一致", _endsWith }
        };
        private static Dictionary<string, Func<string, object, string>> compConverters = new Dictionary<string, Func<string, object, string>> {
          { "=", _equals },
          { "≠", _notEquals },
          { ">", _greaterThan },
          { ">=", _greaterThanOrEqual },
          { "<", _lessThan },
          { "<=", _lessThanOrEqual},
        };
        private static Dictionary<string, Func<string, object, string>> dateConverters = new Dictionary<string, Func<string, object, string>> {
          { "間", _dateBetween},
          { "=", _dateEquals },
          { "≠", _dateNotEquals },
          { ">", _dateGreaterThan },
          { ">=", _dateGreaterThanOrEqual },
          { "<", _dateLessThan },
          { "<=", _dateLessThanOrEqual},
        };

        private static Func<string, object, string> getConverter(GenSearchItem searchItem)
        {
            if (stringConverters.ContainsKey(searchItem.Property.Operator))
            {
                return stringConverters[searchItem.Property.Operator];
            }
            else
            {
                var isDate = searchItem.Property.Operator == "間" ||
                  ((JsonElement)searchItem.Property.Value).ValueKind == JsonValueKind.String && ((JsonElement)searchItem.Property.Value).TryGetDateTime(out _);
                if (isDate && dateConverters.ContainsKey(searchItem.Property.Operator))
                {
                    return dateConverters[searchItem.Property.Operator];
                }
                else if (compConverters.ContainsKey(searchItem.Property.Operator))
                {
                    return compConverters[searchItem.Property.Operator];
                }
            }
            throw new System.Exception("Invalid operator: " + searchItem.Property.Operator);
        }

        /// <remarks>
        /// 【重要】この機能はセキュリティを考慮して作成されていません。問題は発見次第修正しますが安全は担保できません。(SQLインジェクションに類する危険性を内包します)
        /// </remarks>
        public static string CreateConditionSql(List<GenSearchItem> searchItems)
        {
            string searchReq = "";
            foreach (GenSearchItem searchItem in searchItems)
            {
                if (searchItem.CombinationType == CombinationTypes.AND && searchReq != "")
                {
                    searchReq += " AND ";
                }
                else if (searchItem.CombinationType == CombinationTypes.OR)
                {
                    searchReq += " OR ";
                }
                var converter = getConverter(searchItem);
                searchReq += converter(searchItem.Property.PropertyName, searchItem.Property.Value);
            }
            return searchReq;
        }

        /// <summary>
        /// 検索条件を受け取り、検索結果を返却する
        /// </summary>
        /// <typeparam name="T">検索対象モデル</typeparam>
        /// <param name="dbSet">検索対象</param>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns></returns>
        public static IQueryable<T> DoGenSearch<T>(DbSet<T> dbSet, List<GenSearchItem> searchItems) where T : class
        {
            if (searchItems == null || searchItems.Count == 0)
            {
                return dbSet;
            }

            var sql = GenSearchUtil.CreateConditionSql(searchItems);
            // Console.WriteLine(sql);
            return dbSet.Where(sql);
        }

        /// <summary>
        /// ページング用の条件付与
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">ページングを付与するIQueryableオブジェクト</param>
        /// <param name="rows">最大取得件数(ページサイズ)</param>
        /// <param name="pageFrom1">取得ページ番号(1開始)</param>
        /// <returns></returns>
        public static IQueryable<T> AddPaging<T>(IQueryable<T> query, int rows, int pageFrom1)
        {
            IQueryable<T> pagedQuery = query.Skip(rows * (pageFrom1 - 1)).Take(rows);
            System.Diagnostics.Debug.WriteLine(pagedQuery.ToQueryString());
            return pagedQuery;
        }

        ///// <summary>
        ///// ページング用の条件付与
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="query">ページングを付与するIQueryableオブジェクト</param>
        ///// <param name="rows">最大取得件数(ページサイズ)</param>
        ///// <param name="pageFrom1">取得ページ番号(1開始)</param>
        ///// <returns></returns>
        //public static IQueryable<T> AddPaging<T>(IOrderedQueryable<T> query, int rows, int pageFrom1)
        //{
        //    IQueryable<T> pagedQuery = query.Skip(rows * (pageFrom1 - 1)).Take(rows);
        //    System.Diagnostics.Debug.WriteLine(pagedQuery.ToQueryString());
        //    return pagedQuery;
        //}

        /// <remarks>
        /// 【重要】この機能はセキュリティを考慮して作成されていません。問題は発見次第修正しますが安全は担保できません。(SQLインジェクションに類する危険性を内包します)
        /// </remarks>
        public static string CreateConditionSqlConsideringGroupKey(List<GenSearchItem> searchItems)
        {
            var groupedItems = searchItems.GroupBy(item => item.Property.GroupKey);
            string searchReq = "";

            foreach (var group in groupedItems)
            {
                string groupCondition = "";
                foreach (GenSearchItem searchItem in group)
                {
                    if (groupCondition != "")
                    {
                        groupCondition += searchItem.CombinationType == CombinationTypes.AND ? " AND " : " OR ";
                    }
                    var converter = getConverter(searchItem);
                    groupCondition += converter(searchItem.Property.PropertyName, searchItem.Property.Value);
                }

                if (searchReq != "")
                {
                    searchReq += group.First().CombinationType == CombinationTypes.AND ? " AND " : " OR ";
                }
                searchReq += "(" + groupCondition + ")";
            }

            return searchReq;
        }

        /// <summary>
        /// 検索条件を受け取り、検索結果を返却する
        /// </summary>
        /// <typeparam name="T">検索対象モデル</typeparam>
        /// <param name="dbSet">検索対象</param>
        /// <param name="searchItems">検索条件リスト</param>
        /// <returns></returns>
        public static IQueryable<T> DoGenSearchConsideringGroupKey<T>(DbSet<T> dbSet, List<GenSearchItem> searchItems) where T : class
        {
            if (searchItems == null || searchItems.Count == 0)
            {
                return dbSet;
            }

            var sql = GenSearchUtil.CreateConditionSqlConsideringGroupKey(searchItems);
            // Console.WriteLine(sql);
            return dbSet.Where(sql);
        }
    }
}