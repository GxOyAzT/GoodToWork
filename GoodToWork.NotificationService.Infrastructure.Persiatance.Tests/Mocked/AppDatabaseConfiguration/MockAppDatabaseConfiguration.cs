using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using Moq;

namespace GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.AppDatabaseConfiguration;

internal class MockAppDatabaseConfiguration
{
    public static Mock<IAppDatabaseConfiguration> Mock()
    {
        var mockedAppDatabaseConfiguration = new Mock<IAppDatabaseConfiguration>();

        mockedAppDatabaseConfiguration.Setup(madc => madc.ConnectionString).Returns("mongodb://localhost:27017");
        mockedAppDatabaseConfiguration.Setup(madc => madc.DatabaseName).Returns("NotificationServiceTest");
        mockedAppDatabaseConfiguration.Setup(madc => madc.UsersCollectionName).Returns("Users");

        return mockedAppDatabaseConfiguration;
    }
}
