namespace Agree.Allow.Infrastructure.Data;

using System;
using System.Diagnostics;
using Agree.Allow.Domain;
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
        });
    }
}