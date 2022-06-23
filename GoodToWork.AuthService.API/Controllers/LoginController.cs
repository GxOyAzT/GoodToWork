using GoodToWork.AuthService.Application.ApiModels.User;
using GoodToWork.AuthService.Application.Features.User.Commands;
using GoodToWork.AuthService.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodToWork.AuthService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("Login")]
    public async Task<TokenModel> Login(string user, string password, CancellationToken ct)
    {
        return await _mediator.Send(new LoginQuery.Query(user, password), ct);
    }

    [HttpDelete]
    [Route("logoutallsessions")]
    public async Task Login([FromHeader] string senderId, CancellationToken ct)
    {
        await _mediator.Send(new LogoutAllSessions.Command(Guid.Parse(senderId)), ct);
    }
}
