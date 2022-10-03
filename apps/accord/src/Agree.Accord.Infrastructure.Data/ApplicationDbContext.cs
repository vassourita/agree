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
        : base(options) { }

    public DbSet<Server> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserAccount>(b =>
        {
            b.ToTable("UserAccounts");

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
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Server>(b =>
        {
            b.ToTable("Servers");

            b.HasKey(s => s.Id);

            b.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(s => s.Description)
                .HasMaxLength(300);

            b.Property(s => s.CreatedAt)
                .IsRequired();

            b.Property(s => s.PrivacyLevel)
                .HasColumnType("varchar(10)")
                .IsRequired()
                .HasConversion(
                    (privacyLevel) => privacyLevel.ToString(),
                    (value) => Enum.Parse<ServerPrivacy>(value)
                );

            b.HasMany(s => s.Members)
                .WithOne(m => m.Server)
                .HasForeignKey(m => m.ServerId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(s => s.Roles)
                .WithOne(r => r.Server)
                .HasForeignKey(r => r.ServerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(s => s.Categories)
                .WithOne(c => c.Server)
                .HasForeignKey(c => c.ServerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ServerMember>(b =>
        {
            b.ToTable("ServerMembers");

            b.HasKey(sm => new { sm.UserId, sm.ServerId });

            b.Property(sm => sm.UserId)
                .IsRequired();
            b.Property(sm => sm.ServerId)
                .IsRequired();

            b.Ignore(sm => sm.Id);

            b.HasOne(sm => sm.User)
                .WithMany(m => m.Servers)
                .HasForeignKey(sm => sm.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(sm => sm.Server)
                .WithMany(s => s.Members)
                .HasForeignKey(sm => sm.ServerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(sm => sm.Roles)
                .WithMany(r => r.ServerMembers)
                .UsingEntity("ServerMemberRoles");
        });

        modelBuilder.Entity<ServerRole>(b =>
        {
            b.ToTable("ServerRoles");

            b.HasKey(r => r.Id);

            b.Property(r => r.Id)
                .IsRequired();

            b.Property(r => r.ServerId)
                .IsRequired();

            b.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            b.HasOne(r => r.Server)
                .WithMany(s => s.Roles)
                .HasForeignKey(r => r.ServerId);

            b.HasMany(r => r.ServerMembers)
                .WithMany(sm => sm.Roles)
                .UsingEntity("ServerMemberRoles");
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable("Categories");

            b.HasKey(c => c.Id);

            b.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            b.HasOne(c => c.Server)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.ServerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Friendship>(b =>
        {
            b.ToTable("Friendships");

            b.Ignore(f => f.Id);

            b.HasKey(f => new { f.FromId, f.ToId });

            b.HasOne(c => c.To)
                .WithMany()
                .HasForeignKey(c => c.ToId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(c => c.From)
                .WithMany()
                .HasForeignKey(c => c.FromId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DirectMessage>(b =>
        {
            b.ToTable("DirectMessages");

            b.HasKey(f => f.Id);

            b.Property(f => f.Text)
                .IsRequired()
                .HasMaxLength(400);

            b.Property(f => f.Read)
                .IsRequired();

            b.HasOne(c => c.To)
                .WithMany()
                .HasForeignKey(c => c.ToId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(c => c.From)
                .WithMany()
                .HasForeignKey(c => c.FromId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(c => c.InReplyTo)
                .WithMany()
                .HasForeignKey(c => c.InReplyToId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(f => f.CreatedAt)
                .IsRequired();
        });
    }
}