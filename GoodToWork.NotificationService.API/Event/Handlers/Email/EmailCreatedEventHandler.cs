using GoodToWork.NotificationService.API.Event.Converters.Email;
using GoodToWork.Shared.MessageBroker.DTOs.Email;
using GoodToWork.Shared.MessageBroker.Infrastructure.Interfaces;
using MediatR;

namespace GoodToWork.NotificationService.API.Event.Handlers.Email;

public class EmailCreatedEventHandler : IEventHandler<EmailCreatedEvent>
{
    private readonly IMediator _mediator;

    public EmailCreatedEventHandler(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleAsync(EmailCreatedEvent @event)
    {
        await _mediator.Send(EmailCreatedEventConvert.Convert(@event));
    }
}
