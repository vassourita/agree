using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            MappingHelper.AddBaseEntityProperties<User>(builder);

            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Tag)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Domain.ValueObjects.UserTag(v)
                )
                .HasMaxLength(4)
                .IsFixedLength();

            builder.Property(u => u.Status)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(u => u.AvatarFileName)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(u => u.DeletedAt);
            builder.HasQueryFilter(u => u.DeletedAt != null);

            builder.HasMany(u => u.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId);

            builder.HasMany(u => u.Servers)
                .WithMany(server => server.Users)
                .UsingEntity<ServerUser>(
                    j => j
                        .HasOne(su => su.Server)
                        .WithMany(s => s.ServerUsers)
                        .HasForeignKey(su => su.ServerId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne(su => su.User)
                        .WithMany(u => u.UserServers)
                        .HasForeignKey(su => su.UserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(t => new { t.ServerId, t.UserId, t.Id });
                    }
                );
        }
    }
}