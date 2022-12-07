using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Report: BaseEntity
    {
        [StringLength(200)]
        public string Comment { get; set; }

        [Required]
        public int PhotoId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public ReportStatus Status { get; set; }

        public User User { get; set; }
        public Photo Photo { get; set; }

    }



    public enum ReportStatus
    {
        Approved,
        Declined,
        OnReview
    }
}
