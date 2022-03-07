using GoodToWork.NotificationService.Domain.Entities.Shared;

namespace GoodToWork.NotificationService.Domain.Entities;

public class MessageEntity : BaseEntity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Message { get; set; }
    public bool WasSeen { get; set; }
    public DateTime SentTime { get; set; }
}
