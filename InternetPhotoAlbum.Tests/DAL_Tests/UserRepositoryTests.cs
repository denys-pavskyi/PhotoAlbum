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
    public class UserRepositoryTests
    {


        [TestCase(1)]
        [TestCase(2)]
        public async Task UserRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByIdAsync(id);

            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var users = await userRepository.GetAllAsync();

            Assert.That(users, Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);
            var numberOfUsers = context.Users.Count();
            var user = new User { Id = 3, UserName = "Username3", FirstName = "FirstName3", LastName = "LastName3", Password = "P@ssw0rd3", Age = 31, RegistrationDate = new DateTime(2021, 5, 3), EmailAddress = "user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User };

            await userRepository.AddAsync(user);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(numberOfUsers + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);
            var usersNumber = context.Users.Count();
            await userRepository.DeleteByIdAsync(2);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(usersNumber - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task UserRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);
            var user = new User
            {
                Id = 1,
                UserName = "Username22",
                FirstName = "FirstName22",
                LastName = "LastName22",
                Password = "P@ssw0rd22",
                Age = 31,
                RegistrationDate = new DateTime(2021, 5, 3),
                EmailAddress = "user1@gmail.com",
                BirthDate = new DateTime(1991, 3, 5),
                Role = Role.User
            };



            userRepository.Update(user);
            await context.SaveChangesAsync();

            Assert.That(user, Is.EqualTo(new User
            {
                Id = 1,
                UserName = "Username22",
                FirstName = "FirstName22",
                LastName = "LastName22",
                Password = "P@ssw0rd22",
                Age = 31,
                RegistrationDate = new DateTime(2021, 5, 3),
                EmailAddress = "user1@gmail.com",
                BirthDate = new DateTime(1991, 3, 5),
                Role = Role.User
            }).Using(new UserEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByIdWithDetailsAsync(1);

            
            var expectedAlbums = ExpectedAlbums.Where(x => x.UserId == user.Id).OrderBy(i => i.Id);
            var expectedPhotoRatings = ExpectedPhotoRatings.Where(x => x.UserId == user.Id).OrderBy(i => i.Id);
            var expectedPhotos = ExpectedPhotos.Where(x => x.UserId == user.Id).OrderBy(i => i.Id);
            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == 1);

            Assert.That(user,
                Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


          
            Assert.That(user.PhotoRatings,
                Is.EqualTo(expectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            Assert.That(user.Albums,
                Is.EqualTo(expectedAlbums).Using(new AlbumEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(user.Photos,
                Is.EqualTo(expectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");


        }

        [Test]
        public async Task AlbumPhotoRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userRepository = new UserRepository(context);

            var users = await userRepository.GetAllWithDetailsAsync();

            Assert.That(users,
                Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            


            Assert.That(users.SelectMany(x => x.PhotoRatings).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotoRatings).Using(new PhotoRatingEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(users.SelectMany(x => x.Photos).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(users.SelectMany(x => x.Albums).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedAlbums).Using(new AlbumEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



        }


        public static IEnumerable<User> ExpectedUsers =>
           new[]
           {
               new User { Id = 1,UserName = "Username1",  FirstName="FirstName1", LastName = "LastName1", Password="P@ssw0rd1", Age = 31, RegistrationDate = new DateTime(2021,5,3), EmailAddress="user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User },
               new User { Id = 2, UserName = "Username2", FirstName = "FirstName2", LastName = "LastName2", Password = "P@ssw0rd2", Age = 26, RegistrationDate = new DateTime(2022, 10,13), EmailAddress = "user2@gmail.com", BirthDate = new DateTime(1996, 10,17), Role = Role.Admin }
           };

        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=2}
            };

        public static IEnumerable<Album> ExpectedAlbums =>
           new[]
           {
                new Album{Id=1, Title="Album1", NumberOfPictures = 2, CreationDate = new DateTime(2022,12,2), UserId=1},
                new Album{Id=2, Title="Album2", NumberOfPictures = 1, CreationDate = new DateTime(2022,12,3), UserId=2}
           };

        public static IEnumerable<PhotoRating> ExpectedPhotoRatings =>
            new[]
            {
                new PhotoRating { Id = 1, Grade = 5d, PhotoId = 2, RatingDate = new DateTime(2022, 11, 15), UserId = 2 },
                new PhotoRating { Id = 2, Grade = 4d, PhotoId = 1, RatingDate = new DateTime(2022, 11, 12), UserId = 1 }
            };

    }
}
