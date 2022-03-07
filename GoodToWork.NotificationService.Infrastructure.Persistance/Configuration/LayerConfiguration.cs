using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Application.Repositories.Message;
using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Message;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;

public static class LayerConfiguration
{
    public static IServiceCollection AddPersistanceLayer(this IServiceCollection services, IAppDatabaseConfiguration appDatabaseConfiguration)
    {
        services.AddSingleton<IAppDatabaseConfiguration>(appDatabaseConfiguration);

        services.AddScoped<ISharedRepository<UserEntity>, SharedRepository<UserEntity>>();
        services.AddScoped<ISharedRepository<EmailEntity>, SharedRepository<EmailEntity>>();
        services.AddScoped<ISharedRepository<MessageEntity>, SharedRepository<MessageEntity>>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IAppRepository, AppRepository>();

        return services;
    }
}
