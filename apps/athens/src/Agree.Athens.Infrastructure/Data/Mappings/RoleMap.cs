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
            builder.ToTable("role");

            MappingHelper.AddBaseEntityProperties<Role, int>(builder);
            MappingHelper.AddDeletableBaseEntityProperties<Role, int>(builder);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(r => r.ColorHex)
                .IsRequired()
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("color_hex");

            builder.Property(r => r.Order)
                .IsRequired()
                .HasColumnName("order");

            builder.Property(r => r.Permissions.CanCreateNewRoles)
                .IsRequired()
                .HasColumnName("permissions_can_create_new_roles");
            builder.Property(r => r.Permissions.CanDeleteRoles)
                .IsRequired()
                .HasColumnName("permissions_can_delete_roles");
            builder.Property(r => r.Permissions.CanDeleteServer)
                .IsRequired()
                .HasColumnName("permissions_can_delete_server");
            builder.Property(r => r.Permissions.CanRemoveUsers)
                .IsRequired()
                .HasColumnName("permissions_can_remove_users");
            builder.Property(r => r.Permissions.CanUpdateServerAvatar)
                .IsRequired()
                .HasColumnName("permissions_can_update_server_avatar");
            builder.Property(r => r.Permissions.CanUpdateServerDescription)
                .IsRequired()
                .HasColumnName("permissions_can_update_server_description");
            builder.Property(r => r.Permissions.CanUpdateServerName)
                .IsRequired()
                .HasColumnName("permissions_can_update_server_name");

            builder.Property(r => r.ServerId)
                .IsRequired()
                .HasColumnName("server_id");

            builder.HasOne(r => r.Server)
                .WithMany(s => s.Roles)
                .HasForeignKey(r => r.ServerId);
        }
    }
}