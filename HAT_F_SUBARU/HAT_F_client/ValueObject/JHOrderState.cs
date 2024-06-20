using System.Linq;
using System.Collections.Generic;

namespace HatFClient.ValueObject
{
    /// <summary>
    /// <para>受発注画面でのみ使用する、画面表示用の発注状態</para>
    /// <para>DBのORDER_STATEとは無関係</para>
    /// </summary>
    internal class JHOrderState : ValueObjectBase<string>
    {
        /// <summary>コンストラクタ</summary>
        /// <param name="value">値</param>
        public JHOrderState(string value)
            : base(value)
        {
        }

        #region 定数

        /// <summary>発注前(1)</summary>
        public static readonly JHOrderState PreOrder = new JHOrderState("1");

        /// <summary>手配中・回答待(2)</summary>
        public static readonly JHOrderState Ordered = new JHOrderState("2");

        /// <summary>Acos済(3)</summary>
        public static readonly JHOrderState Acos = new JHOrderState("3");

        /// <summary>削除</summary>
        public static readonly JHOrderState Deleted = new JHOrderState("4");

        /// <summary>逸注(5)</summary>
        public static readonly JHOrderState Missing = new JHOrderState("5");

        /// <summary>請書処理済(6)</summary>
        public static readonly JHOrderState Ukesyo = new JHOrderState("6");

        /// <summary>手配済(7)</summary>
        public static readonly JHOrderState Completed = new JHOrderState("7");

        /// <summary>その他(N)</summary>
        public static readonly JHOrderState Other = new JHOrderState("N");

        #endregion 定数

        #region プロパティ

        /// <summary>有効かどうか</summary>
        public bool IsValid
            => this != Missing && this != Deleted;

        /// <summary>発注照合済みかどうか</summary>
        public bool IsCompleted
            => this == Acos || this == Completed || this == Ukesyo;

        /// <summary>
        /// <para>発注照合以前の状態</para>
        /// <para>逸注や削除はfalseとなるので、!IsBeforeComplete == Completedとはならない</para>
        /// </summary>
        public bool IsBeforeComplete
            => IsValid && !IsCompleted;
        #endregion プロパティ

        #region オペレータ

        /// <summary>stringへの変換</summary>
        /// <param name="state">発注状態</param>
        public static implicit operator string(JHOrderState state)
            => state.Value;

        /// <summary>発注状態への変換</summary>
        /// <param name="value">値</param>
        public static implicit operator JHOrderState(string value)
            => new JHOrderState(value);

        /// <summary>比較</summary>
        /// <param name="x">左辺</param>
        /// <param name="y">右辺</param>
        /// <returns>一致するかどうか</returns>
        public static bool operator ==(JHOrderState x, JHOrderState y)
            => x.Equals(y);

        /// <summary>比較</summary>
        /// <param name="x">左辺</param>
        /// <param name="y">右辺</param>
        /// <returns>一致しないかどうか</returns>
        public static bool operator !=(JHOrderState x, JHOrderState y)
            => !x.Equals(y);

        #endregion オペレータ

        #region 比較に必要なObjectのオーバーライド

        /// <summary>比較</summary>
        /// <param name="obj">右辺</param>
        /// <returns>一致するかどうか</returns>
        public override bool Equals(object obj)
            => base.Equals(obj);

        /// <summary>Hash値の取得</summary>
        /// <returns>Hash値の取得</returns>
        public override int GetHashCode()
            => base.GetHashCode();

        #endregion 比較に必要なObjectのオーバーライド
    }
}