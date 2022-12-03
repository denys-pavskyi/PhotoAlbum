using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests.DAL_Tests
{
    public class AlbumPhotoRepositoryTests
    {
        [TestClass]
        public class UnitTest1
        {
            [TestMethod]
            public void TestMethod1()
            {
            }
        }





        public static IEnumerable<AlbumPhoto> ExpectedAlbumPhotos =>
            new[]
            {
                new AlbumPhoto{Id = 1, PhotoId = 1, AlbumId = 1, AdditionDate = new DateTime(2022,12,3)},
                new AlbumPhoto{Id = 2, PhotoId = 2, AlbumId = 1, AdditionDate = new DateTime(2022,12,3)},
            };

        public static IEnumerable<Album> ExpectedAlbums =>
            new[]
            {
                new Album{Id=1, Title="Album1", NumberOfPictures = 2, CreationDate = new DateTime(2022,12,2), UserId=1},
                new Album{Id=2, Title="Album2", NumberOfPictures = 1, CreationDate = new DateTime(2022,12,3), UserId=2}
            };

        public static IEnumerable<Photo> ExpectedPhotos =>
            new[]
            {
                new Photo{Id=1, Title="Photo1", PhotoUrl="PhotoUrl1", TotalRating=3.4d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo2", PhotoUrl="PhotoUrl2", TotalRating=2.5d, UploadDate = new DateTime(2022,11,29), UserId=1},
                new Photo{Id=2, Title="Photo3", PhotoUrl="PhotoUrl3", TotalRating=1.1d, UploadDate = new DateTime(2022,11,27), UserId=2}
            };

        //public static IEnumerable<User>

    }
}
