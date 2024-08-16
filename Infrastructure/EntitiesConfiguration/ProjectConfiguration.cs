using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(t => t.ModifiedBy).HasMaxLength(450);
            builder.Property(t => t.DeletedBy).HasMaxLength(450);
        }
    }
}
