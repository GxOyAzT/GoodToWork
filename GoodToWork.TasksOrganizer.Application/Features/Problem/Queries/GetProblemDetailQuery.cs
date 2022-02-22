using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Problem.Queries;

public sealed record GetProblemDetailQuery(Guid ProblemId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<ProblemDetailModel>;

public sealed class HetProblemDetailHandler : IRequestHandler<GetProblemDetailQuery, ProblemDetailModel>
{
    private readonly IAppRepository _appRepository;
    private readonly ICurrentDateTime _currentDateTime;

    public HetProblemDetailHandler(
        IAppRepository appRepository,
        ICurrentDateTime currentDateTime)
    {
        _appRepository = appRepository;
        _currentDateTime = currentDateTime;
    }

    public async Task<ProblemDetailModel> Handle(GetProblemDetailQuery request, CancellationToken cancellationToken)
    {
        var problem = await _appRepository.Problems.FindProblemWithStatusesComments(p => p.Id == request.ProblemId);

        if (problem is null)
        {
            throw new CannnotFindException($"Cannot find problem of ID: {request.ProblemId}", HttpStatusCode.NotFound);
        }

        return new ProblemDetailModel(problem, _currentDateTime);
    }
}
