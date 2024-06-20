using Microsoft.VisualBasic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Text;

namespace HAT_F_api.Shared
{
    public class SiteMailer
    {
        // 通知メールのプレフィックス
        private static readonly String NoticePrefix = "";

        private readonly SendGridClient _sendGrid;
        private const string FROM_ADDRESS = "test@di-system.co.jp";
        private const string FROM_NAME = "ファシリティーズ基幹システム";
        private const string HR = "***-***-***-***-***-***-***-***-***-***";
        private static readonly EmailAddress SENDER = new EmailAddress
        {
            Email = FROM_ADDRESS,
            Name = FROM_NAME,
        };

        private static readonly string FOOTER = String.Format("\n\n{0}\n{1}\n{0}", new String[] { HR, FROM_NAME });
        private readonly IConfiguration _configuration;

        public SiteMailer(IConfiguration configuration)
        {
            _configuration = configuration;

            string apiKey = _configuration["SendGrid:ApiKey"];
            _sendGrid = new SendGridClient(apiKey);
        }

        // テキストメールを送信する
        public async Task<Response> sendTextMessage(string subject, string body, List<EmailAddress> toEmails, List<EmailAddress> ccEmails = null, List<EmailAddress> bccEmails = null)
        {
            var msg = new SendGridMessage
            {
                From = SENDER,
                Subject = NoticePrefix + subject,
                PlainTextContent = body + FOOTER,
            };
            msg.AddTos(toEmails);
            if (ccEmails?.Count > 0)
            {
                msg.AddCcs(ccEmails);
            }
            if (bccEmails?.Count > 0)
            {
                msg.AddBccs(bccEmails);
            }
            return await _sendGrid.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}

