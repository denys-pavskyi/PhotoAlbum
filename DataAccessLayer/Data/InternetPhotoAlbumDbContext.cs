using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class InternetPhotoAlbumDbContext: DbContext
    {
        public InternetPhotoAlbumDbContext(DbContextOptions<InternetPhotoAlbumDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumPhoto> AlbumPhotos { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoRating> PhotosRating { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasOne(x=>x.User).WithMany(x=>x.Albums).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Album>()
                .HasMany(x=>x.AlbumPhotos).WithOne(x=>x.Album).HasForeignKey(x=>x.AlbumId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AlbumPhoto>()
                .HasOne(x => x.Photo).WithMany(x=>x.AlbumPhotos).HasForeignKey(x=>x.PhotoId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AlbumPhoto>()
                .HasOne(x => x.Album).WithMany(x => x.AlbumPhotos).HasForeignKey(x => x.AlbumId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Photo>()
                .HasMany(x => x.PhotoRatings).WithOne(x => x.Photo).HasForeignKey(x => x.PhotoId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Photo>()
                .HasMany(x => x.PhotoTags).WithOne(x => x.Photo).HasForeignKey(x => x.PhotoId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Photo>()
                .HasMany(x=>x.AlbumPhotos).WithOne(x=>x.Photo).HasForeignKey(x=>x.PhotoId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<PhotoRating>()
                .HasOne(x => x.Photo).WithMany(x => x.PhotoRatings).HasForeignKey(x => x.PhotoId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PhotoRating>()
                .HasOne(x => x.User).WithMany(x => x.PhotoRatings).HasForeignKey(x => x.PhotoId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PhotoTag>()
                .HasOne(x => x.Photo).WithMany(x => x.PhotoTags).HasForeignKey(x => x.PhotoId).OnDelete(DeleteBehavior.NoAction);
           


            modelBuilder.Entity<User>()
                .HasMany(x=>x.PhotoRatings).WithOne(x=>x.User).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Photos).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
            .HasMany(x => x.PhotoRatings).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
            .HasMany(x => x.Reports).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Report>()
                .HasOne(x => x.User).WithMany(x => x.Reports).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);


            SeedData(modelBuilder);


            


        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                    new Tag { Id = 1, Title = "Animals" },
                    new Tag { Id = 2, Title = "Cities" },
                    new Tag { Id = 3, Title = "Japan" },
                    new Tag { Id = 4, Title = "Rivers" },
                    new Tag { Id = 5, Title = "Mountains" },
                    new Tag { Id = 6, Title = "Cars" },
                    new Tag { Id = 7, Title = "Ukraine" }
                );


            var password1_encoded = "p@ssword1";
            var password2_encoded = "p@ssword2";
            var password3_encoded = "p@ssword3";
            var password_salt1 = CreateBase64Secret(128);
            var password_salt2 = CreateBase64Secret(128);
            var password_salt3 = CreateBase64Secret(128);

            var password1 = HashUsingPbkdf2(password1_encoded, password_salt1);
            var password2 = HashUsingPbkdf2(password2_encoded, password_salt2);
            var password3 = HashUsingPbkdf2(password3_encoded, password_salt3);

            modelBuilder.Entity<User>().HasData(
                    new User {Id = 1, BirthDate = new DateTime(1963,2,17), FirstName="Michael", LastName="Jordan",
                        EmailAddress = "michael_jordan@gmail.com", Password = password1, PasswordSalt = password_salt1, Role = Role.Admin,
                        UserName = "jordan100", RegistrationDate= new DateTime(2020, 2, 5), UserStatus= UserStatus.Active
                    },
                    new User
                    {
                        Id = 2,
                        BirthDate = new DateTime(1990, 12, 3),
                        FirstName = "John",
                        LastName = "Peterson",
                        EmailAddress = "john_peterson@gmail.com",
                        Password = password2,
                        PasswordSalt = password_salt2,
                        Role = Role.User,
                        UserName = "johny1",
                        RegistrationDate = new DateTime(2021, 11, 12),
                        UserStatus = UserStatus.Active
                    },
                    new User
                    {
                        Id = 3,
                        BirthDate = new DateTime(1963, 5, 8),
                        FirstName = "Barack",
                        LastName = "Obama",
                        EmailAddress = "barack_obama@gmail.com",
                        Password = password3,
                        PasswordSalt = password_salt3,
                        Role = Role.User,
                        UserName = "barack_usa",
                        RegistrationDate = new DateTime(2019, 1, 22),
                        UserStatus = UserStatus.Active
                    }

                );

            modelBuilder.Entity<Photo>().HasData(
                    new Photo { Id = 1, Title = "Deer",
                        UploadDate = new DateTime(2021,2,3),
                        TotalRating = 5d, 
                        Description= "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/animals1.jpg",
                        UserId = 1
                    },
                    new Photo
                    {
                        Id = 2,
                        Title = "Car in woods",
                        UploadDate = new DateTime(2022, 1, 3),
                        TotalRating = 3d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/car1.jpg",
                        UserId = 3
                    },
                    new Photo
                    {
                        Id = 3,
                        Title = "Japanese city",
                        UploadDate = new DateTime(2021, 1, 1),
                        TotalRating = 4d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/city4.jpg",
                        UserId = 1
                    },
                    new Photo
                    {
                        Id = 4,
                        Title = "Kyiv",
                        UploadDate = new DateTime(2021, 2, 3),
                        TotalRating = 5d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/ukraine1.jpg",
                        UserId = 2
                    },
                    new Photo
                    {
                        Id = 5,
                        Title = "Field",
                        UploadDate = new DateTime(2018, 11,16),
                        TotalRating = 5d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/ukraine3.jpg",
                        UserId = 2
                    },
                    new Photo
                    {
                        Id = 6,
                        Title = "Mercedes",
                        UploadDate = new DateTime(2015, 6, 22),
                        TotalRating = 2.5d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/cars1.jpg",
                        UserId = 1
                    },
                    new Photo
                    {
                        Id = 7,
                        Title = "Porsche",
                        UploadDate = new DateTime(2022, 3, 22),
                        TotalRating = 2.5d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/car2.jpg",
                        UserId = 3
                    },
                    new Photo
                    {
                        Id = 8,
                        Title = "Japan mountain",
                        UploadDate = new DateTime(2019, 6, 17),
                        TotalRating = 3d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/mountain1.jpg",
                        UserId = 1
                    },
                    new Photo
                    {
                        Id = 9,
                        Title = "Hills",
                        UploadDate = new DateTime(2018, 4, 19),
                        TotalRating = 4.7d,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod temDuisfugiat nulla pariaecat cupidata deserunt mollit anim id est laborum",
                        PhotoUrl = "/assets/images/db/nature1.jpg",
                        UserId = 1
                    }

                );


                

        }


        private string HashUsingPbkdf2(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        private static string CreateBase64Secret(int size)
        {
            var key = new byte[size];
            RandomNumberGenerator.Create().GetBytes(key);
            var secret = Convert.ToBase64String(key);

            return secret;
        }

    }
}
