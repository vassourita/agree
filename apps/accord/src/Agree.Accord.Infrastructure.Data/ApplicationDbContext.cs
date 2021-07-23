using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ServerRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Server> Servers { get; set; }

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
            });
        }
    }
}