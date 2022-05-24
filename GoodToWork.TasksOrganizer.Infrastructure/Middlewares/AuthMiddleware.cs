using GoodToWork.Shared.AuthClient.Interfaces;
using GoodToWork.TasksOrganizer.Application.Features.Shared;
using GoodToWork.TasksOrganizer.Infrastructure.Exceptions;
using GoodToWork.TasksOrganizer.Infrastructure.Utilities.UrlDeserialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace GoodToWork.TasksOrganizer.Infrastructure.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthClient _authClient;
    private readonly IUrlDeserializer _urlDeserializer;

    public AuthMiddleware(
        RequestDelegate next, 
        IAuthClient authClient,
        IUrlDeserializer urlDeserializer)
    {
        _next = next;
        _authClient = authClient;
        _urlDeserializer = urlDeserializer;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.TryGetValue("AuthToken", out var token))
        {
            throw new InvalidTokenException();
        }

        var authResponse = await _authClient.ValidateToken(token);

        if (!authResponse.IsValid)
        {
            throw new InvalidTokenException();
        }

        await ValidateSenderId(httpContext, authResponse.UserId!);

        await _next(httpContext);
    }

    private async Task ValidateSenderId(HttpContext httpContext, string userIdFromToken)
    {
        if (httpContext.Request.Method == HttpMethod.Get.ToString() || httpContext.Request.Method == HttpMethod.Delete.ToString())
        {
            if (!httpContext.Request.Headers.TryGetValue("SenderId", out var senderIdFromHeader))
            {
                throw new InvalidTokenException();
            }

            if (userIdFromToken != senderIdFromHeader)
            {
                throw new InvalidTokenException();
            }

            return;
        }

        if (httpContext.Request.Method == HttpMethod.Post.ToString() || httpContext.Request.Method == HttpMethod.Patch.ToString() || httpContext.Request.Method == HttpMethod.Put.ToString())
        {
            try
            {
                //httpContext.Request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
                await httpContext.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var requestBodyString = Encoding.UTF8.GetString(buffer);

                var body = JsonConvert.DeserializeObject<BaseSenderIdRequest>(requestBodyString);
                if (body!.SenderId.ToString() != userIdFromToken)
                {
                    throw new InvalidTokenException();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidTokenException();
            }
            finally
            {
                httpContext.Request.Body.Position = 0;
            }

            return;
        }

        throw new InvalidTokenException();
    }
}

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}