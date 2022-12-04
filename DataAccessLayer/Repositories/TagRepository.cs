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
    public class TagRepository: ITagRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public TagRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }


       


        public async Task AddAsync(Tag entity)
        {
            await _context.Tags.AddAsync(entity);
        }

        public void Delete(Tag entity)
        {
            _context.Tags.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(Tag);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }



        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public void Update(Tag entity)
        {
            _context.Tags.Update(entity);

        }

        public async Task<Tag> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Tags
                .Include(x => x.PhotoTags)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Tag>> GetAllWithDetailsAsync()
        {
            return await _context.Tags
                .Include(x => x.PhotoTags)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllWithNoTrackingAsync()
        {
            return await _context.Tags.AsNoTracking().ToListAsync();
        }
    }
}
