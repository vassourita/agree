using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class ServerUserMap : IEntityTypeConfiguration<ServerUser>
    {
        public void Configure(EntityTypeBuilder<ServerUser> builder)
        {
            MappingHelper.AddBaseEntityProperties<ServerUser>(builder);

            builder.Property(su => su.ServerId)
                .IsRequired();
            builder.Property(su => su.UserId)
                .IsRequired();

            builder.HasOne(su => su.Server)
                .WithMany(s => s.ServerUsers)
                .HasForeignKey(su => su.ServerId);

            builder.HasOne(su => su.User)
                .WithMany(u => u.UserServers)
                .HasForeignKey(su => su.UserId);

            builder.HasMany(su => su.Roles)
                .WithMany(r => r.ServerUsers)
                .UsingEntity<ServerUserRole>(
                    j => j
                        .HasOne(sur => sur.Role)
                        .WithMany(r => r.ServerUserRoles)
                        .HasForeignKey(r => r.ServerUserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne(sur => sur.ServerUser)
                        .WithMany(su => su.ServerUserRoles)
                        .HasForeignKey(su => su.ServerUserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(t => new { t.RoleId, t.ServerUserId, t.Id });
                    }
                );
        }
    }
}