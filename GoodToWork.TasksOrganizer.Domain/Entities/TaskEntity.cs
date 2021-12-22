using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }
    public Guid PerformerId { get; set; }
    public Guid ProjectId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }

    public virtual UserEntity? Creator { get; set; }
    public virtual UserEntity? Performer { get; set; }
    public virtual ProjectEntity? Project { get; set; }
    public virtual List<CommentEntity>? Comments { get; set; }
    public virtual List<StatusEntity>? Statuses { get; set; }
}
