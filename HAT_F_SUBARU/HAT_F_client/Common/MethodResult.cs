using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    /// <summary>
    /// メソッドで期待される戻り値と実行成否の両方を表すクラスです
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MethodResult<T>
    {
        public bool Failed { get { return !this.Success; } }

        private T _result = default;
        private bool _checked = false;
        private bool _success = false;

        public bool Success 
        {
            get
            {
                // 成否の確認済みか
                _checked = true;
                return _success;
            }
        }

        public MethodResult(T result)
        {
            _success = true;
            _result = result;
        }

        public static MethodResult<T> FailedResult
        {
            get
            {
                return new MethodResult<T>(default) { _success = false };
            }
        }

        public T Value
        {
            get
            {
                if (!_checked)
                {
                    throw new InvalidOperationException("成否確認前は値を取得できません。");
                }

                if (!_success)
                {
                    throw new InvalidOperationException("失敗リザルトからは値を取得できません。");
                }

                return this._result;
            }
        }
    }
}
