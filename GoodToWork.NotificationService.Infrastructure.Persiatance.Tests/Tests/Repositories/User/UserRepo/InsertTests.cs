using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.AppDatabaseConfiguration;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.DatabaseReset;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.UserCollection;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Shared;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Tests.Repositories.User.UserRepo;

public class InsertTests
{
    [Fact]
    public async Task CorrectInsert()
    {
        var testDataProvider = new TestDataProvider<UserEntity>();
        await testDataProvider.ResetCollection(MockUserEntities.Empty);

        var mockedAppDatabaseConfiguration = MockAppDatabaseConfiguration.Mock();

        var testedUnit = new SharedRepository<UserEntity>(mockedAppDatabaseConfiguration.Object);

        var userEntity = new UserEntity()
        {
            Email = "test@test.test",
            UserName = "test"
        };

        await testedUnit.Insert(userEntity);

        Assert.Single(await testDataProvider.GetAll());
    }
}
