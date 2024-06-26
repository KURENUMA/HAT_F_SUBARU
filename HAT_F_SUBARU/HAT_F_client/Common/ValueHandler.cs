using System.Reflection.Emit;

namespace HatFClient.Common
{
    /// <summary>値の変更を検知可能にするクラス</summary>
    internal class ValueHandler<T>
    {
        /// <summary>イベントハンドラ定義</summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        public delegate void ChangedEventHandler(T oldValue, T newValue);

        /// <summary>変更前</summary>
        public event ChangedEventHandler ValueChanging;

        /// <summary>変更後</summary>
        public event ChangedEventHandler ValueChanged;

        /// <summary>値実体</summary>
        private T _value;

        /// <summary>値</summary>
        public T Value
        {
            get => _value;
            set
            {
                T prev = _value;
                if (prev?.Equals(value) != true)
                {
                    ValueChanging?.Invoke(prev, value);
                    _value = value;
                    ValueChanged?.Invoke(prev, value);
                }
            }
        }

        /// <summary>暗黙キャスト</summary>
        /// <param name="handler">オブジェクト</param>
        public static implicit operator T(ValueHandler<T> handler)
            => handler.Value;
    }
}