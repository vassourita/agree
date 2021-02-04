using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            MappingHelper.AddBaseEntityProperties<Category>(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.Order)
                .IsRequired();

            builder.Property(c => c.ServerId)
                .IsRequired();

            builder.HasOne(c => c.Server)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.ServerId);

            builder.HasMany(c => c.Channels)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.Property(u => u.DeletedAt);
            builder.HasQueryFilter(u => u.DeletedAt != null);
        }
    }
}