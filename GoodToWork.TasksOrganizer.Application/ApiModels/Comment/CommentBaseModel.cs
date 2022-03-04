using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Comment;

public class CommentBaseModel
{
    private readonly CommentEntity _commentEntity;
    private readonly ICurrentDateTime _currentDateTime;

    public CommentBaseModel(
        CommentEntity commentEntity,
        ICurrentDateTime currentDateTime)
    {
        _commentEntity = commentEntity;
        _currentDateTime = currentDateTime;
    }

    public Guid Id { get => _commentEntity.Id; }
    public string Comment { get => _commentEntity.Comment; }
    public string CreatorName { get => _commentEntity.Creator.Name; }
    public string Created { get => (_currentDateTime.CurrentDateTime - _commentEntity.Created).TotalDays > 1 ? _commentEntity.Created.ToString("hh:mm dd-MM-yyyy") : _commentEntity.Created.ToString("hh:mm"); }
}
