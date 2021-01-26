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
            builder.ToTable("category");

            MappingHelper.AddBaseEntityProperties<Category>(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("name");

            builder.Property(c => c.Order)
                .IsRequired()
                .HasColumnName("order");

            builder.Property(c => c.ServerId)
                .IsRequired()
                .HasColumnName("server_id");

            builder.HasOne(c => c.Server)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.ServerId);

            builder.HasMany(c => c.Channels)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}