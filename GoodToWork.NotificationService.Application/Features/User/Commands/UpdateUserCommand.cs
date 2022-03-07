using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities;
using MediatR;

namespace GoodToWork.NotificationService.Application.Features.User.Commands;

public sealed record UpdateUserCommand(Guid UserId, string Username, string Email) : IRequest<Unit>;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IAppRepository _appRepository;

    public UpdateUserHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _appRepository.Users.Find(request.UserId);

        if (user is null)
        {
            user = new UserEntity()
            {
                Id = request.UserId,
                Email = request.Email,
                UserName = request.Username
            };

            await _appRepository.Users.Insert(user);

            return Unit.Value;
        }

        user.Email = request.Email;
        user.UserName = request.Username;

        await _appRepository.Users.Update(user);

        return Unit.Value;
    }
}