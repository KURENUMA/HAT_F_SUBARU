using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly HatFContext _context;
        private readonly HatFAuthenticationService _hatFAuthenticationService;

        public LoginController(HatFContext context, HatFAuthenticationService hatFAuthenticationService)
        {
            _context = context;
            _hatFAuthenticationService = hatFAuthenticationService;
        }

        public class LoginReq
        {
            public string LoginId { get; set; }
            public string Password { get; set; }
        }

        [HttpPost()]
        public async Task<ActionResult<ApiResponse<DivAuth>>> Login(LoginReq req)
        {
            Debug.WriteLine($"{req.LoginId}:{req.Password}");
            var auth = await _context.DivAuths
              .Where(e => e.AuthCd == req.LoginId && e.AuthPassword == req.Password && !e.AuthDeleted)
              .FirstOrDefaultAsync();
            if (auth == null)
            {
                return new ApiErrorResponse<DivAuth>(ApiResultType.LoginDenied, "���O�C��ID�܂��̓p�X���[�h������������܂���B");
            }
            return new ApiOkResponse<DivAuth>(new DivAuth() { AuthId = auth.AuthId, AuthCd = auth.AuthCd, AuthName = auth.AuthName});
        }


        [HttpPost("auth/")]
        public async Task<ActionResult<ApiResponse<HatFLoginResult>>> Auth([FromBody] HatFLoginRequest reqeuest)
        {
            Debug.WriteLine($"{reqeuest.EmployeeCode}:{reqeuest.LoginPassword}");
            HatFLoginResult loginResult = await _hatFAuthenticationService.AuthenticationAsync(reqeuest);

            if (!loginResult.LoginSucceeded)
            {
                return new ApiErrorResponse<HatFLoginResult>(ApiResultType.LoginDenied, "���O�C��ID�܂��̓p�X���[�h������������܂���B");
            }
            else
            {
                return new ApiOkResponse<HatFLoginResult>(loginResult);
            }
        }

        /// <summary>
        /// �p�X���[�h�ύX(�{�l�p)
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="newPassword"></param>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        [HttpPut("login-password")]
        public async Task<ActionResult<ApiResponse<int>>> PutLoginPassword([FromQuery][Required] int empId, [FromQuery][Required] string newPassword, [FromQuery] string currentPassword = null)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFAuthenticationService.PutLoginPassword(empId, newPassword, currentPassword, false);
            });
        }

        /// <summary>
        /// �p�X���[�h�ύX(�Ǘ��җp)
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "MasterEdit")]    // ���̃G���h�|�C���g�g�p�ɕK�v�ȃ��[��
        [HttpPut("login-password-admin")]
        public async Task<ActionResult<ApiResponse<int>>> PutLoginPassword([FromQuery][Required] int empId, [FromQuery][Required] string newPassword)
        {
            return await ApiLogicRunner.RunAsync(async () =>
            {
                return await _hatFAuthenticationService.PutLoginPassword(empId, newPassword, null, true);
            });
        }

#if false
        [HttpGet("echo")]
        public string GetEcho([FromQuery] string text)
        {
            return text;
        }

        [Authorize]
        [HttpGet("echo-require-auth")]
        public string GetEchoRequireAuth([FromQuery]string text)
        {
            return text;
        }

        [Authorize(Roles = "DebugSampleRole")]
        [HttpGet("echo-require-auth-debugsamplerole")]
        public string GetEchoRequireAuthDebugSampleRole([FromQuery] string text)
        {
            return text;
        }

        [Authorize(Roles = "NotExistsRole")]
        [HttpGet("echo-require-auth-notexistsrole")]
        public string GetEchoRequireAuthNotExistsRole([FromQuery] string text)
        {
            return text;
        }
#endif

    }
}