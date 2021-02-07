using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class ServerUserRoleMap : IEntityTypeConfiguration<ServerUserRole>
    {
        public void Configure(EntityTypeBuilder<ServerUserRole> builder)
        {
            MappingHelper.AddBaseEntityProperties<ServerUserRole>(builder);

            builder.Property(sur => sur.RoleId)
                .IsRequired();
            builder.Property(sur => sur.ServerUserId)
                .IsRequired();

            builder.HasOne(sur => sur.Role)
                .WithMany(r => r.ServerUserRoles)
                .HasForeignKey(sur => sur.RoleId);

            builder.HasOne(sur => sur.ServerUser)
                .WithMany(su => su.ServerUserRoles)
                .HasForeignKey(sur => sur.ServerUserId);
        }
    }
}