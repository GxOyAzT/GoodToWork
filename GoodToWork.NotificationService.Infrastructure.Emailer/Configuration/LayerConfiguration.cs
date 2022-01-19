using GoodToWork.NotificationService.Application.Email;
using GoodToWork.NotificationService.Infrastructure.Emailer.Features.Email;
using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.NotificationService.Infrastructure.Emailer.Configuration;

public static class LayerConfiguration
{
    public static IServiceCollection AddEmailerLayer(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
