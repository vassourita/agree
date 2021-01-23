using Microsoft.EntityFrameworkCore;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Infrastructure.Data.Mappings;

namespace Agree.Athens.Infrastructure.Data.Contexts
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

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>(new RoleMap().Configure);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Server>(new ServerMap().Configure);
            modelBuilder.Entity<Channel>(new ChannelMap().Configure);
            modelBuilder.Entity<Message>(new MessageMap().Configure);
            modelBuilder.Entity<Category>(new CategoryMap().Configure);
            modelBuilder.Entity<ServerUser>(new ServerUserMap().Configure);
            modelBuilder.Entity<ServerUserRole>(new ServerUserRoleMap().Configure);
        }
    }
}