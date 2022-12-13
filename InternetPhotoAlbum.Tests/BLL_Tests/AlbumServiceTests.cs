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
    public class AlbumServiceTests
    {




        [Test]
        public async Task AlbumService_GetAll_ReturnsAllAlbums()
        {
            //arrange
            var expected = GetTestAlbumModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.AlbumRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestAlbumEntities.AsEnumerable());

            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await albumService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AlbumService_GetById_ReturnsAlbumModel()
        {
            //arrange
            var expected = GetTestAlbumModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.AlbumRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestAlbumEntities.First());

            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await albumService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task AlbumService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumRepository.AddAsync(It.IsAny<Album>()));

            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = GetTestAlbumModels.First();

            //act
            await albumService.AddAsync(album);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumRepository.AddAsync(It.Is<Album>(x =>
                            x.Id == album.Id &&
                            x.UserId == album.UserId &&
                            x.CreationDate == album.CreationDate &&
                            x.Description == album.Description
                            )), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task AlbumService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumRepository.AddAsync(It.IsAny<Album>()));

            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = GetTestAlbumModels.First();
            album.UserId = -1;

            //act
            Func<Task> act = async () => await albumService.AddAsync(album);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [Test]
        public async Task AlbumService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithEmptyTitle()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumRepository.AddAsync(It.IsAny<Album>()));

            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = GetTestAlbumModels.First();
            album.Title = string.Empty;

            //act
            Func<Task> act = async () => await albumService.AddAsync(album);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }


       

        [TestCase(1)]
        [TestCase(2)]
        public async Task AlbumService_DeleteAsync_DeletesAlbum(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumRepository.DeleteByIdAsync(It.IsAny<int>()));
            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await albumService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task AlbumService_UpdateAsync_UpdatesAlbum()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AlbumRepository.Update(It.IsAny<Album>()));


            var albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = GetTestAlbumModels.First();

            //act
            await albumService.UpdateAsync(album);

            //assert
            mockUnitOfWork.Verify(x => x.AlbumRepository.Update(It.Is<Album>(x =>
                            x.Id == album.Id &&
                            x.UserId == album.UserId &&
                            x.CreationDate == album.CreationDate &&
                            x.Description == album.Description
                            )), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }


        



        #region Data

        public List<AlbumModel> GetTestAlbumModels =>
            new List<AlbumModel>()
            {
                new AlbumModel{ Id = 1, UserId = 1, CreationDate = new DateTime(2020,1,1), Description = "desc1", Title = "title1",
                    NumberOfPictures = 2, AlbumPhotoIds = new[]{1, 2 } },
                new AlbumModel{ Id = 2, UserId = 1, CreationDate = new DateTime(2020,2,2), Description = "desc2", Title = "title2",
                    NumberOfPictures = 1, AlbumPhotoIds = new[]{1 } }
            };

        public List<Album> GetTestAlbumEntities =>
           new List<Album>()
           {
                new Album{ Id = 1,  CreationDate = new DateTime(2020,1,1), Title = "title1", UserId =1, NumberOfPictures = 2,
                 Description = "desc1", AlbumPhotos = new List<AlbumPhoto>(){
                        new AlbumPhoto{ Id = 1, AlbumId =1, PhotoId =1},
                        new AlbumPhoto{ Id = 2, AlbumId =2, PhotoId =2}
                    }
                },
                new Album{ Id = 2,  CreationDate = new DateTime(2020,2,2), Title = "title2", UserId =1, NumberOfPictures = 1,
                 Description = "desc2", AlbumPhotos = new List<AlbumPhoto>(){
                        new AlbumPhoto{ Id = 1, AlbumId =1, PhotoId =1},
                        
                    }
                }
           };

        #endregion

    }
}
