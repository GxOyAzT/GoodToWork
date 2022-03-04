using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Enums;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Commands;

public sealed record UpdatePerformerRoleCommand(Guid SenderId, Guid UserId, Guid ProjectId, UserProjectRoleEnum Role) : BaseSenderIdRequest(SenderId), IRequest<Unit>;

public sealed class UpdatePerformerRoleHandler : IRequestHandler<UpdatePerformerRoleCommand, Unit>
{
    private readonly IAppRepository _appRepository;

    public UpdatePerformerRoleHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<Unit> Handle(UpdatePerformerRoleCommand request, CancellationToken cancellationToken)
    {
        var project = (await _appRepository.Projects.GetWithUsers(p => p.Id == request.ProjectId)).FirstOrDefault();

        if (project is null)
        {
            throw new CannnotFindException($"Cannot find project of ID: {request.ProjectId}", HttpStatusCode.NotFound);
        }

        var projectSender = project.ProjectUsers.FirstOrDefault(p => p.ProjectId == request.ProjectId && p.UserId == request.SenderId);

        if (projectSender is null)
        {
            throw new CannnotFindException($"Cannot find project-user (sender) of ID: project {request.ProjectId} user {request.UserId}", HttpStatusCode.NotFound);
        }

        if (!projectSender.Role.HasFlag(UserProjectRoleEnum.Moderator))
        {
            throw new NoAccessException("You have no permissions for modyfying this project.", HttpStatusCode.Forbidden);
        }

        var projectUser = project.ProjectUsers.FirstOrDefault(p => p.ProjectId == request.ProjectId && p.UserId == request.UserId);

        if (projectUser is null)
        {
            throw new CannnotFindException($"Cannot find project-user of ID: project {request.ProjectId} user {request.UserId}", HttpStatusCode.NotFound);
        }

        projectUser.Role = request.Role;

        await _appRepository.SaveChangesAsync();

        return Unit.Value;
    }
}