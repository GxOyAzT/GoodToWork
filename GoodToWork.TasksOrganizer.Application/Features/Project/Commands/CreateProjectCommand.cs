using GoodToWork.TasksOrganizer.Application.Builders.Entities.Project;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Commands;

public record CreateProjectCommand(string Name, string Description, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<ProjectEntity>;

public sealed class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectEntity>
{
    private readonly IMediator _mediator;
    private readonly IAppRepository _appRepository;
    private readonly ICurrentDateTime _currentDateTime;

    public CreateProjectHandler(
        IMediator mediator,
        IAppRepository appRepository,
        ICurrentDateTime currentDateTime)
    {
        _mediator = mediator;
        _appRepository = appRepository;
        _currentDateTime = currentDateTime;
    }

    public async Task<ProjectEntity> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ValidateProjectInputQuery(request.Name, request.Description));

        var newProject = ProjectEntityBuilder
                            .Create(_currentDateTime)
                            .WithName(request.Name)
                            .WithDescription(request.Description)
                            .WithCreator(request.SenderId)
                            .Build();

        var insertedProject = await _appRepository.Projects.Add(newProject);

        await _appRepository.SaveChangesAsync();

        return insertedProject;
    }
}