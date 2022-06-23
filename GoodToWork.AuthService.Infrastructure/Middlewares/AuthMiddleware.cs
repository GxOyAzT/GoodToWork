using GoodToWork.AuthService.Application.Interfaces.Repositories;
using GoodToWork.AuthService.Application.Interfaces.Token;
using GoodToWork.AuthService.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GoodToWork.AuthService.Infrastructure.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenDeserializer _tokenDeserializer;
    private readonly ISessionRepository _sessionRepository;

    public AuthMiddleware(
        RequestDelegate next,
        ITokenDeserializer tokenDeserializer,
        ISessionRepository sessionRepository)
    {
        _next = next;
        _tokenDeserializer = tokenDeserializer;
        _sessionRepository = sessionRepository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value.Contains("login/login"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("AuthToken", out var token))
        {
            throw new InvalidTokenException("Passed authentication token is incorrect.");
        }

        var sessionId = _tokenDeserializer.GetSessionIdFromToken(token);

        var userModel = await _sessionRepository.TryGetUserByIdAsync(Guid.Parse(sessionId), new CancellationToken());

        if (context.Request.Headers.TryGetValue("SenderId", out var senderIdFromHeader))
        {
            if (senderIdFromHeader != userModel.Id)
            {
                throw new InvalidTokenException("User id from token do not match user id from header.");
            }
        }

        await _next(context);
        return;
    }
}

public static class AuthMiddlewareFactory
{
    public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}