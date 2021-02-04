using System;
using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            MappingHelper.AddBaseEntityProperties<Role>(builder);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(r => r.ColorHex)
                .IsRequired()
                .HasMaxLength(6)
                .IsFixedLength();

            builder.Property(r => r.Order)
                .IsRequired();

            builder.Property(r => r.CanCreateNewRoles)
                .IsRequired();
            builder.Property(r => r.CanDeleteRoles)
                .IsRequired();
            builder.Property(r => r.CanDeleteServer)
                .IsRequired();
            builder.Property(r => r.CanRemoveUsers)
                .IsRequired();
            builder.Property(r => r.CanUpdateServerAvatar)
                .IsRequired();
            builder.Property(r => r.CanUpdateServerDescription)
                .IsRequired();
            builder.Property(r => r.CanUpdateServerName)
                .IsRequired();

            builder.Property(r => r.ServerId)
                .IsRequired();

            builder.HasOne(r => r.Server)
                .WithMany(s => s.Roles)
                .HasForeignKey(r => r.ServerId);

            builder.HasMany(r => r.ServerUsers)
                .WithMany(su => su.Roles)
                .UsingEntity<ServerUserRole>(
                    j => j
                        .HasOne(sur => sur.ServerUser)
                        .WithMany(su => su.ServerUserRoles)
                        .HasForeignKey(su => su.ServerUserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne(sur => sur.Role)
                        .WithMany(r => r.ServerUserRoles)
                        .HasForeignKey(r => r.ServerUserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(t => new { t.RoleId, t.ServerUserId, t.Id });
                    }
                );
        }
    }
}