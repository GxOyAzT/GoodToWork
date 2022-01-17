namespace GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;

internal interface IAppDatabaseConfiguration
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string UsersCollectionName { get; set; }
}
