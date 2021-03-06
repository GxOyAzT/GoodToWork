using GoodToWork.Shared.Common.Domain.Exceptions.Access;
using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.Shared.MessageBroker.DTOs.Email;
using GoodToWork.TasksOrganizer.Application.Builders.Entities.Problem;
using GoodToWork.TasksOrganizer.Application.Events.EventSender;
using GoodToWork.TasksOrganizer.Application.Features.CurrentDateTime.Interface;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Domain.Enums;
using GoodToWork.TasksOrganizer.Infrastructure.Features.Problem.Queries;
using MediatR;
using System.Net;

namespace GoodToWork.TasksOrganizer.Application.Features.Problem.Commands;

public sealed record CreateProblemCommand(string Title, string Description, Guid ProjectId, Guid PerformerId, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<Guid>;

public class CreateProblemHandler : IRequestHandler<CreateProblemCommand, Guid>
{
    private readonly ICurrentDateTime _currentDateTime;
    private readonly IMediator _mediator;
    private readonly IAppRepository _appRepository;
    private readonly IEventSenderWrapper _eventSender;

    public CreateProblemHandler(
        ICurrentDateTime currentDateTime,
        IMediator mediator,
        IAppRepository appRepository,
        IEventSenderWrapper eventSender)
    {
        _currentDateTime = currentDateTime;
        _mediator = mediator;
        _appRepository = appRepository;
        _eventSender = eventSender;
    }

    public async Task<Guid> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
    {
        var creator = await _appRepository.ProjectUsers
            .Find(e => e.ProjectId == request.ProjectId && e.UserId == request.SenderId &&
            (e.Role.HasFlag(UserProjectRoleEnum.Moderator) | e.Role.HasFlag(UserProjectRoleEnum.Creator)));

        if (creator is null)
        {
            throw new NoAccessException("You have no access to create new task.", HttpStatusCode.Forbidden);
        }

        var performer = await _appRepository.ProjectUsers
            .Find(e => e.ProjectId == request.ProjectId && e.UserId == request.PerformerId &&
            e.Role.HasFlag(UserProjectRoleEnum.Performer));

        if (performer is null)
        {
            throw new NoAccessException("Choosen user cannot be assigned as performer.", HttpStatusCode.Forbidden);
        }

        var validationResult = await _mediator.Send(new ValidateProblemQuery(request.Title, request.Description));

        if (!validationResult.IsValid)
        {
            throw new ValidationFailedException("Incorrect input.", HttpStatusCode.Forbidden, validationResult);
        }

        var newProblem = ProblemEntityBuilder.Create(_currentDateTime)
            .WithTitle(request.Title)
            .WithDescription(request.Description)
            .WithProjectId(request.ProjectId)
            .WithPerfomerId(request.PerformerId)
            .WithCreatorId(request.SenderId)
            .Build();

        var insertedProblem = await _appRepository.Problems.Add(newProblem);

        await _appRepository.SaveChangesAsync();

        if (newProblem.PerformerId != Guid.Empty)
            await _eventSender.Send(new EmailCreatedEvent()
            {
                RecipientId = newProblem.PerformerId,
                Title = $"You have new task.",
                Contents = $"New task of title {newProblem.Title} was assigned to you."
            });

        return insertedProblem.Id;
    }
}