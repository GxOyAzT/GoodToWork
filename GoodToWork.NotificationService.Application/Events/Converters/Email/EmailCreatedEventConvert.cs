using GoodToWork.NotificationService.Application.Features.Email.Commands;
using GoodToWork.Shared.MessageBroker.DTOs.Email;

namespace GoodToWork.NotificationService.Application.Events.Converters.Email;

internal static class EmailCreatedEventConvert
{
    public static CreateEmailCommand Convert(EmailCreatedEvent createdEvent) =>
        new CreateEmailCommand(createdEvent.RecipientId, createdEvent.Title, createdEvent.Contents);
}