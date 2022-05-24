using GoodToWork.AuthService.Domain.Entities;

namespace GoodToWork.AuthService.Application.Interfaces.Token;

public interface ITokenGenerator
{
    string GenerateToken(UserModel userModel, SessionModel sessionModel);
}
