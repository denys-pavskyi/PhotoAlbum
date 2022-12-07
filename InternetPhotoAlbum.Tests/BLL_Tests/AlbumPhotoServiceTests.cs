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
        public async Task AlbumPhotoService_GetById_ReturnsCustomerModel()
        {
            //arrange
            var expected = GetTestCustomerModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.CustomerRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestCustomerEntities.First());

            var customerService = new CustomerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await customerService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }


        #region Data

        public List<AlbumPhotoModel> GetTestAlbumPhotoModels =>
            new List<AlbumPhotoModel>()
            {
                new AlbumPhotoModel{ Id = 1, AlbumId= 1, PhotoId = 1, AdditionDate = new DateTime(2021,1,1) },
                new AlbumPhotoModel{ Id = 2, AlbumId= 2, PhotoId = 2, AdditionDate = new DateTime(2021,2,2) },
                new AlbumPhotoModel{ Id = 3, AlbumId= 2, PhotoId = 3, AdditionDate = new DateTime(2021,3,3) }
            };

        public List<AlbumPhoto> GetTestAlbumPhotoEntities =>
           new List<AlbumPhoto>()
           {
                new AlbumPhoto{ Id = 1, AlbumId= 1, PhotoId = 1, AdditionDate = new DateTime(2021,1,1) },
                new AlbumPhoto{ Id = 2, AlbumId= 2, PhotoId = 2, AdditionDate = new DateTime(2021,2,2) },
                new AlbumPhoto{ Id = 3, AlbumId= 2, PhotoId = 3, AdditionDate = new DateTime(2021,3,3) }
           };


        /*public List<AlbumPhotoModel> GetTestPhotoModels =>
            new List<PhotoModel>()
            {
                new PhotoModel{ Id = 1, AlbumId= 1, PhotoId = 1, AdditionDate = new DateTime(2021,1,1) },

            };

        public List<AlbumModel> GetTestAlbumModels =>
            new List<AlbumModel>()
            {
                new AlbumModel{ Id = 1, CreationDate = new DateTime(2020,2,2), Description="desc1", Title = "title1",
                 UserId},

            };*/


        #endregion

    }
}
