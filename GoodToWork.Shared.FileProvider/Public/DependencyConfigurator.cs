using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.Shared.FileProvider;

public static class DependencyConfigurator
{
    public static IServiceCollection Configure(this IServiceCollection services, Action<Configuration> options)
    {
        var config = new Configuration();
        options(config);
        services.AddSingleton(config);

        services.AddScoped<IFileProvider, Implementations.FileProvider>();

        return services;
    }
}

public class Configuration
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string UrlEndPoint { get; set; }
}