using GoodToWork.TasksOrganizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.EntityConfiguration;

internal class ProblemEntityTypeConfiguration : IEntityTypeConfiguration<ProblemEntity>
{
    public void Configure(EntityTypeBuilder<ProblemEntity> builder)
    {
        builder.HasOne(p => p.Creator)
            .WithMany(u => u.ProblemsCreator)
            .HasForeignKey(p => p.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Performer)
            .WithMany(u => u.ProblemsPerformer)
            .HasForeignKey(p => p.PerformerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Project)
            .WithMany(p => p.Problems)
            .HasForeignKey(p => p.ProjectId);
    }
}
