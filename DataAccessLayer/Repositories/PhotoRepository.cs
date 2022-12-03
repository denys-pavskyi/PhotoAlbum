using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
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




        public async Task AddAsync(Photo entity)
        {
            await _context.Photos.AddAsync(entity);
        }

        public void Delete(Photo entity)
        {
            _context.Photos.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var photo = await GetByIdWithDetailsAsync(id);
            _context.Photos.Remove(photo);
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _context.Photos.ToListAsync();
        }



        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public void Update(Photo entity)
        {
            _context.Photos.Update(entity);

        }

        public async Task<Photo> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Photos
                .Include(x => x.PhotoRatings)
                .Include(x => x.PhotoTags)
                .Include(x => x.AlbumPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Photo>> GetAllWithDetailsAsync()
        {
            return await _context.Photos
                .Include(x => x.PhotoRatings)
                .Include(x => x.PhotoTags)
                .Include(x => x.AlbumPhotos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Photo>> GetAllWithNoTrackingAsync()
        {
            return await _context.Photos.AsNoTracking().ToListAsync();
        }
    }
}
