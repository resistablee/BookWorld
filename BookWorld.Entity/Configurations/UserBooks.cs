using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWorld.Entity.Configurations
{
    public class UserBooks : IEntityTypeConfiguration<Entities.UserBooks>
    {
        public void Configure(EntityTypeBuilder<Entities.UserBooks> builder)
        {
            builder.ToTable("UserBooks");
            builder.Property(x => x.BookID).IsRequired();
            builder.Property(x => x.UserID).IsRequired();
            builder.HasOne(x => x.UserRelation).WithMany(x => x.Relation1).HasForeignKey(x => x.UserID).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.BookRelation).WithMany(x => x.Relation2).HasForeignKey(x => x.BookID).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
