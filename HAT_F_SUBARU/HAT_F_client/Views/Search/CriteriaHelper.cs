using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HatFClient.Repository;
using HatFClient.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace HatFClient.Views.Search
{
    /// <summary>
    /// <see cref="NewFrmAdvancedSearch"/>で使用するための<see cref="ColumnMappingConfig"/>に関するヘルパークラス
    /// </summary>
    internal class CriteriaHelper
    {
        /// <summary>文字列のみの発注状態一覧実体</summary>
        private static Lazy<Dictionary<string,string>> _stringOrderStates = new Lazy<Dictionary<string,string>>(() =>
        {
            return new Dictionary<string, string>()
            {
                {"発注前", "発注前" },
                {"手配中・回答待", "手配中・回答待" },
                {"ACOS済", "ACOS済" },
                {"手配済", "手配済" },
                {"請書処理済", "請書処理済" },
            };
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>伝票区分一覧実体</summary>
        private static Lazy<Dictionary<string,string>> _slips = new Lazy<Dictionary<string,string>>(() =>
        {
            return ClientRepo.GetInstance().Options.DivDenpyo.ToDictionary(d => $"{d.Code}：{d.Name}", d => d.Code);
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>発注先一覧実体</summary>
        private static Lazy<Dictionary<string,string>> _supplierTypes = new Lazy<Dictionary<string,string>>(() =>
        {
            return new Dictionary<string, string>() { { "HAT", "0" }, { "HAT以外", "1" } };
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>伝票区分一覧</summary>
        public static Dictionary<string,string> Slips => _slips.Value;

        /// <summary>発注先一覧</summary>
        public static Dictionary<string,string> SupplierTypes => _supplierTypes.Value;

        /// <summary>文字列のみの発注状態一覧実体</summary>
        public static Dictionary<string, string> StringOrderStates => _stringOrderStates.Value;

        /// <summary>検索画面に表示される選択肢を作成する</summary>
        /// <typeparam name="T">検索条件クラスの型</typeparam>
        /// <param name="forGenSearch">汎用検索用として作成するか</param>
        /// <param name="invisibleProperties">検索画面で非表示にするプロパティをラムダ式で表現する</param>
        /// <returns>選択肢リスト</returns>
        public static List<ColumnMappingConfig> CreateCriteriaDefinitions<T>(bool forGenSearch, params Expression<Func<T, object>>[] invisibleProperties)
        {
            var result = CreateCriteriaDefinitions<T>(forGenSearch);
            foreach (var property in invisibleProperties)
            {
                var body = property.Body as MemberExpression ?? (property.Body as UnaryExpression)?.Operand as MemberExpression;
                result.First(x => x.FieldName == body.Member.Name).Visible = false;
            }
            return result;
        }

        /// <summary>検索画面に表示される選択肢を作成する</summary>
        /// <typeparam name="T">検索条件クラスの型</typeparam>
        /// <param name="invisibleProperties">検索画面で非表示にするプロパティをラムダ式で表現する</param>
        /// <returns>選択肢リスト</returns>
        public static List<ColumnMappingConfig> CreateCriteriaDefinitions<T>(params Expression<Func<T,object>>[] invisibleProperties)
        {
            return CreateCriteriaDefinitions<T>(false, invisibleProperties);
        }

        /// <summary>検索画面に表示される選択肢を作成する</summary>
        /// <typeparam name="T">検索条件クラスの型</typeparam>
        /// <returns>選択肢リスト</returns>
        public static List<ColumnMappingConfig> CreateCriteriaDefinitions<T>()
        {
            return CreateCriteriaDefinitions<T>(false);
        }

        /// <summary>検索画面に表示される選択肢を作成する</summary>
        /// <typeparam name="T">検索条件クラスの型</typeparam>
        /// <param name="forGenSearch">汎用検索用として作成するか</param>
        /// <returns>選択肢リスト</returns>
        public static List<ColumnMappingConfig> CreateCriteriaDefinitions<T>(bool forGenSearch)
        {
            var properties = typeof(T).GetProperties();
            var result = new List<ColumnMappingConfig>();
            foreach(var property in properties)
            {
                if (forGenSearch)
                {
                    // 汎用検索非表示属性のチェック
                    var visiblity = property.GetCustomAttribute<GenSearchVisiblityAttribute>();
                    if (visiblity != null)
                    {
                        if (visiblity.Visible == false) { continue; }
                    }
                }

                // TODO CriteriaDefinitionAttribute対応
                // Conditionクラスのプロパティ名に[CriteriaDefinition("表示名", "列名")]のようにエイリアス機能を設定する想定です。
                // この属性をつけることで、Conditionクラスのプロパティ名とDBの列名が一致していなければならないという条件がはずれます。
                // Conditionクラスに属性を設定するために、CriteriaDefinitionAttributeクラスはHatFApiModel.dllに作成する必要があるので
                // API側で定義したらここも対応します。
#if true
                var definition = property.GetCustomAttribute<CriteriaDefinitionAttribute>();
                var display = definition?.Label ?? property.Name;
                var column = definition?.Column ?? property.Name;
#else
                var display = property.Name;
                var column = property.Name;
#endif
                // Nullableの場合は内部の型を参照する
                var propertyType = property.PropertyType.IsConstructedGenericType ?
                    property.PropertyType.GenericTypeArguments.First() : property.PropertyType;
                result.Add(new ColumnMappingConfig(display, 0, propertyType, TextAlignEnum.LeftCenter, SystemFonts.DialogFont,
                    new LambdaMappingStrategy(data => property.GetValue((T)data)), column));
            }
            return result;
        }

        /// <summary>検索条件選択肢リストの表示状態を変更する</summary>
        /// <typeparam name="TSource">検索条件リストの基となったモデルの型</typeparam>
        /// <typeparam name="TResult">対象フィールドの型</typeparam>
        /// <param name="list">検索条件選択肢リスト</param>
        /// <param name="fieldSelector">対象フィールドを返却するラムダ式</param>
        /// <param name="visible">表示状態</param>
        /// <returns>検索条件選択肢リスト</returns>
        public static List<ColumnMappingConfig> ChangeVisible(List<ColumnMappingConfig> list, string fieldName, bool visible)
        {
            var targets = list.Where(x => x.FieldName == fieldName);
            foreach (var target in targets)
            {
                target.Visible = visible;
            }
            return list;
        }
    }
}
