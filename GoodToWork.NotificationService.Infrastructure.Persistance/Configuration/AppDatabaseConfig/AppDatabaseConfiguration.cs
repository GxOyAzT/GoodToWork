﻿namespace GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;

public class AppDatabaseConfiguration : IAppDatabaseConfiguration
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string UsersCollectionName { get; set; }
    public string EmailsCollectionName { get; set; }
}
