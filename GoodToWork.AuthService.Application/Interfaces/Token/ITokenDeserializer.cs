namespace GoodToWork.AuthService.Application.Interfaces.Token;

public interface ITokenDeserializer
{
    string GetUserIdFromToken(string token);
    string GetSessionIdFromToken(string token);
}
