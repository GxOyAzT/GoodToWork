namespace GoodToWork.AuthService.Infrastructure.Exceptions;

internal class InvalidTokenException : Exception
{
    public InvalidTokenException(string? message) : base(message)
    {
    }
}
