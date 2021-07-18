using BookWorld.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookWorld.DAL
{
    public class Context: DbContext
    {
        public Context (DbContextOptions<Context> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<UserBooks> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration da yer alan modelcreat leri toplu olarak ekleme yapar. Yapılacak olan configure classları IEntityTypeConfiguration dan miras alınmalı.
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly); Context sınıfı ve konfigürasyonlar aynı projede olmadığı için bu kod çalışmıyor. Aşağıdaki gibi elle tek tek eklememiz gerekiyor.
            modelBuilder.ApplyConfiguration(new Entity.Configurations.Books());
            modelBuilder.ApplyConfiguration(new Entity.Configurations.Users());
            
            //Seed data
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 1,
                Name = "Simyacı",
                Author = "Paulo Coelho"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 2,
                Name = "Suç ve Ceza",
                Author = "Fyodor Dostoyevski"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 3,
                Name = "Kürk Mantolu Madonna",
                Author = "Sabahattin Ali"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 4,
                Name = "Yabancı",
                Author = "Albert Camus"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 5,
                Name = "İçimizdeki Şeytan",
                Author = "Sabahattin Ali"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 6,
                Name = "Tutunamayanlar",
                Author = "Oğuz Atay"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 7,
                Name = "Olasılıksız",
                Author = "Adam Fawer"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 8,
                Name = "Tehlikeli Oyunlar",
                Author = "Oğuz Atay"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 9,
                Name = "Incognito - Beynin Gizli Hayatı",
                Author = "David Eagleman"
            });
            modelBuilder.Entity<Books>().HasData(new Books()
            {
                Id = 10,
                Name = "Klon",
                Author = "Kevin Guilfoile"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
