using Microsoft.EntityFrameworkCore;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Infrastructure.Data.EntityFramework.Mappings;
using Microsoft.Extensions.Configuration;
using System;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ServerUser> ServerUsers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Role>(new RoleMap());
            modelBuilder.ApplyConfiguration<User>(new UserMap());
            modelBuilder.ApplyConfiguration<Server>(new ServerMap());
            modelBuilder.ApplyConfiguration<Channel>(new ChannelMap());
            modelBuilder.ApplyConfiguration<Message>(new MessageMap());
            modelBuilder.ApplyConfiguration<Category>(new CategoryMap());
            modelBuilder.ApplyConfiguration<ServerUser>(new ServerUserMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = ConfigurationFactory.CreateConfiguration();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine);

            base.OnConfiguring(optionsBuilder);
        }
    }
}