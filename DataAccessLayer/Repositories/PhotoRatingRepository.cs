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
    public class PhotoRatingRepository: IPhotoRatingRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public PhotoRatingRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }


        public async Task AddAsync(PhotoRating entity)
        {
            await _context.PhotosRating.AddAsync(entity);
        }

        public void Delete(PhotoRating entity)
        {
            _context.PhotosRating.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var photoRating = await _context.PhotosRating.FindAsync(id);
            _context.PhotosRating.Remove(photoRating);
        }

        public async Task<IEnumerable<PhotoRating>> GetAllAsync()
        {
            return await _context.PhotosRating.ToListAsync();
        }



        public async Task<PhotoRating> GetByIdAsync(int id)
        {
            return await _context.PhotosRating.FindAsync(id);
        }

        public void Update(PhotoRating entity)
        {
            _context.PhotosRating.Update(entity);

        }

        public async Task<PhotoRating> GetByIdWithDetailsAsync(int id)
        {
            return await _context.PhotosRating
                .Include(x => x.User)
                .Include(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<PhotoRating>> GetAllWithDetailsAsync()
        {
            return await _context.PhotosRating
                .Include(x => x.User)
                .Include(x => x.Photo)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoRating>> GetAllWithNoTrackingAsync()
        {
            return await _context.PhotosRating.AsNoTracking().ToListAsync();
        }
    }
}
