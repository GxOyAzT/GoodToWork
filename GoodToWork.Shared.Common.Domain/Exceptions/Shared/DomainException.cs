using System.Net;

namespace GoodToWork.Shared.Common.Domain.Exceptions.Shared;

public class DomainException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public DomainException(string? message, HttpStatusCode httpStatusCode) 
        : base(message)
    {
        StatusCode = httpStatusCode;
    }
}
