using Microsoft.Extensions.Logging;
using net_core_baby_reminder.Services.Interface;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace net_core_baby_reminder.Services.Implementation
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger _logger;

        public SendEmailService(ILogger<SendEmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmail()
        {
            try
            {
                _logger.LogInformation($"SendEmailService - Started");

                MailMessage email = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                email.From = new MailAddress("rorro.irg@gmail.com");
                email.To.Add("rorro.irg@gmail.com");
                email.Subject = "Test Mail";
                email.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("rorro.irg@gmail.com", "AndroidApple2019!");
                SmtpServer.EnableSsl = true;


                await SmtpServer.SendMailAsync(email);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"SendEmailService - Failed, Error: {ex.Message}");
            }
        }
    }
}
