using GoodToWork.TasksOrganizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.EntityConfiguration;

internal class StatusEntityTypeConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.HasOne(s => s.Problem)
            .WithMany(p => p.Statuses)
            .HasForeignKey(s => s.ProblemId);
    }
}
