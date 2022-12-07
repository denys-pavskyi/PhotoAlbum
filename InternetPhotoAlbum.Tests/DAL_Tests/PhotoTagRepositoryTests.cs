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
    public class PhotoTagRepositoryTests
    {

            [TestCase(1)]
            [TestCase(2)]
            public async Task PhotoTagRepository_GetByIdAsync_ReturnsSingleValue(int id)
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);

                var photoTag = await photoTagRepository.GetByIdAsync(id);

                var expected = ExpectedPhotoTags.FirstOrDefault(x => x.Id == id);

                Assert.That(photoTag, Is.EqualTo(expected).Using(new PhotoTagEqualityComparer()), message: "GetByIdAsync method works incorrect");
            }

            [Test]
            public async Task PhotoTagRepository_GetAllAsync_ReturnsAllValues()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);

                var photoTags = await photoTagRepository.GetAllAsync();

                Assert.That(photoTags, Is.EqualTo(ExpectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetAllAsync method works incorrect");
            }

            [Test]
            public async Task PhotoTagRepository_AddAsync_AddsValueToDatabase()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);
                var numberOfPhotoTags = context.PhotoTags.Count();
                var photoTag = new PhotoTag { Id = 4, PhotoId = 2, TagId = 1 };

                await photoTagRepository.AddAsync(photoTag);
                await context.SaveChangesAsync();

                Assert.That(context.PhotoTags.Count(), Is.EqualTo(numberOfPhotoTags + 1), message: "AddAsync method works incorrect");
            }

            [Test]
            public async Task PhotoTagRepository_DeleteByIdAsync_DeletesEntity()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);
                var photoTagsNumber = context.PhotoTags.Count();
                await photoTagRepository.DeleteByIdAsync(2);
                await context.SaveChangesAsync();

                Assert.That(context.PhotoTags.Count(), Is.EqualTo(photoTagsNumber - 1), message: "DeleteByIdAsync works incorrect");
            }

            [Test]
            public async Task PhotoTagRepository_Update_UpdatesEntity()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);
                var photoTag = new PhotoTag
                {
                    Id = 1,
                    PhotoId = 2,
                    TagId = 1
                };


                photoTagRepository.Update(photoTag);
                await context.SaveChangesAsync();

                Assert.That(photoTag, Is.EqualTo(new PhotoTag
                {
                    Id = 1,
                    PhotoId = 2,
                    TagId = 1
                }).Using(new PhotoTagEqualityComparer()), message: "Update method works incorrect");
            }

            [Test]
            public async Task PhotoTagRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);

                var photoTag = await photoTagRepository.GetByIdWithDetailsAsync(1);

                var expectedPhoto = ExpectedPhotos.FirstOrDefault(x => x.Id == photoTag.PhotoId);
                var expectedTag = ExpectedTags.FirstOrDefault(x => x.Id == photoTag.TagId);
                var expected = ExpectedPhotoTags.FirstOrDefault(x => x.Id == 1);

                Assert.That(photoTag,
                    Is.EqualTo(expected).Using(new PhotoTagEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


                Assert.That(photoTag.Photo,
                    Is.EqualTo(expectedPhoto).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");
                Assert.That(photoTag.Tag,
                    Is.EqualTo(expectedTag).Using(new TagEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");



            }

            [Test]
            public async Task PhotoTagRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
            {
                using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

                var photoTagRepository = new PhotoTagRepository(context);

                var photoTags = await photoTagRepository.GetAllWithDetailsAsync();

                Assert.That(photoTags,
                    Is.EqualTo(ExpectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

               
                Assert.That(photoTags.Select(i => i.Photo).Distinct().OrderBy(i => i.Id),
                   Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
                Assert.That(photoTags.Select(i => i.Tag).Distinct().OrderBy(i => i.Id),
                  Is.EqualTo(ExpectedTags).Using(new TagEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            }


            public static IEnumerable<PhotoTag> ExpectedPhotoTags =>
            new[]
            {
                new PhotoTag { Id = 1, PhotoId = 1, TagId = 1},
                new PhotoTag { Id = 2, PhotoId = 2, TagId = 2 }
            };

            public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=2}
            };


            public static IEnumerable<Tag> ExpectedTags =>
                new[]
                {
                    new Tag { Id = 1, Title = "Tag1" },
                    new Tag { Id = 2, Title = "Tag2" }
                };


        
    }
}
