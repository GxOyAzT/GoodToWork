using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Domain.Enums;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Access;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Entities;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Commands;

internal class AddPerformerToProjectHandler : IRequestHandler<AddPerformerToProjectCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public AddPerformerToProjectHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(AddPerformerToProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _appDbContext.Projects
            .Include(e => e.ProjectUsers)
            .FirstOrDefaultAsync(e => e.Id == request.ProjectId);

        if (project == null)
        {
            throw new CannnotFindException($"Cannot find project of id {request.ProjectId}", HttpStatusCode.NotFound);
        }

        if (project.ProjectUsers.Any(e => e.UserId == request.SenderId && e.Role == UserProjectRoleEnum.Moderator))
        {
            throw new NoAccessException("You do not have access for adding new performers.", HttpStatusCode.Forbidden);
        }

        if (project.ProjectUsers.Select(e => e.UserId).Contains(request.NewPerformerId))
        {
            throw new DomainException($"User of ID {request.NewPerformerId} already is binded to Project of ID {request.ProjectId}.", HttpStatusCode.BadRequest);
        }

        if (await _appDbContext.Users.FirstOrDefaultAsync(e => e.Id == request.NewPerformerId) is null)
        {
            throw new DomainException($"Cannot find user of ID {request.NewPerformerId}.", HttpStatusCode.BadRequest);
        }

        project.ProjectUsers.Add(new ProjectUserEntity()
        {
            ProjectId = project.Id,
            UserId = request.NewPerformerId,
            Role = UserProjectRoleEnum.Performer
        });

        await _appDbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
