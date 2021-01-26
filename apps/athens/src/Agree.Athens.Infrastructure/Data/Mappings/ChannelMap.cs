using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class ChannelMap : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("channel");

            MappingHelper.AddBaseEntityProperties<Channel>(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("name");

            builder.Property(c => c.Order)
                .IsRequired()
                .HasColumnName("order");

            builder.Property(c => c.Type)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Channel.ChannelType)Enum.Parse(typeof(Channel.ChannelType), v)
                )
                .HasColumnName("type");

            builder.Property(c => c.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Channels)
                .HasForeignKey(c => c.CategoryId);

            builder.HasMany(c => c.Messages)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId);
        }
    }
}