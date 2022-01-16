using GoodToWork.NotificationService.Application.Repositories.Email;
using GoodToWork.NotificationService.Application.Repositories.User;

namespace GoodToWork.NotificationService.Application.Repositories;

public interface IAppRepository
{
    IEmailRepository Emails { get; }
    IUserRepository Users { get; }
}
