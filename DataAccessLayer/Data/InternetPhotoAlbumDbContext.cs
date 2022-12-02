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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }



    }
}
