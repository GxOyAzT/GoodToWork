using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities.Shared;
using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using MongoDB.Driver;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Shared;

public class SharedRepository<TEntity> : ISharedRepository<TEntity> 
    where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection;

    public SharedRepository(
        IAppDatabaseConfiguration configuration)
    {
        var client = new MongoClient(configuration.ConnectionString);
        var database = client.GetDatabase(configuration.DatabaseName);

        _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task Delete(TEntity entity) =>
        await _collection.DeleteOneAsync(e => e.Id == entity.Id);

    public async Task<TEntity> Find(Guid id) => 
        await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();

    public async Task<List<TEntity>> Get() =>
        await (await _collection.FindAsync(e => true)).ToListAsync();

    public async Task Insert(TEntity entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task Update(TEntity entity)
    {
        await Delete(entity);
        await Insert(entity);
    }
}
