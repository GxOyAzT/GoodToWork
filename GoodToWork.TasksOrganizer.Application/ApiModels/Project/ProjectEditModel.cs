using GoodToWork.TasksOrganizer.Application.ApiModels.User;
using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Project;
public class ProjectEditModel
{
    private readonly ProjectEntity projectEntity;
    private readonly List<UserEntity> avaliableUsers;

    public ProjectEditModel(ProjectEntity projectEntity, List<UserEntity> avaliableUsers)
    {
        this.projectEntity = projectEntity;
        this.avaliableUsers = avaliableUsers;
    }

    public Guid Id { get => projectEntity.Id; }
    public string Name { get => projectEntity.Name; }
    public string Description { get => projectEntity.Description; }
    public string Created { get => projectEntity.Created.ToString("dd - MM - yyyy"); }
    public bool IsActive { get => projectEntity.IsActive; }
    public IEnumerable<UserBaseModel> AddedUsers { get => projectEntity.ProjectUsers.Select(pu => pu.User).Select(u => new UserBaseModel() { Id = u.Id, Name = u.Name }); }
    public IEnumerable<UserBaseModel> AvaliableUsers { get => avaliableUsers.Select(u => new UserBaseModel() { Id = u.Id, Name = u.Name }); }
}
