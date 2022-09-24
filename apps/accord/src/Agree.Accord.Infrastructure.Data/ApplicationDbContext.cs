namespace Agree.Accord.Infrastructure.Data;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Domain.Social;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The <see cref="ApplicationDbContext"/> class implements the application database context.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Server> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserAccount>(b =>
        {
            b.HasKey(u => u.Id);

            b.Property(u => u.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            b.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(40);

            b.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            b.Property(u => u.Tag)
                .HasConversion(
                    (tag) => tag.Value,
                    (value) => DiscriminatorTag.Parse(value)
                )
                .IsRequired()
                .HasMaxLength(4);

            b.Ignore(u => u.NameTag);

            b.HasIndex(u => new { u.Tag, u.Username }).IsUnique();

            b.HasMany(u => u.Servers)
                .WithMany(s => s.Members);
        });

        modelBuilder.Entity<Server>(b =>
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

        modelBuilder.Entity<ServerRole>(b => b.HasOne(r => r.Server)
                .WithMany(s => s.Roles));

        modelBuilder.Entity<Category>(b =>
        {
            b.HasKey(c => c.Id);

            b.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            b.HasOne(c => c.Server)
                .WithMany(s => s.Categories);
        });

        modelBuilder.Entity<Friendship>(b =>
        {
            b.Ignore(f => f.Id);

            b.HasKey(f => new { f.FromId, f.ToId });

            b.HasOne(c => c.To);
            b.HasOne(c => c.From);
        });

        modelBuilder.Entity<DirectMessage>(b =>
        {
            b.HasKey(f => f.Id);

            b.Property(f => f.Text)
                .IsRequired()
                .HasMaxLength(400);

            b.HasOne(c => c.To);
            b.HasOne(c => c.From);
        });
    }
}