using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Repositories.Email;

public interface IEmailRepository
{
    Task<List<EmailEntity>> Get(Func<EmailEntity, bool> filter);
    Task Insert (EmailEntity email);
    Task Update (EmailEntity email);
}
