using GoodToWork.TasksOrganizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;

public class AppDbContext : DbContext
{
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProjectUserEntity> ProjectUsers { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    public virtual DbSet<ProblemEntity> Problems { get; set; }
    public virtual DbSet<CommentEntity> Comments { get; set; }
    public virtual DbSet<StatusEntity> Status { get; set; }
}
