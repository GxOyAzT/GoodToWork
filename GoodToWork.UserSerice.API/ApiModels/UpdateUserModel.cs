namespace GoodToWork.UserSerice.API.ApiModels;

public class UpdateUserModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string ImageSrc { get; set; }
}
