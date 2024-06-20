using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace HatFClient.Common
{
    /// <summary>各種モデルクラスのヘルパー</summary>
    /// <typeparam name="TModel">モデルの型</typeparam>
    internal class ModelHelper<TModel>
    {
        /// <summary><see cref="DisplayNameAttribute"/>属性の値を取得</summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns><see cref="DisplayNameAttribute"/>属性の値</returns>
        public string GetDisplayName(string propertyName)
            => GetDisplayName(typeof(TModel).GetProperty(propertyName));

        /// <summary><see cref="DisplayNameAttribute"/>属性の値を取得</summary>
        /// <typeparam name="TProperty">取得対象のプロパティの型</typeparam>
        /// <param name="propertySelector">プロパティを選択するラムダ式</param>
        /// <returns><see cref="DisplayNameAttribute"/>属性の値</returns>
        public string GetDisplayName<TProperty>(Expression<Func<TModel, TProperty>> propertySelector)
            => GetDisplayName((propertySelector.Body as MemberExpression)?.Member as PropertyInfo);

        /// <summary><see cref="DisplayNameAttribute"/>属性の値を取得</summary>
        /// <param name="propertyInfo">取得対象のプロパティ情報</param>
        /// <returns><see cref="DisplayNameAttribute"/>属性の値</returns>
        private string GetDisplayName(PropertyInfo propertyInfo)
            => propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
    }
}