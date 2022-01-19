using GoodToWork.Shared.MessageBroker.DTOs.Shared;

namespace GoodToWork.Shared.MessageBroker.Application.Interfaces;

public interface IEventSender

{
    Task Send<TEvent>(TEvent @event)
        where TEvent : BaseEvent;
}