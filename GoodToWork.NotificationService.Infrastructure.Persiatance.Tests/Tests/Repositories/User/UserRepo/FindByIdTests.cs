using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.AppDatabaseConfiguration;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.DatabaseReset;
using GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.UserCollection;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Tests.Repositories.User.UserRepo;

public class FindByIdTests
{
    [Fact]
    public async Task Find()
    {
        var testDataProvider = new TestDataProvider<UserEntity>("Users");
        await testDataProvider.ResetCollection(MockUserEntities.Single);

        var mockedAppDatabaseConfiguration = MockAppDatabaseConfiguration.Mock();

        var testedUnit = new SharedRepository<UserEntity>(mockedAppDatabaseConfiguration.Object, mockedAppDatabaseConfiguration.Object.UsersCollectionName);

        var user = await testedUnit.Find(Guid.Parse("00000000-0000-0000-0000-000000000001"));

        Assert.Equal("user1@test.com", user.Email);
    }

    [Fact]
    public async Task UserOfIdNotExists()
    {
        var testDataProvider = new TestDataProvider<UserEntity>("Users");
        await testDataProvider.ResetCollection(MockUserEntities.Three);

        var mockedAppDatabaseConfiguration = MockAppDatabaseConfiguration.Mock();

        var testedUnit = new SharedRepository<UserEntity>(mockedAppDatabaseConfiguration.Object, mockedAppDatabaseConfiguration.Object.UsersCollectionName);

        var user = await testedUnit.Find(Guid.Parse("00000000-0000-0000-0000-000000000004"));

        Assert.Null(user);
    }

    [Fact]
    public async Task EmptyCollection()
    {
        var testDataProvider = new TestDataProvider<UserEntity>("Users");
        await testDataProvider.ResetCollection(MockUserEntities.Empty);

        var mockedAppDatabaseConfiguration = MockAppDatabaseConfiguration.Mock();

        var testedUnit = new SharedRepository<UserEntity>(mockedAppDatabaseConfiguration.Object, mockedAppDatabaseConfiguration.Object.UsersCollectionName);

        var user = await testedUnit.Find(Guid.Parse("00000000-0000-0000-0000-000000000001"));

        Assert.Null(user);
    }
}
