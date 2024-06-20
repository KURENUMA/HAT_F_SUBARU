using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class DataHelper
    {
        /// <summary>
        /// DataTableを任意の型のListに変換します。DataTable列名と変換先プロパティ名が一致する項目のみデータがコピーされます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            if (dataTable == null) { throw new ArgumentNullException(); }

            var list = new List<T>();

            Type type = typeof(T);
            Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();
            foreach (DataColumn column in dataTable.Columns)
            {
                var pi = type.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                props.Add(column.ColumnName, pi);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();

                foreach (string key in props.Keys)
                {
                    PropertyInfo pi = props[key];
                    if (pi != null)
                    {
                        pi.SetValue(item, DbNullToClrNull(row[key]));
                    }
                }

                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// DBNull.Valueの場合にnullで返します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static object DbNullToClrNull(object val)
        {
            if (val == DBNull.Value) { return null; }
            return val;
        }


        /// <summary>
        /// DBNull.Valueの可能性がある値を含めてbool型に変換します。null/DBNullは false で返します。
        /// </summary>
        public static bool ToBool(object val)
        {
            if (val == null) return false;
            if (val == DBNull.Value) return false;

            if (bool.TryParse(val.ToString(), out bool result))
            {
                return result;
            }

            return !string.IsNullOrWhiteSpace(val.ToString());
        }


        /// <summary>
        /// DBNull.Valueの可能性がある値を含めてstring型に変換します。null/DBNullは "" で返します。
        /// </summary>
        public static string ToString(object val)
        {
            return ToString(val, "");
        }

        /// <summary>
        /// DBNull.Valueの可能性がある値を含めてstring型に変換します。null/DBNullは "" で返します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <param name="nullAlternateSting">DBNull.Valueかnullだった場合に返却する文字列</param>
        /// <returns></returns>
        public static string ToString(object value, string nullAlternateSting)
        {
            if (value == DBNull.Value) { return nullAlternateSting; }
            if (value == null) { return nullAlternateSting; }
            return value.ToString();
        }

        /// <summary>
        /// 文字列をSQL向けにエスケープします(SQL ServerかDataTable想定)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks>\マーク等はエスケープしません</remarks>
        public static string ToSqlString(string str)
        {
            string sqlString = str ?? "";
            sqlString = sqlString.Replace("'", "''");   //SQL Server、DataTableはこれだけエスケープすればOK
            return sqlString;
        }
    }
}
