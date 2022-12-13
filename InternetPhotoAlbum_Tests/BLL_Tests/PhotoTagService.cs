using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using InternetPhotoAlbum_Tests;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum_Tests.BLL_Tests
{
    public class PhotoTagServiceTests
    {
        [Test]
        public async Task PhotoTagService_GetAll_ReturnsAllPhotoTags()
        {
            //arrange
            var expected = GetTestPhotoTagModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.PhotoTagRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestPhotoTagEntities.AsEnumerable());

            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoTagService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PhotoTagService_GetById_ReturnsPhotoTagModel()
        {
            //arrange
            var expected = GetTestPhotoTagModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.PhotoTagRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestPhotoTagEntities.First());

            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoTagService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task PhotoTagService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoTagRepository.AddAsync(It.IsAny<PhotoTag>()));

            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoTag = GetTestPhotoTagModels.First();

            //act
            await photoTagService.AddAsync(photoTag);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoTagRepository.AddAsync(It.Is<PhotoTag>(x =>
                            x.Id == photoTag.Id &&
                            x.PhotoId == photoTag.PhotoId &&
                            x.TagId == photoTag.TagId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task PhotoTagService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoTagRepository.AddAsync(It.IsAny<PhotoTag>()));

            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoTag = GetTestPhotoTagModels.First();
            photoTag.PhotoId = -1;

            //act
            Func<Task> act = async () => await photoTagService.AddAsync(photoTag);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }




        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task PhotoTagService_DeleteAsync_DeletesPhotoTag(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoTagRepository.DeleteByIdAsync(It.IsAny<int>()));
            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await photoTagService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoTagRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task PhotoTagService_UpdateAsync_UpdatesPhotoTag()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoTagRepository.Update(It.IsAny<PhotoTag>()));


            var photoTagService = new PhotoTagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoTag = GetTestPhotoTagModels.First();

            //act
            await photoTagService.UpdateAsync(photoTag);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoTagRepository.Update(It.Is<PhotoTag>(x =>
                            x.Id == photoTag.Id &&
                            x.PhotoId == photoTag.PhotoId &&
                            x.TagId == photoTag.TagId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }







        #region Data

        public List<PhotoTagModel> GetTestPhotoTagModels =>
            new List<PhotoTagModel>()
            {
                new PhotoTagModel{ Id = 1, PhotoId = 1, TagId = 1},
                new PhotoTagModel{ Id = 2, PhotoId = 2, TagId = 2 }
            };

        public List<PhotoTag> GetTestPhotoTagEntities =>
           new List<PhotoTag>()
           {
                new PhotoTag{ Id = 1, PhotoId = 1, TagId = 1},
                new PhotoTag{ Id = 2, PhotoId = 2, TagId = 2 }
           };

        #endregion
    }
}
