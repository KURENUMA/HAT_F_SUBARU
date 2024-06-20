using HAT_F_api.CustomModels;

namespace HAT_F_api.Utils
{
    public class HatFLoginResultAccesser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HatFLoginResultAccesser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HatFLoginResult HatFLoginResult
        {
            get 
            {
                var loginResult = _httpContextAccessor.HttpContext.Items["HatFLoginResult"] as HatFLoginResult;

                if (loginResult == null)
                {
                    loginResult = new HatFLoginResult();
                    loginResult.LoginSucceeded = false;
                    loginResult.ErrorMessage = "";
                    loginResult.EmployeeId = 0;
                    loginResult.EmployeeCode = "0000";
                    loginResult.EmployeeName = "ログインしていません";
                    loginResult.EmployeeNameKana = "";
                    loginResult.EmployeeTag = "";
                    loginResult.Roles = "";
                }

                return loginResult;
            }
        }
    }
}
