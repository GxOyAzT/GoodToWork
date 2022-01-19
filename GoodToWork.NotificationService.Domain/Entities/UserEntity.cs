using GoodToWork.NotificationService.Domain.Entities.Shared;

namespace GoodToWork.NotificationService.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
}
