using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPhotoRatingRepository: IRepository<PhotoRating>
    {
        Task<IEnumerable<PhotoRating>> GetAllWithDetailsAsync();
        Task<PhotoRating> GetByIdWithDetailsAsync(int id);
    }
}
