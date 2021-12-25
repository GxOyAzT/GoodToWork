using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Commands;

public record CreateProjectCommand(string Name, string Description, Guid SenderId) : BaseSenderIdRequest(SenderId), IRequest<ProjectEntity>;