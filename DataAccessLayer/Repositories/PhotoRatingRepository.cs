using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PhotoRatingRepository: IPhotoRatingRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public PhotoRatingRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }
    }
}
