using GoodToWork.TasksOrganizer.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Persistance.Repositories.User;

namespace GoodToWork.TasksOrganizer.Persistance.Repositories.AppRepo;

internal interface IAppRepository
{
    IUserRepository Users { get; }
    IProjectRepository Projects { get; }
}
