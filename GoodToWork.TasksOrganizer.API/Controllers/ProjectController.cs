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

    /// <summary>
    /// Returns all projects where user is assigned
    /// </summary>
    [HttpGet]
    [Route("getalluser")]
    public async Task<List<ProjectBaseModel>> Get([FromHeader] Guid senderId) =>
        await _mediator.Send(new GetSenderProjectsQuery(senderId));

    /// <summary>
    /// Create new project
    /// </summary>
    [HttpPost]
    [Route("create")]
    public async Task<ProjectEntity> Create([FromBody] CreateProjectCommand createProjectCommand) =>
        await _mediator.Send(createProjectCommand);

    /// <summary>
    /// Get project model with dependencies required in editing view
    /// </summary>
    [HttpGet]
    [Route("getforedit/{projectId}")]
    public async Task<ProjectEditModel> GetForEdit([FromHeader] Guid senderId, [FromRoute] Guid projectId) =>
        await _mediator.Send(new GetProjectForEditQuery(projectId, senderId));

    /// <summary>
    /// Get details about project
    /// </summary>
    [HttpGet]
    [Route("detail/{projectId}")]
    public async Task<ProjectDetailModel> Detail([FromHeader] Guid senderId, [FromRoute] Guid projectId) =>
        await _mediator.Send(new GetProjectForDetailQuery(projectId, senderId));

    /// <summary>
    /// Adding user to project
    /// </summary>
    [HttpPatch]
    [Route("addcoworkertoproject")]
    public async Task<ProjectEditModel> AddCoworkerToProject([FromBody] AddPerformerToProjectCommand addPerformerToProjectCommand)
    {
        await _mediator.Send(addPerformerToProjectCommand);

        return await _mediator.Send(new GetProjectForEditQuery(addPerformerToProjectCommand.ProjectId, addPerformerToProjectCommand.SenderId));
    }

    /// <summary>
    /// Change user role in project
    /// </summary>
    [HttpPatch]
    [Route("updatecoworkerrole")]
    public async Task<Unit> UpdateCorowkerRole([FromBody] UpdatePerformerRoleCommand updatePerformerRoleCommand) =>
        await _mediator.Send(updatePerformerRoleCommand);
}
