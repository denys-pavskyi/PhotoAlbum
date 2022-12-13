using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests
{
    internal class AlbumPhotoEqualityComparer : IEqualityComparer<AlbumPhoto>
    {
        public bool Equals(AlbumPhoto? x, AlbumPhoto? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.AlbumId == y.AlbumId &&
                x.PhotoId == y.PhotoId &&
                x.AdditionDate == y.AdditionDate;
        }

        public int GetHashCode([DisallowNull] AlbumPhoto obj)
        {
            return obj.GetHashCode();
        }
    }


    internal class AlbumEqualityComparer : IEqualityComparer<Album>
    {
        public bool Equals(Album? x, Album? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Title == y.Title &&
                x.Description == y.Description &&
                x.NumberOfPictures == y.NumberOfPictures &&
                x.UserId == y.UserId &&
                x.CreationDate == y.CreationDate;
        }

        public int GetHashCode([DisallowNull] Album obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class PhotoRatingEqualityComparer : IEqualityComparer<PhotoRating>
    {
        public bool Equals(PhotoRating? x, PhotoRating? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;


            return x.Id == y.Id &&
                x.PhotoId == y.PhotoId &&
                x.UserId == y.UserId &&
                x.Grade == y.Grade &&
                x.RatingDate == y.RatingDate;
        }

        public int GetHashCode([DisallowNull] PhotoRating obj)
        {
            return obj.GetHashCode();
        }

    }



    internal class PhotoEqualityComparer : IEqualityComparer<Photo>
    {
        public bool Equals(Photo? x, Photo? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.PhotoPath == y.PhotoPath &&
                x.Description == y.Description &&
                x.Title == y.Title &&
                x.UploadDate == y.UploadDate &&
                x.TotalRating == y.TotalRating;
        }

        public int GetHashCode([DisallowNull] Photo obj)
        {
            return obj.GetHashCode();
        }
    }


    internal class PhotoTagEqualityComparer : IEqualityComparer<PhotoTag>
    {
        public bool Equals(PhotoTag? x, PhotoTag? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.PhotoId == y.PhotoId &&
                x.TagId == y.TagId;
        }

        public int GetHashCode([DisallowNull] PhotoTag obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class TagEqualityComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag? x, Tag? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Title == y.Title;

        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.UserName == y.UserName &&
                x.Password == y.Password &&
                x.EmailAddress == y.EmailAddress &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.BirthDate == y.BirthDate &&
                x.RegistrationDate == y.RegistrationDate &&
                x.Role == y.Role;

        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ReportEqualityComparer : IEqualityComparer<Report>
    {
        public bool Equals(Report? x, Report? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Status == y.Status &&
                x.Comment == y.Comment &&
                x.UserId == y.UserId &&
                x.PhotoId == y.PhotoId;

        }

        

        public int GetHashCode([DisallowNull] Report obj)
        {
            return obj.GetHashCode();
        }
    }
}
