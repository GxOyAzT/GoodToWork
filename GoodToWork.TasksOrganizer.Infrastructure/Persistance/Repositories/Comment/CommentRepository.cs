using GoodToWork.TasksOrganizer.Application.Repositories.Comment;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Shared;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Comment;

internal class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
    }
}
