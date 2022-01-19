using GoodToWork.Shared.MessageBroker.DTOs.User;
using GoodToWork.TasksOrganizer.Application.Features.User.Commands;

namespace GoodToWork.TasksOrganizer.Application.Events.Converters.User;

internal static class UserUpdatedEventConverter
{
    public static UpdateUserCommand Convert(UserUpdatedEvent userUpdated)
    {
        return new UpdateUserCommand(userUpdated.Id, userUpdated.UserName);
    }
}
