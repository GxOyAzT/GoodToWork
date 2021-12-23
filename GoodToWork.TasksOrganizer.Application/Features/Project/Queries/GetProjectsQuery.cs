using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;

namespace GoodToWork.TasksOrganizer.Application.Features.Project.Queries;

public sealed record GetProjectsQuery(Guid SenderId) : IRequest<List<ProjectEntity>>;