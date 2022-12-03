using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class PhotoRating: BaseEntity
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhotoId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required, Range(0d,10d)]
        public double Grade { get; set; }

        [Required]
        public DateTime RatingDate { get; set; }


        public Photo Photo { get; set; }
        public User User { get; set; }
    }
}
