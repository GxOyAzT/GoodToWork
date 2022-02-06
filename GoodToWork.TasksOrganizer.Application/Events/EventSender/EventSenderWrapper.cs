using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.Shared;

namespace GoodToWork.TasksOrganizer.Application.Events.EventSender;

public class EventSenderWrapper : IEventSenderWrapper
{
    private readonly IEventSender _eventSender;

    public EventSenderWrapper(IEventSender eventSender)
    {
        _eventSender = eventSender;
    }

    public Task Send<TEvent>(TEvent @event) where TEvent : BaseEvent
    {
        _eventSender.Send(@event);

        return Task.CompletedTask;
    }
}

public interface IEventSenderWrapper
{
    Task Send<TEvent>(TEvent @event) 
        where TEvent : BaseEvent;
}