using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Infrastructure.Configurations;
using GoodToWork.AuthService.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Infrastructure.Token;

namespace GoodToWork.AuthService.Infrastructure.DependencyConfiguration;

public static class AddServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly applicationAssembly)
    {
        services.AddTransient(s => configuration.GetSection(DatabaseConfig.SectionName).Get<DatabaseConfig>());
        services.AddTransient(s => configuration.GetSection(JwtTokenConfig.SectionName).Get<JwtTokenConfig>());
        services.AddMediator(applicationAssembly);
        services.AddRepositories();
        services.AddUtilities();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISessionRepository, SessionRepository>();
        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);
        return services;
    }

    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddTransient<ITokenGenerator, TokenGenerator>();
        services.AddTransient<ITokenDeserializer, TokenDeserializer>();
        return services;
    }
}
