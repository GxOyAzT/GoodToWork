using GoodToWork.TasksOrganizer.Domain.Entities.Shared;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class CommentEntity : BaseEntity
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }
    public Guid CreatorId { get; set; }

    public string Comment { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    

    public virtual UserEntity? Creator { get; set; }
    public virtual ProblemEntity? Problem { get; set; }
}
