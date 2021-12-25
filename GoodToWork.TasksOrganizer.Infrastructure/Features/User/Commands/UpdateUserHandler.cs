using GoodToWork.TasksOrganizer.Application.Features.User.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Persistance.Repositories.User;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.User.Commands;

internal sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
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

        return Unit.Value;
    }
}
