using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            MappingHelper.AddBaseEntityProperties<Message>(builder);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(m => m.UserId)
                .IsRequired();

            builder.Property(m => m.ChannelId)
                .IsRequired();

            builder.HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            builder.HasOne(m => m.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChannelId);
        }
    }
}