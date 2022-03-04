using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.ProjectUser;

public class ProjectUserBaseModel
{
    private readonly ProjectUserEntity _projectUserEntity;

    public ProjectUserBaseModel(ProjectUserEntity projectUserEntity)
    {
        _projectUserEntity = projectUserEntity;
    }

    public Guid UserId { get => _projectUserEntity.UserId; }
    public Guid ProjectId { get => _projectUserEntity.ProjectId; }
    public string Name { get => _projectUserEntity.User.Name; }
    public UserProjectRoleEnum Role { get => _projectUserEntity.Role; }
}
