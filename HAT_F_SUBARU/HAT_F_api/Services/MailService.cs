using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Shared;
using HAT_F_api.Utils;
using SendGrid.Helpers.Mail;
using System.Text;

namespace HAT_F_api.Services
{
    public class MailService
    {
        /// <summary>DBコンテキスト</summary>
        private readonly HatFContext _hatFContext;
        /// <summary>起動時情報コンテキスト</summary>
        private readonly HatFApiExecutionContext _executionContext;
        private readonly NLog.ILogger _logger;
        private readonly UpdateInfoSetter _updateInfoSetter;
        private readonly SiteMailer _siteMailer;

        /// <summary>コンストラクタ</summary>
        /// <param name="hatFContext">DBコンテキスト</param>
        /// <param name="executionContext">起動時情報コンテキスト</param>
        /// <param name="logger">Logger</param>
        /// <param name="updateInfoSetter"></param>
        /// <param name="siteMailer"></param>
        public MailService(HatFContext hatFContext, HatFApiExecutionContext executionContext, NLog.ILogger logger, UpdateInfoSetter updateInfoSetter, SiteMailer siteMailer)
        {
            _hatFContext = hatFContext;
            _executionContext = executionContext;
            _logger = logger;
            _updateInfoSetter = updateInfoSetter;
            _siteMailer = siteMailer;
        }
        /// <summary>テストメール送信</summary>
        public int SendMailTest()
        {
            var subject = new StringBuilder();
            subject.Append(" テストメール送信");

            var body = new StringBuilder();
            body.AppendLine("＜内容＞");
            body.AppendLine("テストメールです");
            body.Append("・案件番号：").AppendLine("8192");
            body.Append("・案件名：").AppendLine("やればできる子山路");
            body.Append("・<").Append("YMJ").Append(">").Append("担当者名:").AppendLine("山路");
            body.AppendLine();
            body.AppendLine("いつもお世話になっております。");
            body.AppendLine();
            body.AppendLine("★このメールは送信専用メールアドレスから配信されています。");
            body.AppendLine("このメールに返信されても、返信内容の確認およびご返答ができません。");
            body.AppendLine("あらかじめご了承ください。");

            var toEmails = new List<EmailAddress>
            {
                new EmailAddress("rikuyamaji@di-system.co.jp", "山路テストメール")
            };

            var resp = _siteMailer.sendTextMessage(subject.ToString(), body.ToString(), toEmails);

            return 0;
        }
    }
}
