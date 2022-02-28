using GoodToWork.TasksOrganizer.Application.ApiModels.Comment;
using GoodToWork.TasksOrganizer.Application.Features.Comment.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodToWork.TasksOrganizer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<CommentBaseModel> Create([FromBody] CreateCommentCommand createCommentCommand) =>
        await _mediator.Send(createCommentCommand);

}
