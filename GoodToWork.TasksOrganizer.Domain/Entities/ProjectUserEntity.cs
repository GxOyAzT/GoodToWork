using GoodToWork.TasksOrganizer.Domain.Entities.Shared;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class ProjectUserEntity : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public UserProjectRoleEnum Role { get; set; }

    public virtual UserEntity? User { get; set; }
    public virtual ProjectEntity? Project { get; set; }
}
