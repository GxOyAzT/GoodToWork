using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.User;

public class UserBaseModel
{
    private readonly UserEntity _userEntity;

    public UserBaseModel(UserEntity userEntity)
    {
        _userEntity = userEntity;
    }

    public Guid Id { get => _userEntity.Id; }
    public string Name { get => _userEntity.Name; }
}
