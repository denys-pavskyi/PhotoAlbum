using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Castle.Core.Resource;
using FluentAssertions.Execution;
using System.Reflection;
using BuisnessLogicLayer.Validation;

namespace InternetPhotoAlbum.Tests.BLL_Tests
{
    public class AlbumPhotoServiceTests
    {
        [Test]
        public async Task AlbumPhotoService_GetAll_ReturnsAllAlbumPhotos()
        {
            //arrange
            var expected = GetTestAlbumPhotoModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.AlbumPhotoRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestAlbumPhotoEntities.AsEnumerable());

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await albumPhotoService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AlbumPhotoService_GetById_ReturnsAlbumPhotoModel()
        {
            //arrange
            var expected = GetTestAlbumPhotoModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.AlbumPhotoRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestAlbumPhotoEntities.First());

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await albumPhotoService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }


        

        [Test]
        public async Task AlbumPhotoService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumPhotoRepository.AddAsync(It.IsAny<AlbumPhoto>()));

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumPhoto = GetTestAlbumPhotoModels.First();

            //act
            await albumPhotoService.AddAsync(albumPhoto);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumPhotoRepository.AddAsync(It.Is<AlbumPhoto>(x =>
                            x.Id == albumPhoto.Id && x.PhotoId == albumPhoto.PhotoId &&
                            x.AdditionDate == albumPhoto.AdditionDate &&
                            x.AlbumId == albumPhoto.AlbumId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task AlbumPhotoService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumPhotoRepository.AddAsync(It.IsAny<AlbumPhoto>()));

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumPhoto = GetTestAlbumPhotoModels.First();
            albumPhoto.PhotoId = -1;

            //act
            Func<Task> act = async () => await albumPhotoService.AddAsync(albumPhoto);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }


        [Test]
        public async Task AlbumPhotoService_AddAsync_ThrowsMarketExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumPhotoRepository.AddAsync(It.IsAny<AlbumPhoto>()));

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await albumPhotoService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task AlbumPhotoService_DeleteAsync_DeletesAlbumPhoto(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumPhotoRepository.DeleteByIdAsync(It.IsAny<int>()));
            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await albumPhotoService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumPhotoRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        

        [Test]
        public async Task AlbumPhotoService_UpdateAsync_UpdatesAlbumPhoto()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumPhotoRepository.Update(It.IsAny<AlbumPhoto>()));
 

            var albumPhotoService = new AlbumPhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumPhoto = GetTestAlbumPhotoModels.First();

            //act
            await albumPhotoService.UpdateAsync(albumPhoto);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumPhotoRepository.Update(It.Is<AlbumPhoto>(x =>
                x.Id == albumPhoto.Id && x.PhotoId == albumPhoto.PhotoId &&
                            x.AdditionDate == albumPhoto.AdditionDate &&
                            x.AlbumId == albumPhoto.AlbumId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        

       

       

        #region Data

        public static List<AlbumPhotoModel> GetTestAlbumPhotoModels =>
            new()
            {
                new AlbumPhotoModel{ Id = 1, AlbumId= 1, PhotoId = 1, AdditionDate = new DateTime(2021,1,1) },
                new AlbumPhotoModel{ Id = 2, AlbumId= 2, PhotoId = 2, AdditionDate = new DateTime(2021,2,2) },
                new AlbumPhotoModel{ Id = 3, AlbumId= 2, PhotoId = 3, AdditionDate = new DateTime(2021,3,3) }
            };

        public static List<AlbumPhoto> GetTestAlbumPhotoEntities =>
           new()
           {
                new AlbumPhoto{ Id = 1, AlbumId= 1, PhotoId = 1, AdditionDate = new DateTime(2021,1,1) },
                new AlbumPhoto{ Id = 2, AlbumId= 2, PhotoId = 2, AdditionDate = new DateTime(2021,2,2) },
                new AlbumPhoto{ Id = 3, AlbumId= 2, PhotoId = 3, AdditionDate = new DateTime(2021,3,3) }
           };

        #endregion

    }
}
