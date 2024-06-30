using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HatFClient.Views
{
    /// <summary>モードレス管理をするためのファクトリクラス</summary>
    internal static class FormFactory
    {
        /// <summary>フォームインスタンス</summary>
        private static Dictionary<Type, Form> _modelessForms = new Dictionary<Type, Form>();

        /// <summary>モードレス用に唯一のインスタンスを取得する</summary>
        /// <typeparam name="T">フォームの型</typeparam>
        /// <returns>フォームのインスタンス</returns>
        public static T GetModelessForm<T>()
            where T : Form, new()
            => GetModelessForm<T>(null);

        /// <summary>モードレス用に唯一のインスタンスを取得する</summary>
        /// <typeparam name="T">フォームの型</typeparam>
        /// <param name="firstAction">インスタンスを新規生成した場合の処理</param>
        /// <returns>フォームのインスタンス</returns>
        public static T GetModelessForm<T>(Action<T> firstAction)
            where T : Form, new()
        {
            if (!_modelessForms.ContainsKey(typeof(T)))
            {
                var form = new T();
                _modelessForms.Add(typeof(T), form);
                form.FormClosed += (s, e) => _modelessForms.Remove(typeof(T));
                firstAction?.Invoke(form);
            }
            return _modelessForms[typeof(T)] as T;
        }

        /// <summary>キャッシュされているインスタンスを取得する。</summary>
        /// <typeparam name="T">フォームの型</typeparam>
        /// <returns>フォームのインスタンス。キャッシュされていない場合はnull</returns>
        public static T GetModelessFormCache<T>()
            where T : Form, new()
        {
            return _modelessForms.ContainsKey(typeof(T)) ? _modelessForms[typeof(T)] as T : null;
        }
    }
}
