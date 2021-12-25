using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Commands;

public sealed record AddPerformerToProjectCommand(Guid ProjectId, Guid NewPerformer, Guid SenderId) : IRequest<Unit>;
