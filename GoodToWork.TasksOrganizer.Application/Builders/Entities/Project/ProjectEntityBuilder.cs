using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.Builders.Entities.Project;

public class ProjectEntityBuilder :
    IWithName,
    IWithDescription,
    IWithCreator,
    IBuild
{
    public ProjectEntity Project { get; set; }

    private ProjectEntityBuilder()
    {
        Project = new ProjectEntity()
        {
            ProjectUsers = new List<ProjectUserEntity>()
        };
    }

    public static IWithName Create() => new ProjectEntityBuilder();

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