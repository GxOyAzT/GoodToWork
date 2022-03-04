using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Shared;
using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;

public interface IProblemRepository : IBaseRepository<ProblemEntity>
{
    Task<ProblemEntity> FindProblemWithStatusesComments(Func<ProblemEntity, bool> filter);
}
