using GoodToWork.TasksOrganizer.Domain.Entities;

namespace GoodToWork.TasksOrganizer.Application.ApiModels.Status;

public class StatusBaseModel
{
    private readonly StatusEntity _statusEntity;

    public StatusBaseModel(StatusEntity statusEntity)
    {
        _statusEntity = statusEntity;
    }

    public Guid Id { get => _statusEntity.Id; }
    public string Status { get => _statusEntity.Status.ToString(); }
    public string Updated { get => _statusEntity.Updated.ToString("mm:hh dd-MM-yyyy"); }
    public string Updator { get => _statusEntity.Updator.Name; }
}
