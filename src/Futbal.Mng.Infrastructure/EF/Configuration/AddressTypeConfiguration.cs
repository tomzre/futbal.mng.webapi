using Futbal.Mng.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futbal.Mng.Infrastructure.EF.Configuration
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> configuration)
        {
            configuration.ToTable("address");
            configuration.Property<int>("Id")  // Id is a shadow property
                .IsRequired();
            
            configuration.HasKey("Id");
        }
    }
}