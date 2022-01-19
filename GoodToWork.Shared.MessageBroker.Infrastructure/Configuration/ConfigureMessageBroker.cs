using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.Infrastructure.Builder;
using GoodToWork.Shared.MessageBroker.Infrastructure.Features;
using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.Shared.MessageBroker.Infrastructure.Configuration;

public static class ConfigureMessageBroker
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Action<MessageBrokerBuilder> providerBuilder)
    {
        var messageBrokerBuilder = new MessageBrokerBuilder(services);
        providerBuilder.Invoke(messageBrokerBuilder);
        services.AddSingleton(messageBrokerBuilder.Build());

        services.AddTransient<IEventSender, EventSender>();

        return services;
    }
}
