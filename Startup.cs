using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using net_core_baby_reminder.Services.Implementation;
using net_core_baby_reminder.Services.Interface;

[assembly: FunctionsStartup(typeof(net_core_baby_reminder.Startup))]

namespace net_core_baby_reminder
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISendEmailService, SendEmailService>();
        }
    }
}