namespace GoodToWork.AuthService.Infrastructure.Exceptions;

internal class CannotFindException : Exception
{
    public CannotFindException(string? message) : base(message)
    {
    }
}
