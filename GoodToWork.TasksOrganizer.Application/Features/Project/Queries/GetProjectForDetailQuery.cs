using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Queries;

public sealed record GetProjectForDetailQuery(Guid ProjectId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<ProjectDetailModel>;

public sealed class GetProjectForDetailHandler : IRequestHandler<GetProjectForDetailQuery, ProjectDetailModel>
{
    private readonly IAppRepository _appRepository;

    public GetProjectForDetailHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<ProjectDetailModel> Handle(GetProjectForDetailQuery request, CancellationToken cancellationToken)
    {
        var project = (await _appRepository.Projects.GetWithProblemsAndUsers(p => p.Id == request.ProjectId)).FirstOrDefault();

        if (project == null)
        {
            throw new CannnotFindException($"Cannot find project of ID: {request.ProjectId}", HttpStatusCode.NotFound);
        }

        var projectDetailModel = new ProjectDetailModel(project);

        projectDetailModel.AddSenderPermissions(request.SenderId);

        return projectDetailModel;
    }
}