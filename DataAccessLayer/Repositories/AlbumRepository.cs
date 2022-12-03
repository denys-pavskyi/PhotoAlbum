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
    public class AlbumRepository: IAlbumRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public AlbumRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }

        public async Task AddAsync(Album entity)
        {
            await _context.Albums.AddAsync(entity);
        }

        public void Delete(Album entity)
        {
            _context.Albums.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Album = await GetByIdWithDetailsAsync(id);
            _context.Albums.Remove(Album);
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            return await _context.Albums.ToListAsync();
        }



        public async Task<Album> GetByIdAsync(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public void Update(Album entity)
        {
            _context.Albums.Update(entity);

        }

        public async Task<Album> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Albums
                .Include(x => x.User)
                .Include(x => x.AlbumPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Album>> GetAllWithDetailsAsync()
        {
            return await _context.Albums
                .Include(x => x.User)
                .Include(x => x.AlbumPhotos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAllWithNoTrackingAsync()
        {
            return await _context.Albums.AsNoTracking().ToListAsync();
        }
    }
}
