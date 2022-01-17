namespace GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;

public interface IAppDatabaseConfiguration
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string UsersCollectionName { get; set; }
    string EmailsCollectionName { get; set; }
}
