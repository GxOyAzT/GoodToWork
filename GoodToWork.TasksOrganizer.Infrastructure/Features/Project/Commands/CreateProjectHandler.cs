using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Builders.Entities.Project;
using GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Persistance.Repositories.AppRepo;
using MediatR;

namespace GoodToWork.TasksOrganizer.Infrastructure.Features.Project.Commands;

internal sealed class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectEntity>
{
    private readonly IMediator _mediator;
    private readonly IAppRepository _appRepository;

    public CreateProjectHandler(
        IMediator mediator,
        IAppRepository appRepository)
    {
        _mediator = mediator;
        _appRepository = appRepository;
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

        return await _appRepository.Projects.Add(newProject);
    }
}
