using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;

namespace GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;

public interface IAppRepository
{
    IUserRepository Users { get; }
    IProjectRepository Projects { get; }
}
