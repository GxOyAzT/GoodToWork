using GoodToWork.NotificationService.Domain.Exceptions.Shared;
using System.Net;

namespace GoodToWork.NotificationService.Domain.Exceptions.Input;

public class IncorrectInputException : DomainException
{
    public IncorrectInputException(string? message, HttpStatusCode httpStatus) 
        : base(message, httpStatus)
    {
    }
}
