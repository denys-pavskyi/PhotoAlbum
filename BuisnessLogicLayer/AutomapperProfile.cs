using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BuisnessLogicLayer
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Album, AlbumModel>()
                .ForMember(am => am.AlbumPhotoIds, a => a.MapFrom(x => x.AlbumPhotos.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<AlbumPhoto, AlbumPhotoModel>()
                .ReverseMap();

            CreateMap<Photo, PhotoModel>()
                .ForMember(pm => pm.PhotoRatingIds, p => p.MapFrom(x => x.PhotoRatings.Select(x => x.Id)))
                .ForMember(pm => pm.PhotoTagsIds, p => p.MapFrom(x => x.PhotoTags.Select(x => x.Id)))
                .ForMember(pm => pm.AlbumPhotoIds, p => p.MapFrom(x => x.AlbumPhotos.Select(x => x.Id)))
                .ForMember(pm => pm.UserName, p => p.MapFrom(x => x.User.UserName))
                .ReverseMap();

            CreateMap<PhotoRating, PhotoRatingModel>()
                .ReverseMap();

            CreateMap<PhotoTag, PhotoTagModel>()
                .ReverseMap();

            CreateMap<Tag, TagModel>()
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(um => um.PhotoRatingIds, u => u.MapFrom(x => x.PhotoRatings.Select(x => x.Id)))
                .ForMember(um => um.PhotoIds, u => u.MapFrom(x => x.Photos.Select(x => x.Id)))
                .ForMember(um => um.AlbumIds, u => u.MapFrom(x => x.Albums.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<Report, ReportModel>()
                .ReverseMap();


        }
    }
}
