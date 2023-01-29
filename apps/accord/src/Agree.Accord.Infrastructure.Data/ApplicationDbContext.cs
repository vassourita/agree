namespace Agree.Accord.Infrastructure.Data;

using System;
using System.Diagnostics;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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