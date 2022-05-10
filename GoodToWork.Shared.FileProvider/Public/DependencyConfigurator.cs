using GoodToWork.Shared.FileProvider.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.Shared.FileProvider;

public static class DependencyConfigurator
{
    public static IServiceCollection ConfigureFileProvider(this IServiceCollection services, Configuration options)
    {
        services.AddSingleton(options);

        services.AddScoped<IFileProvider, Implementations.FileProvider>();

        return services;
    }
}

