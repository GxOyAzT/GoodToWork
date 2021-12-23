using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Infrastructure.Builders.Entities.Project;

internal class ProjectEntityBuilder :
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

internal interface IWithName
{
    IWithDescription WithName(string name);
}

internal interface IWithDescription
{
    IWithCreator WithDescription(string description);
}

internal interface IWithCreator
{
    IBuild WithCreator(Guid creatorId);
}

internal interface IBuild
{
    ProjectEntity Build();
}