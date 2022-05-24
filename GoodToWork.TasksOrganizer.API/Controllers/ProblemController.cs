using GoodToWork.TasksOrganizer.Application.ApiModels.Problem;
using GoodToWork.TasksOrganizer.Application.Features.Problem.Commands;
using GoodToWork.TasksOrganizer.Application.Features.Problem.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodToWork.TasksOrganizer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProblemController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get details about problem, with comments and statuses
    /// </summary>
    [HttpGet]
    [Route("detail/{problemId}/{senderId}")]
    public async Task<ProblemDetailModel> Create([FromRoute] Guid problemId, [FromHeader] Guid senderId) =>
        await _mediator.Send(new GetProblemDetailQuery(problemId, senderId));

    /// <summary>
    /// Change user role in project
    /// </summary>
    [HttpPost]
    [Route("create")]
    public async Task<Guid> Create([FromBody] CreateProblemCommand createProblemCommand) =>
        await _mediator.Send(createProblemCommand);

    /// <summary>
    /// Update problem's status
    /// </summary>
    [HttpPost]
    [Route("updatestatus")]
    public async Task UpdateStatus([FromBody] UpdateProblemStatusCommand updateProblemStatusCommand) =>
        await _mediator.Send(updateProblemStatusCommand);
}
