using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class ServerMap : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            MappingHelper.AddBaseEntityProperties<Server>(builder);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(u => u.Description)
                .IsRequired();

            builder.Property(u => u.AvatarFileName)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.HasMany(u => u.Users)
                .WithMany(server => server.Servers)
                .UsingEntity<ServerUser>(
                    j => j
                        .HasOne(su => su.User)
                        .WithMany(u => u.UserServers)
                        .HasForeignKey(su => su.UserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne(su => su.Server)
                        .WithMany(s => s.ServerUsers)
                        .HasForeignKey(su => su.ServerId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(t => new { t.ServerId, t.UserId, t.Id });
                    });

            builder.HasMany(s => s.Roles)
                .WithOne(r => r.Server)
                .HasForeignKey(r => r.ServerId);

            builder.HasMany(s => s.Categories)
                .WithOne(c => c.Server)
                .HasForeignKey(c => c.ServerId);
        }
    }
}