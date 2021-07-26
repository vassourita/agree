using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Domain.Social;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    /// <summary>
    /// The <see cref="ApplicationDbContext"/> class implements the application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ServerRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Server> Servers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.DisplayName)
                    .IsRequired()
                    .HasMaxLength(40);

                b.Property(u => u.Tag)
                    .HasConversion(
                        (tag) => tag.Value,
                        (value) => DiscriminatorTag.Parse(value)
                    )
                    .IsRequired()
                    .HasMaxLength(4);

                b.HasIndex(u => new { u.Tag, u.DisplayName }).IsUnique();

                b.HasMany(u => u.Servers)
                    .WithMany(s => s.Members);
            });

            builder.Entity<Server>(b =>
            {
                b.HasKey(s => s.Id);

                b.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                b.Property(s => s.Description)
                    .HasMaxLength(300);

                b.Property(s => s.PrivacyLevel)
                    .HasColumnType("varchar(10)")
                    .IsRequired()
                    .HasConversion(
                        (privacyLevel) => privacyLevel.ToString(),
                        (value) => Enum.Parse<ServerPrivacy>(value)
                    );

                b.HasMany(s => s.Members)
                    .WithMany(m => m.Servers);

                b.HasMany(s => s.Roles)
                    .WithOne(r => r.Server);

                b.HasMany(s => s.Categories)
                    .WithOne(c => c.Server);
            });

            builder.Entity<ServerRole>(b =>
            {
                b.HasOne(r => r.Server)
                    .WithMany(s => s.Roles);
            });

            builder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);

                b.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                b.HasOne(c => c.Server)
                    .WithMany(s => s.Categories);
            });

            builder.Entity<Friendship>(b =>
            {
                b.HasKey(f => new { f.FromId, f.ToId });

                b.HasOne(c => c.To);
                b.HasOne(c => c.From);
            });
        }
    }
}