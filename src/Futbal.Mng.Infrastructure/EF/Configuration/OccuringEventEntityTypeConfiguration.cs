using Futbal.Mng.Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futbal.Mng.Infrastructure.EF.Configuration
{
    public class OccuringEventEntityTypeConfiguration : IEntityTypeConfiguration<OccuringEvent>
    {
        public void Configure(EntityTypeBuilder<OccuringEvent> occuringEventConfiguration)
        {
            occuringEventConfiguration.ToTable("occuring_event");
            occuringEventConfiguration.HasOne(x => x.Game);

            
        }
    }
}