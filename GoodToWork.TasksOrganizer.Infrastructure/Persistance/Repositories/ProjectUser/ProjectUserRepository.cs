using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.ProjectUser;

internal class ProjectUserRepository : BaseRepository<ProjectUserEntity>, IProjectUserRepository
{
    public ProjectUserRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
    }
}
