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
    public class UserServiceTests
    {
        [Test]
        public async Task UserService_GetAll_ReturnsAllUsers()
        {
            //arrange
            var expected = GetTestUserModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.UserRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestUserEntities.AsEnumerable());

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await userService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_GetById_ReturnsUserModel()
        {
            //arrange
            var expected = GetTestUserModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.UserRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestUserEntities.First());

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await userService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task UserService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var user = GetTestUserModels.First();

            //act
            await userService.AddAsync(user);

            //assert
            mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.Is<User>(x =>
                            x.Id == user.Id &&
                            x.FirstName == user.FirstName &&
                            x.LastName == user.LastName &&
                            x.RegistrationDate == user.RegistrationDate &&
                            x.BirthDate == user.BirthDate &&
                            x.UserName == user.UserName)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithEmptyUsername()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var user = GetTestUserModels.First();
            user.UserName = string.Empty;

            //act
            Func<Task> act = async () => await userService.AddAsync(user);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [Test]
        public async Task UserService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithEmptyPassword()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var user = GetTestUserModels.First();
            user.Password = string.Empty;

            //act
            Func<Task> act = async () => await userService.AddAsync(user);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }


        [Test]
        public async Task UserService_AddAsync_ThrowsMarketExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await userService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UserService_DeleteAsync_DeletesUser(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.DeleteByIdAsync(It.IsAny<int>()));
            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await userService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.UserRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task UserService_UpdateAsync_UpdatesUser()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));


            var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var user = GetTestUserModels.First();

            //act
            await userService.UpdateAsync(user);

            //assert
            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.Is<User>(x =>
                            x.Id == user.Id &&
                            x.FirstName == user.FirstName &&
                            x.LastName == user.LastName &&
                            x.RegistrationDate == user.RegistrationDate &&
                            x.BirthDate == user.BirthDate &&
                            x.UserName == user.UserName)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }







        #region Data

        public List<UserModel> GetTestUserModels =>
            new List<UserModel>()
            {
                new UserModel{Id= 1, FirstName = "f1", LastName = "f2", BirthDate  = new DateTime(1990,1,1)
                , RegistrationDate = new DateTime(2020,1,1), EmailAddress = "user1@gmail.com", Password = "pass1",PasswordSalt = "",
                 Role = Role.User, UserName = "username1", PhotoIds = new[]{1,2}, AlbumIds = new List<int>(), PhotoRatingIds = new List<int>() },

                new UserModel{Id= 2, FirstName = "f2", LastName = "f2", BirthDate  = new DateTime(1990,2,2)
                , RegistrationDate = new DateTime(2020,2,2), EmailAddress = "user2@gmail.com", Password = "pass2",PasswordSalt = "",
                 Role = Role.Admin, UserName = "username2", PhotoIds = new List<int>(), AlbumIds = new List<int>(), PhotoRatingIds = new List<int>() }



            };

        public List<User> GetTestUserEntities =>
           new List<User>()
           {
               new User{Id= 1, FirstName = "f1", LastName = "f2", BirthDate  = new DateTime(1990,1,1)
                , RegistrationDate = new DateTime(2020,1,1), EmailAddress = "user1@gmail.com", Password = "pass1", PasswordSalt = "",
                 Role = Role.User, UserName = "username1", Albums = new List<Album>(), PhotoRatings = new List<PhotoRating>(),
                    Photos = new List<Photo>(){
                        new Photo{Id = 1},
                        new Photo{Id = 2}
                    }
               },

                new User{Id= 2, FirstName = "f2", LastName = "f2", BirthDate  = new DateTime(1990,2,2)
                , RegistrationDate = new DateTime(2020,2,2), EmailAddress = "user2@gmail.com", Password = "pass2",PasswordSalt = "",
                 Role = Role.Admin, UserName = "username2", Albums = new List<Album>(), PhotoRatings = new List<PhotoRating>(), Photos = new List<Photo>() }


           };

        #endregion
    }
}
