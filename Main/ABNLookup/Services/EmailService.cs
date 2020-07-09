using ABNLookup.Interfaces;
using ABNLookup.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ABNLookup.Services
{
    public class EmailService : IEmailService
    {
        private AppSettings appSettings;
        public EmailService(IConfiguration configuration)
        {
            appSettings = configuration
                .GetSection("AppSettings")
                .Get<AppSettings>();
        }
        public async Task SendAsync(Exception exception)
        {
            var apiSettings = appSettings.API;
            string contactEmail = apiSettings?.Contact?.Email;
            string smptServer = apiSettings?.SmtpServer;
            string fromEmailAddress =apiSettings?.FromEmailAddress;

            var message = new MailMessage();
            message.From = new MailAddress(fromEmailAddress);
            message.To.Add(contactEmail);
            message.Subject = "Abn Service - Error";
            message.Body = exception.Message + "<br/>" + exception.StackTrace.Trim();
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient(smptServer))
            {
                await smtp.SendMailAsync(message);
            }
                     
        }
    }
}
