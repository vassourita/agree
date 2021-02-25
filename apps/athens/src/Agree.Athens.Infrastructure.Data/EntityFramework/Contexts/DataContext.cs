using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Aggregates.Servers.Factories;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using Agree.Athens.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Contexts
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DataContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<RoleDbModel> Roles { get; set; }
        public DbSet<ServerDbModel> Servers { get; set; }
        public DbSet<MessageDbModel> Messages { get; set; }
        public DbSet<TextChannelDbModel> TextChannels { get; set; }

        public async Task<bool> Commit()
        {
            var entriesWritten = await SaveChangesAsync();
            return entriesWritten > 0;
        }

        public Task<bool> Rollback()
        {
            return Task.FromResult(true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User mapping
            modelBuilder.Entity<UserDbModel>(builder =>
            {
                builder.Property(u => u.Tag)
                    .IsFixedLength()
                    .HasConversion(
                        v => v.Value,
                        v => UserTagFactory.FromString(v)
                    );

                builder.HasMany(u => u.Messages)
                    .WithOne(m => m.User)
                    .HasForeignKey(m => m.UserId);

                builder.HasMany(u => u.Servers)
                    .WithMany(s => s.Users)
                    .UsingEntity(
                        b => b.ToTable("UserServers")
                    );

                builder.HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .UsingEntity(
                        b => b.ToTable("UserRoles")
                    );
            });

            // Role mapping
            modelBuilder.Entity<RoleDbModel>(builder =>
            {
                builder.Property(u => u.ColorHex)
                    .IsFixedLength()
                    .HasConversion(
                        v => v.Value,
                        v => ColorHexFactory.FromString(v)
                    );

                builder.HasOne(r => r.Server)
                    .WithMany(s => s.Roles)
                    .HasForeignKey(r => r.ServerId);

                builder.HasMany(r => r.Users)
                    .WithMany(u => u.Roles)
                    .UsingEntity(
                        b => b.ToTable("UserRoles")
                    );
            });

            // Server mapping
            modelBuilder.Entity<ServerDbModel>(builder =>
            {
                builder.HasMany(s => s.TextChannels)
                    .WithOne(tc => tc.Server)
                    .HasForeignKey(tc => tc.ServerId);

                builder.HasMany(s => s.Users)
                    .WithMany(u => u.Servers)
                    .UsingEntity(
                        b => b.ToTable("UserServers")
                    );

                builder.HasMany(s => s.Roles)
                    .WithOne(r => r.Server)
                    .HasForeignKey(r => r.ServerId);
            });

            // Message mapping
            modelBuilder.Entity<MessageDbModel>(builder =>
            {
                builder.HasOne(m => m.User)
                    .WithMany(u => u.Messages)
                    .HasForeignKey(m => m.UserId);

                builder.HasOne(m => m.Channel)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.TextChannelId);
            });

            // TextChannel mapping
            modelBuilder.Entity<TextChannelDbModel>(builder =>
            {
                builder.HasMany(tc => tc.Messages)
                    .WithOne(m => m.Channel)
                    .HasForeignKey(m => m.TextChannelId);

                builder.HasOne(tc => tc.Server)
                    .WithMany(s => s.TextChannels)
                    .HasForeignKey(tc => tc.ServerId);
            });
        }
    }
}