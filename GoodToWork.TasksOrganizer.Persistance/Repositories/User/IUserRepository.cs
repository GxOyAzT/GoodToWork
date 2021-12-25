using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Persistance.Repositories.Shared;

namespace GoodToWork.TasksOrganizer.Persistance.Repositories.User;

internal interface IUserRepository : IBaseRepository<UserEntity>
{
}
