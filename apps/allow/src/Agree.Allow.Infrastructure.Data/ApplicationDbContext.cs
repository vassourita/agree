using System;
using Agree.Allow.Domain.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agree.Allow.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserAccount>(b =>
                b.HasIndex(u => new { u.Tag, u.DisplayName }).IsUnique());
        }
    }
}