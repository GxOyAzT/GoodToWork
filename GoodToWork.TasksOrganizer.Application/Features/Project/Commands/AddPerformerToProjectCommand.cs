using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Access;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Commands;

public sealed record AddPerformerToProjectCommand(Guid ProjectId, Guid NewPerformerId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<Unit>;

public class AddPerformerToProjectHandler : IRequestHandler<AddPerformerToProjectCommand, Unit>
{
    private readonly IAppRepository _appRepository;

    public AddPerformerToProjectHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<Unit> Handle(AddPerformerToProjectCommand request, CancellationToken cancellationToken)
    {
        var project = (await _appRepository.Projects.GetWithUsers(x => x.Id == request.ProjectId))
            .FirstOrDefault();

        if (project == null)
        {
            throw new CannnotFindException($"Cannot find project of id {request.ProjectId}", HttpStatusCode.NotFound);
        }

        if (!project.ProjectUsers.Any(e => e.UserId == request.SenderId && e.Role == UserProjectRoleEnum.Moderator))
        {
            throw new NoAccessException("You do not have access for adding new performers.", HttpStatusCode.Forbidden);
        }

        if (project.ProjectUsers.Select(e => e.UserId).Contains(request.NewPerformerId))
        {
            throw new DomainException($"User of ID {request.NewPerformerId} already is binded to Project of ID {request.ProjectId}.", HttpStatusCode.BadRequest);
        }

        if (await _appRepository.Users.Find(user => user.Id == request.SenderId) is null)
        {
            throw new DomainException($"Cannot find user of ID {request.NewPerformerId}.", HttpStatusCode.BadRequest);
        }

        project.ProjectUsers.Add(new ProjectUserEntity()
        {
            ProjectId = project.Id,
            UserId = request.NewPerformerId,
            Role = UserProjectRoleEnum.Performer
        });

        await _appRepository.Projects.Update(project);

        await _appRepository.SaveChanges();

        return Unit.Value;
    }
}