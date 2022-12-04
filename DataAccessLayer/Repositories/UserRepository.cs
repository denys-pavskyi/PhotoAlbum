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
    public class UserRepository: IUserRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public UserRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var User = await _context.Users.FindAsync(id);
            _context.Users.Remove(User);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }



        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);

        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Users
                .Include(x => x.PhotoRatings)
                .Include(x => x.Photos)
                .Include(x => x.Albums)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await _context.Users
                .Include(x => x.PhotoRatings)
                .Include(x => x.Photos)
                .Include(x => x.Albums)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithNoTrackingAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
