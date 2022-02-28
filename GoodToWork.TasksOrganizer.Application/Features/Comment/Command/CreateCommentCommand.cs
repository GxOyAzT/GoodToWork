using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Application.ApiModels.Comment;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Comment.Command;

public sealed record CreateCommentCommand(string Comment, Guid ProblemId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<CommentBaseModel>;

public sealed class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentBaseModel>
{
    private readonly IAppRepository _appRepository;
    private readonly ICurrentDateTime _currentDateTime;

    public CreateCommentHandler(
        IAppRepository appRepository,
        ICurrentDateTime currentDateTime)
    {
        _appRepository = appRepository;
        _currentDateTime = currentDateTime;
    }

    public async Task<CommentBaseModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var problem = await _appRepository.Problems.FindProblemWithStatusesComments(p => p.Id == request.ProblemId);

        if (problem == null)
        {
            throw new CannnotFindException($"Cannot find Task of id {request.ProblemId}", HttpStatusCode.NotFound);
        }

        if (String.IsNullOrEmpty(request.Comment))
        {
            throw new ValidationFailedException("Cannot create empty content comment.", HttpStatusCode.BadRequest, null);
        }

        if (!(problem.CreatorId == request.SenderId || problem.PerformerId == request.SenderId))
        {
            throw new NoAccessException("You have no access to this task.", HttpStatusCode.Unauthorized);
        }

        var comment = new CommentEntity()
        {
            Comment = request.Comment,
            Created = _currentDateTime.CurrentDateTime,
            CreatorId = request.SenderId,
            IsDeleted = false
        };

        problem.Comments.Add(comment);

        await _appRepository.SaveChangesAsync();

        return new CommentBaseModel(comment, _currentDateTime);
    }
}