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
    public class PhotoServiceTests
    {
        [Test]
        public async Task PhotoService_GetAll_ReturnsAllPhotos()
        {
            //arrange
            var expected = GetTestPhotoModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.PhotoRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestPhotoEntities.AsEnumerable());

            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PhotoService_GetById_ReturnsPhotoModel()
        {
            //arrange
            var expected = GetTestPhotoModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.PhotoRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestPhotoEntities.First());

            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task PhotoService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRepository.AddAsync(It.IsAny<Photo>()));

            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photo = GetTestPhotoModels.First();

            //act
            await photoService.AddAsync(photo);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRepository.AddAsync(It.Is<Photo>(x =>
                            x.Id == photo.Id &&
                            x.UserId == photo.UserId &&
                            x.Description == photo.Description &&
                            x.Title == photo.Title &&
                            x.PhotoUrl == photo.PhotoUrl)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task PhotoService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRepository.AddAsync(It.IsAny<Photo>()));

            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photo = GetTestPhotoModels.First();
            photo.UserId = -1;

            //act
            Func<Task> act = async () => await photoService.AddAsync(photo);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }


        [Test]
        public async Task PhotoService_AddAsync_ThrowsMarketExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRepository.AddAsync(It.IsAny<Photo>()));

            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await photoService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task PhotoService_DeleteAsync_DeletesPhoto(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRepository.DeleteByIdAsync(It.IsAny<int>()));
            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await photoService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task PhotoService_UpdateAsync_UpdatesPhoto()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRepository.Update(It.IsAny<Photo>()));


            var photoService = new PhotoService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photo = GetTestPhotoModels.First();

            //act
            await photoService.UpdateAsync(photo);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRepository.Update(It.Is<Photo>(x =>
                            x.Id == photo.Id &&
                            x.UserId == photo.UserId &&
                            x.Description == photo.Description &&
                            x.Title == photo.Title &&
                            x.PhotoUrl == photo.PhotoUrl)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }







        #region Data

        public List<PhotoModel> GetTestPhotoModels =>
            new List<PhotoModel>()
            {
                new PhotoModel{Id = 1, Title = "title1", Description = "desc1", UserId =1, PhotoUrl="url1"
                , UploadDate = new DateTime(2020,1,1), TotalRating = 5d, AlbumPhotoIds = new[]{1,2 }, PhotoRatingIds = new List<int>(), PhotoTagsIds = new List<int>()},
                new PhotoModel{Id = 2, Title = "title2", Description = "desc2", UserId =2, PhotoUrl="url2"
                , UploadDate = new DateTime(2020,2,2), TotalRating = 3d, PhotoTagsIds = new List<int>(), PhotoRatingIds = new List<int>(), AlbumPhotoIds = new List<int>()}
            };

        public List<Photo> GetTestPhotoEntities =>
           new List<Photo>()
           {
                new Photo{Id = 1, Title = "title1", Description = "desc1", UserId =1, PhotoUrl="url1"
                , UploadDate = new DateTime(2020,1,1), TotalRating = 5d, 
                    AlbumPhotos = new List<AlbumPhoto>(){ 
                        new AlbumPhoto{Id=1},
                        new AlbumPhoto{Id=2}
                    } , PhotoRatings = new List<PhotoRating>(), PhotoTags= new List<PhotoTag>()
                },
                new Photo{Id = 2, Title = "title2", Description = "desc2", UserId =2, PhotoUrl="url2"
                , UploadDate = new DateTime(2020,2,2), TotalRating = 3d, PhotoRatings = new List<PhotoRating>(), PhotoTags= new List < PhotoTag >(), AlbumPhotos = new List < AlbumPhoto >()}
           };

        #endregion
    }
}
