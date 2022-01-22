using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Application.Repositories.Comment;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.AppRepo;

internal class AppRepository : IAppRepository
{
    private readonly AppDbContext _appDbContext;

    public AppRepository(
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        IProjectUserRepository projectUserRepository,
        IProblemRepository problemRepository,
        ICommentRepository commentRepository,
        AppDbContext appDbContext)
    {
        Users = userRepository;
        Projects = projectRepository;
        ProjectUsers = projectUserRepository;
        Problems = problemRepository;
        Comments = commentRepository;
        _appDbContext = appDbContext;
    }

    public IUserRepository Users { get; }

    public IProjectRepository Projects { get; }

    public IProjectUserRepository ProjectUsers { get; }

    public IProblemRepository Problems { get; }

    public ICommentRepository Comments { get; }

    public async Task SaveChangesAsync() =>
        await _appDbContext.SaveChangesAsync();
}
