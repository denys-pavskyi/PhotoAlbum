using BuisnessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation
{
    public static class ModelsValidation
    {

        public static void AlbumPhotoModelValidation(AlbumPhotoModel model)
        {

            if(model == null)
            {
                throw new InternetPhotoAlbumException("AlbumPhoto was null");
            }
            
            if(model.PhotoId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong photoId");
            }
            if (model.AlbumId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong albumId");
            }
        }



        public static void UserModelValidation(UserModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("User was null");
            }
            
            if(model.FirstName == null || model.FirstName == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong user's firstname");
            }
            if (model.UserName == null || model.UserName == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong user's username");
            }
            if (model.EmailAddress == null || model.EmailAddress == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong user's emailAddress");
            }
            if (model.Password == null || model.Password == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong user's password");
            }
            if (model.BirthDate > DateTime.Now || model.BirthDate < DateTime.Now.AddYears(-120))
            {
                throw new InternetPhotoAlbumException("Wrong user's birth date");
            }          
            
        }


        public static void AlbumModelValidation(AlbumModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("Album was null");
            }
            
            if (model.Title == null || model.Title == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong album title");
            }

            if(model.Description != null && model.Description == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong album description");
            }

            if(model.NumberOfPictures!=null && model.NumberOfPictures < 0)
            {
                throw new InternetPhotoAlbumException("Wrong album's number of pictures");
            }
            if (model.UserId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong userId");
            }
        }

        public static void PhotoModelValidation(PhotoModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("Photo was null");
            }
            
            if(model.PhotoPath == null || model.PhotoPath == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong Photo Url");
            }
            if (model.Title == null || model.Title == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong Photo title");
            }

            if (model.Description != null && model.Description == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong photo description");
            }
            if (model.UserId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong userId");
            }
            if(model.TotalRating != null && model.TotalRating < 0)
            {
                throw new InternetPhotoAlbumException("Wrong album's total rating ");
            }
        }


        public static void PhotoRatingModelValidation(PhotoRatingModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("PhotoRating was null");
            }
            
            if (model.UserId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong userId");
            }
            if (model.Grade < 0)
            {
                throw new InternetPhotoAlbumException("Wrong grade");
            }
        }

        public static void PhotoTagModelValidation(PhotoTagModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("PhotoTag was null");
            }
            
            if (model.PhotoId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong photoId");
            }
            if (model.TagId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong tagId");
            }
        }

        public static void TagModelValidation(TagModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("Tag was null");
            }
            
            if (model.Title == null || model.Title == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong Tag title");
            }
        }

        public static void ReportModelValidation(ReportModel model)
        {
            if (model == null)
            {
                throw new InternetPhotoAlbumException("Report was null");
            }

            if (model.Comment != null && model.Comment == String.Empty)
            {
                throw new InternetPhotoAlbumException("Wrong report comment");
            }

            if (model.UserId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong userId");
            }

            if (model.PhotoId < 0)
            {
                throw new InternetPhotoAlbumException("Wrong photoId");
            }

        }

       
    }
}
