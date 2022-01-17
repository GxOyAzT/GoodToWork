using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToWork.NotificationService.Infrastructure.Persiatance.Tests.Mocked.DatabaseReset;

internal class TestDataProvider<TCollection>
{
    private readonly IMongoCollection<TCollection> _collection;

    public TestDataProvider()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("NotificationServiceTest");
        _collection = database.GetCollection<TCollection>(typeof(TCollection).Name);
    }

    public async Task ResetCollection(List<TCollection> entities)
    {
        await DeleteMany();
        if (entities.Any())
            await InsertMany(entities);
    }

    public async Task<List<TCollection>> GetAll() =>
        (await _collection.FindAsync(c => true)).ToList();

    private async Task DeleteMany() =>
        await _collection.DeleteManyAsync(e => true);

    private async Task InsertMany(List<TCollection> entities) =>
        await _collection.InsertManyAsync(entities);
}
