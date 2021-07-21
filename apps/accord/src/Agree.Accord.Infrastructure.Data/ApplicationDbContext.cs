using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserAccount>(b =>
            {
                b.HasKey(u => u.Id);

                b.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(40);

                b.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                b.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                b.Property(u => u.Tag)
                    .HasConversion(
                        (tag) => tag.Value,
                        (value) => DiscriminatorTag.Parse(value)
                    )
                    .IsRequired()
                    .HasMaxLength(4);

                b.HasIndex(u => new { u.Tag, u.UserName }).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}