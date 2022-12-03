using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PhotoTagRepository: IPhotoTagRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public PhotoTagRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }
    }
}
