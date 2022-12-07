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
    public class PhotoRatingRepositoryTests
    {

        [TestCase(1)]
        [TestCase(2)]
        public async Task PhotoRatingRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);

            var photoRating = await photoRatingRepository.GetByIdAsync(id);

            var expected = ExpectedPhotoRatings.FirstOrDefault(x => x.Id == id);

            Assert.That(photoRating, Is.EqualTo(expected).Using(new PhotoRatingEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRatingRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);

            var photoRatings = await photoRatingRepository.GetAllAsync();

            Assert.That(photoRatings, Is.EqualTo(ExpectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRatingRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);
            var numberOfPhotoRatings = context.PhotosRating.Count();
            var photoRating = new PhotoRating { Id = 3, Grade = 5d, PhotoId = 2, RatingDate = new DateTime(2022, 11, 15), UserId = 2 };

            await photoRatingRepository.AddAsync(photoRating);
            await context.SaveChangesAsync();

            Assert.That(context.PhotosRating.Count(), Is.EqualTo(numberOfPhotoRatings+1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PhotoRatingRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);
            var photoRatingsNumber = context.PhotosRating.Count();
            await photoRatingRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.PhotosRating.Count(), Is.EqualTo(photoRatingsNumber - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task PhotoRatingRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);
            var photoRating = new PhotoRating
            {
                Id = 1,
                Grade = 2d,
                PhotoId = 2,
                RatingDate = new DateTime(2018, 11, 15),
                UserId = 2

            };

            photoRatingRepository.Update(photoRating);
            await context.SaveChangesAsync();

            Assert.That(photoRating, Is.EqualTo(new PhotoRating
            {
                Id = 1,
                Grade = 2d,
                PhotoId = 2,
                RatingDate = new DateTime(2018, 11, 15),
                UserId = 2
            }).Using(new PhotoRatingEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task PhotoRatingRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);

            var photoRating = await photoRatingRepository.GetByIdWithDetailsAsync(1);

            var expectedUser = ExpectedUsers.FirstOrDefault(x => x.Id == photoRating.UserId);
            var expectedPhoto = ExpectedPhotos.FirstOrDefault(x => x.Id == photoRating.PhotoId);
            var expected = ExpectedPhotoRatings.FirstOrDefault(x => x.Id == 1);

            Assert.That(photoRating,
                Is.EqualTo(expected).Using(new PhotoRatingEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


            Assert.That(photoRating.User,
                Is.EqualTo(expectedUser).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(photoRating.Photo,
                Is.EqualTo(expectedPhoto).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");


        }

        [Test]
        public async Task PhotoRatingRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var photoRatingRepository = new PhotoRatingRepository(context);

            var photoRatings = await photoRatingRepository.GetAllWithDetailsAsync();

            Assert.That(photoRatings,
                Is.EqualTo(ExpectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(photoRatings.Select(i => i.User).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



            Assert.That(photoRatings.Select(x => x.Photo).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



        }








        public static IEnumerable<PhotoRating> ExpectedPhotoRatings =>
            new[]
            {
                new PhotoRating { Id = 1, Grade = 5d, PhotoId = 2, RatingDate = new DateTime(2022, 11, 15), UserId = 2 },
                new PhotoRating { Id = 2, Grade = 4d, PhotoId = 1, RatingDate = new DateTime(2022, 11, 12), UserId = 1 }
            };

        public static IEnumerable<User> ExpectedUsers =>
            new[]
            {
               new User { Id = 1,UserName = "Username1",  FirstName="FirstName1", LastName = "LastName1", Password="P@ssw0rd1" , RegistrationDate = new DateTime(2021,5,3), EmailAddress="user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User },
               new User { Id = 2, UserName = "Username2", FirstName = "FirstName2", LastName = "LastName2", Password = "P@ssw0rd2" , RegistrationDate = new DateTime(2022, 10,13), EmailAddress = "user2@gmail.com", BirthDate = new DateTime(1996, 10,17), Role = Role.Admin }
            };


        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=1}
            };

    }
}
