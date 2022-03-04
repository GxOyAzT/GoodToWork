using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Project;

public class ProjectBaseModel
{
    private readonly ProjectEntity projectEntity;

    public ProjectBaseModel(ProjectEntity projectEntity)
    {
        this.projectEntity = projectEntity;
    }

    public Guid Id { get => projectEntity.Id; }
    public string Name { get => projectEntity.Name; }
    public string Description { get => projectEntity.Description; }
    public DateTime Created { get => projectEntity.Created; }
    public bool IsActive { get => projectEntity.IsActive; }
    public int CoworkersCount { get => projectEntity.ProjectUsers == null ? 0 : projectEntity.ProjectUsers.Count; }
    public IEnumerable<Guid> ModeratorIds { get => projectEntity.ProjectUsers.Where(pu => pu.Role == Domain.Enums.UserProjectRoleEnum.Moderator).Select(pu => pu.UserId); }
}
