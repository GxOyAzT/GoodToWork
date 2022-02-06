using GoodToWork.Shared.Common.Domain.Exceptions.Shared;
using System.Net;

namespace GoodToWork.Shared.Common.Domain.Exceptions.Access;

public class NoAccessException : DomainException
{
    public NoAccessException(string? message, HttpStatusCode httpStatusCode)
        : base(message, httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }
}
