using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Application.Repositories.Comment;

namespace GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;

public interface IAppRepository
{
    IUserRepository Users { get; }
    IProjectRepository Projects { get; }
    IProjectUserRepository ProjectUsers { get; }
    IProblemRepository Problems { get; }
    ICommentRepository Comments { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}
