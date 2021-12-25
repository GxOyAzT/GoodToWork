using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Persistance.Repositories.AppRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;

internal sealed class GetProjectsHandler : IRequestHandler<GetProjectsQuery, List<ProjectEntity>>
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