using GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;
using System.Net;

namespace GoodToWork.TasksOrganizer.Domain.Exceptions.Validation;

public class ValidationFailedError : DomainException
{
    public ValidationFailedError(string? message, HttpStatusCode httpStatusCode, object? responseObject) 
        : base(message, httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
        ResponseObject = responseObject;
    }

    public HttpStatusCode HttpStatusCode { get; }
    public object? ResponseObject { get; }
}