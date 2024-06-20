using HAT_F_api.CustomModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    [DebuggerStepThrough]
    internal class ApiResult<T>
    {
        /// <summary>
        /// API呼出が成功したかを表します。
        /// </summary>
        public bool Successed { get; private set; } = false;

        /// <summary>
        /// API呼出が失敗したかを表します。
        /// </summary>
        public bool Failed { get { return !Successed; } }

        private T _value = default(T);

        private ApiResponse<T> _apiResponse = null;

        public ApiResponse<T> ApiOkResponse
        { 
            get 
            { 
                return _apiResponse as ApiOkResponse<T>;
            } 
        }

        public ApiResponse<T> ApiErrorResponse
        {
            get
            {
                return _apiResponse as ApiErrorResponse<T>;
            }
        }

        /// <summary>
        /// API呼出が返却した値を表します。
        /// </summary>
        public T Value 
        {
            get 
            {
                if (this.Failed)
                {
                    throw new InvalidOperationException("API呼出が成功しなければ値を取得できません。");
                }

                return _value;
            }

            private set 
            {
                _value = value;
            }
        }

        public ApiResult(bool success, T value, ApiResponse<T> apiResponse) 
        { 
            this.Successed = success;
            this.Value = value;
            this._apiResponse = apiResponse;
        }
    }
}
