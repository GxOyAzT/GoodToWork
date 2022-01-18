using GoodToWork.Shared.MessageBroker.DTOs.Shared;

namespace GoodToWork.Shared.MessageBroker.DTOs.Email;

public class EmailCreatedEvent : BaseEvent
{
    public Guid RecipientId { get; set; }
    public string Title { get; set; }
    public string Contents { get; set; }
}
