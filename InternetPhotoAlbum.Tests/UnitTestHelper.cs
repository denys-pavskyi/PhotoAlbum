using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InternetPhotoAlbum.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<InternetPhotoAlbumDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<InternetPhotoAlbumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new InternetPhotoAlbumDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }


        public static void SeedData(InternetPhotoAlbumDbContext context)
        {
            context.AlbumPhotos.AddRange(
                new AlbumPhoto { Id = 1, PhotoId = 1, AlbumId = 1, AdditionDate = new DateTime(2022, 12, 3) },
                new AlbumPhoto { Id = 2, PhotoId = 2, AlbumId = 1, AdditionDate = new DateTime(2022, 12, 3) }
            );


            context.Photos.AddRange(
                    new Photo { Id = 1, Title = "Photo1", PhotoUrl = "PhotoUrl1", TotalRating = 3.4d, UploadDate = new DateTime(2022, 11, 29), UserId = 1 },
                    new Photo { Id = 2, Title = "Photo2", PhotoUrl = "PhotoUrl2", TotalRating = 2.5d, UploadDate = new DateTime(2022, 11, 29), UserId = 1 },
                    new Photo { Id = 3, Title = "Photo3", PhotoUrl = "PhotoUrl3", TotalRating = 1.1d, UploadDate = new DateTime(2022, 11, 27), UserId = 2 }
                );

            context.Albums.AddRange(
                new Album { Id = 1, Title = "Album1", NumberOfPictures = 2, CreationDate = new DateTime(2022, 12, 2), UserId = 1 },
                new Album { Id = 2, Title = "Album2", NumberOfPictures = 1, CreationDate = new DateTime(2022, 12, 3), UserId = 2 }
                );

            context.PhotosRating.AddRange(
                new PhotoRating { Id = 1, Grade = 5d, PhotoId = 2, RatingDate = new DateTime(2022, 11, 15), UserId = 2 },
                new PhotoRating { Id = 2, Grade = 4d, PhotoId = 1, RatingDate = new DateTime(2022, 11, 12), UserId = 1 }
                );


            context.Tags.AddRange(
                    new Tag { Id=1, Title="Tag1"},
                    new Tag { Id = 2, Title = "Tag2" }
                );

            context.PhotoTags.AddRange(
                    new PhotoTag { Id = 1, PhotoId = 1, TagId = 1},
                    new PhotoTag { Id = 2, PhotoId = 2, TagId = 1 },
                    new PhotoTag { Id = 3, PhotoId = 2, TagId = 2 }
                );

            context.Users.AddRange(
                    new User { Id = 1, }
                );


        }



    }
}
