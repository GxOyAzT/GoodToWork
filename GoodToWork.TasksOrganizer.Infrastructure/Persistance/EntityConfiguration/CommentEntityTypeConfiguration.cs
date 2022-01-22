using GoodToWork.TasksOrganizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodToWork.TasksOrganizer.Infrastructure.Persistance.EntityConfiguration;

internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasOne(c => c.Creator)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Problem)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.ProblemId);
    }
}
