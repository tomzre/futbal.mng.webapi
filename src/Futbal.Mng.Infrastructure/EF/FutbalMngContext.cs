using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Futbal.Mng.Infrastructure.EF
{
    public class FutbalMngContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<UserGame> UserGame { get; set; }

        public FutbalMngContext(DbContextOptions<FutbalMngContext> opts) : base(opts)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GameEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GameUserTypeConfiguration());
        }
    }
}