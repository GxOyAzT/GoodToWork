using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Queries;

public sealed record GetSenderProjectsQuery(Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<List<ProjectEntity>>;

public sealed class GetSenderProjectsHandler : IRequestHandler<GetSenderProjectsQuery, List<ProjectEntity>>
{
    private readonly IAppRepository _appRepository;

    public GetSenderProjectsHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<List<ProjectEntity>> Handle(GetSenderProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _appRepository.Projects.GetWithUsers(p => p.IsActive && p.ProjectUsers.Select(pu => pu.UserId).Contains(request.SenderId));

        if (!projects.Any())
        {
            throw new CannnotFindException("You are not assigned to any project yet.", HttpStatusCode.NotFound);
        }

        return projects;
    }
}