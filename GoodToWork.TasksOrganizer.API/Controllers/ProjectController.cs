using GoodToWork.TasksOrganizer.Application.ApiModels.Project;
using GoodToWork.TasksOrganizer.Application.Features.Project.Commands;
using GoodToWork.TasksOrganizer.Application.Features.Project.Queries;
using GoodToWork.TasksOrganizer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodToWork.TasksOrganizer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getalluser/{senderId}")]
    public async Task<List<ProjectBaseModel>> Get([FromRoute] Guid senderId) =>
        await _mediator.Send(new GetSenderProjectsQuery(senderId));

    [HttpPost]
    [Route("create")]
    public async Task<ProjectEntity> Create([FromBody] CreateProjectCommand createProjectCommand) =>
        await _mediator.Send(createProjectCommand);

    [HttpGet]
    [Route("getforedit/{senderId}/{projectId}")]
    public async Task<ProjectEditModel> GetForEdit([FromRoute] Guid senderId, [FromRoute] Guid projectId) =>
        await _mediator.Send(new GetProjectForEditQuery(projectId, senderId));

    [HttpPatch]
    [Route("addcoworkertoproject")]
    public async Task<ProjectEditModel> AddCoworkerToProject([FromBody] AddPerformerToProjectCommand addPerformerToProjectCommand)
    {
        await _mediator.Send(addPerformerToProjectCommand);

        return await _mediator.Send(new GetProjectForEditQuery(addPerformerToProjectCommand.ProjectId, addPerformerToProjectCommand.SenderId));
    }

    [HttpGet]
    [Route("detail/{senderId}/{projectId}")]
    public async Task<ProjectDetailModel> Detail([FromRoute] Guid senderId, [FromRoute] Guid projectId) =>
        await _mediator.Send(new GetProjectForDetailQuery(projectId, senderId));
}
