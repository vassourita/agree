using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class ServerUserRoleMap : IEntityTypeConfiguration<ServerUserRole>
    {
        public void Configure(EntityTypeBuilder<ServerUserRole> builder)
        {
            builder.ToTable("server_user_role");

            MappingHelper.AddBaseEntityProperties<ServerUserRole>(builder);

            builder.Property(sur => sur.RoleId)
                .IsRequired()
                .HasColumnName("role_id");
            builder.Property(sur => sur.ServerUserId)
                .IsRequired()
                .HasColumnName("server_user_id");

            builder.HasOne(sur => sur.Role)
                .WithMany(r => r.ServerUserRoles)
                .HasForeignKey(sur => sur.RoleId);

            builder.HasOne(sur => sur.ServerUser)
                .WithMany(su => su.ServerUserRoles)
                .HasForeignKey(sur => sur.ServerUserId);
        }
    }
}