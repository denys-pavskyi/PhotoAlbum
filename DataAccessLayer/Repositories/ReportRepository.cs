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
    public class ReportRepository : IReportRepository
    {
        private readonly InternetPhotoAlbumDbContext _context;

        public ReportRepository(InternetPhotoAlbumDbContext internetPhotoAlbumDbContext)
        {
            _context = internetPhotoAlbumDbContext;
        }

        public async Task AddAsync(Report entity)
        {
            await _context.Reports.AddAsync(entity);
        }

        public void Delete(Report entity)
        {
            _context.Reports.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(Report);
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync(); 
        }



        public async Task<Report> GetByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public void Update(Report entity)
        {
            _context.Reports.Update(entity);

        }

        public async Task<Report> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Reports
                .Include(x => x.Photo)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Report>> GetAllWithDetailsAsync()
        {
            return await _context.Reports
                .Include(x => x.Photo)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetAllWithNoTrackingAsync()
        {
            return await _context.Reports.AsNoTracking().ToListAsync();
        }
    }
}
