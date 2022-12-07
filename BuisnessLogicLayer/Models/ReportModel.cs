using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public ReportStatus Status { get; set; }

    }
}
