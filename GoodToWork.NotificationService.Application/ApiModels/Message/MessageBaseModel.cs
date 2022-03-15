using GoodToWork.NotificationService.Domain.Entities;

namespace GoodToWork.NotificationService.Application.ApiModels.Message;

public class MessageBaseModel
{
    private readonly MessageEntity _messageEntity;

    public MessageBaseModel(MessageEntity messageEntity)
    {
        _messageEntity = messageEntity;
    }

    public Guid Id { get => _messageEntity.Id; }
    public Guid SenderId { get => _messageEntity.SenderId; }
    public Guid ReceiverId { get => _messageEntity.ReceiverId; }
    public string Message { get => _messageEntity.Message; }
    public string SentTime { get => _messageEntity.SentTime.ToString("hh:mm dd-MM-yyyy"); }
}
