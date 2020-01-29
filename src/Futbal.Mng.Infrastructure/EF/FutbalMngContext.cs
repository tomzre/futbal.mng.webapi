using System;
using System.Threading;
using System.Threading.Tasks;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.EF.Configuration;
using Futbal.Mng.Infrastructure.EventHandler.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Futbal.Mng.Infrastructure.EF {
    public class FutbalMngContext : DbContext {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<UserGame> UserGame { get; set; }

        public FutbalMngContext (DbContextOptions<FutbalMngContext> opts) : base (opts) {
            ChangeTracker.StateChanged += OnEntityStateChanged;
            ChangeTracker.Tracked += OnEntityTracked;
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration (new UserEntityTypeConfiguration ());
            modelBuilder.ApplyConfiguration (new GameEntityTypeConfiguration ());
            //modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
            modelBuilder.ApplyConfiguration (new GameUserTypeConfiguration ());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            AggragateRoot aggRoot = null;
            if (e.Entry.Entity is AggragateRoot) {
                aggRoot = e.Entry.Entity as AggragateRoot;
                foreach (IDomainEvent domainEvent in aggRoot.DomainEvents) {
                    if(!e.FromQuery && e.Entry.State == EntityState.Added)
                        DomainEvents.Dispatch (domainEvent);
                    }
                }
        }

        private void OnEntityStateChanged (object sender, EntityStateChangedEventArgs e) {
            AggragateRoot aggRoot = null;
            if (e.Entry.Entity is AggragateRoot) {
                aggRoot = e.Entry.Entity as AggragateRoot;
                foreach (IDomainEvent domainEvent in aggRoot.DomainEvents) {
                    if (e.NewState == EntityState.Modified) {
                        DomainEvents.Dispatch (domainEvent);
                    }
                }
            }
        }
    }
}