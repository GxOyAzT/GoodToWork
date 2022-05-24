using GoodToWork.AuthService.Domain.Entities;

namespace GoodToWork.AuthService.Application.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<SessionModel> AddSession(SessionModel sessionModel, CancellationToken ct);
    Task<UserModel> TryGetUserById(Guid id, CancellationToken ct);
}
