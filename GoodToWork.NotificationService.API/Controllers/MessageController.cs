using GoodToWork.NotificationService.Application.ApiModels.Message;
using GoodToWork.NotificationService.Application.Features.Message.Commands;
using GoodToWork.NotificationService.Application.Features.Message.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodToWork.NotificationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<MessageBaseModel> Create([FromBody] CreateMessageCommand createMessageCommand) =>
        await _mediator.Send(createMessageCommand);

    [HttpGet]
    [Route("get/{senderId}/{receiverId}/{interval}")]
    public async Task<List<MessageBaseModel>> Get([FromRoute] Guid senderId, [FromRoute] Guid receiverId, [FromRoute] int interval) =>
        await _mediator.Send(new GetMessagesQuery(senderId, receiverId, interval));
}
