using Backend.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
namespace Backend.Utils
{
    public class SendMail
    {
        public readonly SmtpClient _client;
        public readonly EmailSettings _setting;

        public SendMail(IOptions<EmailSettings> setting)
        {
            this._setting = setting.Value;
            _client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_setting.Email, _setting.Password),
                EnableSsl = true
            };
        }

        public async Task MailSender(string to, string subject, string message)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(_setting.Email),
                IsBodyHtml = true,
                Body = message,
                Subject = subject
            };
            mailMessage.To.Add(to);

            await _client.SendMailAsync(mailMessage);
            

        }

    }
}
