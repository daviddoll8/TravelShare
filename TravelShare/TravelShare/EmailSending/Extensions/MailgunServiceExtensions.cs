using TravelShare.EmailSending.Options;

namespace TravelShare.Extensions;

public static class MailgunServiceExtensions
{
    public static void ConfigureMailgunOptions(this IServiceCollection services,
        IHostEnvironment environment, IConfiguration configuration)
    {
        services.Configure<MailgunOptions>(options =>
        {
            options.Domain = environment.IsDevelopment() ? 
                "sandboxeab86c1ee4534f8ea0723f4919f6a0f4.mailgun.org" : "mail.dollworks.pro";
            options.ApiKey = configuration["Services:Mailgun:ApiKey"];
        });
    }
}