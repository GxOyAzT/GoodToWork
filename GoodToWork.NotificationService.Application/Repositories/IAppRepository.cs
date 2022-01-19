using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Repositories;

public interface IAppRepository
{
    ISharedRepository<EmailEntity> Emails { get; }
    ISharedRepository<UserEntity> Users { get; }
}
