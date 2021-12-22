namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual List<ProjectUserEntity>? ProjectUsers { get; set; }
    public virtual List<TaskEntity>? CreatorTasks { get; set; }
    public virtual List<TaskEntity>? PerformerTasks { get; set; }
    public virtual List<CommentEntity>? Comments { get; set; }
}
