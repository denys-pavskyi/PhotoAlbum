using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InternetPhotoAlbum_Tests.DAL_Tests
{
    [TestFixture]
    public class AlbumPhotoRepositoryTests
    {


        [TestCase(1)]
        [TestCase(2)]
        public async Task AlbumPhotoRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);

            var albumPhoto = await albumPhotoRepository.GetByIdAsync(id);

            var expected = ExpectedAlbumPhotos.FirstOrDefault(x => x.Id == id);

            Assert.That(albumPhoto, Is.EqualTo(expected).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);

            var albumPhotos = await albumPhotoRepository.GetAllAsync();

            Assert.That(albumPhotos, Is.EqualTo(ExpectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);
            var albumPhoto = new AlbumPhoto { Id = 3 };

            await albumPhotoRepository.AddAsync(albumPhoto);
            await context.SaveChangesAsync();

            Assert.That(context.AlbumPhotos.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);

            await albumPhotoRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.AlbumPhotos.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);
            var albumPhoto = new AlbumPhoto
            {
                Id = 1,
                PhotoId = 1,
                AlbumId = 1,
                AdditionDate = new DateTime(2020, 1, 1)

            };

            albumPhotoRepository.Update(albumPhoto);
            await context.SaveChangesAsync();

            Assert.That(albumPhoto, Is.EqualTo(new AlbumPhoto
            {
                Id = 1,
                PhotoId = 1,
                AlbumId = 1,
                AdditionDate = new DateTime(2020, 1, 1)
            }).Using(new AlbumPhotoEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);

            var albumPhoto = await albumPhotoRepository.GetByIdWithDetailsAsync(1);

            var expected = ExpectedAlbumPhotos.FirstOrDefault(x => x.Id == 1);
            var expectedAlbum = ExpectedAlbums.FirstOrDefault(x => x.Id == albumPhoto.AlbumId);
            var expectedPhoto = ExpectedPhotos.FirstOrDefault(x => x.Id == albumPhoto.PhotoId);

            Assert.That(albumPhoto,
                Is.EqualTo(expected).Using(new AlbumPhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(albumPhoto.Album,
                Is.EqualTo(expectedAlbum).Using(new AlbumEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(albumPhoto.Photo,
                Is.EqualTo(expectedPhoto).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


        }

        [Test]
        public async Task AlbumPhotoRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var albumPhotoRepository = new AlbumPhotoRepository(context);

            var albumPhotos = await albumPhotoRepository.GetAllWithDetailsAsync();

            Assert.That(albumPhotos,
                Is.EqualTo(ExpectedAlbumPhotos).Using(new AlbumPhotoEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(albumPhotos.Select(i => i.Album).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedAlbums).Using(new AlbumEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



            Assert.That(albumPhotos.Select(i => i.Photo).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



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
                new Photo{Id=1, Title="Photo1", PhotoPath="PhotoPath1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoPath="PhotoPath2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=1}
            };



    }
}
