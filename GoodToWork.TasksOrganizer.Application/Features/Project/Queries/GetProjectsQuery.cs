using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Queries;

public sealed record GetProjectsQuery(Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<List<ProjectEntity>>;

public sealed class GetProjectsHandler : IRequestHandler<GetProjectsQuery, List<ProjectEntity>>
{
    private readonly IAppRepository _appRepository;

    public GetProjectsHandler(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<List<ProjectEntity>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _appRepository.Projects.GetWithUsers();
    }
}