using GoodToWork.Shared.Common.Domain.Exceptions.Shared;
using System.Net;

namespace GoodToWork.Shared.Common.Domain.Exceptions.Validation;

public class ValidationFailedException : DomainException
{
    public ValidationFailedException(string? message, HttpStatusCode httpStatusCode, object? responseObject) 
        : base(message, httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
        ResponseObject = responseObject;
    }

    public HttpStatusCode HttpStatusCode { get; }
    public object? ResponseObject { get; }
}