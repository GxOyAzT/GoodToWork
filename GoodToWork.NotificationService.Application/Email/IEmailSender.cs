using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.Email;

public interface IEmailSender
{
    Task SendEmail(EmailEntity email);
}
