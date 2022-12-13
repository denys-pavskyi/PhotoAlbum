using BuisnessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Interfaces
{
    public interface IReportService: ICrud<ReportModel>
    {
        Task<IEnumerable<ReportModel>> GetReportsOnReview();
        Task<IEnumerable<ReportModel>> GetReportsCompleted();
    }
}
