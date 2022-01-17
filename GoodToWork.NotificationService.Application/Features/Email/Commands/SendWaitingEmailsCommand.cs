using GoodToWork.NotificationService.Application.Email;
using GoodToWork.NotificationService.Application.Repositories;
using MediatR;

namespace GoodToWork.NotificationService.Application.Features.Email.Commands;

public sealed record SendWaitingEmailsCommand : IRequest<Unit>;

public sealed class SendWaitingEmailsHandler : IRequestHandler<SendWaitingEmailsCommand, Unit>
{
    private readonly IAppRepository _appRepository;
    private readonly IEmailSender _emailSender;

    public SendWaitingEmailsHandler(
        IAppRepository appRepository,
        IEmailSender emailSender)
    {
        _appRepository = appRepository;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(SendWaitingEmailsCommand request, CancellationToken cancellationToken)
    {
        var emails = (await _appRepository.Emails.Get()).Where(e => !e.WasSent);

        foreach(var email in emails)
        {
            await _emailSender.SendEmail(email);

            email.WasSent = true;
            await _appRepository.Emails.Update(email);
        }

        return Unit.Value;
    }
}

