using GoodToWork.NotificationService.Application.Repositories;
using GoodToWork.NotificationService.Domain.Entities;
using MediatR;

namespace GoodToWork.NotificationService.Application.Features.Message.Queries;

public sealed record GetMessagesQuery(Guid SenderId, Guid ReceiverId, int Interval) : IRequest<List<MessageEntity>>;

public sealed class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageEntity>>
{
    private readonly IAppRepository _appRepository;

    public GetMessagesHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<List<MessageEntity>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        return await _appRepository.Messages.GetChat(request.SenderId, request.ReceiverId, request.Interval);
    }
}
