using System.Net;

namespace GoodToWork.TasksOrganizer.Domain.Exceptions.Shared;

public class DomainException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public DomainException(string? message, HttpStatusCode httpStatusCode) 
        : base(message)
    {
        StatusCode = httpStatusCode;
    }
}
