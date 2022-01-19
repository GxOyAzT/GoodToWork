using GoodToWork.NotificationService.Application.Events.Converters.Email;
using GoodToWork.Shared.MessageBroker.Application.Interfaces;
using GoodToWork.Shared.MessageBroker.DTOs.Email;
using MediatR;

namespace GoodToWork.NotificationService.Application.Events.Handlers.Email;

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
