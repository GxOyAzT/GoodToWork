using GoodToWork.NotificationService.Application.Features.CurrentDateTime;
using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities;
using GoodToWork.Shared.Common.Domain.Exceptions.Entities;
using MediatR;

namespace GoodToWork.NotificationService.Application.Features.Message.Commands;

public sealed record CreateMessageCommand(Guid SenderId, Guid ReceiverId, string Message) : IRequest<MessageEntity>;

public sealed class CreateMessageHandler : IRequestHandler<CreateMessageCommand, MessageEntity>
{
    private readonly IAppRepository _appRepository;
    private readonly ICurrentDateTime _currentDateTime;

    public CreateMessageHandler(
        IAppRepository appRepository,
        ICurrentDateTime currentDateTime)
    {
        _appRepository = appRepository;
        _currentDateTime = currentDateTime;
    }

    public async Task<MessageEntity> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var x = await _appRepository.Users.Get();

        var sender = await _appRepository.Users.Find(request.SenderId);

        if (sender == null)
        {
            throw new CannnotFindException($"Cannot find sender of ID: {request.SenderId}", System.Net.HttpStatusCode.NotFound);
        }

        var receiver = await _appRepository.Users.Find(request.ReceiverId);

        if (receiver == null)
        {
            throw new CannnotFindException($"Cannot find receiver of ID: {request.ReceiverId}", System.Net.HttpStatusCode.NotFound);
        }

        var newMessage = new MessageEntity()
        {
            ReceiverId = receiver.Id,
            SenderId = sender.Id,
            SentTime = _currentDateTime.CurrentDateTime,
            WasSeen = false,
            Message = request.Message
        };

        await _appRepository.Messages.Insert(newMessage);

        return newMessage;
    }
}
