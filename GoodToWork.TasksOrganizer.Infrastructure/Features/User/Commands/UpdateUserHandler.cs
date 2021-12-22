using GoodToWork.TasksOrganizer.Application.Features.User.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.User.Commands;

internal sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly AppDbContext _appDbContext;

    public UpdateUserHandler(
        AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _appDbContext.Users.FirstOrDefault(e => e.Id == request.UserId);

        if (user is not null)
        {
            user.Name = request.Username;
            _appDbContext.Users.Update(user);
        }

        if (user is null)
        {
            user = new UserEntity()
            {
                Id = request.UserId,
                Name = request.Username
            };

            await _appDbContext.Users.AddAsync(user);
        }

        await _appDbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
