using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("message");

            MappingHelper.AddBaseEntityProperties<Message>(builder);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("content");

            builder.Property(m => m.UserId)
                .IsRequired()
                .HasColumnName("user_id");
            builder.Property(m => m.ChannelId)
                .IsRequired()
                .HasColumnName("channel_id");

            builder.HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            builder.HasOne(m => m.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChannelId);
        }
    }
}