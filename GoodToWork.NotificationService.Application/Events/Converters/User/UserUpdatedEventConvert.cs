using GoodToWork.NotificationService.Application.Features.User.Commands;
using GoodToWork.Shared.MessageBroker.DTOs.User;

namespace GoodToWork.NotificationService.Application.Events.Converters.User;

internal static class UserUpdatedEventConvert
{
    public static UpdateUserCommand Convert(UserUpdatedEvent userUpdate) =>
        new UpdateUserCommand(userUpdate.Id, userUpdate.UserName, userUpdate.Email);
}
