using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Application.Persistance.Repositories.User;
using GoodToWork.TasksOrganizer.Application.Repositories.Comment;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.AppRepo;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Comment;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Problem;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.Project;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.ProjectUser;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodToWork.TasksOrganizer.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<AppDbContext>(config => config.UseSqlServer(dbConnectionString));

        services.AddScoped<IAppRepository, AppRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProblemRepository, ProblemRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }
}
