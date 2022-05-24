using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.TasksOrganizer.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;
using Microsoft.AspNetCore.Builder;

namespace GoodToWork.TasksOrganizer.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (InvalidTokenException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        catch (ValidationFailedException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}