using GoodToWork.NotificationService.Application.Email;
using GoodToWork.NotificationService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GoodToWork.NotificationService.Infrastructure.Emailer.Features.Email;

internal class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmail(EmailEntity email)
    {
        _logger.LogInformation($"Sending Email... To: {email.Recipient.Email} Title: {email.Title}");

        return Task.CompletedTask;
    }
}
