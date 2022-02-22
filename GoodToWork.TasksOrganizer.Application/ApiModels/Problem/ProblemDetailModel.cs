using GoodToWork.TasksOrganizer.Application.ApiModels.Comment;
using GoodToWork.TasksOrganizer.Application.ApiModels.Status;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Problem;

public class ProblemDetailModel
{
    private readonly ProblemEntity _problemEntity;
    private readonly ICurrentDateTime _currentDateTime;

    public ProblemDetailModel(
        ProblemEntity problemEntity,
        ICurrentDateTime currentDateTime)
    {
        _problemEntity = problemEntity;
        _currentDateTime = currentDateTime;
    }

    public Guid Id { get => _problemEntity.Id; }
    public string Title { get => _problemEntity.Title; }
    public string Description { get => _problemEntity.Description; }
    public string Created { get => _problemEntity.Created.ToString("hh:mm dd-MM-yyyy"); }

    public string CreatorName { get => _problemEntity.Creator.Name; }
    public string PerformerName { get => _problemEntity.Performer.Name; }

    public ProblemStatusEnum ProblemStatus { get => _problemEntity.Statuses.OrderByDescending(s => s.Updated).FirstOrDefault().Status; }

    public IEnumerable<StatusBaseModel> Statuses { get => _problemEntity.Statuses.OrderBy(s => s.Updated).Select(s => new StatusBaseModel(s)); }

    public IEnumerable<CommentBaseModel> Comments { get => _problemEntity.Comments.Where(c => !c.IsDeleted).OrderBy(c => c.Created).Select(c => new CommentBaseModel(c, _currentDateTime)); }
}
