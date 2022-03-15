using GoodToWork.NotificationService.Application.ApiModels.Message;
using GoodToWork.NotificationService.Application.Repositories;
using MediatR;

namespace GoodToWork.NotificationService.Application.Features.Message.Queries;

public sealed record GetMessagesQuery(Guid SenderId, Guid ReceiverId, int Interval) : IRequest<List<MessageBaseModel>>;

public sealed class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageBaseModel>>
{
    private readonly IAppRepository _appRepository;

    public GetMessagesHandler(
        IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public async Task<List<MessageBaseModel>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        return (await _appRepository.Messages.GetChat(request.SenderId, request.ReceiverId, request.Interval))
            .OrderByDescending(m => m.SentTime)
            .Take(10 * request.Interval)
            .Reverse()
            .Select(m => new MessageBaseModel(m))
            .ToList();
    }
}
