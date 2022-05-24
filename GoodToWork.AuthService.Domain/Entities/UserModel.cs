namespace GoodToWork.AuthService.Domain.Entities;

public class UserModel
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public DateTime Created { get; set; }
}
