using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReportRepository: IRepository<Report>
    {
        Task<IEnumerable<Report>> GetAllWithDetailsAsync();
        Task<Report> GetByIdWithDetailsAsync(int id);
    }
}
