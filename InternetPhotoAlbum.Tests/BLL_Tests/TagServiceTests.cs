using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests.BLL_Tests
{
    public class TagServiceTests
    {
        [Test]
        public async Task TagService_GetAll_ReturnsAllTags()
        {
            //arrange
            var expected = GetTestTagModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.TagRepository.GetAllAsync())
                .ReturnsAsync(GetTestTagEntities.AsEnumerable());

            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await tagService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task TagService_GetById_ReturnsTagModel()
        {
            //arrange
            var expected = GetTestTagModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.TagRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestTagEntities.First());

            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await tagService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task TagService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.TagRepository.AddAsync(It.IsAny<Tag>()));

            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var tag = GetTestTagModels.First();

            //act
            await tagService.AddAsync(tag);

            //assert
            mockUnitOfWork.Verify(x => x.TagRepository.AddAsync(It.Is<Tag>(x =>
                            x.Id == tag.Id &&
                            x.Title == tag.Title)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task TagService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithEmptyTitle()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.TagRepository.AddAsync(It.IsAny<Tag>()));

            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var tag = GetTestTagModels.First();
            tag.Title = string.Empty;

            //act
            Func<Task> act = async () => await tagService.AddAsync(tag);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }


        [Test]
        public async Task TagService_AddAsync_ThrowsMarketExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.TagRepository.AddAsync(It.IsAny<Tag>()));

            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await tagService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task TagService_DeleteAsync_DeletesTag(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.TagRepository.DeleteByIdAsync(It.IsAny<int>()));
            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await tagService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.TagRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task TagService_UpdateAsync_UpdatesTag()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.TagRepository.Update(It.IsAny<Tag>()));


            var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var tag = GetTestTagModels.First();

            //act
            await tagService.UpdateAsync(tag);

            //assert
            mockUnitOfWork.Verify(x => x.TagRepository.Update(It.Is<Tag>(x =>
                            x.Id == tag.Id &&
                            x.Title == tag.Title)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }







        #region Data

        public static List<TagModel> GetTestTagModels =>
            new()
            {
                new TagModel{ Id = 1, Title = "title1"},
                new TagModel{ Id = 2, Title = "title2"}
                
            };

        public static List<Tag> GetTestTagEntities =>
           new()
           {
                new Tag{ Id = 1, Title = "title1"},
                new Tag{ Id = 2, Title = "title2"}
           };

        #endregion
    }
}
