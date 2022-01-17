using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using MongoDB.Driver;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.User;

internal class UserRepository : ISharedRepository<UserEntity>
{
    private readonly IMongoCollection<UserEntity> _users;

    public UserRepository(
        IAppDatabaseConfiguration configuration)
    {
        var client = new MongoClient(configuration.ConnectionString);
        var database = client.GetDatabase(configuration.DatabaseName);

        _users = database.GetCollection<UserEntity>(configuration.UsersCollectionName);
    }

    public Task Delete(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> Find(Guid id) =>
        await (await _users.FindAsync(u => u.Id == id)).FirstOrDefaultAsync();

    public Task<List<UserEntity>> Get()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(UserEntity entity) =>
        await _users.InsertOneAsync(entity);

    public Task Update(UserEntity entity)
    {
        throw new NotImplementedException();
    }
}
