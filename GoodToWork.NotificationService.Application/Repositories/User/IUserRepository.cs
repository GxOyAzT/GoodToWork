using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Repositories.User;

public interface IUserRepository
{
    Task<UserEntity> Find(Func<UserEntity, bool> filter);
}
