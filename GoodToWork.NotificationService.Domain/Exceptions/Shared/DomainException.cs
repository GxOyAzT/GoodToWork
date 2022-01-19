using System.Net;

namespace GoodToWork.NotificationService.Domain.Exceptions.Shared;

public class DomainException : Exception
{
    public DomainException(string? message, HttpStatusCode httpStatus) : base(message)
    {
        HttpStatus = httpStatus;
    }

    public HttpStatusCode HttpStatus { get; }
}
