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
    public class AlbumPhotoRepository: IAlbumPhotoRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public AlbumPhotoRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }

        public async Task AddAsync(AlbumPhoto entity)
        {
            await _context.AlbumPhotos.AddAsync(entity);
        }

        public void Delete(AlbumPhoto entity)
        {
            _context.AlbumPhotos.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var AlbumPhoto = await GetByIdWithDetailsAsync(id);
            _context.AlbumPhotos.Remove(AlbumPhoto);
        }

        public async Task<IEnumerable<AlbumPhoto>> GetAllAsync()
        {
            return await _context.AlbumPhotos.ToListAsync();
        }



        public async Task<AlbumPhoto> GetByIdAsync(int id)
        {
            return await _context.AlbumPhotos.FindAsync(id);
        }

        public void Update(AlbumPhoto entity)
        {
            _context.AlbumPhotos.Update(entity);

        }

        public async Task<AlbumPhoto> GetByIdWithDetailsAsync(int id)
        {
            return await _context.AlbumPhotos
                .Include(x=>x.Album)
                    .ThenInclude(x=>x.User)
                .Include(x=>x.Photo)
                .FirstOrDefaultAsync(x=>x.Id==id);

        }

        public async Task<IEnumerable<AlbumPhoto>> GetAllWithDetailsAsync()
        {
            return await _context.AlbumPhotos
                .Include(x => x.Album)
                    .ThenInclude(x => x.User)
                .Include(x => x.Photo)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlbumPhoto>> GetAllWithNoTrackingAsync()
        {
            return await _context.AlbumPhotos.AsNoTracking().ToListAsync();
        }
    }
}
