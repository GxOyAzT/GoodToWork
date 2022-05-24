namespace GoodToWork.TasksOrganizer.Infrastructure.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException() 
        : base("Invalid auth token passed. Add AuthToken header with valid token to request.")
    {
    }
}
