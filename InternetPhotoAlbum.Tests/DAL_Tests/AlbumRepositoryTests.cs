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
    public class AlbumRepositoryTests
    {


        [TestCase(1)]
        [TestCase(2)]
        public async Task AlbumRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);

            var album = await albumRepository.GetByIdAsync(id);

            var expected = ExpectedAlbums.FirstOrDefault(x => x.Id == id);

            Assert.That(album, Is.EqualTo(expected).Using(new AlbumEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task AlbumRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);

            var albums = await albumRepository.GetAllAsync();

            Assert.That(albums, Is.EqualTo(ExpectedAlbums).Using(new AlbumEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AlbumRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);
            var album = new Album { Id = 3, Title="Album3", UserId = 1, CreationDate = DateTime.Now };

            await albumRepository.AddAsync(album);
            await context.SaveChangesAsync();

            Assert.That(context.Albums.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task AlbumRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);
            var albumsNumber = context.Albums.Count();
            await albumRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Albums.Count(), Is.EqualTo(albumsNumber-1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task AlbumRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);
            var album = new Album
            {
                Id = 1,
                Title = "Album111",
                NumberOfPictures = 4,
                CreationDate = new DateTime(2022, 12, 2),
                UserId = 1

            };

            albumRepository.Update(album);
            await context.SaveChangesAsync();

            Assert.That(album, Is.EqualTo(new Album
            {
                Id = 1,
                Title = "Album111",
                NumberOfPictures = 4,
                CreationDate = new DateTime(2022, 12, 2),
                UserId = 1
            }).Using(new AlbumEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AlbumRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);

            var album = await albumRepository.GetByIdWithDetailsAsync(1);

            var expectedUser = ExpectedUsers.FirstOrDefault(x => x.Id == album.UserId);
            var expectedAlbumPhotos = ExpectedAlbumPhotos.Where(x => x.AlbumId == album.Id);
            var expected = ExpectedAlbums.FirstOrDefault(x => x.Id == 1);
            
            Assert.That(album,
                Is.EqualTo(expected).Using(new AlbumEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(album.User,
                Is.EqualTo(expectedUser).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(album.AlbumPhotos,
                Is.EqualTo(expectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");


        }

        [Test]
        public async Task AlbumRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumRepository = new AlbumRepository(context);

            var albums = await albumRepository.GetAllWithDetailsAsync();

            Assert.That(albums,
                Is.EqualTo(ExpectedAlbums).Using(new AlbumEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(albums.Select(i => i.User).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



            Assert.That(albums.SelectMany(x=>x.AlbumPhotos).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



        }



        public static IEnumerable<AlbumPhoto> ExpectedAlbumPhotos =>
            new[]
            {
                new AlbumPhoto{Id = 1, PhotoId = 1, AlbumId = 1, AdditionDate = new DateTime(2022,12,3)},
                new AlbumPhoto{Id = 2, PhotoId = 2, AlbumId = 2, AdditionDate = new DateTime(2022,12,3)},
            };

        public static IEnumerable<Album> ExpectedAlbums =>
            new[]
            {
                new Album{Id=1, Title="Album1", NumberOfPictures = 2, CreationDate = new DateTime(2022,12,2), UserId=1},
                new Album{Id=2, Title="Album2", NumberOfPictures = 1, CreationDate = new DateTime(2022,12,3), UserId=2}
            };

        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=1}
            };
        public static IEnumerable<User> ExpectedUsers =>
            new[]
            {
               new User { Id = 1,UserName = "Username1",  FirstName="FirstName1", LastName = "LastName1", Password="P@ssw0rd1" , RegistrationDate = new DateTime(2021,5,3), EmailAddress="user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User },
               new User { Id = 2, UserName = "Username2", FirstName = "FirstName2", LastName = "LastName2", Password = "P@ssw0rd2" , RegistrationDate = new DateTime(2022, 10,13), EmailAddress = "user2@gmail.com", BirthDate = new DateTime(1996, 10,17), Role = Role.Admin }
            };


    }
}
