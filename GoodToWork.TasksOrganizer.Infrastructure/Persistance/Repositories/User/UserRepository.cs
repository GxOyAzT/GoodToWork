using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.User;

internal class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
    }
}
