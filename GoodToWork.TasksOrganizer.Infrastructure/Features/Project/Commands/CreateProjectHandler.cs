using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Builders.Entities.Project;
using GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Commands;

internal sealed class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectEntity>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMediator _mediator;

    public CreateProjectHandler(
        AppDbContext appDbContext,
        IMediator mediator)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
    }

    public async Task<ProjectEntity> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ValidateProjectInputQuery(request.Name, request.Description));

        var newProject = ProjectEntityBuilder
                            .Create()
                            .WithName(request.Name)
                            .WithDescription(request.Description)
                            .WithCreator(request.SenderId)
                            .Build();

        await _appDbContext.Projects.AddAsync(newProject);

        await _appDbContext.SaveChangesAsync();

        return newProject;
    }
}
