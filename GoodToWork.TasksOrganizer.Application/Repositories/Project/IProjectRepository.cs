using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Shared;
using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task<List<ProjectEntity>> GetWithUsers(Func<ProjectEntity, bool> filter);
    Task<List<ProjectEntity>> GetWithUsers();
    Task<List<ProjectEntity>> GetWithProblems(Func<ProjectEntity, bool> filter);
}
