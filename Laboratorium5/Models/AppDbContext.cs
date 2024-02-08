using Laboratorium5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Laboratorium5
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Piosenka> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Album.db");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Dane//

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>().HasData(
                new Album { Id = 1, Nazwa = "Jeszcze 5 minut", Zespol = "Kizo", Notowanie = 4124124, Data_wydania = new DateTime(2021, 11, 15) },
                new Album { Id = 2, Nazwa = "Uczta", Zespol = "Sanah", Notowanie = 52323423, Data_wydania = new DateTime(2022, 08, 15) }
            );

            modelBuilder.Entity<Piosenka>().HasData(
                new Piosenka { Id = 1, Tytul = "Piosenka 1", Czas_trwania = new TimeSpan(0, 3, 30), AlbumId = 1 },
                new Piosenka { Id = 2, Tytul = "Piosenka 2", Czas_trwania = new TimeSpan(0, 4, 15), AlbumId = 1 },
                new Piosenka { Id = 3, Tytul = "Piosenka 3", Czas_trwania = new TimeSpan(0, 3, 50), AlbumId = 1 },
                new Piosenka { Id = 4, Tytul = "Piosenka 1", Czas_trwania = new TimeSpan(0, 3, 45), AlbumId = 2 },
                new Piosenka { Id = 5, Tytul = "Piosenka 2", Czas_trwania = new TimeSpan(0, 4, 20), AlbumId = 2 }
            );

            modelBuilder.Entity<Album>()
         .HasMany(album => album.Piosenki) 
         .WithOne(piosenka => piosenka.Album) 
         .HasForeignKey(piosenka => piosenka.AlbumId) 
         .OnDelete(DeleteBehavior.Cascade);





            //Uzytkownicy//

            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            // dodanie roli administratora
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            // utworzenie administratora jako użytkownika
            var admin = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "adam@wsei.edu.pl",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADAM@WSEI.EDU.PL"
            };

            // haszowanie hasła
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "1234abcd!@#$ABCD");

            // zapisanie użytkownika
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            // przypisanie roli administratora użytkownikowi
            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
            string USER_ROLE_ID = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "user",
                NormalizedName = "USER",
                Id = USER_ROLE_ID,
                ConcurrencyStamp = USER_ROLE_ID
            });
            string USER_ID = Guid.NewGuid().ToString();

            var user = new IdentityUser
            {
                Id = USER_ID,
                Email = "user@esei.edu.pl",
                EmailConfirmed = true,
                UserName = "user",
                NormalizedUserName = "USER",
                NormalizedEmail = "USER@WSEI.EDU.PL"

            };

            user.PasswordHash = ph.HashPassword(user, "userPassword123");
            modelBuilder.Entity<IdentityUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = USER_ROLE_ID,
                UserId = USER_ID
            });

        }
    }
}
