using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class ServerMap : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            MappingHelper.AddBaseEntityProperties<Server>(builder);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(s => s.Description)
                .IsRequired(false);

            builder.Property(s => s.AvatarFileName)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.HasMany(s => s.Users)
                .WithMany(server => server.Servers)
                .UsingEntity<ServerUser>(
                    j => j
                        .HasOne(su => su.User)
                        .WithMany(s => s.UserServers)
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