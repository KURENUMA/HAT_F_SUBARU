using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Services;
using HAT_F_api.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SendGrid.Helpers.Mail;
using System.Text;

namespace HAT_F_api.Controllers
{
    [Route("api/notice")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly HatFContext _context;
        private readonly NLog.ILogger _logger;
        private readonly IConfiguration _config;
        private readonly MailService _mailService;

        public NoticeController(HatFContext context, NLog.ILogger logger, IConfiguration config,MailService mailService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _mailService = mailService;
        }
        [HttpPost("test-mail")]
        public async Task<ActionResult<ApiResponse<int>>> PostSendTestMailAsync()
        {
            return await ApiLogicRunner.RunAsync(() =>
            {
                var result = _mailService.SendMailTest();
                return Task.FromResult(result);
            });
        }
    }
}
