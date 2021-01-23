using Agree.Athens.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.Mappings
{
    public class ServerUserMap : IEntityTypeConfiguration<ServerUser>
    {
        public void Configure(EntityTypeBuilder<ServerUser> builder)
        {
            builder.ToTable("server_user");

            MappingHelper.AddBaseEntityProperties<ServerUser, int>(builder);
            MappingHelper.AddDeletableBaseEntityProperties<ServerUser, int>(builder);

            builder.Property(su => su.ServerId)
                .IsRequired()
                .HasColumnName("server_id");
            builder.Property(su => su.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder.HasOne(su => su.Server)
                .WithMany(s => s.ServerUsers)
                .HasForeignKey(su => su.ServerId);

            builder.HasOne(su => su.User)
                .WithMany(u => u.UserServers)
                .HasForeignKey(su => su.UserId);
        }
    }
}