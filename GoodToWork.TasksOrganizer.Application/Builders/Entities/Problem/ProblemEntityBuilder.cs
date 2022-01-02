using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.Builders.Entities.Problem;

public class ProblemEntityBuilder :
    IWithTitle,
    IWithDescription,
    IWithProjectId,
    IWithPerformerId,
    IWithCreatorId,
    IBuild
{
    ProblemEntity Problem { get; set; }

    ICurrentDateTime _currentDateTime { get; set; }

    private ProblemEntityBuilder(ICurrentDateTime currentDateTime)
    {
        _currentDateTime = currentDateTime;
        Problem = new ProblemEntity();
    }
        
    public static IWithTitle Create(ICurrentDateTime currentDateTime)
    {
        return new ProblemEntityBuilder(currentDateTime);
    }

    public ProblemEntity Build()
    {
        Problem.Created = _currentDateTime.CurrentDateTime;
        return Problem;
    }
        
    public IBuild WithCreatorId(Guid creatorId)
    {
        Problem.CreatorId = creatorId;
        return this;
    }

    public IWithProjectId WithDescription(string description)
    {
        Problem.Description = description;
        return this;
    }

    public IWithCreatorId WithPerfomerId(Guid performerId)
    {
        Problem.PerformerId = performerId;
        return this;
    }

    public IWithPerformerId WithProjectId(Guid projectId)
    {
        Problem.ProjectId = projectId;
        return this;
    }

    public IWithDescription WithTitle(string title)
    {
        Problem.Title = title;
        return this;
    }
}

public interface IWithTitle
{
    IWithDescription WithTitle(string title);
}

public interface IWithDescription
{
    IWithProjectId WithDescription(string description);
}

public interface IWithProjectId
{
    IWithPerformerId WithProjectId(Guid projectId);
}

public interface IWithPerformerId
{
    IWithCreatorId WithPerfomerId(Guid performerId);
}

public interface IWithCreatorId
{
    IBuild WithCreatorId(Guid creatorId);
}

public interface IBuild
{
    ProblemEntity Build();
}