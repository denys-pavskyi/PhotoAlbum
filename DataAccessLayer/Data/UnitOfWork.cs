using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly InternetPhotoAlbumDbContext dbContext;
        private AlbumPhotoRepository albumPhotoRepository;
        private AlbumRepository albumRepository;
        private PhotoRepository photoRepository;
        private PhotoRatingRepository photoRatingRepository;
        private PhotoTagRepository photoTagRepository;
        private TagRepository tagRepository;
        private UserRepository userRepository;
        private ReportRepository reportRepository;


        public UnitOfWork(InternetPhotoAlbumDbContext context)
        {
            dbContext = context;
        }

        public IAlbumPhotoRepository AlbumPhotoRepository
        {
            get
            {
                if (albumPhotoRepository == null)
                {
                    albumPhotoRepository = new AlbumPhotoRepository(dbContext);
                }
                return albumPhotoRepository;
            }
        }

        public IAlbumRepository AlbumRepository
        {
            get
            {
                if (albumRepository == null)
                {
                    albumRepository = new AlbumRepository(dbContext);
                }
                return albumRepository;
            }
        }

        public IPhotoRatingRepository PhotoRatingRepository
        {
            get
            {
                if (photoRatingRepository == null)
                {
                    photoRatingRepository = new PhotoRatingRepository(dbContext);
                }
                return photoRatingRepository;
            }
        }

        public IPhotoTagRepository PhotoTagRepository
        {
            get
            {
                if (photoTagRepository == null)
                {
                    photoTagRepository = new PhotoTagRepository(dbContext);
                }
                return photoTagRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                if (tagRepository == null)
                {
                    tagRepository = new TagRepository(dbContext);
                }
                return tagRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(dbContext);
                }
                return userRepository;
            }
        }

        public IPhotoRepository PhotoRepository
        {
            get
            {
                if (photoRepository == null)
                {
                    photoRepository = new PhotoRepository(dbContext);
                }
                return photoRepository;
            }
        }

        public IReportRepository ReportRepository
        {
            get
            {
                if (reportRepository == null)
                {
                    reportRepository = new ReportRepository(dbContext);
                }
                return reportRepository;
            }
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
