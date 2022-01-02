using GoodToWork.TasksOrganizer.Domain.Entities.Shared;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class UserEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual List<ProjectUserEntity>? ProjectUsers { get; set; }
    public virtual List<ProblemEntity>? CreatorProblems { get; set; }
    public virtual List<ProblemEntity>? PerformerProblems { get; set; }
    public virtual List<CommentEntity>? Comments { get; set; }
}
