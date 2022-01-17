using GoodToWork.NotificationService.Application.Builders.Email;
using GoodToWork.NotificationService.Application.Features.CurrentDateTime;
using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Exceptions.Input;
using MediatR;
using System.Net;

namespace GoodToWork.NotificationService.Application.Features.Email.Commands;

public sealed record CreateEmailCommand(Guid RecipientId, string Title, string Contents) : IRequest<Unit>;

public sealed class CreateEmailHandler : IRequestHandler<CreateEmailCommand, Unit>
{
    private readonly IAppRepository _appRepository;
    private readonly ICurrentDateTime _currentDateTime;

    public CreateEmailHandler(
        IAppRepository appRepository, 
        ICurrentDateTime currentDateTime)
    {
        _appRepository = appRepository;
        _currentDateTime = currentDateTime;
    }

    public async Task<Unit> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
    {
        var recipient = await _appRepository.Users.FindById(request.RecipientId);

        if (recipient == null)
        {
            throw new IncorrectInputException($"Cannot find recipient of id {request.RecipientId}", HttpStatusCode.BadRequest);
        }

        var newEmail = EmailBuilder.Create(_currentDateTime)
            .AddTitle(request.Title)
            .AddContents(request.Contents)
            .AddRecipient(recipient)
            .Build();

        await _appRepository.Emails.Insert(newEmail);

        return Unit.Value;
    }
}
