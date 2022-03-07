using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Repositories.Message;

public interface IMessageRepository : ISharedRepository<MessageEntity>
{
    public Task<List<MessageEntity>> GetChat(Guid senderId, Guid receiverId, int interval);
}
