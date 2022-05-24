using GoodToWork.AuthService.Domain.Entities;

namespace GoodToWork.AuthService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserModel> TryFindUserByPassword(string user, string password, CancellationToken ct);
    Task<UserModel> TryFindUserById(Guid id, CancellationToken ct);
}
