using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Problem;

internal class ProblemRepository : BaseRepository<ProblemEntity>, IProblemRepository
{
    public ProblemRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
    }

    public Task<ProblemEntity> FindProblemWithStatusesComments(Func<ProblemEntity, bool> filter) =>
        Task.FromResult(_appDbContext.Set<ProblemEntity>()
            .Include(p => p.Statuses).ThenInclude(s => s.Updator)
            .Include(p => p.Performer)
            .Include(p => p.Creator)
            .Include(p => p.Comments).ThenInclude(c => c.Creator)
            .FirstOrDefault(filter));
}
