using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Problem;

public class ProblemBaseModel
{
    private readonly ProblemEntity _problem;

    public ProblemBaseModel(ProblemEntity problem)
    {
        _problem = problem;
    }

    public Guid Id { get => _problem.Id; }
    public string Title { get => _problem.Title; }
    public string PerformerName { get => _problem.Performer.Name; }
    public ProblemStatusEnum ProblemStatus { get => _problem.Statuses.OrderByDescending(s => s.Updated).FirstOrDefault().Status; }
}
