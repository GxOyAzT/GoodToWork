using GoodToWork.NotificationService.Application.Repositories.Message;
using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Shared;
using MongoDB.Driver;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories.Message;

internal class MessageRepository : SharedRepository<MessageEntity>, IMessageRepository
{
    public MessageRepository(IAppDatabaseConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<List<MessageEntity>> GetChat(Guid senderId, Guid receiverId, int interval)
    {
        var builder = Builders<MessageEntity>.Filter;

        var filter = builder.Or(
            builder.And(builder.Eq(x => x.SenderId, senderId), builder.Eq(x => x.ReceiverId, receiverId)),
            builder.And(builder.Eq(x => x.SenderId, receiverId), builder.Eq(x => x.ReceiverId, senderId)));

        return (await _collection.FindAsync(filter)).ToList();
    }
}
