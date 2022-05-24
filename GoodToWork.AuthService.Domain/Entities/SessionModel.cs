namespace GoodToWork.AuthService.Domain.Entities;

public class SessionModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
