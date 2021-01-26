using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public static class MappingHelper
    {
        public static void AddBaseEntityProperties<T>(EntityTypeBuilder<T> builder)
            where T : BaseEntity
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.DeletedAt)
                .HasColumnName("deleted_at");
            builder.HasQueryFilter(u => u.DeletedAt != null);

            builder.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at")
                .ValueGeneratedOnUpdate();
        }
    }
}