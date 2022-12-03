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
    public class PhotoTagRepository: IPhotoTagRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public PhotoTagRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }

        public async Task AddAsync(PhotoTag entity)
        {
            await _context.PhotoTags.AddAsync(entity);
        }

        public void Delete(PhotoTag entity)
        {
            _context.PhotoTags.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var PhotoTag = await GetByIdWithDetailsAsync(id);
            _context.PhotoTags.Remove(PhotoTag);
        }

        public async Task<IEnumerable<PhotoTag>> GetAllAsync()
        {
            return await _context.PhotoTags.ToListAsync();
        }



        public async Task<PhotoTag> GetByIdAsync(int id)
        {
            return await _context.PhotoTags.FindAsync(id);
        }

        public void Update(PhotoTag entity)
        {
            _context.PhotoTags.Update(entity);

        }

        public async Task<PhotoTag> GetByIdWithDetailsAsync(int id)
        {
            return await _context.PhotoTags
                .Include(x => x.Photo)
                .Include(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<PhotoTag>> GetAllWithDetailsAsync()
        {
            return await _context.PhotoTags
                .Include(x => x.Photo)
                .Include(x => x.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoTag>> GetAllWithNoTrackingAsync()
        {
            return await _context.PhotoTags.AsNoTracking().ToListAsync();
        }
    }
}
