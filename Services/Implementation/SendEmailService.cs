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
                var currentDate = DateTime.UtcNow;
                var deliveryDate = new DateTime(2021, 2, 5);
                var days = (deliveryDate.Date - currentDate.Date).Days;
                var body = @"<html>
                                <body>
                                    <h1>Faltan " + days + " dias para conocer al bebecito</h1>" +
                                    "<img src=cid:myImageID alt='BEBECITO' width=\"500\" height=\"600\"/>" +
                                "</body>" +
                            "</html>";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource theEmailImage = new LinkedResource("./Images/DaniOLeo.jpeg")
                {
                    ContentId = "myImageID"
                };
                htmlView.LinkedResources.Add(theEmailImage);
                
                email.From = new MailAddress("rodrigo3.tech@gmail.com");
                email.AlternateViews.Add(htmlView);
                email.To.Add("rorro.irg@gmail.com,Macarena.crg@hotmail.com,Francisco2.ins@gmail.com,Ingrid.pgc@hotmail.com,carlos.rojas@fluor.com");
                email.Subject = "Test Mail";
                email.IsBodyHtml = true;
                email.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("rodrigo3.tech@gmail.com", "OpenSource2019!");
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
