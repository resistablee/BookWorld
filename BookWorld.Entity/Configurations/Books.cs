using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWorld.Entity.Configurations
{
    public class Books : IEntityTypeConfiguration<Entities.Books>
    {
        public void Configure(EntityTypeBuilder<Entities.Books> book)
        {
            book.ToTable("Books");
            book.HasKey(x => x.Id);
            book.Property(x => x.Name).HasMaxLength(100);
            book.Property(x => x.Name).IsRequired();
            book.Property(x => x.Author).HasMaxLength(100);
        }
    }
}
