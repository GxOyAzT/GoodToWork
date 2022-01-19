using GoodToWork.NotificationService.Application.Features.CurrentDateTime;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GoodToWork.NotificationService.Application.Configuration;

public static class LayerConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient<ICurrentDateTime, CurrentDateTimeFeature>();

        return services;
    }
}
