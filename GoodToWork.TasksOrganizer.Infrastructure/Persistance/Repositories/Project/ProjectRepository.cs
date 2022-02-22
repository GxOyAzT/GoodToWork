using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Project;

internal class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    public ProjectRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
    }

    public Task<List<ProjectEntity>> GetWithProblemsAndUsers(Func<ProjectEntity, bool> filter) =>
        Task.FromResult(_appDbContext.Set<ProjectEntity>()
            .Include(p => p.Problems).ThenInclude(p => p.Statuses)
            .Include(p => p.Problems).ThenInclude(p => p.Performer)
            .Include(p => p.ProjectUsers).ThenInclude(pu => pu.User) 
            .Where(filter)
            .ToList());

    public Task<List<ProjectEntity>> GetWithUsers(Func<ProjectEntity, bool> filter) =>
        Task.FromResult(_appDbContext.Set<ProjectEntity>()
            .Include(p => p.ProjectUsers).ThenInclude(pu => pu.User)
            .Where(filter)
            .ToList());

    public async Task<List<ProjectEntity>> GetWithUsers() =>
        await _appDbContext.Set<ProjectEntity>()
            .Include(p => p.ProjectUsers).ThenInclude(pu => pu.User)
            .ToListAsync();
}
