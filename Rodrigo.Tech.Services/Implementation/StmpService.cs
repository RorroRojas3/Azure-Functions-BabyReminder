using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Models.Constants;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables;
using Rodrigo.Tech.Services.Helpers;
using Rodrigo.Tech.Services.Interface;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Implementation
{
    public class StmpService : IStmpService
    {
        private readonly ILogger _logger;
        private readonly IRepositoryPattern<Email> _emailRepository;
        private readonly IRepositoryPattern<EmailBody> _emailBodyRepository;
        private readonly IRepositoryPattern<Language> _languageRepository;

        public StmpService(ILogger<StmpService> logger,
                            IRepositoryPattern<Email> emailRepositoryService,
                            IRepositoryPattern<EmailBody> emailBodyRepository,
                            IRepositoryPattern<Language> languageRepository)
        {
            _logger = logger;
            _emailRepository = emailRepositoryService;
            _emailBodyRepository = emailBodyRepository;
            _languageRepository = languageRepository;
        }

        /// <inheritdoc/>
        public async Task SendEmail()
        {
            _logger.LogInformation($"{nameof(StmpService)} - {nameof(SendEmail)} - Started");

            var currentDate = DateTime.UtcNow;
            var deliveryDate = new DateTime(2021, 2, 5);
            var days = (deliveryDate.Date - currentDate.Date).Days;

            var stmpEmail = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.STMP_EMAIL);
            SmtpClient SmtpServer = new SmtpClient(Environment.GetEnvironmentVariable(EnvironmentVariableConstants.STMP_CLIENT))
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(stmpEmail, Environment.GetEnvironmentVariable(EnvironmentVariableConstants.STMP_PASSWORD)),
                EnableSsl = true
            };

            var languages = await _languageRepository.GetAll();
            foreach(var item in languages)
            {
                var emails = _emailRepository.GetAllWithExpression(x => x.LanguageId == item.Id);

                if (emails.Count == 0)
                {
                    _logger.LogInformation($"{nameof(StmpService)} - {nameof(SendEmail)} - No emails found for " +
                        $"{item.Name} language");
                    continue;
                }

                var htmlFile = _emailBodyRepository.GetWithExpression(x => x.LanguageId == item.Id);

                if (htmlFile == null)
                {
                    _logger.LogInformation($"{nameof(StmpService)} - {nameof(SendEmail)} - Not HTML file found for " +
                        $"{item.Name} language");
                    continue;
                }

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(stmpEmail),
                    Subject = "Daniel/Leo/Oliver",
                    IsBodyHtml = true
                };
                foreach (var email in emails)
                {
                    mail.To.Add(email.EmailAddress);
                }

                var body = Encoding.UTF8.GetString(htmlFile.File, 0, htmlFile.File.Length);
                body = string.Format(body, days);
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource theEmailImage = new LinkedResource($"{DirectoryHelper.GetCurrentDirectory()}\\Images\\DaniOLeo.jpeg")
                {
                    ContentId = "myImageID"
                };
                htmlView.LinkedResources.Add(theEmailImage);
                mail.AlternateViews.Add(htmlView);
                mail.Body = body;
                await SmtpServer.SendMailAsync(mail);
                _logger.LogInformation($"{nameof(StmpService)} - {nameof(SendEmail)} - Email sent for " +
                        $"{item.Name} language");
            }
            _logger.LogInformation($"{nameof(StmpService)} - {nameof(SendEmail)} - Finished");
        }
    }
}
