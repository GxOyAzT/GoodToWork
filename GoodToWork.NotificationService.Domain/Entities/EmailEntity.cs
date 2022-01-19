using GoodToWork.NotificationService.Domain.Entities.Shared;

namespace GoodToWork.NotificationService.Domain.Entities;

public class EmailEntity : BaseEntity
{
    public string? Title { get; set; }
    public string? Contents { get; set; }
    public DateTime CreationDate { get; set; }
    public bool WasSent { get; set; }

    public UserEntity? Recipient { get; set; } = null;
}
