using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PhotoRepository:IPhotoRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public PhotoRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }
    }
}
