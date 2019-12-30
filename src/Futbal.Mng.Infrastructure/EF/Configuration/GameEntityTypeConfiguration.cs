using Futbal.Mng.Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futbal.Mng.Infrastructure.EF.Configuration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> configuration)
        {
            configuration.ToTable("games")
                .HasKey(x => x.Id);

            configuration.OwnsOne(x => x.Place, address =>
                {
                    address.WithOwner().HasForeignKey("GameId");
                    address.ToTable("address");
                    address.Property<int>("Id")  // Id is a shadow property
                        .IsRequired();

                    address.HasKey("Id");
                }
            );
            configuration.HasMany(x => x.Attendees);

            var navigation = configuration.Metadata.FindNavigation(nameof(Game.Attendees));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}