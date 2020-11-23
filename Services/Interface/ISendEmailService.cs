using System.Threading.Tasks;

namespace net_core_baby_reminder.Services.Interface
{
    public interface ISendEmailService
    {
        public Task SendEmail();

        public Task SendEmailEnglish();
    }
}
