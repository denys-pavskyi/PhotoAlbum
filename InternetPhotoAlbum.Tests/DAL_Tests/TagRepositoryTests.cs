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
    public class TagRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task TagRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);

            var tag =  await tagRepository.GetByIdAsync(id);

            var expected = ExpectedTags.FirstOrDefault(x => x.Id == id);

            Assert.That(tag, Is.EqualTo(expected).Using(new TagEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task TagRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);

            var tags = await tagRepository.GetAllAsync();

            Assert.That(tags, Is.EqualTo(ExpectedTags).Using(new TagEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task TagRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);
            var numberOfPhotoTags = context.Tags.Count();
            var tag =  new Tag { Id = 3, Title = "Tag3" };

            await tagRepository.AddAsync(tag);
            await context.SaveChangesAsync();

            Assert.That(context.Tags.Count(), Is.EqualTo(numberOfPhotoTags + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task TagRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);
            var tagsNumber = context.Tags.Count();
            await tagRepository.DeleteByIdAsync(2);
            await context.SaveChangesAsync();

            Assert.That(context.Tags.Count(), Is.EqualTo(tagsNumber - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task TagRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);
            var tag = new Tag
            {
                Id = 1,
                Title = "Tag23"
            };


            tagRepository.Update(tag);
            await context.SaveChangesAsync();

            Assert.That(tag, Is.EqualTo(new Tag
            {
                Id = 1,
                Title = "Tag23"
            }).Using(new TagEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AlbumPhotoRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);

            var tag =  await tagRepository.GetByIdWithDetailsAsync(1);

            var expectedPhotoTags = ExpectedPhotoTags.Where(x => x.TagId == tag.Id).Distinct().OrderBy(i => i.Id);
            var expected = ExpectedTags.FirstOrDefault(x => x.Id == 1);

            Assert.That(tag,
                Is.EqualTo(expected).Using(new TagEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(tag.PhotoTags,
                Is.EqualTo(expectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");



        }

        [Test]
        public async Task AlbumPhotoRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var tagRepository = new TagRepository(context);

            var tags = await tagRepository.GetAllWithDetailsAsync();

            Assert.That(tags,
                Is.EqualTo(ExpectedTags).Using(new TagEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            var tmp = tags.SelectMany(x => x.PhotoTags).OrderBy(i => i.Id).ToList();

            Assert.That(tags.SelectMany(x => x.PhotoTags).OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotoTags).Using(new PhotoTagEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }



        public static IEnumerable<PhotoTag> ExpectedPhotoTags =>
            new[]
            {
                new PhotoTag { Id = 1, PhotoId = 1, TagId = 1},
                new PhotoTag { Id = 2, PhotoId = 2, TagId = 2 }
            };

        public static IEnumerable<Tag> ExpectedTags =>
                new[]
                {
                    new Tag { Id = 1, Title = "Tag1" },
                    new Tag { Id = 2, Title = "Tag2" }
                };

    }
}
