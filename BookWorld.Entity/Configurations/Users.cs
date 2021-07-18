using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWorld.Entity.Configurations
{
    public class Users : IEntityTypeConfiguration<Entities.Users>
    {
        public void Configure(EntityTypeBuilder<Entities.Users> user)
        {
            user.ToTable("Users");
            user.HasKey(x => x.Id);
            user.Property(x => x.Name).HasMaxLength(100);
            user.Property(x => x.Surname).HasMaxLength(100);
            user.Property(x => x.Username).HasMaxLength(50);
            user.Property(x => x.Password).HasMaxLength(32);
            user.Property(x => x.Name).IsRequired();
            user.Property(x => x.Surname).IsRequired();
            user.Property(x => x.Username).IsRequired();
            user.Property(x => x.Password).IsRequired();
        }
    }
}
