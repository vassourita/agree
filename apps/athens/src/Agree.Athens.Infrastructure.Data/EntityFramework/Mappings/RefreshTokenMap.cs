using Agree.Athens.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(rt => rt.ExpiryOn)
                .IsRequired();

            builder.Property(rt => rt.CreatedOn)
                .IsRequired();

            builder.Property(rt => rt.CreatedByIp)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(rt => rt.RevokedOn)
                .IsRequired();

            builder.Property(rt => rt.RevokedByIp)
                .HasMaxLength(80)
                .IsRequired();

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}