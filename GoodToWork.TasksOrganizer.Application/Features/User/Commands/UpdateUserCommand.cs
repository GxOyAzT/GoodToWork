using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.User.Commands;

public sealed record UpdateUserCommand(Guid UserId, string Username) : IRequest<Unit>;