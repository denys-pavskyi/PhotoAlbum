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
    public class PhotoRatingServiceTests
    {
        [Test]
        public async Task PhotoRatingService_GetAll_ReturnsAllPhotoRatings()
        {
            //arrange
            var expected = GetTestPhotoRatingModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.PhotoRatingRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestPhotoRatingEntities.AsEnumerable());

            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoRatingService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PhotoRatingService_GetById_ReturnsPhotoRatingModel()
        {
            //arrange
            var expected = GetTestPhotoRatingModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.PhotoRatingRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestPhotoRatingEntities.First());

            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await photoRatingService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task PhotoRatingService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRatingRepository.AddAsync(It.IsAny<PhotoRating>()));

            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoRating = GetTestPhotoRatingModels.First();

            //act
            await photoRatingService.AddAsync(photoRating);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRatingRepository.AddAsync(It.Is<PhotoRating>(x =>
                            x.Id == photoRating.Id &&
                            x.Grade == photoRating.Grade &&
                            x.PhotoId == photoRating.PhotoId &&
                            x.RatingDate == photoRating.RatingDate
                            )), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task PhotoRatingService_AddAsync_ThrowsInternetPhotoPhotoRatingExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRatingRepository.AddAsync(It.IsAny<PhotoRating>()));

            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoRating = GetTestPhotoRatingModels.First();
            photoRating.UserId = -1;

            //act
            Func<Task> act = async () => await photoRatingService.AddAsync(photoRating);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [Test]
        public async Task PhotoRatingService_AddAsync_ThrowsInternetPhotoPhotoRatingExceptionWithWrongUserId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRatingRepository.AddAsync(It.IsAny<PhotoRating>()));

            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoRating = GetTestPhotoRatingModels.First();
            photoRating.UserId = -1;

            //act
            Func<Task> act = async () => await photoRatingService.AddAsync(photoRating);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }




        [TestCase(1)]
        [TestCase(2)]
        public async Task PhotoRatingService_DeleteAsync_DeletesPhotoRating(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRatingRepository.DeleteByIdAsync(It.IsAny<int>()));
            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await photoRatingService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRatingRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task PhotoRatingService_UpdateAsync_UpdatesPhotoRating()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PhotoRatingRepository.Update(It.IsAny<PhotoRating>()));


            var photoRatingService = new PhotoRatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var photoRating = GetTestPhotoRatingModels.First();

            //act
            await photoRatingService.UpdateAsync(photoRating);

            //assert
            mockUnitOfWork.Verify(x => x.PhotoRatingRepository.Update(It.Is<PhotoRating>(x =>
                            x.Id == photoRating.Id &&
                            x.Grade == photoRating.Grade &&
                            x.PhotoId == photoRating.PhotoId &&
                            x.RatingDate == photoRating.RatingDate

                            )), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }






        #region Data

        public List<PhotoRatingModel> GetTestPhotoRatingModels =>
            new List<PhotoRatingModel>()
            {
                new PhotoRatingModel{Id = 1, Grade = 5d, UserId = 1, PhotoId = 1, RatingDate = new DateTime(2020,1,1)},
                new PhotoRatingModel{Id = 2, Grade = 3d, UserId = 1, PhotoId = 2, RatingDate = new DateTime(2020,1,1)}
            };

        public List<PhotoRating> GetTestPhotoRatingEntities =>
           new List<PhotoRating>()
           {
                new PhotoRating{Id = 1, Grade = 5d, UserId = 1, PhotoId = 1, RatingDate = new DateTime(2020,1,1)},
                new PhotoRating{Id = 2, Grade = 3d, UserId = 1, PhotoId = 2, RatingDate = new DateTime(2020,1,1)}
           };

        #endregion

    }
}
