using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SafariGo.Core.Configure_Services;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Services
{
    public class MaillingService : IMaillingService

    {
        private readonly MailSettings _mailSettings;

        public MaillingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task ConfirmEamilAsync(string mailTo, string userName, string url)
        {
            var pathFile = $"{Directory.GetCurrentDirectory()}\\Template\\EmailTemplate.html ";
            var str = new StreamReader(pathFile);
            var mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[confirmbtn]", $"<a confirmBtn href=\"{url}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm Email</a>\r\n");
            mailText = mailText.Replace("[Username]", userName);
            mailText = mailText.Replace("[Message]", "This email is to confirm that we have received your registration information and to verify the email address you have provided.");
            mailText = mailText.Replace("[type_of_action]", "Confirm Email Address");
            await SendEmailAsync(mailTo, "Confirm Email Address", mailText);
        }

        public async Task ResetPasswordAsync(string mailTo, string userName, string url)
        {
            var pathFile = $"{Directory.GetCurrentDirectory()}\\Template\\EmailTemplate.html ";
            var str = new StreamReader(pathFile);
            var mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[confirmbtn]", $"<a confirmBtn href=\"{url}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Reset Password</a>\r\n")
            .Replace("[Username]", userName)
            .Replace("[Message]", $"You recently requested to reset your password for your {mailTo} account.")
            .Replace("[type_of_action]", "Reset Password");
            await SendEmailAsync(mailTo, "Reset Password", mailText);
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject,
            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
