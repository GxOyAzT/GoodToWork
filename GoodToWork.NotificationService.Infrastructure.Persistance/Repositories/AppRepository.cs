using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Application.Repositories.Message;
using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories;

internal class AppRepository : IAppRepository
{
    public AppRepository(
        ISharedRepository<UserEntity> users,
        ISharedRepository<EmailEntity> emails,
        IMessageRepository messages)
    {
        Users = users;
        Emails = emails;
        Messages = messages;
    }

    public ISharedRepository<EmailEntity> Emails { get; }
    public ISharedRepository<UserEntity> Users { get; }
    public IMessageRepository Messages { get; }
}
