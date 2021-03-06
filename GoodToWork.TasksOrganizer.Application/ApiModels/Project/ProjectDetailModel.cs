using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Application.ApiModels.User;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Project;

public class ProjectDetailModel
{
    private readonly ProjectEntity _project;

    public ProjectDetailModel(ProjectEntity project)
    {
        _project = project;
    }

    public Guid Id { get => _project.Id; }
    public string Name { get => _project.Name; }
    public string Description { get => _project.Description; }
    public bool HasCreateRole { get; private set; }
    public IEnumerable<ProblemBaseModel> Problems { get => _project.Problems.Select(p => new ProblemBaseModel(p)); }
    public IEnumerable<UserBaseModel> Performers { get => _project.ProjectUsers.Where(e => e.Role.HasFlag(UserProjectRoleEnum.Performer)).Select(pu => new UserBaseModel(pu.User)); }

    public void AddSenderPermissions(Guid senderId) => HasCreateRole = _project.ProjectUsers.Any(pu => pu.UserId == senderId && (UserProjectRoleEnum.Creator | UserProjectRoleEnum.Moderator).HasFlag(pu.Role));
}
