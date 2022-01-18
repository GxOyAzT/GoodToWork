using GoodToWork.Shared.MessageBroker.DTOs.Shared;

namespace GoodToWork.Shared.MessageBroker.Infrastructure.Interfaces;

public interface IEventHandler<TEvent> where TEvent : BaseEvent
{
    Task HandleAsync(TEvent @event);
}
