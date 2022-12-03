using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AlbumPhotoRepository: IAlbumPhotoRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public AlbumPhotoRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }
    }
}
