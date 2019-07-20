using Futbal.Mng.Domain.Event;
using Microsoft.EntityFrameworkCore;

namespace Futbal.Mng.Infrastructure.EF.Configuration
{
    public class GameUserTypeConfiguration : IEntityTypeConfiguration<UserGame>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserGame> entity)
        {
            entity.HasKey(p => new {p.UserId, p.GameId } );

            entity.HasOne(pt => pt.Game)
                .WithMany(p => p.Attendees)
                .HasForeignKey(pt => pt.GameId);

            entity.HasOne(pt => pt.User)
                .WithMany(p => p.Games)
                .HasForeignKey(pt => pt.UserId);
        }
    }
}