using GoodToWork.TasksOrganizer.Domain.Entities.Shared;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class StatusEntity : BaseEntity
{
    public Guid Id { get; set; }
    public Guid ProblemId { get; set; }
    public TaskStatusEnum Status { get; set; }
    public DateTime Updated { get; set; }

    public virtual ProblemEntity? Problem { get; set; }
}
