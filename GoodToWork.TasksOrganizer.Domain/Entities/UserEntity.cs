using GoodToWork.TasksOrganizer.Domain.Entities.Shared;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class UserEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual List<ProjectUserEntity> ProjectUsers { get; set; } = new List<ProjectUserEntity>();
    public virtual List<ProblemEntity> ProblemsCreator { get; set; } = new List<ProblemEntity>();
    public virtual List<ProblemEntity> ProblemsPerformer { get; set; } = new List<ProblemEntity>();
    public virtual List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}
