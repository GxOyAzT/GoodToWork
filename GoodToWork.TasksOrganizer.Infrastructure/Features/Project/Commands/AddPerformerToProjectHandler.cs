using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Domain.Enums;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Access;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Entities;
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

        if (project.ProjectUsers is not null && project.ProjectUsers.Any(e => e.UserId == request.SenderId && e.Role == UserProjectRoleEnum.Moderator))
        {
            throw new NoAccessException("You do not have access for adding new performers.", HttpStatusCode.);
        }


    }
}
