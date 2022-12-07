using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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




            /*modelBuilder.Entity<Tag>().HasData(
                    new Tag {  Title = "Sightseeing"},

                );*/


        }



    }
}
