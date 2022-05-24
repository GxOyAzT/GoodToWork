using GoodToWork.AuthService.Application.Features.User.Queries;
using Grpc.Core;
using MediatR;

namespace GoodToWork.AuthService.API.Grpc.Listeners;

public class ValidateTokenListener : ValidateToken.ValidateTokenBase
{
    private readonly IMediator _mediator;

    public ValidateTokenListener(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<TokenResponse> ValidateToken(TokenRequest request, ServerCallContext context)
    {
        var userModel = await _mediator.Send(new GetUserFromToken.Query(request.Token), context.CancellationToken);

        return new TokenResponse()
        {
            UserId = userModel.Id.ToString()
        };
    }
}
