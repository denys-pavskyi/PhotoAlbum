using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPhotoTagRepository : IRepository<PhotoTag>
    {
        Task<IEnumerable<PhotoTag>> GetAllWithDetailsAsync();
        Task<PhotoTag> GetByIdWithDetailsAsync(int id);
    }
}
