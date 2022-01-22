using GoodToWork.TasksOrganizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.EntityConfiguration;

internal class ProjectUserEntityTypeConfiguration : IEntityTypeConfiguration<ProjectUserEntity>
{
    public void Configure(EntityTypeBuilder<ProjectUserEntity> builder)
    {
        builder.HasKey(pu => new { pu.ProjectId, pu.UserId });

        builder.HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);

        builder.HasOne(pu => pu.Project)
            .WithMany(p => p.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);
    }
}
