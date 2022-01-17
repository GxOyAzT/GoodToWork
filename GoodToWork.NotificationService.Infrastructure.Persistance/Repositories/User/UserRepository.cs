using GoodToWork.NotificationService.Application.Repositories.User;
using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using MongoDB.Driver;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.User;

internal class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserEntity> _users;

    public UserRepository(
        IAppDatabaseConfiguration configuration)
    {
        var client = new MongoClient(configuration.ConnectionString);
        var database = client.GetDatabase(configuration.DatabaseName);

        _users = database.GetCollection<UserEntity>(configuration.UsersCollectionName);
    }

    public async Task<UserEntity> FindById(Guid userId) =>
        (await _users.FindAsync(user => user.Id == userId)).FirstOrDefault();

    public async Task Insert(UserEntity user) =>
        await _users.InsertOneAsync(user);
}
