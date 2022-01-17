using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Infrastructure.Persistance.Repositories;

internal class AppRepository : IAppRepository
{
    public AppRepository(
        ISharedRepository<UserEntity> users,
        ISharedRepository<EmailEntity> emails)
    {
        Users = users;
        Emails = emails;
    }

    public ISharedRepository<EmailEntity> Emails { get; }
    public ISharedRepository<UserEntity> Users { get; }
}
