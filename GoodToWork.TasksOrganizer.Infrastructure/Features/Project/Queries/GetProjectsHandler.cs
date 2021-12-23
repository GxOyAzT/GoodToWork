using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;

internal sealed class GetProjectsHandler : IRequestHandler<GetProjectsQuery, List<ProjectEntity>>
{
    private readonly AppDbContext _appDbContext;

    public GetProjectsHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ProjectEntity>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.ProjectUsers
            .Include(e => e.Project)
            .Where(e => e.UserId == request.SenderId)
            .Select(e => e.Project)
            .ToListAsync();
    }
}