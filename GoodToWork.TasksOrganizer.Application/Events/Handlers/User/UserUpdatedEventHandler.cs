using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.User;
using GoodToWork.TasksOrganizer.Application.Events.Converters.User;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Events.Handlers.User;

internal class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
{
    private readonly IMediator _mediator;

    public UserUpdatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task HandleAsync(UserUpdatedEvent @event)
    {
        _mediator.Send(UserUpdatedEventConverter.Convert(@event));

        return Task.CompletedTask;
    }
}
