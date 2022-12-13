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
    public class ReportServiceTests
    {
        [Test]
        public async Task ReportService_GetAll_ReturnsAllReports()
        {
            //arrange
            var expected = GetTestReportModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.ReportRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(GetTestReportEntities.AsEnumerable());

            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await reportService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ReportService_GetById_ReturnsReportModel()
        {
            //arrange
            var expected = GetTestReportModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.ReportRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestReportEntities.First());

            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await reportService.GetByIdAsync(1);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }




        [Test]
        public async Task ReportService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ReportRepository.AddAsync(It.IsAny<Report>()));

            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var report = GetTestReportModels.First();

            //act
            await reportService.AddAsync(report);

            //assert
            mockUnitOfWork.Verify(x => x.ReportRepository.AddAsync(It.Is<Report>(x =>
                            x.Id == report.Id &&
                            x.PhotoId == report.PhotoId &&
                            x.UserId == report.UserId &&
                            x.Comment == report.Comment)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ReportService_AddAsync_ThrowsInternetPhotoAlbumExceptionWithWrongId()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ReportRepository.AddAsync(It.IsAny<Report>()));

            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var report = GetTestReportModels.First();
            report.PhotoId = -1;

            //act
            Func<Task> act = async () => await reportService.AddAsync(report);

            //assert
            await act.Should().ThrowAsync<InternetPhotoAlbumException>();
        }




        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ReportService_DeleteAsync_DeletesReport(int id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ReportRepository.DeleteByIdAsync(It.IsAny<int>()));
            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await reportService.DeleteAsync(id);

            //assert
            mockUnitOfWork.Verify(x => x.ReportRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }



        [Test]
        public async Task ReportService_UpdateAsync_UpdatesReport()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ReportRepository.Update(It.IsAny<Report>()));


            var reportService = new ReportService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var report = GetTestReportModels.First();

            //act
            await reportService.UpdateAsync(report);

            //assert
            mockUnitOfWork.Verify(x => x.ReportRepository.Update(It.Is<Report>(x =>
                             x.Id == report.Id &&
                            x.PhotoId == report.PhotoId &&
                            x.UserId == report.UserId &&
                            x.Comment == report.Comment)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }


        #region Data

        public List<ReportModel> GetTestReportModels =>
            new List<ReportModel>()
            {
                new ReportModel{Id = 1, PhotoId = 1, UserId = 1, Status = ReportStatus.Approved, Comment = "cmnt1"},
                new ReportModel{Id = 2, PhotoId = 2, UserId = 2, Status = ReportStatus.Declined, Comment = "cmnt2"}
            };

        public List<Report> GetTestReportEntities =>
           new List<Report>()
           {
                new Report{Id = 1, PhotoId = 1, UserId = 1, Status = ReportStatus.Approved, Comment = "cmnt1"},
                new Report{Id = 2, PhotoId = 2, UserId = 2, Status = ReportStatus.Declined, Comment = "cmnt2"}
           };

        #endregion

    }
}
