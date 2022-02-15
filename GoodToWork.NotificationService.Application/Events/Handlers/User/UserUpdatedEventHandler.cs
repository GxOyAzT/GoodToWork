using GoodToWork.NotificationService.Application.Events.Converters.User;
using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.User;
using MediatR;

namespace GoodToWork.NotificationService.Application.Events.Handlers.User;

public class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
{
    private readonly IMediator _mediator;
    
    public UserUpdatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleAsync(UserUpdatedEvent @event)
    {
        await _mediator.Send(UserUpdatedEventConvert.Convert(@event));
    }
}
