using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Problem.Commands;

public sealed record UpdateProblemStatusCommand(Guid ProblemId, ProblemStatusEnum ProblemStatus, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<Unit>;

public sealed class UpdateProblemStatusHandler : IRequestHandler<UpdateProblemStatusCommand, Unit>
{
    private readonly ICurrentDateTime _currentDateTime;
    private readonly IAppRepository _appRepository;

    public UpdateProblemStatusHandler(
        ICurrentDateTime currentDateTime,
        IAppRepository appRepository)
    {
        _currentDateTime = currentDateTime;
        _appRepository = appRepository;
    }

    public async Task<Unit> Handle(UpdateProblemStatusCommand request, CancellationToken cancellationToken)
    {
        var problem = await _appRepository.Problems.FindProblemWithStatusesComments(p => p.Id == request.ProblemId);

        if (problem is null)
        {
            throw new CannnotFindException($"Cannot find task.", HttpStatusCode.NotFound);
        }

        if (!(problem.CreatorId == request.SenderId || problem.PerformerId == request.SenderId))
        {
            throw new NoAccessException("You have no access to this task.", HttpStatusCode.Forbidden);
        }

        problem.Statuses.Add(new StatusEntity()
        {
            Status = request.ProblemStatus,
            Updated = _currentDateTime.CurrentDateTime,
            UpdatorId = request.SenderId
        });

        await _appRepository.SaveChangesAsync();

        return Unit.Value;
    }
}