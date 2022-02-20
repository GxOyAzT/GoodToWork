using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Queries;

public sealed record GetProjectForEditQuery(Guid ProjectId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<ProjectEditModel>;

public sealed class GetProjectForEditHandler : IRequestHandler<GetProjectForEditQuery, ProjectEditModel>
{
    private readonly IAppRepository _appRepository;

    public GetProjectForEditHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<ProjectEditModel> Handle(GetProjectForEditQuery request, CancellationToken cancellationToken)
    {
        var project = (await _appRepository.Projects.GetWithUsers(p => p.Id == request.ProjectId)).FirstOrDefault();

        if (project == null)
        {
            throw new CannnotFindException($"Cannot find project of ID: {request.ProjectId}", HttpStatusCode.NotFound);
        }

        var avaliableUsers = (await _appRepository.Users.Get()).Where(u => !project.ProjectUsers.Select(pu => pu.UserId).Contains(u.Id)).ToList();

        return new ProjectEditModel(project, avaliableUsers);
    }
}