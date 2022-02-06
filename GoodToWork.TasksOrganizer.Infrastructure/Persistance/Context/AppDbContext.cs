using GoodToWork.TasksOrganizer.Domain.Entities;
using GoodToWork.TasksOrganizer.Infrastructure.Persistance.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProjectUserEntity> ProjectUsers { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    public virtual DbSet<ProblemEntity> Problems { get; set; }
    public virtual DbSet<CommentEntity> Comments { get; set; }
    public virtual DbSet<StatusEntity> Status { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProblemEntityTypeConfiguration().Configure(modelBuilder.Entity<ProblemEntity>());
        new CommentEntityTypeConfiguration().Configure(modelBuilder.Entity<CommentEntity>());
        new ProjectUserEntityTypeConfiguration().Configure(modelBuilder.Entity<ProjectUserEntity>());
        new StatusEntityTypeConfiguration().Configure(modelBuilder.Entity<StatusEntity>());
    }
}
