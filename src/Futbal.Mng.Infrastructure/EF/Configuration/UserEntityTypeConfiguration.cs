using Futbal.Mng.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Futbal.Mng.Infrastructure.EF.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
        }
    }
}