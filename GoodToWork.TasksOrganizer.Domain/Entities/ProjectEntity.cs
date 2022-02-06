using GoodToWork.TasksOrganizer.Domain.Entities.Shared;

namespace GoodToWork.TasksOrganizer.Domain.Entities;

public class ProjectEntity : BaseEntity
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }

    public virtual List<ProjectUserEntity>? ProjectUsers { get; set; }
    public virtual List<ProblemEntity>? Problems { get; set; }
}
