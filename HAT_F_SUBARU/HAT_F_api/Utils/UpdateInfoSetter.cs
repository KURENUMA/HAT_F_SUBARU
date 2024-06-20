using AutoMapper.Internal;
using HAT_F_api.CustomModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace HAT_F_api.Utils
{
    /// <summary>
    /// エンティティ更新情報設定
    /// </summary>
    public class UpdateInfoSetter
    {
        private readonly HatFApiExecutionContext _hatFApiExecutionContext;
        private readonly HatFLoginResultAccesser _hatFLoginResultAccesser;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hatFApiExecutionContext">実行コンテキスト</param>
        /// <param name="hatFLoginResultAccesser">ログイン情報取得機能</param>
        public UpdateInfoSetter(HatFApiExecutionContext hatFApiExecutionContext, HatFLoginResultAccesser hatFLoginResultAccesser)
        {
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _hatFLoginResultAccesser = hatFLoginResultAccesser;

#if DEBUG
            if (_hatFApiExecutionContext == null)
            {
                // Swagger実行を想定
                _hatFApiExecutionContext = new HatFApiExecutionContext();
            }

            //if (_hatFLoginResultAccesser.HatFLoginResult == null || _hatFLoginResultAccesser.HatFLoginResult.LoginSucceeded == false)
            //{
            //    // Swagger実行を想定
            //    var loginResult = new HatFLoginResult();
            //    loginResult.LoginSucceeded = true;
            //    loginResult.ErrorMessage = "";
            //    loginResult.EmployeeId = 9999;
            //    loginResult.EmployeeCode = "9999";
            //    loginResult.EmployeeName = "Swagger";
            //    loginResult.EmployeeNameKana = "スワッガー";
            //    loginResult.EmployeeTag = "";
            //    _hatFLoginResult = loginResult;
            //}
#endif
        }

        /// <summary>
        /// DataTableに作成日時、作成者、更新日時、更新者をセットします。作成日時、作成者は既存の値があるとき変更しません。
        /// </summary>
        /// <param name="dataTable"></param>
        public void SetUpdateInfo(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                SetValueIfEmplty("CreateDate", row, _hatFApiExecutionContext.ExecuteDateTimeJst);
                SetValueIfEmplty("CREATE_DATE", row, _hatFApiExecutionContext.ExecuteDateTimeJst);
                SetValueIfEmplty("Creator", row, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
                SetValueIfEmplty("CREATOR", row, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);

                SetValue("UpdateDate", row, _hatFApiExecutionContext.ExecuteDateTimeJst);
                SetValue("UPDATE_DATE", row, _hatFApiExecutionContext.ExecuteDateTimeJst);
                SetValue("Updater", row, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
                SetValue("UPDATER", row, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
            }
        }

        /// <summary>
        /// エンティティにセットする作成/更新日時を取得します。主にIQueryable.ExecuteUpdateAsync()等で使用する想定です。
        /// </summary>
        public DateTime EntityUpdateDateTimeJst 
        { 
            get 
            {
                return _hatFApiExecutionContext.ExecuteDateTimeJst;
            } 
        }

        /// <summary>
        /// エンティティにセットする作成/更新者の社員IDを取得します。主にIQueryable.ExecuteUpdateAsync()等で使用する想定です。
        /// </summary>
        public int EntityUpdaterId
        {
            get
            {
                return _hatFLoginResultAccesser.HatFLoginResult.EmployeeId;
            }
        }

        /// <summary>
        /// 指定項目が(ほぼ)未設定値なら値を設定します(nullや空、DateTime.MinValueなど)
        /// </summary>
        private void SetValueIfEmplty(string columnName, DataRow destination, object value)
        {
            DataColumnCollection columns = destination.Table.Columns;
            if (columns.Contains(columnName))
            {
                object val = destination[columnName];
                if (IsNearlyEmpty(val))
                {
                    destination[columnName] = value;
                }
            }
        }

        /// <summary>
        /// 指定項目に値を設定します
        /// </summary>
        private void SetValue(string columnName, DataRow destination, object value)
        {
            DataColumnCollection columns = destination.Table.Columns;
            if (columns.Contains(columnName))
            {
                destination[columnName] = value;
            }
        }

        /// <summary>
        /// エンティティ(単体)に作成日時、作成者、更新日時、更新者をセットします。作成日時、作成者は既存の値があるとき変更しません。
        /// </summary>
        public void SetUpdateInfo(object model)
        {
            if (model == null) { throw new ArgumentNullException(nameof(model)); }

            Type type = model.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            PropertyInfo createDatePropertyInfo = type.GetProperty("CreateDate", bindingFlags);
            PropertyInfo creatorPropertyInfo = type.GetProperty("Creator", bindingFlags);
            PropertyInfo updateDatePropertyInfo = type.GetProperty("UpdateDate", bindingFlags);
            PropertyInfo updaterPropertyInfo = type.GetProperty("Updater", bindingFlags);

            // 作成日時
            SetValueIfEmpty(createDatePropertyInfo, model, _hatFApiExecutionContext.ExecuteDateTimeJst);

            // 作成者
            SetValueIfEmpty(creatorPropertyInfo, model, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);

            // 更新日時
            SetValue(updateDatePropertyInfo, model, _hatFApiExecutionContext.ExecuteDateTimeJst);

            // 更新者
            SetValue(updaterPropertyInfo, model, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
        }

        /// <summary>
        /// エンティティ(配列系)に作成日時、作成者、更新日時、更新者をセットします。作成日時、作成者は既存の値があるとき変更しません。
        /// </summary>
        public void SetUpdateInfo<T>(IEnumerable<T> models)
        {
            if (models == null) { throw new ArgumentNullException(nameof(models)); }

            Type type = typeof(T);
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            PropertyInfo createDatePropertyInfo = type.GetProperty("CreateDate", bindingFlags);
            PropertyInfo creatorPropertyInfo = type.GetProperty("Creator", bindingFlags);
            PropertyInfo updateDatePropertyInfo = type.GetProperty("UpdateDate", bindingFlags);
            PropertyInfo updaterPropertyInfo = type.GetProperty("Updater", bindingFlags);

            foreach(object  model in models)
            {
                // 作成日時
                SetValueIfEmpty(createDatePropertyInfo, model, _hatFApiExecutionContext.ExecuteDateTimeJst);

                // 作成者
                SetValueIfEmpty(creatorPropertyInfo, model, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);

                // 更新日時
                SetValue(updateDatePropertyInfo, model, _hatFApiExecutionContext.ExecuteDateTimeJst);

                // 更新者
                SetValue(updaterPropertyInfo, model, _hatFLoginResultAccesser.HatFLoginResult.EmployeeId);
            }
        }

        /// <summary>
        /// 指定項目が(ほぼ)未設定値なら値を設定します(nullや空、DateTime.MinValueなど)
        /// </summary>
        private void SetValueIfEmpty<T>(PropertyInfo pi, T destination, object value)
        {
            if (pi != null)
            {
                // 空（新規）なら入れる
                object val = pi.GetValue(destination, null);
                if (IsNearlyEmpty(val))
                {
                    pi.SetValue(destination, value);
                }
            }
        }

        /// <summary>
        /// 指定項目に値を設定します
        /// </summary>
        private void SetValue<T>(PropertyInfo pi, T destination, object value)
        {
            if (pi != null)
            {
                // 値をセット
                pi.SetValue(destination, value);
            }
        }

        /// <summary>
        /// 実質、空とみなせる値かを判定
        /// </summary>
        private bool IsNearlyEmpty(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value == DBNull.Value)
            {
                return true;
            }

            if (value is DateTime)
            {
                return ((DateTime)value == DateTime.MinValue);
            }

            if (value is int)
            {
                return ((int)value == 0);
            }

            if (value is long)
            {
                return ((long)value == 0L);
            }

            if (Microsoft.VisualBasic.Information.IsNumeric(value))
            {
                return ((decimal)value == 0M);
            }

            return string.IsNullOrEmpty(value.ToString());
        }
    }
}
