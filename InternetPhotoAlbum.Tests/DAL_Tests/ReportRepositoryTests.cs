using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests.DAL_Tests
{
    public class ReportRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task ReportRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);

            var report = await reportRepository.GetByIdAsync(id);

            var expected = ExpectedReports.FirstOrDefault(x => x.Id == id);

            Assert.That(report, Is.EqualTo(expected).Using(new ReportEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task ReportRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);

            var reports = await reportRepository.GetAllAsync();

            Assert.That(reports, Is.EqualTo(ExpectedReports).Using(new ReportEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task ReportRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);
            var numberOfReports = context.Reports.Count();
            var report = new Report { Id = 3, Comment = "cmnt1", UserId= 1, PhotoId = 1 };

            await reportRepository.AddAsync(report);
            await context.SaveChangesAsync();

            Assert.That(context.Reports.Count(), Is.EqualTo(numberOfReports + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ReportRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);
            var ReportsNumber = context.Reports.Count();
            await reportRepository.DeleteByIdAsync(2);
            await context.SaveChangesAsync();

            Assert.That(context.Reports.Count(), Is.EqualTo(ReportsNumber - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task ReportRepository_Update_UpdatesEntity()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);
            var report = new Report
            {
                Id = 1,
                Comment = "cmnt1",
                UserId = 1,
                PhotoId = 1
            };


            reportRepository.Update(report);
            await context.SaveChangesAsync();

            Assert.That(report, Is.EqualTo(new Report
            {
                Id = 1,
                Comment = "cmnt1",
                UserId = 1,
                PhotoId = 1
            }).Using(new ReportEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task ReportRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);

            var report = await reportRepository.GetByIdWithDetailsAsync(1);

            var expectedPhoto = ExpectedPhotos.FirstOrDefault(x => x.Id == report.PhotoId);
            var expectedUser = ExpectedUsers.FirstOrDefault(x => x.Id == report.UserId);
            var expected = ExpectedReports.FirstOrDefault(x => x.Id == 1);

            Assert.That(report,
                Is.EqualTo(expected).Using(new ReportEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");


            Assert.That(report.Photo,
                Is.EqualTo(expectedPhoto).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");
            Assert.That(report.User,
                Is.EqualTo(expectedUser).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");



        }

        [Test]
        public async Task ReportRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new InternetPhotoAlbumDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var reportRepository = new ReportRepository(context);

            var reports = await reportRepository.GetAllWithDetailsAsync();

            Assert.That(reports,
                Is.EqualTo(ExpectedReports).Using(new ReportEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");


            Assert.That(reports.Select(i => i.Photo).Distinct().OrderBy(i => i.Id),
               Is.EqualTo(ExpectedPhotos).Using(new PhotoEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            Assert.That(reports.Select(i => i.User).Distinct().OrderBy(i => i.Id),
              Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }


        public static IEnumerable<User> ExpectedUsers =>
           new[]
           {
               new User { Id = 1,UserName = "Username1",  FirstName="FirstName1", LastName = "LastName1", Password="P@ssw0rd1" , RegistrationDate = new DateTime(2021,5,3), EmailAddress="user1@gmail.com", BirthDate = new DateTime(1991, 3, 5), Role = Role.User },
               new User { Id = 2, UserName = "Username2", FirstName = "FirstName2", LastName = "LastName2", Password = "P@ssw0rd2" , RegistrationDate = new DateTime(2022, 10,13), EmailAddress = "user2@gmail.com", BirthDate = new DateTime(1996, 10,17), Role = Role.Admin }
           };


        public static IEnumerable<Report> ExpectedReports =>
           new[]
           {
              new Report { Id = 1, Comment = "comment1", PhotoId = 1, Status = ReportStatus.OnReview, UserId = 1},
              new Report { Id = 2, Comment = "comment2", PhotoId = 2, Status = ReportStatus.Declined, UserId = 2 }
           };

        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoPath="PhotoPath1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoPath="PhotoPath2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=2}
            };


    }
}
