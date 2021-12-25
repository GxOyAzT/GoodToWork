using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Persistance.Repositories.Shared;

namespace GoodToWork.TasksOrganizer.Persistance.Repositories.Project;

internal interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task<List<ProjectEntity>> GetWithUsers(Func<ProjectEntity, bool> filter);
    Task<List<ProjectEntity>> GetWithUsers();
}
