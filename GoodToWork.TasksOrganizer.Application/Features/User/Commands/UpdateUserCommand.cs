using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.User.Commands;

public sealed record UpdateUserCommand(Guid UserId, string Username) : IRequest<Unit>;

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
        var user = await _appRepository.Users.Find(u => u.Id == request.UserId);

        if (user is not null)
        {
            user.Name = request.Username;
            await _appRepository.Users.Update(user);
        }

        if (user is null)
        {
            user = new UserEntity()
            {
                Id = request.UserId,
                Name = request.Username
            };

            await _appRepository.Users.Add(user);
        }

        await _appRepository.SaveChanges();

        return Unit.Value;
    }
}