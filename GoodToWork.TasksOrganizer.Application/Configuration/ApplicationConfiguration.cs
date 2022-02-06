using GoodToWork.TasksOrganizer.Application.Events.EventSender;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GoodToWork.TasksOrganizer.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient<ICurrentDateTime, GetCurrentDateTime>();
        services.AddTransient<IEventSenderWrapper, EventSenderWrapper>();

        return services;
    }
}
