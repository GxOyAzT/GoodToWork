using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.Builders.Entities.Project;

public class ProjectEntityBuilder :
    IWithName,
    IWithDescription,
    IWithCreator,
    IBuild
{
    private readonly ICurrentDateTime _currentDateTime;

    public ProjectEntity Project { get; set; }

    private ProjectEntityBuilder(ICurrentDateTime currentDateTime)
    {
        Project = new ProjectEntity()
        {
            ProjectUsers = new List<ProjectUserEntity>()
        };
        _currentDateTime = currentDateTime;
    }

    public static IWithName Create(ICurrentDateTime currentDateTime) => new ProjectEntityBuilder(currentDateTime);

    public IWithDescription WithName(string name)
    {
        Project.Name = name;
        return this;
    }

    public IWithCreator WithDescription(string description)
    {
        Project.Description = description;
        return this;
    }

    public IBuild WithCreator(Guid creatorId)
    {
        Project.IsActive = true;
        Project.Created = _currentDateTime.CurrentDateTime;
        Project.ProjectUsers.Add(
            new ProjectUserEntity() 
            { 
                UserId = creatorId, 
                Role = UserProjectRoleEnum.Moderator 
            });
        return this;
    }

    public ProjectEntity Build() => Project;
}

public interface IWithName
{
    IWithDescription WithName(string name);
}

public interface IWithDescription
{
    IWithCreator WithDescription(string description);
}

public interface IWithCreator
{
    IBuild WithCreator(Guid creatorId);
}

public interface IBuild
{
    ProjectEntity Build();
}