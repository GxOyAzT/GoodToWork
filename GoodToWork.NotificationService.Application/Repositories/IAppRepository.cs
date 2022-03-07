using GoodToWork.NotificationService.Application.Repositories.Message;
using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Repositories;

public interface IAppRepository
{
    ISharedRepository<EmailEntity> Emails { get; }
    IMessageRepository Messages { get; }
    ISharedRepository<UserEntity> Users { get; }
}
