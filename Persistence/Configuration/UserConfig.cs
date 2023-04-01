using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    /// <UserConfig>
    /// This class contains the configuration for the client and will map all the characteristics of the class to the database.
    /// </UserConfig>
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        /// <Configure>
        /// This method contains the characteristics used in the database, string size is placed as well as whether 
        /// it is required or not, this will mark if it is null or not, all those characteristics that the table and 
        /// its columns will have are placed here.
        /// </Configure>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User")
                .HasOne(x => x.Client)
                .WithOne()
                .HasPrincipalKey<User>(x => x.ClientId);
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserName)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(x => x.Password)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.Level)
                .IsRequired();
        }
    }
}
