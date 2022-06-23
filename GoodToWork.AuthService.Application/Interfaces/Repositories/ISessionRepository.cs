using GoodToWork.AuthService.Domain.Entities;

namespace GoodToWork.AuthService.Application.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<SessionModel> AddSessionAsync(SessionModel sessionModel, CancellationToken ct);
    Task<UserModel> TryGetUserByIdAsync(Guid id, CancellationToken ct);
    Task DeactivateAllUserSessionsAsync(Guid userId, CancellationToken ct);
}
