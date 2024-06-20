using System;

namespace HatFClient.Common
{
    /// <summary>usingでスコープ定義をし、Dispose処理を指定できるクラス</summary>
    internal class Scope : IDisposable
    {
        /// <summary>終了時処理</summary>
        protected Action OnDispose { get; set; }

        /// <summary>
        /// <para>デフォルトコンストラクタ</para>
        /// <para>派生先でのみ使用可</para>
        /// </summary>
        protected Scope()
        {
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="onDispose">終了時処理</param>
        public Scope(Action onDispose)
        {
            OnDispose = onDispose;
        }

        /// <summary>終了</summary>
        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}
