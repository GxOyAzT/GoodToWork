using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Domain.Entities;

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
    public IEnumerable<ProblemBaseModel> Problems { get => _project.Problems.Select(p => new ProblemBaseModel(p)); }
}
