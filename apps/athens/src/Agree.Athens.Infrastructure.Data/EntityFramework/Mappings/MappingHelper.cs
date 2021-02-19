using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public static class MappingHelper
    {
        public static void AddBaseEntityProperties<T>(EntityTypeBuilder<T> builder)
            where T : BaseEntity
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();
        }
    }
}