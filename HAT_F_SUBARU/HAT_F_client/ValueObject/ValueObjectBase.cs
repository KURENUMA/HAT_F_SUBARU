using System;

namespace HatFClient.ValueObject
{
    /// <summary>ValueObjectの基底クラス</summary>
    /// <typeparam name="T">値の型</typeparam>
    internal abstract class ValueObjectBase<T> : IConvertible
        where T : IConvertible
    {
        /// <summary>値</summary>
        protected readonly T Value;

        /// <summary>コンストラクタ</summary>
        /// <param name="value">値</param>
        protected ValueObjectBase(T value)
        {
            Value = value;
        }

        #region 比較に必要なObjectクラスのオーバーライド

        /// <summary>比較</summary>
        /// <param name="obj">ValueObject</param>
        /// <returns>一致するかどうか</returns>
        public override bool Equals(object obj)
            => (obj is ValueObjectBase<T> value) && Value.Equals(value.Value);

        /// <summary>Hash値の取得</summary>
        /// <returns>Hash値</returns>
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <summary>文字列化</summary>
        /// <returns>文字列</returns>
        public override string ToString()
            => Value.ToString();

        #endregion 比較に必要なObjectクラスのオーバーライド

        #region IConvertibleの実装

        /// <summary><see cref="TypeCode"/>への変換</summary>
        /// <returns><see cref="TypeCode"/>値</returns>
        public TypeCode GetTypeCode()
            => Value.GetTypeCode();

        /// <summary><see cref="bool"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="bool"/>値</returns>
        public bool ToBoolean(IFormatProvider provider)
            => Value.ToBoolean(provider);

        /// <summary><see cref="byte"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="byte"/>値</returns>
        public byte ToByte(IFormatProvider provider)
            => Value.ToByte(provider);

        /// <summary><see cref="char"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="char"/>値</returns>
        public char ToChar(IFormatProvider provider)
            => Value.ToChar(provider);

        /// <summary><see cref="DateTime"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="DateTime"/>値</returns>
        public DateTime ToDateTime(IFormatProvider provider)
            => Value.ToDateTime(provider);

        /// <summary><see cref="decimal"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="decimal"/>値</returns>
        public decimal ToDecimal(IFormatProvider provider)
            => Value.ToDecimal(provider);

        /// <summary><see cref="double"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="double"/>値</returns>
        public double ToDouble(IFormatProvider provider)
            => Value.ToDouble(provider);

        /// <summary><see cref="short"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="short"/>値</returns>
        public short ToInt16(IFormatProvider provider)
            => Value.ToInt16(provider);

        /// <summary><see cref="int"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="int"/>値</returns>
        public int ToInt32(IFormatProvider provider)
            => Value.ToInt32(provider);

        /// <summary><see cref="long"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="long"/>値</returns>
        public long ToInt64(IFormatProvider provider)
            => Value.ToInt64(provider);

        /// <summary><see cref="sbyte"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="sbyte"/>値</returns>
        public sbyte ToSByte(IFormatProvider provider)
            => Value.ToSByte(provider);

        /// <summary><see cref="float"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="float"/>値</returns>
        public float ToSingle(IFormatProvider provider)
            => Value.ToSingle(provider);

        /// <summary><see cref="string"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="string"/>値</returns>
        public string ToString(IFormatProvider provider)
            => Value.ToString(provider);

        /// <summary><see cref="Type"/>への変換</summary>
        /// <param name="conversionType"></param>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="Type"/>値</returns>
        public object ToType(Type conversionType, IFormatProvider provider)
            => Value.ToType(conversionType, provider);

        /// <summary><see cref="ushort"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="ushort"/>値</returns>
        public ushort ToUInt16(IFormatProvider provider)
            => Value.ToUInt16(provider);

        /// <summary><see cref="uint"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="uint"/>値</returns>
        public uint ToUInt32(IFormatProvider provider)
            => Value.ToUInt32(provider);

        /// <summary><see cref="ulong"/>への変換</summary>
        /// <param name="provider">書式プロバイダ</param>
        /// <returns><see cref="ulong"/>値</returns>
        public ulong ToUInt64(IFormatProvider provider)
            => Value.ToUInt64(provider);

        #endregion IConvertibleの実装
    }
}