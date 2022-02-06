using GoodToWork.Shared.Common.Domain.Exceptions.Shared;
using System.Net;

namespace GoodToWork.Shared.Common.Domain.Exceptions.Entities;

public class CannnotFindException : DomainException
{
    public CannnotFindException(string? message, HttpStatusCode httpStatusCode)
        : base(message, httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }
}
