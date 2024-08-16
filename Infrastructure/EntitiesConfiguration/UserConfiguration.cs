using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(h => h.UserId);
            builder.Property(h => h.UserId).ValueGeneratedOnAdd();

            builder.Property(t => t.UserName).HasMaxLength(500).IsRequired();
            builder.Property(t => t.NormalizedUserName).HasMaxLength(500).IsRequired();
            builder.Property(t => t.FirstName).HasMaxLength(500).IsRequired();
            builder.Property(t => t.LastName).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Description);
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.CreatedBy).HasMaxLength(450).IsRequired();
            builder.Property(t => t.ModifiedBy).HasMaxLength(450);
            builder.Property(t => t.DeletedBy).HasMaxLength(450);
        }
    }
}
