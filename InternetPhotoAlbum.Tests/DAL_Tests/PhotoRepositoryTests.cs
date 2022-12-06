using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests.DAL_Tests
{
    [TestFixture]
    public class PhotoRepositoryTests
    {

        [TestCase(1)]
        [TestCase(2)]
        public async Task PhotoRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);

            var photo = await photoRepository.GetByIdAsync(id);

            var expected = ExpectedPhotos.FirstOrDefault(x => x.Id == id);

            Assert.That(photo, Is.EqualTo(expected).Using(new PhotoEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);

            var photos = await photoRepository.GetAllAsync();

            Assert.That(photos, Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);
            var numberOfPhotos = context.Photos.Count();
            var photo = new Photo { Id = 3, Title = "Photo3", PhotoUrl = "PhotoUrl3", TotalRating = 4.5d, UploadDate = new DateTime(2022, 11, 10), UserId = 1 };

            await photoRepository.AddAsync(photo);
            await context.SaveChangesAsync();

            Assert.That(context.Photos.Count(), Is.EqualTo(numberOfPhotos + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);
            var photosNumber = context.Photos.Count();
            await photoRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Photos.Count(), Is.EqualTo(photosNumber - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task PhotoRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);
            var photo = new Photo { Id = 1,
                Title = "Photo6",
                PhotoUrl = "PhotoUrl6",
                TotalRating = 1d,
                UploadDate = new DateTime(2022, 11, 10),
                UserId = 1 
            };


            photoRepository.Update(photo);
            await context.SaveChangesAsync();

            Assert.That(photo, Is.EqualTo(new Photo
            {
                Id = 1,
                Title = "Photo6",
                PhotoUrl = "PhotoUrl6",
                TotalRating = 1d,
                UploadDate = new DateTime(2022, 11, 10),
                UserId = 1
            }).Using(new PhotoEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);

            var photo = await photoRepository.GetByIdWithDetailsAsync(1);

            var expectedUser = ExpectedUsers.FirstOrDefault(x => x.Id == photo.UserId);
            var expectedAlbumPhotos = ExpectedAlbumPhotos.Where(x => x.PhotoId == photo.Id).OrderBy(i => i.Id);
            var expectedPhotoRatings = ExpectedPhotoRatings.Where(x => x.PhotoId == photo.Id).OrderBy(i => i.Id);
            var expectedPhotoTags = ExpectedPhotoTags.Where(x => x.PhotoId == photo.Id).OrderBy(i => i.Id);
            var expected = ExpectedPhotos.FirstOrDefault(x => x.Id == 1);

            Assert.That(photo,
                Is.EqualTo(expected).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


            Assert.That(photo.User,
                Is.EqualTo(expectedUser).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(photo.PhotoRatings,
                Is.EqualTo(expectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            Assert.That(photo.AlbumPhotos,
                Is.EqualTo(expectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(photo.PhotoTags,
                Is.EqualTo(expectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");


        }

        [Test]
        public async Task AlbumPhotoRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRepository = new PhotoRepository(context);

            var photos = await photoRepository.GetAllWithDetailsAsync();

            Assert.That(photos,
                Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(photos.Select(i => i.User).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



            Assert.That(photos.SelectMany(x => x.PhotoTags).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            
            Assert.That(photos.SelectMany(x => x.AlbumPhotos).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            
            Assert.That(photos.SelectMany(x => x.PhotoRatings).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



        }





        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=2}
            };

        public static IEnumerable<User> ExpectedUsers =>
           new[]
           {
               new User { Id = 1,UserName = "Username1",  FirstName="FirstName1", LastName = "LastName1", Password="P@ssw0rd1" , RegistrationDate = new DateTime(2021,5,3), EmailAddress="user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User },
               new User { Id = 2, UserName = "Username2", FirstName = "FirstName2", LastName = "LastName2", Password = "P@ssw0rd2" , RegistrationDate = new DateTime(2022, 10,13), EmailAddress = "user2@gmail.com", BirthDate = new DateTime(1996, 10,17), Role = Role.Admin }
           };

        public static IEnumerable<AlbumPhoto> ExpectedAlbumPhotos =>
            new[]
            {
                new AlbumPhoto{Id = 1, PhotoId = 1, AlbumId = 1, AdditionDate = new DateTime(2022,12,3)},
                new AlbumPhoto{Id = 2, PhotoId = 2, AlbumId = 2, AdditionDate = new DateTime(2022,12,3)},
            };
        public static IEnumerable<PhotoRating> ExpectedPhotoRatings =>
            new[]
            {
                new PhotoRating { Id = 1, Grade = 5d, PhotoId = 2, RatingDate = new DateTime(2022, 11, 15), UserId = 2 },
                new PhotoRating { Id = 2, Grade = 4d, PhotoId = 1, RatingDate = new DateTime(2022, 11, 12), UserId = 1 }
            };

        public static IEnumerable<PhotoTag> ExpectedPhotoTags =>
            new[]
            {
                new PhotoTag { Id = 1, PhotoId = 1, TagId = 1},
                new PhotoTag { Id = 2, PhotoId = 2, TagId = 2 }
            };

    }
}
